﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;


using TotalModel.Validations;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Common;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.Entity;
using TotalModel.Helpers;

namespace TotalModel.Models
{

    public partial class TotalSmartCodingEntities
    {
        public override int SaveChanges()
        {
            IEnumerable<DbEntityValidationResult> errors = new List<DbEntityValidationResult>();
            if (this.Configuration.ValidateOnSaveEnabled)
            {
                errors = this.GetValidationErrors();
            }
            if (!errors.Any())
            {
                try
                {
                    //base.ChangeTracker.DetectChanges();
                    return base.SaveChanges();
                }
                catch (OptimisticConcurrencyException concurrencyException)
                {
                    throw new DatabaseConcurrencyException("Someone else has edited the entity in the same time of you. Please refresh and save again.", concurrencyException);
                }
                catch (DBConcurrencyException concurrencyException)
                {
                    throw new DatabaseConcurrencyException("Someone else has edited the entity in the same time of you. Please refresh and save again.", concurrencyException);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw new DatabaseConcurrencyException("Someone else has edited the entity in the same time of you. Please refresh and save again.", e);
                }
                catch (DbEntityValidationException ex)
                {
                    throw new DatabaseValidationErrors(ex.EntityValidationErrors);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                throw new DatabaseValidationErrors(errors);
            }

        }

    }



    #region APPLICATION ROLE
    /// <summary>
    /// EF6 with Application Roles: BY:crmckenzie/EF6-AppRole.cs
    /// https://gist.github.com/crmckenzie/f19df419453bd12adaa1
    /// </summary>

    /// <summary>
    /// 
    /// Intercept Database Command in Entity Framework: 
    /// Configure the interceptor in app.config file, as shown below:
    /// <entityFramework>
    ///     <interceptors>
    ///         <interceptor type="EF6DBFirstTutorials.EFCommandInterceptor, EF6DBFirstTutorials">
    ///         </interceptor>
    ///     </interceptors>
    /// </entityFramework>
    /// 
    /// If you use code-based configuration, then configure it as shown below.
    /// public class FE6CodeConfig : DbConfiguration
    ///{
    ///    public FE6CodeConfig()
    ///    {
    ///        this.AddInterceptor(new EFCommandInterceptor());
    ///    }
    ///}
    /// </summary>


    //OTHER LINK TO READ:
    //How to enable SQL Application Role via Entity Framework
    //https://stackoverflow.com/questions/13048888/how-to-enable-sql-application-role-via-entity-framework/41679548#41679548
    //I have just implemented this code using an IDbConnectionInterceptor in EF6 and works a treat. Call SetAppRole in the Opened method and UnSetAppRole in the Closing method. Thanks. – Duncan Ho

    ////MS SQL Server : Applicaton Role
    ////https://github.com/aspnet/EntityFrameworkCore/issues/4234

    ////Securing Data With Application Role
    ////https://www.codeproject.com/Articles/63894/Securing-Data-With-Application-Role



    public class FE6CodeConfig : DbConfiguration
    {
        public FE6CodeConfig()
        {
            if (ApplicationRoles.Required)
                this.AddInterceptor(new DbConnectionApplicationRoleInterceptor());
        }
    }

    public class DbConnectionApplicationRoleInterceptor : IDbConnectionInterceptor
    {
        private string _appRole;
        private string _password;
        private byte[] _cookie;

        public DbConnectionApplicationRoleInterceptor()
            : this(ApplicationRoles.Name, ApplicationRoles.Password)
        {
        }

        public DbConnectionApplicationRoleInterceptor(string appRole, string password)
        {
            _appRole = appRole;
            _password = password;
        }

        public void Opened(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            try
            {
                Debug.WriteLine("Connection Opened.");
                ApplicationRoles.ExceptionMessage = "";

                if (_appRole == "" && ApplicationRoles.Name != "") { _appRole = ApplicationRoles.Name; _password = ApplicationRoles.Password; }

                if (connection.State != ConnectionState.Open) return;
                if (_appRole != "") ActivateApplicationRole(connection, _appRole, _password);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                ApplicationRoles.ExceptionMessage = e.Message;
            }
        }

        public void Closing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            Debug.WriteLine("Connection Closing.");
            if (connection.State != ConnectionState.Open) return;
            if (_appRole != "") DeActivateApplicationRole(connection, _cookie);
        }

        public virtual void ActivateApplicationRole(DbConnection dbConn, string appRoleName, string password)
        {
            if (dbConn == null)
                throw new ArgumentNullException("DbConnection");
            if (ConnectionState.Open != dbConn.State)
                throw new InvalidOperationException("DBConnection must be opened before activating application role");
            if (string.IsNullOrWhiteSpace(appRoleName))
                throw new ArgumentNullException("appRoleName");
            if (password == null)
                throw new ArgumentNullException("password");
            SetApplicationRole(dbConn, appRoleName, password);
        }

        private string GetCurrentUserName(DbConnection dbConn)
        {
            using (var cmd = dbConn.CreateCommand())
            {
                cmd.CommandText = "SELECT USER_NAME();";
                return (string)cmd.ExecuteScalar();
            }
        }

        private void SetApplicationRole(DbConnection dbConn, string appRoleName, string password)
        {
            var currentUser = GetCurrentUserName(dbConn);
            using (var cmd = dbConn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_setapprole";
                cmd.Parameters.Add(new SqlParameter("@rolename", appRoleName));
                cmd.Parameters.Add(new SqlParameter("@password", password));
                cmd.Parameters.Add(new SqlParameter("@fCreateCookie", SqlDbType.Bit) { Value = true });
                var cookie = new SqlParameter("@cookie", SqlDbType.Binary, 50)
                {
                    Direction = ParameterDirection.InputOutput
                };

                cmd.Parameters.Add(cookie);

                cmd.ExecuteNonQuery();

                if (cookie.Value == null)
                {
                    throw new InvalidOperationException(
                        "Failed to set application role, verify the database is configure correctly and the application role name / passwordis valid.");
                }

                _cookie = (byte[])cookie.Value;
            }

            var appUserName = GetCurrentUserName(dbConn);
            //The new user name should be the application role and not the app pool account.

            if (string.Compare(currentUser, appUserName, true) == 0)
            {
                throw new InvalidOperationException(
                    "Failed to set MediaTypeNames.Application Role, verify the app role is configure correctly or the web configuration is valid.");
            }
        }

        public virtual void DeActivateApplicationRole(DbConnection dbConn, byte[] cookie)
        {
            try
            {                

                using (var cmd = dbConn.CreateCommand())
                {
                    cmd.CommandText = "sp_unsetapprole";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@cookie", SqlDbType.VarBinary, 50) { Value = cookie });
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);                
            }
        }

        #region Other DbConnection Interception

        public void BeganTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
        {
            Debug.WriteLine("Transaction Began.");
        }

        public void BeginningTransaction(DbConnection connection,
            BeginTransactionInterceptionContext interceptionContext)
        {
            Debug.WriteLine("Transaction BeginningTransaction.");
        }

        public void Closed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            Debug.WriteLine("Connection Closed.");
        }

        public void ConnectionStringGetting(DbConnection connection,
            DbConnectionInterceptionContext<string> interceptionContext)
        {
            Debug.WriteLine("Connection ConnectionStringGetting.");
        }

        public void ConnectionStringGot(DbConnection connection,
            DbConnectionInterceptionContext<string> interceptionContext)
        {
            Debug.WriteLine("Connection ConnectionStringGot.");
        }

        public void ConnectionStringSet(DbConnection connection,
            DbConnectionPropertyInterceptionContext<string> interceptionContext)
        {
            Debug.WriteLine("Connection ConnectionStringSet.");
        }

        public void ConnectionStringSetting(DbConnection connection,
            DbConnectionPropertyInterceptionContext<string> interceptionContext)
        {
            Debug.WriteLine("Connection ConnectionStringSetting.");
        }

        public void ConnectionTimeoutGetting(DbConnection connection,
            DbConnectionInterceptionContext<int> interceptionContext)
        {
            Debug.WriteLine("Connection ConnectionTimeoutGetting.");
        }

        public void ConnectionTimeoutGot(DbConnection connection,
            DbConnectionInterceptionContext<int> interceptionContext)
        {
            Debug.WriteLine("Connection ConnectionTimeoutGot.");
        }

        public void DataSourceGetting(DbConnection connection,
            DbConnectionInterceptionContext<string> interceptionContext)
        {
            Debug.WriteLine("Connection DataSourceGetting.");
        }

        public void DataSourceGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            Debug.WriteLine("Connection DataSourceGot.");
        }

        public void DatabaseGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            Debug.WriteLine("Connection DatabaseGetting.");
        }

        public void DatabaseGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            Debug.WriteLine("Connection DatabaseGot.");
        }

        public void Disposed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            Debug.WriteLine("Connection Disposed.");
        }

        public void Disposing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            Debug.WriteLine("Connection Disposing.");
        }

        public void EnlistedTransaction(DbConnection connection,
            EnlistTransactionInterceptionContext interceptionContext)
        {
            Debug.WriteLine("Connection EnlistedTransaction.");
        }

        public void EnlistingTransaction(DbConnection connection,
            EnlistTransactionInterceptionContext interceptionContext)
        {
            Debug.WriteLine("Connection EnlistingTransaction.");
        }

        public void Opening(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            Debug.WriteLine("Connection Opening.");
        }

        public void ServerVersionGetting(DbConnection connection,
            DbConnectionInterceptionContext<string> interceptionContext)
        {
            Debug.WriteLine("Connection ServerVersionGetting.");
        }

        public void ServerVersionGot(DbConnection connection,
            DbConnectionInterceptionContext<string> interceptionContext)
        {
            Debug.WriteLine("Connection ServerVersionGot.");
        }

        public void StateGetting(DbConnection connection,
            DbConnectionInterceptionContext<ConnectionState> interceptionContext)
        {
            Debug.WriteLine("Connection StateGetting.");
        }

        public void StateGot(DbConnection connection,
            DbConnectionInterceptionContext<ConnectionState> interceptionContext)
        {
            Debug.WriteLine("Connection StateGot.");
        }

        #endregion
    }

    #endregion APPLICATION ROLE
}
