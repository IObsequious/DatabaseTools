using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

public static class ConsoleEx
{
    public static void Write(string text, params object[] args) => Console.Write(text, args);
    public static void WriteLine(string text, params object[] args) => Console.WriteLine(text, args);

    private static void WritePaddedLine(string text)
    {
        int spacesToWrite = Console.WindowWidth - text.Length;
        if (spacesToWrite > 0)
            Console.WriteLine(text + new string(' ', spacesToWrite - 1));
        else
            Console.WriteLine(text);
    }

    private static void WriteProgressLine(int percentComplete, int level = 1)
    {
        char symbol = 'o';

        int windowWidth = Console.WindowWidth;

        windowWidth = windowWidth - 7;

        int adjustedLevel = level + 3;

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < adjustedLevel; i++)
        {
            sb.Append(' ');
        }
        sb.Append("[");

        windowWidth = windowWidth - adjustedLevel + 1;

        double percent = windowWidth * 0.01;

        int adjustedPercent = (int)(percentComplete * percent);
        int remainder = 110 - adjustedPercent;

        sb.Append(new string(symbol, adjustedPercent));
        if (percentComplete < 100)
            sb.Append(new string(' ', remainder));
        sb.Append("]  ");

        WritePaddedLine(sb.ToString());

    }

    private static void WriteProgressBanner()
    {
        Color teal = Color.Teal;
        SetColor(ConsoleColor.DarkMagenta, teal);
        Console.BackgroundColor = ConsoleColor.DarkMagenta;

        Console.ForegroundColor = ConsoleColor.Yellow;
    }

    public static void WriteProgress(string activity, string status, string operation = null, int percentComplete = 0, int secondsRemaining = 0)
    {

        WriteProgressBanner();
        int left = Console.CursorLeft;
        int top = Console.CursorTop;
        Console.SetCursorPosition(0, 3);
        WritePaddedLine("");
        WritePaddedLine("");
        WritePaddedLine($" {activity}");
        WritePaddedLine($"    {status}");
        if (percentComplete > 0)
            WriteProgressLine(percentComplete);
        if (secondsRemaining > 0)
            WritePaddedLine($"    {TimeSpan.FromSeconds(secondsRemaining)} remaining.");
        WritePaddedLine("");
        WritePaddedLine("");
        if (!string.IsNullOrEmpty(operation))
            WritePaddedLine($"    {operation}");
        WritePaddedLine("");
        ResetProgressBanner();
        Console.WriteLine();
        Console.WriteLine();
        Console.SetCursorPosition(left, top);
    }

    private static void ResetProgressBanner()
    {
        Color black = Color.Black;
        SetColor(ConsoleColor.Black, black);
        Console.BackgroundColor = ConsoleColor.Black;

        //Color gray = Color.Gray;
        //SetColor(ConsoleColor.Gray, gray);
        Console.ForegroundColor = ConsoleColor.White;

    }

    public static int SetScreenColors(Color foregroundColor, Color backgroundColor)
    {
        int irc;
        irc = SetColor(ConsoleColor.Gray, foregroundColor);
        if (irc != 0) return irc;
        irc = SetColor(ConsoleColor.Black, backgroundColor);
        if (irc != 0) return irc;

        return 0;
    }
    public static int SetColor(ConsoleColor consoleColor, Color targetColor)
    {
        return SetColor(consoleColor, targetColor.R, targetColor.G, targetColor.B);
    }

    public static int SetColor(ConsoleColor color, uint r, uint g, uint b)
    {
        CONSOLE_SCREEN_BUFFER_INFO_EX csbe = new CONSOLE_SCREEN_BUFFER_INFO_EX();
        csbe.cbSize = (int)Marshal.SizeOf(csbe);                    // 96 = 0x60
        IntPtr hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE);    // 7
        if (hConsoleOutput == INVALID_HANDLE_VALUE)
        {
            return Marshal.GetLastWin32Error();
        }
        bool brc = GetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
        if (!brc)
        {
            return Marshal.GetLastWin32Error();
        }

        switch (color)
        {
            case ConsoleColor.Black:
                csbe.black = new COLORREF(r, g, b);
                break;
            case ConsoleColor.DarkBlue:
                csbe.darkBlue = new COLORREF(r, g, b);
                break;
            case ConsoleColor.DarkGreen:
                csbe.darkGreen = new COLORREF(r, g, b);
                break;
            case ConsoleColor.DarkCyan:
                csbe.darkCyan = new COLORREF(r, g, b);
                break;
            case ConsoleColor.DarkRed:
                csbe.darkRed = new COLORREF(r, g, b);
                break;
            case ConsoleColor.DarkMagenta:
                csbe.darkMagenta = new COLORREF(r, g, b);
                break;
            case ConsoleColor.DarkYellow:
                csbe.darkYellow = new COLORREF(r, g, b);
                break;
            case ConsoleColor.Gray:
                csbe.gray = new COLORREF(r, g, b);
                break;
            case ConsoleColor.DarkGray:
                csbe.darkGray = new COLORREF(r, g, b);
                break;
            case ConsoleColor.Blue:
                csbe.blue = new COLORREF(r, g, b);
                break;
            case ConsoleColor.Green:
                csbe.green = new COLORREF(r, g, b);
                break;
            case ConsoleColor.Cyan:
                csbe.cyan = new COLORREF(r, g, b);
                break;
            case ConsoleColor.Red:
                csbe.red = new COLORREF(r, g, b);
                break;
            case ConsoleColor.Magenta:
                csbe.magenta = new COLORREF(r, g, b);
                break;
            case ConsoleColor.Yellow:
                csbe.yellow = new COLORREF(r, g, b);
                break;
            case ConsoleColor.White:
                csbe.white = new COLORREF(r, g, b);
                break;
        }
        ++csbe.srWindow.Bottom;
        ++csbe.srWindow.Right;
        brc = SetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
        if (!brc)
        {
            return Marshal.GetLastWin32Error();
        }
        return 0;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct COORD
    {
        internal short X;
        internal short Y;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct SMALL_RECT
    {
        internal short Left;
        internal short Top;
        internal short Right;
        internal short Bottom;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct COLORREF
    {
        internal uint ColorDWORD;

        internal COLORREF(Color color)
        {
            ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
        }

        internal COLORREF(uint r, uint g, uint b)
        {
            ColorDWORD = r + (g << 8) + (b << 16);
        }

        internal Color GetColor()
        {
            return Color.FromArgb((int)(0x000000FFU & ColorDWORD),
               (int)(0x0000FF00U & ColorDWORD) >> 8, (int)(0x00FF0000U & ColorDWORD) >> 16);
        }

        internal void SetColor(Color color)
        {
            ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct CONSOLE_SCREEN_BUFFER_INFO_EX
    {
        internal int cbSize;
        internal COORD dwSize;
        internal COORD dwCursorPosition;
        internal ushort wAttributes;
        internal SMALL_RECT srWindow;
        internal COORD dwMaximumWindowSize;
        internal ushort wPopupAttributes;
        internal bool bFullscreenSupported;
        internal COLORREF black;
        internal COLORREF darkBlue;
        internal COLORREF darkGreen;
        internal COLORREF darkCyan;
        internal COLORREF darkRed;
        internal COLORREF darkMagenta;
        internal COLORREF darkYellow;
        internal COLORREF gray;
        internal COLORREF darkGray;
        internal COLORREF blue;
        internal COLORREF green;
        internal COLORREF cyan;
        internal COLORREF red;
        internal COLORREF magenta;
        internal COLORREF yellow;
        internal COLORREF white;
    }

    const int STD_OUTPUT_HANDLE = -11;                                        // per WinBase.h
    internal static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);    // per WinBase.h

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);
}

