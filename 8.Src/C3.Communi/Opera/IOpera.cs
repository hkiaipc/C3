﻿using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public interface IOpera
    {
        string Name { get; set; }
        string Text { get; set; }
        byte[] CreateSendBytes(IDevice device);

        SendPart SendPart { get; set; }

        ReceivePartCollection ReceiveParts { get; set; }

        IParseResult ParseReceivedBytes(IDevice device, byte[] received);
    }
}
