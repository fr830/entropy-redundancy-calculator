using System;
using System.Collections.Generic;
using System.IO;

namespace calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, float> symbolOccurrences;   
            byte[] fileBytes;
            double H, R;

            Console.WriteLine("TXT file");
            fileBytes = File.ReadAllBytes("resources/file.txt");
            symbolOccurrences = CountSymbolsOccurrences(fileBytes);
            H = CalculateEntropy(symbolOccurrences, fileBytes.Length);
            R = 1 - (H / Math.Log(symbolOccurrences.Count, 2));
            Console.WriteLine("Entropy: " + H);
            Console.WriteLine("Redundancy: " + R);
            
            Console.WriteLine("\nPDF file");
            fileBytes = File.ReadAllBytes("resources/file.pdf");
            symbolOccurrences = CountSymbolsOccurrences(fileBytes);
            H = CalculateEntropy(symbolOccurrences, fileBytes.Length);
            R = 1 - H / Math.Log(symbolOccurrences.Count, 2);
            Console.WriteLine("Entropy: " + H);
            Console.WriteLine("Redundancy: " + R);
            
            Console.WriteLine("\nZIP file");
            fileBytes = File.ReadAllBytes("resources/file.zip");
            symbolOccurrences = CountSymbolsOccurrences(fileBytes);
            H = CalculateEntropy(symbolOccurrences, fileBytes.Length);
            R = 1 - H / Math.Log(symbolOccurrences.Count, 2);
            Console.WriteLine("Entropy: " + H);
            Console.WriteLine("Redundancy: " + R);
        }

        private static double CalculateEntropy(Dictionary<char,float> symbolOccurrences, int fileLength)
        {
            double h = 0;
            foreach (KeyValuePair<char,float> symbol in symbolOccurrences)
            {
                double symbolProbability = symbol.Value / fileLength;
                h = h + symbolProbability * Math.Log(symbolProbability, 2);
            }

            return -h;
        }

        /// <summary>
        /// Counts the ocurrences of symbols in an array of bytes
        /// </summary>
        /// <param name="fileBytes">An array of bytes from a file</param>
        /// <returns>A Dictionary that holds a count for every symbol in the bytes array</returns>
        private static Dictionary<char, float> CountSymbolsOccurrences(byte[] fileBytes)
        {
            Dictionary<char, float> symbolOccurrences = new Dictionary<char, float>();
            foreach (byte b in fileBytes)
            {
                if (symbolOccurrences.ContainsKey((char) b))
                {
                    symbolOccurrences[(char)b]++;
                }
                else
                {
                    symbolOccurrences.Add((char)b, 1);
                }
            }

            return symbolOccurrences;
        }
    }
}