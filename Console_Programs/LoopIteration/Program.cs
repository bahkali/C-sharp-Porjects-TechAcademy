﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopIteration
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part One  -  Array Iteration
            string[] customerName = { "kaly", "Jessica", "Salihu" };

            Console.Write("Enter some text to append in the array: ");
            string str = Console.ReadLine();
            for(int i = 0; i < customerName.Length; i++)
            {
                customerName[i] = string.Concat(customerName[i] ,  str);   
            }
            // Display array list
            foreach( string e in customerName)
            {
                Console.WriteLine(e);
            }

            //Part Two Infinite Loop

            int num = 0;
            while (true)
            {
                // Fix the infinite loop
                if(num == 10) { break; }
                Console.Write(num + " ");
                num++;
                
            }
            Console.WriteLine();

            //Part Tree - Loop with condition
            int[] arr2 = { 2, 4, 5, 6, 8, 9 };
            Console.WriteLine("Even number from the array:");
            for (int i = 0; i < arr2.Length; i++){
                // print even number from the array
                if(arr2[i] % 2 == 0)
                {
                    Console.Write(arr2[i] + " ");
                }

            }
            Console.WriteLine();
            int attemp = 1;
            string secret = "boat";
            char holder;
            do {
                Console.WriteLine("Enter any letter that can be in the secret word: ");
                // Get only one character from user
                holder = Console.ReadKey().KeyChar;
                Console.WriteLine();
                // check if secret contains the character if not increment attemp
                if (secret.Contains(holder))
                {
                    Console.WriteLine($"Woow good work. the secret word is {secret}");
                    break;
                }
                else
                {
                    Console.WriteLine("Try again..");
                    attemp++;
                }
                
            } while (attemp <= 3);

            // Part Four 

            List<string> uniqueString = new List<string>() { "car", "plane", "bicycle" };
            Console.Write("Enter a text to search in the array \nHint: Type of vehicle: ");
            string str2 = Console.ReadLine();

            for(int i = 0; i < uniqueString.Count; i++)
            {
                if (uniqueString[i] == str2) { 
                    Console.WriteLine($"The index {i} contains the matching text {str2}");
                    break;
                }
                else { 
                    Console.WriteLine($"Sorry the word {str2} isn't in the array");
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine();

            // Part Five

            List<string> duplicatetring = new List<string>() { "car", "plane", "car", "bicycle" };
            Console.Write("Enter a text to search in the array \nHint: Type of vehicle: ");
            string str3 = Console.ReadLine();
            List<int> indices = new List<int>();
            // loop to find duplicate and add it to indices list.
            for (int i = 0; i < duplicatetring.Count; i++)
            {
                if (duplicatetring[i] == str3)
                {
                    indices.Add(i);
                }
                
            }
            // check if indices is empty
            if(indices.Capacity == 0)
            {
                Console.WriteLine($"Sorry the word {str3} isn't in the array");
            }else
            {
                Console.WriteLine("the indices are : ");
                foreach(int i in indices) { Console.WriteLine(i); }
            }

            // Part Six
            List<string> itemList = new List<string>() { "car", "plane", "car", "bicycle" };
            foreach (string s in itemList)
            {
                if (itemList.Any(t => t == s)) {
                    Console.WriteLine($"items {s} has a duplicate");
                }
                else
                {
                    Console.WriteLine($"items {s} doesn't have a duplicate.");
                }
            }

            // Before termination
            Console.WriteLine("Press any key to terminate the program.");
            Console.ReadKey();
        }
    }
}
