using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using GlmNet;
using PrimordialEngine.Interfaces;
using SharpDX;
using SharpGL;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;

namespace PrimordialEngine.OpenGLRenderer
{
    public class OpenGLRenderer : IPrimordialRenderer
    {
        public struct poStruct{
            public PrimordialObject _primordialObject;
            public VertexBufferArray _vertexBufferArray;
        }

        private OpenGLRenderingForm _openGLRenderingForm;
        Camera camera;
        const uint attributeIndexPosition = 0;
        const uint attributeIndexColour = 1;

        private float lastTime = 0;

        List<VertexBufferArray> _vertexBufferArray;

        private ShaderProgram shaderProgram;

        private List<PrimordialObject> _primordialObject;

        private List<poStruct> _poStructs;

        private Stopwatch _stopwatch;

        private float mouseX, mouseY = 0;

        private Keys key =Keys.Clear;

        private void InitializeOpenGL(OpenGL gl, int width, int height)
        {
            //  Set a blue clear colour.
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);

            //  Create the shader program.
            var vertexShaderSource = ManifestResourceLoader.LoadTextFile("OpenGLRenderer\\Shaders\\VertexShader.glsl");
            var fragmentShaderSource = ManifestResourceLoader.LoadTextFile("OpenGLRenderer\\Shaders\\FragmentShader.glsl");
            shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
            shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
            shaderProgram.AssertValid(gl);
            //  Now create the geometry for the square.
            _poStructs = new List<poStruct>();
            foreach (var po in _primordialObject)
            {
                _poStructs.Add(new poStruct
                {
                    _vertexBufferArray = CreateVerticesForSquare(gl, po),
                    _primordialObject = po
                });
            }

        }

        private void KeyDownMethod(object sender, KeyEventArgs args)
        {
            key = args.KeyCode;
            if(args.KeyCode == Keys.Escape)
            {
                _openGLRenderingForm.Dispose();
            }
        }

        private void KeyUpEvent(object sender, KeyEventArgs args)
        {
            key = Keys.Clear;
        }

        private void MouseMoveEvent(object sender, MouseEventArgs args)
        {
            var point = new System.Drawing.Point(_openGLRenderingForm.Location.X +(_openGLRenderingForm.Size.Width/2), _openGLRenderingForm.Location.Y +(_openGLRenderingForm.Size.Height / 2));
            mouseX -= args.X - point.X + _openGLRenderingForm.Location.X;
            mouseY -= args.Y - point.Y + _openGLRenderingForm.Location.Y;
            camera.Pitch = mouseY * 0.001f;
            camera.Yaw = mouseX * 0.001f;
        }

        private void Draw(OpenGL gl)
        {
            var point = new System.Drawing.Point(_openGLRenderingForm.Location.X + (_openGLRenderingForm.Size.Width / 2), _openGLRenderingForm.Location.Y + (_openGLRenderingForm.Size.Height / 2));
            Cursor.Position = point;
            var time = _stopwatch.ElapsedMilliseconds / 1000.0f;
            //System.Console.WriteLine((1/(time - lastTime)));
            var dt = time - lastTime;

            if (key == Keys.W)
            {
                camera.goForward(dt);
            }

            lastTime = time;
            
            //  Clear the scene.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);

            //  Bind the shader, set the matrices.
            shaderProgram.Bind(gl);
            foreach (var structt in _poStructs)
            {
                var worldViewProj = Matrix.RotationX(time) * Matrix.RotationY(time * .1f) * Matrix.RotationZ(time * .1f) * Matrix.Translation(structt._primordialObject.Position) * camera.ViewProjectionMatrix;
                shaderProgram.SetUniformMatrix4(gl, "MVP_Matrix", worldViewProj.ToArray());

                //  Bind the out vertex array.
                structt._vertexBufferArray.Bind(gl);

                //  Draw the square.
                gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, structt._primordialObject.VertexData.Length / 2);

                //  Unbind our vertex array and shader.
                structt._vertexBufferArray.Unbind(gl);
            }

            shaderProgram.Unbind(gl);
            
        }

        public void Initialize(int width, int height, List<PrimordialObject> primordialObject)
        {
            _primordialObject = primordialObject;
            _stopwatch = Stopwatch.StartNew();
            _openGLRenderingForm = new OpenGLRenderingForm(InitializeOpenGL, Draw, width, height);
            camera = new Camera(60, width / (float)height);
            _openGLRenderingForm._openGLControl.KeyDown += KeyDownMethod;
            _openGLRenderingForm._openGLControl.MouseMove += MouseMoveEvent;
            _openGLRenderingForm._openGLControl.KeyUp += KeyUpEvent;
            var point = new System.Drawing.Point(_openGLRenderingForm._openGLControl.Location.X + (_openGLRenderingForm.Size.Width / 2), _openGLRenderingForm.Location.Y + (_openGLRenderingForm.Size.Height / 2));
            Cursor.Position = point;
            UserControl userControl = _openGLRenderingForm._openGLControl;
        }

        public void Start()
        {
            if(_openGLRenderingForm != null)
                Application.Run(_openGLRenderingForm);
        }

        public void Dispose()
        {
            _openGLRenderingForm?.Dispose();
        }
        private VertexBufferArray CreateVerticesForSquare(OpenGL gl, PrimordialObject primordialObject)
        {
            var vertices = new float[18];
            var colors = new float[18]; // Colors for our vertices  
            vertices[0] = -0.5f; vertices[1] = -0.5f; vertices[2] = 0.0f; // Bottom left corner  
            colors[0] = 1.0f; colors[1] = 1.0f; colors[2] = 1.0f; // Bottom left corner  
            vertices[3] = -0.5f; vertices[4] = 0.5f; vertices[5] = 0.0f; // Top left corner  
            colors[3] = 1.0f; colors[4] = 0.0f; colors[5] = 0.0f; // Top left corner  
            vertices[6] = 0.5f; vertices[7] = 0.5f; vertices[8] = 0.0f; // Top Right corner  
            colors[6] = 0.0f; colors[7] = 1.0f; colors[8] = 0.0f; // Top Right corner  
            vertices[9] = 0.5f; vertices[10] = -0.5f; vertices[11] = 0.0f; // Bottom right corner  
            colors[9] = 0.0f; colors[10] = 0.0f; colors[11] = 1.0f; // Bottom right corner  
            vertices[12] = -0.5f; vertices[13] = -0.5f; vertices[14] = 0.0f; // Bottom left corner  
            colors[12] = 1.0f; colors[13] = 1.0f; colors[14] = 1.0f; // Bottom left corner  
            vertices[15] = 0.5f; vertices[16] = 0.5f; vertices[17] = 0.0f; // Top Right corner  
            colors[15] = 0.0f; colors[16] = 1.0f; colors[17] = 0.0f; // Top Right corner  

            
            //  Create the vertex array object.
            var vertexBufferArray = new VertexBufferArray();
            vertexBufferArray.Create(gl);
            vertexBufferArray.Bind(gl);

            var vertexDataBuffer = new VertexBuffer();
            vertexDataBuffer.Create(gl);
            vertexDataBuffer.Bind(gl);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, primordialObject.VertexData, OpenGL.GL_STATIC_DRAW);
            gl.VertexAttribPointer(0, 4, OpenGL.GL_FLOAT, false, typeof(VertexDataStruct).GetFields().Length * 4 * sizeof(float), IntPtr.Zero);
            gl.EnableVertexAttribArray(0);


            gl.VertexAttribPointer(1, 4, OpenGL.GL_FLOAT, false, typeof(VertexDataStruct).GetFields().Length * 4 * sizeof(float), IntPtr.Add(IntPtr.Zero, 4 * sizeof(float)));
            gl.EnableVertexAttribArray(1);
            //colourDataBuffer.SetData(gl, 1, colors, false, 3);

            //  Unbind the vertex array, we've finished specifying data for it.
            vertexBufferArray.Unbind(gl);
            return vertexBufferArray;
        }
    }
}
