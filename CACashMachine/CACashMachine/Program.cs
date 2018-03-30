using System;

namespace CACashMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Банкомат ПриРуслат24");
                Console.ResetColor();

                CashMachine.SignIn();
            }
        }
    }
}