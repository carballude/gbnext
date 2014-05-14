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
        /// Swap upper & lower nibles of n.
        /// 
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Reset.
        /// </summary>
        private void SWAP_r(int register)
        {
            byte lowerByte = (byte) (((int)registers[A] & 0xF0) >> 4);
            byte upperByte = (byte) (((int)registers[A] & 0x0F) << 4);

            registers[A] = (byte) ((int)upperByte | (int) lowerByte);

            FlagZ = registers[A] == 0;
            FlagN = false;
            FlagH = false;
            FlagC = false;

            ConsumeCycle(8);
        }

        /// <summary>
        /// Swap upper & lower nibles of n.
        /// 
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Reset.
        /// </summary>
        private void SWAP_rm(UInt16 registerMemory)
        {
            var data = memoryController.GetPosition(registerMemory);

            byte lowerByte = (byte)(((int)data & 0xF0) >> 4);
            byte upperByte = (byte)(((int)data & 0x0F) << 4);

            registers[A] = (byte)((int)upperByte | (int)lowerByte);

            FlagZ = registers[A] == 0;
            FlagN = false;
            FlagH = false;
            FlagC = false;

            ConsumeCycle(16);
        }

        /// <summary>
        /// Decimal adjust register A.
        /// This instruction adjusts register A so that the
        /// correct representation of Binary Coded Decimal (BCD)
        /// is obtained.
        /// 
        /// Flags affected:
        /// Z - Set if register A is zero.
        /// N - Not affected.
        /// H - Reset.
        /// C - Set or reset according to operation.
        /// </summary>
        private void DAA()
        {
            //TODO DAA
            NotImplemented(0x27);
        }

        /// <summary>
        /// Description:
        /// Complement A register. (Flip all bits.)
        /// 
        /// Flags affected:
        /// Z - Not affected.
        /// N - Set.
        /// H - Set.
        /// C - Not affected.
        /// </summary>
        private void CPL()
        {
            registers[A] = (byte) ((int)registers[A] ^ 0xFF);

            FlagN = true;
            FlagH = true;

            ConsumeCycle(4);
        }
        
        
        /// <summary>
        /// Complement carry flag 
        /// 
        ///If C flag is set, then reset it
        ///If C flag is reset, then set it
        ///
        /// Flags affected:
        /// Z - Not affected.
        /// N - Reset.
        /// H - Reset.
        /// C - Complemented.
        /// </summary>
        private void CCF()
        {
            FlagC = !FlagC;
            FlagN = false;
            FlagH = false;

            ConsumeCycle(4);
        }

        /// <summary>
        /// Set Carry flag 
        /// 
        /// Flags affected:
        /// Z - Not affected.
        /// N - Reset.
        /// H - Reset.
        /// C - Set.
        /// </summary>
        private void SCF()
        {
            FlagC = true;
            FlagN = false;
            FlagH = false;

            ConsumeCycle(4);
        }


        /// <summary>
        /// No operation
        /// </summary>
        private void NOP()
        {
            ConsumeCycle(4);
        }

        /// <summary>
        /// Power down CPU until an interrupt occurs. Use this
        /// when ever possible to reduce energy consumption.
        /// </summary>
        private void HALT()
        {
            //TODO HALT
            NotImplemented(0x76);
            ConsumeCycle(4);
        }

        /// <summary>
        /// Halt CPU and LCD display until button pressed
        /// </summary>
        private void STOP()
        {
            //TODO STOP
            NotImplemented(0x1000);

            ConsumeCycle(4);
        }

        /// <summary>
        /// This instruction disables interrupts but not
        /// immediately. Interrupts are disabled after
        /// instruction after DI is executed.
        /// </summary>
        private void DI()
        {
            FlagInterrupt = false;

            ConsumeCycle(4);
        }


        /// <summary>
        /// Enable interrupts. This intruction enables interrupts
        /// but not immediately. Interrupts are enabled after
        /// instruction after EI is executed.
        /// </summary>
        private void EI()
        {
            FlagInterrupt = true;

            ConsumeCycle(4);
        }
    }
}
