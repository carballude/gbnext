using GBNext.Disassembler;
using GBNext.Hardware.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBNext.Hardware.CPU
{
    public partial class CPU
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

        UInt16 _SP;
        UInt16 SP
        {
            get
            {
                return --_SP; //¿PORQUEEEEEEEEEEEEEE?
            }
            set
            {
                _SP = value;
            }
        }

        UInt16 PC;
        bool FlagZ, FlagN, FlagH, FlagC, FlagInterrupt, FlagDoubleOpcode;
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

        public CPU(IMemoryController memoryController)
        {
            this.memoryController = memoryController;
            Init();
        }

        private enum JumpCondition { NZ, Z, NC, C };

        /// <summary>
        /// Initializes the emulation.
        /// </summary>
        private void Init()
        {
            PC = 0x100; // 256
            SP = 0xFFFE; // 65534
        }

        public override string ToString()
        {
            var ret = "PC: " + string.Format("0x{0:X2}", PC) + "\n";
            ret += "SP: " + string.Format("0x{0:X2}", _SP);
            return ret;
        }

        public void ExecuteNextInstruction()
        {
            var opcode = memoryController.GetPosition(PC++);
            var instruction = Mnemonics.Mnemonic(opcode);
            var instructionText = instruction.Text;
            if (instruction.ExtraOpcodes == 1)
                instructionText = instructionText.Replace("n", string.Format("0x{0:X2}", memoryController.GetPosition((ushort)(PC))));
            else if(instruction.ExtraOpcodes == 2)
            {
                var first = memoryController.GetPosition((ushort)(PC));
                var second = memoryController.GetPosition((ushort)(PC + 1));
                instructionText = instructionText.Replace("nn", string.Format("0x{0:X2}{1:X2}", second, first));
            }
            Console.WriteLine("Executing: {0}", instructionText);
            ExecuteInstruction(opcode);
            ShowNextInstruction();
            Console.WriteLine(this);
        }

        public void ShowNextInstruction()
        {
            var opcode = memoryController.GetPosition(PC);
            var instruction = Mnemonics.Mnemonic(opcode);
            var instructionText = instruction.Text;
            if (instruction.ExtraOpcodes == 1)
                instructionText = instructionText.Replace("n", string.Format("0x{0:X2}", memoryController.GetPosition((ushort)(PC+1))));
            else if (instruction.ExtraOpcodes == 2)
            {
                var first = memoryController.GetPosition((ushort)(PC+1));
                var second = memoryController.GetPosition((ushort)(PC + 2));
                instructionText = instructionText.Replace("nn", string.Format("0x{0:X2}{1:X2}", second, first));
            }
            Console.WriteLine("Next instruction: {0}", instructionText);
        }

        /// <summary>
        /// Executes the instruction selected.
        /// </summary>
        /// <param name="currentInstruction">The current instruction.</param>
        public void ExecuteInstruction(byte currentInstruction)
        {
            if (!FlagDoubleOpcode)
            {
                switch (currentInstruction)
                {
                    case 0x00: NOP(); break;
                    case 0x01: LD_rr_nn(BC); break;
                    case 0x02: LD_rm_r(BC, A); break;
                    case 0x03: INC_rr(B,C); break;
                    case 0x04: INC_r(B); break;
                    case 0x05: DEC_r(B); break;
                    case 0x06: LD_r_n(B); break;
                    case 0x07: RLCA(); break;
                    case 0x08: LD_nn_SP(); break;
                    case 0x09: ADD_HL_rr(BC); break;
                    case 0x0A: LD_r_rm(A, BC); break;
                    case 0x0B: DEC_rr(B,C); break;
                    case 0x0C: INC_r(C); break;
                    case 0x0D: DEC_r(C); break;
                    case 0x0E: LD_r_n(C); break;
                    case 0x0F: RRCA(); break;
                    case 0x10: STOP(); break;
                    case 0x11: LD_rr_nn(DE); break;
                    case 0x12: LD_rm_r(DE, A); break;
                    case 0x13: INC_rr(D,E); break;
                    case 0x14: INC_r(D); break;
                    case 0x15: DEC_r(D); break;
                    case 0x16: LD_r_n(D); break;
                    case 0x17: RLA(); break;
                    case 0x18: JR_n(); break;
                    case 0x19: ADD_HL_rr(DE); break;
                    case 0x1A: LD_r_rm(A, DE); break;
                    case 0x1B: DEC_rr(D,E); break;
                    case 0x1C: INC_r(E); break;
                    case 0x1D: DEC_r(E); break;
                    case 0x1E: LD_r_n(E); break;
                    case 0x1F: RRA(); break;
                    case 0x20: JR_cc_n(JumpCondition.NZ); break;
                    case 0x21: LD_rr_nn(HL); break;
                    case 0x22: LDI_rm_r(HL, A); break;
                    case 0x23: INC_rr(H, L); break;
                    case 0x24: INC_r(H); break;
                    case 0x25: DEC_r(H); break;
                    case 0x26: LD_r_n(H); break;
                    case 0x27: DAA(); break;
                    case 0x28: JR_cc_n(JumpCondition.Z); break;
                    case 0x29: ADD_HL_rr(HL); break;
                    case 0x2A: LDI_r_rm(A, HL); break;
                    case 0x2B: DEC_rr(H, L); break;
                    case 0x2C: INC_r(L); break;
                    case 0x2D: DEC_r(L); break;
                    case 0x2E: LD_r_n(L); break;
                    case 0x2F: CPL(); break;
                    case 0x30: JR_cc_n(JumpCondition.NC); break;
                    case 0x31: LD_rr_nn(SP); break;
                    case 0x32: LDD_rm_r(HL, A); break;
                    case 0x33: INC_SP(); break;
                    case 0x34: INC_rm(HL); break;
                    case 0x35: DEC_rm(HL); break;
                    case 0x36: LD_rm_n(HL); break;
                    case 0x37: SCF(); break;
                    case 0x38: JR_cc_n(JumpCondition.C); break;
                    case 0x39: ADD_HL_rr(_SP); break;
                    case 0x3A: LDD_r_rm(A, HL); break;
                    case 0x3B: DEC_SP(); break;
                    case 0x3C: INC_r(A); break;
                    case 0x3D: DEC_r(A); break;
                    case 0x3E: LD_r_n(A); break;
                    case 0X3F: CCF(); break;
                    case 0x40: LD_X_X(); break;
                    case 0x41: LD_r_r(B, C); break;
                    case 0x42: LD_r_r(B, D); break;
                    case 0x43: LD_r_r(B, E); break;
                    case 0x44: LD_r_r(B, H); break;
                    case 0x45: LD_r_r(B, L); break;
                    case 0x46: LD_r_rm(B, HL); break;
                    case 0x47: LD_r_r(B, A); break;
                    case 0x48: LD_r_r(C, B); break;
                    case 0x49: LD_X_X(); break;
                    case 0x4A: LD_r_r(C, D); break;
                    case 0x4B: LD_r_r(C, E); break;
                    case 0x4C: LD_r_r(C, H); break;
                    case 0x4D: LD_r_r(C, L); break;
                    case 0x4E: LD_r_rm(C, HL); break;
                    case 0x4F: LD_r_r(C, A); break;
                    case 0x50: LD_r_r(D, B); break;
                    case 0x51: LD_r_r(D, C); break;
                    case 0x52: LD_X_X(); break;
                    case 0x53: LD_r_r(D, E); break;
                    case 0x54: LD_r_r(D, H); break;
                    case 0x55: LD_r_r(D, L); break;
                    case 0x56: LD_r_rm(D, HL); break;
                    case 0x57: LD_r_r(D, A); break;
                    case 0x58: LD_r_r(E, B); break;
                    case 0x59: LD_r_r(E, C); break;
                    case 0x5A: LD_r_r(E, D); break;
                    case 0x5B: LD_X_X(); break;
                    case 0x5C: LD_r_r(E, H); break;
                    case 0x5D: LD_r_r(E, L); break;
                    case 0x5E: LD_r_rm(E, HL); break;
                    case 0x5F: LD_r_r(E, A); break;
                    case 0x60: LD_r_r(H, B); break;
                    case 0x61: LD_r_r(H, C); break;
                    case 0x62: LD_r_r(H, D); break;
                    case 0x63: LD_r_r(H, E); break;
                    case 0x64: LD_X_X(); break;
                    case 0x65: LD_r_r(H, L); break;
                    case 0x66: LD_r_rm(H, HL); break;
                    case 0x67: LD_r_r(H, A); break;
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
                    case 0x76: HALT(); break;
                    case 0x77: LD_rm_r(HL, A); break;
                    case 0x78: LD_r_r(A, B); break;
                    case 0x79: LD_r_r(A, C); break;
                    case 0x7A: LD_r_r(A, D); break;
                    case 0x7B: LD_r_r(A, E); break;
                    case 0x7C: LD_r_r(A, H); break;
                    case 0x7D: LD_r_r(A, L); break;
                    case 0x7E: LD_r_rm(A, HL); break;
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
                    case 0xA8: XOR_r(B); break;
                    case 0xA9: XOR_r(C); break;
                    case 0xAA: XOR_r(D); break;
                    case 0xAB: XOR_r(E); break;
                    case 0xAC: XOR_r(H); break;
                    case 0xAD: XOR_r(L); break;
                    case 0xAE: XOR_rm(HL); break;
                    case 0xAF: XOR_r(A); break;
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
                    case 0xC0: RET_cc(JumpCondition.NZ); break;
                    case 0xC1: POP_BC(); break;
                    case 0xC2: JP_cc_nn(JumpCondition.NZ); break;
                    case 0xC3: JP_nn(); break;
                    case 0xC4: CALL_cc_nn(JumpCondition.NZ); break;
                    case 0xC5: PUSH_BC(); break;
                    case 0xC6: ADD_n(); break;
                    case 0xC7: RST_n(0x00); break;
                    case 0xC8: RET_cc(JumpCondition.Z); break;
                    case 0xC9: RET(); break;
                    case 0xCA: JP_cc_nn(JumpCondition.Z); break;
                    case 0XCB: FlagDoubleOpcode = true; break;
                    case 0xCC: CALL_cc_nn(JumpCondition.Z); break;
                    case 0xCD: CALL(); break;
                    case 0xCE: ADC_n(); break;
                    case 0xCF: RST_n(0x08); ; break;
                    case 0xD0: RET_cc(JumpCondition.NC); break;
                    case 0xD1: POP_DE(); break;
                    case 0xD2: JP_cc_nn(JumpCondition.NC); break;
                    case 211: NotImplemented(211); break;
                    case 0xD4: CALL_cc_nn(JumpCondition.NC); break;
                    case 0xD5: PUSH_DE(); ; break;
                    case 0xD6: SUB_nn(); break;
                    case 0xD7: RST_n(0x10); break;
                    case 0xD8: RET_cc(JumpCondition.C); break;
                    case 0xD9: RETI(); break;
                    case 0xDA: JP_cc_nn(JumpCondition.C); break;
                    case 219: NotImplemented(219); break;
                    case 0xDC: CALL_cc_nn(JumpCondition.C); break;
                    case 221: NotImplemented(221); break;
                    case 222: NotImplemented(222); break;
                    case 0xDF: RST_n(0x18); break;
                    case 0xE0: LD_ffnn_r(A); break;
                    case 0xE1: POP_HL(); break;
                    case 0xE2: LD_ffrm_r(C, A); break;
                    case 227: NotImplemented(227); break;
                    case 228: NotImplemented(228); break;
                    case 0xE5: PUSH_HL(); break;
                    case 0xE6: AND_n(); break;
                    case 0xE7: RST_n(0x20); break;
                    case 0xE8: ADD_SP(); break;
                    case 0xE9: JP_rm(HL); break;
                    case 0xEA: LD_nn_r(A); break;
                    case 235: NotImplemented(235); break;
                    case 236: NotImplemented(236); break;
                    case 237: NotImplemented(237); break;
                    case 0xEE: XOR_n(); break;
                    case 0xEF: RST_n(0x28); break;
                    case 0xF0: LD_r_ffnn(A); break;
                    case 0xF1: POP_AF(); break;
                    case 0xF2: LD_r_ffrm(A, C); break;
                    case 0xF3: DI(); break;
                    case 244: NotImplemented(244); break;
                    case 0xF5: PUSH_AF(); break;
                    case 0xF6: OR_n(); break;
                    case 0xF7: RST_n(0x30); break;
                    case 0xF8: LDHL_SP_n(); break;
                    case 0xF9: LD_rr_rr(SP, HL); break;
                    case 0xFA: LD_r_nn(A); break;
                    case 0xFB: EI(); break;
                    case 252: NotImplemented(252); break;
                    case 253: NotImplemented(253); break;
                    case 0xFE: CP_n(); break;
                    case 0XFF: RST_n(0x38); break;
                }
            }
            else
            {
                switch (currentInstruction)
                {

                    case 0x00: RLC_r(B); break;
                    case 0x01: RLC_r(C); break;
                    case 0x02: RLC_r(D); break;
                    case 0x03: RLC_r(E); break;
                    case 0x04: RLC_r(H); break;
                    case 0x05: RLC_r(L); break;
                    case 0x06: RLC_rm(HL); break;
                    case 0x07: RLC_r(A); break;
                    case 0x08: RRC_r(B); break;
                    case 0x09: RRC_r(C); break;
                    case 0x0A: RRC_r(D); break;
                    case 0x0B: RRC_r(E); break;
                    case 0x0C: RRC_r(H); break;
                    case 0x0D: RRC_r(L); break;
                    case 0x0E: RRC_rm(HL); break;
                    case 0x0F: RRC_r(A); break;
                    case 0x10: RL_r(B); break;
                    case 0x11: RL_r(C); break;
                    case 0x12: RL_r(D); break;
                    case 0x13: RL_r(E); break;
                    case 0x14: RL_r(H); break;
                    case 0x15: RL_r(L); break;
                    case 0x16: RL_rm(HL); break;
                    case 0x17: RL_r(A); break;
                    case 0x18: RR_r(B); break;
                    case 0x19: RR_r(C); break;
                    case 0x1A: RR_r(D); break;
                    case 0x1B: RR_r(E); break;
                    case 0x1C: RR_r(H); break;
                    case 0x1D: RR_r(L); break;
                    case 0x1E: RR_rm(HL); break;
                    case 0x1F: RR_r(A); break;
                    case 0x20: SLA_r(B); break;
                    case 0x21: SLA_r(C); break;
                    case 0x22: SLA_r(D); break;
                    case 0x23: SLA_r(E); break;
                    case 0x24: SLA_r(H); break;
                    case 0x25: SLA_r(L); break;
                    case 0x26: SLA_rm(HL); break;
                    case 0x27: SLA_r(A); break;
                    case 0x28: SRA_r(B); break;
                    case 0x29: SRA_r(C); break;
                    case 0x2A: SRA_r(D); break;
                    case 0x2B: SRA_r(E); break;
                    case 0x2C: SRA_r(H); break;
                    case 0x2D: SRA_r(L); break;
                    case 0x2E: SRA_rm(HL); break;
                    case 0x2F: SRA_r(A); break;
                    case 0x30: SWAP_r(B); break;
                    case 0x31: SWAP_r(C); break;
                    case 0x32: SWAP_r(D); break;
                    case 0x33: SWAP_r(E); break;
                    case 0x34: SWAP_r(H); break;
                    case 0x35: SWAP_r(L); break;
                    case 0x36: SWAP_rm(HL); break;
                    case 0x37: SWAP_r(A); break;
                    case 0x38: SRL_r(B); break;
                    case 0x39: SRL_r(C); break;
                    case 0x3A: SRL_r(D); break;
                    case 0x3B: SRL_r(E); break;
                    case 0x3C: SRL_r(H); break;
                    case 0x3D: SRL_r(L); break;
                    case 0x3E: SRL_rm(HL); break;
                    case 0x3F: SRL_r(A); break;
                    case 0x40: BIT_b_r(B); break;
                    case 0x41: BIT_b_r(C); break;
                    case 0x42: BIT_b_r(D); break;
                    case 0x43: BIT_b_r(E); break;
                    case 0x44: BIT_b_r(H); break;
                    case 0x45: BIT_b_r(L); break;
                    case 0x46: BIT_b_rm(HL); break;
                    case 0x47: BIT_b_r(A); break;

                    case 0x80: RES_b_r(B); break;
                    case 0x81: RES_b_r(C); break;
                    case 0x82: RES_b_r(D); break;
                    case 0x83: RES_b_r(E); break;
                    case 0x84: RES_b_r(H); break;
                    case 0x85: RES_b_r(L); break;
                    case 0x86: RES_b_rm(HL); break;
                    case 0x87: RES_b_r(A); break;

                    case 0xC0: SET_b_r(B); break;
                    case 0xC1: SET_b_r(C); break;
                    case 0xC2: SET_b_r(D); break;
                    case 0xC3: SET_b_r(E); break;
                    case 0xC4: SET_b_r(H); break;
                    case 0xC5: SET_b_r(L); break;
                    case 0xC6: SET_b_rm(HL); break;
                    case 0xC7: SET_b_r(A); break;

                }
            }
        }

        /// <summary>
        /// Consumes CPU cycles.
        /// </summary>
        /// <param name="cycles">The number of cycles.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void ConsumeCycle(int cycles)
        {
            Debug.WriteLine("We should be consuming " + cycles + " cycles");
        }


        /// <summary>
        /// An instruction is not implemented.
        /// </summary>
        /// <param name="instruction">The instruction number.</param>
        /// <exception cref="System.NotImplementedException">Instruction  + instruction +  hasn't been implemented yet!</exception>
        private void NotImplemented(int instruction)
        {
            throw new NotImplementedException("Instruction " + instruction + " hasn't been implemented yet!");
        }

    }
}
