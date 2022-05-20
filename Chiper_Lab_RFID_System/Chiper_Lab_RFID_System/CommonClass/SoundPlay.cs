using System;
using System.Collections.Generic;
using System.Text;

namespace Chiper_Lab_RFID_System.CommonClass
{
    public interface SoundsPlay
    {
        void PlaySound(string MsgType);
        void StopSound();
    }
}
