using Rewired;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Divide.PCInput
{
    internal static class KeyBindings
    {
        // Configure your key binding here
        public const KeyboardKeyCode KEY_UP = KeyboardKeyCode.W;
        public const KeyboardKeyCode KEY_DOWN = KeyboardKeyCode.S;
        public const KeyboardKeyCode KEY_LEFT = KeyboardKeyCode.A;
        public const KeyboardKeyCode KEY_RIGHT = KeyboardKeyCode.D;
        public const KeyboardKeyCode KEY_SPRINT = KeyboardKeyCode.LeftShift;
        public const KeyboardKeyCode KEY_TOGGLE_SOLUS = KeyboardKeyCode.Tab;
        public const KeyboardKeyCode KEY_TOGGLE_MODE = KeyboardKeyCode.F;
        public const KeyboardKeyCode KEY_DRAW_WEAPON = KeyboardKeyCode.LeftControl;
        public const KeyboardKeyCode KEY_LEFT_STICK_CLICK = KeyboardKeyCode.Q;
        public const KeyboardKeyCode KEY_RIGHT_STICK_CLICK = KeyboardKeyCode.E;
        public const KeyboardKeyCode KEY_START = KeyboardKeyCode.Escape;
        public const KeyboardKeyCode KEY_PRIMARY_TRIGGER = KeyboardKeyCode.Space;
        public const MouseInputElement MOUSE_PRIMARY = MouseInputElement.Button0;
        public const MouseInputElement MOUSE_SECONDARY = MouseInputElement.Button1;
    }
}
