using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBNext.Hardware.Memory
{
    public interface IMemoryController
    {
        byte GetPosition(UInt16 position);
        UInt16 GetPosition16(UInt16 position);

        void Write(UInt16 position, byte data);
    }
}
