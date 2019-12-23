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

        public static int PartTwo()
        {
            List<int> outputsForPhaseSettingSequences = new List<int>();

            int[] phaseSettingSequence = new int[]
            {
                9, 8, 7, 6, 5
            };
            var allPosibleCombinations = GetPhaseSettingSequenceCombinations(phaseSettingSequence);

            foreach (var sequence in allPosibleCombinations)
            {
                int outputForPhaseSettingSequences = RunSequenceWithFeedbackLoop(sequence.ToArray());
                outputsForPhaseSettingSequences.Add(outputForPhaseSettingSequences);
            }
            return outputsForPhaseSettingSequences.Max(x => x);
        }

        private static int RunSequenceWithFeedbackLoop(int[] phaseSettingSequence)
        {
            List<IntcodeComputer> computers = new List<IntcodeComputer>();
            int secondInputValue = 0;

            // first run of each computer
            foreach (int phaseSetting in phaseSettingSequence)
            {
                int[] input = new List<int>(InputData).ToArray();
                IntcodeComputer computer = new IntcodeComputer(input, phaseSetting, secondInputValue);
                computer.ProcessIntcodeUntilOutput();
                computers.Add(computer);
                secondInputValue = computer.OutputData[0];
            }

            // keep going until 99 is hit on all of them and return output of E
            while(computers.Any(x => !x.HitOpcode99))
            {
                foreach(var computer in computers.Where(x => !x.HitOpcode99))
                {
                    int indexOfPreviousComputer = computers.IndexOf(computer) == 0 ? computers.Count - 1 : computers.IndexOf(computer) - 1;
                    secondInputValue = computers[indexOfPreviousComputer].OutputData.Last();
                    computer.SetSecondInputValue(secondInputValue);
                    computer.ProcessIntcodeUntilOutput();
                    secondInputValue = computer.OutputData.Last();
                }
            }

            return secondInputValue;
        }

        private static int RunSequence(int[] phaseSettingSequence)
        {
            int secondInputValue = 0;
            foreach(int phaseSetting in phaseSettingSequence)
            {
                int[] input = new List<int>(InputData).ToArray();
                IntcodeComputer computer = new IntcodeComputer(input, phaseSetting, secondInputValue);
                computer.ProcessAllIntcode();
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
