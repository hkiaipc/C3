
using System;
using C3.Data;
using Xdgk.Common;
using Xdgk.GR.Data;


namespace Xdgk.GR.Data
{
    public class FlowmeterData : DataBase
    {
#region InstantFlux
        /// <summary>
        /// 
        /// </summary>
        [DataItem("˲ʱ", 10, Unit.M3PerSecond, "f2")]
            public double InstantFlux
            {
                get
                {
                    return _instantFlux;
                }
                set
                {
                    _instantFlux = value;
                }
            } private double _instantFlux;
#endregion //InstantFlux

#region Sum
        /// <summary>
        /// 
        /// </summary>
        [DataItem("�ۼ�", 20, Unit.M3, "f0")]
            public double Sum
            {
                get
                {
                    return _sum;
                }
                set
                {
                    _sum = value;
                }
            } private double _sum;
#endregion //Sum
    }

}
