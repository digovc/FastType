using System;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace FastType
{
    public static class TypeWriter
    {
        private static InputSimulator input = new();
        private static Random random = new();

        internal static void Init()
        {
            InterceptKeys.HotkeyCallback = HotkeyCallback;
        }

        private static void HotkeyCallback()
        {
            if (input.InputDeviceState.IsHardwareKeyUp(VirtualKeyCode.CONTROL))
            {
                return;
            }

            if (input.InputDeviceState.IsHardwareKeyUp(VirtualKeyCode.SHIFT))
            {
                return;
            }

            var clipboard = Clipboard.GetText()?.Trim();

            if (string.IsNullOrEmpty(clipboard))
            {
                return;
            }

            var lines = clipboard.Split(Environment.NewLine);

            foreach (var line in lines)
            {
                TypeLine(line);
            }
        }

        private static bool IsEscPressed()
        {
            return input.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.ESCAPE);
        }

        private static void TypeChar(char character)
        {
            if (IsEscPressed())
            {
                return;
            }

            Application.DoEvents();

            input.Keyboard.TextEntry(character);
            var delay = random.NextDouble() * 77f + 25f;
            Thread.Sleep((int)delay);
        }

        private static void TypeLine(string line)
        {
            if (IsEscPressed())
            {
                return;
            }

            Application.DoEvents();

            line = line.Trim();

            if (string.IsNullOrEmpty(line))
            {
                return;
            }

            foreach (var character in line)
            {
                TypeChar(character);
            }

            input.Keyboard.KeyPress(VirtualKeyCode.RETURN);
        }
    }
}