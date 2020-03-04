using SharpDX;
using System.Linq;
using System.Runtime.InteropServices;

namespace PrimordialEngine
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct VertexDataStruct
    {
        public readonly Vector4 Position;
        public readonly Color4 Color;


        public VertexDataStruct(Vector4 position, Color4 color)
        {
            Position = position;
            Color = color;
        }

        public float[] ToArray()
        {
            return Position.ToArray().Concat(Color.ToArray()).ToArray();
        }
    }
}
