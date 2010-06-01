using System.Windows.Forms;

namespace QuestTracker.QuestControls
{
    public class FixedLabel : Label
    {
        private const int WM_GETTEXT = 0xD;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private bool doubleclickflag;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDBLCLK)
            {
                doubleclickflag = true;
            }
            if (m.Msg == WM_GETTEXT && doubleclickflag)
            {
                doubleclickflag = false;
                return;
            }
            base.WndProc(ref m);
        }
    }
}