using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCoTestWin
{
    class PacketParser
    {
        private byte[] packet;
        public PacketParser() { }

        public void SetPacket(byte[] data)
        {
            packet = data;
        }

        public void ParsePacket()
        {
            // 正規パケットのようなのでCRC16を計算する
            ushort crc = (ushort)((packet[packet.Length - 1] << 8) | packet[packet.Length - 2]);
            ushort c_crc = Util.CRC16_CCITT(packet, packet.Length - 2);
            if (c_crc != crc)
            {
                // CRC16の計算結果が異なる(=パケットが化けてる)
                // パケットを処理しない
                Debug.WriteLine("CRC except: {0}, actual: {1}", c_crc, crc);
                return;
            }
            uint srcAddr = BitConverter.ToUInt32(packet, 3);
            Console.WriteLine("Source Address: {0}, LQI: {1} ({2}%)", srcAddr, packet[7], ((packet[7] / 255.0f) * 100));
            Packet p = new Packet();
            p.srcAddr = srcAddr;
            p.LQI = packet[7];
            p.Cmd = BitConverter.ToUInt16(packet, 9);
            if (p.Cmd == 0) return;
            p.payload = new byte[packet[11]];
            Array.Copy(packet, 12, p.payload, 0, packet[11]);
            p.DebugData();
        }
    }
}
