using System;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace PreventSleep
{

    public static class Interaction
    {
        public static void ChangeState(int state)
        {
            //state 0 = PreventSleep
            //state 1 = AllowSleep
            if (state == 0)
            {
                try
                {
                    NativeMethods.PreventSleep();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                try
                {
                    NativeMethods.AllowSleep();
                }
                catch (Exception a)
                {
                    Console.WriteLine(a.Message);
                }
            }

        }

    }

    internal static class NativeMethods {

            public static void PreventSleep() {
                SetThreadExecutionState(ExecutionState.EsContinuous | ExecutionState.EsSystemRequired);
                Console.WriteLine("Reached PreventSleep Function");
            }

            public static void AllowSleep() {
                SetThreadExecutionState(ExecutionState.EsContinuous);
                Console.WriteLine("Reached AllowSleep Function");
            }

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);

            [FlagsAttribute]
            private enum ExecutionState : uint {
                EsAwaymodeRequired = 0x00000040,
                EsContinuous = 0x80000000,
                EsDisplayRequired = 0x00000002,
                EsSystemRequired = 0x00000001
            }
        }
    
    public class Launch
    {
        public static Char StringToFirstChar(String input)
        {
            char firstChar;
            firstChar = input[0];

            return firstChar;
        }

        public static void Main(string[] args)
        {
            String rawInput;
            Char userChar;
            Char userInput;
            String menu;

            userInput = 'b';

            menu = ("Press \'A\' to allow auto-sleep, Press \'T\' to turn off auto-sleep\n~~~~Press \'Q\' to quit~~~~\nIf you already entered the \'T\' command, keep this window open");

            while (userInput != 'q')
            {
                Console.WriteLine(menu);
                rawInput = Console.ReadLine();
                userChar = StringToFirstChar(rawInput);
                userInput = Char.ToLower(userChar);

                if (userInput == 'a')
                {
                    //allows auto-sleep
                    Interaction.ChangeState(1);
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    Console.WriteLine();
                    continue;
                }
                else if (userInput == 't')
                {
                    //prevents auto-sleep
                    Interaction.ChangeState(0);
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    Console.WriteLine();
                    continue;
                }
                else if (userInput != 'q')
                {
                    Console.WriteLine("Command Not Recognized");
                    Console.WriteLine();
                    continue;
                }
                else {
                    break;
                }

            }

        }

    }
}
