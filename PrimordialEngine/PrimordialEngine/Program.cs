using SharpDX;
using System;


namespace PrimordialEngine
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var primordialObject = new PrimordialObject
            {
                VertexData = new VertexData[]
                {
                    new VertexData(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Color.Red),
                    new VertexData(new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), Color.Red),
                    new VertexData(new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), Color.Red),
                    new VertexData(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Color.Red),
                    new VertexData(new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), Color.Red),
                    new VertexData(new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), Color.Red),

                    new VertexData(new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), Color.Green),
                    new VertexData(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), Color.Green),
                    new VertexData(new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), Color.Green),
                    new VertexData(new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), Color.Green),
                    new VertexData(new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), Color.Green),
                    new VertexData(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), Color.Green),

                    new VertexData(new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), Color.Blue),
                    new VertexData(new Vector4(-1.0f, 1.0f,  1.0f,  1.0f), Color.Blue),
                    new VertexData(new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), Color.Blue),
                    new VertexData(new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), Color.Blue),
                    new VertexData(new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), Color.Blue),
                    new VertexData(new Vector4( 1.0f, 1.0f, -1.0f,  1.0f), Color.Blue),

                    new VertexData(new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), Color.Purple),
                    new VertexData(new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), Color.Purple),
                    new VertexData(new Vector4(-1.0f,-1.0f,  1.0f,  1.0f), Color.Purple),
                    new VertexData(new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), Color.Purple),
                    new VertexData(new Vector4( 1.0f,-1.0f, -1.0f,  1.0f), Color.Purple),
                    new VertexData(new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), Color.Purple),

                    new VertexData(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Color.Yellow),
                    new VertexData(new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), Color.Yellow),
                    new VertexData(new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), Color.Yellow),
                    new VertexData(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Color.Yellow),
                    new VertexData(new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), Color.Yellow),
                    new VertexData(new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), Color.Yellow),

                    new VertexData(new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), Color.Lime),
                    new VertexData(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), Color.Lime),
                    new VertexData(new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), Color.Lime),
                    new VertexData(new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), Color.Lime),
                    new VertexData(new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), Color.Lime),
                    new VertexData(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), Color.Lime)
                },
                Position = new Vector3(0.0f, 0.0f, 0.0f)
            };
            using var renderer = new OpenGLRenderer.OpenGLRenderer();
            using var renderer2 = new DirectXRenderer.DirectXRenderer();
            renderer.Initialize(800,800, primordialObject);
            renderer.Start();
            renderer2.Initialize(800,800, primordialObject);
            renderer2.Start();
        }
    }
}
