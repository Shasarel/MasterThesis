using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimordialEngine
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            new OpenGLForm();
            //Application.Run(new DirectXForm().renderForm);
        }
    }
}
