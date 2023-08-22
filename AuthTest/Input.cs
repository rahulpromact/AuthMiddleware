using System.Text;

namespace AuthTest;

public class Input
{
    public CredDto TakeInput()
    {
        var cred = new CredDto();
        Console.Write("Enter your username: ");
        cred.Username = Console.ReadLine();
        Console.WriteLine();
        
        Console.Write("Enter your password: ");
        cred.Password = GetPasswordFromConsole();
        Console.WriteLine();
        return cred;
    }
    
    static string GetPasswordFromConsole()
    {
        StringBuilder password = new StringBuilder();
        ConsoleKeyInfo keyInfo;

        do
        {
            keyInfo = Console.ReadKey(true); // true hides the character

            if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                // Handle backspace by removing the last character
                password.Length--;
                Console.Write("\b \b"); // Erase the character on screen
            }
            else if (keyInfo.Key != ConsoleKey.Enter)
            {
                password.Append(keyInfo.KeyChar);
                Console.Write("*"); // Print an asterisk for each character
            }
        } while (keyInfo.Key != ConsoleKey.Enter);

        return password.ToString();
    }
}

public class CredDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}