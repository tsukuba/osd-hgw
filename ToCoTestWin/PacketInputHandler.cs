using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCoTestWin
{
    class PacketInputHandler
    {
        public delegate void DataArriveEventHandler(object sender, DataArriveEventArgs e);

        public event DataArriveEventHandler DataArrive;

        protected virtual void OnDataArrive(DataArriveEventArgs e)
        {
            if (DataArrive != null)
            {
                DataArrive(this, e);
            }
        }
    }

    public class DataArriveEventArgs : EventArgs
    {
        public byte[] data;
    }
}
