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
