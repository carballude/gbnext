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
        /// Push address of next instruction onto stack and then
        /// jump to address nn.
        /// Use with: 
        /// nn = two byte immediate value. (LS byte first.)
        /// </summary>
        private void CALL()
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);

            memoryController.Write(SP, memoryController.GetPosition(PC));

            PC = (ushort)((hi << 8) | lo);

            ConsumeCycle(12);
        }

        /// <summary>
        /// Call address n if following condition is true:
        /// cc = NZ, Call if Z flag is reset.
        /// cc = Z, Call if Z flag is set.
        /// cc = NC, Call if C flag is reset.
        /// cc = C, Call if C flag is set.
        /// Use with: 
        /// nn = two byte immediate value. (LS byte first.)
        /// </summary>
        /// <param name="condition">The condition.</param>
        private void CALL_cc_nn(JumpCondition condition)
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);

            switch (condition)
            {
                case JumpCondition.NZ:
                    if (!FlagZ)
                    {
                        memoryController.Write(SP, memoryController.GetPosition(PC));
                        PC = (ushort)((hi << 8) | lo);
                    }
                    break;
                case JumpCondition.Z:
                    if (FlagZ)
                    {
                        memoryController.Write(SP, memoryController.GetPosition(PC));
                        PC = (ushort)((hi << 8) | lo);
                    }
                    break;
                case JumpCondition.NC:
                    if (!FlagC)
                    {
                        memoryController.Write(SP, memoryController.GetPosition(PC));
                        PC = (ushort)((hi << 8) | lo);
                    }
                    break;
                case JumpCondition.C:
                    if (FlagC)
                    {
                        memoryController.Write(SP, memoryController.GetPosition(PC));
                        PC = (ushort)((hi << 8) | lo);
                    }
                    break;
            }

            ConsumeCycle(12);
        }

    }
}
