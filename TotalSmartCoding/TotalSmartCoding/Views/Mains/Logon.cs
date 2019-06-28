using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using Ninject;

using TotalDAL;
using TotalBase;
using TotalBase.Enums;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalSmartCoding.Libraries;
using TotalCore.Repositories.Commons;
using TotalModel.Models;
using TotalDAL.Repositories;
using TotalCore.Repositories;
using System.Data.Entity.Core.Objects;
using System.DirectoryServices.AccountManagement;
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalCore.Repositories.Generals;
using System.Net;
using System.Net.Sockets;
using TotalModel.Helpers;
using TotalDTO;

namespace TotalSmartCoding.Views.Mains
{
    public partial class Logon : Form
    {
        IBaseRepository baseRepository;

        public Logon()
        {
            InitializeComponent();
        }


        public int EmployeeID { get; set; }

        private void PublicApplicationLogon_Load(object sender, EventArgs e)
        {
            #region TEST
            //// List of strings for your names
            //List<string> allUsers = new List<string>();

            //// create your domain context and define the OU container to search in
            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "DOMAINNAME",
            //                                            "OU=SomeOU,dc=YourCompany,dc=com");

            //// define a "query-by-example" principal - here, we search for a UserPrincipal (user)
            //UserPrincipal qbeUser = new UserPrincipal(ctx);

            //// create your principal searcher passing in the QBE principal    
            //PrincipalSearcher srch = new PrincipalSearcher(qbeUser);

            //// find all matches
            //foreach (var found in srch.FindAll())
            //{
            //    // do whatever here - "found" is of type "Principal" - it could be user, group, computer.....          
            //    allUsers.Add(found.DisplayName);
            //}







            //using (var context = new PrincipalContext(ContextType.Domain, "yourdomain.com"))
            //{
            //    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
            //    {
            //        foreach (var result in searcher.FindAll())
            //        {
            //            DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
            //            Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
            //            Console.WriteLine("Last Name : " + de.Properties["sn"].Value);
            //            Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
            //            Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
            //            Console.WriteLine();
            //        }
            //    }
            //}
            //Console.ReadLine();




            ////string plainText = "Lê Minh Hiệp";
            ////// Convert the plain string pwd into bytes
            //////byte[] plainTextBytes = UnicodeEncoding.Unicode.GetBytes(plainText);
            //////System.Security.Cryptography.HashAlgorithm hashAlgo = new System.Security.Cryptography.SHA256Managed();
            //////byte[] hash = hashAlgo.ComputeHash(plainTextBytes);

            ////byte[] data = UnicodeEncoding.Unicode.GetBytes(plainText);
            ////data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            ////String hash = UnicodeEncoding.Unicode.GetString(data);
            //////CustomMsgBox.Show(hash);

            #endregion TEST

            ////JUST FOR 4L FillingLine ONLY. SHOULD REMOVE NEXT TIME
            //CommonConfigs.AddUpdateAppSetting("ConfigID", ((int)GlobalVariables.FillingLine.Import).ToString());

            try
            {
                if (!(int.TryParse(CommonConfigs.ReadSetting("ConfigID"), out GlobalVariables.ConfigID)))
                    throw new Exception("Please check ConfigID value in config file.");

                UserPrincipal currentUserPrincipal = UserPrincipal.Current;
                if (currentUserPrincipal == null || currentUserPrincipal.Sid == null) throw new Exception("Sorry, can not get current user principal!");

                this.baseRepository = CommonNinject.Kernel.Get<IBaseRepository>();

                UserAPIs userAPIs = new UserAPIs(CommonNinject.Kernel.Get<IUserAPIRepository>());
                IList<ActiveUser> activeUsers = userAPIs.GetActiveUsers(GlobalEnums.CBPP ? "S-1-5-21-2058209122-1687518253-2045704780-1001" : currentUserPrincipal.Sid.Value);

                if (activeUsers.Count > 0)
                {
                    if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Smallpack || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pail || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Medium4L || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Import || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Drum)
                    {
                        int? accessLevel = this.baseRepository.TotalSmartCodingEntities.GetAccessLevel(activeUsers[0].UserID, (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.SmartCoding + (int)GlobalVariables.ConfigID, 0).Single();
                        if (accessLevel != (int)TotalBase.Enums.GlobalEnums.AccessLevel.Editable) activeUsers = new List<ActiveUser>();

                        this.comboUserID.Enabled = false;
                    }
                    else
                        this.Height = this.Height - 80;
                }

                if (activeUsers.Count > 0)
                {
                    this.comboSecurityIdentifier.Items.Add(activeUsers[0].UserName);
                    this.comboSecurityIdentifier.SelectedIndex = 0;

                    this.comboUserID.DataSource = activeUsers;
                    this.comboUserID.DisplayMember = CommonExpressions.PropertyName<ActiveUser>(p => p.FullyQualifiedOrganizationalUnitName);
                    this.comboUserID.ValueMember = CommonExpressions.PropertyName<ActiveUser>(p => p.UserID);

                    FillingLineAPIs fillingLineAPIs = new FillingLineAPIs(CommonNinject.Kernel.Get<IFillingLineAPIRepository>());

                    this.comboFillingLineID.DataSource = fillingLineAPIs.GetFillingLineBases();
                    this.comboFillingLineID.DisplayMember = CommonExpressions.PropertyName<FillingLineBase>(p => p.Name);
                    this.comboFillingLineID.ValueMember = CommonExpressions.PropertyName<FillingLineBase>(p => p.FillingLineID);


                    if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Smallpack || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pail || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Medium4L || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Import || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Drum)
                        this.comboFillingLineID.SelectedValue = GlobalVariables.ConfigID;

                    if (!(GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Smallpack || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pail || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Medium4L || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Import || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Drum))
                    {
                        this.labelFillingLineID.Visible = false;
                        this.comboFillingLineID.Visible = false;
                    }

                    if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Drum)
                        this.checkGlobalDrumWithDigit.Visible = true;

                    this.comboComportName.DataSource = System.IO.Ports.SerialPort.GetPortNames();
                    if (this.comboComportName.Items.Count == 0)
                    {
                        this.comboComportName.DataSource = null;
                        this.comboComportName.Items.Add("COM0");
                    }

                    string comportName = CommonConfigs.ReadSetting("ComportName");
                    if (this.comboComportName.Items.IndexOf(comportName) >= 0)
                        this.comboComportName.SelectedIndex = this.comboComportName.Items.IndexOf(comportName);


                    this.buttonDownload.Visible = true;
                    this.buttonLoginRestore.Visible = activeUsers[0].IsDatabaseAdmin;
                    this.buttonConnectServer.Visible = activeUsers[0].IsDatabaseAdmin;
                    this.buttonWebapi.Visible = activeUsers[0].IsDatabaseAdmin;
                    this.buttonApplicationRoleIgnored.Visible = activeUsers[0].IsDatabaseAdmin;
                    this.separatorResetApplicationRole.Visible = activeUsers[0].IsDatabaseAdmin;
                }
                else
                {
                    this.comboSecurityIdentifier.Visible = false;
                    this.comboUserID.Visible = false;
                    this.comboFillingLineID.Visible = false;

                    this.labelUserID.Visible = false;
                    this.labelFillingLineID.Visible = false;
                    this.labelSecurityIdentifier.Text = "\r\n" + "Sorry, user: " + currentUserPrincipal.Name + "\r\n" + "Don't have permission to run this program." + "\r\n" + "\r\n" + "Contact your admin for more information. Thank you!" + "\r\n" + "\r\n" + "\r\n" + "Xin lỗi, bạn chưa được cấp quyền sử dụng phần mềm này.";

                    this.buttonLogin.Visible = false;
                }

                if (ApplicationRoles.ExceptionMessage != null && ApplicationRoles.ExceptionMessage != "")
                {
                    CustomMsgBox.Show(this, ApplicationRoles.ExceptionMessage, "Warning", MessageBoxButtons.OK);
                }
                this.buttonApplicationRoleRequired.Visible = !ApplicationRoles.Required;
                this.buttonApplicationRoleIgnored.Visible = ApplicationRoles.Required;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState == BindingCompleteState.Exception)
            { ExceptionHandlers.ShowExceptionMessageBox(this, e.ErrorText); e.Cancel = true; }
        }

        private void pictureBoxIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.comboFillingLineID.Visible)
            {
                this.labelComportName.Visible = true;
                this.comboComportName.Visible = true;
            }
        }

        private void buttonLoginExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonExit)) { this.DialogResult = DialogResult.Cancel; return; }

                if (this.comboUserID.SelectedIndex >= 0)
                {
                    ActiveUser activeUser = this.comboUserID.SelectedItem as ActiveUser;
                    if (activeUser != null)
                    {
                        ContextAttributes.LocalIPAddress = this.GetLocalIPAddress();
                        ContextAttributes.User = new UserInformation(activeUser.UserID, activeUser.OrganizationalUnitID, activeUser.LocationID, activeUser.LocationName, activeUser.UserName, activeUser.SecurityIdentifier, activeUser.FullyQualifiedUserName, activeUser.IsDatabaseAdmin, new DateTime());

                        if (this.comboFillingLineID.Visible && (this.comboFillingLineID.SelectedIndex < 0 || this.comboComportName.SelectedIndex < 0)) throw new System.ArgumentException("Vui lòng chọn chuyền sản xuất (NOF1, NOF2, NOF...), và chọn đúng cổng COM để chạy phần mềm"); // || (this.comboFillingLineID.Enabled && (GlobalVariables.ProductionLine)this.comboFillingLineID.SelectedValue == GlobalVariables.ProductionLine.SERVER)

                        if (this.comboFillingLineID.Visible)
                        {
                            GlobalVariables.FillingLineID = (GlobalVariables.FillingLine)this.comboFillingLineID.SelectedValue;
                            GlobalVariables.FillingLineCode = ((FillingLineBase)this.comboFillingLineID.SelectedItem).Code;
                            GlobalVariables.FillingLineName = ((FillingLineBase)this.comboFillingLineID.SelectedItem).Name;
                        }
                        else
                            GlobalVariables.FillingLineID = GlobalVariables.FillingLine.None;

                        if (GlobalVariables.FillingLineID == GlobalVariables.FillingLine.Drum)
                            TotalBase.Enums.GlobalEnums.GlobalDrumWithDigit = this.checkGlobalDrumWithDigit.Checked;

                        GlobalVariables.ComportName = (string)this.comboComportName.SelectedValue;

                        CommonConfigs.AddUpdateAppSetting("ConfigID", (GlobalVariables.ConfigID).ToString());
                        CommonConfigs.AddUpdateAppSetting("ComportName", GlobalVariables.ComportName);

                        if (CommonConfigs.ReadSetting("ReportServerUrl") == "") CommonConfigs.AddUpdateAppSetting("ReportServerUrl", GlobalVariables.ReportServerUrl); //INIT NEW Setting IN CONFIG FILE
                        GlobalVariables.ReportServerUrl = CommonConfigs.ReadSetting("ReportServerUrl");

                        this.VersionValidate();

                        #region EMPTY DATABASE
                        if (false && this.checkEmptyData.Checked)
                        {
                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     WarehouseAdjustmentDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('WarehouseAdjustmentDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     WarehouseAdjustments", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('WarehouseAdjustments', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     GoodsIssueTransferDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('GoodsIssueTransferDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     GoodsIssueDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('GoodsIssueDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     GoodsIssues", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('GoodsIssues', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     TransferOrderDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('TransferOrderDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     TransferOrders", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('TransferOrders', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     DeliveryAdviceDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('DeliveryAdviceDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     DeliveryAdvices", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('DeliveryAdvices', RESEED, 0)", new ObjectParameter[] { });


                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     SalesOrderDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('SalesOrderDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     SalesOrders", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('SalesOrders', RESEED, 0)", new ObjectParameter[] { });



                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     GoodsReceiptDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('GoodsReceiptDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     GoodsReceipts", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('GoodsReceipts', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     PickupDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('PickupDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     Pickups", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('Pickups', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     Packs", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('Packs', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     Cartons", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('Cartons', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     Pallets", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('Pallets', RESEED, 0)", new ObjectParameter[] { });

                            //this.baseRepository.ExecuteStoreCommand("DELETE FROM     Batches", new ObjectParameter[] { });
                            //this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('Batches', RESEED, 0)", new ObjectParameter[] { });

                            //this.baseRepository.ExecuteStoreCommand("DELETE FROM     Commodities", new ObjectParameter[] { });
                            //this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('Commodities', RESEED, 0)", new ObjectParameter[] { });
                        }
                        #endregion

                        if (this.baseRepository.AutoUpdates(sender.Equals(this.buttonLoginRestore)))
                            this.DialogResult = DialogResult.OK;
                        else
                        {
                            CustomMsgBox.Show(this, "The program on this computer must be updated to the latest version." + "\r\n" + "\r\n" + "Contact your administrator for more information.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            this.buttonDownload_Click(this.buttonDownload, new EventArgs());
                        }



                        SmartLogDTO.Init();
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);

                this.DialogResult = DialogResult.None;
            }
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            try
            {
                throw new Exception("Please open your program again in order to update new version." + "\r\n" + "\r\n" + "Contact your admin for more information. Thank you!" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "Vui lòng mở lại phần mềm để cập nhật phiên bản mới nhất. Cám ơn.");
            }
            catch (Exception exception)
            {
                CommonConfigs.AddUpdateAppSetting("VersionID", "-9");
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void buttonApplicationRole_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.buttonApplicationRoleRequired) || sender.Equals(this.buttonApplicationRoleIgnored))
            {
                CommonConfigs.AddUpdateAppSetting("ApplicationRoleRequired", sender.Equals(this.buttonApplicationRoleRequired) ? "true" : "false");

                CustomMsgBox.Show(this, "Please open your program again in order to take new effect.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                this.DialogResult = DialogResult.Cancel;
            }
            if (sender.Equals(this.buttonConnectServer))
            {
                ConnectServer connectServer = new ConnectServer(true);
                DialogResult dialogResult = connectServer.ShowDialog(); connectServer.Dispose();
                if (dialogResult == System.Windows.Forms.DialogResult.OK) { CustomMsgBox.Show(this, "Please open your program again in order to take new effect.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); this.DialogResult = DialogResult.Cancel; }
            }
        }

        private bool VersionValidate()
        {
            try
            {
                foreach (GlobalVariables.FillingLine fillingLine in Enum.GetValues(typeof(GlobalVariables.FillingLine)))
                {
                    this.baseRepository.ExecuteStoreCommand("UPDATE Configs SET VersionID = " + GlobalVariables.ConfigVersionID((int)fillingLine) + ", VersionDate = GETDATE() WHERE ConfigID = " + (int)fillingLine + " AND VersionID < " + GlobalVariables.ConfigVersionID((int)fillingLine), new ObjectParameter[] { });
                }


                if (this.baseRepository.VersionValidate(GlobalVariables.ConfigID, GlobalVariables.ConfigVersionID(GlobalVariables.ConfigID)))
                    CommonConfigs.AddUpdateAppSetting("VersionID", GlobalVariables.ConfigVersionID(GlobalVariables.ConfigID).ToString());

                return true;
            }
            catch (Exception exception)
            {
                CommonConfigs.AddUpdateAppSetting("VersionID", "-9");
                throw exception;
            }
        }

        private void comboFillingLineID_Validated(object sender, EventArgs e)
        {
            if (this.comboFillingLineID.SelectedIndex < 0) this.comboFillingLineID.Text = "";
        }



        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void buttonWebapi_Click(object sender, EventArgs e)
        {
            Webapi webapi = new Webapi();
            DialogResult dialogResult = webapi.ShowDialog(); webapi.Dispose();
            if (dialogResult == System.Windows.Forms.DialogResult.OK) { CustomMsgBox.Show(this, "Please open your program again in order to take new effect.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); this.DialogResult = DialogResult.Cancel; }
        }






    }
}
