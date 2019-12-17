using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Days.Seven
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
    }
}
