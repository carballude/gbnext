using GBNext.Hardware.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBNext.Hardware.CPU
{
    public class Class1
    {

        private IMemoryController memoryController;

        #region Registers

        private const int A = 0;
        private const int B = 1;
        private const int C = 2;
        private const int D = 3;
        private const int E = 4;
        private const int F = 5;
        private const int H = 6;
        private const int L = 7;

        byte[] registers = new byte[8];
        UInt16 SP;
        UInt16 PC;
        bool FlagZ, FlagN, FlagH, FlagC;
        bool[] lowerFlags = new bool[4];
        UInt16 AF
        {
            get
            {
                UInt16 af = registers[A];
                af = (UInt16)((af << 8) | registers[F]);
                return af;
            }
            set
            {
                registers[A] = (byte)value;
                registers[F] = (byte)(value >> 8);
            }
        }

        UInt16 BC
        {
            get
            {
                UInt16 bc = registers[B];
                bc = (UInt16)((bc << 8) | registers[C]);
                return bc;
            }
            set
            {
                registers[B] = (byte)value;
                registers[C] = (byte)(value >> 8);
            }
        }

        UInt16 DE
        {
            get
            {
                UInt16 de = registers[D];
                de = (UInt16)((de << 8) | registers[E]);
                return de;
            }
            set
            {
                registers[D] = (byte)value;
                registers[E] = (byte)(value >> 8);
            }
        }

        UInt16 HL
        {
            get
            {
                UInt16 hl = registers[H];
                return (UInt16)((hl << 8) | L);
            }
            set
            {
                registers[H] = (byte)value;
                registers[L] = (byte)(value >> 8);
            }
        }
        #endregion

        private void Init()
        {
            PC = 0x100; // 256
            SP = 0xFFFE; // 65534
        }

        public void ExecuteInstruction(byte currentInstruction)
        {
            switch (currentInstruction)
            {
                case 0: noop(); break;
                case 0x01: LD_rr_nn(BC); break;
                case 0x02: LD_rm_r(BC,A); break;
                case 3: NotImplemented(3); break;
                case 0x04: INC_r(B); break;
                case 0x05: DEC_r(B); break;
                case 0x06: LD_r_n(B); break;
                case 7: NotImplemented(7); break;
                case 0x08: LD_nn_SP(); break;
                case 9: NotImplemented(9); break;
                case 0x0A: LD_r_rm(A,BC); break;
                case 11: NotImplemented(11); break;
                case 0x0C: INC_r(C); break;
                case 0x0D: DEC_r(C); break;
                case 0x0E: LD_r_n(C); break;
                case 15: NotImplemented(15); break;
                case 16: NotImplemented(16); break;
                case 0x11: LD_rr_nn(DE); break;
                case 0x12: LD_rm_r(DE,A); break;
                case 19: NotImplemented(19); break;
                case 0x14: INC_r(D); break;
                case 0x15: DEC_r(D); break;
                case 0x16: LD_r_n(D); break;
                case 23: NotImplemented(23); break;
                case 24: NotImplemented(24); break;
                case 25: NotImplemented(25); break;
                case 0x1A: LD_r_rm(A,DE); break;
                case 27: NotImplemented(27); break;
                case 0x1C: INC_r(E); break;
                case 0x1D: DEC_r(E); break;
                case 0x1E: LD_r_n(E); break;
                case 31: NotImplemented(31); break;
                case 32: NotImplemented(32); break;
                case 0x21: LD_rr_nn(HL); break;
                case 0x22: LDI_rm_r(HL,A); break;
                case 35: NotImplemented(35); break;
                case 0x24: INC_r(H); break;
                case 0x25: DEC_r(H); break;
                case 0x26: LD_r_n(H); break;
                case 39: NotImplemented(39); break;
                case 40: NotImplemented(40); break;
                case 41: NotImplemented(41); break;
                case 0x2A: LDI_r_rm(A, HL); break;
                case 43: NotImplemented(43); break;
                case 0x2C: INC_r(L); break;
                case 0x2D: DEC_r(L); break;
                case 0x2E: LD_r_n(L); break;
                case 47: NotImplemented(47); break;
                case 48: NotImplemented(48); break;
                case 0x31: LD_rr_nn(SP); break;
                case 0x32: LDD_rm_r(HL,A); break;
                case 51: NotImplemented(51); break;
                case 0x34: INC_rm(HL); break;
                case 0x35: DEC_rm(HL); break;
                case 0x36: LD_rm_n(HL); break;
                case 55: NotImplemented(55); break;
                case 56: NotImplemented(56); break;
                case 57: NotImplemented(57); break;
                case 0x3A: LDD_r_rm(A, HL); break;
                case 59: NotImplemented(59); break;
                case 0x3C: INC_r(A); break;
                case 0x3D: DEC_r(A); break;
                case 62: LD_r_n(A); break;
                case 63: NotImplemented(63); break;
                case 0x40: LD_X_X(); break;
                case 0x41: LD_r_r(B, C); break;
                case 0x42: LD_r_r(B, D); break;
                case 0x43: LD_r_r(B, E); break;
                case 0x44: LD_r_r(B, H); break;
                case 0x45: LD_r_r(B, L); break;
                case 0x46: LD_r_rm(B,HL); break;
                case 0x47: LD_r_r(B,A); break;
                case 0x48: LD_r_r(C, B); break;
                case 0x49: LD_X_X(); break;
                case 0x4A: LD_r_r(C, D); break;
                case 0x4B: LD_r_r(C, E); break;
                case 0x4C: LD_r_r(C, H); break;
                case 0x4D: LD_r_r(C, L); break;
                case 0x4E: LD_r_rm(C,HL); break;
                case 0x4F: LD_r_r(C,A); break;
                case 0x50: LD_r_r(D, B); break;
                case 0x51: LD_r_r(D, C); break;
                case 0x52: LD_X_X(); break;
                case 0x53: LD_r_r(D, E); break;
                case 0x54: LD_r_r(D, H); break;
                case 0x55: LD_r_r(D, L); break;
                case 0x56: LD_r_rm(D,HL); break;
                case 0x57: LD_r_r(D,A); break;
                case 0x58: LD_r_r(E, B); break;
                case 0x59: LD_r_r(E, C); break;
                case 0x5A: LD_r_r(E, D); break;
                case 0x5B: LD_X_X(); break;
                case 0x5C: LD_r_r(E, H); break;
                case 0x5D: LD_r_r(E, L); break;
                case 0x5E: LD_r_rm(E,HL); break;
                case 0x5F: LD_r_r(E, A); break;
                case 0x60: LD_r_r(H, B); break;
                case 0x61: LD_r_r(H, C); break;
                case 0x62: LD_r_r(H, D); break;
                case 0x63: LD_r_r(H, E); break;
                case 0x64: LD_X_X(); break;
                case 0x65: LD_r_r(H, L); break;
                case 0x66: LD_r_rm(H,HL); break;
                case 0x67: LD_r_r(H,A); break;
                case 0x68: LD_r_r(L, B); break;
                case 0x69: LD_r_r(L, C); break;
                case 0x6A: LD_r_r(L, D); break;
                case 0x6B: LD_r_r(L, E); break;
                case 0x6C: LD_r_r(L, H); break;
                case 0x6D: LD_X_X(); break;
                case 0x6E: LD_L_m(); break;
                case 0x6F: LD_r_r(L, A); break;
                case 0x70: LD_rm_r(HL, B); break;
                case 0x71: LD_rm_r(HL, C); break;
                case 0x72: LD_rm_r(HL, D); break;
                case 0x73: LD_rm_r(HL, E); break;
                case 0x74: LD_rm_r(HL, H); break;
                case 0x75: LD_rm_r(HL, L); break;
                case 118: NotImplemented(118); break;
                case 0x77: LD_rm_r(HL,A); break;
                case 0x78: LD_r_r(A, B); break;
                case 0x79: LD_r_r(A, C); break;
                case 0x7A: LD_r_r(A, D); break;
                case 0x7B: LD_r_r(A, E); break;
                case 0x7C: LD_r_r(A, H); break;
                case 0x7D: LD_r_r(A, L); break;
                case 0x7E: LD_r_rm(A,HL); break;
                case 0x7F: LD_X_X(); break;
                case 0x80: ADD_r(B); break;
                case 0x81: ADD_r(C); break;
                case 0x82: ADD_r(D); break;
                case 0x83: ADD_r(E); break;
                case 0x84: ADD_r(H); break;
                case 0x85: ADD_r(L); break;
                case 0x86: ADD_rm(HL); break;
                case 0x87: ADD_r(A); break;
                case 0x88: ADC_r(B); break;
                case 0x89: ADC_r(C); break;
                case 0x8A: ADC_r(D); break;
                case 0x8B: ADC_r(E); break;
                case 0x8C: ADC_r(H); break;
                case 0x8D: ADC_r(L); break;
                case 0x8E: ADC_rm(HL); break;
                case 0x8F: ADC_r(A); break;
                case 0x90: SUB_r(B); break;
                case 0x91: SUB_r(C); break;
                case 0x92: SUB_r(D); break;
                case 0x93: SUB_r(E); break;
                case 0x94: SUB_r(H); break;
                case 0x95: SUB_r(L); break;
                case 0x96: SUB_rm(HL); break;
                case 0x97: SUB_r(A); break;
                case 0x98: SBC_r(B); break;
                case 0x99: SBC_r(C); break;
                case 0x9A: SBC_r(D); break;
                case 0x9B: SBC_r(E); break;
                case 0x9C: SBC_r(H); break;
                case 0x9D: SBC_r(L); break;
                case 0x9E: SBC_rm(HL); break;
                case 0x9F: SBC_r(A); break;
                case 0xA0: AND_r(B); break;
                case 0xA1: AND_r(C); break;
                case 0xA2: AND_r(D); break;
                case 0xA3: AND_r(E); break;
                case 0xA4: AND_r(H); break;
                case 0xA5: AND_r(L); break;
                case 0xA6: AND_rm(HL); break;
                case 0xA7: AND_r(A); break;
                case 168: XOR_r(B); break;
                case 169: XOR_r(C); break;
                case 170: XOR_r(D); break;
                case 171: XOR_r(E); break;
                case 172: XOR_r(H); break;
                case 173: XOR_r(L); break;
                case 174: XOR_rm(HL); break;
                case 175: XOR_r(A); break;
                case 0xB0: OR_r(B); break;
                case 0xB1: OR_r(C); break;
                case 0xB2: OR_r(D); break;
                case 0xB3: OR_r(E); break;
                case 0xB4: OR_r(H); break;
                case 0xB5: OR_r(L); break;
                case 0xB6: OR_rm(HL); break;
                case 0xB7: OR_r(A); break;
                case 0xB8: CP_r(B); break;
                case 0xB9: CP_r(C); break;
                case 0xBA: CP_r(D); break;
                case 0xBB: CP_r(E); break;
                case 0xBC: CP_r(H); break;
                case 0xBD: CP_r(L); break;
                case 0xBE: CP_rm(HL); break;
                case 0xBF: CP_r(A); break;
                case 192: NotImplemented(192); break;
                case 193: NotImplemented(193); break;
                case 194: NotImplemented(194); break;
                case 195: NotImplemented(195); break;
                case 196: NotImplemented(196); break;
                case 197: NotImplemented(197); break;
                case 0xC6: ADD_n(); break;
                case 199: NotImplemented(199); break;
                case 200: NotImplemented(200); break;
                case 201: NotImplemented(201); break;
                case 202: NotImplemented(202); break;
                case 203: NotImplemented(203); break;
                case 204: NotImplemented(204); break;
                case 205: NotImplemented(205); break;
                case 0xCE: ADC_n(); break;
                case 207: NotImplemented(207); break;
                case 208: NotImplemented(208); break;
                case 209: NotImplemented(209); break;
                case 210: NotImplemented(210); break;
                case 211: NotImplemented(211); break;
                case 212: NotImplemented(212); break;
                case 213: NotImplemented(213); break;
                case 0xD6: SUB_nn(); break;
                case 215: NotImplemented(215); break;
                case 216: NotImplemented(216); break;
                case 217: NotImplemented(217); break;
                case 218: NotImplemented(218); break;
                case 219: NotImplemented(219); break;
                case 220: NotImplemented(220); break;
                case 221: NotImplemented(221); break;
                case 222: NotImplemented(222); break;
                case 223: NotImplemented(223); break;
                case 0xE0: LD_ffnn_r(A); break;
                case 225: NotImplemented(225); break;
                case 0xE2: LD_ffrm_r(C, A); break;
                case 227: NotImplemented(227); break;
                case 228: NotImplemented(228); break;
                case 229: NotImplemented(229); break;
                case 0xE6: AND_n(); break;
                case 231: NotImplemented(231); break;
                case 232: NotImplemented(232); break;
                case 233: NotImplemented(233); break;
                case 0xEA: LD_nn_r(A); break;
                case 235: NotImplemented(235); break;
                case 236: NotImplemented(236); break;
                case 237: NotImplemented(237); break;
                case 0xEE: XOR_n(); break;
                case 239: NotImplemented(239); break;
                case 0xF0: LD_r_ffnn(A); break;
                case 241: NotImplemented(241); break;
                case 0xF2: LD_r_ffrm(A,C); break;
                case 243: NotImplemented(243); break;
                case 244: NotImplemented(244); break;
                case 245: NotImplemented(245); break;
                case 0xF6: OR_n(); break;
                case 247: NotImplemented(247); break;
                case 0xF8: LDHL_SP_n(); break;
                case 0xF9: LD_rr_rr(SP,HL); break;
                case 0xFA: LD_r_nn(A); break;
                case 251: NotImplemented(251); break;
                case 252: NotImplemented(252); break;
                case 253: NotImplemented(253); break;
                case 0xFE: CP_n(); break;
            }
        }



        #region 8-BIT INSTRUCTIONS

        private void DEC_r(int register)
        {
            registers[register]--;
            FlagZ = registers[register] == 0;
            FlagN = true;
            FlagH = (registers[register] & 0x0F) == 0x0F;
            ConsumeCycle(4);
        }

        private void INC_r(int register)
        {
            registers[register]++;
            FlagZ = registers[register] == 0;
            FlagN = false;
            FlagH = (registers[register] & 0x0F) == 0x00;
            ConsumeCycle(4);
        }

        private void DEC_rm(UInt16 registerMemory)
        {
            var value = (byte)(memoryController.GetPosition(registerMemory) + 1);
            memoryController.Write(registerMemory, value);
            FlagZ = value == 0;
            FlagN = true;
            FlagH = (value & 0x0F) == 0x0F;
            ConsumeCycle(12);
        }

        private void INC_rm(UInt16 registerMemory)
        {
            var value = (byte)(memoryController.GetPosition(registerMemory) + 1);
            memoryController.Write(registerMemory, value);
            FlagZ = value == 0;
            FlagN = true;
            FlagH = (value & 0x0F) == 0x00;
            ConsumeCycle(12);
        }

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

        private void ConsumeCycle(int cycles)
        {
            throw new NotImplementedException();
        }


        private void noop() { }

        #endregion

        #region 16-BIT INSTRUCTIONS

        private void LD_rr_nn(UInt16 registry)
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            registry = (ushort)((hi << 8) | lo);
            ConsumeCycle(12);
        }

        private void LD_rr_rr(UInt16 to, UInt16 from)
        {
            to = from;
            ConsumeCycle(8);
        }

        private void LDHL_SP_n()
        {            
            var n = (sbyte)memoryController.GetPosition(PC++);
            FlagH = ((SP & 0x0F) + (n & 0x0F)) > 0x0F;
            FlagC = ((SP & 0xFF) + (n & 0xFF)) > 0xFF;
            FlagZ = FlagN = false;
            ConsumeCycle(12);
        }

        private void LD_nn_SP()
        {
            var lo = memoryController.GetPosition(PC++);
            var hi = memoryController.GetPosition(PC++);
            var address = (ushort)((hi << 8) | lo);            
            memoryController.Write(address, (byte)SP);
            ConsumeCycle(20);
        }

        #endregion

        #region ALU

        #region SBC
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

        #region SUB
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

        #region ADD

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

        private void ADD_rm(UInt16 registerMemory)
        {
            UInt16 operation = (UInt16)(registers[A] + memoryController.GetPosition(registerMemory));
            registers[A] = (byte)operation;
            FlagZ = registers[A] == 0;
            FlagN = false;
            FlagH = operation > 0x0f;
            FlagC = registers[A] > 0xff;
            ConsumeCycle(8);
        }

        private void ADD_n()
        {
            UInt16 operation = (UInt16)(registers[A] + memoryController.GetPosition(PC++));
            registers[A] = (byte)operation;
            FlagZ = registers[A] == 0;
            FlagN = false;
            FlagH = operation > 0x0f;
            FlagC = registers[A] > 0xff;
            ConsumeCycle(8);
        }

        #endregion

        #region ADC

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

        #region AND
        private void AND_r(int register)
        {
            UInt16 operation = (UInt16)(registers[register] & registers[A]);
            registers[A] = (byte)operation;
            FlagH = !(FlagC = FlagN = false);
            FlagZ = registers[A] == 0;
            ConsumeCycle(4);
        }

        private void AND_rm(UInt16 registerMemory)
        {
            UInt16 operation = (UInt16)(memoryController.GetPosition(registerMemory) & registers[A]);
            registers[A] = (byte)operation;
            FlagH = !(FlagC = FlagN = false);
            FlagZ = registers[A] == 0;
            ConsumeCycle(8);
        }

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
        private void OR_r(int register)
        {
            FlagZ = (registers[A] |= registers[register]) == 0;
            FlagN = FlagC = FlagH = false;
            ConsumeCycle(4);
        }

        private void OR_rm(UInt16 registerMemory)
        {
            FlagZ = (registers[A] |= memoryController.GetPosition(registerMemory)) == 0;
            FlagN = FlagC = FlagH = false;
            ConsumeCycle(8);
        }

        private void OR_n()
        {
            FlagZ = (registers[A] |= memoryController.GetPosition(PC++)) == 0;
            FlagN = FlagC = FlagH = false;
            ConsumeCycle(8);
        }
        #endregion

        #region XOR
        private void XOR_r(int register)
        {
            FlagN = FlagH = FlagC = false;
            FlagZ = (registers[A] ^= registers[register]) == 0;
            ConsumeCycle(4);
        }

        private void XOR_rm(UInt16 registerMemory)
        {
            FlagN = FlagH = FlagC = false;
            FlagZ = (registers[A] ^= memoryController.GetPosition(registerMemory)) == 0;
            ConsumeCycle(8);
        }

        private void XOR_n()
        {
            FlagN = FlagH = FlagC = false;
            FlagZ = (registers[A] ^= memoryController.GetPosition(PC++)) == 0;
            ConsumeCycle(8);
        }
        #endregion

        #region CP

        private void CP_r(int register)
        {
            UInt16 operation = (UInt16)(registers[A] - registers[register]);
            FlagZ = operation == 0;
            FlagN = true;
            FlagH = (registers[A] & 0x0F) - (registers[register] & 0x0F) > 0x0F;
            FlagC = operation < 0 ? true : false;
            ConsumeCycle(4);
        }

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

        #endregion

        private void NotImplemented(int instruction)
        {
            throw new NotImplementedException("Instruction " + instruction + " hasn't been implemented yet!");
        }

    }
}
