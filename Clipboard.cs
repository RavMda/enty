using System.Runtime.InteropServices;

namespace fgasfsasf;

public static class Clipboard
{
    [DllImport("user32.dll")]
    private static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll")]
    private static extern bool CloseClipboard();

    [DllImport("user32.dll")]
    private static extern bool SetClipboardData(uint uFormat, IntPtr data);

    public static void Set(string text)
    {
        OpenClipboard(IntPtr.Zero);
        var ptr = Marshal.StringToHGlobalUni(text);
        SetClipboardData(13, ptr);
        CloseClipboard();
        Marshal.FreeHGlobal(ptr);
    }
}