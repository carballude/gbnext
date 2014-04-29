﻿using GBNext.Hardware.Memory;
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
        Int16 AF
        {
            get
            {
                Int16 af = registers[A];
                af = (Int16)((af << 8) | registers[F]);
                return af;
            }
            set
            {
                registers[A] = (byte)value;
                registers[F] = (byte)(value >> 8);
            }
        }

        Int16 BC
        {
            get
            {
                Int16 bc = registers[B];
                bc = (Int16)((bc << 8) | registers[C]);
                return bc;
            }
            set
            {
                registers[B] = (byte)value;
                registers[C] = (byte)(value >> 8);
            }
        }

        Int16 DE
        {
            get
            {
                Int16 de = registers[D];
                de = (Int16)((de << 8) | registers[E]);
                return de;
            }
            set
            {
                registers[D] = (byte)value;
                registers[E] = (byte)(value >> 8);
            }
        }

        Int16 HL
        {
            get
            {
                Int16 hl = registers[H];
                return (Int16)((hl << 8) | L);
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
                case 1: NotImplemented(1); break;
                case 0x02: LD_rm_r(BC,A); break;
                case 3: NotImplemented(3); break;
                case 4: NotImplemented(4); break;
                case 5: NotImplemented(5); break;
                case 0x06: LD_r_n(B); break;
                case 7: NotImplemented(7); break;
                case 8: NotImplemented(8); break;
                case 9: NotImplemented(9); break;
                case 0x0A: LD_r_rm(A,BC); break;
                case 11: NotImplemented(11); break;
                case 12: NotImplemented(12); break;
                case 13: NotImplemented(13); break;
                case 0x0E: LD_r_n(C); break;
                case 15: NotImplemented(15); break;
                case 16: NotImplemented(16); break;
                case 17: NotImplemented(17); break;
                case 0x12: LD_rm_r(DE,A); break;
                case 19: NotImplemented(19); break;
                case 20: NotImplemented(20); break;
                case 21: NotImplemented(21); break;
                case 0x16: LD_r_n(D); break;
                case 23: NotImplemented(23); break;
                case 24: NotImplemented(24); break;
                case 25: NotImplemented(25); break;
                case 0x1A: LD_r_rm(A,DE); break;
                case 27: NotImplemented(27); break;
                case 28: NotImplemented(28); break;
                case 29: NotImplemented(29); break;
                case 0x1E: LD_r_n(E); break;
                case 31: NotImplemented(31); break;
                case 32: NotImplemented(32); break;
                case 33: NotImplemented(33); break;
                case 34: NotImplemented(34); break;
                case 35: NotImplemented(35); break;
                case 36: NotImplemented(36); break;
                case 37: NotImplemented(37); break;
                case 0x26: LD_r_n(H); break;
                case 39: NotImplemented(39); break;
                case 40: NotImplemented(40); break;
                case 41: NotImplemented(41); break;
                case 42: NotImplemented(42); break;
                case 43: NotImplemented(43); break;
                case 44: NotImplemented(44); break;
                case 45: NotImplemented(45); break;
                case 0x2E: LD_r_n(L); break;
                case 47: NotImplemented(47); break;
                case 48: NotImplemented(48); break;
                case 49: NotImplemented(49); break;
                case 50: LDD_mHL_r(A); break;
                case 51: NotImplemented(51); break;
                case 52: NotImplemented(52); break;
                case 53: NotImplemented(53); break;
                case 0x36: LD_mr_n(HL); break;
                case 55: NotImplemented(55); break;
                case 56: NotImplemented(56); break;
                case 57: NotImplemented(57); break;
                case 0x3A: LDD_r_mHL(A); break;
                case 59: NotImplemented(59); break;
                case 60: NotImplemented(60); break;
                case 61: NotImplemented(61); break;
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
                case 0x80: ADDRegisterToA(0x80, B); break;
                case 0x81: ADDRegisterToA(0x81, C); break;
                case 0x82: ADDRegisterToA(0x82, D); break;
                case 0x83: ADDRegisterToA(0x83, E); break;
                case 0x84: ADDRegisterToA(0x84, H); break;
                case 0x85: ADDRegisterToA(0x85, L); break;
                case 0x86: ADDMemoryToA(0x86); break;
                case 0x87: ADDRegisterToA(0x87, A); break;
                case 136: NotImplemented(136); break;
                case 137: NotImplemented(137); break;
                case 138: NotImplemented(138); break;
                case 139: NotImplemented(139); break;
                case 140: NotImplemented(140); break;
                case 141: NotImplemented(141); break;
                case 142: NotImplemented(142); break;
                case 143: NotImplemented(143); break;
                case 144: NotImplemented(144); break;
                case 145: NotImplemented(145); break;
                case 146: NotImplemented(146); break;
                case 147: NotImplemented(147); break;
                case 148: NotImplemented(148); break;
                case 149: NotImplemented(149); break;
                case 150: NotImplemented(150); break;
                case 151: NotImplemented(151); break;
                case 152: NotImplemented(152); break;
                case 153: NotImplemented(153); break;
                case 154: NotImplemented(154); break;
                case 155: NotImplemented(155); break;
                case 156: NotImplemented(156); break;
                case 157: NotImplemented(157); break;
                case 158: NotImplemented(158); break;
                case 159: NotImplemented(159); break;
                case 160: NotImplemented(160); break;
                case 161: NotImplemented(161); break;
                case 162: NotImplemented(162); break;
                case 163: NotImplemented(163); break;
                case 164: NotImplemented(164); break;
                case 165: NotImplemented(165); break;
                case 166: NotImplemented(166); break;
                case 167: NotImplemented(167); break;
                case 168: NotImplemented(168); break;
                case 169: NotImplemented(169); break;
                case 170: NotImplemented(170); break;
                case 171: NotImplemented(171); break;
                case 172: NotImplemented(172); break;
                case 173: NotImplemented(173); break;
                case 174: NotImplemented(174); break;
                case 175: NotImplemented(175); break;
                case 176: NotImplemented(176); break;
                case 177: NotImplemented(177); break;
                case 178: NotImplemented(178); break;
                case 179: NotImplemented(179); break;
                case 180: NotImplemented(180); break;
                case 181: NotImplemented(181); break;
                case 182: NotImplemented(182); break;
                case 183: NotImplemented(183); break;
                case 184: NotImplemented(184); break;
                case 185: NotImplemented(185); break;
                case 186: NotImplemented(186); break;
                case 187: NotImplemented(187); break;
                case 188: NotImplemented(188); break;
                case 189: NotImplemented(189); break;
                case 190: NotImplemented(190); break;
                case 191: NotImplemented(191); break;
                case 192: NotImplemented(192); break;
                case 193: NotImplemented(193); break;
                case 194: NotImplemented(194); break;
                case 195: NotImplemented(195); break;
                case 196: NotImplemented(196); break;
                case 197: NotImplemented(197); break;
                case 0xC6: ADDInmediateToA(0xC6); break;
                case 199: NotImplemented(199); break;
                case 200: NotImplemented(200); break;
                case 201: NotImplemented(201); break;
                case 202: NotImplemented(202); break;
                case 203: NotImplemented(203); break;
                case 204: NotImplemented(204); break;
                case 205: NotImplemented(205); break;
                case 206: NotImplemented(206); break;
                case 207: NotImplemented(207); break;
                case 208: NotImplemented(208); break;
                case 209: NotImplemented(209); break;
                case 210: NotImplemented(210); break;
                case 211: NotImplemented(211); break;
                case 212: NotImplemented(212); break;
                case 213: NotImplemented(213); break;
                case 214: NotImplemented(214); break;
                case 215: NotImplemented(215); break;
                case 216: NotImplemented(216); break;
                case 217: NotImplemented(217); break;
                case 218: NotImplemented(218); break;
                case 219: NotImplemented(219); break;
                case 220: NotImplemented(220); break;
                case 221: NotImplemented(221); break;
                case 222: NotImplemented(222); break;
                case 223: NotImplemented(223); break;
                case 224: NotImplemented(224); break;
                case 225: NotImplemented(225); break;
                case 0xE2: LD_ffrm_r(C, A); break;
                case 227: NotImplemented(227); break;
                case 228: NotImplemented(228); break;
                case 229: NotImplemented(229); break;
                case 230: NotImplemented(230); break;
                case 231: NotImplemented(231); break;
                case 232: NotImplemented(232); break;
                case 233: NotImplemented(233); break;
                case 0xEA: LD_nn_r(A); break;
                case 235: NotImplemented(235); break;
                case 236: NotImplemented(236); break;
                case 237: NotImplemented(237); break;
                case 238: NotImplemented(238); break;
                case 239: NotImplemented(239); break;
                case 240: NotImplemented(240); break;
                case 241: NotImplemented(241); break;
                case 0xF2: LD_r_ffrm(A,C); break;
                case 243: NotImplemented(243); break;
                case 244: NotImplemented(244); break;
                case 245: NotImplemented(245); break;
                case 246: NotImplemented(246); break;
                case 247: NotImplemented(247); break;
                case 248: NotImplemented(248); break;
                case 249: NotImplemented(249); break;
                case 0xFA: LD_r_nn(A); break;
                case 251: NotImplemented(251); break;
                case 252: NotImplemented(252); break;
                case 253: NotImplemented(253); break;
                case 254: NotImplemented(254); break;
            }
        }

        private void LDD_mHL_r(int register)
        {
            memoryController.Write((ushort)HL, registers[register]);
            registers[L]--;
            ConsumeCycle(8);
        }

        private void LDD_r_mHL(int register)
        {
            registers[register] = memoryController.GetPosition((ushort)HL);
            registers[L]--;
            ConsumeCycle(8);
        }

        private void LD_rm_r(Int16 registerMemory, int register)
        {
            memoryController.Write((ushort)registerMemory, registers[register]);
            ConsumeCycle(8);
        }

        private void LD_r_r(int to, int from)
        {
            memoryController.Write(registers[to], registers[from]);
            ConsumeCycle(4);
        }

        private void LD_r_rm(int register, Int16 registryMemory)
        {
            registers[register] = memoryController.GetPosition((ushort)registryMemory);
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

        private void LD_mr_n(Int16 register)
        {
            memoryController.Write(memoryController.GetPosition(registers[register]), memoryController.GetPosition(PC++));
            ConsumeCycle(12);
        }

        private void ConsumeCycle(int cycles)
        {
            throw new NotImplementedException();
        }

        #region Instructions
        private void noop() { }
        #endregion

        #region ALU

        #region ADD
        private void ADDRegisterToA(int opcode, byte registerValue)
        {
            UInt16 result = (UInt16)(registers[A] + registerValue);
            registers[A] = (byte)result;
            ADDComprobeFlags(result);
            ConsumeCycle(4);
        }

        private void ADDMemoryToA(int opcode)
        {
            UInt16 result = (UInt16)(registers[A] + memoryController.GetPosition((ushort)HL));
            registers[A] = (byte)result;
            ADDComprobeFlags(result);
            ConsumeCycle(8);
        }

        private void ADDInmediateToA(int opcode)
        {
            UInt16 result = (UInt16)(registers[A] + memoryController.GetPosition(PC++));
            registers[A] = (byte)result;
            ADDComprobeFlags(result);
            ConsumeCycle(8);
        }

        private void ADDComprobeFlags(UInt16 result)
        {
            FlagH = result > 0x0f;
            FlagC = result > 0xff;
            FlagZ = registers[A] == 0;
            FlagN = false;
        }

        #endregion

        #endregion

        private void NotImplemented(int instruction)
        {
            throw new NotImplementedException("Instruction " + instruction + " hasn't been implemented yet!");
        }

    }
}
