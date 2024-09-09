using Project1;
using System;

public static class Program
{
    private static void Main()
    {
        using var game = new Game1();
        game.Run();
    }
}
