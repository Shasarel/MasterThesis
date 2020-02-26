using System;
using System.Windows;

namespace PrimordialEngine
{
    public partial class App
    {
        private void ApplicationStartup(object sender, StartupEventArgs e) {
            var graphicApi = PrimordialEngine.Properties.Settings.Default?.GraphicAPI?.ToUpper();

            switch (graphicApi)
            {
                case "OPENGL":
                    StartupUri = new Uri("OpenGLWindow.xaml", System.UriKind.Relative);
                    break;
                case "DIRECTX":
                    StartupUri = new Uri("OpenGLWindow.xaml", System.UriKind.Relative);
                    break;
                default:
                    return;
            }
        }
    }
}
