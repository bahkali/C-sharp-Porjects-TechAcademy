﻿using System;
using Casino;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Casino.TwentyOne;
using System.Data.SqlClient;
using System.Data;

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
            bool validAnswer = false;

            Console.WriteLine(logo);

            DateTime dateTime = new DateTime();

            string file_path = @"c:\users\precisionm4800\desktop\c-sharp-projects-techacademy\logs\log.txt";
            //File.ReadAllText(File_path);

            Console.WriteLine("Welcome to the Grand Hotel and Casino.\nLet's start by telling me your name.");
            string playerName = Console.ReadLine();

            if(playerName.ToLower() == "admin")
            {
                List<ExceptionEntity> Exceptions = ReadExceptions();
                foreach(var exception in Exceptions)
                {
                    Console.Write(exception.Id + " | ");
                    Console.Write(exception.ExceptionType + " | ");
                    Console.Write(exception.ExceptionMessage+ " | ");
                    Console.Write(exception.TimeStamp + " | ");
                    Console.WriteLine();
                }
                Console.Read();
                return;
            }

            Console.WriteLine("And how much money did you bring today?");
            int bank = Convert.ToInt32(Console.ReadLine());

            //Check the input from the user
            while (!validAnswer) 
            {
                Console.WriteLine("And how much money did you bring today?");
                validAnswer = int.TryParse(Console.ReadLine(), out bank);
                if (!validAnswer) Console.WriteLine("Please enter digits only. no decimals.");
            }

            Console.WriteLine("Hello, {0}. Would you like to join a game of 21 right now?", playerName);
            string answer = Console.ReadLine().ToLower();

            if (answer == "yes" || answer == "yeah" || answer == "y" || answer == "ya")
            {
                Player player = new Player(playerName, bank);
                player.Id = Guid.NewGuid();
                
                using (StreamWriter file = new StreamWriter(file_path, true))
                {
                    file.WriteLine(player.Id);
                }
                Game game = new TwentyOneGame();
                game += player;
                player.isActivelyPlaying = true;
                while (player.isActivelyPlaying && player.Balance > 0)
                {
                    try
                    {
                        game.Play();
                    }
                    catch (FraudException ex)
                    {
                        Console.WriteLine(ex.Message);
                        UpdateDbWithException(ex);
                        Console.Read();
                        return;
                    }

                    catch (Exception ex)
                    {

                        Console.WriteLine("An error occurred. Please contact your system administrator.");
                        UpdateDbWithException(ex);
                        Console.Read();
                        return;
                    }
                    

                }
                game -= player;
                Console.WriteLine("Thank you for playing!");
            }
            Console.WriteLine("Feel free to look around the casino. Bye for now.");
            //End Program
            Console.ReadKey();
        }

        private static List<ExceptionEntity> ReadExceptions()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TwentyOneGame;
                                            Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                            TrustServerCertificate=False;ApplicationIntent=ReadWrite;
                                                MultiSubnetFailover=False";
            string queryString = @"SELECT Id, ExceptionType, ExceptionMessage, TimeStamp FROM Exceptions";
            List<ExceptionEntity> Execptions = new List<ExceptionEntity>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ExceptionEntity exception = new ExceptionEntity();
                    exception.Id = Convert.ToInt32(reader["id"]);
                    exception.ExceptionType = reader["ExceptionType"].ToString();
                    exception.ExceptionMessage = reader["ExceptionMessage"].ToString();
                    exception.TimeStamp = Convert.ToDateTime(reader["TimeStamp"]);
                    Execptions.Add(exception);
                }
                connection.Close();
            }
            return Execptions;
        }

        private static void UpdateDbWithException(Exception ex)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TwentyOneGame;
                                            Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                            TrustServerCertificate=False;ApplicationIntent=ReadWrite;
                                                MultiSubnetFailover=False";

            string queryString = @"INSERT INTO Exceptions (ExceptionType, ExceptionMessage, TimeStamp) VALUES
                                        (@ExceptionType, @ExceptionMessage, @TimeStamp)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@ExceptionType", SqlDbType.VarChar);
                command.Parameters.Add("@ExceptionMessage", SqlDbType.VarChar);
                command.Parameters.Add("@TimeStamp", SqlDbType.DateTime);

                command.Parameters["@ExceptionType"].Value = ex.GetType().ToString();
                command.Parameters["@ExceptionMessage"].Value = ex.Message;
                command.Parameters["@TimeStamp"].Value = DateTime.Now;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
