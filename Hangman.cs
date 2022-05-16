// ABDIEL WONG AVILA
/*****************************************************************************\
|*                                                                           *|
\*****************************************************************************/

using System;

public class Hangman
{
    /**************************************************************************\
    |* Game Constants                                                         *|
    \**************************************************************************/

    string[] words;
    string selectedWord;
    bool[] guessedLetters;
    int guessCount;
    string[] gallows;


    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public static void Main(string[] arg)
    {
        Hangman ag = new Hangman();
        ag.Start();
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public Hangman()
    {

    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public void Start()
    {
        string input;

        Init();

        ShowGameStartScreen();

        do
        {
            ShowBoard();

            do
            {
                ShowInputOptions();

                input = GetInput();
            }
            while (!IsValidInput(input));

            ProcessInput(input);

        }
        while (!IsGameOver());

        ShowGameOverScreen();
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public void Init()
    {
        words = new string[] { "house", "mirror", "floor" };

        Random rng = new Random();

        int rIndex = rng.Next(words.Length);

        selectedWord = words[rIndex];

        guessedLetters = new bool[selectedWord.Length];

        guessCount = 0;

        gallows = new string[7];

        gallows[0] = "_____ \n" +
                     "|  \n" +
                     "| \n" +
                     "| \n" +
                     "| \n" +
                     "|______ \n";

        gallows[1] = "_____ \n" +
                     "| | \n" +
                     "| O \n" +
                     "| \n" +
                     "| \n" +
                     "|______ \n";

        gallows[2] = "_____ \n" +
                     "| | \n" +
                     "| O \n" +
                     "| | \n" +
                     "| \n" +
                     "|______ \n";

        gallows[3] = "_____ \n" +
                     "| | \n" +
                     "| O \n" +
                     "| |\\ \n" +
                     "| \n" +
                     "|______ \n";

        gallows[4] = "_____ \n" +
                     "| | \n" +
                     "| O \n" +
                     "|/|\\ \n" +
                     "| \n" +
                     "|______ \n";

        gallows[5] = "_____ \n" +
                     "| | \n" +
                     "| O \n" +
                     "|/|\\\n" +
                     "|  \\\n" +
                     "|______ \n";


        gallows[6] = "_____ \n" +
                     "| | \n" +
                     "| O \n" +
                     "|/|\\\n" +
                     "|/ \\\n" +
                     "|______ \n";
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public void ShowGameStartScreen()
    {
        Console.WriteLine("Welcome to Hangman!");
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public void ShowBoard()
    {
        Console.WriteLine(gallows[guessCount]);

        for (int i = 0; i < guessedLetters.Length; i++)
        {
            if (guessedLetters[i] == true)
            {
                Console.Write(selectedWord[i] + " ");
            }
            else
            {
                Console.Write("_" + " ");
            }
        }

        if (selectedWord.Length == 5)
        {
            Console.WriteLine($"\nYou have {selectedWord.Length - guessCount + 1} tries left");
        }
        else
        {
            Console.WriteLine($"\nYou have {selectedWord.Length - guessCount} tries left");
        }
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public void ShowInputOptions()
    {
        Console.Write("Enter any letter: ");
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public string GetInput()
    {
        string input = Console.ReadLine().Trim().ToLower();

        return input;
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public bool IsValidInput(string input)
    {
        if (input.Length == 1 && input.All(char.IsLetter))
        {
            return true;
        }
        else if (input.Length > 1 && input.All(char.IsLetter))
        {
            Console.WriteLine("Please, input one letter");
            return false;
        }
        else if (!input.All(char.IsLetter))
        {
            Console.WriteLine("Numbers and symbols aren't allowed\nPlease, input one letter");
            return false;
        }
        else
        {
            Console.WriteLine("Please input... wait, how did you get in here?\nOh, blank space now I remember.");
            return false;
        }
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public void ProcessInput(string input)
    {
        bool compatibleWord = false;

        for (int i = 0; i < selectedWord.Length; i++)
        {
            if (selectedWord[i] == input[0])
            {
                guessedLetters[i] = true;
                compatibleWord = true;
            }
        }

        if (compatibleWord == false)
        {
            guessCount++;
        }
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public bool IsGameOver()
    {
        return (CheckWin() || CheckLoss());
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public bool CheckWin()
    {
        return guessedLetters.All(x => x);
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public bool CheckLoss()
    {
        if (guessCount >= 6)
        {
            return true;
        }

        return false;
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public void ShowGameOverScreen()
    {
        if (CheckWin())
        {
            Console.WriteLine(gallows[guessCount]);
            Console.WriteLine("Congratulations! You Won!");
        }
        else if (CheckLoss())
        {
            Console.WriteLine(gallows[guessCount]);
            Console.WriteLine($"You Lost! The word was {selectedWord}.");
        }
        else
        {
            Console.WriteLine("Something went really wrong! This is never supposed to happen!");
        }
    }
}