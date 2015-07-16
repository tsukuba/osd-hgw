namespace ToCoTestWin
{
    abstract class _Sensor
    {
        public short SensorID { get; protected set; }
        // シリアルからの生データ
        protected byte[] rawdata;
        // 他、センサ固有データはそれぞれのクラスで扱う

        public _Sensor(byte[] data)
        {
            rawdata = data;
        }
    }
}
