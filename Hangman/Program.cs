using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class WordList : List<string>
{
}
public class Hangman
{

    private static WordList words;
    private static Random rnd = new Random();

    public static void Main(string[] args)
    {
        Console.Title = "Hangman";
        Console.WriteLine("Welcome to the Hangman Game!");

        string[] words = new string[10];
        words[0] = "dator";
        words[1] = "gissningar";
        words[2] = "programmering";
        words[3] = "avsluta";
        words[4] = "spelare";
        words[5] = "utbildning";
        words[6] = "kunskap";
        words[7] = "över";
        words[8] = "stad";
        words[9] = "fortsätter";


        var i = rnd.Next(0, words.Count() - 1);
        string word = words[i];
        
        int MenuChoice = 0;
        while (MenuChoice != 4)
        {
            Console.Write("\n\t1) Display the word list");
            Console.Write("\n\t2) Play");
            Console.Write("\n\t3) End\n\n");

            Console.Write("\n\tChoose 1-3: ");

            MenuChoice = Convert.ToInt32(Console.ReadLine());

            switch (MenuChoice)
            {
                case 1:
                    Console.Clear();
                    Console.Write("\n\tWord List\n\n");
                    foreach (string w in words)
                        Console.WriteLine(w);
                    break;
                case 2:
                    Console.Clear();
                    int numGuessesInt = -1;
                    while (numGuessesInt == -1)

                    {
                        UserpickGuesses(ref numGuessesInt);
                    }

                    StringBuilder guessedLetters = new StringBuilder();
                    bool solved = false;
                    
                    while (solved == false)
                    {
                        string wordToDisplay = ShowWord(guessedLetters, word);

                        if (!wordToDisplay.Contains("_"))
                        {
                            solved = true;
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("Congratulations, You Won!  The word you guessed was:  " + word);
                            Console.ForegroundColor = ConsoleColor.Black;
                            //UserReplay();
                            ExitGame();
                        }
                        else if (wordToDisplay.Contains(word))
                        {
                            solved = true;
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("Congratulations, You Won!  The word you guessed was:  " + word);
                            Console.ForegroundColor = ConsoleColor.Black;
                            //UserReplay();
                            ExitGame();
                        }

                        else if (numGuessesInt <= 0)
                        {
                            solved = true;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Game Over,!The correct word was :  " + word);
                            Console.ForegroundColor = ConsoleColor.Black;
                            //UserReplay();
                            ExitGame();
                        }

                        else
                        {
                            LetterGuess(guessedLetters, word, wordToDisplay, ref numGuessesInt);
                        }
                    }

                    break;

                case 3:
                    Console.WriteLine("\n\t End\n\n");
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Error[1]: Wrong Key, Try Again!");
                    break;
            }

        }

    }

    private static void UserpickGuesses(ref int userNumGuessesInt)
    {
        userNumGuessesInt = 10;
    }

    private static string RandomWord()
    {
        string[] words = new string[10];
        words[0] = "dator";
        words[1] = "gissningar";
        words[2] = "programmering";
        words[3] = "avsluta";
        words[4] = "spelare";
        words[5] = "utbildning";
        words[6] = "kunskap";
        words[7] = "över";
        words[8] = "stad";
        words[9] = "fortsätter";


        Random rnd = new Random();
        var i = rnd.Next(0, 9);
        string word = words[i];
        return word;   
    }

    private static string ShowWord(StringBuilder guessedCharacters, string word)
    {
        string returnedWord = "";
        if (guessedCharacters.Length == 0)
        {
            foreach (char letter in word)
            {
                returnedWord += "_ ";
            }
            return returnedWord;
        }
        foreach (char letter in word)
        {
            bool letterMatch = false;
            for (int i = 0; i < guessedCharacters.Length; i++)
            {
                if (guessedCharacters[i] == letter)
                {
                    returnedWord += guessedCharacters[i] + " ";
                    letterMatch = true;
                    break;
                }
                else
                {
                    letterMatch = false;
                }
            }
            if (letterMatch == false)
            {
                returnedWord += "_ ";
            }
        }
        return returnedWord;
    }


    static void LetterGuess(StringBuilder guessedCharacters, string word, string wordToDisplay, ref int numGuessesLeft)
    {
        string letters = "";
       
        for (int i = 0; i < guessedCharacters.Length; i++)
        {
            letters += " " + guessedCharacters[i];
        }

        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Guess for a letter between a-ö");
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write("The Swedish word consists of {0} letters: ", word.Length);
        Console.WriteLine(wordToDisplay);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Used Letters: " + letters);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("You have {0} lives", numGuessesLeft);

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        string guess = Console.ReadLine();
        StringBuilder guessedLetter = new StringBuilder();
        StringBuilder strB = new StringBuilder();

        guessedLetter.Append(guess + " ");

       
        bool repeat = false;
        for (int i = 0; i < guessedCharacters.Length; i++)
        {
            if (guessedCharacters[i].Equals(guessedLetter))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error[4]: You Entered this letter already, try another");
                repeat = true;
            }   
        }
        if (repeat == false)
        {
            guessedCharacters.Append(guessedLetter);
            numGuessesLeft -= 1;
        }

    }

    static void UserReplay()
    {
        Console.WriteLine("Do you want to play again? (Y/N)");
        string playAgain = Console.ReadLine();

        if (playAgain == "n")
        {
            Environment.Exit(1);
        }
        Console.Clear();
    }
    static void ExitGame()
    {
        Console.WriteLine("\n\tPress any key to Exit\n\n");
        Console.ReadKey();
        Environment.Exit(1);
    }
}
