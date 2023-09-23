using System.Windows.Controls;

namespace Saper
{
    class pole
    {
        public bool isBomb { get; set; } = false;
        public bool isOpened { get; set; } = false;
        public int bombsNear { get; set; } = 0;
        public bool isChecked { get; set; } = false;
        public Button? button { get; set; }
    }
}
