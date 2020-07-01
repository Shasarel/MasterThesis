using System.Collections.Generic;
using SharpDX;
using System.Linq;

namespace PrimordialEngine
{
    public class PrimordialObject
    {
        public float[] VertexData { get; set; }

        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }

        public void SetVertexData(List<VertexDataStruct> vertexDataStructList)
        {
            VertexData = new float[vertexDataStructList.Count * 3 * 4];
            for (int i = 0; i < vertexDataStructList.Count; i++)
            {
                vertexDataStructList[i].Position.ToArray().CopyTo(VertexData, i * 12) ;
                vertexDataStructList[i].Color.ToArray().CopyTo(VertexData, (i * 12) + 4);
                vertexDataStructList[i].Normal.ToArray().CopyTo(VertexData, (i * 12) + 8);
            }
        }
    }
}
