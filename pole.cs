using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls;

namespace Saper
{
    public class pole
    {
        public bool isBomb { get; set; } = false;
        public bool isOpened { get; set; } = false;
        public int bombsNear { get; set; } = 0;
        public bool isChecked { get; set; } = false;
        public Button? button { get; set; }

        ~pole()
        {
            button = null;
        }
    }
}
