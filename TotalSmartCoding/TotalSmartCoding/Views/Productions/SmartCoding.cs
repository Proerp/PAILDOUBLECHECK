using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading;

using Ninject;
using AutoMapper;

using TotalBase;
using TotalBase.Enums;
using TotalCore.Repositories.Productions;
using TotalCore.Services.Productions;
using TotalModel.Models;
using TotalDTO.Productions;

using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;

using TotalSmartCoding.Controllers.Generals;
using TotalSmartCoding.Controllers.Productions;
using TotalSmartCoding.Controllers.APIs.Productions;

using TotalSmartCoding.Views.Commons;
using TotalSmartCoding.Views.Commons.FillingLines;
using TotalSmartCoding.Views.Mains;


namespace TotalSmartCoding.Views.Productions
{
    public partial class SmartCoding : Form, IToolstripMerge
    {
        #region Declaration

        private ScannerAPIs scannerAPIs;

        public readonly FillingData fillingData;


        private PrinterController digitController;
        private PrinterController packController;
        private PrinterController cartonController;
        private PrinterController palletController;

        private ScannerController scannerController;

        private DataServerController dataServerController;



        private Thread digitThread;
        private Thread packThread;
        private Thread cartonThread;
        private Thread palletThread;

        private Thread scannerThread;

        private Thread dataServerThread;

        private Thread backupDataThread;



        delegate void SetTextCallback(string text);
        delegate void propertyChangedThread(object sender, PropertyChangedEventArgs e);

        #endregion Declaration

        #region Contructor & Implement Interface

        public SmartCoding()
        {
            InitializeComponent();

            try
            {
                this.fillingData = new FillingData();

                this.Initialize();

                this.scannerAPIs = new ScannerAPIs(CommonNinject.Kernel.Get<IPackRepository>(), CommonNinject.Kernel.Get<ICartonRepository>(), CommonNinject.Kernel.Get<IPalletRepository>());

                IBatchService batchService = CommonNinject.Kernel.Get<IBatchService>();//ALL PrinterController MUST SHARE THE SAME IBatchService, BECAUSE WE NEED TO LOCK IBatchService IN ORDER TO CORRECTED UPDATE DATA BY IBatchService

                digitController = new PrinterController(batchService, this.fillingData, GlobalVariables.PrinterName.DigitInkjet, GlobalEnums.InstallLaser && this.fillingData.FillingLineID == GlobalVariables.FillingLine.Pail ? true : false);
                packController = new PrinterController(batchService, this.fillingData, GlobalVariables.PrinterName.PackInkjet);
                cartonController = new PrinterController(batchService, this.fillingData, GlobalVariables.PrinterName.CartonInkjet);
                palletController = new PrinterController(batchService, this.fillingData, GlobalVariables.PrinterName.PalletLabel);

                this.scannerController = new ScannerController(batchService, this.fillingData);
                this.dataServerController = new DataServerController();

                digitController.PropertyChanged += new PropertyChangedEventHandler(controller_PropertyChanged);
                packController.PropertyChanged += new PropertyChangedEventHandler(controller_PropertyChanged);
                cartonController.PropertyChanged += new PropertyChangedEventHandler(controller_PropertyChanged);
                palletController.PropertyChanged += new PropertyChangedEventHandler(controller_PropertyChanged);

                scannerController.PropertyChanged += new PropertyChangedEventHandler(controller_PropertyChanged);
                dataServerController.PropertyChanged += new PropertyChangedEventHandler(controller_PropertyChanged);

                this.fillingData.PropertyChanged += fillingData_PropertyChanged;

                this.textBoxFillingLineName.TextBox.DataBindings.Add("Text", this.fillingData, "FillingLineName");
                this.textBoxSettingDate.TextBox.DataBindings.Add("Text", this.fillingData, "SettingDateShortDateFormat");
                this.textBoxCommodityCode.TextBox.DataBindings.Add("Text", this.fillingData, "CommodityCode");
                this.textBoxCommodityAPICode.TextBox.DataBindings.Add("Text", this.fillingData, "CommodityOfficialCode"); //CommodityAPICode
                this.textBoxCommodityOfficialCode.TextBox.DataBindings.Add("Text", this.fillingData, "CommodityOfficialCode");
                this.textBoxBatchCode.TextBox.DataBindings.Add("Text", this.fillingData, "BatchCode");
                this.textNextDigitNo.TextBox.DataBindings.Add("Text", this.fillingData, "NextDigitNo");
                this.textNextPackNo.TextBox.DataBindings.Add("Text", this.fillingData, "NextPackNo");
                this.textNextCartonNo.TextBox.DataBindings.Add("Text", this.fillingData, "NextCartonNo");
                this.textBatchCartonNo.TextBox.DataBindings.Add("Text", this.fillingData, "BatchCartonNo");
                this.textFinalCartonNo.TextBox.DataBindings.Add("Text", this.fillingData, "FinalCartonNo");
                this.textNextPalletNo.TextBox.DataBindings.Add("Text", this.fillingData, "NextPalletNo");
                

                this.textNthCartontoZebra.TextBox.DataBindings.Add("Text", this.fillingData, "NthCartontoZebra", true, DataSourceUpdateMode.OnPropertyChanged);

                this.comboBoxEmptyCarton.ComboBox.Items.AddRange(new string[] { "Ignore empty carton", "Keep empty carton" });
                this.comboBoxEmptyCarton.ComboBox.SelectedIndex = GlobalVariables.IgnoreEmptyCarton ? 0 : 1;
                this.comboBoxEmptyCarton.Enabled = this.fillingData.FillingLineID != GlobalVariables.FillingLine.Pail && this.fillingData.FillingLineID != GlobalVariables.FillingLine.Medium4L && this.fillingData.FillingLineID != GlobalVariables.FillingLine.Import;

                this.comboBoxSendToZebra.ComboBox.Items.AddRange(new string[] { "Stop print label", "Print new label" });
                this.comboBoxSendToZebra.ComboBox.SelectedIndex = GlobalEnums.SendToZebra ? 1 : 0;
                this.comboBoxSendToZebra.Visible = this.fillingData.FillingLineID == GlobalVariables.FillingLine.Drum && !GlobalEnums.DrumWithDigit;
                this.separatorSendToZebra.Visible = this.fillingData.FillingLineID == GlobalVariables.FillingLine.Drum && !GlobalEnums.DrumWithDigit;
                this.buttonSendToZebra.Visible = this.fillingData.FillingLineID == GlobalVariables.FillingLine.Drum && !GlobalEnums.DrumWithDigit;


                if (!fillingData.HasPack) { this.labelNextPackNo.Visible = false; this.textNextPackNo.Visible = false; this.dgvCartonPendingQueue.RowTemplate.Height = fillingData.HasCartonLabel ? 188 : 280; this.dgvCartonQueue.RowTemplate.Height = fillingData.HasCartonLabel ? 188 : 280; this.dgvCartonsetQueue.RowTemplate.Height = fillingData.HasCartonLabel ? 188 : 280; this.labelLEDPack.Visible = false; this.labelLEDCartonIgnore.Visible = false; this.separatorLEDPack.Visible = false; this.separatorLEDCartonIgnore.Visible = false; } //this.labelCommodityNameCarton.Visible = !this.fillingData.CartonViaPalletZebra; 
                if (!fillingData.HasCarton && this.fillingData.FillingLineID != GlobalVariables.FillingLine.Import) { this.labelNextCartonNo.Visible = false; this.textNextCartonNo.Visible = false; this.dgvPalletQueue.RowTemplate.Height = 280; this.dgvPalletPickupQueue.RowTemplate.Height = 280; this.labelLEDCarton.Visible = false; this.labelLEDCartonPending.Visible = false; this.separatorLEDCarton.Visible = false; this.separatorLEDCartonPending.Visible = false; this.labelCommodityNamePallet.Visible = true; }
                if (!fillingData.HasCarton && GlobalEnums.DrumWithDigit) { this.labelNextPalletNo.Visible = false; this.textNextPalletNo.Visible = false; } else { this.labelNextDigitNo.Visible = false; this.textNextDigitNo.Visible = false; }
                if (fillingData.HasCartonLabel) { this.buttonRemoveCartonPending.Visible = false; } //DON'T ALLOW TO RETURN CARTON BACK. MUST TO CARTON SCAN AGAIN.
                if (!this.fillingData.CartonViaPalletZebra) { this.buttonSendCartontoZebra.Visible = false; this.textNthCartontoZebra.Visible = false; this.labelNthCartontoZebra.Visible = false; this.separatorSendCartontoZebra.Visible = false; }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void fillingData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "BatchID" && this.fillingData != null && this.fillingData.BatchID > 0 )
                this.scannerController.Initialize();
        }

        public void Initialize()
        {
            try
            {
                if (GlobalEnums.NoPallet) GlobalEnums.OnTestPalletReceivedNow = true;


                BatchIndex batchIndex = (new BatchAPIs(CommonNinject.Kernel.Get<IBatchAPIRepository>())).GetActiveBatchIndex();
                if (batchIndex != null) Mapper.Map<BatchIndex, FillingData>(batchIndex, this.fillingData);

                string commodityDescription = this.fillingData.CommodityName + "      [Volume: " + this.fillingData.Volume.ToString("N2") + (this.fillingData.HasPack ? ", Carton size: " + this.fillingData.PackPerCarton + " packs" : "") + (this.fillingData.HasCarton ? ", Pallet size: " + this.fillingData.CartonPerPallet + " cartons" : "") + "]";
                this.labelCommodityNamePack.Text = "                                         " + commodityDescription;
                this.labelCommodityNameCarton.Text = "  " + commodityDescription;
                this.labelCommodityNamePallet.Text = "                            " + commodityDescription;

                this.Text = "Printing & Scanning" + (!fillingData.HasPack ? " - " + commodityDescription : "");

                this.buttonCartonNoreadNow.Visible = GlobalEnums.OnTestScanner && GlobalEnums.OnTestCartonNoreadNowVisible;
                this.buttonPalletReceivedNow.Visible = GlobalEnums.NoPallet ? false : GlobalEnums.OnTestScanner;

                this.labelFinalCartonNo.Visible = GlobalEnums.OnTestPrinter; this.textFinalCartonNo.Visible = GlobalEnums.OnTestPrinter;
                this.digitStatusbox.BackColor = GlobalEnums.OnTestPrinter ? SystemColors.ControlDark : SystemColors.Control; this.digitStatusbox.ScrollBars = GlobalEnums.OnTestPrinter ? ScrollBars.None : ScrollBars.Vertical;
                this.packStatusbox.BackColor = this.digitStatusbox.BackColor; this.packStatusbox.ScrollBars = this.digitStatusbox.ScrollBars;
                this.cartonStatusbox.BackColor = this.digitStatusbox.BackColor; this.cartonStatusbox.ScrollBars = this.digitStatusbox.ScrollBars;
                this.palletStatusbox.BackColor = this.digitStatusbox.BackColor; this.palletStatusbox.ScrollBars = this.digitStatusbox.ScrollBars;
                this.scannerStatusbox.BackColor = GlobalEnums.OnTestScanner ? SystemColors.ControlDark : SystemColors.Control; this.scannerStatusbox.ScrollBars = GlobalEnums.OnTestScanner ? ScrollBars.None : ScrollBars.Vertical;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SmartCoding_Load(object sender, EventArgs e)
        {
            try
            {
                this.buttonRemoveCarton.Enabled = !GlobalEnums.DisableRemove;
                this.buttonRemoveCartonset.Enabled = !GlobalEnums.DisableRemove;

                this.buttonDeleteCartonPending.Enabled = !GlobalEnums.DisableRemove;
                this.buttonDeleteAllCartonPending.Enabled = !GlobalEnums.DisableRemove;

                this.splitPendingQueue.SplitterDistance = 1020;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void SmartCoding_Shown(object sender, System.EventArgs e)
        {
            try
            {
                this.scannerController.Initialize();

                if (dataServerThread != null && dataServerThread.IsAlive) dataServerThread.Abort();
                dataServerThread = new Thread(new ThreadStart(dataServerController.ThreadRoutine));

                dataServerThread.Start();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void splitContainerMaster_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.splitContainerPack.SplitterDistance = this.SplitterDistanceQuality();
                this.splitContainerCarton.SplitterDistance = this.SplitterContainerCarton();
                this.splitContainerPallet.SplitterDistance = this.SplitterContainerPallet();



                this.splitDigit.SplitterDistance = this.splitContainerMaster.Panel1.Width / 5;
                this.splitPack.SplitterDistance = this.splitContainerMaster.Panel1.Width / 5;
                this.splitCarton.SplitterDistance = this.splitContainerMaster.Panel1.Width / 5;
                this.splitPallet.SplitterDistance = this.splitContainerMaster.Panel1.Width / 5;
            }
            catch { }
        }

        private void SmartCoding_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (digitThread != null && digitThread.IsAlive) { e.Cancel = true; return; }
                if (packThread != null && packThread.IsAlive) { e.Cancel = true; return; }
                if (cartonThread != null && cartonThread.IsAlive) { e.Cancel = true; return; }
                if (palletThread != null && palletThread.IsAlive) { e.Cancel = true; return; }

                if (scannerThread != null && scannerThread.IsAlive) { e.Cancel = true; return; }

                if (dataServerThread != null && dataServerThread.IsAlive) { dataServerController.StopUpload(); dataServerController.LoopRoutine = false; e.Cancel = true; return; }

                if (backupDataThread != null && backupDataThread.IsAlive) { e.Cancel = true; return; }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void comboBoxEmptyCarton_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GlobalVariables.IgnoreEmptyCarton = this.comboBoxEmptyCarton.ComboBox.SelectedIndex == 0;
                GlobalRegistry.Write("IgnoreEmptyCarton", GlobalVariables.IgnoreEmptyCarton ? "1" : "0");
            }
            catch
            { }
        }


        private void comboBoxSendToZebra_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GlobalEnums.SendToZebra = this.comboBoxSendToZebra.ComboBox.SelectedIndex == 1;
            }
            catch
            { }
        }


        public ToolStrip toolstripChild { get { return this.toolStripChildForm; } }

        private int SplitterDistanceQuality()
        {
            switch (GlobalVariables.FillingLineID)
            {
                case GlobalVariables.FillingLine.Smallpack:
                    return 346; //364 
                case GlobalVariables.FillingLine.Pail:
                    return 0;
                case GlobalVariables.FillingLine.Medium4L:
                    return 0;
                case GlobalVariables.FillingLine.Import:
                    return 0;
                case GlobalVariables.FillingLine.Drum:
                    return 0; //86;
                default:
                    return 1;
            }
        }

        private int SplitterContainerCarton()
        {
            switch (GlobalVariables.FillingLineID)
            {
                case GlobalVariables.FillingLine.Smallpack:
                    return 225;
                case GlobalVariables.FillingLine.Pail:
                    return 458;
                case GlobalVariables.FillingLine.Medium4L:
                    return 458;
                case GlobalVariables.FillingLine.Import:
                    return 458;
                case GlobalVariables.FillingLine.Drum:
                    return 347; //86;
                default:
                    return 1;
            }
        }

        private int SplitterContainerPallet()
        {
            switch (GlobalVariables.FillingLineID)
            {
                case GlobalVariables.FillingLine.Smallpack:
                    return 111;
                case GlobalVariables.FillingLine.Pail:
                    return 344;
                case GlobalVariables.FillingLine.Medium4L:
                    return 344;
                case GlobalVariables.FillingLine.Import:
                    return 344;
                case GlobalVariables.FillingLine.Drum:
                    return 0; //86;
                default:
                    return 1;
            }
        }

        #endregion Contructor & Implement Interface

        #region Toolstrip bar

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                BatchIndex batchIndex = (new BatchAPIs(CommonNinject.Kernel.Get<IBatchAPIRepository>())).GetActiveBatchIndex();
                if (batchIndex != null)
                {
                    if (this.fillingData.IsChange(batchIndex))
                    {
                        Mapper.Map<BatchIndex, FillingData>(batchIndex, this.fillingData);
                        throw new Exception("Số liệu in phun của lô này đã thay đổi." + "\r\n" + "\r\n" + "Vui lòng kiểm tra và xác nhận số liệu trước khi tiếp tục.");
                    }
                    else
                    {
                        GlobalEnums.IOAlarm = false; this.cutStatusBox(true);

                        if (backupDataThread == null || !backupDataThread.IsAlive)
                        {
                            if (digitThread != null && digitThread.IsAlive) digitThread.Abort();
                            digitThread = new Thread(new ThreadStart(digitController.ThreadRoutine));

                            if (packThread != null && packThread.IsAlive) packThread.Abort();
                            packThread = new Thread(new ThreadStart(packController.ThreadRoutine));

                            if (cartonThread != null && cartonThread.IsAlive) cartonThread.Abort();
                            cartonThread = new Thread(new ThreadStart(cartonController.ThreadRoutine));

                            if (palletThread != null && palletThread.IsAlive) palletThread.Abort();
                            palletThread = new Thread(new ThreadStart(palletController.ThreadRoutine));

                            if (scannerThread != null && scannerThread.IsAlive) scannerThread.Abort();
                            scannerThread = new Thread(new ThreadStart(scannerController.ThreadRoutine));


                            digitThread.Start();
                            packThread.Start();
                            cartonThread.Start();
                            palletThread.Start();
                            scannerThread.Start();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                digitController.LoopRoutine = false;
                packController.LoopRoutine = false;
                cartonController.LoopRoutine = false;
                palletController.LoopRoutine = false;
                scannerController.LoopRoutine = false;

                this.setToolStripActive();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                this.cutStatusBox(true);

                this.digitController.StartPrint();
                this.packController.StartPrint();
                this.cartonController.StartPrint();
                this.palletController.StartPrint();
                this.scannerController.StartScanner();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustomMsgBox.Show(this, "Phần mềm đang kết nối hệ thống máy in và đầu đọc mã vạch." + (char)13 + (char)13 + "Bạn có muốn dừng phần mềm không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    if (CustomMsgBox.Show(this, "Bạn thật sự muốn dừng phần mềm?" + (char)13 + (char)13 + "Vui lòng nhấn Yes để dừng phần mềm ngay lập tức.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.StopPrint();
                        this.scannerController.StopScanner();
                    }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void StopPrint()
        {
            this.StopPrint(true, true, true, true);
        }

        private void StopPrint(bool stopDigitInkjet, bool stopPackInkjet, bool stopCartonInkjet, bool stopPalletLabel)
        {
            if (stopDigitInkjet) this.digitController.StopPrint();
            if (stopPackInkjet) this.packController.StopPrint();
            if (stopCartonInkjet) this.cartonController.StopPrint();
            if (stopPalletLabel) this.palletController.StopPrint();
        }

        private void StartUpload()
        {
            try
            {
                if (dataServerThread != null && dataServerThread.IsAlive)
                    dataServerController.StartUpload();
                else
                {
                    dataServerThread = new Thread(new ThreadStart(dataServerController.ThreadRoutine));

                    dataServerThread.Start();
                }
                this.timerEveryUpload = 0;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonSendCartontoZebra_Click(object sender, EventArgs e)
        {
            if (this.digitController.OnPrinting && this.packController.OnPrinting && this.cartonController.OnPrinting && this.palletController.OnPrinting && this.scannerController.OnScanning)
                this.palletController.StartSendCartontoZebra(this.fillingData.NthCartontoZebra);
        }


        private void buttonSetting_Click(object sender, EventArgs e)
        {
            try
            {
                //Debug.Print(this.splitContainerPack.SplitterDistance.ToString());
                //Debug.Print(this.splitContainerCarton.SplitterDistance.ToString());
                //Debug.Print(this.splitContainerPallet.SplitterDistance.ToString());

                GlobalEnums.NmvnTaskID nmvnTaskID = sender.Equals(this.buttonBatches) ? GlobalEnums.NmvnTaskID.Batch : GlobalEnums.NmvnTaskID.FillingLine;
                Form loadedView; if (sender.Equals(this.buttonBatches)) loadedView = new Batches(this, this.scannerController.AllQueueEmpty); else loadedView = new FillingLines();

                MasterMDI masterMDI = new MasterMDI(nmvnTaskID, loadedView, false);

                masterMDI.ShowDialog();

                masterMDI.Dispose();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonReprint_Click(object sender, EventArgs e)
        {
            this.scannerController.Reprint();
        }


        private void buttonPalletReceivedNow_Click(object sender, EventArgs e)
        {
            GlobalEnums.OnTestPalletReceivedNow = true;
        }


        private void buttonCartonNoreadNow_Click(object sender, EventArgs e)
        {
            GlobalEnums.OnTestCartonNoreadNow = true;
        }

        private int timerEveryUpload { get; set; }
        private void timerEverySecond_Tick(object sender, EventArgs e)
        {
            try
            {
                //this.fillingData.SettingDate = DateTime.Now; //NOT CHANGE. KEEP SettingDate AS THE DATE AT LOGON
                if (this.fillingData != null)
                {
                    if (this.fillingData.EntryMonthID != CommonExpressions.GetEntryMonthID())
                        this.iconNewMonth.Visible = !this.iconNewMonth.Visible;
                    else
                        this.iconNewMonth.Visible = false;
                }

                if (this.timerEveryUpload >= 180) this.StartUpload(); else this.timerEveryUpload++;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        #endregion Toolstrip bar

        #region Handler

        private void controller_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                propertyChangedThread propertyChangedDelegate = new propertyChangedThread(propertyChangedHandler);
                this.Invoke(propertyChangedDelegate, new object[] { sender, e });
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void propertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                this.setToolStripActive();

                if (sender.Equals(this.digitController))
                {
                    if (e.PropertyName == "MainStatus") { if (this.digitController.MainStatus != "") { this.digitStatusbox.Text = "[" + DateTime.Now.ToString("hh:mm:ss") + "] " + this.digitController.MainStatus + "\r\n" + this.digitStatusbox.Text; this.cutStatusBox(false); } return; }
                    if (e.PropertyName == "LedStatus") { this.digitLEDGreen.Enabled = this.digitController.LedGreenOn; this.digitLEDAmber.Enabled = this.digitController.LedAmberOn; this.digitLEDRed.Enabled = this.digitController.LedRedOn; if (this.digitController.LedRedOn) { GlobalEnums.IOAlarm = true; this.StopPrint(true, true, this.fillingData.FillingLineID == GlobalVariables.FillingLine.Pail, false); } return; }

                    if (e.PropertyName == "NextDigitNo") { this.fillingData.NextDigitNo = this.digitController.NextDigitNo; return; }
                    if (e.PropertyName == "SentDigitNo") { this.fillingData.SentDigitNo = this.digitController.SentDigitNo; return; }
                }
                else if (sender.Equals(this.packController))
                {
                    if (e.PropertyName == "MainStatus") { if (this.packController.MainStatus != "") { this.packStatusbox.Text = "[" + DateTime.Now.ToString("hh:mm:ss") + "] " + this.packController.MainStatus + "\r\n" + this.packStatusbox.Text; this.cutStatusBox(false); } return; }
                    if (e.PropertyName == "LedStatus") { this.packLEDGreen.Enabled = this.packController.LedGreenOn; this.packLEDAmber.Enabled = this.packController.LedAmberOn; this.packLEDRed.Enabled = this.packController.LedRedOn; if (this.packController.LedRedOn) { GlobalEnums.IOAlarm = true; this.StopPrint(true, true, this.fillingData.FillingLineID == GlobalVariables.FillingLine.Pail, false); } return; }

                    if (e.PropertyName == "NextPackNo") { this.fillingData.NextPackNo = this.packController.NextPackNo; return; }
                    if (e.PropertyName == "SentPackNo") { this.fillingData.SentPackNo = this.packController.SentPackNo; return; }
                }
                else if (sender.Equals(this.cartonController))
                {
                    if (e.PropertyName == "MainStatus") { if (this.cartonController.MainStatus != "") { this.cartonStatusbox.Text = "[" + DateTime.Now.ToString("hh:mm:ss") + "] " + this.cartonController.MainStatus + "\r\n" + this.cartonStatusbox.Text; this.cutStatusBox(false); } return; }
                    if (e.PropertyName == "LedStatus") { this.cartonLEDGreen.Enabled = this.cartonController.LedGreenOn; this.cartonLEDAmber.Enabled = this.cartonController.LedAmberOn; this.cartonLEDRed.Enabled = this.cartonController.LedRedOn; if (this.cartonController.LedRedOn) { GlobalEnums.IOAlarm = true; this.StopPrint(this.fillingData.FillingLineID == GlobalVariables.FillingLine.Pail, this.fillingData.FillingLineID == GlobalVariables.FillingLine.Pail, this.fillingData.FillingLineID == GlobalVariables.FillingLine.Pail, false); } return; }

                    if (e.PropertyName == "NextCartonNo") { this.fillingData.NextCartonNo = this.cartonController.NextCartonNo; return; }
                    if (e.PropertyName == "SentCartonNo") { this.fillingData.SentCartonNo = this.cartonController.SentCartonNo; return; }
                }

                else if (sender.Equals(this.palletController))
                {
                    if (e.PropertyName == "MainStatus") { if (this.palletController.MainStatus != "") { this.palletStatusbox.Text = "[" + DateTime.Now.ToString("hh:mm:ss") + "] " + this.palletController.MainStatus + "\r\n" + this.palletStatusbox.Text; this.cutStatusBox(false); } return; }
                    if (e.PropertyName == "LedStatus") { this.palletLEDGreen.Enabled = this.palletController.LedGreenOn; this.palletLEDAmber.Enabled = this.palletController.LedAmberOn; this.palletLEDRed.Enabled = this.palletController.LedRedOn; if (this.palletController.LedRedOn) { GlobalEnums.IOAlarm = true; this.StopPrint(); } return; }

                    if (e.PropertyName == "NextCartonNo" && this.fillingData.CartonViaPalletZebra) { this.fillingData.NextCartonNo = this.palletController.NextCartonNo; return; }
                    if (e.PropertyName == "NextPalletNo") { this.fillingData.NextPalletNo = this.palletController.NextPalletNo; return; }

                    if (e.PropertyName == "SentCartonNo" && this.fillingData.CartonViaPalletZebra) { this.fillingData.SentCartonNo = this.palletController.SentCartonNo; return; }
                    if (e.PropertyName == "SentPalletNo") { this.fillingData.SentPalletNo = this.palletController.SentPalletNo; return; }
                }

                else if (sender.Equals(this.scannerController))
                {
                    if (e.PropertyName == "MainStatus") { if (this.scannerController.MainStatus != "") { this.scannerStatusbox.Text = "[" + DateTime.Now.ToString("hh:mm:ss") + "] " + this.scannerController.MainStatus + "\r\n" + this.scannerStatusbox.Text; this.cutStatusBox(false); } return; }
                    if (e.PropertyName == "LedStatus") { this.scannerLEDGreen.Enabled = this.scannerController.LedGreenOn; this.scannerLEDAmber.Enabled = this.scannerController.LedAmberOn; this.scannerLEDRed.Enabled = this.scannerController.LedRedOn; if (this.scannerController.LedRedOn) { GlobalEnums.IOAlarm = true; this.StopPrint(); } return; }

                    if (e.PropertyName == "LedMCU") { this.toolStripMCUQuanlity.Enabled = this.scannerController.LedMCUQualityOn; this.toolStripMCUMatching.Enabled = this.scannerController.LedMCUMatchingOn; this.toolStripMCUCarton.Enabled = this.scannerController.LedMCUCartonOn; return; }
                    
                    if (e.PropertyName == "BatchCartonNo") { this.fillingData.BatchCartonNo = this.scannerController.BatchCartonNo; return; }


                    if (e.PropertyName == "PackQueue")
                    {
                        int currentRowIndex = -1; int currentColumnIndex = -1;
                        if (this.dgvPackQueue.CurrentCell != null) { currentRowIndex = this.dgvPackQueue.CurrentCell.RowIndex; currentColumnIndex = this.dgvPackQueue.CurrentCell.ColumnIndex; }

                        this.dgvPackQueue.DataSource = this.scannerController.GetPackQueue();

                        if (currentRowIndex >= 0 && currentRowIndex < this.dgvPackQueue.Rows.Count && currentColumnIndex >= 0 && currentColumnIndex < this.dgvPackQueue.ColumnCount) this.dgvPackQueue.CurrentCell = this.dgvPackQueue[currentColumnIndex, currentRowIndex]; //Keep current cell

                        this.buttonDeleteAllPack.Text = "[" + this.scannerController.PackQueueCount.ToString("N0") + "]";
                        this.buttonPackQueueCount.Text = this.scannerController.NextPackQueueDescription;
                        this.labelLEDPack.Text = this.scannerController.PackQueueCount.ToString("N0");
                    }

                    if (e.PropertyName == "PacksetQueue") { this.dgvPacksetQueue.DataSource = this.scannerController.GetPacksetQueue(); this.buttonPacksetQueueCount.Text = "[" + this.scannerController.PacksetQueueCount.ToString("N0") + "]"; }

                    if (e.PropertyName == "PackIgnoreCount") { this.labelLEDPackIgnore.Text = this.scannerController.PackIgnoreCount != 0 ? this.scannerController.PackIgnoreCount.ToString("N0") : "   "; }
                    if (e.PropertyName == "CartonIgnoreCount") { this.labelLEDCartonIgnore.Text = this.scannerController.CartonIgnoreCount != 0 ? this.scannerController.CartonIgnoreCount.ToString("N0") : "   "; }

                    if (e.PropertyName == "CartonPendingQueue")
                    {
                        this.dgvCartonPendingQueue.DataSource = this.scannerController.GetCartonPendingQueue();

                        if (this.dgvCartonPendingQueue.Rows.Count >= 2) { this.dgvCartonPendingQueue.Rows[0].Height = 92; }
                        if (this.dgvCartonPendingQueue.Rows.Count > 1) this.dgvCartonPendingQueue.CurrentCell = this.dgvCartonPendingQueue.Rows[0].Cells[0];

                        this.buttonCartonPendingQueueCount.Text = "[" + this.scannerController.CartonPendingQueueCount.ToString("N0") + "]";
                        this.labelLEDCartonPending.Text = this.scannerController.CartonPendingQueueCount != 0 ? this.scannerController.CartonPendingQueueCount.ToString("N0") : "    ";
                    }

                    if (e.PropertyName == "CartonQueue")
                    {
                        this.dgvCartonQueue.DataSource = this.scannerController.GetCartonQueue();

                        if (this.dgvCartonQueue.Rows.Count >= 2) { this.dgvCartonQueue.Rows[0].Height = 92; }
                        if (this.dgvCartonQueue.Rows.Count > 1) this.dgvCartonQueue.CurrentCell = this.dgvCartonQueue.Rows[0].Cells[0];

                        this.buttonCartonQueueCount.Text = "[" + this.scannerController.CartonQueueCount.ToString("N0") + "]";
                        this.labelLEDCarton.Text = this.scannerController.CartonQueueCount.ToString("N0");
                    }

                    if (e.PropertyName == "CartonsetQueue")
                    {
                        this.dgvCartonsetQueue.DataSource = this.scannerController.GetCartonsetQueue();

                        if (this.dgvCartonsetQueue.Rows.Count >= 2) { this.dgvCartonsetQueue.Rows[0].Height = 92; }

                        this.buttonCartonsetQueueCount.Text = "[" + this.scannerController.CartonsetQueueCount.ToString("N0") + "]";
                    }

                    if (e.PropertyName == "PalletQueue")
                    {
                        this.dgvPalletQueue.DataSource = this.scannerController.GetPalletQueue();
                        this.buttonPalletQueueCount.Text = "[" + this.scannerController.PalletQueueCount.ToString("N0") + "]";
                        this.labelLEDPallet.Text = this.scannerController.PalletQueueCount.ToString("N0");

                        this.StartUpload();
                    }

                    if (e.PropertyName == "PalletPickupQueue")
                    {
                        this.dgvPalletPickupQueue.DataSource = this.scannerController.GetPalletPickupQueue();
                        this.buttonPalletPickupQueueCount.Text = "[" + this.scannerController.PalletPickupQueueCount.ToString("N0") + "]";
                    }
                }
                else if (sender.Equals(this.dataServerController))
                {
                    if (e.PropertyName == "MainStatus")
                    {
                        if (this.dataServerController.MainStatus != "")
                        {
                            this.buttonCartonAttributes.Text = this.dataServerController.MainStatus;
                            if (this.dataServerController.MainStatus == "Idling" && (scannerThread == null || !scannerThread.IsAlive))
                            {
                                this.scannerController.InitializePallet();
                            }
                        }
                        return;
                    }
                }

            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void setToolStripActive()
        {
            bool anyLoopRoutine = digitController.LoopRoutine | packController.LoopRoutine | cartonController.LoopRoutine | palletController.LoopRoutine | scannerController.LoopRoutine;
            bool allLoopRoutine = digitController.LoopRoutine && packController.LoopRoutine && cartonController.LoopRoutine && palletController.LoopRoutine && scannerController.LoopRoutine;

            bool anyOnPrinting = digitController.OnPrinting | packController.OnPrinting | cartonController.OnPrinting | palletController.OnPrinting | scannerController.OnScanning;
            //bool allOnPrinting = digitInkjetDominoPrinter.OnPrinting && barcodeInkjetDominoPrinter.OnPrinting && cartonInkjetDominoPrinter.OnPrinting  && palletInkjetDominoPrinter.OnPrinting && barcodeScannerMCU.OnPrinting;

            bool allLedGreenOn = digitController.LedGreenOn && packController.LedGreenOn && cartonController.LedGreenOn && palletController.LedGreenOn && scannerController.LedGreenOn;

            this.buttonConnect.Enabled = !anyLoopRoutine;
            this.buttonDisconnect.Enabled = anyLoopRoutine && !anyOnPrinting;
            this.buttonStart.Enabled = allLoopRoutine && !anyOnPrinting && allLedGreenOn;
            this.buttonStop.Enabled = anyOnPrinting;

            this.buttonReprint.Enabled = this.palletController.LedGreenOn && palletController.OnPrinting;

            this.buttonBatches.Enabled = !anyLoopRoutine;



            this.digitLEDGreen.Enabled = digitController.LoopRoutine && this.digitController.LedGreenOn;
            this.packLEDGreen.Enabled = packController.LoopRoutine && this.packController.LedGreenOn;
            this.cartonLEDGreen.Enabled = cartonController.LoopRoutine && this.cartonController.LedGreenOn;
            this.palletLEDGreen.Enabled = palletController.LoopRoutine && this.palletController.LedGreenOn;
            this.scannerLEDGreen.Enabled = scannerController.LoopRoutine && this.scannerController.LedGreenOn;


            this.digitLEDPrinting.Enabled = digitController.OnPrinting && this.digitController.LedGreenOn;
            this.packLEDPrinting.Enabled = packController.OnPrinting && this.packController.LedGreenOn;
            this.cartonLEDPrinting.Enabled = cartonController.OnPrinting && this.cartonController.LedGreenOn;
            this.palletLEDPrinting.Enabled = palletController.OnPrinting && this.palletController.LedGreenOn;
            this.scannerLEDScanning.Enabled = scannerController.OnScanning && this.scannerController.LedGreenOn;
        }

        private void cutStatusBox(bool clearStatusBox)
        {
            if (clearStatusBox)
            {
                this.digitStatusbox.Text = "";
                this.packStatusbox.Text = "";
                this.cartonStatusbox.Text = "";
                this.palletStatusbox.Text = "";
                this.scannerStatusbox.Text = "";
            }
            else
            {
                if (this.digitStatusbox.TextLength > 1000) this.digitStatusbox.Text = this.digitStatusbox.Text.Substring(0, 1000);
                if (this.packStatusbox.TextLength > 1000) this.packStatusbox.Text = this.packStatusbox.Text.Substring(0, 1000);
                if (this.cartonStatusbox.TextLength > 1000) this.cartonStatusbox.Text = this.cartonStatusbox.Text.Substring(0, 1000);
                if (this.palletStatusbox.TextLength > 1000) this.palletStatusbox.Text = this.palletStatusbox.Text.Substring(0, 1000);
                if (this.scannerStatusbox.TextLength > 1000) this.scannerStatusbox.Text = this.scannerStatusbox.Text.Substring(0, 1000);
            }
        }


        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                e.Value = this.GetSerialNumber(e.Value.ToString(), sender);
            }
            catch
            { }
        }

        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)//e.RowIndex == -1 &&
                {
                    e.PaintBackground(e.CellBounds, true);
                    e.Graphics.TranslateTransform(e.CellBounds.Left, e.CellBounds.Bottom);
                    e.Graphics.RotateTransform(270);
                    e.Graphics.DrawString(e.FormattedValue.ToString(), e.CellStyle.Font, Brushes.Black, 5, 5);
                    e.Graphics.ResetTransform();
                    e.Handled = true;
                }
            }
            catch
            { }
        }

        private void dataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dataGridView_Enter(object sender, EventArgs e)
        {
            this.dgvPackQueue.ScrollBars = ScrollBars.Horizontal;
        }

        private void dataGridView_Leave(object sender, EventArgs e)
        {
            this.dgvPackQueue.ScrollBars = ScrollBars.None;
        }

        private string GetSerialNumber(string printedBarcode)
        { return this.GetSerialNumber(printedBarcode, null); }
        private string GetSerialNumber(string printedBarcode, object sender)
        {
            int indexOfDoubleTabChar = printedBarcode.IndexOf(GlobalVariables.doubleTabChar.ToString());
            if (indexOfDoubleTabChar == 0)
                printedBarcode = ""; //10-AUG-2017: WHAT IS GlobalVariables.doubleTabChar.ToString()???
            //else if (printedBarcode.Length > 6) printedBarcode = printedBarcode.Substring(printedBarcode.Length - 7, 6); //Char[3][4][5]...[9]: Serial Number
            else
            {
                if (printedBarcode.Length >= 29)
                {
                    if (this.fillingData.HasPack)
                        printedBarcode = printedBarcode.Substring(indexOfDoubleTabChar - 6, 6);
                    else
                        if (this.fillingData.HasLabel)
                        {
                            if (sender != null && (sender.Equals(this.dgvPalletQueue) || sender.Equals(this.dgvPalletPickupQueue)))
                                printedBarcode = printedBarcode.Substring(indexOfDoubleTabChar - 6, 6);
                            else
                                printedBarcode = printedBarcode.Substring(0, indexOfDoubleTabChar - 7); //printedBarcode.Substring(indexOfDoubleTabChar - 20, 20);
                        }
                        else
                        {
                            if (sender != null && ((this.fillingData.HasCarton || this.fillingData.FillingLineID == GlobalVariables.FillingLine.Import) && (sender.Equals(this.dgvPalletQueue) || sender.Equals(this.dgvPalletPickupQueue))))
                                printedBarcode = printedBarcode.Substring(indexOfDoubleTabChar - 6, 6);
                            else
                                printedBarcode = printedBarcode.Substring(0, indexOfDoubleTabChar);
                        }
                }
                else
                    printedBarcode = printedBarcode.Substring(0, indexOfDoubleTabChar);
            }

            //////--BP CASTROL
            ////if (printedBarcode.IndexOf(GlobalVariables.doubleTabChar.ToString()) == 0) printedBarcode = "";
            //////else if (printedBarcode.Length > 6) printedBarcode = printedBarcode.Substring(printedBarcode.Length - 7, 6); //Char[3][4][5]...[9]: Serial Number
            ////else
            ////    if (printedBarcode.Length >= 29) printedBarcode = printedBarcode.Substring(23, 6); //Char[3][4][5]...[9]: Serial Number
            ////    else if (printedBarcode.Length >= 12) printedBarcode = printedBarcode.Substring(6, 5);

            return printedBarcode;
        }

        #endregion Handler


        #region Exception Handler

        /// <summary>
        /// Find a specific pack number in matching queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPackQueue_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string cellValue = "";
                if (CustomInputBox.Show("BP Filling System", "Please input pack number", ref cellValue) == System.Windows.Forms.DialogResult.OK)
                {
                    for (int rowIndex = 0; rowIndex < this.dgvPackQueue.Rows.Count; rowIndex++)
                    {
                        for (int columnIndex = 0; columnIndex < this.dgvPackQueue.Rows[rowIndex].Cells.Count; columnIndex++)
                        {
                            if (this.GetSerialNumber(this.dgvPackQueue[columnIndex, rowIndex].Value.ToString()).IndexOf(cellValue) != -1)
                            {
                                if (rowIndex >= 0 && rowIndex < this.dgvPackQueue.Rows.Count && columnIndex >= 0 && columnIndex < this.dgvPackQueue.ColumnCount)
                                    this.dgvPackQueue.CurrentCell = this.dgvPackQueue[columnIndex, rowIndex];
                                else
                                    this.dgvPackQueue.CurrentCell = null;
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                this.dgvPackQueue.CurrentCell = null;
            }
        }

        /// <summary>
        /// Remove a specific pack in matching queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeletePack_Click(object sender, EventArgs e)
        {
            if (this.dgvPackQueue.CurrentCell != null)
            {
                try
                {                //Handle exception for PackInOneCarton
                    string selectedBarcode = "";
                    int packID = this.getBarcodeID(this.dgvPackQueue.CurrentCell, out selectedBarcode);
                    if (packID > 0 && CustomMsgBox.Show(this, (sender.Equals(this.buttonDeleteAllPack) ? "Xóa toàn bộ chai đang trên chuyền" : "Xóa chai này:" + (char)13 + (char)13 + selectedBarcode) + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        if (this.scannerController.RemovePackInPackQueue(packID, sender.Equals(this.buttonDeleteAllPack))) CustomMsgBox.Show(this, (sender.Equals(this.buttonDeleteAllPack) ? "Toàn bộ chai đang trên chuyền" : "Pack: " + selectedBarcode) + "\r\n\r\nĐã được xóa thành công.", "Handle exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {
                    ExceptionHandlers.ShowExceptionMessageBox(this, exception);
                }
            }
        }

        /// <summary>
        /// Move Packset To Carton Pending Queue (THE SAME AS NoRead, BUT: WITHOUT READ BY SCANNER)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPackset_Remove(object sender, EventArgs e)
        {
            if (this.dgvPacksetQueue.CurrentCell != null)
            {
                try
                {
                    string selectedBarcode = "";
                    int packID = this.getBarcodeID(this.dgvPacksetQueue.CurrentCell, out selectedBarcode);
                    if (packID > 0 && CustomMsgBox.Show(this, "Thùng carton này chuẩn bị đóng, bạn muốn bỏ ra khỏi chuyền xử lý sau:" + (char)13 + (char)13 + selectedBarcode, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        if (this.scannerController.MovePacksetToCartonPendingQueue(packID)) CustomMsgBox.Show(this, "Thùng carton (chuẩn bị đóng) chứa chai: " + selectedBarcode + "\r\nđã bỏ ra khỏi chuyền.", "Handle exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {
                    ExceptionHandlers.ShowExceptionMessageBox(this, exception);
                }
            }
        }


        private void dgvCarton_Remove(object sender, EventArgs e)
        {
            try
            {
                if (GlobalEnums.DisableRemove) return; //DISABLE REMOVE CARTON AT BP (03/MAR/2019)

                DataGridView dataGridView = sender.Equals(this.buttonRemoveCarton) ? this.dgvCartonQueue : this.dgvCartonsetQueue;
                if (dataGridView != null && dataGridView.CurrentCell != null)
                {
                    string selectedBarcode = "";
                    int barcodeID = this.getBarcodeID(dataGridView.CurrentCell, out selectedBarcode);
                    if (barcodeID > 0 && CustomMsgBox.Show(this, "Are you sure you want to remove this carton:" + (char)13 + (char)13 + selectedBarcode, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        if (this.scannerController.MoveCartonToPendingQueue(barcodeID, dataGridView.Equals(this.dgvCartonsetQueue), true)) CustomMsgBox.Show(this, "Carton: " + selectedBarcode + "\r\nHas been removed successfully.", "Handle exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        /// <summary>
        /// REMOVE: Unwrap a Noread || Pending carton
        /// DELETE: DELETE ALL PACK IN a Noread || Pending carton => USER MUST TO SCAN BY MATCHING SCANNER AGAIN IN ORDER TO MAKE CARTON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCartonPending_RemoveDelete(object sender, EventArgs e)
        {
            if (this.dgvCartonPendingQueue.CurrentCell != null)
            {
                try
                {
                    string selectedBarcode = "";
                    int cartonID = this.getBarcodeID(this.dgvCartonPendingQueue.CurrentCell, out selectedBarcode);
                    if (cartonID > 0 && CustomMsgBox.Show(this, "Bạn có muốn " + (sender.Equals(this.buttonRemoveCartonPending) ? "xã thùng carton này ra và đóng lại không:" : "xóa " + (sender.Equals(this.buttonDeleteAllCartonPending) ? "toàn bộ " : "") + "thùng carton, bao gồm các chai bên trong") + (sender.Equals(this.buttonDeleteAllCartonPending) ? "?" + (char)13 + (char)13 + "[XÓA TOÀN BỘ]" : ":" + (char)13 + (char)13 + selectedBarcode), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (sender.Equals(this.buttonRemoveCartonPending))
                            if ((this.fillingData.HasPack && this.scannerController.UnwrapCartontoPack(cartonID)) || (!this.fillingData.HasPack && this.scannerController.TakebackCartonFromPendingQueue(cartonID))) CustomMsgBox.Show(this, "Carton: " + selectedBarcode + "\r\nĐã được xã thành công.", "Handle exception", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (sender.Equals(this.buttonDeleteCartonPending) || sender.Equals(this.buttonDeleteAllCartonPending))
                            if (this.scannerController.DeleteCarton(cartonID, sender.Equals(this.buttonDeleteAllCartonPending))) CustomMsgBox.Show(this, "Carton: " + selectedBarcode + "\r\nĐã được xóa, bao gồm các chai bên trong." + "\r\nVui lòng đọc lại từng chai nếu muốn đóng lại carton.", "Handle exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandlers.ShowExceptionMessageBox(this, exception);
                }
            }
        }

        private void dgvQueue_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                try
                {
                    DataGridView dataGridView = sender as DataGridView;
                    if (dataGridView != null && dataGridView.CurrentCell != null)
                    {
                        string selectedBarcode = "";
                        int barcodeID = this.getBarcodeID(dataGridView.CurrentCell, out selectedBarcode);
                        if (barcodeID > 0)
                        {
                            int cartonID = sender.Equals(this.dgvCartonPendingQueue) || sender.Equals(this.dgvCartonQueue) || sender.Equals(this.dgvCartonsetQueue) ? barcodeID : 0;
                            int palletID = sender.Equals(this.dgvCartonPendingQueue) || sender.Equals(this.dgvCartonQueue) || sender.Equals(this.dgvCartonsetQueue) ? 0 : barcodeID;
                            QuickView quickView = new QuickView(this.scannerAPIs.GetBarcodeList(this.fillingData.FillingLineID, cartonID, palletID), (cartonID != 0 ? "Carton: " : "Pallet: ") + selectedBarcode);
                            quickView.ShowDialog(); quickView.Dispose();
                        }
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandlers.ShowExceptionMessageBox(this, exception);
                }
            }
        }

        private int getBarcodeID(DataGridViewCell dataGridViewCell, out string selectedBarcode)
        {
            int barcodeID;
            if (dataGridViewCell != null)
            {
                selectedBarcode = dataGridViewCell.Value as string;
                if (selectedBarcode != null)
                {
                    int startIndexOfPackID = selectedBarcode.IndexOf(GlobalVariables.doubleTabChar.ToString() + GlobalVariables.doubleTabChar.ToString());
                    if (startIndexOfPackID >= 0 && int.TryParse(selectedBarcode.Substring(startIndexOfPackID + 2), out barcodeID))
                    {
                        //selectedBarcode = this.GetSerialNumber(selectedBarcode) + ": " + selectedBarcode.Substring(0, startIndexOfPackID);
                        selectedBarcode = selectedBarcode.Substring(0, startIndexOfPackID);
                        return barcodeID;
                    }
                }
            }
            selectedBarcode = null;
            return -1;
        }

        private void buttonPackQueueCount_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustomMsgBox.Show(this, "Bạn có muốn reset số thứ tự chia làn auto packer về vị trí gốc [1, 1].?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    this.scannerController.ResetNextPackQueueID();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonPacksetQueueCount_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.scannerController.PackQueueCount > 0 && this.scannerController.PacksetQueueCount == 0 && CustomMsgBox.Show(this, "Bạn có muốn chia đều chai vào các làn auto packer không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    this.scannerController.ReAllocationPack();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonCartonQueueCount_Click(object sender, EventArgs e)
        {
            try
            {//HERE: WE CHECK CONDITION BEFORE CALL ToggleLastCartonset (TO PROTEST ACCIDENTAL PRESS BY WORKER). SURELY, WE CAN OMIT THESE CONDITION  ============> NOW, 09-OCT-2017: WE OMIT SOME CONDITIONS!!! LATER: WE CAN RE-ADD THESE CONDITIONS FOR CHECKING AGAIN, IF NEEDED!!!         this.scannerController.PackQueueCount == 0 && this.scannerController.PacksetQueueCount == 0 && this.scannerController.CartonPendingQueueCount == 0 && 
                if (this.scannerController.CartonQueueCount > 0 && this.scannerController.CartonQueueCount < this.fillingData.CartonPerPallet && this.scannerController.CartonsetQueueCount == 0 && CustomMsgBox.Show(this, "Số lượng carton ít hơn số lượng cần đóng pallet.\r\n\r\nBạn có muốn đóng pallet ngay bây giờ không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    this.scannerController.ToggleLastCartonset(true);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonCartonsetQueueCount_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.scannerController.CartonsetQueueCount > 0 && CustomMsgBox.Show(this, "Đóng pallet ngay bây giờ.\r\n\r\nVui lòng chọn Yes để tiếp tục, hoặc chọn No để bỏ qua?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    GlobalEnums.OnTestPalletReceivedImmediately = true;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonCartonAttributes_Click(object sender, EventArgs e)
        {
            try
            {
                this.StartUpload();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonPalletQueueCount_Click(object sender, System.EventArgs e)
        {
            try
            {
                IList<BarcodeDTO> barcodeList = this.scannerAPIs.GetCartonAttributes(GlobalVariables.FillingLineID, (int)GlobalVariables.SubmitStatus.Freshnew + "," + (int)GlobalVariables.SubmitStatus.Failed + "," + (int)GlobalVariables.SubmitStatus.Exported, null);
                QuickView quickView = new QuickView(barcodeList, (barcodeList.Count == 500 ? "Top 500" : barcodeList.Count.ToString()) + " Cartons [NOT SENT]");
                quickView.ShowDialog(); quickView.Dispose();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonExportCartons_Click(object sender, System.EventArgs e)
        {
            try
            {
                string fileDate = (DateTime.Now).ToString("yyyyMMdd-HHmm");

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Document (.txt)|*.txt";
                saveFileDialog.FileName = fileDate + ".txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "")
                {
                    DataServerController dataServerController = new DataServerController();
                    int fileNo = dataServerController.ExportText(saveFileDialog.FileName.Replace(".txt", ""));
                    CustomMsgBox.Show(this, "Xuất dữ liệu hoàn tất!" + "\r\n" + "\r\n" + "Tổng cộng " + fileNo.ToString() + " file đã được xuất ra txt.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonBatchSumups_Click(object sender, EventArgs e)
        {
            try
            {
                BatchSumups batchSumups = new BatchSumups();
                batchSumups.ShowDialog(); batchSumups.Dispose();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        #endregion Exception Handler




        #region Backup

        private void timerNmvnBackup_Tick(object sender, EventArgs e)
        {
            try
            {
                this.BackupDataHandler();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void BackupDataHandler()
        {
            try
            {
                if (digitThread == null || !digitThread.IsAlive)
                {
                    if (packThread == null || !packThread.IsAlive)
                    {
                        if (cartonThread == null || !cartonThread.IsAlive)
                        {
                            if (palletThread == null || !palletThread.IsAlive)
                            {
                                if (scannerThread == null || !scannerThread.IsAlive)
                                {
                                    if (dataServerThread == null || !dataServerThread.IsAlive)
                                    {
                                        if (backupDataThread == null || !backupDataThread.IsAlive)
                                        {
                                            backupDataThread = new Thread(new ThreadStart(this.scannerController.BackupData));
                                            backupDataThread.Start();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        #endregion Backup




    }
}


