using GlmNet;
using SharpDX;
using SharpDX.D3DCompiler;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimordialEngine
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var vertexDataCube = new List<VertexDataStruct> {
                    new VertexDataStruct(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Color.Red, new Vector4(0f,0f,-1f,1f)),
                    new VertexDataStruct(new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), Color.Red, new Vector4(0f,0f,-1f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), Color.Red, new Vector4(0f,0f,-1f,1f)),
                    new VertexDataStruct(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Color.Red, new Vector4(0f,0f,-1f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), Color.Red, new Vector4(0f,0f,-1f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), Color.Red, new Vector4(0f,0f,-1f,1f)),

                    new VertexDataStruct(new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), Color.Green, new Vector4(0f,0f,1f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), Color.Green, new Vector4(0f,0f,1f,1f)),
                    new VertexDataStruct(new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), Color.Green, new Vector4(0f,0f,1f,1f)),
                    new VertexDataStruct(new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), Color.Green, new Vector4(0f,0f,1f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), Color.Green, new Vector4(0f,0f,1f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), Color.Green, new Vector4(0f,0f,1f,1f)),

                    new VertexDataStruct(new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), Color.Blue, new Vector4(0f,1f,0f,1f)),
                    new VertexDataStruct(new Vector4(-1.0f, 1.0f,  1.0f,  1.0f), Color.Blue, new Vector4(0f,1f,0f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), Color.Blue, new Vector4(0f,1f,0f,1f)),
                    new VertexDataStruct(new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), Color.Blue, new Vector4(0f,1f,0f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), Color.Blue, new Vector4(0f,1f,0f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f, 1.0f, -1.0f,  1.0f), Color.Blue, new Vector4(0f,1f,0f,1f)),

                    new VertexDataStruct(new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), Color.Purple, new Vector4(0f,-1f,0f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), Color.Purple, new Vector4(0f,-1f,0f,1f)),
                    new VertexDataStruct(new Vector4(-1.0f,-1.0f,  1.0f,  1.0f), Color.Purple, new Vector4(0f,-1f,0f,1f)),
                    new VertexDataStruct(new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), Color.Purple, new Vector4(0f,-1f,0f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f,-1.0f, -1.0f,  1.0f), Color.Purple, new Vector4(0f,-1f,0f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), Color.Purple, new Vector4(0f,-1f,0f,1f)),

                    new VertexDataStruct(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Color.Yellow, new Vector4(-1f,0f,0f,1f)),
                    new VertexDataStruct(new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), Color.Yellow, new Vector4(-1f,0f,0f,1f)),
                    new VertexDataStruct(new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), Color.Yellow, new Vector4(-1f,0f,0f,1f)),
                    new VertexDataStruct(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Color.Yellow, new Vector4(-1f,0f,0f,1f)),
                    new VertexDataStruct(new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), Color.Yellow, new Vector4(-1f,0f,0f,1f)),
                    new VertexDataStruct(new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), Color.Yellow, new Vector4(-1f,0f,0f,1f)),

                    new VertexDataStruct(new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), Color.Lime, new Vector4(1f,0f,0f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), Color.Lime, new Vector4(1f,0f,0f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), Color.Lime, new Vector4(1f,0f,0f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), Color.Lime, new Vector4(1f,0f,0f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), Color.Lime, new Vector4(1f,0f,0f,1f)),
                    new VertexDataStruct(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), Color.Lime, new Vector4(1f,0f,0f,1f))
                };

            var a = new List<PrimordialObject>();

            /* for(int i = 0; i < 10; i++)
             {
                 for (int j = 0; j < 10; j++)
                 {
                     var primordialObject = new PrimordialObject
                     {
                         Position = new Vector3(10*i, -10, 10*j),
                         //Position = new Vector3(0, 0, -10),
                         Scale = new Vector3(1.0f, 1.0f, 1.0f)
                     };
                     primordialObject.SetVertexData(vertexDataCube);
                     a.Add(primordialObject);
                 }
             }*/
            var primordialObject = new PrimordialObject
            {
                Position = new Vector3(0, 0, 10),
                //Position = new Vector3(0, 0, -10),
                Scale = new Vector3(0.1f, 0.1f, 0.1f)
            };
            var data = createCube(vertexDataCube, 5);

            //data = data.Select(x => normalize(Vector4.Zero, x, -300f)).ToList();
            //data = data.GetRange(0, 10000000);
            //Console.WriteLine(data.Count());
            primordialObject.SetVertexData(data);
            a.Add(primordialObject);
            using var renderer = new OpenGLRenderer.OpenGLRenderer();
            //using var renderer = new DirectXRenderer.DirectXRenderer();
            renderer.Initialize(800,800, a);
            renderer.Start();
        }

     /*   public static VertexDataStruct normalize(Vector4 center, VertexDataStruct point, float length)
        {
            var dx = center.X - point.Position.X;
            var dy = center.Y - point.Position.Y;
            var dz = center.Z - point.Position.Z;

            dx = dx * (length / getDistance(center, point.Position));
            dy = dy * (length / getDistance(center, point.Position));
            dz = dz * (length / getDistance(center, point.Position));

            point.Position.X = point.Position.X + dx;
            point.Position.Y = point.Position.Y + dy;
            point.Position.Z = point.Position.Z + dz;
            var sum = (point.Position.X + point.Position.Y + point.Position.Z);
            point.Color = new Color4(point.Position.X/sum, point.Position.Y/sum, point.Position.Z/sum, 1.0f);
            return new VertexDataStruct(point.Position, point.Color);
        }*/
        public static float getDistance(Vector4 point1, Vector4 point2)
        {
            return (float)  Math.Sqrt(Math.Pow((point1.X - point2.X), 2) + Math.Pow((point1.Y - point2.Y), 2) + Math.Pow((point1.Z - point2.Z), 2));
        }

        public static List<VertexDataStruct> createCube(List<VertexDataStruct> data, int count)
        {
            var final = new List<VertexDataStruct>();
            for (int i = 0; i < data.Count; i += 3)
            {
                final = final.Concat(createTriangles(data.GetRange(i, 3), count)).ToList();
            }
            return final;
        }
        public static List<VertexDataStruct> createTriangles(List<VertexDataStruct> triangles, int count)
        {

            var vertexMid1= new VertexDataStruct(new Vector4((triangles[0].Position.X + triangles[1].Position.X) / 2, (triangles[0].Position.Y + triangles[1].Position.Y) / 2, (triangles[0].Position.Z + triangles[1].Position.Z) / 2, 1.0f), Color.White, triangles[0].Normal);
            var vertexMid2= new VertexDataStruct(new Vector4((triangles[1].Position.X + triangles[2].Position.X) / 2, (triangles[1].Position.Y + triangles[2].Position.Y) / 2, (triangles[1].Position.Z + triangles[2].Position.Z) / 2, 1.0f), Color.White, triangles[0].Normal);
            var vertexMid3 = new VertexDataStruct(new Vector4((triangles[2].Position.X + triangles[0].Position.X) / 2, (triangles[2].Position.Y + triangles[0].Position.Y) / 2, (triangles[2].Position.Z + triangles[0].Position.Z) / 2, 1.0f), Color.White, triangles[0].Normal);
            var newTriangles = new List <VertexDataStruct>();
            var trianglesFinal = new List<VertexDataStruct>();

            newTriangles.Add(triangles[0]);
            newTriangles.Add(vertexMid1);
            newTriangles.Add(vertexMid3);
            
            if(count > 0)
            {
                trianglesFinal = trianglesFinal.Concat(createTriangles(newTriangles, count -1)).ToList();
            }
            else
            {
                trianglesFinal = trianglesFinal.Concat(newTriangles).ToList();
            }
            newTriangles = new List<VertexDataStruct>();


            newTriangles.Add(vertexMid1);
            newTriangles.Add(triangles[1]);
            newTriangles.Add(vertexMid2);

            if (count > 0)
            {
                trianglesFinal = trianglesFinal.Concat(createTriangles(newTriangles, count - 1)).ToList();
            }
            else
            {
                trianglesFinal = trianglesFinal.Concat(newTriangles).ToList();
            }
            newTriangles = new List<VertexDataStruct>();

            newTriangles.Add(vertexMid1);
            newTriangles.Add(vertexMid2);
            newTriangles.Add(vertexMid3);

            if (count > 0)
            {
                trianglesFinal = trianglesFinal.Concat(createTriangles(newTriangles, count - 1)).ToList();
            }
            else
            {
                trianglesFinal = trianglesFinal.Concat(newTriangles).ToList();
            }
            newTriangles = new List<VertexDataStruct>();

            newTriangles.Add(vertexMid3);
            newTriangles.Add(vertexMid2);
            newTriangles.Add(triangles[2]);

            if (count > 0)
            {
                trianglesFinal = trianglesFinal.Concat(createTriangles(newTriangles, count - 1)).ToList();
            }
            else
            {
                trianglesFinal = trianglesFinal.Concat(newTriangles).ToList();
            }

            return trianglesFinal;
        }
    }
}
