using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCoTestWin.Sensors
{
    class AM2320 : Sensor
    {
        private ushort Temp;
        private ushort Humidity;

        public AM2320() : this(null) { }

        public AM2320(byte[] data) : base(data)
        {
            SensorID = 0x01;
            if (data != null) ParseData();
        }

        public override bool ParseData()
        {
            if (rawdata == null) return false;

            Temp = BitConverter.ToUInt16(rawdata, 0);
            Humidity = BitConverter.ToUInt16(rawdata, 2);

            return true;
        }

        public override void ShowData()
        {
            Console.WriteLine("Temp: {0} / Humidity: {1}", Temp / 10.0f, Humidity / 10.0f);
        }
    }
}
