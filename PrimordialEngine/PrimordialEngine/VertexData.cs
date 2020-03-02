using SharpDX;
using System.Runtime.InteropServices;

namespace PrimordialEngine
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct VertexData
    {
        public readonly Vector4 Position;
        public readonly Color4 Color;


        public VertexData(Vector4 position, Color4 color)
        {
            Position = position;
            Color = color;
        }

        public float[] ToFloat()
        {
            var vertexDataFloat = new float[8];

            vertexDataFloat[0] = Position.X;
            vertexDataFloat[1] = Position.Y;
            vertexDataFloat[2] = Position.Z;
            vertexDataFloat[3] = Position.W;
            vertexDataFloat[4] = Color.Red;
            vertexDataFloat[5] = Color.Green;
            vertexDataFloat[6] = Color.Blue;
            vertexDataFloat[7] = Color.Alpha;

            return vertexDataFloat;
        }
    }
}
