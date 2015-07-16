using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCoTestWin.Sensors
{
    class LPS331 : Sensor
    {
        ushort Temp;
        uint Pressure;

        public LPS331() : this(null) { }

        public LPS331(byte[] data) : base(data) 
        {
            SensorID = 0x02;
            Pressure = BitConverter.ToUInt32(rawdata, 0);
            Temp = BitConverter.ToUInt16(rawdata, 5);
        }

        public void ShowData()
        {
            double press, temp;
            if ((Pressure & (1 << 23)) != 0)
            {
                 press = -((Pressure ^ 0xFFFFFF) + 1) / 4096.0f;
            }
            else
            {
                press = Pressure / 4096.0f;
            }

            if ((Temp & (1 << 15)) != 0)
            {
                temp = 42.5 - ((Temp ^ 0xFFFF) + 1) / 480.0f;
            }
            else
            {
                temp = 42.5 + Temp / 480.0f;
            }
            
            Console.WriteLine("Temp: {0}, Hum: {1}, Pre: {2}", temp, press);
            Console.WriteLine("Temp: {0}, Hum: {1}, Pre: {2}", Temp, Pressure);
        }
    }
}
