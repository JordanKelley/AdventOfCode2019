using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Days.Five
{
    public class IntcodeInstruction
    {
        public OpcodeEnum Opcode { get; }
        public ParameterModesEnum FirstParameterMode { get; }
        public ParameterModesEnum SecondParameterMode { get; }
        public ParameterModesEnum ThirdParameterMode { get; }
        public int IndexesToMoveForNextInstruction { get; set; }
        public int IndexForNextInstruction { get; set; }
        public bool UseIndexForNextInstruction { get; set; }

        public IntcodeInstruction(OpcodeEnum opcode, ParameterModesEnum firstParameterMode, ParameterModesEnum secondParameterMode, ParameterModesEnum thirdParameterMode)
        {
            Opcode = opcode;
            FirstParameterMode = firstParameterMode;
            SecondParameterMode = secondParameterMode;
            ThirdParameterMode = thirdParameterMode;
        }

        //public int IndexesToMoveForNextInstruction()
        //{
        //    switch(Opcode)
        //    {
        //        case OpcodeEnum.Add:
        //        case OpcodeEnum.Multiply:
        //            return 4;
        //        case OpcodeEnum.Input:
        //        case OpcodeEnum.Output:
        //            return 2;
        //        case OpcodeEnum.EndProgram:
        //            return 0;
        //        default:
        //            throw new Exception("Invalid opcode");
        //    }
        //}
    }
}
