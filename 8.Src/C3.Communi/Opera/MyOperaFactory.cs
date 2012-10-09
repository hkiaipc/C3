using System;
using System.Collections;
using System.Xml;
using Xdgk.Common;
using NLog;

namespace C3.Communi
{
    public class MyOperaFactory
    {

        #region Members
        /// <summary>
        /// 
        /// </summary>
        static private Logger log = LogManager.GetCurrentClassLogger();
        #endregion //Members

        #region Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="operaname"></param>
        /// <returns></returns>
        static public Opera Create(string devicetype, string operaname, XmlNode deviceDefineNode)
        {
            XmlNodeList opnodelist = deviceDefineNode.SelectNodes(DeviceDefineNodeNames.OperaDefine);
            foreach (XmlNode opnode in opnodelist)
            {
                string opname = GetAttribute(opnode as XmlElement, DeviceDefineNodeNames.OperaName);
                if (StringHelper.Equal(opname, operaname))
                {
                    return CreateFromOperaNode(devicetype, opnode);
                }
            }
            return null;
        }
        #endregion //Create

        #region Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operanode"></param>
        /// <returns></returns>
        static private Opera CreateFromOperaNode(string deviceType, XmlNode operaNode)
        {
            XmlElement e = operaNode as XmlElement;
            Opera opera = null;
            SendPart sp = null;
            ReceivePartCollection rps = new ReceivePartCollection();
            string name = GetAttribute(e, DeviceDefineNodeNames.OperaName);
            string text = GetAttribute(e, DeviceDefineNodeNames.OperaText, true);

            string args = GetAttribute(e, DeviceDefineNodeNames.OperaArgs, true);


            foreach (XmlNode node in operaNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case DeviceDefineNodeNames.SendPart:
                        sp = CreateSendPart(node);
                        break;

                    case DeviceDefineNodeNames.ReceivePart:
                        ReceivePart rp = CreateReceivePart(node);
                        rps.Add(rp);
                        break;
                }
            }

            opera = new Opera(deviceType, name, text, args);
            opera.SendPart = sp;
            opera.ReceiveParts = rps;
            log.Info("Create opera '{0}', receivepart count '{1}'", name, rps.Count);
            return opera;
        }
        #endregion //Create

        #region CreateReceivePart
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        static private ReceivePart CreateReceivePart(XmlNode recievepartnode)
        {
            XmlElement e = recievepartnode as XmlElement;
            string str = GetAttribute(e, DeviceDefineNodeNames.ReceivePartDataLength);
            int rpLength = int.Parse(str);

            string name = GetAttribute(e, DeviceDefineNodeNames.ReceivePartName, true);

            ReceivePart rp = new ReceivePart(name, rpLength);

            foreach (XmlNode node in recievepartnode.ChildNodes)
            {
                switch (node.Name)
                {
                    case DeviceDefineNodeNames.DataField:
                        DataField df = CreateDataField(node);
                        df.IsBytesVolatile = true;
                        rp.Add(df);
                        break;
                }
            }
            ICRCer crcer = GetCRCer(recievepartnode);
            rp.DataFieldManager.CRCer = crcer;

            if (rp.DataFieldManager.CRCer == null &&
                    rp.DataFieldManager.GetCRCDataField() != null)
            {
                throw new ConfigException("not exist CRCer");
            }
            return rp;
        }
        #endregion //CreateReceivePart

        #region CreateSendPart
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendpartnode"></param>
        /// <returns></returns>
        static private SendPart CreateSendPart(XmlNode sendpartnode)
        {
            SendPart sp = new SendPart();

            DataField df = null;
            foreach (XmlNode node in sendpartnode.ChildNodes)
            {
                switch (node.Name)
                {
                    case DeviceDefineNodeNames.DataField:
                        df = CreateDataField(node);
                        df.IsValueVolatile = true;
                        sp.Add(df);
                        break;
                }
            }

            ICRCer crcer = GetCRCer(sendpartnode);
            sp.DataFieldManager.CRCer = crcer;
            return sp;
        }
        #endregion //CreateSendPart

        #region GetCRCer
        /// <summary>
        /// 获取sendpart或receivepart的crcer,如不存在返回null
        /// </summary>
        /// <param name="node">sendpart xmlnode or receivepart xmlnode</param>
        /// <returns></returns>
        static private ICRCer GetCRCer(XmlNode node)
        {
            XmlNode crcerNode = node.SelectSingleNode(DeviceDefineNodeNames.CRCer);
            if (crcerNode != null)
            {
                string name = crcerNode.Attributes["name"].Value;
                Soft soft = SoftManager.GetSoft();
                ICRCer crcer = soft.CRCerManager.GetCRCer(name);
                if (crcer == null)
                    throw new ConfigException("not find CRCer: " + name);
                return crcer;
            }
            return null;

        }
        #endregion //GetCRCer

        #region GetAttribute
        /// <summary>
        /// 
        /// </summary>
        /// <param name="el"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static private string GetAttribute(XmlElement el, string name)
        {
            return Xdgk.Common.XmlHelper.GetAttribute(el, name);
        }
        #endregion //GetAttribute

        #region GetAttribute
        /// <summary>
        /// 
        /// </summary>
        /// <param name="el"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static private string GetAttribute(XmlElement el, string name, bool canNull)
        {
            return Xdgk.Common.XmlHelper.GetAttribute(el, name, canNull);
        }
        #endregion //GetAttribute

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataFieldNode"></param>
        /// <returns></returns>
        static private bool HasBytesConverterChildNode(XmlNode dataFieldNode)
        {
            XmlNode bytesConverterNode = dataFieldNode.SelectSingleNode(DeviceDefineNodeNames.BytesConverter);
            return bytesConverterNode != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataFieldNode"></param>
        /// <returns></returns>
        static private BytesConverterConfig CreateBytesConverterConfig(XmlNode bytesConverterNode)
        {
            BytesConverterConfig r = new BytesConverterConfig();
            string name = GetAttribute((XmlElement)bytesConverterNode, DeviceDefineNodeNames.Name,false );

            //
            //
            string str = GetAttribute((XmlElement)bytesConverterNode, DeviceDefineNodeNames.HasInner, true);
            bool hasInner = false;
            if (str != null && str.Length > 0)
            {
                hasInner = Convert.ToBoolean(str);
            }

            r.Name = name;
            r.HasInner = hasInner;

            r.Propertys = GetPropertys(bytesConverterNode);
            if (hasInner)
            {
                XmlNode innerNode = bytesConverterNode.SelectSingleNode(DeviceDefineNodeNames.BytesConverter);
                if (innerNode == null)
                {
                    string s = string.Format("has not inner bytesConverter");
                    throw new InvalidOperationException(s);
                }
                r.InnerBytesConverterConfig = CreateBytesConverterConfig(innerNode);
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytesConverterNode"></param>
        /// <returns></returns>
        private static System.Collections.Hashtable GetPropertys(XmlNode bytesConverterNode)
        {
            Hashtable hash = new Hashtable();
            XmlNodeList nodes = bytesConverterNode.SelectNodes(DeviceDefineNodeNames.Property);
            foreach (XmlNode node in nodes)
            {
                string name = GetAttribute((XmlElement)node, DeviceDefineNodeNames.Name, false);
                string value = GetAttribute((XmlElement)node, DeviceDefineNodeNames.Value, false);
                hash[name] = value;
            }
            return hash;
        }



        #region CreateDataField
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datafieldnode"></param>
        /// <returns></returns>
        static private DataField CreateDataField(XmlNode datafieldnode)
        {
            string name = string.Empty;
            string factor = string.Empty;
            object[] convertArgs = null;
            int begin = 0;
            int length = 0;
            IBytesConverter ibc = null;
            bool iscrc = false;
            bool isMatchCheck = false;
            byte[] bytes = null;
            bool littleEndian = true;
            bool isLazy = false;

            string str = string.Empty;

            XmlElement el = datafieldnode as XmlElement;
            name = GetAttribute(el, DeviceDefineNodeNames.DataFieldName);

            str = GetAttribute(el, DeviceDefineNodeNames.Begin);
            begin = int.Parse(str);

            str = GetAttribute(el, DeviceDefineNodeNames.Length);
            length = int.Parse(str);


            if (HasBytesConverterChildNode(datafieldnode))
            {
                XmlNode bytesConverterNode = datafieldnode.SelectSingleNode(
                    DeviceDefineNodeNames.BytesConverter);
                BytesConverterConfig cfg = CreateBytesConverterConfig(bytesConverterNode);
                ibc = GetBytesConverter(cfg);
            }
            else
            {
                // converter
                // 
                str = GetAttribute(el, DeviceDefineNodeNames.Converter, true);
                if (str == null || str.Length == 0)
                {
                    str = "Xdgk.Communi.OriginalConverter";
                }

                //
                //
                factor = GetAttribute(el, DeviceDefineNodeNames.Factor, true);
                if (factor != null && factor.Length > 0)
                {
                    try
                    {
                        float n = Convert.ToSingle(factor);
                        convertArgs = new object[] { n };
                    }
                    catch (FormatException formatEx)
                    {
                        string s = string.Format("Invalid Factor '{0}'", factor);
                        throw new ConfigException(s, formatEx);
                    }
                }
                ibc = GetBytesConvert(str, convertArgs);
            }

            //
            //
            str = GetAttribute(el, DeviceDefineNodeNames.LittleEndian, true);
            if (str.Length > 0)
            {
                littleEndian = bool.Parse(str);
            }
            ibc.IsLittleEndian = littleEndian;

            str = GetAttribute(el, DeviceDefineNodeNames.Bytes, true);
            if (str != null && str.Length > 0)
            {
                bytes = HexStringConverter.Default.ConvertToBytes(str);
            }

            str = GetAttribute(el, DeviceDefineNodeNames.Crc, true);
            if (str != null && str.Length > 0)
            {
                iscrc = bool.Parse(str);
            }

            str = GetAttribute(el, DeviceDefineNodeNames.MatchCheck, true);
            if (str != null && str.Length > 0)
            {
                isMatchCheck = bool.Parse(str);
            }

            str = GetAttribute(el, DeviceDefineNodeNames.Lazy, true);
            if (str != null && str.Length > 0)
            {
                isLazy = bool.Parse(str);
            }

            DataField df = new DataField(name, begin, length, (IBytesConverter)ibc);
            df.IsMatchCheck = isMatchCheck;
            if (bytes != null)
                df.Bytes = bytes;

            if (df.IsMatchCheck &&
                    (df.Bytes == null || df.Bytes.Length == 0))
            {
                throw new Exception("must set bytes while matchCheck == true");
            }
            df.IsCRC = iscrc;
            df.IsLazy = isLazy;

            return df;
        }
        #endregion //CreateDataField

        #region GetBytesConvert
        /// <summary>
        /// 
        /// </summary>
        /// <param name="convertName"></param>
        /// <returns></returns>
        static private IBytesConverter GetBytesConvert(string converterName, object[] args)
        {
            Soft soft = SoftManager.GetSoft();
            BytesConverterManager bcm = soft.BytesConverterManager;
            IBytesConverter bc = bcm.CreateBytesConverter(converterName, args);
            if (bc == null)
            {
                log.Error("not find BytesConvert: " + converterName);
                throw new ArgumentException("not find BytesConvert: " + converterName);
            }
            else
            {
            }
            return bc;
        }

        static private IBytesConverter GetBytesConverter(BytesConverterConfig cfg)
        {
            Soft soft = SoftManager.GetSoft();
            BytesConverterManager bcm = soft.BytesConverterManager;
            IBytesConverter bc = bcm.CreateBytesConverter(cfg);
            return bc;
        }
        #endregion //GetBytesConvert

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="name"></param>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        internal static IPicker CreatePicker(string deviceType, XmlNode xmlNode)
        {
            DataFieldPicker  r = null;

            foreach (XmlNode node in xmlNode.ChildNodes )
            {
                string name = XmlHelper.GetAttribute(xmlNode, "name", false);
                switch (node.Name)
                {
                    case DeviceDefineNodeNames.ReceivePart:
                        ReceivePart rp = CreateReceivePart(node);
                        //rps.Add(rp);
                        r = new DataFieldPicker(name, rp);
                        break;
                }
            }
            return r;
        }
    }

}
