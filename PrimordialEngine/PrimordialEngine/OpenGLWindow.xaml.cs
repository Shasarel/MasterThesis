using System.Windows;
using SharpGL;
using SharpGL.SceneGraph;

namespace PrimordialEngine
{
    public partial class OpenGLWindow
    {
        public OpenGLWindow()
        {
            if (Properties.Settings.Default.WindowAutoSize)
            {
                Height = SystemParameters.PrimaryScreenWidth;
                Width = SystemParameters.PrimaryScreenHeight;
            }
            else
            {
                Height = Properties.Settings.Default.WindowHeight;
                Width = Properties.Settings.Default.WindowWidth;
            }

            InitializeComponent();
        }

        private void OpenGLControl_OnOpenGLDraw(object sender, OpenGLEventArgs args)
        {
             //  Get the OpenGL instance that's been passed to us.
            OpenGL gl = args.OpenGL;

            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Reset the modelview matrix.
            gl.LoadIdentity();         

            //  Move the geometry into a fairly central position.
            gl.Translate(-1.5f, 0.0f, -6.0f);

            //  Draw a pyramid. First, rotate the modelview matrix.
            gl.Rotate(rotatePyramid, 0.0f, 1.0f, 0.0f);

            //  Start drawing triangles.
            gl.Begin(OpenGL.GL_TRIANGLES);

                gl.Color(1.0f, 0.0f, 0.0f);        
                gl.Vertex(0.0f, 1.0f, 0.0f);    
                gl.Color(0.0f, 1.0f, 0.0f);        
                gl.Vertex(-1.0f, -1.0f, 1.0f);    
                gl.Color(0.0f, 0.0f, 1.0f);        
                gl.Vertex(1.0f, -1.0f, 1.0f);    

                gl.Color(1.0f, 0.0f, 0.0f);        
                gl.Vertex(0.0f, 1.0f, 0.0f);    
                gl.Color(0.0f, 0.0f, 1.0f);        
                gl.Vertex(1.0f, -1.0f, 1.0f);    
                gl.Color(0.0f, 1.0f, 0.0f);        
                gl.Vertex(1.0f, -1.0f, -1.0f);    

                gl.Color(1.0f, 0.0f, 0.0f);        
                gl.Vertex(0.0f, 1.0f, 0.0f);    
                gl.Color(0.0f, 1.0f, 0.0f);        
                gl.Vertex(1.0f, -1.0f, -1.0f);    
                gl.Color(0.0f, 0.0f, 1.0f);        
                gl.Vertex(-1.0f, -1.0f, -1.0f);    

                gl.Color(1.0f, 0.0f, 0.0f);        
                gl.Vertex(0.0f, 1.0f, 0.0f);    
                gl.Color(0.0f, 0.0f, 1.0f);        
                gl.Vertex(-1.0f, -1.0f, -1.0f);    
                gl.Color(0.0f, 1.0f, 0.0f);        
                gl.Vertex(-1.0f, -1.0f, 1.0f);    

            gl.End();                        

            //  Reset the modelview.
            gl.LoadIdentity();

            //  Move into a more central position.
            gl.Translate(1.5f, 0.0f, -7.0f);

            //  Rotate the cube.
            gl.Rotate(rquad, 1.0f, 1.0f, 1.0f);

            //  Provide the cube colors and geometry.
            gl.Begin(OpenGL.GL_QUADS);            

                gl.Color(0.0f, 1.0f, 0.0f);            
                gl.Vertex(1.0f, 1.0f, -1.0f);        
                gl.Vertex(-1.0f, 1.0f, -1.0f);        
                gl.Vertex(-1.0f, 1.0f, 1.0f);        
                gl.Vertex(1.0f, 1.0f, 1.0f);        

                gl.Color(1.0f, 0.5f, 0.0f);            
                gl.Vertex(1.0f, -1.0f, 1.0f);        
                gl.Vertex(-1.0f, -1.0f, 1.0f);        
                gl.Vertex(-1.0f, -1.0f, -1.0f);        
                gl.Vertex(1.0f, -1.0f, -1.0f);        

                gl.Color(1.0f, 0.0f, 0.0f);            
                gl.Vertex(1.0f, 1.0f, 1.0f);        
                gl.Vertex(-1.0f, 1.0f, 1.0f);        
                gl.Vertex(-1.0f, -1.0f, 1.0f);        
                gl.Vertex(1.0f, -1.0f, 1.0f);        

                gl.Color(1.0f, 1.0f, 0.0f);            
                gl.Vertex(1.0f, -1.0f, -1.0f);        
                gl.Vertex(-1.0f, -1.0f, -1.0f);        
                gl.Vertex(-1.0f, 1.0f, -1.0f);        
                gl.Vertex(1.0f, 1.0f, -1.0f);        

                gl.Color(0.0f, 0.0f, 1.0f);            
                gl.Vertex(-1.0f, 1.0f, 1.0f);        
                gl.Vertex(-1.0f, 1.0f, -1.0f);        
                gl.Vertex(-1.0f, -1.0f, -1.0f);        
                gl.Vertex(-1.0f, -1.0f, 1.0f);        

                gl.Color(1.0f, 0.0f, 1.0f);            
                gl.Vertex(1.0f, 1.0f, -1.0f);        
                gl.Vertex(1.0f, 1.0f, 1.0f);        
                gl.Vertex(1.0f, -1.0f, 1.0f);        
                gl.Vertex(1.0f, -1.0f, -1.0f);        

            gl.End();

            //  Flush OpenGL.
            gl.Flush();

            //  Rotate the geometry a bit.
            rotatePyramid += 3.0f;
            rquad -= 3.0f;
        }

        float rotatePyramid = 0;
        float rquad = 0;

        private void OpenGLControl_OnOpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            //  Enable the OpenGL depth testing functionality.
            args.OpenGL.Enable(OpenGL.GL_DEPTH_TEST);
        }
    }
}
