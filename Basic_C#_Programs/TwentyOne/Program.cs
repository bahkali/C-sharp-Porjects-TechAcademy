﻿using System;
using Casino;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Casino.TwentyOne;

namespace TwentyOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var logo = @"
                     ______________________________________
                     /   _______________________________    \
                    |   |            __ __              |   |
                    | _ |    /\     ) _) /''''',        |   |
                    |(.)|   <  >    \ ) // '  `,        |   |
                    | ` |    \/       \/// ~ |~:    +   |   |
                    |   |             ///*\  ' :    |   |   |
                    |   |            ///***\_~.'    |   |   |
                    |   |  .'.    __///````,,..\_   |   |   |
                    |   |   ` \ _/* +_\#\\~\ooo/ \  |   |   |
                    |   |     |/:\ + *\_\#\~\so/!!\ |   |   |
                    |   |    _\_::\ * + \-\#\\o/!!!\|   |   |
                    |   |   / <_=::\ + */_____@_!!!_|   |   |
                    |   |  <__/_____\ */( /\______ _|   |   |
                    |   |   |_   _ __\/_)/* \   ._/  >  |   |
                    |   |   | !!! @     /* + \::=_>_/   |   |
                    |   |   |\!!!/o\\#\_\ + * \::_\     |   |
                    |   |   | \!!/os\~\#_\_* + \:/|     |   |
                    |   |   |  \_/ooo\~\\#_\+_*/- \     |   |
                    |   |   |    \''``,,,,///      .`.  |   |
                    |   |   |     ,.- ***///        '   |   |
                    |   |   |    : ~  \*///             |   |
                    |   |   +    : |   \//\             |   |
                    |   |        ,~  ~ //_( \     /\    | ; |
                    |   |        ,'  ` /_(__(    <  >   |(_)|
                    |   |         `````           \/    |   |
                    |   |_______________________________|   |
                     \______________________________________/

";

            Console.WriteLine(logo);

            DateTime dateTime = new DateTime();

            //string File_path = @"C:\Users\PrecisionM4800\Desktop\C-sharp-Projects-TechAcademy\Logs";
            //File.ReadAllText(File_path);

            Console.WriteLine("Welcome to the Grand Hotel and Casino.\nLet's start by telling me your name.");
            string playerName = Console.ReadLine();

            Console.WriteLine("And how much money did you bring today?");
            int bank = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Hello, {0}. Would you like to join a game of 21 right now?", playerName);
            string answer = Console.ReadLine().ToLower();

            if (answer == "yes" || answer == "yeah" || answer == "y" || answer == "ya")
            {
                Player player = new Player(playerName, bank);
                Game game = new TwentyOneGame();
                game += player;
                player.isActivelyPlaying = true;
                while (player.isActivelyPlaying && player.Balance > 0)
                {
                    game.Play();
                }
                game -= player;
                Console.WriteLine("Thank you for playing!");
            }
            Console.WriteLine("Feel free to look around the casino. Bye for now.");
            //End Program
            Console.ReadKey();
        } 
    
    }
}
