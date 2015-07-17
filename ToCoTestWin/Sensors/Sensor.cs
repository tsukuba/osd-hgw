namespace ToCoTestWin.Sensors
{
    abstract class Sensor
    {
        // センサ固有ID
        // クラス内でしか設定出来ないようにしてある
        public ushort SensorID { get; protected set; }
        
        // シリアルからの生データ
        protected byte[] rawdata;

        // パースしたかどうか（処理してないときになんかしてもヤバイ）
        protected bool IsParsed;

        // 他、センサ固有データはそれぞれのクラスで扱う

        public Sensor(byte[] data)
        {
            if (data != null) rawdata = data;
            IsParsed = false;
        }

        public abstract bool ParseData();
        public abstract void ShowData();
        public void SetData(byte[] data) { rawdata = data; }
    }
}
