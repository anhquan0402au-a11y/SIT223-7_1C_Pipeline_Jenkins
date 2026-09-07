using System;

namespace DuplicateCode
{
    class RevisedCode
    {
        const int MaxTaskLength = 30;

        const int ColumnWidth = 30;

        static void Main(string[] args)
        {
            string[] personalTasks = new string[0];
            string[] workTasks = new string[0];
            string[] familyTasks = new string[0];

            bool running = true;

            while (running)
            {
                DisplayTasks(personalTasks, workTasks, familyTasks);

                string listName = AskCategory();

                if (listName == null || listName == "exit")
                {
                    running = false;
                    continue;
                }

                string task = AskTask();

                if (task == null)
                {
                    running = false;
                    continue;
                }

                if (listName == "personal")
                {
                    personalTasks = AddTask(personalTasks, task);
                }
                else if (listName == "work")
                {
                    workTasks = AddTask(workTasks, task);
                }
                else
                {
                    familyTasks = AddTask(familyTasks, task);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Goodbye!");
        }

        static string[] AddTask(string[] tasks, string newTask)
        {
            string[] extended = new string[tasks.Length + 1];

            for (int i = 0; i < tasks.Length; i++)
            {
                extended[i] = tasks[i];
            }

            extended[extended.Length - 1] = newTask;

            return extended;
        }

        static void DisplayTasks(string[] personalTasks, string[] workTasks, string[] familyTasks)
        {
            TryClear();

            int max = MaxOfThree(personalTasks.Length, workTasks.Length, familyTasks.Length);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(new string(' ', 12) + "CATEGORIES");
            Console.WriteLine(new string(' ', 10) + new string('-', 94));
            Console.WriteLine("{0,10}|{1,30}|{2,30}|{3,30}|", "item #", "Personal", "Work", "Family");
            Console.WriteLine(new string(' ', 10) + new string('-', 94));

            for (int i = 0; i < max; i++)
            {
                Console.Write("{0,10}|", i);
                PrintCell(personalTasks, i);
                PrintCell(workTasks, i);
                PrintCell(familyTasks, i);
                Console.WriteLine();
            }

            Console.ResetColor();
        }

        static void PrintCell(string[] tasks, int i)
        {
            string text;
            if (tasks.Length > i)
                text = tasks[i];
            else
                text = "N/A";

            if (text.Length > ColumnWidth)
                text = text.Substring(0, ColumnWidth);

            Console.Write("{0," + ColumnWidth + "}|", text);
        }


        static string AskCategory()
        {
            while (true)
            {
                Console.WriteLine("\nWhich category do you want to place a new task? " + "Type 'Personal', 'Work' or 'Family' ('Exit' to quit).");
                Console.Write(">> ");

                string answer = Console.ReadLine();

                if (answer == null)
                    return null;                      

                answer = answer.Trim().ToLowerInvariant();

                switch (answer)
                {
                    case "personal":
                    case "work":
                    case "family":
                    case "exit":
                        return answer;
                }

                Console.WriteLine("  '" + answer + "' is not a known category. Please try again.");
            }
        }

        static string AskTask()
        {
            while (true)
            {
                Console.WriteLine("Describe your task below (max. " + MaxTaskLength + " symbols).");
                Console.Write(">> ");

                string task = Console.ReadLine();

                if (task == null)
                    return null;                       

                task = task.Trim();

                if (task.Length == 0)
                {
                    Console.WriteLine("The description cannot be empty. Please try again.");
                    continue;                       
                }

                if (task.Length > MaxTaskLength)
                {
                    task = task.Substring(0, MaxTaskLength);
                    Console.WriteLine("The description was shortened to " + MaxTaskLength + " characters.");
                }

                return task;
            }
        }

        static int MaxOfThree(int a, int b, int c)
        {
            int max;
            if (a > b)
                max = a;
            else
                max = b;

            if (c > max)
                max = c;

            return max;
        }

        static void TryClear()
        {
            try
            {
                Console.Clear();
            }
            catch (Exception)
            {
                Console.WriteLine();
            }
        }
    }
}