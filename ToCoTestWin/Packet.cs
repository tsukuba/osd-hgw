using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using ToCoTestWin.Sensors;

namespace ToCoTestWin
{
    class Packet
    {
        public uint srcAddr { get; set; }
        public byte LQI { get; set; }
        public ushort Cmd { get; set; }
        public byte[] payload { get; set; }

        // Constructor
        public Packet() { }

        public void DebugData()
        {
            Debug.WriteLine("srcAddr: {0}", srcAddr);
            Debug.WriteLine("LQI: {0} ({1}%)", LQI, (LQI / 255.0f) * 100.0f);
            Debug.WriteLine("Cmd: {0}", Cmd);
        }
    }
}
