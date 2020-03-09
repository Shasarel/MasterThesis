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
            var vertexDataCube = new List<VertexDataStruct> {
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
                };
            var a = new List<PrimordialObject>();

            for(int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var primordialObject = new PrimordialObject
                    {
                        Position = new Vector3(10*i, 10, 10*j),
                        //Position = new Vector3(0, 0, -10),
                        Scale = new Vector3(1.0f, 1.0f, 1.0f)
                    };
                    primordialObject.SetVertexData(vertexDataCube);
                    a.Add(primordialObject);
                }
            }
            using var renderer = new OpenGLRenderer.OpenGLRenderer();
            //using var renderer = new DirectXRenderer.DirectXRenderer();
            renderer.Initialize(800,800, a);
            renderer.Start();
        }
    }
}
