
using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;


namespace Xdgk.GR.Data
{
    public class XD100Defines
    {
        /// <summary>
        ///  
        /// </summary>
        static public TemperatureControlModeCollection TemperatureControlModeCollection
        {
            get
            {
                if (_temperatureControlModeCollection == null)
                {
                    _temperatureControlModeCollection = new TemperatureControlModeCollection();

                    TemperatureControlMode m = null;
                    m = new TemperatureControlMode(
                            //CZGR1.XD100.XD100Strings.OTGT2ControlLine, 
                            "�¶ȿ�������",
                            TemperatureControlModeEnum.OT_GT2);
                    _temperatureControlModeCollection.Add(m);

                    m = new TemperatureControlMode(
                            //CZGR1.XD100.XD100Strings.TimeControlLine, 
                            "��ʱ��������",
                            TemperatureControlModeEnum.Time_GT2);
                    _temperatureControlModeCollection.Add(m);
                }
                return _temperatureControlModeCollection;
            }
        } static private TemperatureControlModeCollection _temperatureControlModeCollection;


        /// <summary>
        /// 
        /// </summary>
        static public ValveTypeCollection ValveTypeCollection
        {
            get
            {
                if (_valveTypeCollection == null)
                {
                    _valveTypeCollection = new ValveTypeCollection();
                    ValveType v = null;
                    v = new ValveType(
                            //CZGR1.XD100.XD100Strings.AnalogValve, 
                            "ģ�ⷧ",
                            ValveTypeEnum.AnalogValve);
                    _valveTypeCollection.Add(v);

                    v = new ValveType(
                            //CZGR1.XD100.XD100Strings.TrinityValve, 
                            "��λ��",
                            ValveTypeEnum.TrinityValve);
                    _valveTypeCollection.Add(v);
                }
                return _valveTypeCollection;
            }
        } static private ValveTypeCollection _valveTypeCollection;

    }

}
