using System;
using System.Linq;
using System.Collections.Generic;

using AutoMapper;

using TotalModel.Models;

using TotalDTO.Commons;
using TotalDTO.Productions;
using TotalDTO.Sales;
using TotalDTO.Inventories;

using TotalSmartCoding.ViewModels.Productions;
using TotalSmartCoding.ViewModels.Sales;
using TotalSmartCoding.ViewModels.Inventories;
using TotalSmartCoding.ViewModels.Commons;
using TotalDTO.Generals;
using TotalSmartCoding.ViewModels.Generals;

namespace TotalSmartCoding.Libraries
{
    public static class AutoMapperConfig
    {
        public static void SetupMappings()
        {
            ////////https://github.com/AutoMapper/AutoMapper/wiki/Static-and-Instance-API

            Mapper.Initialize(cfg =>
            {
               



                cfg.CreateMap<Pickup, PickupViewModel>();
                cfg.CreateMap<Pickup, PickupDTO>();
                cfg.CreateMap<PickupPrimitiveDTO, Pickup>();
                cfg.CreateMap<PickupViewDetail, PickupDetailDTO>();
                cfg.CreateMap<PickupDetailDTO, PickupDetail>();

                cfg.CreateMap<GoodsReceipt, GoodsReceiptViewModel>();
                cfg.CreateMap<GoodsReceipt, GoodsReceiptDTO>();
                cfg.CreateMap<GoodsReceiptPrimitiveDTO, GoodsReceipt>();
                cfg.CreateMap<GoodsReceiptViewDetail, GoodsReceiptDetailDTO>();
                cfg.CreateMap<GoodsReceiptDetailDTO, GoodsReceiptDetail>();


                cfg.CreateMap<Forecast, ForecastViewModel>();
                cfg.CreateMap<Forecast, ForecastDTO>();
                cfg.CreateMap<ForecastPrimitiveDTO, Forecast>();
                cfg.CreateMap<ForecastViewDetail, ForecastDetailDTO>();
                cfg.CreateMap<ForecastDetailDTO, ForecastDetail>();

                cfg.CreateMap<SalesOrder, SalesOrderViewModel>();
                cfg.CreateMap<SalesOrder, SalesOrderDTO>();
                cfg.CreateMap<SalesOrderPrimitiveDTO, SalesOrder>();
                cfg.CreateMap<SalesOrderViewDetail, SalesOrderDetailDTO>();
                cfg.CreateMap<SalesOrderDetailDTO, SalesOrderDetail>();


                cfg.CreateMap<DeliveryAdvice, DeliveryAdviceViewModel>();
                cfg.CreateMap<DeliveryAdvice, DeliveryAdviceDTO>();
                cfg.CreateMap<DeliveryAdvicePrimitiveDTO, DeliveryAdvice>();
                cfg.CreateMap<DeliveryAdviceViewDetail, DeliveryAdviceDetailDTO>();
                cfg.CreateMap<DeliveryAdviceDetailDTO, DeliveryAdviceDetail>();


                cfg.CreateMap<TransferOrder, TransferOrderViewModel>();
                cfg.CreateMap<TransferOrder, TransferOrderDTO>();
                cfg.CreateMap<TransferOrderPrimitiveDTO, TransferOrder>();
                cfg.CreateMap<TransferOrderViewDetail, TransferOrderDetailDTO>();
                cfg.CreateMap<TransferOrderDetailDTO, TransferOrderDetail>();


                cfg.CreateMap<GoodsIssue, GoodsIssueViewModel>();
                cfg.CreateMap<GoodsIssue, GoodsIssueDTO>();
                cfg.CreateMap<GoodsIssuePrimitiveDTO, GoodsIssue>();
                cfg.CreateMap<GoodsIssueViewDetail, GoodsIssueDetailDTO>();
                cfg.CreateMap<GoodsIssueDetailDTO, GoodsIssueDetail>();



                cfg.CreateMap<WarehouseAdjustment, WarehouseAdjustmentViewModel>();
                cfg.CreateMap<WarehouseAdjustment, WarehouseAdjustmentDTO>();
                cfg.CreateMap<WarehouseAdjustmentPrimitiveDTO, WarehouseAdjustment>();
                cfg.CreateMap<WarehouseAdjustmentViewDetail, WarehouseAdjustmentDetailDTO>();
                cfg.CreateMap<WarehouseAdjustmentDetailDTO, WarehouseAdjustmentDetail>();


                cfg.CreateMap<FillingLine, FillingLineViewModel>();
                cfg.CreateMap<FillingLine, FillingLineDTO>();
                cfg.CreateMap<FillingLinePrimitiveDTO, FillingLine>();
                cfg.CreateMap<FillingLineDetail, FillingLineDetailDTO>();
                cfg.CreateMap<FillingLineDetailDTO, FillingLineDetail>();

                cfg.CreateMap<Batch, BatchViewModel>();
                cfg.CreateMap<Batch, BatchDTO>();
                cfg.CreateMap<BatchPrimitiveDTO, Batch>();

                cfg.CreateMap<BatchIndex, FillingData>();
                


                cfg.CreateMap<Pack, PackViewModel>();
                cfg.CreateMap<Pack, PackDTO>();
                cfg.CreateMap<Pack, BarcodeDTO>();
                cfg.CreateMap<PackPrimitiveDTO, Pack>();


                cfg.CreateMap<Carton, CartonViewModel>();
                cfg.CreateMap<Carton, CartonDTO>();
                cfg.CreateMap<Carton, BarcodeDTO>();
                cfg.CreateMap<CartonPrimitiveDTO, Carton>();
                cfg.CreateMap<CartonAttribute, CartonDTO>();

                cfg.CreateMap<Pallet, PalletViewModel>();
                cfg.CreateMap<Pallet, PalletDTO>();
                cfg.CreateMap<Pallet, BarcodeDTO>();
                cfg.CreateMap<PalletPrimitiveDTO, Pallet>();


                cfg.CreateMap<Commodity, CommodityViewModel>();
                cfg.CreateMap<Commodity, CommodityDTO>();
                cfg.CreateMap<CommodityPrimitiveDTO, Commodity>();

                cfg.CreateMap<CommodityType, CommodityTypeViewModel>();
                cfg.CreateMap<CommodityType, CommodityTypeDTO>();
                cfg.CreateMap<CommodityTypePrimitiveDTO, CommodityType>();

                cfg.CreateMap<CommodityCategory, CommodityCategoryViewModel>();
                cfg.CreateMap<CommodityCategory, CommodityCategoryDTO>();
                cfg.CreateMap<CommodityCategoryPrimitiveDTO, CommodityCategory>();

                cfg.CreateMap<CommoditySetting, CommoditySettingViewModel>();
                cfg.CreateMap<CommoditySetting, CommoditySettingDTO>();
                cfg.CreateMap<CommoditySettingPrimitiveDTO, CommoditySetting>();
                cfg.CreateMap<CommoditySettingDetail, CommoditySettingDetailDTO>();
                cfg.CreateMap<CommoditySettingDetailDTO, CommoditySettingDetail>();

                //cfg.CreateMap<Employee, EmployeeBaseDTO>();
                cfg.CreateMap<Employee, EmployeeViewModel>();
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<EmployeePrimitiveDTO, Employee>();

                cfg.CreateMap<Customer, CustomerBaseDTO>();
                cfg.CreateMap<Customer, CustomerViewModel>();
                cfg.CreateMap<Customer, CustomerDTO>();
                cfg.CreateMap<CustomerPrimitiveDTO, Customer>();

                cfg.CreateMap<CustomerType, CustomerTypeViewModel>();
                cfg.CreateMap<CustomerType, CustomerTypeDTO>();
                cfg.CreateMap<CustomerTypePrimitiveDTO, CustomerType>();

                cfg.CreateMap<CustomerCategory, CustomerCategoryViewModel>();
                cfg.CreateMap<CustomerCategory, CustomerCategoryDTO>();
                cfg.CreateMap<CustomerCategoryPrimitiveDTO, CustomerCategory>();

                cfg.CreateMap<Team, TeamViewModel>();
                cfg.CreateMap<Team, TeamDTO>();
                cfg.CreateMap<TeamPrimitiveDTO, Team>();

                cfg.CreateMap<Territory, TerritoryViewModel>();
                cfg.CreateMap<Territory, TerritoryDTO>();
                cfg.CreateMap<TerritoryPrimitiveDTO, Territory>();

                cfg.CreateMap<BinLocation, BinLocationViewModel>();
                cfg.CreateMap<BinLocation, BinLocationDTO>();
                cfg.CreateMap<BinLocationPrimitiveDTO, BinLocation>();

                cfg.CreateMap<Warehouse, WarehouseBaseDTO>();
                cfg.CreateMap<Warehouse, WarehouseViewModel>();
                cfg.CreateMap<Warehouse, WarehouseDTO>();
                cfg.CreateMap<WarehousePrimitiveDTO, Warehouse>();
                
                cfg.CreateMap<WarehouseAdjustmentType, WarehouseAdjustmentTypeBaseDTO>();
                
                cfg.CreateMap<UserAccessControl, UserAccessControlDTO>();
                cfg.CreateMap<UserGroupControl, UserGroupControlDTO>();
                cfg.CreateMap<UserGroupReport, UserGroupReportDTO>();

                cfg.CreateMap<Report, ReportViewModel>();
                cfg.CreateMap<Report, ReportDTO>();
                cfg.CreateMap<ReportPrimitiveDTO, Report>();

                cfg.CreateMap<ColumnMapping, ColumnMappingDTO>();
            });
        }
    }
}