using SharpDX;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace PrimordialEngine
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct VertexDataStruct
    {
        public Vector4 Position;
        public Color4 Color;
       public Vector4 Normal;


        public VertexDataStruct(Vector4 position, Color4 color, Vector4 normal)
        {
            Position = position;
            Color = color;
            Normal =  normal;

        }

        public float[] ToArray()
        {
            return Position.ToArray().Concat(Color.ToArray()).ToArray();
        }
    }
}
