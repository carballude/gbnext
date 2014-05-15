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
        /// Add n to HL.
		/// Use with: 
		/// n = BC,DE,HL,SP
		/// Flags affected:
		/// Z - Not affected.
		/// N - Reset.
		/// H - Set if carry from bit 11.
		/// C - Set if carry from bit 15.
        /// </summary>
        /// <param name="register">The register.</param>
        private void ADD_HL_rr(ushort register)
        {
            var operation = HL + register;
            byte hi = (byte)(operation >> 8);
            byte lo = (byte)operation;
            registers[H] = hi;
            registers[L] = lo;
            FlagN = false;
            FlagC = (operation & 0x8000) == 0x8000;
            FlagH = (operation & 0x800) == 0x800;
            ConsumeCycle(8);
        }

        /// <summary>
        /// Add n to Stack Pointer (SP).
		/// Use with: 
		/// n = one byte signed immediate value (#).
		/// Flags affected:
		/// Z - Reset.
		/// N - Reset.
		/// H - Set or reset according to operation.
		/// C - Set or reset according to operation
        /// </summary>
        private void ADD_SP()
        {
            UInt16 operation = (UInt16)(SP + (short)memoryController.GetPosition(PC++));
            SP = operation;
            FlagZ = FlagN = false;
            FlagC = operation > 0xFF;
            FlagH = operation > 0x0F;
            ConsumeCycle(16);
        }

        /// <summary>
        /// Increment register nn. 
		/// Use with: 
		/// nn = BC,DE,HL 
        /// </summary>
        /// <param name="R">The r.</param>
        /// <param name="R2">The r2.</param>
        private void INC_rr(int R, int R2)
        {
            if (registers[R2] == 0xFF)
            {
                registers[R2] = 0x00;
                ++registers[R];
            }
            else
                ++registers[R2];
            ConsumeCycle(8);
        }

        /// <summary>
        /// Increment register nn. 
        /// Use with: 
        /// nn = SP
        /// </summary>
        private void INC_SP()
        {
            ++_SP;
        }


        /// <summary>
        /// Des the C_RR.
        /// </summary>
        /// <param name="R">The r.</param>
        /// <param name="R2">The r2.</param>
		public void DEC_rr(int R, int R2)
        {
			//TODO DEC nn
        }

        /// <summary>
        /// Decrement register nn. 
        /// Use with: 
        /// nn = SP
        /// </summary>
        public void DEC_SP()
        {
			--_SP;
        }
       
    }
}
