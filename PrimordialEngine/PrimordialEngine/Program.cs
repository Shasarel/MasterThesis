using SharpDX;
using System;
using System.Collections.Generic;

namespace PrimordialEngine
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var primordialObject = new PrimordialObject
            {
                Position = new Vector3(0.0f, 0.0f, 10.0f),
                Scale = new Vector3(1.0f, 1.0f, 1.0f)
            };

            primordialObject.SetVertexData(new List<VertexDataStruct> {
                    new VertexDataStruct(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Color.Red),
                    new VertexDataStruct(new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), Color.Red),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), Color.Red),
                    new VertexDataStruct(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Color.Red),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), Color.Red),
                    new VertexDataStruct(new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), Color.Red),

                    new VertexDataStruct(new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), Color.Green),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), Color.Green),
                    new VertexDataStruct(new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), Color.Green),
                    new VertexDataStruct(new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), Color.Green),
                    new VertexDataStruct(new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), Color.Green),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), Color.Green),

                    new VertexDataStruct(new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), Color.Blue),
                    new VertexDataStruct(new Vector4(-1.0f, 1.0f,  1.0f,  1.0f), Color.Blue),
                    new VertexDataStruct(new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), Color.Blue),
                    new VertexDataStruct(new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), Color.Blue),
                    new VertexDataStruct(new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), Color.Blue),
                    new VertexDataStruct(new Vector4( 1.0f, 1.0f, -1.0f,  1.0f), Color.Blue),

                    new VertexDataStruct(new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), Color.Purple),
                    new VertexDataStruct(new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), Color.Purple),
                    new VertexDataStruct(new Vector4(-1.0f,-1.0f,  1.0f,  1.0f), Color.Purple),
                    new VertexDataStruct(new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), Color.Purple),
                    new VertexDataStruct(new Vector4( 1.0f,-1.0f, -1.0f,  1.0f), Color.Purple),
                    new VertexDataStruct(new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), Color.Purple),

                    new VertexDataStruct(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Color.Yellow),
                    new VertexDataStruct(new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), Color.Yellow),
                    new VertexDataStruct(new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), Color.Yellow),
                    new VertexDataStruct(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Color.Yellow),
                    new VertexDataStruct(new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), Color.Yellow),
                    new VertexDataStruct(new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), Color.Yellow),

                    new VertexDataStruct(new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), Color.Lime),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), Color.Lime),
                    new VertexDataStruct(new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), Color.Lime),
                    new VertexDataStruct(new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), Color.Lime),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), Color.Lime),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), Color.Lime)
                });
            using var renderer = new OpenGLRenderer.OpenGLRenderer();
            using var renderer2 = new DirectXRenderer.DirectXRenderer();
            renderer.Initialize(800,800, primordialObject);
            renderer.Start();
            renderer2.Initialize(800,800, primordialObject);
            renderer2.Start();
        }
    }
}
