using System;
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
    public class OpenGLRenderer:IPrimordialRenderer
    {

        private readonly Scene _scene = new Scene();
        private OpenGLRenderingForm _openGLRenderingForm;
        mat4 _projectionMatrix;
        mat4 _viewMatrix;
        mat4 _modelMatrix;
        const uint attributeIndexPosition = 0;
        const uint attributeIndexColour = 1;

        private float lastTime = 0;

        VertexBufferArray vertexBufferArray;

        private ShaderProgram shaderProgram;

        private PrimordialObject _primordialObject;

        private Stopwatch _stopwatch;

        private void InitializeOpenGL(OpenGL gl, int height, int width)
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

            const float rads = (90.0f / 360.0f) * (float)Math.PI * 2.0f;
            _projectionMatrix = glm.perspective(rads, width / (float)height, 0.1f, 100.0f);
            _viewMatrix = glm.translate(new mat4(1.0f), new vec3(0.0f, 0.0f, -4.0f));

            _modelMatrix = glm.scale(new mat4(1.0f), new vec3(1.0f));

            //  Now create the geometry for the square.
            CreateVerticesForSquare(gl);
            _scene.Initialise(gl,width,height);
        }

        private void Draw(OpenGL gl)
        {
            
            var time = _stopwatch.ElapsedMilliseconds / 1000.0f;
            System.Console.WriteLine((1/(time - lastTime)));
            lastTime = time;
            
            //  Clear the scene.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);

            var viewMatrix = _viewMatrix * glm.rotate(time, new vec3(0.0f, 0.0f, 1.0f)) * glm.rotate(time, new vec3(0.0f,1.0f, 0.0f)) * glm.rotate(time, new vec3(1.0f, 0.0f, 0.0f));;

            //  Bind the shader, set the matrices.
            shaderProgram.Bind(gl);
            shaderProgram.SetUniformMatrix4(gl, "projectionMatrix", _projectionMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "modelMatrix", _modelMatrix.to_array());

            //  Bind the out vertex array.
            vertexBufferArray.Bind(gl);

            //  Draw the square.
            gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, _primordialObject.VertexDataFloat.Length/2);

            //  Unbind our vertex array and shader.
            vertexBufferArray.Unbind(gl);
            shaderProgram.Unbind(gl);
            
        }

        public void Initialize(int height, int width, PrimordialObject primordialObject)
        {
            _primordialObject = primordialObject.ToVertexDataFloat();
            _stopwatch = Stopwatch.StartNew();
            _openGLRenderingForm = new OpenGLRenderingForm(InitializeOpenGL, Draw, height, width);
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
        private void CreateVerticesForSquare(OpenGL gl)
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
            vertexBufferArray = new VertexBufferArray();
            vertexBufferArray.Create(gl);
            vertexBufferArray.Bind(gl);

            var vertexDataBuffer = new VertexBuffer();
            vertexDataBuffer.Create(gl);
            vertexDataBuffer.Bind(gl);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, _primordialObject.VertexDataFloat, OpenGL.GL_STATIC_DRAW);
            gl.VertexAttribPointer(0, 4, OpenGL.GL_FLOAT, false, typeof(VertexData).GetFields().Length * 4 * sizeof(float), IntPtr.Zero);
            gl.EnableVertexAttribArray(0);


            gl.VertexAttribPointer(1, 4, OpenGL.GL_FLOAT, false, typeof(VertexData).GetFields().Length * 4 * sizeof(float), IntPtr.Add(IntPtr.Zero, 4 * sizeof(float)));
            gl.EnableVertexAttribArray(1);
            //colourDataBuffer.SetData(gl, 1, colors, false, 3);

            //  Unbind the vertex array, we've finished specifying data for it.
            vertexBufferArray.Unbind(gl);
        }
    }
}
