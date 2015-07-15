using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToCoTestWin
{
    class SerialInput : PacketInputHandler
    {
        SerialPort com;
        CancellationTokenSource cts;
        Task task;

        public SerialInput(string port)
        {            
            com = new SerialPort(port);
            com.BaudRate = 115200;
            com.Parity = Parity.None;
            com.StopBits = StopBits.One;
        }

        public async Task ReadPort(SerialPort com, CancellationToken token)
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();

                // 意味不明の非同期イベント
                // http://stackoverflow.com/questions/23230375/sample-serial-port-comms-code-using-async-api-in-net-4-5
                await TaskExt.FromEvent<SerialDataReceivedEventHandler, SerialDataReceivedEventArgs>(
                    (complete, cancel, reject) =>
                        (sender, args) => complete(args),
                    handler =>
                        com.DataReceived += handler,
                    handler =>
                        com.DataReceived -= handler,
                    (complete, cancel, reject) =>
                        { if (com.BytesToRead != 0) complete(null); },
                    token);
                byte[] data = new byte[com.BytesToRead];
                com.Read(data, 0, com.BytesToRead);
                DataArriveEventArgs e = new DataArriveEventArgs();
                e.data = data;
                OnDataArrive(e);
            }
        }


        public bool isOpen()
        {
            return (com == null) ? false : com.IsOpen;
        }

        public void portOpen()
        {
            if (com == null) return;
            if (com.IsOpen) com.Close();

            com.Open();

            cts = new CancellationTokenSource();
            task = ReadPort(com, cts.Token);
        }

        public void portClose()
        {
            cts.Cancel();
            try
            {
                task.Wait();
            } catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }

            com.Close();
        }
    }
}