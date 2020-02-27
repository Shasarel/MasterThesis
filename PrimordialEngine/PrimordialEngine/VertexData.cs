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
    }
}
