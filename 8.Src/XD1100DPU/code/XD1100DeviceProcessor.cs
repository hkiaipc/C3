using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;
using Xdgk.Communi.Interface;
using Xdgk.GR.Data;
using NLog;

namespace XD1100DPU
{
    /// <summary>
    /// 
    /// </summary>
    public class XD1100DeviceProcessor : TaskProcessorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pr"></param>
        public override void OnProcess(ITask task, IParseResult pr)
        {
            if (pr.IsSuccess)
            {
                string opera = task.Opera.Name;
                if (StringHelper.Equal(opera, XD1100OperaNames.ReadReal))
                {
                    XD1100Device d = (XD1100Device)task.Device;
                    ProcessReadReal(d, pr);
                }
                else if (StringHelper.Equal(opera, XD1100OperaNames.OPERA_READ))
                {
                    // nothing
                }
                else if (StringHelper.Equal(opera, XD1100OperaNames.OPERA_WRITE))
                {
                    // nothing
                }
                else
                {
                    throw new NotImplementedException(
                        string.Format("not process xd1100 opera '{0}'", opera)
                        );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="pr"></param>
        private void ProcessReadReal(XD1100Device d, IParseResult pr)
        {
            XD1100Data data = new XD1100Data();

            data.DT = DateTime.Now;

            data.GT1 = Convert.ToSingle(pr.Results["GT1"]);
            data.BT1 = Convert.ToSingle(pr.Results["BT1"]);
            data.GT2 = Convert.ToSingle(pr.Results["GT2"]);
            data.BT2 = Convert.ToSingle(pr.Results["BT2"]);
            data.OT = Convert.ToSingle(pr.Results["OT"]);
            data.GTBase2 = Convert.ToSingle(pr.Results["GTBase2"]);
            data.GP1 = Convert.ToSingle(pr.Results["GP1"]);
            data.BP1 = Convert.ToSingle(pr.Results["BP1"]);
            data.WL = Convert.ToSingle(pr.Results["WL"]);
            data.GP2 = Convert.ToSingle(pr.Results["GP2"]);
            data.BP2 = Convert.ToSingle(pr.Results["BP2"]);
            data.I1 = Convert.ToSingle(pr.Results["I1"]);
            data.IR = Convert.ToSingle(pr.Results["IR"]);
            data.I2 = Convert.ToSingle(pr.Results["I2"]);
            data.S2 = Convert.ToInt32(pr.Results["S2"]);
            data.S1 = Convert.ToInt32(pr.Results["S1"]);
            data.SR = Convert.ToInt32(pr.Results["SR"]);
            data.OD = Convert.ToInt32(pr.Results["OD"]);

            PumpStateCollection pss = (PumpStateCollection)pr.Results["pumpstate"];
            foreach (PumpState ps in pss)
            {
                if (ps.PumpTypeEnum == PumpTypeEnum.CyclePump)
                {
                    switch (ps.PumpNO)
                    {
                        case 1:
                            data.CM1 = IsPumpRun(ps.PumpStateEnum);
                            break;
                        case 2:
                            data.CM2 = IsPumpRun(ps.PumpStateEnum);
                            break;
                        case 3:
                            data.CM3 = IsPumpRun(ps.PumpStateEnum);
                            break;
                        default:
                            break;
                    }
                }
                else if (ps.PumpTypeEnum == PumpTypeEnum.RecruitPump)
                {
                    switch (ps.PumpNO)
                    {
                        case 1:
                            data.RM1 = IsPumpRun(ps.PumpStateEnum);
                            break;
                        case 2:
                            data.RM2 = IsPumpRun(ps.PumpStateEnum);
                            break;
                        default:
                            break;
                    }
                }
            }

            d.DeviceDataManager.Last = data;

            // save
            //
            int id = GuidHelper.ConvertToInt32(d.Guid);
            DBI.Instance.InsertXD1100Data(id, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pse"></param>
        /// <returns></returns>
        PumpStatus IsPumpRun(PumpStateEnum pse)
        {
            if (pse == PumpStateEnum.Run)
            {
                return PumpStatus.Run;
            }
            else if (pse == PumpStateEnum.Stop)
            {
                return PumpStatus.Stop;
            }
            throw new ArgumentException(pse.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="pr"></param>
        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {
            throw new NotImplementedException();
        }
    }

}
