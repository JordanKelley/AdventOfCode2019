using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Days.Eight
{
    public static class DayEight
    {
        public static int PartOne()
        {
            string inputData = Input.InputData.Data;
            int width = Input.InputData.Width;
            int height = Input.InputData.Height;

            List<List<string>> image = ConstructImage(inputData, width, height);

            List<string> layerWithFewestZeros = GetLayerWithFewestZeros(image);

            return NumberOf1DigitsMultipliedByNumberOf2Digits(layerWithFewestZeros);
        }

        public static List<List<string>> PartTwo()
        {
            string inputData = Input.PartTwoSmallTestData.Data;
            int width = Input.PartTwoSmallTestData.Width;
            int height = Input.PartTwoSmallTestData.Height;

            List<List<string>> image = ConstructImage(inputData, width, height);

            return DecodeImage(image, width, height);
        }

        private static List<List<string>> DecodeImage(List<List<string>> image, int width, int height)
        {
            List<int> minimums = new List<int>();
            List<List<string>> decodedImage = new List<List<string>>();
            int numberOfLayersForDecodedImage = image.Count / 2;

            foreach(List<string> layer in image)
            {
                var min = layer.SelectMany(x => x).Select(x => x.ToString()).Select(x => Int32.Parse(x)).Min(x => x);
                minimums.Add(min);
            }

            var minimumsToString = minimums.Select(x => x.ToString());

            String input = "";

            foreach(var s in minimumsToString)
            {
                input = input + s;
            }

            return ConstructImage(input, width, height);
        }

        private static List<List<string>> DecodeImage(List<List<string>> image)
        {
            List<string> firstLayer = image[0];
            List<string> decodedPixels = new List<string>();

            for(int i = 0; i < firstLayer.Count; i++)
            {
                if(firstLayer[i] == "1" | firstLayer[i] == "0")
                {

                }
            }

            throw new NotImplementedException();
        }

        private static List<List<string>> ConstructImage(string inputData, int width, int height)
        {
            int numberOfLayers = inputData.Length / width / height;

            int count = 0;

            List<List<string>> image = new List<List<string>>();

            for (int i = 0; i < numberOfLayers; i++)
            {
                List<string> layer = new List<string>();

                for (int j = 0; j < height; j++)
                {
                    string pixel = inputData.Substring(count, width);
                    layer.Add(pixel);
                    count += width;
                }
                image.Add(layer);
            }
            return image;
        }

        private static List<string> GetLayerWithFewestZeros(List<List<string>> image)
        {
            int maxZerosInALayer = GetCountOfZerosInLayerWithMostZeros(image);
            List<string> layerWithLeastZeros = new List<string>();

            foreach(var layer in image)
            {
                int zerosOnLayer = layer.SelectMany(x => x).Count(y => y == '0');

                if (zerosOnLayer < maxZerosInALayer)
                {
                    layerWithLeastZeros = layer;
                    maxZerosInALayer = zerosOnLayer;
                }
            }
            return layerWithLeastZeros;
        }

        private static int GetCountOfZerosInLayerWithMostZeros(List<List<string>> image)
        {
            int maxCountOfZeros = 0;

            foreach (var layer in image)
            {
                int zerosOnLayer = layer.SelectMany(x => x).Count(y => y == '0');
                if (zerosOnLayer > maxCountOfZeros)
                {
                    maxCountOfZeros = zerosOnLayer;
                }
            }
            if(maxCountOfZeros == 0)
            {
                throw new Exception("There are no zeros in data");
            }
            return maxCountOfZeros;
        }

        private static int NumberOf1DigitsMultipliedByNumberOf2Digits(List<string> layer)
        {
            int numberOf1Digits = 0;
            int numberOf2Digits = 0;

            foreach(string pixel in layer)
            {
                numberOf1Digits += pixel.Count(x => x == '1');
                numberOf2Digits += pixel.Count(x => x == '2');
            }

            return numberOf1Digits * numberOf2Digits;
        }
    }
}
