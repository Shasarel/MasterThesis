using System;
using GlmNet;
using SharpGL;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;

namespace PrimordialEngine
{
    /// <summary>
    /// Represents the Scene for renderingForm sample.
    /// </summary>
    /// <remarks>
    /// renderingForm code is based on work from the OpenGL 4.x Swiftless tutorials, please see:
    /// http://www.swiftless.com/opengl4tuts.html
    /// </remarks>
    public class Scene
    {
        //  The projection, view and model matrices.
        mat4 projectionMatrix;
        mat4 viewMatrix;
        mat4 modelMatrix;
        const uint attributeIndexPosition = 0;
        const uint attributeIndexColour = 1;

        VertexBufferArray vertexBufferArray;
    
        private ShaderProgram shaderProgram;

        /// <summary>
        /// Initialises the scene.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="width">The width of the screen.</param>
        /// <param name="height">The height of the screen.</param>
        public void Initialise(OpenGL gl, float width, float height)
        {
            const float rads = (60.0f / 360.0f) * (float)Math.PI * 2.0f;
            projectionMatrix = glm.perspective(rads, 1, 0.1f, 100.0f);

            //  Create a view matrix to move us back a bit.
            viewMatrix = glm.translate(new mat4(1.0f), new vec3(0.0f, 0.0f, -5.0f));

            //  Create a model matrix to make the model a little bigger.
            modelMatrix = glm.scale(new mat4(1.0f), new vec3(2.5f));

            //  Now create the geometry for the square.
        }

        /// <summary>
        /// Draws the scene.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        public void Draw(OpenGL gl, ShaderProgram shaderProgram, VertexBufferArray vertexBufferArray, mat4 modelMatrix, mat4 viewMatrix, mat4 projectionMatrix)
        {
            //  Clear the scene.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);

            //  Bind the shader, set the matrices.
            shaderProgram.Bind(gl);
            shaderProgram.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());

            //  Bind the out vertex array.
            vertexBufferArray.Bind(gl);

            //  Draw the square.
            gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, 6); 

            //  Unbind our vertex array and shader.
            vertexBufferArray.Unbind(gl);
            shaderProgram.Unbind(gl);
        }
        
        /// <summary>
        /// Creates the geometry for the square, also creating the vertex buffer array.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>

    }
}