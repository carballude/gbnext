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
        /// Jump to address nn.
        /// </summary>
        private void JP_nn()
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            PC = (ushort)((hi << 8) | lo);

            ConsumeCycle(12);
        }

        /// <summary>
        /// Jump to address n if following condition is true:
        /// cc = NZ, Jump if Z flag is reset.
        /// cc = Z, Jump if Z flag is set.
        /// cc = NC, Jump if C flag is reset.
        /// cc = C, Jump if C flag is set.
        /// </summary>
        /// <param name="condition">The condition.</param>
        private void JP_cc_nn(JumpCondition condition)
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);

            switch (condition)
            {
                case JumpCondition.NZ:
                    PC = FlagZ ? PC : (ushort)((hi << 8) | lo);
                    break;
                case JumpCondition.Z:
                    PC = FlagZ ? (ushort)((hi << 8) | lo) : PC;
                    break;
                case JumpCondition.NC:
                    PC = FlagC ? PC : (ushort)((hi << 8) | lo);
                    break;
                case JumpCondition.C:
                    PC = FlagC ? (ushort)((hi << 8) | lo) : PC;
                    break;
            }

            ConsumeCycle(12);
        }

        /// <summary>
        /// Jump to address contained in HL.
        /// </summary>
        /// <param name="registerMemory">The register memory.</param>
        private void JP_rm(UInt16 registerMemory)
        {
            PC = memoryController.GetPosition(registerMemory);
            ConsumeCycle(4);
        }

        /// <summary>
        /// Add n to current address and jump to i
        /// 
        /// Use with: 
        /// n = one byte signed immediate value
        /// </summary>
        private void JR_n()
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);

            var jumpValue = (short)((hi << 8) | lo);

            if (jumpValue >= 0)
            {
                PC += (ushort)jumpValue;
            }
            else
            {
                PC -= (ushort)-jumpValue;
            }
            ConsumeCycle(8);
        }

        /// <summary>
        /// If following condition is true then add n to current
        /// address and jump to it
        /// 
        /// Use with: 
        /// n = one byte signed immediate value
        /// 
        /// cc = NZ, Jump if Z flag is reset.
        /// cc = Z, Jump if Z flag is set.
        /// cc = NC, Jump if C flag is reset.
        /// cc = C, Jump if C flag is set.
        /// </summary>
        /// <param name="condition">The condition.</param>
        private void JR_cc_n(JumpCondition condition)
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);

            var jumpValue = (short)((hi << 8) | lo);

            var updatedPC = PC;

            if (jumpValue >= 0)
            {
                updatedPC += (ushort)jumpValue;
            }
            else
            {
                updatedPC -= (ushort)-jumpValue;
            }

            switch (condition)
            {
                case JumpCondition.NZ:
                    PC = FlagZ ? PC : updatedPC;
                    break;
                case JumpCondition.Z:
                    PC = FlagZ ? updatedPC : PC;
                    break;
                case JumpCondition.NC:
                    PC = FlagC ? PC : updatedPC;
                    break;
                case JumpCondition.C:
                    PC = FlagC ? updatedPC : PC;
                    break;
            }

            ConsumeCycle(8);
        }

    }
}
