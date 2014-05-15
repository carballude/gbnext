using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBNext.Hardware.CPU
{
    public partial class CPU
    {
        private void LD_rr_nn(UInt16 registry)
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            registry = (ushort)((hi << 8) | lo);
            ConsumeCycle(12);
        }

        private void LD_rr_rr(UInt16 to, UInt16 from)
        {
            to = from;
            ConsumeCycle(8);
        }

        private void LDHL_SP_n()
        {
            var n = (sbyte)memoryController.GetPosition(PC++);
            FlagH = ((SP & 0x0F) + (n & 0x0F)) > 0x0F;
            FlagC = ((SP & 0xFF) + (n & 0xFF)) > 0xFF;
            FlagZ = FlagN = false;
            ConsumeCycle(12);
        }

        private void LD_nn_SP()
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            var address = (ushort)((hi << 8) | lo);
            memoryController.Write(address, (byte)SP);
            ConsumeCycle(20);
        }
    }
}
