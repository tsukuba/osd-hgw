using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCoTestWin
{
    abstract class Sensor
    {
        // シリアルからの生データ
        protected byte[] rawdata;
        // 他、センサ固有データはそれぞれのクラスで扱う

        public Sensor(byte[] data)
        {
            rawdata = data;
        }
    }
}
