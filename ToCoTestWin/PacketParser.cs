using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToCoTestWin.Sensors;

namespace ToCoTestWin
{
    class PacketParser
    {
        private byte[] packet;
        private Queue<ushort> sensorQueue;
        private List<Sensor> sensors;
        public PacketParser() { }

        public void SetPacket(byte[] data)
        {
            packet = data;
            sensors.Add(new AM2320());
            sensors.Add(new BME280());
            sensors.Add(new LPS331());
        }

        public void ParsePacket()
        {
            // 正規パケットのようなのでCRC16を計算する
            ushort crc = BitConverter.ToUInt16(packet, packet.Length - 2);
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
            p.payload = new byte[packet[11]];
            // HELLO Packet
            if (p.Cmd == 0) return;
            // PacketQueue
            if (p.Cmd == 1)
            {
                for (int i = 0; i < p.payload.Length; i+=2)
                {
                    sensorQueue.Enqueue(BitConverter.ToUInt16(p.payload, i));
                }
                return;
            }
            if (p.Cmd == 2)
            {
                // ヘッダダンプ
                p.DebugData();
                // 0-1
                ushort sensorID = BitConverter.ToUInt16(p.payload, 0);
                Sensor s = SensorUtil.SearchSensorFromID(sensors, sensorID);
                byte[] data = new byte[p.payload.Length - 2];
                Array.Copy(p.payload, 2, data, 0, p.payload.Length - 2);
                s.SetData(data);
                s.ParseData();
            }
        }
    }
}
