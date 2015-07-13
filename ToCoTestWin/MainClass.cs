using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCoTestWin
{
    class MainClass
    {
        public MainClass()
        {
            // 入力クラスを用意する
            // データ到着時のイベントを登録
            SerialInput sinput = new SerialInput("COM9");
            sinput.DataArrive += new SerialInput.DataArriveEventHandler(OnPacketArrive);
            // Serial Port Open
            sinput.portOpen();
            Console.ReadLine();

            sinput.portClose();
        }

        private void OnPacketArrive(object sender, DataArriveEventArgs e)
        {
            Console.WriteLine(BitConverter.ToString(e.data));
            Debug.WriteLine(BitConverter.ToString(e.data));
            byte[] data = e.data;
            if (data.Length < 3)
            {
                // 1,2個しかこない(Lengthまできてない)
                Console.WriteLine("Invaid dataLength(size: {0})", data.Length);
                return;
            }
            if ((data[0] == 0x99) && (data[1] == 0x97))
            {
                Console.WriteLine("Node Data CRC16 Error(Master Firm)");
                return;
            }
            if ((data[0] == 0x99) && (data[1] == 0x98))
            {
                if (data[2] != data.Length)
                {
                    Console.WriteLine("Invaid dataLength(except: {0}, actual: {1})", data.Length, (int)data[2]);
                    return;
                }
                // ここから下、パケットパーサークラスを作って処理する？
                // 正規パケットのようなのでCRC16を計算する
                ushort crc = (ushort)((data[data.Length - 1] << 8) | data[data.Length - 2]);
                ushort c_crc = Util.CRC16_CCITT(data, data.Length - 2);
                //Debug.WriteLine("CRC except: {0}, actual: {1}", c_crc, crc);

            }
        }

    }
}
