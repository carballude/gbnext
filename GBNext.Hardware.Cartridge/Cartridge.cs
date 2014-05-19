using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBNext.Hardware.Cartridge
{
    public class Cartridge
    {
        public byte[] Content { get; internal set; }

        public CartridgeTypes Configuration { get; internal set; }

        public Cartridge(string path)
        {
            Content = File.ReadAllBytes(path);
            Configuration = ReadCartridgeType();
            RomSize = ReadRomSize();
        }

        private int ReadRomSize()
        {
            switch (Content[0x148])
            {
                case 0x00: return 256;
                default: throw new ArgumentException("This type of cartridge is not supported yet! :(");
            }
        }

        private CartridgeTypes ReadCartridgeType()
        {
            var value = Content[0x147];
            if (!Enum.IsDefined(typeof(CartridgeTypes), (int)value)) throw new ArgumentException("This type or cartridge is not supported yet! :(");            
            return (CartridgeTypes)Content[0x147];
        }

        public enum CartridgeTypes { RomOnly = 0x00 }
        public int RomSize { get; internal set; }
      
    }
}
