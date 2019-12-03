using System;

namespace ConsoleApp1.Days.Two
{
    public static class DayTwo
    {
        public static int[] PartOne()
        {
            int[] testInput = Input.TestInput;

            return ProcessIntCode(testInput);
        }

        public static int[] PartTwo()
        {
            for(int i = 0; i < 100; i++)
            {
                for(var j = 0; j < 100; j++)
                {
                    int[] testInput = new int[Input.TestInput.Length];
                    Array.Copy(Input.TestInput, 0, testInput, 0, Input.TestInput.Length);

                    testInput[1] = i;
                    testInput[2] = j;
                    ProcessIntCode(testInput);

                    if(testInput[0] == 19690720)
                    {
                        return testInput;
                    }
                }
            }
            throw new Exception("The desired output value was never found");
        }

        private static int[] ProcessIntCode(int[] testInput)
        {
            for (int i = 0; i < testInput.Length; i += 4)
            {
                OpcodeEnum opcode = GetOpcode(i, testInput);

                if (opcode == OpcodeEnum.EndProgram)
                {
                    return testInput;
                }

                testInput = TranslateBatchOfIntCode(opcode, i, testInput);
            }
            return testInput;
        }

        private static OpcodeEnum GetOpcode(int index, int[] testInput)
        {
            switch(testInput[index])
            {
                case 1:
                    return OpcodeEnum.Add;
                case 2:
                    return OpcodeEnum.Multiply;
                case 99:
                    return OpcodeEnum.EndProgram;
                default:
                    throw new Exception("Invalid opcode");
            }
        }

        private static int[] TranslateBatchOfIntCode(OpcodeEnum opcode, int index, int[] testInput)
        {
            switch(opcode)
            {
                case OpcodeEnum.Add:
                    return AddBatchOfIntCode(index, testInput);
                case OpcodeEnum.Multiply:
                    return MultiplyBatchOfIntCode(index, testInput);
                default:
                    throw new Exception("Invalid opcode upstream");
            }
        }

        private static int[] AddBatchOfIntCode(int index, int[] testInput)
        {
            int firstAddNum = testInput[testInput[index + 1]];
            int secondAddNum = testInput[testInput[index + 2]];
            int sum = firstAddNum + secondAddNum;

            testInput[testInput[index + 3]] = sum;
            return testInput;
        }

        private static int[] MultiplyBatchOfIntCode(int index, int[] testInput)
        {
            int firstMultNum = testInput[testInput[index + 1]];
            int secondMultNum = testInput[testInput[index + 2]];
            int product = firstMultNum * secondMultNum;

            testInput[testInput[index + 3]] = product;
            return testInput;
        }
    }
}
