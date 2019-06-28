﻿using System;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Data.Entity;

using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories;





using TotalModel; //for Loading (09/07/2015) - let review and optimize Loading laster





namespace TotalDAL.Repositories
{
    public class GenericRepository<TEntity> : BaseRepository, IGenericRepository<TEntity>
        where TEntity : class, IAccessControlAttribute //IAccessControlAttribute: for Loading (09/07/2015) - let review and optimize Loading laster
    {
        private DbSet<TEntity> modelDbSet = null;

        private readonly string functionNameEditable;
        private readonly string functionNameApproved;
        private readonly string functionNameDeletable;
        private readonly string functionNameVoidable;

        public GenericRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : this(totalSmartCodingEntities, null) { }

        public GenericRepository(TotalSmartCodingEntities totalSmartCodingEntities, string functionNameEditable)
            : this(totalSmartCodingEntities, functionNameEditable, null) { }

        public GenericRepository(TotalSmartCodingEntities totalSmartCodingEntities, string functionNameEditable, string functionNameApproved)
            : this(totalSmartCodingEntities, functionNameEditable, functionNameApproved, null) { }

        public GenericRepository(TotalSmartCodingEntities totalSmartCodingEntities, string functionNameEditable, string functionNameApproved, string functionNameDeletable)
            : this(totalSmartCodingEntities, functionNameEditable, functionNameApproved, functionNameDeletable, null) { }

        public GenericRepository(TotalSmartCodingEntities totalSmartCodingEntities, string functionNameEditable, string functionNameApproved, string functionNameDeletable, string functionNameVoidable)
            : base(totalSmartCodingEntities)
        {
            modelDbSet = this.TotalSmartCodingEntities.Set<TEntity>();

            this.functionNameEditable = functionNameEditable;
            this.functionNameApproved = functionNameApproved;
            this.functionNameDeletable = functionNameDeletable;
            this.functionNameVoidable = functionNameVoidable;
        }



        public DbContextTransaction BeginTransaction()
        {
            return this.TotalSmartCodingEntities.Database.BeginTransaction();
        }














        public IQueryable<TEntity> GetAll()
        {
            return this.modelDbSet;
        }

        /// <summary>
        /// SEE Find(id) FROM MICROSOFT HELP (MSDN) FOR MORE INFORMATION
        /// Finds an entity with the given primary key values. 
        /// If an entity with the given primary key values exists in the context, then it is returned immediately without making a request to the store. 
        /// Otherwise, a request is made to the store for an entity with the given primary key values and this entity, if found, is attached to the context and returned. If no entity is found in the context or the store, then null is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetByID(int id)
        {
            return this.GetByID(id, false);
        }
        public TEntity GetByID(int id, bool withReload)
        {
            TEntity entity = this.modelDbSet.Find(id);//STEP 1) FOR THIS WINFORM PROJECT => AT FIRST WE NEED TO FIND BY PRIMARYKEY

            if (withReload)
            {
                if (entity != null) this.TotalSmartCodingEntities.Entry(entity).Reload(); // STEP 2) => NEXT: WE RELOAD FROM DATABASE. IF THE Entity HAS BEEN DELETE BY SOMEONE ELSE => THE this.modelDbSet WILL REMOVE Entity FROM ENTITIES CACHE
                entity = this.modelDbSet.Find(id); //STEP 3) FINALLY => WE FIND BY PRIMARYKEY AGAIN. THIS WILL FIND FROM THE CACHE
            }

            return entity;

            ////////IMPORTANT NOTES: WITH MVC, WE DON'T NEED TO DO 3 STEP, BECAUSE: IN MMVC:  THE DbContext TotalSmartCodingEntities WILL BE LOAD FOR EVERY THREAT, SO Find(id) ALWAYS SEARCH FROM DATABASE
        }



        public TEntity GetEntity(params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntity<TEntity>(includes);
        }
        public TEntity GetEntity(bool proxyCreationEnabled, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntity<TEntity>(proxyCreationEnabled, includes);
        }
        public TEntity GetEntity(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntity<TEntity>(predicate, includes);
        }
        public TEntity GetEntity(bool proxyCreationEnabled, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntity<TEntity>(proxyCreationEnabled, predicate, includes);
        }




        public ICollection<TEntity> GetEntities(params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntities<TEntity>(includes);
        }
        public ICollection<TEntity> GetEntities(bool proxyCreationEnabled, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntities<TEntity>(proxyCreationEnabled, includes);
        }
        public ICollection<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntities<TEntity>(predicate, includes);
        }
        public ICollection<TEntity> GetEntities(bool proxyCreationEnabled, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntities<TEntity>(proxyCreationEnabled, predicate, includes);
        }



        public DateTime GetEditLockedDate(int? locationID, GlobalEnums.NmvnTaskID nmvnTaskID)
        {
            DateTime? lockedDate = this.TotalSmartCodingEntities.GetLockedDate(locationID).Single();
            if (lockedDate == null || lockedDate <= new DateTime(2016, 1, 1)) lockedDate = new DateTime(2016, 1, 1);

            return (DateTime)lockedDate;
        }


        public GlobalEnums.AccessLevel GetAccessLevel(int? userID, GlobalEnums.NmvnTaskID nmvnTaskID, int? organizationalUnitID)
        {
            if (userID == null || userID == 0 || (int)nmvnTaskID == 0) return GlobalEnums.AccessLevel.NoAccess;

            int? accessLevel = this.TotalSmartCodingEntities.GetAccessLevel(userID, (int)nmvnTaskID, organizationalUnitID).Single();
            return accessLevel == null || accessLevel == (int)GlobalEnums.AccessLevel.NoAccess ? GlobalEnums.AccessLevel.NoAccess : (accessLevel == (int)GlobalEnums.AccessLevel.Readable ? GlobalEnums.AccessLevel.Readable : (accessLevel == (int)GlobalEnums.AccessLevel.Editable ? GlobalEnums.AccessLevel.Editable : GlobalEnums.AccessLevel.NoAccess));
        }


        public bool GetApprovalPermitted(int? userID, GlobalEnums.NmvnTaskID nmvnTaskID, int? organizationalUnitID)
        {
            if (userID == null || userID == 0 || (int)nmvnTaskID == 0) return false;

            bool? approvalPermitted = this.TotalSmartCodingEntities.GetApprovalPermitted(userID, (int)nmvnTaskID, organizationalUnitID).Single();
            return approvalPermitted == null ? false : (bool)approvalPermitted;
        }

        public bool GetUnApprovalPermitted(int? userID, GlobalEnums.NmvnTaskID nmvnTaskID, int? organizationalUnitID)
        {
            if (userID == null || userID == 0 || (int)nmvnTaskID == 0) return false;

            bool? unApprovalPermitted = this.TotalSmartCodingEntities.GetUnApprovalPermitted(userID, (int)nmvnTaskID, organizationalUnitID).Single();
            return unApprovalPermitted == null ? false : (bool)unApprovalPermitted;
        }

        public bool GetVoidablePermitted(int? userID, GlobalEnums.NmvnTaskID nmvnTaskID, int? organizationalUnitID)
        {
            if (userID == null || userID == 0 || (int)nmvnTaskID == 0) return false;

            bool? voidablePermitted = this.TotalSmartCodingEntities.GetVoidablePermitted(userID, (int)nmvnTaskID, organizationalUnitID).Single();
            return voidablePermitted == null ? false : (bool)voidablePermitted;
        }

        public bool GetUnVoidablePermitted(int? userID, GlobalEnums.NmvnTaskID nmvnTaskID, int? organizationalUnitID)
        {
            if (userID == null || userID == 0 || (int)nmvnTaskID == 0) return false;

            bool? unVoidablePermitted = this.TotalSmartCodingEntities.GetUnVoidablePermitted(userID, (int)nmvnTaskID, organizationalUnitID).Single();
            return unVoidablePermitted == null ? false : (bool)unVoidablePermitted;
        }

        public bool GetShowDiscount(int? userID, GlobalEnums.NmvnTaskID nmvnTaskID)
        {
            if (userID == null || userID == 0 || (int)nmvnTaskID == 0) return false;

            bool? showDiscount = this.TotalSmartCodingEntities.GetShowDiscount(userID, (int)nmvnTaskID).Single();
            return showDiscount == null ? false : (bool)showDiscount;
        }

        public bool GetApproved(int id)
        {
            return this.CheckExisting(id, this.functionNameApproved);
        }

        public bool GetEditable(int id)
        {
            return !this.CheckExisting(id, this.functionNameEditable);
        }

        public bool GetDeletable(int id)
        {
            return !this.CheckExisting(id, this.functionNameDeletable);
        }


        public bool GetVoidable(int id)
        {
            return !this.CheckExisting(id, this.functionNameVoidable);
        }


        public bool CheckExisting(int id, string functionName)
        {
            return this.GetExisting(id, functionName) != null;
        }

        public string GetExisting(int id, string functionName)
        {
            if (id <= 0 || functionName == null || functionName == "")
                return null;
            else
            {
                ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("EntityID", id) };
                var foundEntityID = base.ExecuteFunction<string>(functionName, parameters);

                return foundEntityID.FirstOrDefault();
            }
        }


        public TEntity Add(TEntity entity)
        {
            return this.modelDbSet.Add(entity);
        }

        public TEntity Remove(TEntity entity)
        {
            return this.modelDbSet.Remove(entity);
        }

        public int SaveChanges()
        {
            return this.TotalSmartCodingEntities.SaveChanges();
        }


    }
}
