using System;
using System.Collections.Generic;
using System.Text;

namespace PrimordialEngine.Interfaces
{
    interface IPrimordialRenderer:IDisposable
    {
        public void Initialize(int height, int width, PrimordialObject primordialObject);
        public void Start();
    }
}
