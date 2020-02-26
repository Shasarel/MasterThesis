using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PrimordialEngine
{
    class DirectXForm
    {
        public RenderForm renderForm;

        private const int Width = 1280;
        private const int Height = 720;

        public DirectXForm()
        {
            renderForm = new RenderForm("My first SharpDX game");
            renderForm.ClientSize = new Size(Width, Height);
            renderForm.AllowUserResizing = false;
            //RenderLoop.Run
        }
    }
}
