using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Debug.WriteLine("LQI: {0}", LQI);
            Debug.WriteLine("Cmd: {0}", Cmd);
            Debug.WriteLine("payload: {0}", BitConverter.ToString(payload));
        }
    }
}
