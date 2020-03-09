using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PrimordialEngine
{
    class InputManager
    {
        private List<Keys> _keysDown;
        public InputManager(UserControl userControl)
        {
            _keysDown = new List<Keys>();
            userControl.KeyDown += KeyDownEvent;
            userControl.KeyUp += KeyUpEvent;
        }

        private void KeyDownEvent(object sender, KeyEventArgs args)
        {
            _keysDown.Add(args.KeyCode);
        }
        private void KeyUpEvent(object sender, KeyEventArgs args)
        {
            _keysDown.Remove(args.KeyCode);
        }

        public bool IsKeyDown(Keys key)
        {
            return _keysDown.Contains(key);
        }
    }
}
