using MyPhotoViewer.Core;
using System;

namespace PhotoDiscoverService.Data
{
    class AdminPasswordProvider
    {
        private const string EnterPasswordPrompt = "Enter admin password: ";
        private const string ConfirmPasswordPrompt = "Confirm admin password: ";

        public string GetAdminPassword()
        {
            Console.Write(EnterPasswordPrompt);
            string password = ReadPassword();
            Console.WriteLine();

            Console.Write(ConfirmPasswordPrompt);
            string confirmedPassword = ReadPassword();
            Console.WriteLine();

            Verifiers.Verify(string.Equals(password, confirmedPassword, StringComparison.Ordinal),
                             "Failed to obtain admin password: Password is not accepted");

            return password;
        }

        private string ReadPassword()
        {
            string password = string.Empty;
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, (password.Length - 1));
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            return password;
        }
    }
}
