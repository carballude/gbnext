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
        /// Shift n left into Carry. LSB of n set to 0.
        /// Use with:
        /// n = A,B,C,D,E,H,L
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 7 data
        /// </summary>
        /// <param name="register">The register.</param>
        private void SLA_r(int register)
        {
            //TODO SLA
            NotImplemented(0xCB20);

            ConsumeCycle(8);
        }

        /// <summary>
        /// Shift n left into Carry. LSB of n set to 0.
        /// Use with:
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 7 data
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void SLA_rm(UInt16 registerMemory)
        {
            //TODO SLA
            NotImplemented(0xCB26);

            ConsumeCycle(16);
        }

        /// <summary>
        /// Shift n right into Carry. MSB doesn't change.
        /// Use with: 
        /// n = A,B,C,D,E,H,L
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 0 data
        /// </summary>
        /// <param name="register">The register.</param>
        private void SRA_r(int register)
        {
            //TODO SRA
            NotImplemented(0xCB2A);

            ConsumeCycle(8);
        }

        /// <summary>
        /// Shift n right into Carry. MSB doesn't change.
        /// Use with: 
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 0 data
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void SRA_rm(UInt16 registerMemory)
        {
            //TODO SRA
            NotImplemented(0xCB2E);

            ConsumeCycle(16);
        }

        /// <summary>
        /// Shift n right into Carry. MSB set to 0.
        /// Use with: 
        /// n = A,B,C,D,E,H,L
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 0 data
        /// </summary>
        /// <param name="register">The register.</param>
        private void SRL_r(int register)
        {
            //TODO SRL
            NotImplemented(0xCB3A);

            ConsumeCycle(8);
        }

        /// <summary>
        /// Shift n right into Carry. MSB set to 0.
        /// Use with: 
        /// n = (HL)
        /// Flags affected:
        /// Z - Set if result is zero.
        /// N - Reset.
        /// H - Reset.
        /// C - Contains old bit 0 data
        /// </summary>
        private void SRL_rm(UInt16 registerMemory)
        {
            //TODO SRL
            NotImplemented(0xCB3E);

            ConsumeCycle(16);
        }
    }
}
