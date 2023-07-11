using System;
using System.Collections.Generic;

class BoyerMoore
{
    private static int[] BuildBadCharacterTable(string pattern)
    {
        int[] badCharacterTable = new int[256];
        int patternLength = pattern.Length;

        for (int i = 0; i < badCharacterTable.Length; i++)
        {
            badCharacterTable[i] = patternLength;
        }

        for (int i = 0; i < patternLength - 1; i++)
        {
            char c = pattern[i];
            badCharacterTable[c] = patternLength - 1 - i;
        }

        return badCharacterTable;
    }

    private static int[] BuildGoodSuffixTable(string pattern)
    {
        int patternLength = pattern.Length;
        int[] goodSuffixTable = new int[patternLength];
        int[] suffixes = new int[patternLength];
        int lastPrefixIndex = patternLength;

        for (int i = patternLength - 1; i >= 0; i--)
        {
            if (IsPrefix(pattern, i + 1))
            {
                lastPrefixIndex = i + 1;
            }

            suffixes[i] = lastPrefixIndex - i + patternLength - 1;
        }

        for (int i = 0; i < patternLength - 1; i++)
        {
            int suffixLength = GetSuffixLength(pattern, i);
            goodSuffixTable[suffixLength] = patternLength - 1 - i + suffixLength;
        }

        return goodSuffixTable;
    }

    private static bool IsPrefix(string pattern, int index)
    {
        int patternLength = pattern.Length;

        for (int i = index, j = 0; i < patternLength; i++, j++)
        {
            if (pattern[i] != pattern[j])
            {
                return false;
            }
        }

        return true;
    }

    private static int GetSuffixLength(string pattern, int index)
    {
        int length = 0;
        int patternLength = pattern.Length;

        for (int i = index, j = patternLength - 1; i >= 0 && pattern[i] == pattern[j]; i--, j--)
        {
            length++;
        }

        return length;
    }

    private static List<int> FindMatches(string text, string pattern)
    {
        List<int> matches = new List<int>();
        int textLength = text.Length;
        int patternLength = pattern.Length;
        int[] badCharacterTable = BuildBadCharacterTable(pattern);
        int[] goodSuffixTable = BuildGoodSuffixTable(pattern);
        int shift = 0;

        while (shift <= textLength - patternLength)
        {
            int j = patternLength - 1;

            while (j >= 0 && pattern[j] == text[shift + j])
            {
                j--;
            }

            if (j < 0)
            {
                matches.Add(shift);
                shift += (shift + patternLength < textLength) ? patternLength - badCharacterTable[text[shift + patternLength]] : 1;
            }
            else
            {
                int badCharacterShift = badCharacterTable[text[shift + j]];
                int goodSuffixShift = goodSuffixTable[j];

                shift += Math.Max(badCharacterShift, goodSuffixShift);
            }
        }

        return matches;
    }

    public static void Main(string[] args)
    {
        string text = "fadfaevasegsostente";
        string pattern = "ostente";
        List<int> matches = FindMatches(text, pattern);

        if (matches.Count > 0)
        {
            Console.WriteLine("Pattern found at positions:");

            foreach (int match in matches)
            {
                Console.WriteLine(match);
            }
        }
        else
        {
            Console.WriteLine("Pattern not found in the text.");
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}
