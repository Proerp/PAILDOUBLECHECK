using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using Ninject;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Helpers;
using TotalCore.Repositories;

using TotalSmartCoding.Libraries;
using TotalSmartCoding.Views.Mains;
using TotalSmartCoding.Views.Productions;
using TotalSmartCoding.Views.Inventories.Pickups;
using TotalSmartCoding.Views.Inventories.GoodsIssues;
using TotalSmartCoding.Libraries.Helpers;
using TotalCore.Extensions;


namespace TotalSmartCoding
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static Mutex mutex = null;
        [STAThread]
        static void Main()
        {
            const string applicationName = "TotalSmartCodingSolution"; bool createdNew;
            mutex = new Mutex(true, applicationName, out createdNew);
            if (!createdNew) { return; }   //app is already running! Exiting the application  

            Registries.ProductName = Application.ProductName.ToUpper();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            AutoMapperConfig.SetupMappings();

            //string ApplicationRoleRequired = "false"; //COMMENT ON 11-JUL-2018: NOT USE ApplicationRoleRequired. JUST REMOVE THIS COMMENT ONLY -> TO USE ApplicationRoleRequired (GET ApplicationRoleRequired OPTION FROM CONFIG SETTING BY THE FOLLOWING CommonConfigs.ReadSetting("ApplicationRoleRequired")).
            string ApplicationRoleRequired = CommonConfigs.ReadSetting("ApplicationRoleRequired"); 
            ApplicationRoles.Required = true; ApplicationRoles.Name = ""; ApplicationRoles.Password = ""; bool applicationRoleRequired = false;
            if (bool.TryParse(ApplicationRoleRequired, out applicationRoleRequired))
                ApplicationRoles.Required = applicationRoleRequired;


            TrialConnects trialConnects = new TrialConnects();
            DialogResult trialConnectResult = trialConnects.Connected();
            if (trialConnectResult == DialogResult.Yes)
            {
                Logon logon = new Logon();

                if (logon.ShowDialog() == DialogResult.OK)
                {
                    if (!GlobalEnums.CBPP)
                    {
                        LegalNotice legalNotice = new LegalNotice();
                        legalNotice.ShowDialog(); legalNotice.Dispose();
                    }

                    if (GlobalVariables.FillingLineID == GlobalVariables.FillingLine.Smallpack || GlobalVariables.FillingLineID == GlobalVariables.FillingLine.Pail || GlobalVariables.FillingLineID == GlobalVariables.FillingLine.Medium4L || GlobalVariables.FillingLineID == GlobalVariables.FillingLine.Import || GlobalVariables.FillingLineID == GlobalVariables.FillingLine.Drum)
                        Application.Run(new MasterMDI(GlobalEnums.NmvnTaskID.SmartCoding, new SmartCoding()));
                    else
                    {
                        if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pickup)
                            Application.Run(new MasterMDI(GlobalEnums.NmvnTaskID.Pickups, new Pickups()));
                        else if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.GoodsIssue)
                            Application.Run(new MasterMDI(GlobalEnums.NmvnTaskID.GoodsIssues, new GoodsIssues()));
                        else
                            Application.Run(new MasterMDI());
                    }
                }
                logon.Dispose();
            }
            else
                if (trialConnectResult == DialogResult.No)
                {
                    if (ApplicationRoles.Required)
                    {
                        ConnectServer connectServer = new ConnectServer(false);
                        connectServer.ShowDialog(); connectServer.Dispose();
                    }
                    else
                        if (CustomMsgBox.Show(new Form(), "Do you want to specify new application role and password?", "Warning", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                        {
                            ConnectServer connectServer = new ConnectServer(true);
                            connectServer.ShowDialog(); connectServer.Dispose();
                        }
                }
        }

    }

    public class TrialConnects
    {
        public DialogResult Connected()
        {
            try
            {
                IBaseRepository baseRepository = CommonNinject.Kernel.Get<IBaseRepository>();
                if (ApplicationRoles.Required) baseRepository.GetApplicationRoles();

                baseRepository.GetWebapis();

                return baseRepository.GetVersionID((int)GlobalVariables.FillingLine.None) != null ? DialogResult.Yes : DialogResult.No;
            }
            catch (Exception exception)
            {
                ApplicationRoles.ExceptionMessage = exception.Message;
                ExceptionHandlers.ShowExceptionMessageBox(new Form(), exception);
                return DialogResult.No;
            }
        }
    }
}
