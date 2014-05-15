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
		/// Test bit b in register r.
        /// 
		/// Flags affected:
		/// Z - Set if bit b of register r is 0.
		/// N - Reset.
		/// H - Set.
		/// C - Not affected.
        /// </summary>
        /// <param name="register">The register.</param>
        private void BIT_b_r(int register)
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            int bitToCompare = (hi << 8) | lo;

            int bitMask = 1 << bitToCompare;
            FlagZ = (bitMask & registers[register]) == 0 ? true : false;
            FlagN = false;
            FlagH = true;

            ConsumeCycle(8);
        }
		
        /// <summary>
        /// Test bit b in register memory register.
        /// 
        /// Flags affected:
        /// Z - Set if bit b of register r is 0.
        /// N - Reset.
        /// H - Set.
        /// C - Not affected.
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void BIT_b_rm(UInt16 registerMemory)
        {
            UInt16 value = memoryController.GetPosition(registerMemory);

            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            int bitToCompare = (hi << 8) | lo;

            int bitMask = 1 << bitToCompare;
            FlagZ = (bitMask & value) == 0 ? true : false;
            FlagN = false;
            FlagH = true;

            ConsumeCycle(16);
        }

        /// <summary>
        /// Set bit b in register r
        /// </summary>
        /// <param name="register">The register.</param>
        private void SET_b_r(int register)
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            int bitToSet = (hi << 8) | lo;

            int bitMask = 1 << bitToSet;
            registers[register] = (byte)(bitMask | registers[register]);

            ConsumeCycle(8);
        }

        /// <summary>
        /// Set bit b in register memory
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void SET_b_rm(UInt16 registerMemory)
        {
            UInt16 value = memoryController.GetPosition(registerMemory);

            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            int bitToSet = (hi << 8) | lo;

            int bitMask = 1 << bitToSet;
            memoryController.Write(registerMemory, (byte)(bitMask | value));

            ConsumeCycle(16);
        }

        /// <summary>
        /// Reset bit b in register r
        /// </summary>
        /// <param name="register">The register.</param>
        private void RES_b_r(int register)
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            int bitToReset = (hi << 8) | lo;

            int bitMask = 0xFF;
            bitMask = 0 << bitToReset;
            registers[register] = (byte)(bitMask & registers[register]);

            ConsumeCycle(8);
        }

        /// <summary>
        /// Reset bit b in register memory
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void RES_b_rm(UInt16 registerMemory)
        {
            UInt16 value = memoryController.GetPosition(registerMemory);

            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            int bitToReset = (hi << 8) | lo;

            int bitMask = 0xFF;
            bitMask = 0 << bitToReset;
            memoryController.Write(registerMemory, (byte)(bitMask & value));

            ConsumeCycle(16);
        }

    }
}
