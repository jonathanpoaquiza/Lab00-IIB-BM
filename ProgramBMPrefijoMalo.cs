//using System;

/*class BoyerMoore
{
    private const int ASCII_SIZE = 256;

    private int[] BadCharacterTable;

    public BoyerMoore()
    {
        BadCharacterTable = new int[ASCII_SIZE];
    }

    public int Search(string text, string pattern)
    {
        if (string.IsNullOrEmpty(pattern) || pattern.Length > text.Length)
            return -1;

        PreprocessBadCharacters(pattern);

        int textIndex = pattern.Length - 1;
        int patternIndex = pattern.Length - 1;

        while (textIndex < text.Length)
        {
            if (text[textIndex] == pattern[patternIndex])
            {
                if (patternIndex == 0)
                    return textIndex;

                patternIndex--;
                textIndex--;
            }
            else
            {
                textIndex += pattern.Length - Math.Min(patternIndex, 1 + BadCharacterTable[text[textIndex]]);
                patternIndex = pattern.Length - 1;
            }
        }

        return -1;
    }

    private void PreprocessBadCharacters(string pattern)
    {
        Array.Fill(BadCharacterTable, -1);

        for (int i = 0; i < pattern.Length; i++)
            BadCharacterTable[pattern[i]] = i;
    }
}

class Program
{
    /*static void Main(string[] args)
    {
        string text = "fadfaevasegsostente";
        string pattern = "ostente";

        BoyerMoore bm = new BoyerMoore();
        int index = bm.Search(text, pattern);

        if (index != -1)
            Console.WriteLine("Pattern found at index: " + index);
        else
            Console.WriteLine("Pattern not found.");
    }
}*/
