using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBNext.Hardware.CPU
{
    public partial class CPU
    {
        /// <summary>
        /// Push present address onto stack.
        /// Jump to address $0000 + n.
        /// 
        /// Use with: 
        /// n = $00,$08,$10,$18,$20,$28,$30,$38
        /// </summary>
        /// <param name="resetAddress">The reset address.</param>
        private void RST_n(ushort resetAddress)
        {
            byte[] bitsPC = BitConverter.GetBytes(PC);
            memoryController.Write(SP, bitsPC[0]);
            memoryController.Write(SP, bitsPC[1]);

            PC = resetAddress;

            ConsumeCycle(32);
        }

    }
}
