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
        private PacketParser parser = new PacketParser();
        public MainClass()
        {
            // 入力クラスを用意する
            // データ到着時のイベントを登録
            SerialInput sinput = new SerialInput("COM5");
            sinput.DataArrive += new SerialInput.DataArriveEventHandler(OnPacketArrive);
            // Serial Port Open
            sinput.portOpen();

            Console.ReadLine();

            sinput.portClose();
        }

        private void OnPacketArrive(object sender, DataArriveEventArgs e)
        {
            Debug.WriteLine(BitConverter.ToString(e.data));
            // パケットデータをコピー
            byte[] data = e.data;

            // パケット長のバリデーション
            if (data.Length < 3)
            {
                // 1,2個しかこない(Lengthまできてない)
                // パケットを破棄
                Console.WriteLine("Invaid dataLength(size: {0})", data.Length);
                return;
            }
            if ((data[0] == 0x99) && (data[1] == 0x97))
            {
                // ToCoStickからNodeのCRCが一致しなかったとの連絡
                // パケットを破棄
                Console.WriteLine("Node Data CRC16 Error(from Master Firm)");
                return;
            }
            // 正規のmagic byte
            if ((data[0] == 0x99) && (data[1] == 0x98))
            {
                // パケット長の確認
                if (data[2] != data.Length)
                {
                    // パケット長が異なるため、破棄
                    Console.WriteLine("Invaid dataLength(except: {0}, actual: {1})", data.Length, (int)data[2]);
                    return;
                }
                parser.SetPacket(data);
                parser.ParsePacket();
            }
        }

    }
}
