﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;
using Xdgk.GR.UI;

namespace XD1100DPU
{
    public partial class frmXD100ModbusTemperatureControl : NUnit.UiKit.SettingsDialogBase
    {

        private _1100ControllerInterface _controller;

        /// <summary>
        /// 
        /// </summary>
        //private bool _canWrite = false;



        // private read | write | none state
        //
        // can write state
        //


        #region frmXD100TemperatureControl

        public frmXD100ModbusTemperatureControl(_1100ControllerInterface controller)
        {
            InitializeComponent();

            BindDatas();

            this.ucotControlLine1.Size = this.ucTimeControlLine21.Size;
            this.ucotControlLine1.Location = this.ucTimeControlLine21.Location;

            this.ucValveOpenDegree1.Size = this.ucTimeControlLine21.Size;
            this.ucValveOpenDegree1.Location = this.ucTimeControlLine21.Location;

            this._controller = controller;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        //public frmXD100ModbusTemperatureControl(IDevice device)
        //{
        //    InitializeComponent();

        //    //this.txtStationName.Text = this.Device.Station.Name;
        //    BindDatas();
        //    //this.Device = device;
        //    this.RegisterEvent();
        //    this.ucotControlLine1.Size = this.ucTimeControlLine21.Size;
        //    this.ucotControlLine1.Location = this.ucTimeControlLine21.Location;

        //    this.ucValveOpenDegree1.Size = this.ucTimeControlLine21.Size;
        //    this.ucValveOpenDegree1.Location = this.ucTimeControlLine21.Location;
        //}
        #endregion //frmXD100TemperatureControl


        #region RegisterEvent
        /// <summary>
        /// 
        /// </summary>
        private void RegisterEvent()
        {
            //TaskManager tm = CZGRApp.Default.Soft.TaskManager;
            //tm.Executed += new TaskExecutedEventHandler(tm_Executed);
        }
        #endregion //RegisterEvent


        #region UnRegisterEvent
        /// <summary>
        /// 
        /// </summary>
        private void UnRegisterEvent()
        {
            //TaskManager tm = CZGRApp.Default.Soft.TaskManager;
            //tm.Executed -= new TaskExecutedEventHandler(tm_Executed);
        }
        #endregion //UnRegisterEvent


        #region BindDatas
        /// <summary>
        /// 
        /// </summary>
        private void BindDatas()
        {
            this.cmbControlMode.DataSource = Xdgk.XD100Modbus.XD100ModbusDefines.TemperatureControlModeCollection;
            this.cmbControlMode.DisplayMember = "Name";
            this.cmbControlMode.ValueMember = "Value";
            //this.cmbControlMode.SelectedItem 

            this.cmbValveType.DataSource = Xdgk.GR.Data.XD100Defines.ValveTypeCollection;
                //Xdgk.XD100.XD100Defines.ValveTypeCollection;
            this.cmbValveType.DisplayMember = "Name";
            this.cmbValveType.ValueMember = "Value";
        }
        #endregion //BindDatas


        #region cancelButton_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion //cancelButton_Click


        #region UC
        ///// <summary>
        ///// 
        ///// </summary>
        //public UCTimeControlLine UC
        //{
        //    // tt
        //    //get { return ucTimeControlLine1; }
        //    get { return null; }
        //}
        #endregion //UC

        #region TimeControlLine2
        /// <summary>
        /// 
        /// </summary>
        public UCTimeControlLine2 TimeControlLine2
        {
            get 
            {
                return this.ucTimeControlLine21;
            }
        }
        #endregion //TimeControlLine2

        #region frmXD100TemperatureControl_Load
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmXD100TemperatureControl_Load(object sender, EventArgs e)
        {
        }
        #endregion //frmXD100TemperatureControl_Load


        #region okButton_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            return;
        }
        #endregion //okButton_Click


        #region btnRead_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            ParameterBag pb = new ParameterBag();

            //pb.DeviceID = this._controller
            pb.OperaName = "ReadModbusControl";
            _controller.Execute(pb);
                //_controller.ReadMode();
            //if (this._state == State.None)
            //{
                //_controller.ReadMode();
                //if (Device.Station.CommuniPort != null)
                //{
                //    this.ReadMode();
                //    this.SetState(State.Read);
                //}
                //else
                //{
                //    NUnit.UiKit.UserMessage.DisplayFailure("XD100.XD100Strings.NotConnected");
                //}
            //}
            //else
            //{
            //    //ShowOPError();
            //    NUnit.UiKit.UserMessage.DisplayFailure("XD100.XD100Strings.Executing");
            //}
        }
        #endregion //btnRead_Click


        #region ShowError
        /// <summary>
        /// 
        /// </summary>
        private void ShowError()
        {
            //string s = GetStateText(this._state) + "XD100.XD100Strings.Fail";
            //ShowError(s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        private void ShowError(string msg)
        {
            //this.lblError.Text = msg;
            //this.statusBarPanel1.Text = DateTime.Now + " " + msg;
            NUnit.UiKit.UserMessage.DisplayFailure(msg);
        }
        #endregion //ShowError





        #region ReadMode
        /// <summary>
        /// 
        /// </summary>
        private void ReadMode()
        {
         
        }
        #endregion //ReadMode


        

        //#region ReadLine
        ///// <summary>
        ///// 
        ///// </summary>
        //private void ReadLine()
        //{
        //    //Opera op = new Opera(CZGRCommon.DeviceTypes.GRDevice, "ReadTempControlData");
        //    Opera op = CreateOpera("ReadTempControlData");
        //    _task = new Task(this.Device, op, new ImmediateStrategy());
        //    CZGRApp.Default.Soft.TaskManager.Tasks.AddToHead(_task);
        //}
        //#endregion //ReadLine


        //#region ProcessReadLine
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="e"></param>
        //private void ProcessReadLine(TaskExecutedEventArgs e)
        //{
        //    if (e.ParseResult.Success)
        //    {
        //        object obj = e.ParseResult.NameObjects.GetObject("TimeControlLine");
        //        int[] adjustValues = obj as int[];

        //        //KeyValuePair<int, int>[] timeControlLine = obj as KeyValuePair<int, int>[adjustValue.Length];

        //        KeyValuePair<int, int>[] timeControlLine = CreateTimeControlLineByAdjustValues(adjustValues);
        //        // tt
        //        //this.ucTimeControlLine1.TimeControlLine = timeControlLine;
        //        this.TimeControlLine2.TimeControlLine = timeControlLine;

        //        // ot-gt2 line
        //        //
        //        obj = e.ParseResult.NameObjects.GetObject("OTControlLine");
        //        KeyValuePair<int, int>[] otControlLine = (KeyValuePair<int, int>[])obj;
        //        this.ucotControlLine1.OTControlLine = otControlLine;

        //        this._canWrite = true;
        //    }
        //    else
        //    {
        //        ShowError();
        //    }
        //    SetState(State.None);
        //}
        //#endregion //ProcessReadLine


        #region KeyValuePair
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private KeyValuePair<int, int>[] CreateTimeControlLineByAdjustValues(int[] values)
        {
            KeyValuePair<int, int>[] r = new KeyValuePair<int, int>[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                r[i] = new KeyValuePair<int, int>(i * 2, values[i]);
            }
            return r;
        }
        #endregion //KeyValuePair

        #region CreateAdjustValuesByTimeControlLine
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int[] CreateAdjustValuesByTimeControlLine(KeyValuePair<int, int>[] timeControlLine)
        {
            int[] r = new int[timeControlLine.Length];
            for (int i = 0; i < timeControlLine.Length; i++)
            {
                r[i] = timeControlLine[i].Value;
            }
            return r;
        }
        #endregion //CreateAdjustValuesByTimeControlLine


        #region tm_Executed
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void tm_Executed(object sender, TaskExecutedEventArgs e)
        //{
        //    //throw new NotImplementedException();

        //    // TODO: check is this task
        //    //
        //    switch (this._state)
        //    {
        //        case State.Read :
        //            ProcessReadMode(e);
        //            break;

        //        //case State.ReadLine:
        //        //    ProcessReadLine(e);
        //        //    break;

        //        case State.Write :
        //            ProcessWriteMode(e);
        //            break;

        //        //case State.WriteLine :
        //        //    ProcessWriteLine(e);
        //        //    break;

        //        default:
        //            break;
        //    }
        //}
        #endregion //tm_Executed



        #region GetSettingValue
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetSettingValue()
        {
            int value = 0;
            Xdgk.XD100Modbus.TemperatureControlMode mode = this.cmbControlMode.SelectedItem as Xdgk.XD100Modbus.TemperatureControlMode ;
            if (mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.ValveOpenDegree)
            {
                value = this.ucValveOpenDegree1.ValveOpenDegree;
            }
            //else if (mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.SettingAndBT2 ||
            //    mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.SettingAndDiffT2 ||
            //    mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.SettingAndGT2)
            else
            {
                value = (int)ucTimeControlLine21.GTBase2;
            }

            value *= 10;
            return value;
        }
        #endregion //GetSettingValue

        #region ProcessWriteMode
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="e"></param>
        //private void ProcessWriteMode(TaskExecutedEventArgs e)
        //{
        //    if (e.ParseResult.Success)
        //    {
        //        // tt
        //        //KeyValuePair<int, int>[] tcLine = this.ucTimeControlLine1.TimeControlLine;
        //        //int[] adjustValues = CreateAdjustValuesByTimeControlLine(tcLine);
        //        //WriteLine(adjustValues);
        //        //this.SetState(State.WriteLine);

        //        //KeyValuePair<int, int>[] tcLine = this.TimeControlLine2.TimeControlLine;
        //        //int[] adjustValues = CreateAdjustValuesByTimeControlLine(tcLine);
        //        //WriteLine(adjustValues);
        //        //this.SetState(State.WriteLine);

        //    }
        //    else
        //    {
        //        ShowError();
        //    }
        //    this.SetState(State.None);
        //}
        #endregion //ProcessWriteMode

        //#region WriteLine
        ///// <summary>
        ///// 
        ///// </summary>
        ////private void WriteLine(KeyValuePair<int, int>[] timeControlLine)
        //private void WriteLine(int[] adjustValues)
        //{
        //    Opera op = CreateOpera("WriteTempControlData");
        //    op.SendPart["TimeControlLine"] = adjustValues;

        //    // TODO: 2010-08-31 ot control line
        //    //
        //    op.SendPart["OTControlLine"] = this.ucotControlLine1.OTControlLine;
        //    _task = new Task(this.Device, op, new ImmediateStrategy());
        //    CZGRApp.Default.Soft.TaskManager.Tasks.AddToHead(_task);
        //}
        //#endregion //WriteLine


        //#region ProcessWriteLine
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="e"></param>
        //private void ProcessWriteLine(TaskExecutedEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    if (e.ParseResult.Success)
        //    {
        //    }
        //    else
        //    {
        //        ShowError();
        //    }
        //    SetState(State.None);
        //}
        //#endregion //ProcessWriteLine


        #region btnWrite_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWrite_Click(object sender, EventArgs e)
        {
            //if (this.Device.Station.CommuniPort == null)
            //{
            //    NUnit.UiKit.UserMessage.DisplayFailure("XD100.XD100Strings.NotConnected");
            //    return;
            //}

            //if (!this._canWrite)
            //{
            //    NUnit.UiKit.UserMessage.DisplayFailure("XD100.XD100Strings.FirstReadGRControlParams");
            //    return;
            //}

            //if (this._state != State.None)
            //{
            //    NUnit.UiKit.UserMessage.DisplayFailure("XD100.XD100Strings.Executing");
            //    return;
            //}
            //if (this.cmbControlMode.SelectedItem != null)
            //{
            //    Xdgk.XD100Modbus.TemperatureControlMode mode = this.cmbControlMode.SelectedItem as Xdgk.XD100Modbus.TemperatureControlMode;
            //    if (mode != null)
            //    {
            //        if (mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.ValveOpenDegree)
            //        {
            //            string s = string.Format(XD100.XD100ModbusStrings.NotSupportMode,mode.Name );
            //            NUnit.UiKit.UserMessage.DisplayFailure(s);
            //            return;
            //        }
            //    }
            //}
            //WriteMode();

            //this._controller.WriteMode();
            //this.SetState(State.Write);
        }
        #endregion //btnWrite_Click

        #region frmXD100TemperatureControl_FormClosing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmXD100TemperatureControl_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        #endregion //frmXD100TemperatureControl_FormClosing

        #region frmXD100TemperatureControl_FormClosed
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmXD100TemperatureControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.UnRegisterEvent();
        }
        #endregion //frmXD100TemperatureControl_FormClosed

        #region cmbControlMode_SelectedIndexChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: local and resize uc
            //
            object obj = this.cmbControlMode.SelectedItem;
            Xdgk.XD100Modbus.TemperatureControlMode mode = obj as Xdgk.XD100Modbus.TemperatureControlMode ;

            //if (mode.Mode == Xdgk.XD100.TemperatureControlModeEnum.OT_GT2)
            if( mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.LineAndBT2 ||
                mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.LineAndGT2 ||
                mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.LineAndDiffT2 )
            {
                //this.ucotControlLine1.Visible = true;
                //this.ucTimeControlLine21.Visible = false;
                ShowUCControl(this.ucotControlLine1);
            }
            //else if (mode.Mode == Xdgk.XD100.TemperatureControlModeEnum.Time_GT2)
            else if ( mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.SettingAndBT2 ||
                mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.SettingAndDiffT2 ||
                mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.SettingAndGT2 )
            {
                //this.ucotControlLine1.Visible = false;
                //this.ucTimeControlLine21.Visible = true;
                ShowUCControl(this.ucTimeControlLine21);
            }
            else if (mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.ValveOpenDegree)
            {
                //this.ucTimeControlLine21.Visible = false;
                //this.ucotControlLine1.Visible = false;
                ShowUCControl(ucValveOpenDegree1);
            }
            else
            {
                ShowUCControl(null);
            }
        }
        #endregion //cmbControlMode_SelectedIndexChanged

        #region ShowUCControl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        private void ShowUCControl(Control control)
        {
            foreach (Control c in UCControlList)
            {
                c.Visible = (control == c);
            }
        }
        #endregion //ShowUCControl

        #region UCControlList
        /// <summary>
        /// 获取用户空间列表, 每个用户控件对应一类控制方式
        /// </summary>
        private Control[] UCControlList
        {
            get
            {
                if (_ucControlList == null)
                {
                    _ucControlList = new Control[]
                    {
                        this.ucotControlLine1 ,
                        this.ucTimeControlLine21 ,
                        this.ucValveOpenDegree1
                    };
                }
                return _ucControlList;
            }
        } private Control[] _ucControlList;
        #endregion //UCControlList

    }



}


