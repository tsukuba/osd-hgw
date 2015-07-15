using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToCoTestWin
{
    // 非同期用
    // http://stackoverflow.com/questions/23230375/sample-serial-port-comms-code-using-async-api-in-net-4-5
    public static class TaskExt
    {
        public static async Task<TEventArgs> FromEvent<TEventHandler, TEventArgs>(
            Func<Action<TEventArgs>, Action, Action<Exception>, TEventHandler> getHandler,
            Action<TEventHandler> subscribe,
            Action<TEventHandler> unsubscribe,
            Action<Action<TEventArgs>, Action, Action<Exception>> initiate,
            CancellationToken token) where TEventHandler : class
        {
            var tcs = new TaskCompletionSource<TEventArgs>();
            Action<TEventArgs> complete = (args) => tcs.TrySetResult(args);
            Action cancel = () => tcs.TrySetCanceled();
            Action<Exception> reject = (ex) => tcs.TrySetException(ex);

            TEventHandler handler = getHandler(complete, cancel, reject);

            subscribe(handler);

            try
            {
                using (token.Register(() => tcs.TrySetCanceled()))
                {
                    initiate(complete, cancel, reject);
                    return await tcs.Task;
                }
            }
            finally
            {
                unsubscribe(handler);
            }
        }
    }
    class Util
    {
        private Util() { }

        public static ushort CRC16_CCITT(byte[] data, int len)
        {
            ushort crc = 0xFFFF;
            int i, j;

            for (i = 0; i < len; i++)
            {
                crc ^= (ushort)(data[i] << 8);
                for (j = 0; j < 8; j++)
                {
                    if ((crc & 0x8000) != 0)
                    {
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    } else
                    {
                        crc <<= 1;
                    }
                }
            }
            return crc;
        }
    }
}
