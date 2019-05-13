using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.MsilEngine
{
    public interface IMsilEngineService
    {
        IMsilReader MsilReader { get; }
        IMsilStack NewStack();
        IMsilLocals NewLocals();
        IMsilArguments Arguments { get; }
        IMsilTokenResolver TokenResolver { get; }
    }
    public interface IMsilOpCode { }

    public interface IMsilReader
    {
        int OpCodeValue { get; }
        bool ReadOpCode();
        OpCode Current { get; }
    }

    public interface IMsilLocals
    {

    }
    public interface IMsilArguments
    {

    }

    public interface IMsilEngine
    {
        IMsilReader OpCodeReader { get; }
        IMsilExecutionResult Result { get; }
        IMsilStack Stack { get; }
        IMsilLocals Locals { get; }
        IMsilArguments Arguments { get; }
        IMsilTokenResolver TokenResolver { get; }
        void Execute();
    }
    public interface IMsilExecutionResult
    {
        object ObjectResult { get; }
    }
    public interface IMsilExecutionResult<T> : IMsilExecutionResult
    {
    }
    public struct MsilExecutionResult : IMsilExecutionResult
    {
        public object ObjectResult { get;  }
    }
    public struct MsilExecuteResult<T> : IMsilExecutionResult<T>
    {
        public object ObjectResult => Result;
        T Result { get; set; }
    }
    public struct MsilVoidResult { }

    public class MsilEngine : IMsilEngine
    {
        public IMsilReader OpCodeReader { get; }
        public IMsilStack Stack { get; }
        public IMsilLocals Locals { get; }
        public IMsilArguments Arguments { get; }
        public IMsilTokenResolver TokenResolver { get; }
        public IMsilExecutionResult Result { get; private set; }

        public MsilEngine(IMsilEngineService service)
        {
            this.OpCodeReader = service.MsilReader;
            Stack = service.NewStack();
            Locals = service.NewLocals();
            Arguments = service.Arguments;
            TokenResolver = service.TokenResolver;
        }
        public void Execute()
        {

            while (OpCodeReader.ReadOpCode())
            {
                switch (OpCodeReader.OpCodeValue)
                {
                    case MsilOpCodes.Nop:// 0x00 (0)
                        OpCodeHandler<MsilOpCode.Nop>.Handle(this);
                        break;
                    case MsilOpCodes.Break:// 0x01 (1)
                        OpCodeHandler<MsilOpCode.Break>.Handle(this);
                        break;
                    case MsilOpCodes.Ldarg_0:// 0x02 (2)
                        OpCodeHandler<MsilOpCode.Ldarg_0>.Handle(this);
                        break;
                    case MsilOpCodes.Ldarg_1:// 0x03 (3)
                        OpCodeHandler<MsilOpCode.Ldarg_1>.Handle(this);
                        break;
                    case MsilOpCodes.Ldarg_2:// 0x04 (4)
                        OpCodeHandler<MsilOpCode.Ldarg_2>.Handle(this);
                        break;
                    case MsilOpCodes.Ldarg_3:// 0x05 (5)
                        OpCodeHandler<MsilOpCode.Ldarg_3>.Handle(this);
                        break;
                    case MsilOpCodes.Ldloc_0:// 0x06 (6)
                        OpCodeHandler<MsilOpCode.Ldloc_0>.Handle(this);
                        break;
                    case MsilOpCodes.Ldloc_1:// 0x07 (7)
                        OpCodeHandler<MsilOpCode.Ldloc_1>.Handle(this);
                        break;
                    case MsilOpCodes.Ldloc_2:// 0x08 (8)
                        OpCodeHandler<MsilOpCode.Ldloc_2>.Handle(this);
                        break;
                    case MsilOpCodes.Ldloc_3:// 0x09 (9)
                        OpCodeHandler<MsilOpCode.Ldloc_3>.Handle(this);
                        break;
                    case MsilOpCodes.Stloc_0:// 0x0A (10)
                        OpCodeHandler<MsilOpCode.Stloc_0>.Handle(this);
                        break;
                    case MsilOpCodes.Stloc_1:// 0x0B (11)
                        OpCodeHandler<MsilOpCode.Stloc_1>.Handle(this);
                        break;
                    case MsilOpCodes.Stloc_2:// 0x0C (12)
                        OpCodeHandler<MsilOpCode.Stloc_2>.Handle(this);
                        break;
                    case MsilOpCodes.Stloc_3:// 0x0D (13)
                        OpCodeHandler<MsilOpCode.Stloc_3>.Handle(this);
                        break;
                    case MsilOpCodes.Ldarg_S:// 0x0E (14)
                        OpCodeHandler<MsilOpCode.Ldarg_S>.Handle(this);
                        break;
                    case MsilOpCodes.Ldarga_S:// 0x0F (15)
                        OpCodeHandler<MsilOpCode.Ldarga_S>.Handle(this);
                        break;
                    case MsilOpCodes.Starg_S:// 0x10 (16)
                        OpCodeHandler<MsilOpCode.Starg_S>.Handle(this);
                        break;
                    case MsilOpCodes.Ldloc_S:// 0x11 (17)
                        OpCodeHandler<MsilOpCode.Ldloc_S>.Handle(this);
                        break;
                    case MsilOpCodes.Ldloca_S:// 0x12 (18)
                        OpCodeHandler<MsilOpCode.Ldloca_S>.Handle(this);
                        break;
                    case MsilOpCodes.Stloc_S:// 0x13 (19)
                        OpCodeHandler<MsilOpCode.Stloc_S>.Handle(this);
                        break;
                    case MsilOpCodes.Ldnull:// 0x14 (20)
                        OpCodeHandler<MsilOpCode.Ldnull>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_I4_M1:// 0x15 (21)
                        OpCodeHandler<MsilOpCode.Ldc_I4_M1>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_I4_0:// 0x16 (22)
                        OpCodeHandler<MsilOpCode.Ldc_I4_0>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_I4_1:// 0x17 (23)
                        OpCodeHandler<MsilOpCode.Ldc_I4_1>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_I4_2:// 0x18 (24)
                        OpCodeHandler<MsilOpCode.Ldc_I4_2>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_I4_3:// 0x19 (25)
                        OpCodeHandler<MsilOpCode.Ldc_I4_3>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_I4_4:// 0x1A (26)
                        OpCodeHandler<MsilOpCode.Ldc_I4_4>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_I4_5:// 0x1B (27)
                        OpCodeHandler<MsilOpCode.Ldc_I4_5>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_I4_6:// 0x1C (28)
                        OpCodeHandler<MsilOpCode.Ldc_I4_6>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_I4_7:// 0x1D (29)
                        OpCodeHandler<MsilOpCode.Ldc_I4_7>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_I4_8:// 0x1E (30)
                        OpCodeHandler<MsilOpCode.Ldc_I4_8>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_I4_S:// 0x1F (31)
                        OpCodeHandler<MsilOpCode.Ldc_I4_S>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_I4:// 0x20 (32)
                        OpCodeHandler<MsilOpCode.Ldc_I4>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_I8:// 0x21 (33)
                        OpCodeHandler<MsilOpCode.Ldc_I8>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_R4:// 0x22 (34)
                        OpCodeHandler<MsilOpCode.Ldc_R4>.Handle(this);
                        break;
                    case MsilOpCodes.Ldc_R8:// 0x23 (35)
                        OpCodeHandler<MsilOpCode.Ldc_R8>.Handle(this);
                        break;
                    case MsilOpCodes.Dup:// 0x25 (37)
                        OpCodeHandler<MsilOpCode.Dup>.Handle(this);
                        break;
                    case MsilOpCodes.Pop:// 0x26 (38)
                        OpCodeHandler<MsilOpCode.Pop>.Handle(this);
                        break;
                    case MsilOpCodes.Jmp:// 0x27 (39)
                        OpCodeHandler<MsilOpCode.Jmp>.Handle(this);
                        break;
                    case MsilOpCodes.Call:// 0x28 (40)
                        OpCodeHandler<MsilOpCode.Call>.Handle(this);
                        break;
                    case MsilOpCodes.Calli:// 0x29 (41)
                        OpCodeHandler<MsilOpCode.Calli>.Handle(this);
                        break;
                    case MsilOpCodes.Ret:// 0x2A (42)
                        OpCodeHandler<MsilOpCode.Ret>.Handle(this);
                        return;
                    case MsilOpCodes.Br_S:// 0x2B (43)
                        OpCodeHandler<MsilOpCode.Br_S>.Handle(this);
                        break;
                    case MsilOpCodes.Brfalse_S:// 0x2C (44)
                        OpCodeHandler<MsilOpCode.Brfalse_S>.Handle(this);
                        break;
                    case MsilOpCodes.Brtrue_S:// 0x2D (45)
                        OpCodeHandler<MsilOpCode.Brtrue_S>.Handle(this);
                        break;
                    case MsilOpCodes.Beq_S:// 0x2E (46)
                        OpCodeHandler<MsilOpCode.Beq_S>.Handle(this);
                        break;
                    case MsilOpCodes.Bge_S:// 0x2F (47)
                        OpCodeHandler<MsilOpCode.Bge_S>.Handle(this);
                        break;
                    case MsilOpCodes.Bgt_S:// 0x30 (48)
                        OpCodeHandler<MsilOpCode.Bgt_S>.Handle(this);
                        break;
                    case MsilOpCodes.Ble_S:// 0x31 (49)
                        OpCodeHandler<MsilOpCode.Ble_S>.Handle(this);
                        break;
                    case MsilOpCodes.Blt_S:// 0x32 (50)
                        OpCodeHandler<MsilOpCode.Blt_S>.Handle(this);
                        break;
                    case MsilOpCodes.Bne_Un_S:// 0x33 (51)
                        OpCodeHandler<MsilOpCode.Bne_Un_S>.Handle(this);
                        break;
                    case MsilOpCodes.Bge_Un_S:// 0x34 (52)
                        OpCodeHandler<MsilOpCode.Bge_Un_S>.Handle(this);
                        break;
                    case MsilOpCodes.Bgt_Un_S:// 0x35 (53)
                        OpCodeHandler<MsilOpCode.Bgt_Un_S>.Handle(this);
                        break;
                    case MsilOpCodes.Ble_Un_S:// 0x36 (54)
                        OpCodeHandler<MsilOpCode.Ble_Un_S>.Handle(this);
                        break;
                    case MsilOpCodes.Blt_Un_S:// 0x37 (55)
                        OpCodeHandler<MsilOpCode.Blt_Un_S>.Handle(this);
                        break;
                    case MsilOpCodes.Br:// 0x38 (56)
                        OpCodeHandler<MsilOpCode.Br>.Handle(this);
                        break;
                    case MsilOpCodes.Brfalse:// 0x39 (57)
                        OpCodeHandler<MsilOpCode.Brfalse>.Handle(this);
                        break;
                    case MsilOpCodes.Brtrue:// 0x3A (58)
                        OpCodeHandler<MsilOpCode.Brtrue>.Handle(this);
                        break;
                    case MsilOpCodes.Beq:// 0x3B (59)
                        OpCodeHandler<MsilOpCode.Beq>.Handle(this);
                        break;
                    case MsilOpCodes.Bge:// 0x3C (60)
                        OpCodeHandler<MsilOpCode.Bge>.Handle(this);
                        break;
                    case MsilOpCodes.Bgt:// 0x3D (61)
                        OpCodeHandler<MsilOpCode.Bgt>.Handle(this);
                        break;
                    case MsilOpCodes.Ble:// 0x3E (62)
                        OpCodeHandler<MsilOpCode.Ble>.Handle(this);
                        break;
                    case MsilOpCodes.Blt:// 0x3F (63)
                        OpCodeHandler<MsilOpCode.Blt>.Handle(this);
                        break;
                    case MsilOpCodes.Bne_Un:// 0x40 (64)
                        OpCodeHandler<MsilOpCode.Bne_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Bge_Un:// 0x41 (65)
                        OpCodeHandler<MsilOpCode.Bge_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Bgt_Un:// 0x42 (66)
                        OpCodeHandler<MsilOpCode.Bgt_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Ble_Un:// 0x43 (67)
                        OpCodeHandler<MsilOpCode.Ble_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Blt_Un:// 0x44 (68)
                        OpCodeHandler<MsilOpCode.Blt_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Switch:// 0x45 (69)
                        OpCodeHandler<MsilOpCode.Switch>.Handle(this);
                        break;
                    case MsilOpCodes.Ldind_I1:// 0x46 (70)
                        OpCodeHandler<MsilOpCode.Ldind_I1>.Handle(this);
                        break;
                    case MsilOpCodes.Ldind_U1:// 0x47 (71)
                        OpCodeHandler<MsilOpCode.Ldind_U1>.Handle(this);
                        break;
                    case MsilOpCodes.Ldind_I2:// 0x48 (72)
                        OpCodeHandler<MsilOpCode.Ldind_I2>.Handle(this);
                        break;
                    case MsilOpCodes.Ldind_U2:// 0x49 (73)
                        OpCodeHandler<MsilOpCode.Ldind_U2>.Handle(this);
                        break;
                    case MsilOpCodes.Ldind_I4:// 0x4A (74)
                        OpCodeHandler<MsilOpCode.Ldind_I4>.Handle(this);
                        break;
                    case MsilOpCodes.Ldind_U4:// 0x4B (75)
                        OpCodeHandler<MsilOpCode.Ldind_U4>.Handle(this);
                        break;
                    case MsilOpCodes.Ldind_I8:// 0x4C (76)
                        OpCodeHandler<MsilOpCode.Ldind_I8>.Handle(this);
                        break;
                    case MsilOpCodes.Ldind_I:// 0x4D (77)
                        OpCodeHandler<MsilOpCode.Ldind_I>.Handle(this);
                        break;
                    case MsilOpCodes.Ldind_R4:// 0x4E (78)
                        OpCodeHandler<MsilOpCode.Ldind_R4>.Handle(this);
                        break;
                    case MsilOpCodes.Ldind_R8:// 0x4F (79)
                        OpCodeHandler<MsilOpCode.Ldind_R8>.Handle(this);
                        break;
                    case MsilOpCodes.Ldind_Ref:// 0x50 (80)
                        OpCodeHandler<MsilOpCode.Ldind_Ref>.Handle(this);
                        break;
                    case MsilOpCodes.Stind_Ref:// 0x51 (81)
                        OpCodeHandler<MsilOpCode.Stind_Ref>.Handle(this);
                        break;
                    case MsilOpCodes.Stind_I1:// 0x52 (82)
                        OpCodeHandler<MsilOpCode.Stind_I1>.Handle(this);
                        break;
                    case MsilOpCodes.Stind_I2:// 0x53 (83)
                        OpCodeHandler<MsilOpCode.Stind_I2>.Handle(this);
                        break;
                    case MsilOpCodes.Stind_I4:// 0x54 (84)
                        OpCodeHandler<MsilOpCode.Stind_I4>.Handle(this);
                        break;
                    case MsilOpCodes.Stind_I8:// 0x55 (85)
                        OpCodeHandler<MsilOpCode.Stind_I8>.Handle(this);
                        break;
                    case MsilOpCodes.Stind_R4:// 0x56 (86)
                        OpCodeHandler<MsilOpCode.Stind_R4>.Handle(this);
                        break;
                    case MsilOpCodes.Stind_R8:// 0x57 (87)
                        OpCodeHandler<MsilOpCode.Stind_R8>.Handle(this);
                        break;
                    case MsilOpCodes.Add:// 0x58 (88)
                        OpCodeHandler<MsilOpCode.Add>.Handle(this);
                        break;
                    case MsilOpCodes.Sub:// 0x59 (89)
                        OpCodeHandler<MsilOpCode.Sub>.Handle(this);
                        break;
                    case MsilOpCodes.Mul:// 0x5A (90)
                        OpCodeHandler<MsilOpCode.Mul>.Handle(this);
                        break;
                    case MsilOpCodes.Div:// 0x5B (91)
                        OpCodeHandler<MsilOpCode.Div>.Handle(this);
                        break;
                    case MsilOpCodes.Div_Un:// 0x5C (92)
                        OpCodeHandler<MsilOpCode.Div_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Rem:// 0x5D (93)
                        OpCodeHandler<MsilOpCode.Rem>.Handle(this);
                        break;
                    case MsilOpCodes.Rem_Un:// 0x5E (94)
                        OpCodeHandler<MsilOpCode.Rem_Un>.Handle(this);
                        break;
                    case MsilOpCodes.And:// 0x5F (95)
                        OpCodeHandler<MsilOpCode.And>.Handle(this);
                        break;
                    case MsilOpCodes.Or:// 0x60 (96)
                        OpCodeHandler<MsilOpCode.Or>.Handle(this);
                        break;
                    case MsilOpCodes.Xor:// 0x61 (97)
                        OpCodeHandler<MsilOpCode.Xor>.Handle(this);
                        break;
                    case MsilOpCodes.Shl:// 0x62 (98)
                        OpCodeHandler<MsilOpCode.Shl>.Handle(this);
                        break;
                    case MsilOpCodes.Shr:// 0x63 (99)
                        OpCodeHandler<MsilOpCode.Shr>.Handle(this);
                        break;
                    case MsilOpCodes.Shr_Un:// 0x64 (100)
                        OpCodeHandler<MsilOpCode.Shr_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Neg:// 0x65 (101)
                        OpCodeHandler<MsilOpCode.Neg>.Handle(this);
                        break;
                    case MsilOpCodes.Not:// 0x66 (102)
                        OpCodeHandler<MsilOpCode.Not>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_I1:// 0x67 (103)
                        OpCodeHandler<MsilOpCode.Conv_I1>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_I2:// 0x68 (104)
                        OpCodeHandler<MsilOpCode.Conv_I2>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_I4:// 0x69 (105)
                        OpCodeHandler<MsilOpCode.Conv_I4>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_I8:// 0x6A (106)
                        OpCodeHandler<MsilOpCode.Conv_I8>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_R4:// 0x6B (107)
                        OpCodeHandler<MsilOpCode.Conv_R4>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_R8:// 0x6C (108)
                        OpCodeHandler<MsilOpCode.Conv_R8>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_U4:// 0x6D (109)
                        OpCodeHandler<MsilOpCode.Conv_U4>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_U8:// 0x6E (110)
                        OpCodeHandler<MsilOpCode.Conv_U8>.Handle(this);
                        break;
                    case MsilOpCodes.Callvirt:// 0x6F (111)
                        OpCodeHandler<MsilOpCode.Callvirt>.Handle(this);
                        break;
                    case MsilOpCodes.Cpobj:// 0x70 (112)
                        OpCodeHandler<MsilOpCode.Cpobj>.Handle(this);
                        break;
                    case MsilOpCodes.Ldobj:// 0x71 (113)
                        OpCodeHandler<MsilOpCode.Ldobj>.Handle(this);
                        break;
                    case MsilOpCodes.Ldstr:// 0x72 (114)
                        OpCodeHandler<MsilOpCode.Ldstr>.Handle(this);
                        break;
                    case MsilOpCodes.Newobj:// 0x73 (115)
                        OpCodeHandler<MsilOpCode.Newobj>.Handle(this);
                        break;
                    case MsilOpCodes.Castclass:// 0x74 (116)
                        OpCodeHandler<MsilOpCode.Castclass>.Handle(this);
                        break;
                    case MsilOpCodes.Isinst:// 0x75 (117)
                        OpCodeHandler<MsilOpCode.Isinst>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_R_Un:// 0x76 (118)
                        OpCodeHandler<MsilOpCode.Conv_R_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Unbox:// 0x79 (121)
                        OpCodeHandler<MsilOpCode.Unbox>.Handle(this);
                        break;
                    case MsilOpCodes.Throw:// 0x7A (122)
                        OpCodeHandler<MsilOpCode.Throw>.Handle(this);
                        break;
                    case MsilOpCodes.Ldfld:// 0x7B (123)
                        OpCodeHandler<MsilOpCode.Ldfld>.Handle(this);
                        break;
                    case MsilOpCodes.Ldflda:// 0x7C (124)
                        OpCodeHandler<MsilOpCode.Ldflda>.Handle(this);
                        break;
                    case MsilOpCodes.Stfld:// 0x7D (125)
                        OpCodeHandler<MsilOpCode.Stfld>.Handle(this);
                        break;
                    case MsilOpCodes.Ldsfld:// 0x7E (126)
                        OpCodeHandler<MsilOpCode.Ldsfld>.Handle(this);
                        break;
                    case MsilOpCodes.Ldsflda:// 0x7F (127)
                        OpCodeHandler<MsilOpCode.Ldsflda>.Handle(this);
                        break;
                    case MsilOpCodes.Stsfld:// 0x80 (128)
                        OpCodeHandler<MsilOpCode.Stsfld>.Handle(this);
                        break;
                    case MsilOpCodes.Stobj:// 0x81 (129)
                        OpCodeHandler<MsilOpCode.Stobj>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_I1_Un:// 0x82 (130)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_I1_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_I2_Un:// 0x83 (131)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_I2_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_I4_Un:// 0x84 (132)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_I4_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_I8_Un:// 0x85 (133)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_I8_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_U1_Un:// 0x86 (134)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_U1_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_U2_Un:// 0x87 (135)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_U2_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_U4_Un:// 0x88 (136)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_U4_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_U8_Un:// 0x89 (137)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_U8_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_I_Un:// 0x8A (138)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_I_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_U_Un:// 0x8B (139)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_U_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Box:// 0x8C (140)
                        OpCodeHandler<MsilOpCode.Box>.Handle(this);
                        break;
                    case MsilOpCodes.Newarr:// 0x8D (141)
                        OpCodeHandler<MsilOpCode.Newarr>.Handle(this);
                        break;
                    case MsilOpCodes.Ldlen:// 0x8E (142)
                        OpCodeHandler<MsilOpCode.Ldlen>.Handle(this);
                        break;
                    case MsilOpCodes.Ldelema:// 0x8F (143)
                        OpCodeHandler<MsilOpCode.Ldelema>.Handle(this);
                        break;
                    case MsilOpCodes.Ldelem_I1:// 0x90 (144)
                        OpCodeHandler<MsilOpCode.Ldelem_I1>.Handle(this);
                        break;
                    case MsilOpCodes.Ldelem_U1:// 0x91 (145)
                        OpCodeHandler<MsilOpCode.Ldelem_U1>.Handle(this);
                        break;
                    case MsilOpCodes.Ldelem_I2:// 0x92 (146)
                        OpCodeHandler<MsilOpCode.Ldelem_I2>.Handle(this);
                        break;
                    case MsilOpCodes.Ldelem_U2:// 0x93 (147)
                        OpCodeHandler<MsilOpCode.Ldelem_U2>.Handle(this);
                        break;
                    case MsilOpCodes.Ldelem_I4:// 0x94 (148)
                        OpCodeHandler<MsilOpCode.Ldelem_I4>.Handle(this);
                        break;
                    case MsilOpCodes.Ldelem_U4:// 0x95 (149)
                        OpCodeHandler<MsilOpCode.Ldelem_U4>.Handle(this);
                        break;
                    case MsilOpCodes.Ldelem_I8:// 0x96 (150)
                        OpCodeHandler<MsilOpCode.Ldelem_I8>.Handle(this);
                        break;
                    case MsilOpCodes.Ldelem_I:// 0x97 (151)
                        OpCodeHandler<MsilOpCode.Ldelem_I>.Handle(this);
                        break;
                    case MsilOpCodes.Ldelem_R4:// 0x98 (152)
                        OpCodeHandler<MsilOpCode.Ldelem_R4>.Handle(this);
                        break;
                    case MsilOpCodes.Ldelem_R8:// 0x99 (153)
                        OpCodeHandler<MsilOpCode.Ldelem_R8>.Handle(this);
                        break;
                    case MsilOpCodes.Ldelem_Ref:// 0x9A (154)
                        OpCodeHandler<MsilOpCode.Ldelem_Ref>.Handle(this);
                        break;
                    case MsilOpCodes.Stelem_I:// 0x9B (155)
                        OpCodeHandler<MsilOpCode.Stelem_I>.Handle(this);
                        break;
                    case MsilOpCodes.Stelem_I1:// 0x9C (156)
                        OpCodeHandler<MsilOpCode.Stelem_I1>.Handle(this);
                        break;
                    case MsilOpCodes.Stelem_I2:// 0x9D (157)
                        OpCodeHandler<MsilOpCode.Stelem_I2>.Handle(this);
                        break;
                    case MsilOpCodes.Stelem_I4:// 0x9E (158)
                        OpCodeHandler<MsilOpCode.Stelem_I4>.Handle(this);
                        break;
                    case MsilOpCodes.Stelem_I8:// 0x9F (159)
                        OpCodeHandler<MsilOpCode.Stelem_I8>.Handle(this);
                        break;
                    case MsilOpCodes.Stelem_R4:// 0xA0 (160)
                        OpCodeHandler<MsilOpCode.Stelem_R4>.Handle(this);
                        break;
                    case MsilOpCodes.Stelem_R8:// 0xA1 (161)
                        OpCodeHandler<MsilOpCode.Stelem_R8>.Handle(this);
                        break;
                    case MsilOpCodes.Stelem_Ref:// 0xA2 (162)
                        OpCodeHandler<MsilOpCode.Stelem_Ref>.Handle(this);
                        break;
                    case MsilOpCodes.Ldelem:// 0xA3 (163)
                        OpCodeHandler<MsilOpCode.Ldelem>.Handle(this);
                        break;
                    case MsilOpCodes.Stelem:// 0xA4 (164)
                        OpCodeHandler<MsilOpCode.Stelem>.Handle(this);
                        break;
                    case MsilOpCodes.Unbox_Any:// 0xA5 (165)
                        OpCodeHandler<MsilOpCode.Unbox_Any>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_I1:// 0xB3 (179)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_I1>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_U1:// 0xB4 (180)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_U1>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_I2:// 0xB5 (181)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_I2>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_U2:// 0xB6 (182)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_U2>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_I4:// 0xB7 (183)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_I4>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_U4:// 0xB8 (184)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_U4>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_I8:// 0xB9 (185)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_I8>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_U8:// 0xBA (186)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_U8>.Handle(this);
                        break;
                    case MsilOpCodes.Refanyval:// 0xC2 (194)
                        OpCodeHandler<MsilOpCode.Refanyval>.Handle(this);
                        break;
                    case MsilOpCodes.Ckfinite:// 0xC3 (195)
                        OpCodeHandler<MsilOpCode.Ckfinite>.Handle(this);
                        break;
                    case MsilOpCodes.Mkrefany:// 0xC6 (198)
                        OpCodeHandler<MsilOpCode.Mkrefany>.Handle(this);
                        break;
                    case MsilOpCodes.Ldtoken:// 0xD0 (208)
                        OpCodeHandler<MsilOpCode.Ldtoken>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_U2:// 0xD1 (209)
                        OpCodeHandler<MsilOpCode.Conv_U2>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_U1:// 0xD2 (210)
                        OpCodeHandler<MsilOpCode.Conv_U1>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_I:// 0xD3 (211)
                        OpCodeHandler<MsilOpCode.Conv_I>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_I:// 0xD4 (212)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_I>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_Ovf_U:// 0xD5 (213)
                        OpCodeHandler<MsilOpCode.Conv_Ovf_U>.Handle(this);
                        break;
                    case MsilOpCodes.Add_Ovf:// 0xD6 (214)
                        OpCodeHandler<MsilOpCode.Add_Ovf>.Handle(this);
                        break;
                    case MsilOpCodes.Add_Ovf_Un:// 0xD7 (215)
                        OpCodeHandler<MsilOpCode.Add_Ovf_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Mul_Ovf:// 0xD8 (216)
                        OpCodeHandler<MsilOpCode.Mul_Ovf>.Handle(this);
                        break;
                    case MsilOpCodes.Mul_Ovf_Un:// 0xD9 (217)
                        OpCodeHandler<MsilOpCode.Mul_Ovf_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Sub_Ovf:// 0xDA (218)
                        OpCodeHandler<MsilOpCode.Sub_Ovf>.Handle(this);
                        break;
                    case MsilOpCodes.Sub_Ovf_Un:// 0xDB (219)
                        OpCodeHandler<MsilOpCode.Sub_Ovf_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Endfinally:// 0xDC (220)
                        OpCodeHandler<MsilOpCode.Endfinally>.Handle(this);
                        break;
                    case MsilOpCodes.Leave:// 0xDD (221)
                        OpCodeHandler<MsilOpCode.Leave>.Handle(this);
                        break;
                    case MsilOpCodes.Leave_S:// 0xDE (222)
                        OpCodeHandler<MsilOpCode.Leave_S>.Handle(this);
                        break;
                    case MsilOpCodes.Stind_I:// 0xDF (223)
                        OpCodeHandler<MsilOpCode.Stind_I>.Handle(this);
                        break;
                    case MsilOpCodes.Conv_U:// 0xE0 (224)
                        OpCodeHandler<MsilOpCode.Conv_U>.Handle(this);
                        break;
                    case MsilOpCodes.Prefix7:// 0xF8 (248)
                        OpCodeHandler<MsilOpCode.Prefix7>.Handle(this);
                        break;
                    case MsilOpCodes.Prefix6:// 0xF9 (249)
                        OpCodeHandler<MsilOpCode.Prefix6>.Handle(this);
                        break;
                    case MsilOpCodes.Prefix5:// 0xFA (250)
                        OpCodeHandler<MsilOpCode.Prefix5>.Handle(this);
                        break;
                    case MsilOpCodes.Prefix4:// 0xFB (251)
                        OpCodeHandler<MsilOpCode.Prefix4>.Handle(this);
                        break;
                    case MsilOpCodes.Prefix3:// 0xFC (252)
                        OpCodeHandler<MsilOpCode.Prefix3>.Handle(this);
                        break;
                    case MsilOpCodes.Prefix2:// 0xFD (253)
                        OpCodeHandler<MsilOpCode.Prefix2>.Handle(this);
                        break;
                    case MsilOpCodes.Prefix1:// 0xFE (254)
                        OpCodeHandler<MsilOpCode.Prefix1>.Handle(this);
                        break;
                    case MsilOpCodes.Prefixref:// 0xFF (255)
                        OpCodeHandler<MsilOpCode.Prefixref>.Handle(this);
                        break;
                    case MsilOpCodes.Arglist:// 0xFE00 (65024 - -512)
                        OpCodeHandler<MsilOpCode.Arglist>.Handle(this);
                        break;
                    case MsilOpCodes.Ceq:// 0xFE01 (65025 - -511)
                        OpCodeHandler<MsilOpCode.Ceq>.Handle(this);
                        break;
                    case MsilOpCodes.Cgt:// 0xFE02 (65026 - -510)
                        OpCodeHandler<MsilOpCode.Cgt>.Handle(this);
                        break;
                    case MsilOpCodes.Cgt_Un:// 0xFE03 (65027 - -509)
                        OpCodeHandler<MsilOpCode.Cgt_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Clt:// 0xFE04 (65028 - -508)
                        OpCodeHandler<MsilOpCode.Clt>.Handle(this);
                        break;
                    case MsilOpCodes.Clt_Un:// 0xFE05 (65029 - -507)
                        OpCodeHandler<MsilOpCode.Clt_Un>.Handle(this);
                        break;
                    case MsilOpCodes.Ldftn:// 0xFE06 (65030 - -506)
                        OpCodeHandler<MsilOpCode.Ldftn>.Handle(this);
                        break;
                    case MsilOpCodes.Ldvirtftn:// 0xFE07 (65031 - -505)
                        OpCodeHandler<MsilOpCode.Ldvirtftn>.Handle(this);
                        break;
                    case MsilOpCodes.Ldarg:// 0xFE09 (65033 - -503)
                        OpCodeHandler<MsilOpCode.Ldarg>.Handle(this);
                        break;
                    case MsilOpCodes.Ldarga:// 0xFE0A (65034 - -502)
                        OpCodeHandler<MsilOpCode.Ldarga>.Handle(this);
                        break;
                    case MsilOpCodes.Starg:// 0xFE0B (65035 - -501)
                        OpCodeHandler<MsilOpCode.Starg>.Handle(this);
                        break;
                    case MsilOpCodes.Ldloc:// 0xFE0C (65036 - -500)
                        OpCodeHandler<MsilOpCode.Ldloc>.Handle(this);
                        break;
                    case MsilOpCodes.Ldloca:// 0xFE0D (65037 - -499)
                        OpCodeHandler<MsilOpCode.Ldloca>.Handle(this);
                        break;
                    case MsilOpCodes.Stloc:// 0xFE0E (65038 - -498)
                        OpCodeHandler<MsilOpCode.Stloc>.Handle(this);
                        break;
                    case MsilOpCodes.Localloc:// 0xFE0F (65039 - -497)
                        OpCodeHandler<MsilOpCode.Localloc>.Handle(this);
                        break;
                    case MsilOpCodes.Endfilter:// 0xFE11 (65041 - -495)
                        OpCodeHandler<MsilOpCode.Endfilter>.Handle(this);
                        break;
                    case MsilOpCodes.Unaligned:// 0xFE12 (65042 - -494)
                        OpCodeHandler<MsilOpCode.Unaligned>.Handle(this);
                        break;
                    case MsilOpCodes.Volatile:// 0xFE13 (65043 - -493)
                        OpCodeHandler<MsilOpCode.Volatile>.Handle(this);
                        break;
                    case MsilOpCodes.Tailcall:// 0xFE14 (65044 - -492)
                        OpCodeHandler<MsilOpCode.Tailcall>.Handle(this);
                        break;
                    case MsilOpCodes.Initobj:// 0xFE15 (65045 - -491)
                        OpCodeHandler<MsilOpCode.Initobj>.Handle(this);
                        break;
                    case MsilOpCodes.Constrained:// 0xFE16 (65046 - -490)
                        OpCodeHandler<MsilOpCode.Constrained>.Handle(this);
                        break;
                    case MsilOpCodes.Cpblk:// 0xFE17 (65047 - -489)
                        OpCodeHandler<MsilOpCode.Cpblk>.Handle(this);
                        break;
                    case MsilOpCodes.Initblk:// 0xFE18 (65048 - -488)
                        OpCodeHandler<MsilOpCode.Initblk>.Handle(this);
                        break;
                    case MsilOpCodes.Rethrow:// 0xFE1A (65050 - -486)
                        OpCodeHandler<MsilOpCode.Rethrow>.Handle(this);
                        break;
                    case MsilOpCodes.Sizeof:// 0xFE1C (65052 - -484)
                        OpCodeHandler<MsilOpCode.Sizeof>.Handle(this);
                        break;
                    case MsilOpCodes.Refanytype:// 0xFE1D (65053 - -483)
                        OpCodeHandler<MsilOpCode.Refanytype>.Handle(this);
                        break;
                    case MsilOpCodes.Readonly:// 0xFE1E (65054 - -482)
                        OpCodeHandler<MsilOpCode.Readonly>.Handle(this);
                        break;
                    case -1:
                        throw new InvalidProgramException("Reached end of instructions without a return statement");
                    default:
                        throw new NotImplementedException();
                }

            }
        }


    }

    public struct OpCodeHandler<T>
    {
        public static Action<IMsilEngine> Handle = OpCodeHandlers<T>.GetHandler();
    }
    public class OpCodeHandlers<T>
    {
        private static Action<IMsilEngine> NotImplemented => (engine) => throw new NotImplementedException();
        internal static Action<IMsilEngine> GetHandler()
        {
            var handlerType = typeof(T);
            switch (handlerType.Name)
            {
                default:
                    return NotImplemented;
            }

        }
    }
}
