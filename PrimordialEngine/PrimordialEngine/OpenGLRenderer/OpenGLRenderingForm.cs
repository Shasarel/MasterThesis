using SharpGL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PrimordialEngine.OpenGLRenderer
{
    class OpenGLRenderingForm:Form
    {
        private OpenGLControl _openGLControl;

        private System.ComponentModel.IContainer _components = null;

        private Action<OpenGL, int, int> _initializeAction;

        private Action<OpenGL> _drawAction;

        public OpenGLRenderingForm(Action<OpenGL, int, int> initializeAction, Action<OpenGL> drawAction,int windowWidth, int windowHeight)
        {
            _initializeAction = initializeAction;
            _drawAction = drawAction;

            _openGLControl = new OpenGLControl();
            _openGLControl.FrameRate = 1000;
            ((System.ComponentModel.ISupportInitialize)(_openGLControl)).BeginInit();
            SuspendLayout();
            _openGLControl.Dock = DockStyle.Fill;
            _openGLControl.DrawFPS = false;
            _openGLControl.Location = new System.Drawing.Point(0, 0);
            _openGLControl.Name = "openGLControl";
            _openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL4_4;
            _openGLControl.RenderContextType = RenderContextType.NativeWindow;
            _openGLControl.RenderTrigger = RenderTrigger.TimerBased;
            _openGLControl.Size = new System.Drawing.Size(windowWidth, windowHeight);
            _openGLControl.TabIndex = 0;
            _openGLControl.OpenGLInitialized += OpenGLControlInitialized;
            _openGLControl.OpenGLDraw += OpenGLControlDraw;
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(windowWidth, windowHeight);
            Controls.Add(_openGLControl);
            KeyDown += new KeyEventHandler(KeyDownEventMethod);
            Name = "PrimordialEngine";
            Text = "PrimordialEngine";
            ((System.ComponentModel.ISupportInitialize)(_openGLControl)).EndInit();
            ResumeLayout(false);
        }
        private void OpenGLControlInitialized(object sender, EventArgs e)
        {
            _initializeAction.Invoke(_openGLControl.OpenGL, Height, Width);
        }

        private void OpenGLControlDraw(object sender, RenderEventArgs args)
        {
            _drawAction.Invoke(_openGLControl.OpenGL);
        }

        private void KeyDownEventMethod(object sender, KeyEventArgs e)
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
