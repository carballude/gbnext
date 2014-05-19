using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBNext.Hardware.Memory
{
    public class RomOnly : IMemoryController
    {

        private Cartridge.Cartridge cartridge;

        public RomOnly(Cartridge.Cartridge cartridge)
        {
            this.cartridge = cartridge;
        }

        public byte GetPosition(ushort position)
        {
            return cartridge.Content[position];
        }

        public ushort GetPosition16(ushort position)
        {
            throw new NotImplementedException();
        }

        public void Write(ushort position, byte data)
        {
            throw new NotImplementedException();
        }

        public void Write16(ushort position, ushort data)
        {
            throw new NotImplementedException();
        }
    }
}
