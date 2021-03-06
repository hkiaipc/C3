using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class DeviceTypeCollection : Xdgk.Common.Collection<DeviceType>
    {
        #region Add
        new internal void Add(DeviceType value)
        {
            base.Add(value);
        }
        #endregion //Add

        #region Insert
        new internal void Insert(int index, DeviceType value)
        {
            base.Insert(index, value);
        }
        #endregion //Insert

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
        }
    }

    public class StationTypeCollection : Xdgk.Common.Collection<StationType>
    {
        #region Add
        new internal void Add(StationType value)
        {
            base.Add(value);
        }
        #endregion //Add

        #region Insert
        new internal void Insert(int index, StationType value)
        {
            base.Insert(index, value);
        }
        #endregion //Insert

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
        }
    }

}
