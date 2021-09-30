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
        //InitializeWordList();
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




                    //string word = RandomWord();

                    //List<char> guessedLetters = new List<char>();
                    char[] guessedLetters = new char['a'];
                    //StringBuilder guessedLetters = new StringBuilder("a", 50);
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
                            UserReplay();

                        }
                       
                        else if (numGuessesInt <= 0)
                        {
                            solved = true;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Game Over,!The correct word was :  " + word);
                            Console.ForegroundColor = ConsoleColor.Black;
                            UserReplay();

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

    private static void InitializeWordList()
    {
       
       
        //words = new WordList();
        //words.Add("dator");         
        //words.Add("gissningar");
        //words.Add("programmering");
        //words.Add("avsluta");
        //words.Add("spelare");
        //words.Add("på");
        //words.Add("över");
        //words.Add("fortsätter");
        //words.Add("utbildning");
        //words.Add("kunskap");  
        //words.Sort();                
    }


    private static void UserpickGuesses(ref int userNumGuessesInt)
    {
        userNumGuessesInt = 10;
                                                                
    }

    private static string RandomWord()
    {
        //Random rnd = new Random();
        //var i = rnd.Next(0, words.Count() - 1);
        //return words[i];
        return words[rnd.Next(0, words.Count() - 1)];
    }

    //private static string ShowWord(List<char> guessedCharacters, string word)
    private static string ShowWord(char [] guessedCharacters, string word)
    {
        string returnedWord = ""; 
        if (guessedCharacters.Count() == 0)
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
            foreach (char character in guessedCharacters)
            {
                if (character == letter) 
                {  
                    returnedWord += character + " ";
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


    static void LetterGuess(char[] guessedCharacters, string word, string wordToDisplay, ref int numGuessesLeft)
    {
        string letters = "";
        foreach (char letter in guessedCharacters)
        {
            letters += " " + letter;
        }

        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Guess for a letter between A-Ö");
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write("The Swedish word consists of {0} letters Word: ", word.Length);
        Console.WriteLine(wordToDisplay);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Used Letters: " + letters);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("You have {0} lives" , numGuessesLeft);

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        string guess = Console.ReadLine();
        char guessedLetter = 'a';


        if (guess.Length > 1)
        {
            //word = RandomWord();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(guess == word ? string.Format("Congratulations, You Won! The correct word was {0}", word) : string.Format("You Lost! The correct word was '{0}'", word));

            Console.WriteLine("\n\tPress any key to Exit\n\n");
            Console.ReadKey();
            Environment.Exit(1);

        }
        else
        {
            try
            {
                guessedLetter = Convert.ToChar(guess);
                if (!char.IsLetter(guessedLetter))
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error[3]: Enter one LETTER or guess the WORD!");

            }

            bool repeat = false;
            for (int i = 0; i < guessedCharacters.Count(); i++)
            {
                if (guessedCharacters[i] == guessedLetter)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error[4]: You Entered this letter already, try another");
                    repeat = true;
                }
            }
            if (repeat == false)
            {
                for (int runs = 0; runs < 1; runs++)
                {
                    guessedCharacters[runs] = guessedLetter;
                }
                //guessedCharacters.add(guessedLetter)  ;
                numGuessesLeft -= 1;
            }
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
}