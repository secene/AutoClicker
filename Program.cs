using System;
using System.Runtime.InteropServices;
using System.Threading;

class AutoClicker
{
    // Import the mouse_event function from user32.dll
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

    // Import GetAsyncKeyState to detect key presses
    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    // Mouse event constants
    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int VK_F6 = 0x75; // F6 key

    static void Main()
    {
        Console.WriteLine("Press F6 to start/stop the autoclicker.");
        bool isRunning = false;

        // Event loop to check for keypresses
        while (true)
        {
            if (GetAsyncKeyState(VK_F6) < 0)
            {
                isRunning = !isRunning;
                Console.WriteLine($"Is running: {isRunning}");
                Thread.Sleep(120000); // Debounce to prevent multiple toggles
            }

            if (isRunning)
            {
                ClickMouse();
                Thread.Sleep(180000); // Interval between clicks (in milliseconds)
            }
        }
    }

    static void ClickMouse()
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        Console.WriteLine($"Mouse clicked: {DateTime.Now}!");
    }
}
