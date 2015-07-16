using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCoTestWin.Sensors
{
    class BME280 : Sensor
    {
        private int Temp, Temp2;
        private uint Humidity, Pressure;

        public BME280() : this(null) { }

        public BME280(byte[] data) : base(data)
        {
            SensorID = 0x02;
            if (data != null) { ParseData(); }
        }

        public void ShowData()
        {
            Console.WriteLine("Temp: {0}, Hum: {1}, Pre: {2}", Temp/100.0f, Humidity/1024.0f, Pressure/100.0f);
            Console.WriteLine("Temp: {0}, Hum: {1}, Pre: {2}", Temp, Humidity, Pressure);
        }

        public override bool ParseData()
        {
            if (rawdata == null) return false;

            Temp = BitConverter.ToInt32(rawdata, 0);
            Humidity = BitConverter.ToUInt32(rawdata, 4);
            Pressure = BitConverter.ToUInt32(rawdata, 8);

            return true;
        }
    }
}
