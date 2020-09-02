using System;
using System.Collections.Generic;
using System.Text;

namespace PrimordialEngine.Interfaces
{
    interface IPrimordialRenderer:IDisposable
    {
        public void Initialize(int height, int width, List<PrimordialObject> primordialObject, string fileTitle);
        public void Start();
    }
}
