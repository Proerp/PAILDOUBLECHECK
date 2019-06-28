using System;
using System.Net;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using AutoMapper;

using TotalBase;
using TotalBase.Enums;
using TotalModel;
using TotalDTO;
using TotalCore.Services;

using TotalSmartCoding.ViewModels.Helpers;
using System.ComponentModel;


namespace TotalSmartCoding.Controllers
{
    public abstract class GenericViewDetailController<TEntity, TEntityDetail, TEntityViewDetail, TDto, TPrimitiveDto, TDtoDetail, TViewDetailViewModel> : GenericSimpleController<TEntity, TDto, TPrimitiveDto, TViewDetailViewModel>

        where TEntity : class, IPrimitiveEntity, IBaseEntity, IBaseDetailEntity<TEntityDetail>, new()
        where TEntityDetail : class, IPrimitiveEntity, new()
        where TEntityViewDetail : class
        where TDto : TPrimitiveDto, IBaseDetailEntity<TDtoDetail>
        where TPrimitiveDto : BaseDTO, IPrimitiveEntity, IPrimitiveDTO, new()
        where TDtoDetail : class, IPrimitiveEntity
        where TViewDetailViewModel : TDto, IViewDetailViewModel<TDtoDetail>, new()
    {
        private readonly IGenericWithViewDetailService<TEntity, TEntityDetail, TEntityViewDetail, TDto, TPrimitiveDto, TDtoDetail> genericWithViewDetailService;
        private readonly TViewDetailViewModel viewDetailViewModel;

        public GenericViewDetailController(IGenericWithViewDetailService<TEntity, TEntityDetail, TEntityViewDetail, TDto, TPrimitiveDto, TDtoDetail> genericWithViewDetailService, TViewDetailViewModel viewDetailViewModel)
            : this(genericWithViewDetailService, viewDetailViewModel, false, true)
        { }

        public GenericViewDetailController(IGenericWithViewDetailService<TEntity, TEntityDetail, TEntityViewDetail, TDto, TPrimitiveDto, TDtoDetail> genericWithViewDetailService, TViewDetailViewModel viewDetailViewModel, bool isCreateWizard)
            : this(genericWithViewDetailService, viewDetailViewModel, isCreateWizard, false)
        { }

        public GenericViewDetailController(IGenericWithViewDetailService<TEntity, TEntityDetail, TEntityViewDetail, TDto, TPrimitiveDto, TDtoDetail> genericWithViewDetailService, TViewDetailViewModel viewDetailViewModel, bool isCreateWizard, bool isSimpleCreate)
            : base(genericWithViewDetailService, viewDetailViewModel, isCreateWizard, isSimpleCreate)
        {
            this.genericWithViewDetailService = genericWithViewDetailService;

            this.viewDetailViewModel = viewDetailViewModel;
            this.viewDetailViewModel.ViewDetails.ListChanged += new ListChangedEventHandler(viewDetailViewModel_ListChanged);
        }

        protected void viewDetailViewModel_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.viewDetailViewModel.SetDirty();
        }


        protected override void Init()
        {
            base.Init();
            this.viewDetailViewModel.ViewDetails.Clear();
        }

        protected override TViewDetailViewModel DecorateViewModel(TViewDetailViewModel viewDetailViewModel)
        {
            viewDetailViewModel = base.DecorateViewModel(viewDetailViewModel);
            return this.GetViewDetails(viewDetailViewModel);
        }

        protected virtual TViewDetailViewModel GetViewDetails(TViewDetailViewModel viewDetailViewModel)
        {
            ICollection<TEntityViewDetail> entityViewDetails = this.GetEntityViewDetails(viewDetailViewModel);

            viewDetailViewModel.ViewDetails.RaiseListChangedEvents = false;
            Mapper.Map<ICollection<TEntityViewDetail>, ICollection<TDtoDetail>>(entityViewDetails, viewDetailViewModel.ViewDetails);
            viewDetailViewModel.ViewDetails.RaiseListChangedEvents = true;
            viewDetailViewModel.ViewDetails.ResetBindings();

            return viewDetailViewModel;
        }


        protected virtual ICollection<TEntityViewDetail> GetEntityViewDetails(TViewDetailViewModel viewDetailViewModel)
        {
            ICollection<TEntityViewDetail> entityViewDetails = this.genericWithViewDetailService.GetViewDetails(viewDetailViewModel.GetID());

            return entityViewDetails;
        }








        #region Smart Logs
        public override void AddViewDetailDataLogs(TDto dto, string actionType)
        {
            try
            {
                base.AddViewDetailDataLogs(dto, actionType);
                if (!SmartLogDTO.OnDataLogs) return;// DO NOTHING: IMPORTANT: SmartLogDTO.OnDataLogs WILL BE CHECKED AND SET EVERY TIME CALL AddDataLogs. TO: SHOULD CALL AddDataLogs BEFORE CALL AddViewDetailDataLogs

                List<string> entityPropertyNames = typeof(TEntityDetail).GetProperties().Select(s => s.Name).ToList();
                List<PropertyInfo> propertyInfos = typeof(TDtoDetail).GetProperties().OrderBy(o => o.Name).ToList();

                List<string> entityViewDetailPropertyNames = typeof(TEntityViewDetail).GetProperties().Select(s => s.Name).ToList(); //JUST ADD PROPERTIES RETURNED BY TEntityViewDetail

                if (dto.GetDetails() != null && dto.GetDetails().Count > 0)
                    dto.GetDetails().Each(detailDTO =>
                    {
                        foreach (PropertyInfo propertyInfo in propertyInfos)
                        {
                            if (SmartLogDTO.CheckProperty(propertyInfo.PropertyType.Name, propertyInfo.Name))
                            {
                                if ((!SmartLogDTO.OptionalNames.Contains(propertyInfo.Name) || entityPropertyNames.Contains(propertyInfo.Name)) && entityViewDetailPropertyNames.Contains(propertyInfo.Name))
                                    this.genericWithViewDetailService.AddDataLogs(dto.GetID(), detailDTO.GetID(), dto.EditedDate, dto.NMVNTaskID.ToString(), actionType, typeof(TDtoDetail).Name.Replace("DTO", ""), propertyInfo.Name, (propertyInfo.GetValue(detailDTO) != null ? propertyInfo.GetValue(detailDTO).ToString() : null));
                            }
                        }
                    });
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion Smart Logs
    }
}