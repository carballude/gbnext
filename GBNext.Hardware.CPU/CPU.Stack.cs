using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBNext.Hardware.CPU
{
    public partial class CPU
    {
        private void PUSH_AF()
        {
            _SP -= 2;
            memoryController.Write16(_SP, AF);
            ConsumeCycle(16);
        }

        private void PUSH_BC()
        {
            _SP -= 2;
            memoryController.Write16(_SP, BC);
            ConsumeCycle(16);
        }

        private void PUSH_DE()
        {
            _SP -= 2;
            memoryController.Write16(_SP, DE);
            ConsumeCycle(16);
        }

        private void PUSH_HL()
        {
            _SP -= 2;
            memoryController.Write16(_SP, HL);
            ConsumeCycle(16);
        }

        private void POP_BC()
        {
            BC = memoryController.GetPosition16(_SP);
            _SP += 2;
            ConsumeCycle(8);
        }

        private void POP_AF()
        {
            AF = memoryController.GetPosition16(_SP);
            _SP += 2;
            ConsumeCycle(8);
        }

        private void POP_DE()
        {
            DE = memoryController.GetPosition16(_SP);
            _SP += 2;
            ConsumeCycle(8);
        }

        private void POP_HL()
        {
            HL = memoryController.GetPosition16(_SP);
            _SP += 2;
            ConsumeCycle(8);
        }
    }
}
