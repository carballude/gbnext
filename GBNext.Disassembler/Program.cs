using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBNext.Disassembler
{
    class Program
    {
        //Delete this comment
        private string cartridgePath;

        private byte[] cartridge;

        public Program()
        {
            string[] command = null;
            do
            {
                var answer = WritePrompt();
                command = answer.ToLowerInvariant().Split(new char[] { ' ' });
                switch (command.First())
                {
                    case "load":
                        LoadCartridge(command);
                        break;
                    case "info":
                        PrintCartridgeInfo();
                        break;
                    case "entry":
                        PrintEntryPoint();
                        break;
                    case "show":
                        ShowCode(command);
                        break;
                    case "status":
                        PrintDevelopementStatus();
                        break;
                }
            } while (command.First() != "exit");
        }

        private void PrintDevelopementStatus()
        {
            var counter = 0;
            for (int i = 0; i < 256; i++)
                if(Mnemonics.Mnemonic((byte)i).CompareTo("UNKNOWN")==0)
                    counter++;
            Console.WriteLine("So far {0} out of 256 instructions have been implemented. Only {1} to go!", 256 - counter, counter);
        }

        private void ShowCode(string[] command)
        {
            if (string.IsNullOrWhiteSpace(cartridgePath)) { Console.Error.WriteLine("You must load a cartridge first!"); return; }
            if (command.Length < 3) { Console.Error.WriteLine("You must specify an address range."); return; }            
            var sb = new StringBuilder();
            for (int i = Int32.Parse(command[1], System.Globalization.NumberStyles.HexNumber); i <= Int32.Parse(command[2], System.Globalization.NumberStyles.HexNumber); i++)
            {
                sb.Append(string.Format("{0:X2} 0x{1:X2} {2}\n", i, cartridge[i], Mnemonics.Mnemonic(cartridge[i])));
            }
            Console.WriteLine(sb.ToString());
        }

        private void PrintEntryPoint()
        {
            if (string.IsNullOrWhiteSpace(cartridgePath)) { Console.Error.WriteLine("You must load a cartridge first!"); return; }
            var sb = new StringBuilder();
            for (int i = 0x100; i < 0x103; i++)
            {
                sb.Append(string.Format("{0:X2} 0x{1:X2} {2}\n", i, cartridge[i], Mnemonics.Mnemonic(cartridge[i])));
            }
            Console.WriteLine(sb.ToString());
        }        

        private void PrintCartridgeInfo()
        {
            if (string.IsNullOrWhiteSpace(cartridgePath)) { Console.Error.WriteLine("You must load a cartridge first!"); return; }
            Console.WriteLine("Binary title:");
            Console.WriteLine(ReadTitleBinary());
            Console.WriteLine("ASCII title: {0}", ReadTitleASCII());
            Console.WriteLine("Cartridge type: {0}", ReadCartridgeType());
            Console.WriteLine("ROM Size: {0}", ReadROMSize());
            Console.WriteLine("RAM Size: {0}", ReadRAMSize());
        }

        private string ReadROMSize()
        {
            var ret = "UNKWNOWN?";
            switch (cartridge[0x148])
            {
                case 0x00: ret = "32KByte (no ROM banking)"; break;
                case 0x01: ret = "64KByte (4 banks)"; break;
                case 0x02: ret = "128KByte (8 banks)"; break;
                case 0x03: ret = "256KByte (16 banks)"; break;
                case 0x04: ret = "512KByte (32 banks)"; break;
                case 0x05: ret = "1MByte (64 banks)  - only 63 banks used by MBC1"; break;
                case 0x06: ret = "2MByte (128 banks) - only 125 banks used by MBC1"; break;
                case 0x07: ret = "4MByte (256 banks)"; break;
                case 0x52: ret = "1.1MByte (72 banks)"; break;
                case 0x53: ret = "1.2MByte (80 banks)"; break;
                case 0x54: ret = "1.5MByte (96 banks)"; break;
            }
            return ret;
        }

        private string ReadRAMSize()
        {
            var ret = "UNKNOWN?";
            switch (cartridge[0x149])
            {
                case 0x00: ret = "None"; break;
                case 0x01: ret = "2 KBytes"; break;
                case 0x02: ret = "8 Kbytes"; break;
                case 0x03: ret = "32 KBytes (4 banks of 8KBytes each)"; break;
            }
            return ret;
        }

        private string ReadCartridgeType()
        {
            var ret = "UNKNOWN?";
            switch (cartridge[0x147])
            {
                case 0x00: ret = "ROM ONLY"; break;
                case 0x13: ret = "MBC3+RAM+BATTERY"; break;
                case 0x01: ret = "MBC1"; break;
                case 0x15: ret = "MBC4"; break;
                case 0x02: ret = "MBC1+RAM"; break;
                case 0x16: ret = "MBC4+RAM"; break;
                case 0x03: ret = "MBC1+RAM+BATTERY"; break;
                case 0x17: ret = "MBC4+RAM+BATTERY"; break;
                case 0x05: ret = "MBC2"; break;
                case 0x19: ret = "MBC5"; break;
                case 0x06: ret = "MBC2+BATTERY"; break;
                case 0x1A: ret = "MBC5+RAM"; break;
                case 0x08: ret = "ROM+RAM"; break;
                case 0x1B: ret = "MBC5+RAM+BATTERY"; break;
                case 0x09: ret = "ROM+RAM+BATTERY"; break;
                case 0x1C: ret = "MBC5+RUMBLE"; break;
                case 0x0B: ret = "MMM01"; break;
                case 0x1D: ret = "MBC5+RUMBLE+RAM"; break;
                case 0x0C: ret = "MMM01+RAM"; break;
                case 0x1E: ret = "MBC5+RUMBLE+RAM+BATTERY"; break;
                case 0x0D: ret = "MMM01+RAM+BATTERY"; break;
                case 0xFC: ret = "POCKET CAMERA"; break;
                case 0x0F: ret = "MBC3+TIMER+BATTERY"; break;
                case 0xFD: ret = "BANDAI TAMA5"; break;
                case 0x10: ret = "MBC3+TIMER+RAM+BATTERY"; break;
                case 0xFE: ret = "HuC3"; break;
                case 0x11: ret = "MBC3"; break;
                case 0xFF: ret = "HuC1+RAM+BATTERY"; break;
                case 0x12: ret = "MBC3+RAM"; break;
            }
            return ret;
        }

        private string ReadTitleASCII()
        {
            var sb = new StringBuilder();
            for (int i = 0x134; i < 0x143; i++) { sb.Append((char)cartridge[i]); }
            return sb.ToString();
        }

        private string ReadTitleBinary()
        {
            var sb = new StringBuilder();
            for (int i = 0x134; i < 0x143; i++) { sb.Append(string.Format("0x{0:X2} ", cartridge[i])); }
            return sb.ToString();
        }

        private void LoadCartridge(string[] command)
        {
            if (command.Length < 2) { Console.Error.WriteLine("You need to specify the path to the ROM!"); return; }
            if (!File.Exists(command[1])) { Console.Error.WriteLine("Path {0} doesn't seem to be valid!", command[1]); return; }
            cartridgePath = command[1];
            cartridge = File.ReadAllBytes(cartridgePath);
            Console.WriteLine("Cartridge loaded!");
        }

        private string WritePrompt()
        {
            Console.Write("> ");
            return Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("GBNext Disassembler v0.1");
            Console.WriteLine("Brought to you by: ");
            Console.WriteLine("\tPablo Carballude");
            Console.WriteLine("\tCarlos Carrillo");
            Console.WriteLine("\tJose Angel Fernandez");
            new Program();
        }
    }
}
