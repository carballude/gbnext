using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBNext.Hardware.CPU
{
    public partial class CPU
    {
        #region LD
        private void LD_r_ffnn(int register)
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            registers[register] = memoryController.GetPosition((ushort)(0xFF00 + (ushort)((hi << 8) | lo)));
            ConsumeCycle(12);
        }

        private void LD_ffnn_r(int register)
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            memoryController.Write(memoryController.GetPosition((ushort)(0xFF00 + (ushort)((hi << 8) | lo))), registers[register]);
            ConsumeCycle(12);
        }

        private void LDD_rm_r(UInt16 registerMemory, int register)
        {
            memoryController.Write(registerMemory, registers[register]);
            registers[L]--;
            ConsumeCycle(8);
        }

        private void LDI_r_rm(int register, UInt16 registerMemory)
        {
            registers[register] = memoryController.GetPosition(registerMemory);
            registers[L]++;
            ConsumeCycle(8);
        }

        private void LDI_rm_r(UInt16 registerMemory, int register)
        {
            memoryController.Write(registerMemory, registers[register]);
            registers[L]++;
            ConsumeCycle(8);
        }

        private void LDD_r_rm(int register, UInt16 registerMemory)
        {
            registers[register] = memoryController.GetPosition(registerMemory);
            registers[L]--;
            ConsumeCycle(8);
        }

        private void LD_rm_r(UInt16 registerMemory, int register)
        {
            memoryController.Write(registerMemory, registers[register]);
            ConsumeCycle(8);
        }

        private void LD_r_r(int to, int from)
        {
            memoryController.Write(registers[to], registers[from]);
            ConsumeCycle(4);
        }

        private void LD_r_rm(int register, UInt16 registryMemory)
        {
            registers[register] = memoryController.GetPosition(registryMemory);
            ConsumeCycle(8);
        }

        private void LD_r_ffrm(int to, int from)
        {
            registers[to] = memoryController.GetPosition((UInt16)(0xFF00 + registers[from]));
            ConsumeCycle(8);
        }

        private void LD_ffrm_r(int to, int from)
        {
            memoryController.Write(memoryController.GetPosition((UInt16)(0xFF00 + registers[to])), registers[from]);
            ConsumeCycle(8);
        }

        private void LD_r_nn(int register)
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            registers[register] = memoryController.GetPosition((ushort)((hi << 8) | lo));
            ConsumeCycle(16);
        }

        private void LD_nn_r(int register)
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            memoryController.Write((ushort)((hi << 8) | lo), registers[register]);
            ConsumeCycle(16);
        }

        private void LD_L_m()
        {
            registers[L] = memoryController.GetPosition((ushort)HL);
            ConsumeCycle(8);
        }

        private void LD_X_X() { ConsumeCycle(4); }

        private void LD_r_n(int register)
        {
            registers[register] = memoryController.GetPosition(PC++);
            ConsumeCycle(8);
        }

        private void LD_rm_n(UInt16 register)
        {
            memoryController.Write(memoryController.GetPosition(registers[register]), memoryController.GetPosition(PC++));
            ConsumeCycle(12);
        }
        #endregion
    }
}
