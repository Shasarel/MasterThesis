using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        const uint attributeIndexNormal = 2;

        private float lastTime = 0;

        List<VertexBufferArray> _vertexBufferArray;

        private ShaderProgram shaderProgram;

        private List<PrimordialObject> _primordialObject;

        private List<poStruct> _poStructs;

        private Stopwatch _stopwatch;

        private float mouseX, mouseY = 0;

        long frameTime = 0;
        int timeCount = 0;
        string fileTitle = "";

        private Keys key =Keys.Clear;
        VertexBuffer vertexDataBuffer;
        VertexBufferArray vertexBufferArray;
        OpenGL gl;

        private void InitializeOpenGL(OpenGL gl, int width, int height)
        {
            //  Set a blue clear colour.
            this.gl = gl;
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            gl.PolygonMode(SharpGL.Enumerations.FaceMode.Front, SharpGL.Enumerations.PolygonMode.Lines);
            gl.Enable(OpenGL.GL_CULL_FACE);
            gl.CullFace(OpenGL.GL_BACK);

            //  Create the shader program.
            var vertexShaderSource = ManifestResourceLoader.LoadTextFile("OpenGLRenderer\\Shaders\\VertexShader - Copy.glsl");
            var fragmentShaderSource = ManifestResourceLoader.LoadTextFile("OpenGLRenderer\\Shaders\\FragmentShader - Copy.glsl");

            //for(int i = 0; i<100; i++) {
                shaderProgram = new ShaderProgram();
                //var shaderTime = _stopwatch.ElapsedMilliseconds;
                shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
                //File.AppendAllText("shader" + ".txt", (_stopwatch.ElapsedMilliseconds - shaderTime).ToString() + "\n");
            //}

            shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
            shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
            shaderProgram.BindAttributeLocation(gl, attributeIndexNormal, "in_Normal");
            shaderProgram.AssertValid(gl);
            //  Now create the geometry for the square.
            _poStructs = new List<poStruct>();
            foreach (var po in _primordialObject)
            {
                _poStructs.Add(new poStruct
                {
                    _vertexBufferArray = CreateVertexBufferArray(gl, po),
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
            //camera.Pitch = mouseY * 0.001f;
            //camera.Yaw = mouseX * 0.001f;
        }

        private void Draw(OpenGL gl)
        {
            var elapsedTime=_stopwatch.ElapsedMilliseconds - frameTime;
            if (frameTime != 0 && elapsedTime>15)
            {
                File.AppendAllText(fileTitle + ".txt", elapsedTime.ToString() + "\n");
                timeCount++;
            }
           // if(timeCount >1)
           // {
            //    _openGLRenderingForm.Dispose();
          //  }
            if(_stopwatch.ElapsedMilliseconds / 1000 > 100)
            {
                //_openGLRenderingForm.Dispose();
           }
            var point = new System.Drawing.Point(_openGLRenderingForm.Location.X + (_openGLRenderingForm.Size.Width / 2), _openGLRenderingForm.Location.Y + (_openGLRenderingForm.Size.Height / 2));
           // Cursor.Position = point;
            var time = _stopwatch.ElapsedMilliseconds / 1000.0f;
            //System.Console.WriteLine((1/(time - lastTime)));
            var dt = time - lastTime;

            if (key == Keys.W)
            {
                camera.goForward(dt);
            }
            if (key == Keys.S)
            {
                camera.goBack(dt);
            }
            if (key == Keys.A)
            {
                camera.goLeft(dt);
            }
            if (key == Keys.D)
            {
                camera.goRight(dt);
            }

            lastTime = time;

            //  Clear the scene.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);

            //  Bind the shader, set the matrices.
            shaderProgram.Bind(gl);
            foreach (var structt in _poStructs)
            {
              
                var modelMatrix = Matrix.RotationX(time *1f) * Matrix.RotationY(time *1f) * Matrix.RotationZ(time *1f) * Matrix.Translation(structt._primordialObject.Position);
                var worldViewProj = modelMatrix * camera.ViewProjectionMatrix;
                shaderProgram.SetUniformMatrix4(gl, "MVP_Matrix", worldViewProj.ToArray());
                shaderProgram.SetUniformMatrix4(gl, "model_Matrix", modelMatrix.ToArray());
                shaderProgram.SetUniform3(gl, "viewPos", camera.PositionX, camera.PositionY, camera.PositionZ);

                structt._vertexBufferArray.Bind(gl);

                gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, structt._primordialObject.VertexData.Length / 2);

                structt._vertexBufferArray.Unbind(gl);
            }

            shaderProgram.Unbind(gl);
            frameTime = _stopwatch.ElapsedMilliseconds;
        }

        public void Initialize(int width, int height, List<PrimordialObject> primordialObject, string fileTitle)
        {
            this.fileTitle = fileTitle;
            _primordialObject = primordialObject;
            _stopwatch = Stopwatch.StartNew();
            _openGLRenderingForm = new OpenGLRenderingForm(InitializeOpenGL, Draw, width, height);
            camera = new Camera(60, width / (float)height);
            _openGLRenderingForm._openGLControl.KeyDown += KeyDownMethod;
            _openGLRenderingForm._openGLControl.MouseMove += MouseMoveEvent;
            _openGLRenderingForm._openGLControl.KeyUp += KeyUpEvent;
            Cursor.Hide();
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
            vertexBufferArray.Bind(gl);
            gl.DeleteBuffers(1, new uint[]{ vertexDataBuffer.VertexBufferObject});
            vertexBufferArray.Unbind(gl);
            vertexBufferArray.Delete(gl);
            _openGLRenderingForm?.Dispose();
        }
        private VertexBufferArray CreateVertexBufferArray(OpenGL gl, PrimordialObject primordialObject)
        {
            vertexBufferArray = new VertexBufferArray();
            vertexBufferArray.Create(gl);
            vertexBufferArray.Bind(gl);

            vertexDataBuffer = new VertexBuffer();
            vertexDataBuffer.Create(gl);
            vertexDataBuffer.Bind(gl);
          //  for(int i = 0; i<100; i++) {
              //  var sendDataTime = _stopwatch.ElapsedMilliseconds;
                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, primordialObject.VertexData, OpenGL.GL_STATIC_DRAW);
              //  File.AppendAllText(fileTitle + "Data.txt", (_stopwatch.ElapsedMilliseconds - sendDataTime).ToString() + "\n");
           // }
            gl.VertexAttribPointer(attributeIndexPosition, 4, OpenGL.GL_FLOAT, false, 3 * 4 * sizeof(float), IntPtr.Zero);
            gl.EnableVertexAttribArray(0);


            gl.VertexAttribPointer(attributeIndexColour, 4, OpenGL.GL_FLOAT, false, 3 * 4 * sizeof(float), IntPtr.Add(IntPtr.Zero, 4 * sizeof(float)));
            gl.EnableVertexAttribArray(1);

            gl.VertexAttribPointer(attributeIndexNormal, 4, OpenGL.GL_FLOAT, false, 3* 4 * sizeof(float), IntPtr.Add(IntPtr.Zero, 8 * sizeof(float)));
            gl.EnableVertexAttribArray(2);
            vertexDataBuffer.Unbind(gl);
            vertexBufferArray.Unbind(gl);
            return vertexBufferArray;
        }
    }
}
