namespace Hangman
{
    internal class Program
    {
        static readonly string[] words = ["programming", "hangman", "csharp", "developer", "programmer"];
        static string selectedWord;
        static char[] guessedWord;
        static int incorrectGuesses = 0;
        const int maxIncorrectGuesses = 5;

        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Game();
                Show("Try again? (Y/N)", 10, 10, ConsoleColor.Magenta);
            }
            while (Console.ReadKey().Key != ConsoleKey.N);
            Console.Clear();
        }


        static void Game()
        {
            Show($"Welcome to the Hangman Game!".PadRight(30), 10, 4, ConsoleColor.Magenta);
            Show($"You will have {maxIncorrectGuesses} attempts to guess the right letters.".PadRight(30), 10, 5, ConsoleColor.DarkMagenta);
            Show($"Run out of guesses and you lose!".PadRight(30), 10, 6, ConsoleColor.DarkMagenta);
            Show($"Press any key to continue...".PadRight(30), 10, 7, ConsoleColor.DarkMagenta);
            Console.ReadKey();
            Console.Clear();

            InitializeGame();

            do
            {
                DisplayWord();
                char guessedLetter = GetGuess();
                CheckGuess(guessedLetter);

            } while (!IsGameOver());
            DisplayResult();
            incorrectGuesses = 0;
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
            Show($"Current word: " + new string(guessedWord).PadRight(30), 10, 7, ConsoleColor.White);
        }

        static char GetGuess()
        {
            Show($"Enter a letter: ", 10, 8, ConsoleColor.Yellow);
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
                    Show($"Correct guess!".PadRight(50), 10, 9, ConsoleColor.DarkGreen);
                }
            }

            if (!found)
            {
                incorrectGuesses++;
                Show($"Incorrect guess! You have {maxIncorrectGuesses - incorrectGuesses} guesses left.".PadRight(30), 10, 9, ConsoleColor.DarkRed);
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
                Show($"Congratulations! You guessed the word: " + selectedWord.PadRight(30), 10, 9, ConsoleColor.DarkGreen);
            }
            else
            {
                Show($"Sorry! You ran out of attempts. The correct word was: " + selectedWord.PadRight(30), 10, 9, ConsoleColor.DarkRed);
            }
        }

        static void Show(object text, int x, int y, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(text.ToString());
        }
    }
}
