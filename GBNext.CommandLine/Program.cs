using GBNext.Hardware.Cartridge;
using GBNext.Hardware.CPU;
using GBNext.Hardware.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBNext.CommandLine
{
    class Program
    {

        public Program()
        {
            Menu();
        }

        private void Menu()
        {
            Console.Write("> ");
            var response = Console.ReadLine().ToLowerInvariant();
            switch (response.Split(new char[]{' '}).First())
            {
                case "exit":
                    Environment.Exit(0);
                    break;
                case "load":
                    Load(response);
                    break;
                case "next":
                    Next();
                    break;
                case "shownext":
                    ShowNextInstruction();
                    break;
                case "status":
                    ShowCpuStatus();
                    break;
            }
            Menu();
        }

        private void ShowNextInstruction()
        {
            Cpu.ShowNextInstruction();
        }

        private void ShowCpuStatus()
        {
            Console.WriteLine(Cpu);
        }

        private void Next()
        {
            Cpu.ExecuteNextInstruction();
        }

        private Cartridge Cartridge { get; set; }
        private CPU Cpu { get; set; }

        private void Load(string response)
        {
            var chunks = response.Split(new char[] { ' ' });
            if (chunks.Length != 2) throw new ArgumentException("Load only takes the path to the file");
            if (!File.Exists(chunks.Last())) throw new FileNotFoundException("File " + chunks.Last() + " doesn't seem to exist!");
            Cartridge = new Hardware.Cartridge.Cartridge(chunks.Last());
            Console.WriteLine("Cartridge has configuration: " + Cartridge.Configuration);
            Console.WriteLine("Rom size: " + Cartridge.RomSize + "kb");
            Console.WriteLine("Cartridge has been loaded :)");
            Cpu = new CPU(CreateMemory());
            Console.WriteLine("CPU has been created with appropriate memory model :)");
        }

        private IMemoryController CreateMemory()
        {
            if (Cartridge.Configuration == Hardware.Cartridge.Cartridge.CartridgeTypes.RomOnly)
                return new RomOnly(Cartridge);
            throw new ArgumentException("Not supported cartridge configuration");
        }

       

        static void Main(string[] args)
        {
            new Program();
        }
    }
}
