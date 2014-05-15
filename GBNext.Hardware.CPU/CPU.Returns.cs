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
        /// Pop two bytes from stack & jump to that address.
        /// </summary>
        private void RET()
        {
            var lo = memoryController.GetPosition(_SP++);
            var hi = memoryController.GetPosition(_SP++);

            PC = (ushort)((hi << 8) | lo);

            ConsumeCycle(8);
        }



        /// <summary>
        /// Return if following condition is true:
        /// Use with: 
        /// cc = NZ, Return if Z flag is reset.
        /// cc = Z, Return if Z flag is set.
        /// cc = NC, Return if C flag is reset.
        /// cc = C, Return if C flag is set.
        /// </summary>
        /// <param name="condition">The condition.</param>
        private void RET_cc(JumpCondition condition)
        {
            switch (condition)
            {
                case JumpCondition.NZ:
                    if (!FlagZ)
                    {
                        var lo = memoryController.GetPosition(_SP++);
                        var hi = memoryController.GetPosition(_SP++);

                        PC = (ushort)((hi << 8) | lo);
                    }
                    break;
                case JumpCondition.Z:
                    if (FlagZ)
                    {
                        var lo = memoryController.GetPosition(_SP++);
                        var hi = memoryController.GetPosition(_SP++);

                        PC = (ushort)((hi << 8) | lo);
                    }
                    break;
                case JumpCondition.NC:
                    if (!FlagC)
                    {
                        var lo = memoryController.GetPosition(_SP++);
                        var hi = memoryController.GetPosition(_SP++);

                        PC = (ushort)((hi << 8) | lo);
                    }
                    break;
                case JumpCondition.C:
                    if (FlagC)
                    {
                        var lo = memoryController.GetPosition(_SP++);
                        var hi = memoryController.GetPosition(_SP++);

                        PC = (ushort)((hi << 8) | lo);
                    }
                    break;
            }

            ConsumeCycle(8);
        }



        /// <summary>
        /// Pop two bytes from stack & jump to that address then
        /// enable interrupts.
        /// </summary>
        private void RETI()
        {
            var lo = memoryController.GetPosition(_SP++);
            var hi = memoryController.GetPosition(_SP++);

            PC = (ushort)((hi << 8) | lo);

            FlagInterrupt = true;

            ConsumeCycle(8);
        }

    }
}
