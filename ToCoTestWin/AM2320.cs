using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCoTestWin
{
    class AM2320 : Sensor
    {
        ushort Temp;
        ushort Humidity;

        public AM2320(byte[] data) : base(data)
        {
            // ここでparse
            Temp = (ushort)((rawdata[0] << 8) | rawdata[1]);
            Humidity = (ushort)((rawdata[2] << 8) | rawdata[3]);
        }

        public void ShowData()
        {
            float c_temp, c_hum;
            c_temp = Temp / 10.0f;
            c_hum = Humidity / 10.0f;
            //Console.WriteLine("Temp: {0} / Humidity: {1}", c_temp.ToString(), c_hum.ToString());
            Console.WriteLine("Temp: {0} / Humidity: {1}", Temp / 10.0f, Humidity / 10.0f);
        }
    }
}
