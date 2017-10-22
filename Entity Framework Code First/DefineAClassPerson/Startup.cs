using System;
using System.Linq;

namespace DefineAClassPerson
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
        private static void MathUtilities()
        {
            string input = "";

            while (input != "End")
            {
                input = Console.ReadLine();
                if (input != "End")
                {
                    string[] arg =
                input.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries).ToArray();
                    double arg1 = double.Parse(arg[1]);
                    double arg2 = double.Parse(arg[2]);
                    double result = 0;
                    switch (arg[0])
                    {
                        case "Sum":
                            result = MathUtilities.Sum(arg1, arg2); break;
                        case "Subtract":
                            result = MathUtilities.Subtract(arg1, arg2); break;
                        case "Multiply":
                            result = MathUtilities.Multiply(arg1, arg2); break;
                        case "Divide":
                            result = MathUtilities.Divide(arg1, arg2); break;
                        case "Percentage":
                            result = MathUtilities.Percentage(arg1, arg2); break;

                    }
                    Console.WriteLine("{0:0.00}", result);
                }
            }
        }
        private static void PlanckConstant()
        {
            Console.WriteLine(Calculation.Result());
        }
        private static void Students()
        {
            string input = Console.ReadLine();

            while (input != "End")
            {
                Student student = new Student(input);

                input = Console.ReadLine();
            }
            Console.WriteLine(Student.count);
        }
        private static void OldestFamilyMember()
        {
            Console.Write("Enter number of people: ");
            int n = int.Parse(Console.ReadLine());

            Family f = new Family();
            Console.WriteLine("Enter name and age: ");
            for (int i = 0; i < n; i++)
            {
                string[] inputArgs = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                Person p = new Person(inputArgs[0], int.Parse(inputArgs[1]));
                f.AddMember(p);
            }
            Console.WriteLine($"{f.GetOldestPerson().Name} {f.GetOldestPerson().Age}");
        }
        private static void CreatePersonConstructor()
        {
            string[] inputArgs = Console.ReadLine().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (inputArgs.Length == 0)
            {
                Person p = new Person();
                Console.WriteLine($"{p.Name} {p.Age}");
            }
            else if (inputArgs.Length == 1)
            {
                string argument = inputArgs[0];
                int age = -1;
                if (int.TryParse(argument, out age))
                {
                    Person p = new Person(age);
                    Console.WriteLine($"{p.Name} {p.Age}");
                }
                else
                {
                    Person p = new Person(argument);
                    Console.WriteLine($"{p.Name} {p.Age}");
                }
            }
            else if (inputArgs.Length == 2)
            {
                string name = inputArgs[0];
                int age = int.Parse(inputArgs[1]);
                Person p = new Person(name, age);
                Console.WriteLine($"{p.Name} {p.Age}");
            }
        }
        private static void CreatePerson()
        {
            Person person1 = new Person();
            Person person2 = new Person();
            Person person3 = new Person();
            person1.Name = "Pesho";
            person1.Age = 20;
            person2.Name = "Gosho";
            person2.Age = 18;
            person3.Name = "Stamat";
            person3.Age = 43;

            Console.WriteLine($"{person1.Name} {person1.Age}");
            Console.WriteLine($"{person2.Name} {person2.Age}");
            Console.WriteLine($"{person3.Name} {person3.Age}");
        }
    }
}
