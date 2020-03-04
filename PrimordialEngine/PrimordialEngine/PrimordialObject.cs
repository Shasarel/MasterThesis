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
            VertexData = new float[0];
            vertexDataStructList.ForEach(x => VertexData = VertexData.Concat(x.ToArray()).ToArray()); ;
        }
    }
}
