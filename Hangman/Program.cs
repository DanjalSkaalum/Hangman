namespace Hangman
{
    internal class Program
    {
        static readonly string[] words = ["programming", "hangman", "csharp", "developer", "programmer"];
        static string selectedWord;
        static char[] guessedWord;
        static int incorrectGuesses = 0;
        const int maxIncorrectGuesses = 6;

        static void Main()
        {
            InitializeGame();
            Console.WriteLine("Welcome to the Hangman Game!");

            do
            {
                DisplayWord();
                char guessedLetter = GetGuess();
                CheckGuess(guessedLetter);

            } while (!IsGameOver());

            DisplayResult();
        }

        static void InitializeGame()
        {
            Random random = new();
            selectedWord = words[random.Next(words.Length)];
            guessedWord = new char[selectedWord.Length];

            for (int i = 0; i < selectedWord.Length; i++)
            {
                guessedWord[i] = '_';
            }
        }

        static void DisplayWord()
        {
            Console.WriteLine("Current word: " + new string(guessedWord));
        }

        static char GetGuess()
        {
            Console.Write("Enter a letter: ");
            char guessedLetter = Console.ReadKey().KeyChar;
            Console.WriteLine(); // Move to the next line after reading the letter
            return guessedLetter;
        }

        static void CheckGuess(char guessedLetter)
        {
            bool found = false;
            for (int i = 0; i < selectedWord.Length; i++)
            {
                if (selectedWord[i] == guessedLetter)
                {
                    guessedWord[i] = guessedLetter;
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Incorrect guess!");
                incorrectGuesses++;
            }
        }

        static bool IsGameOver()
        {
            if (incorrectGuesses >= maxIncorrectGuesses)
            {
                return true;
            }

            if (Array.IndexOf(guessedWord, '_') == -1)
            {
                return true;
            }

            return false;
        }

        static void DisplayResult()
        {
            if (Array.IndexOf(guessedWord, '_') == -1)
            {
                Console.WriteLine("Congratulations! You guessed the word: " + selectedWord);
            }
            else
            {
                Console.WriteLine("Sorry! You ran out of attempts. The correct word was: " + selectedWord);
            }
        }
    }
}
