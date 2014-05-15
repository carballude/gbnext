using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBNext.Hardware.CPU
{
    public partial class CPU
    {

        #region ADD
        /// <summary>
        /// Add n to A.
        /// 
        /// Use with: 
		/// n = A,B,C,D,E,H,L
        /// 
		/// Flags affected:
		/// Z - Set if result is zero.
		/// N - Reset.
		/// H - Set if carry from bit 3.
		/// C - Set if carry from bit 7
        /// </summary>
        /// <param name="register">The register.</param>
        private void ADD_r(int register)
        {
            UInt16 operation = (UInt16)(registers[A] + registers[register]);
            registers[A] = (byte)operation;
            FlagZ = registers[A] == 0;
            FlagN = false;
            FlagH = operation > 0x0f;
            FlagC = registers[A] > 0xff;
            ConsumeCycle(4);
        }

        /// <summary>
        /// Add n to A.
        /// 
        /// Use with: 
        /// n = (HL),#
        /// 
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Set if carry from bit 3.
        /// C - Set if carry from bit 7
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void ADD_rm(UInt16 registerMemory)
        {
            UInt16 operation = (UInt16)(registers[A] + memoryController.GetPosition(registerMemory));
            registers[A] = (byte)operation;
            FlagZ = registers[A] == 0;
            FlagN = false;
            FlagH = operation > 0x0f;
            FlagC = operation > 0xff;
            ConsumeCycle(8);
        }

        /// <summary>
        /// Add n to A.
        /// 
        /// Use with: 
        /// n = #
        /// 
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Set if carry from bit 3.
        /// C - Set if carry from bit 7
        /// </summary>
        private void ADD_n()
        {
            UInt16 operation = (UInt16)(registers[A] + memoryController.GetPosition(PC++));
            registers[A] = (byte)operation;
            FlagZ = registers[A] == 0;
            FlagN = false;
            FlagH = operation > 0x0f;
            FlagC = operation > 0xff;
            ConsumeCycle(8);
        }



        #endregion ADD

        #region ADC

        /// <summary>
        /// Add n + Carry flag to A.
		/// Use with: 
		/// n = A,B,C,D,E,H,L
		/// Flags affected:
		/// Z - Set if result is zero.
		/// N - Reset.
		/// H - Set if carry from bit 3.
		/// C - Set if carry from bit 7.
        /// </summary>
        /// <param name="register">The register.</param>
        private void ADC_r(int register)
        {
            UInt16 operation = (UInt16)(registers[A] + registers[register] + (FlagC ? 1 : 0));
            registers[A] = (byte)operation;
            FlagZ = registers[A] == 0;
            FlagN = false;
            FlagH = operation > 0x0f;
            FlagC = registers[A] > 0xff;
            ConsumeCycle(4);
        }

        /// <summary>
        /// Add n + Carry flag to A.
        /// Use with: 
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Set if carry from bit 3.
        /// C - Set if carry from bit 7.
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void ADC_rm(UInt16 registerMemory)
        {
            UInt16 operation = (UInt16)(registers[A] + memoryController.GetPosition(registerMemory) + (FlagC ? 1 : 0));
            registers[A] = (byte)operation;
            FlagZ = registers[A] == 0;
            FlagN = false;
            FlagH = operation > 0x0f;
            FlagC = registers[A] > 0xff;
            ConsumeCycle(8);
        }

        /// <summary>
        /// Add n + Carry flag to A.
        /// Use with: 
        /// n = #
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Set if carry from bit 3.
        /// C - Set if carry from bit 7.
        /// </summary>
        private void ADC_n()
        {
            UInt16 operation = (UInt16)(registers[A] + memoryController.GetPosition(PC++) + (FlagC ? 1 : 0));
            registers[A] = (byte)operation;
            FlagZ = registers[A] == 0;
            FlagN = false;
            FlagH = operation > 0x0f;
            FlagC = registers[A] > 0xff;
            ConsumeCycle(8);
        }

        #endregion

        #region SUB
        /// <summary>
        /// Subtract n from A.
		/// Use with: 
		/// n = A,B,C,D,E,H,L,(HL),#
		/// Flags affected:
		/// Z - Set if result is zero.
		/// N - Set.
		/// H - Set if no borrow from bit 4.
		/// C - Set if no borrow.
        /// </summary>
        /// <param name="register">The register.</param>
        private void SUB_r(int register)
        {
            UInt16 operation = (UInt16)(registers[A] - registers[register]);
            FlagH = (registers[A] & 0x0F) - (registers[register] & 0x0F) > 0x0F;
            registers[A] -= (byte)operation;
            FlagC = operation > 0xFF;
            FlagZ = registers[A] == 0;
            FlagN = true;
            ConsumeCycle(4);
        }

        /// <summary>
        /// Subtract n from A.
        /// Use with: 
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Set.
        /// H - Set if no borrow from bit 4.
        /// C - Set if no borrow.
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void SUB_rm(UInt16 registerMemory)
        {
            var x = memoryController.GetPosition(registerMemory);
            FlagH = (registers[A] & 0x0F) - (x & 0x0F) > 0x0F;
            registers[A] -= x;
            FlagC = registers[A] > 0xFF;
            FlagZ = registers[A] == 0;
            FlagN = true;
            ConsumeCycle(8);
        }

        /// <summary>
        /// Subtract n from A.
        /// Use with: 
        /// n = #
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Set.
        /// H - Set if no borrow from bit 4.
        /// C - Set if no borrow.
        /// </summary>
        private void SUB_nn()
        {
            var x = memoryController.GetPosition(PC++);
            FlagH = (registers[A] & 0x0F) - (x & 0x0F) > 0x0F;
            registers[A] -= x;
            FlagC = registers[A] > 0xFF;
            FlagZ = registers[A] == 0;
            FlagN = true;
            ConsumeCycle(8);
        }

        #endregion

        #region SBC
        /// <summary>
        /// Subtract n + Carry flag from A.
		/// Use with: 
		/// n = A,B,C,D,E,H,L
		/// Flags affected:
		/// Z - Set if result is zero.
		/// N - Set.
		/// H - Set if no borrow from bit 4.
		/// C - Set if no borrow.
        /// </summary>
        /// <param name="register">The register.</param>
        private void SBC_r(int register)
        {
            UInt16 operation = (UInt16)(registers[register] + (FlagC ? 1 : 0));
            FlagH = ((registers[A] & 0x0F) - (registers[register] & 0x0F) - (FlagC ? 1 : 0)) > 0x0F;
            registers[A] -= (byte)operation;
            FlagZ = registers[A] == 0;
            FlagN = true;
            FlagC = operation > 0xFF;
            ConsumeCycle(4);
        }

        /// <summary>
        /// Subtract n + Carry flag from A.
        /// Use with: 
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Set.
        /// H - Set if no borrow from bit 4.
        /// C - Set if no borrow.
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void SBC_rm(UInt16 registerMemory)
        {
            UInt16 operation = (UInt16)(memoryController.GetPosition(registerMemory) + (FlagC ? 1 : 0));
            FlagH = ((registers[A] & 0x0F) - (memoryController.GetPosition(registerMemory) & 0x0F) - (FlagC ? 1 : 0)) > 0x0F;
            registers[A] -= (byte)operation;
            FlagZ = registers[A] == 0;
            FlagN = true;
            FlagC = operation > 0xFF;
            ConsumeCycle(8);
        }
        #endregion
        
        #region AND
        /// <summary>
        /// Logically AND n with A, result in A.
		/// Use with: 
		/// n = A,B,C,D,E,H,L
		/// Flags affected:
		/// Z - Set if result is zero.
		/// N - Reset.
		/// H - Set.
		/// C - Reset.
        /// </summary>
        /// <param name="register">The register.</param>
        private void AND_r(int register)
        {
            UInt16 operation = (UInt16)(registers[register] & registers[A]);
            registers[A] = (byte)operation;
            FlagH = !(FlagC = FlagN = false);
            FlagZ = registers[A] == 0;
            ConsumeCycle(4);
        }

        /// <summary>
        /// Logically AND n with A, result in A.
        /// Use with: 
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Set.
        /// C - Reset.
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void AND_rm(UInt16 registerMemory)
        {
            UInt16 operation = (UInt16)(memoryController.GetPosition(registerMemory) & registers[A]);
            registers[A] = (byte)operation;
            FlagH = !(FlagC = FlagN = false);
            FlagZ = registers[A] == 0;
            ConsumeCycle(8);
        }

        /// <summary>
        /// Logically AND n with A, result in A.
        /// Use with: 
        /// n = #
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Set.
        /// C - Reset.
        /// </summary>
        private void AND_n()
        {
            UInt16 operation = (UInt16)(memoryController.GetPosition(PC++) & registers[A]);
            registers[A] = (byte)operation;
            FlagH = !(FlagC = FlagN = false);
            FlagZ = registers[A] == 0;
            ConsumeCycle(8);
        }
        #endregion

        #region OR
        /// <summary>
		/// Logical OR n with register A, result in A.
		/// Use with: 
		/// n = A,B,C,D,E,H,L
		/// Flags affected:
		/// Z - Set if result is zero.
		/// N - Reset.
		/// H - Reset.
		/// C - Reset
        /// </summary>
        /// <param name="register">The register.</param>
        private void OR_r(int register)
        {
            FlagZ = (registers[A] |= registers[register]) == 0;
            FlagN = FlagC = FlagH = false;
            ConsumeCycle(4);
        }

        /// Logical OR n with register A, result in A.
        /// Use with: 
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Reset
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void OR_rm(UInt16 registerMemory)
        {
            FlagZ = (registers[A] |= memoryController.GetPosition(registerMemory)) == 0;
            FlagN = FlagC = FlagH = false;
            ConsumeCycle(8);
        }

        /// Logical OR n with register A, result in A.
        /// Use with: 
        /// n = #
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Reset
        /// </summary>
        private void OR_n()
        {
            FlagZ = (registers[A] |= memoryController.GetPosition(PC++)) == 0;
            FlagN = FlagC = FlagH = false;
            ConsumeCycle(8);
        }
        #endregion

        #region XOR
        /// <summary>
        /// Logical exclusive OR n with register A, result in A.
		/// Use with: 
		/// n = A,B,C,D,E,H,L
		/// Flags affected:
		/// Z - Set if result is zero.
		/// N - Reset.
		/// H - Reset.
		/// C - Reset
        /// </summary>
        /// <param name="register">The register.</param>
        private void XOR_r(int register)
        {
            FlagN = FlagH = FlagC = false;
            FlagZ = (registers[A] ^= registers[register]) == 0;
            ConsumeCycle(4);
        }

        /// <summary>
        /// Logical exclusive OR n with register A, result in A.
        /// Use with: 
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Reset
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void XOR_rm(UInt16 registerMemory)
        {
            FlagN = FlagH = FlagC = false;
            FlagZ = (registers[A] ^= memoryController.GetPosition(registerMemory)) == 0;
            ConsumeCycle(8);
        }

        /// <summary>
        /// Logical exclusive OR n with register A, result in A.
        /// Use with: 
        /// n = #
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Reset
        /// </summary>
        private void XOR_n()
        {
            FlagN = FlagH = FlagC = false;
            FlagZ = (registers[A] ^= memoryController.GetPosition(PC++)) == 0;
            ConsumeCycle(8);
        }
        #endregion

        #region CP

        /// <summary>
        /// Compare A with n. This is basically an A - n
		///	subtraction instruction but the results are thrown
		///	away.
		///	Use with: 
		///	n = A,B,C,D,E,H,L
		///	Flags affected:
		///	Z - Set if result is zero. (Set if A = n.)
		///	N - Set.
		///	H - Set if no borrow from bit 4.
		///	C - Set for no borrow. (Set if A < n.)
        /// </summary>
        /// <param name="register">The register.</param>
        private void CP_r(int register)
        {
            UInt16 operation = (UInt16)(registers[A] - registers[register]);
            FlagZ = operation == 0;
            FlagN = true;
            FlagH = (registers[A] & 0x0F) - (registers[register] & 0x0F) > 0x0F;
            FlagC = operation < 0 ? true : false;
            ConsumeCycle(4);
        }

        /// <summary>
        /// Compare A with n. This is basically an A - n
        ///	subtraction instruction but the results are thrown
        ///	away.
        ///	Use with: 
        ///	n = (HL)
        ///	Flags affected:
        ///	Z - Set if result is zero. (Set if A = n.)
        ///	N - Set.
        ///	H - Set if no borrow from bit 4.
        ///	C - Set for no borrow. (Set if A < n.)
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void CP_rm(UInt16 registerMemory)
        {
            var x = memoryController.GetPosition(registerMemory);
            UInt16 operation = (UInt16)(registers[A] - x);
            FlagZ = operation == 0;
            FlagN = true;
            FlagH = (registers[A] & 0x0F) - (x & 0x0F) > 0x0F;
            FlagC = operation < 0 ? true : false;
            ConsumeCycle(8);
        }

        /// <summary>
        /// Compare A with n. This is basically an A - n
        ///	subtraction instruction but the results are thrown
        ///	away.
        ///	Use with: 
        ///	n = #
        ///	Flags affected:
        ///	Z - Set if result is zero. (Set if A = n.)
        ///	N - Set.
        ///	H - Set if no borrow from bit 4.
        ///	C - Set for no borrow. (Set if A < n.)
        /// </summary>
        private void CP_n()
        {
            var x = memoryController.GetPosition(PC++);
            UInt16 operation = (UInt16)(registers[A] - x);
            FlagZ = operation == 0;
            FlagN = true;
            FlagH = (registers[A] & 0x0F) - (x & 0x0F) > 0x0F;
            FlagC = operation < 0 ? true : false;
            ConsumeCycle(8);
        }
        #endregion

        #region INC
        /// <summary>
        /// Increment register n.
		/// Use with: 
		/// n = A,B,C,D,E,H,L
		/// Flags affected:
		/// Z - Set if result is zero.
		/// N - Reset.
		/// H - Set if carry from bit 3.
		/// C - Not affected
        /// </summary>
        /// <param name="register">The register.</param>
        private void INC_r(int register)
        {
            registers[register]++;
            FlagZ = registers[register] == 0;
            FlagN = false;
            FlagH = (registers[register] & 0x0F) == 0x00;
            ConsumeCycle(4);
        }

        /// <summary>
        /// Increment register n.
        /// Use with: 
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Set if carry from bit 3.
        /// C - Not affected
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void INC_rm(UInt16 registerMemory)
        {
            var value = (byte)(memoryController.GetPosition(registerMemory) + 1);
            memoryController.Write(registerMemory, value);
            FlagZ = value == 0;
            FlagN = true;
            FlagH = (value & 0x0F) == 0x00;
            ConsumeCycle(12);
        }

        #endregion

        #region DEC

        /// <summary>
        /// Decrement register n.
		/// Use with: 
		/// n = A,B,C,D,E,H,L
		/// Flags affected:
		/// Z - Set if reselt is zero.
		/// N - Set.
		/// H - Set if no borrow from bit 4.
		/// C - Not affected.
        /// </summary>
        /// <param name="register">The register.</param>
        private void DEC_r(int register)
        {
            registers[register]--;
            FlagZ = registers[register] == 0;
            FlagN = true;
            FlagH = (registers[register] & 0x0F) == 0x0F;
            ConsumeCycle(4);
        }

        /// <summary>
        /// Decrement register n.
        /// Use with: 
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if reselt is zero.
        /// N - Set.
        /// H - Set if no borrow from bit 4.
        /// C - Not affected.
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void DEC_rm(UInt16 registerMemory)
        {
            var value = (byte)(memoryController.GetPosition(registerMemory) + 1);
            memoryController.Write(registerMemory, value);
            FlagZ = value == 0;
            FlagN = true;
            FlagH = (value & 0x0F) == 0x0F;
            ConsumeCycle(12);
        }

        #endregion
    }
}
