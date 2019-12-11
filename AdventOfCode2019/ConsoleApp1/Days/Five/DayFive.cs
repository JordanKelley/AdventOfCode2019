using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Days.Five
{
    public static class DayFive
    {
        private static List<int> OutputData = new List<int>();
        public static int PartOne()
        {
            int[] testInput = Input.TestInput;

            InterpretIntCode(testInput, true);

            return OutputData[OutputData.Count - 1];
        }

        public static int PartTwo()
        {
            int[] testInput = Input.TestInput;

            InterpretIntCode(testInput, false);

            return OutputData[0];
        }

        private static void InterpretIntCode(int[] testInput, bool isPartOne)
        {
            int count = 0;

            while (count < testInput.Length)
            {
                if(testInput[count] == 99)
                {
                    return;
                }

                IntcodeInstruction instructions = GetInstructions(testInput[count]);

                ProcessInstructions(testInput, instructions, count, isPartOne);

                if(instructions.UseIndexForNextInstruction)
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

        private static void ProcessInstructions(int[] input, IntcodeInstruction instructions, int currentIndex, bool isPartOne)
        {
            // strategy pattern would be cleaner
            switch(instructions.Opcode)
            {
                case OpcodeEnum.Add:
                    AddBatchOfIntcode(currentIndex, input, instructions);
                    break;
                case OpcodeEnum.Multiply:
                    MultiplyBatchOfIntcode(currentIndex, input, instructions);
                    break;
                case OpcodeEnum.Input:
                    InputBatchOfIntcode(currentIndex, input, instructions, isPartOne);
                    break;
                case OpcodeEnum.Output:
                    OutputBatchOfIntcode(currentIndex, input, instructions);
                    break;
                case OpcodeEnum.JumpIfTrue:
                    JumpIfTrueBatchOfIntcode(currentIndex, input, instructions);
                    break;
                case OpcodeEnum.JumpIfFalse:
                    JumpIfFalseBatchOfIntcode(currentIndex, input, instructions);
                    break;
                case OpcodeEnum.LessThan:
                    LessThanBatchOfIntcode(currentIndex, input, instructions);
                    break;
                case OpcodeEnum.Equals:
                    EqualsBatchOfIntcode(currentIndex, input, instructions);
                    break;
                case OpcodeEnum.EndProgram:
                    break;
                default:
                    throw new Exception("Invalid opcode");
            }
        }

        private static void AddBatchOfIntcode(int index, int[] testInput, IntcodeInstruction instructions)
        {
            int firstAddNum = instructions.FirstParameterMode == ParameterModesEnum.Position ? testInput[testInput[index + 1]] : testInput[index + 1];
            int secondAddNum = instructions.SecondParameterMode == ParameterModesEnum.Position ? testInput[testInput[index + 2]] : testInput[index + 2];

            int sum = firstAddNum + secondAddNum;

            testInput[testInput[index + 3]] = sum;
            instructions.IndexesToMoveForNextInstruction = 4;
        }

        private static void MultiplyBatchOfIntcode(int index, int[] testInput, IntcodeInstruction instructions)
        {
            int firstMultNum = instructions.FirstParameterMode == ParameterModesEnum.Position ? testInput[testInput[index + 1]] : testInput[index + 1];
            int secondMultNum = instructions.SecondParameterMode == ParameterModesEnum.Position ? testInput[testInput[index + 2]] : testInput[index + 2];
            int product = firstMultNum * secondMultNum;

            testInput[testInput[index + 3]] = product;
            instructions.IndexesToMoveForNextInstruction = 4;
        }

        private static void InputBatchOfIntcode(int index, int[] testInput, IntcodeInstruction instructions, bool isPartOne)
        {
            int inputValue = isPartOne ? 1 : 5;
            int saveToPosition = testInput[index + 1];
            testInput[saveToPosition] = inputValue;
            instructions.IndexesToMoveForNextInstruction = 2;
        }

        private static void OutputBatchOfIntcode(int index, int[] testInput, IntcodeInstruction instructions)
        {
            int outputValue;
            if(instructions.FirstParameterMode == ParameterModesEnum.Position)
            {
                outputValue = testInput[testInput[index + 1]];
            }
            else
            {
                outputValue = testInput[index + 1];
            }
            OutputData.Add(outputValue);
            instructions.IndexesToMoveForNextInstruction = 2;
        }

        public static void JumpIfTrueBatchOfIntcode(int index, int[] testInput, IntcodeInstruction instructions)
        {
            int firstParam = instructions.FirstParameterMode == ParameterModesEnum.Immediate ? testInput[index + 1] : testInput[testInput[index + 1]];
            int secondParam = instructions.SecondParameterMode == ParameterModesEnum.Immediate ? testInput[index + 2] : testInput[testInput[index + 2]];
            if (firstParam != 0)
            {
                testInput[index] = secondParam;
                instructions.IndexForNextInstruction = secondParam;
                instructions.UseIndexForNextInstruction = true;
            }
            else
            {
                instructions.IndexesToMoveForNextInstruction = 3;
            }
        }

        public static void JumpIfFalseBatchOfIntcode(int index, int[] testInput, IntcodeInstruction instructions)
        {
            int firstParam = instructions.FirstParameterMode == ParameterModesEnum.Immediate ? testInput[index + 1] : testInput[testInput[index + 1]];
            int secondParam = instructions.SecondParameterMode == ParameterModesEnum.Immediate ? testInput[index + 2] : testInput[testInput[index + 2]];

            if (firstParam == 0)
            {
                testInput[index] = secondParam;
                instructions.IndexForNextInstruction = secondParam;
                instructions.UseIndexForNextInstruction = true;
            }
            else
            {
                instructions.IndexesToMoveForNextInstruction = 3;
            }
        }

        public static void LessThanBatchOfIntcode(int index, int[] testInput, IntcodeInstruction instructions)
        {
            int firstParam = instructions.FirstParameterMode == ParameterModesEnum.Immediate ? testInput[index + 1] : testInput[testInput[index + 1]];
            int secondParam = instructions.SecondParameterMode == ParameterModesEnum.Immediate ? testInput[index + 2] : testInput[testInput[index + 2]];
            int thirdParam =  testInput[index + 3];

            if (firstParam < secondParam)
            {
                testInput[thirdParam] = 1;
            }
            else
            {
                testInput[thirdParam] = 0;
            }
            instructions.IndexesToMoveForNextInstruction = 4;
        }

        private static void EqualsBatchOfIntcode(int index, int[] testInput, IntcodeInstruction instructions)
        {
            int firstParam = instructions.FirstParameterMode == ParameterModesEnum.Immediate ? testInput[index + 1] : testInput[testInput[index + 1]];
            int secondParam = instructions.SecondParameterMode == ParameterModesEnum.Immediate ? testInput[index + 2] : testInput[testInput[index + 2]];
            int thirdParam = testInput[index + 3];

            if (firstParam == secondParam)
            {
                testInput[thirdParam] = 1;
            }
            else
            {
                testInput[thirdParam] = 0;
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
