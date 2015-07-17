using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCoTestWin.Sensors
{
    class SensorUtil
    {
        private SensorUtil() { }

        public static Sensor SearchSensorFromID(List<Sensor> sensors, ushort id)
        {
            foreach (var sensor in sensors)
            {
                if (sensor.SensorID == id)
                {
                    return sensor;
                }
            }
            return null;
        }

        public static IEnumerable<ushort> ListSensorID(List<Sensor> SensorList)
        {
            foreach (var Sensor in SensorList)
            {
                yield return Sensor.SensorID;
            }
        }
    }
}