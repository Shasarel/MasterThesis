using System.Collections.Generic;
using SharpDX;

namespace PrimordialEngine
{
    public class PrimordialObject
    {
        public VertexData[] VertexData { get; set; }

        public float[] VertexDataFloat { get; set; }

        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }

        public PrimordialObject ToVertexDataFloat()
        {
            var vertexDataFloat = new List<float>();

            foreach (var vertexData in VertexData)
            {
                vertexDataFloat.Add(vertexData.Position.X);
                vertexDataFloat.Add(vertexData.Position.Y);
                vertexDataFloat.Add(vertexData.Position.Z);
                vertexDataFloat.Add(vertexData.Position.W);

                vertexDataFloat.Add(vertexData.Color.Red);
                vertexDataFloat.Add(vertexData.Color.Green);
                vertexDataFloat.Add(vertexData.Color.Blue);
                vertexDataFloat.Add(vertexData.Color.Alpha);
            }

            return new PrimordialObject
            {
                Position = Position,
                Rotation = Rotation,
                Scale = Scale,
                VertexDataFloat = vertexDataFloat.ToArray()
            };
        }
    }
}
