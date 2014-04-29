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
        byte A, B, D, H, F, C, E, L;
        UInt16 SP;
        UInt16 PC;
        bool FlagZ, FlagN, FlagH, FlagC;
        bool[] lowerFlags = new bool[4];
        Int16 AF { 
            get
            {
                Int16 af = A;
                af = (Int16)((af << 8) | F);
                return af;
            }
            set
            {
                A = (byte)value;
                F = (byte)(value >> 8);
            }
        }

        Int16 HL
        {
            get
            {
                Int16 hl = H;
                return (Int16)((hl << 8) | L);
            }
            set
            {
                H = (byte)value;
                L = (byte)(value >> 8);
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
                case 2: NotImplemented(2); break;
                case 3: NotImplemented(3); break;
                case 4: NotImplemented(4); break;
                case 5: NotImplemented(5); break;
                case 0x06: LD_B_N(); break;
                case 7: NotImplemented(7); break;
                case 8: NotImplemented(8); break;
                case 9: NotImplemented(9); break;
                case 10: NotImplemented(10); break;
                case 11: NotImplemented(11); break;
                case 12: NotImplemented(12); break;
                case 13: NotImplemented(13); break;
                case 0x0E: LD_C_N(); break;
                case 15: NotImplemented(15); break;
                case 16: NotImplemented(16); break;
                case 17: NotImplemented(17); break;
                case 18: NotImplemented(18); break;
                case 19: NotImplemented(19); break;
                case 20: NotImplemented(20); break;
                case 21: NotImplemented(21); break;
                case 0x16: LD_D_N(); break;
                case 23: NotImplemented(23); break;
                case 24: NotImplemented(24); break;
                case 25: NotImplemented(25); break;
                case 26: NotImplemented(26); break;
                case 27: NotImplemented(27); break;
                case 28: NotImplemented(28); break;
                case 29: NotImplemented(29); break;
                case 0x1E: LD_E_N(); break;
                case 31: NotImplemented(31); break;
                case 32: NotImplemented(32); break;
                case 33: NotImplemented(33); break;
                case 34: NotImplemented(34); break;
                case 35: NotImplemented(35); break;
                case 36: NotImplemented(36); break;
                case 37: NotImplemented(37); break;
                case 0x26: LD_H_N(); break;
                case 39: NotImplemented(39); break;
                case 40: NotImplemented(40); break;
                case 41: NotImplemented(41); break;
                case 42: NotImplemented(42); break;
                case 43: NotImplemented(43); break;
                case 44: NotImplemented(44); break;
                case 45: NotImplemented(45); break;
                case 0x2E: LD_L_N(); break;
                case 47: NotImplemented(47); break;
                case 48: NotImplemented(48); break;
                case 49: NotImplemented(49); break;
                case 50: NotImplemented(50); break;
                case 51: NotImplemented(51); break;
                case 52: NotImplemented(52); break;
                case 53: NotImplemented(53); break;
                case 54: NotImplemented(54); break;
                case 55: NotImplemented(55); break;
                case 56: NotImplemented(56); break;
                case 57: NotImplemented(57); break;
                case 58: NotImplemented(58); break;
                case 59: NotImplemented(59); break;
                case 60: NotImplemented(60); break;
                case 61: NotImplemented(61); break;
                case 62: NotImplemented(62); break;
                case 63: NotImplemented(63); break;
                case 64: NotImplemented(64); break;
                case 65: NotImplemented(65); break;
                case 66: NotImplemented(66); break;
                case 67: NotImplemented(67); break;
                case 68: NotImplemented(68); break;
                case 69: NotImplemented(69); break;
                case 70: NotImplemented(70); break;
                case 71: NotImplemented(71); break;
                case 72: NotImplemented(72); break;
                case 73: NotImplemented(73); break;
                case 74: NotImplemented(74); break;
                case 75: NotImplemented(75); break;
                case 76: NotImplemented(76); break;
                case 77: NotImplemented(77); break;
                case 78: NotImplemented(78); break;
                case 79: NotImplemented(79); break;
                case 80: NotImplemented(80); break;
                case 81: NotImplemented(81); break;
                case 82: NotImplemented(82); break;
                case 83: NotImplemented(83); break;
                case 84: NotImplemented(84); break;
                case 85: NotImplemented(85); break;
                case 86: NotImplemented(86); break;
                case 87: NotImplemented(87); break;
                case 88: NotImplemented(88); break;
                case 89: NotImplemented(89); break;
                case 90: NotImplemented(90); break;
                case 91: NotImplemented(91); break;
                case 92: NotImplemented(92); break;
                case 93: NotImplemented(93); break;
                case 94: NotImplemented(94); break;
                case 95: NotImplemented(95); break;
                case 96: NotImplemented(96); break;
                case 97: NotImplemented(97); break;
                case 98: NotImplemented(98); break;
                case 99: NotImplemented(99); break;
                case 100: NotImplemented(100); break;
                case 101: NotImplemented(101); break;
                case 102: NotImplemented(102); break;
                case 103: NotImplemented(103); break;
                case 104: NotImplemented(104); break;
                case 105: NotImplemented(105); break;
                case 106: NotImplemented(106); break;
                case 107: NotImplemented(107); break;
                case 108: NotImplemented(108); break;
                case 109: NotImplemented(109); break;
                case 110: NotImplemented(110); break;
                case 111: NotImplemented(111); break;
                case 112: NotImplemented(112); break;
                case 113: NotImplemented(113); break;
                case 114: NotImplemented(114); break;
                case 115: NotImplemented(115); break;
                case 116: NotImplemented(116); break;
                case 117: NotImplemented(117); break;
                case 118: NotImplemented(118); break;
                case 119: NotImplemented(119); break;
                case 0x78: LD_A_r(B); break;
                case 0x79: LD_A_r(C); break;
                case 0x7A: LD_A_r(D); break;
                case 0x7B: LD_A_r(E); break;
                case 0x7C: LD_A_r(H); break;
                case 0x7D: LD_A_r(L); break;
                case 0x7E: LD_A_m(); break;
                case 0x7F: LD_A_A(); break;
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
                case 226: NotImplemented(226); break;
                case 227: NotImplemented(227); break;
                case 228: NotImplemented(228); break;
                case 229: NotImplemented(229); break;
                case 230: NotImplemented(230); break;
                case 231: NotImplemented(231); break;
                case 232: NotImplemented(232); break;
                case 233: NotImplemented(233); break;
                case 234: NotImplemented(234); break;
                case 235: NotImplemented(235); break;
                case 236: NotImplemented(236); break;
                case 237: NotImplemented(237); break;
                case 238: NotImplemented(238); break;
                case 239: NotImplemented(239); break;
                case 240: NotImplemented(240); break;
                case 241: NotImplemented(241); break;
                case 242: NotImplemented(242); break;
                case 243: NotImplemented(243); break;
                case 244: NotImplemented(244); break;
                case 245: NotImplemented(245); break;
                case 246: NotImplemented(246); break;
                case 247: NotImplemented(247); break;
                case 248: NotImplemented(248); break;
                case 249: NotImplemented(249); break;
                case 250: NotImplemented(250); break;
                case 251: NotImplemented(251); break;
                case 252: NotImplemented(252); break;
                case 253: NotImplemented(253); break;
                case 254: NotImplemented(254); break;
            }
        }

        private void LD_A_r(byte register)
        {
            A = register;
            ConsumeCycle(4);
        }

        private void LD_B_r(byte register)
        {
            A = register;
            ConsumeCycle(4);
        }

        private void LD_A_m()
        {
            A = memoryController.GetPosition((ushort)HL);
            ConsumeCycle(8);
        }

        private void LD_A_A()
        {
            ConsumeCycle(4);
        }

        private void LD_B_B()
        {
            ConsumeCycle(4);
        }

        private void LD_L_N()
        {
            L = memoryController.GetPosition(PC++);
            ConsumeCycle(8);
        }

        private void LD_H_N()
        {
            H = memoryController.GetPosition(PC++);
            ConsumeCycle(8);
        }

        private void LD_E_N()
        {
            E = memoryController.GetPosition(PC++);
            ConsumeCycle(8);
        }

        private void LD_D_N()
        {
            D = memoryController.GetPosition(PC++);
            ConsumeCycle(8);
        }

        private void LD_C_N()
        {
            C = memoryController.GetPosition(PC++);
            ConsumeCycle(8);
        }

        private void LD_B_N()
        {
            B = memoryController.GetPosition(PC++);
            ConsumeCycle(8);
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
            UInt16 result = (UInt16)(A + registerValue);
            A = (byte)result;
            ADDComprobeFlags(result);
            ConsumeCycle(4);
        }

        private void ADDMemoryToA(int opcode)
        {
            UInt16 result = (UInt16)(A + memoryController.GetPosition((ushort)HL));
            A = (byte)result;
            ADDComprobeFlags(result);
            ConsumeCycle(8);
        }

        private void ADDInmediateToA(int opcode)
        {
            UInt16 result = (UInt16)(A + memoryController.GetPosition(PC++));
            A = (byte)result;
            ADDComprobeFlags(result);
            ConsumeCycle(8);
        }

        private void ADDComprobeFlags(UInt16 result)
        {
            FlagH = result > 0x0f;
            FlagC = result > 0xff;
            FlagZ = A == 0;
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
