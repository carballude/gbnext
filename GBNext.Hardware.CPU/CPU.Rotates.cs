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
        /// Rotate A left. Old bit 7 to Carry flag.
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 7 data.
        /// </summary>
        private void RLCA()
        {
            FlagC = (registers[A] & 0x80) == 0x80;
            registers[A] = (byte)(registers[A] << 1);
            FlagZ = registers[A] == 0;
            FlagH = FlagN = false;
            ConsumeCycle(4);
        }

        /// <summary>
        /// Rotate A left through Carry flag.
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 7 data.
        /// </summary>
        private void RLA()
        {
            //TODO RLA
            NotImplemented(0x17);

            ConsumeCycle(4);
        }

        /// <summary>
        /// Rotate A right. Old bit 0 to Carry flag.
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 0 data.
        /// </summary>
        private void RRCA()
        {
            //TODO RRCA
            NotImplemented(0x0F);

            ConsumeCycle(4);
        }

        /// <summary>
        /// Rotate A right through Carry flag.
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 0 data
        /// </summary>
        private void RRA()
        {
            //TODO RRA
            NotImplemented(0x1F);

            ConsumeCycle(4);
        }

        /// <summary>
        /// Rotate n left. Old bit 7 to Carry flag.
        /// Use with: 
        /// n = A,B,C,D,E,H,L
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 7 data
        /// </summary>
        /// <param name="register">The register.</param>
        private void RLC_r(int register)
        {
            //TODO RLC
            NotImplemented(0xCB00);

            ConsumeCycle(8);
        }

        /// <summary>
        /// Rotate n left. Old bit 7 to Carry flag.
        /// Use with: 
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 7 data
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void RLC_rm(UInt16 registerMemory)
        {
            //TODO RLC
            NotImplemented(0xCB06);

            ConsumeCycle(16);
        }

        /// <summary>
        /// Rotate n left through Carry flag.
        /// Use with:
        /// n = A,B,C,D,E,H,L
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 7 data
        /// </summary>
        /// <param name="register">The register.</param>
        private void RL_r(int register)
        {
            //TODO RL
            NotImplemented(0xCB10);

            ConsumeCycle(8);
        }

        /// <summary>
        /// Rotate n left through Carry flag.
        /// Use with: 
        /// n =  (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 7 data
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void RL_rm(UInt16 registerMemory)
        {
            //TODO RL
            NotImplemented(0xCB16);

            ConsumeCycle(16);
        }

        /// <summary>
        /// Rotate n right. Old bit 0 to Carry flag.
        /// Use with:
        /// n = A,B,C,D,E,H,L
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 0 data.
        /// </summary>
        /// <param name="register">The register.</param>
        private void RRC_r(int register)
        {
            //TODO RRC
            NotImplemented(0xCB0A);

            ConsumeCycle(8);
        }

        /// Rotate n right. Old bit 0 to Carry flag.
        /// Use with:
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 0 data.
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void RRC_rm(UInt16 registerMemory)
        {
            //TODO RRC
            NotImplemented(0xCB0E);

            ConsumeCycle(16);
        }

        /// <summary>
        /// Rotate n right through Carry flag.
        /// Use with: 
        /// n = A,B,C,D,E,H,L
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 0 data.
        /// </summary>
        /// <param name="register">The register.</param>
        public void RR_r(int register)
        {
            //TODO RR
            NotImplemented(0xCB1A);

            ConsumeCycle(8);
        }

        /// <summary>
        /// Rotate n right through Carry flag.
        /// Use with:
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 0 data.
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        public void RR_rm(UInt16 registerMemory)
        {
            //TODO RR
            NotImplemented(0xCB1E);

            ConsumeCycle(16);
        }
    }
}
