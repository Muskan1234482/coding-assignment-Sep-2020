using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class GoodieDilema
{
    public static void Main()
    {
        int filenumber = 0;
        foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt"))
        {
            filenumber = filenumber + 1;
            findgoodies(File.ReadAllLines(file), filenumber);
        }
        Console.ReadLine();
    }

    static void findgoodies(string[] lines, int filenumber)
    {
        int numberofemployees, result, maximum, minimum;
        numberofemployees = Int32.Parse(lines[0].Split(':')[1].Trim());
        lines = lines.Where((line, index) => !string.IsNullOrWhiteSpace(line) && index > 3).ToArray();
        var dict = lines.Select(line => line.Split(':')).ToDictionary(split => split[0].Trim(), split => Int32.Parse(split[1].Trim()));
        int[] arr = dict.Values.ToArray();
        minDiff(arr, arr.Length, numberofemployees, out result, out minimum, out maximum);
        var matches = dict.Where(kvp => kvp.Value >= minimum && kvp.Value <= maximum);
        using (StreamWriter file = new StreamWriter("output-" + filenumber + ".txt", false))
        {
            file.WriteLine("Here the goodies that are selected for distribution are:");
            file.WriteLine("");
            foreach (var entry in matches)
            {
                file.WriteLine("{0}: {1}", entry.Key, entry.Value);
            }
            file.WriteLine("");
            file.WriteLine("And the difference between the chosen goodie with highest price and the lowest price is {0}", result);
        }

    }

    static void minDiff(int[] arr, int n, int k, out int result, out int minimum, out int maximum)
    {
        Array.Sort(arr);
        result = int.MaxValue;
        maximum = 0;
        minimum = 0;
        for (int i = 0; i <= n - k; i++)
        {
            var res = Math.Min(result, arr[i + k - 1] - arr[i]);
            if (res < result)
            {
                result = res;
                maximum = arr[i + k - 1];
                minimum = arr[i];
            }
        }
    }
}