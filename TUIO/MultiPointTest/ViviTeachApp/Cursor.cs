using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Drawing;

namespace MT
{
    class TCursor
    {
        public Point Position;
        public long FingerID;
        public int SessionID;
        public string DeviceId;

        public TCursor(Point Position, long FingerID, int SessionID,string DeviceId) {
            this.Position = Position;
            this.FingerID = FingerID;
            this.SessionID = SessionID;
            this.DeviceId = DeviceId;
        }
    }
}
