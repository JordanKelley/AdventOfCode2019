using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Days.Seven
{
    public static class DaySeven
    {
        private static int[] InputData = Input.TestInput;
        public static int PartOne()
        {
            List<int> outputsForPhaseSettingSequences = new List<int>();

            int[] phaseSettingSequence = new int[]
            {
                0,1,2,3,4
            };
            var allPosibleCombinations = GetPhaseSettingSequenceCombinations(phaseSettingSequence);

            foreach(var sequence in allPosibleCombinations)
            {
                int outputForPhaseSettingSequence = RunSequence(sequence.ToArray());
                outputsForPhaseSettingSequences.Add(outputForPhaseSettingSequence);
            }
            return outputsForPhaseSettingSequences.Max(x => x);
        }

        private static int RunSequence(int[] phaseSettingSequence)
        {
            int secondInputValue = 0;
            foreach(int phaseSetting in phaseSettingSequence)
            {
                int[] input = new List<int>(InputData).ToArray();
                IntcodeComputer computer = new IntcodeComputer(input, phaseSetting, secondInputValue);
                secondInputValue = computer.OutputData[0];
            }
            return secondInputValue;
        }

        private static List<IEnumerable<int>> GetPhaseSettingSequenceCombinations(int[] phaseSettingSequence)
        {
            // ripped from https://stackoverflow.com/questions/33312488/permutations-of-numbers-in-array
            var results1 = GetPermutations.GetPermutationsWithRept(phaseSettingSequence, phaseSettingSequence.Length).ToList();

            var sorted_list = phaseSettingSequence.OrderBy(item => item).ToList();
            
            var result2 = results1
                            .Where(rlist => rlist.OrderBy(x => x).SequenceEqual(sorted_list)) //first filter
                            .Distinct(new EnumerableComparer<int>()) //second filter
                            .ToList();

            return result2;
        }
    }
}
