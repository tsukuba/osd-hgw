using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCoTestWin
{
    class BME280 : Sensor
    {
        int Temp, Temp2;
        uint Humidity, Pressure;

        public BME280(byte[] data) : base(data)
        {
            Temp = BitConverter.ToInt32(data, 0);
            Humidity = BitConverter.ToUInt32(data, 4);
            Pressure = BitConverter.ToUInt32(data, 8);
        }

        public void ShowData()
        {
            Console.WriteLine("Temp: {0}, Hum: {1}, Pre: {2}", Temp/100.0f, Humidity/1024.0f, Pressure/100.0f);
            Console.WriteLine("Temp: {0}, Hum: {1}, Pre: {2}", Temp, Humidity, Pressure);
        }
    }
}
