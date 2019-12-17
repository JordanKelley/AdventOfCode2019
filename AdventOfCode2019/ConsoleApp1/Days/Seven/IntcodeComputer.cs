using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Days.Seven
{
    public class IntcodeComputer
    {
        private int[] Intcode;
        public List<int> OutputData;
        private int InputValue;
        private int SecondInputValue;
        private int InputIterations = 0;
        public IntcodeComputer(int[] intCode, int inputValue, int secondInputValue)
        {
            Intcode = intCode;
            OutputData = new List<int>();
            InputValue = inputValue;
            SecondInputValue = secondInputValue;

            InterpretIntCode();
        }

        private void InterpretIntCode()
        {
            int count = 0;

            while (count < Intcode.Length)
            {
                if (Intcode[count] == 99)
                {
                    return;
                }

                IntcodeInstruction instructions = GetInstructions(Intcode[count]);

                ProcessInstructions(instructions, count);

                if (instructions.UseIndexForNextInstruction)
                {
                    count = instructions.IndexForNextInstruction;
                }
                else
                {
                    count += instructions.IndexesToMoveForNextInstruction;
                }
            }
        }

        private static IntcodeInstruction GetInstructions(int input)
        {
            string inputStr = input.ToString();

            int lengthOfInput = inputStr.Length;

            IntcodeInstruction instructions;

            // I'd prefer a more robust solution than what is below
            OpcodeEnum opcode;
            ParameterModesEnum firstParameterMode;
            ParameterModesEnum secondParameterMode;
            ParameterModesEnum thirdParameterMode;
            switch (lengthOfInput)
            {
                case 1:
                    opcode = GetOpcode(input);
                    instructions = new IntcodeInstruction(opcode, ParameterModesEnum.Position, ParameterModesEnum.Position, ParameterModesEnum.Position);
                    return instructions;
                case 3:
                    opcode = GetOpcode(Int32.Parse(inputStr[2].ToString()));
                    firstParameterMode = GetParameter(Int32.Parse(inputStr[0].ToString()));
                    instructions = new IntcodeInstruction(opcode, firstParameterMode, ParameterModesEnum.Position, ParameterModesEnum.Position);
                    return instructions;
                case 4:
                    opcode = GetOpcode(Int32.Parse(inputStr[3].ToString()));
                    firstParameterMode = GetParameter(Int32.Parse(inputStr[1].ToString()));
                    secondParameterMode = GetParameter(Int32.Parse(inputStr[0].ToString()));
                    instructions = new IntcodeInstruction(opcode, firstParameterMode, secondParameterMode, ParameterModesEnum.Position);
                    return instructions;
                case 5:
                    opcode = GetOpcode(Int32.Parse(inputStr[4].ToString()));
                    firstParameterMode = GetParameter(Int32.Parse(inputStr[2].ToString()));
                    secondParameterMode = GetParameter(Int32.Parse(inputStr[1].ToString()));
                    thirdParameterMode = GetParameter(Int32.Parse(inputStr[0].ToString()));
                    instructions = new IntcodeInstruction(opcode, firstParameterMode, secondParameterMode, thirdParameterMode);
                    return instructions;
                default:
                    throw new Exception("Invalid lenght of instructions");
            }
        }

        private void ProcessInstructions(IntcodeInstruction instructions, int currentIndex)
        {
            // strategy pattern would be cleaner
            switch (instructions.Opcode)
            {
                case OpcodeEnum.Add:
                    AddBatchOfIntcode(currentIndex, instructions);
                    break;
                case OpcodeEnum.Multiply:
                    MultiplyBatchOfIntcode(currentIndex, instructions);
                    break;
                case OpcodeEnum.Input:
                    InputBatchOfIntcode(currentIndex, instructions);
                    break;
                case OpcodeEnum.Output:
                    OutputBatchOfIntcode(currentIndex, instructions);
                    break;
                case OpcodeEnum.JumpIfTrue:
                    JumpIfTrueBatchOfIntcode(currentIndex, instructions);
                    break;
                case OpcodeEnum.JumpIfFalse:
                    JumpIfFalseBatchOfIntcode(currentIndex, instructions);
                    break;
                case OpcodeEnum.LessThan:
                    LessThanBatchOfIntcode(currentIndex, instructions);
                    break;
                case OpcodeEnum.Equals:
                    EqualsBatchOfIntcode(currentIndex, instructions);
                    break;
                case OpcodeEnum.EndProgram:
                    break;
                default:
                    throw new Exception("Invalid opcode");
            }
        }

        private void AddBatchOfIntcode(int index, IntcodeInstruction instructions)
        {
            int firstAddNum = instructions.FirstParameterMode == ParameterModesEnum.Position ? Intcode[Intcode[index + 1]] : Intcode[index + 1];
            int secondAddNum = instructions.SecondParameterMode == ParameterModesEnum.Position ? Intcode[Intcode[index + 2]] : Intcode[index + 2];

            int sum = firstAddNum + secondAddNum;

            Intcode[Intcode[index + 3]] = sum;
            instructions.IndexesToMoveForNextInstruction = 4;
        }

        private void MultiplyBatchOfIntcode(int index, IntcodeInstruction instructions)
        {
            int firstMultNum = instructions.FirstParameterMode == ParameterModesEnum.Position ? Intcode[Intcode[index + 1]] : Intcode[index + 1];
            int secondMultNum = instructions.SecondParameterMode == ParameterModesEnum.Position ? Intcode[Intcode[index + 2]] : Intcode[index + 2];
            int product = firstMultNum * secondMultNum;

            Intcode[Intcode[index + 3]] = product;
            instructions.IndexesToMoveForNextInstruction = 4;
        }

        private void InputBatchOfIntcode(int index, IntcodeInstruction instructions)
        {
            InputIterations++;
            int saveToPosition = Intcode[index + 1];
            Intcode[saveToPosition] = InputIterations > 1 ? SecondInputValue : InputValue;
            instructions.IndexesToMoveForNextInstruction = 2;
        }

        private void OutputBatchOfIntcode(int index, IntcodeInstruction instructions)
        {
            int outputValue;
            if (instructions.FirstParameterMode == ParameterModesEnum.Position)
            {
                outputValue = Intcode[Intcode[index + 1]];
            }
            else
            {
                outputValue = Intcode[index + 1];
            }
            OutputData.Add(outputValue);
            instructions.IndexesToMoveForNextInstruction = 2;
        }

        public void JumpIfTrueBatchOfIntcode(int index, IntcodeInstruction instructions)
        {
            int firstParam = instructions.FirstParameterMode == ParameterModesEnum.Immediate ? Intcode[index + 1] : Intcode[Intcode[index + 1]];
            int secondParam = instructions.SecondParameterMode == ParameterModesEnum.Immediate ? Intcode[index + 2] : Intcode[Intcode[index + 2]];
            if (firstParam != 0)
            {
                Intcode[index] = secondParam;
                instructions.IndexForNextInstruction = secondParam;
                instructions.UseIndexForNextInstruction = true;
            }
            else
            {
                instructions.IndexesToMoveForNextInstruction = 3;
            }
        }

        public void JumpIfFalseBatchOfIntcode(int index, IntcodeInstruction instructions)
        {
            int firstParam = instructions.FirstParameterMode == ParameterModesEnum.Immediate ? Intcode[index + 1] : Intcode[Intcode[index + 1]];
            int secondParam = instructions.SecondParameterMode == ParameterModesEnum.Immediate ? Intcode[index + 2] : Intcode[Intcode[index + 2]];

            if (firstParam == 0)
            {
                Intcode[index] = secondParam;
                instructions.IndexForNextInstruction = secondParam;
                instructions.UseIndexForNextInstruction = true;
            }
            else
            {
                instructions.IndexesToMoveForNextInstruction = 3;
            }
        }

        public void LessThanBatchOfIntcode(int index, IntcodeInstruction instructions)
        {
            int firstParam = instructions.FirstParameterMode == ParameterModesEnum.Immediate ? Intcode[index + 1] : Intcode[Intcode[index + 1]];
            int secondParam = instructions.SecondParameterMode == ParameterModesEnum.Immediate ? Intcode[index + 2] : Intcode[Intcode[index + 2]];
            int thirdParam = Intcode[index + 3];

            if (firstParam < secondParam)
            {
                Intcode[thirdParam] = 1;
            }
            else
            {
                Intcode[thirdParam] = 0;
            }
            instructions.IndexesToMoveForNextInstruction = 4;
        }

        private void EqualsBatchOfIntcode(int index, IntcodeInstruction instructions)
        {
            int firstParam = instructions.FirstParameterMode == ParameterModesEnum.Immediate ? Intcode[index + 1] : Intcode[Intcode[index + 1]];
            int secondParam = instructions.SecondParameterMode == ParameterModesEnum.Immediate ? Intcode[index + 2] : Intcode[Intcode[index + 2]];
            int thirdParam = Intcode[index + 3];

            if (firstParam == secondParam)
            {
                Intcode[thirdParam] = 1;
            }
            else
            {
                Intcode[thirdParam] = 0;
            }
            instructions.IndexesToMoveForNextInstruction = 4;
        }

        private static OpcodeEnum GetOpcode(int value)
        {
            return value switch
            {
                1 => OpcodeEnum.Add,
                2 => OpcodeEnum.Multiply,
                3 => OpcodeEnum.Input,
                4 => OpcodeEnum.Output,
                5 => OpcodeEnum.JumpIfTrue,
                6 => OpcodeEnum.JumpIfFalse,
                7 => OpcodeEnum.LessThan,
                8 => OpcodeEnum.Equals,
                99 => OpcodeEnum.EndProgram,
                _ => throw new Exception("Invalid opcode"),
            };
        }

        private static ParameterModesEnum GetParameter(int value)
        {
            switch (value)
            {
                case 0:
                    return ParameterModesEnum.Position;
                case 1:
                    return ParameterModesEnum.Immediate;
                default:
                    throw new Exception("Ivalid parameter mode");
            }
        }
    }
}
