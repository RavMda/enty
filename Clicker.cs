using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable CA1806

namespace fgasfsasf;

public static class Clicker
{
    private static IntPtr _dbd;

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern short GetKeyState(int keyCode);

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

    private static void FindDBD()
    {
        EnumWindows(delegate(IntPtr handle, int param)
        {
            var builder = new StringBuilder(32);
            GetWindowText(handle, builder, 32);

            if (builder.ToString().Contains("DeadByDaylight")) _dbd = handle;

            return true;
        }, 0);
    }

    public static void Start()
    {
        var thread = new Thread(Clicky);
        thread.Start();
    }

    private static void Clicky()
    {
        while (true)
        {
            if (_dbd == IntPtr.Zero || !IsWindow(_dbd))
            {
                FindDBD();
                Thread.Sleep(2500);
                continue;
            }

            if (_dbd != GetForegroundWindow() || !Main.flashlight)
            {
                Thread.Sleep(200);
                continue;
            }

            var key = GetKeyState('C') & 0x8000;
            if (key != 0)
            {
                mouse_event(0x0008, 0, 0, 0, 0);
                mouse_event(0x0010, 0, 0, 0, 0);

                Thread.Sleep(1);
                continue;
            }

            Thread.Sleep(25);
        }
    }

    private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);
}