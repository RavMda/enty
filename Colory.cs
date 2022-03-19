using System.Text.RegularExpressions;

namespace fgasfsasf;

public static class Colory
{
    private static readonly Dictionary<string, ConsoleColor> Codes = new();

    static Colory()
    {
        Codes.Add(":cg:", ConsoleColor.Green);
        Codes.Add(":cr:", ConsoleColor.Red);
        Codes.Add(":cdy:", ConsoleColor.DarkYellow);
        Codes.Add(":cc:", ConsoleColor.Gray);
        Codes.Add(":ccc:", ConsoleColor.DarkGray);
    }

    public static void Print(int line, string str)
    {
        // clear line
        Console.SetCursorPosition(0, line);
        Console.Write(new string(' ', Console.WindowWidth));

        // set cursor for further interactions
        Console.SetCursorPosition(0, line);
        
        // split the string to get color codes
        var split = Regex.Split(str, @"(:\w+:)");
        
        foreach (var part in split)
        {
            // if its the color code -> set color
            if (Codes.ContainsKey(part))
            {
                Console.ForegroundColor = Codes[part];
                continue;
            }
            
            // if its something else just write it and strip the color away
            Console.Write(part);
            Console.ResetColor();
        }
    }
}