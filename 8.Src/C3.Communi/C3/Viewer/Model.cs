﻿using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

namespace C3
{
    /// <summary>
    /// 
    /// </summary>
    abstract public class Model
    {
        public Type ControllerType
        {
            get { return _controllerType; }
            set { _controllerType = value; }
        } private Type _controllerType;

        /// <summary>
        /// 
        /// </summary>
        abstract public string Title
        {
            get;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class StationModel : Model
    {
        public StationModel(IStation station)
        {
            this._station = station;
            this.ControllerType = typeof(StationController);
        }

        #region Station
        /// <summary>
        /// 
        /// </summary>
        public IStation Station
        {
            get
            {
                return _station;
            }
        } private IStation _station;
        #endregion //Station


        public override string Title
        {
            get { return this.Station.Name; }
        }
    }

    public class DeviceMode : Model
    {
        public DeviceMode(IDevice device)
        {
            this._device = device;
            this.ControllerType = typeof(DeviceController);
        }

        /// <summary>
        /// 
        /// </summary>
        public IDevice Device
        {
            get { return _device; }
        } private IDevice _device;


        /// <summary>
        /// 
        /// </summary>
        public override string Title
        {
            get { return this.Device.Name + ":" + this.Device.Text; }
        }
    }
}
