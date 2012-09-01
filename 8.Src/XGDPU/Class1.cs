﻿using System;
using System.Data;
using System.Collections.Generic;
using C3.Communi;
using Xdgk.Common;
using C3.Data;

namespace XGDPU
{
    internal class DBI : DBIBase
    {
        /// <summary>
        /// 
        /// </summary>
        static internal DBI Instance
        {
            get
            {
                if (_instance == null)
                {

                    SourceConfig sc = SoftManager.GetSoft().SourceConfigs.Find("Connection");
                    if (sc != null)
                    {
                        _instance = new DBI(sc.Value);
                    }
                    else
                    {
                        throw new ConfigException("connection");
                    }
                }
                return _instance;
            }
        }

        static private DBI _instance;

        private DBI(string s)
            : base(s)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable ExecuteXGDeviceDataTable()
        {
            string s = "select * from tblDevice where DeviceType = 'xgdevice'";
            return ExecuteDataTable(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xgData"></param>
        public void InsertXGData(XGDevice xgdevice, XGData xgData)
        {
            int cardID;
            string person;
            GetCardID(xgData.CardSN, out cardID, out person);

            int deviceID = GuidHelper.ConvertToInt32(xgdevice.Guid);
            int stationID = GuidHelper.ConvertToInt32(xgdevice.StationGuid);

            string s = string.Format(
                "INSERT INTO tblXGData(DT, Person, DeviceID, StationID, CardID) " +
                "VALUES('{0}', '{1}', {2}, {3}, {4})",
                xgData.DT, person, deviceID, stationID, cardID);
            ExecuteScalar(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardSn"></param>
        /// <returns></returns>
        public void GetCardID(string cardSn, out int cardID, out string person)
        {
            string s = string.Format("select * from tblCard where sn = '{0}'", cardSn);
            DataTable tbl = ExecuteDataTable(s);
            if (tbl.Rows.Count > 0)
            {
                DataRow row = tbl.Rows[0];
                cardID = Convert.ToInt32(row["cardID"]);
                person = row["Person"].ToString();
            }
            else
            {
                InsertCard(cardSn, "unknown");
                GetCardID(cardSn, out cardID, out person);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardSn"></param>
        /// <param name="person"></param>
        private void InsertCard(string cardSn, string person)
        {
            string s = string.Format(
                "insert into tblCard(sn, person) values('{0}', '{1}')",
                cardSn, person );
            ExecuteScalar(s);
        }

    }

    public class XGDevicePersister : DevicePersisterBase
    {
        protected override void OnAdd(IDevice device)
        {
            XGDevice d = (XGDevice)device;

            string s = string.Format(
                "insert into tblDevice(address, deviceType, stationID) values({0}, '{1}', {2}); select @@identity;",
                d.Address,
                d.DeviceType.Name,
                GuidHelper.ConvertToInt32(d.Station.Guid)
                );

            object obj = DBI.Instance.ExecuteScalar(s);
            d.Guid = GuidHelper.Create(Convert.ToInt32(obj));
        }

        protected override void OnUpdate(IDevice device)
        {
            string s = string.Format(
                "update tblDevice set address = {0} where DeviceID = {1}",
                device.Address, GuidHelper.ConvertToInt32(device.Guid));

            DBI.Instance.ExecuteScalar(s);
        }

        protected override void OnDelete(IDevice device)
        {
            string s = string.Format(
                "delete from tblDevice where DeviceID = {0}",
                GuidHelper.ConvertToInt32(device.Guid));
            DBI.Instance.ExecuteScalar(s);

        }
    }

    internal class XGDeviceSource : DeviceSourceBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        internal XGDeviceSource(DataRow row)
        {
            this.DataRow = row;
        }

        #region DataRow
        /// <summary>
        /// 
        /// </summary>
        public DataRow DataRow
        {
            get
            {
                return _dataRow;
            }
            set
            {
                _dataRow = value;
                this.Address = Convert.ToUInt64(_dataRow["address"]);
                this.DevcieTypeName = _dataRow["DeviceType"].ToString().Trim();
                this.Guid = GuidHelper.Create(
                    Convert.ToInt32(_dataRow["DeviceID"])
                    );
                this.StationGuid = GuidHelper.Create(
                    Convert.ToInt32(_dataRow["StationID"])
                    );
            }
        } private DataRow _dataRow;
        #endregion //DataRow
    }

    internal class XGDeviceSourceProvider : DeviceSourceProviderBase
    {
        public override IDeviceSource[] OnGetDeviceSources()
        {
            List<IDeviceSource> list = new List<IDeviceSource>();

            DataTable tbl = DBI.Instance.ExecuteXGDeviceDataTable();
            foreach (DataRow row in tbl.Rows)
            {
                XGDeviceSource item = new XGDeviceSource(row);
                list.Add(item);
            }
            return list.ToArray();
        }
    }

    internal class XGData : IDeviceData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cardSn"></param>
        public XGData(DateTime dt, string cardSn)
        {
            if (cardSn == null) throw new ArgumentNullException("cardSn");
            if( cardSn.Trim( ).Length == 0 )
            {
                throw new ArgumentException("cardSn exception");
            }
            this.DT = dt;
            this.CardSN = cardSn;
        }

        #region IDeviceData 成员

        /// <summary>
        /// 
        /// </summary>
        public DateTime DT
        {
            get
            {
                return _dt;
            }
            set
            {
                _dt = value;
            }
        } private DateTime _dt;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReportItemCollection GetReportItems()
        {
            ReportItemCollection r = new ReportItemCollection();
            r.Add(new ReportItem("时间", this.DT, Unit.FindByName(Unit.None)));
            r.Add(new ReportItem("卡号", this.CardSN, Unit.FindByName(Unit.None)));
            return r;
        }

        #region CardSN
        /// <summary>
        /// 
        /// </summary>
        public string CardSN
        {
            get
            {
                if (_cardSN == null)
                {
                    _cardSN = string.Empty;
                }
                return _cardSN;
            }
            set
            {
                _cardSN = value;
            }
        } private string _cardSN;
        #endregion //CardSN


        #endregion
    }

    internal class XGDevice : DeviceBase
    {
        public override object GetLazyDataFieldValue(string name)
        {
            if (StringHelper.Equal(name, "date"))
            {
                return DateTime.Now.Date;
            }
            else if (StringHelper.Equal(name, "time"))
            {
                return DateTime.Now.TimeOfDay;
            }

            return base.GetLazyDataFieldValue(name);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    internal class XGOperaNames
    {
        public const string ReadCount = "readcount";
        public const string ReadRecord = "readrecord";
        public const string ClearRecord = "clearrecord";
        public const string RecordUpload = "recordupload";
        public const string RemoveUpload = "removeupload";
        public const string ReadXGDate = "readdate";
        public const string ReadXGTime = "readtime";
        public const string WriteXGDate = "writedate";
        public const string WriteXGTime = "writetime";
    }

    /// <summary>
    /// 
    /// </summary>
    public class XGDpu : DPUBase
    {
        public XGDpu()
        {
            this.Name = "XDDpu";
            this.DeviceFactory = new XGDeviceFactory(this);
            this.DevicePersister = new XGDevicePersister();
            this.DeviceSourceProvider = new XGDeviceSourceProvider();
            this.DeviceType = DeviceTypeManager.AddDeviceType("XGDevice",
                "Xun Geng(Text)",
                typeof(XGDevice));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new XGDeviceProcessor();

            string path = PathUtils.GetAssemblyDirectory(typeof(XGDevice).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }
}