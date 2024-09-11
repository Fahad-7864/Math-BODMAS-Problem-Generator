using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


/*
   Main class for the arithmetic quiz application.
   This class handles user interaction, question presentation, answer collection,
   and displays the results. It also initiates the generation of a report for the teacher.
 */


namespace Math_BODMAS_Problem_Generator
{

    internal class Program
    {

        static void Main(string[] args)
            {
                Console.Write("Enter your name: ");
                string studentName = Console.ReadLine();

                QuestionGenerator questionGenerator = new QuestionGenerator();
                ScoringSystem scoringSystem = new ScoringSystem();

                // Test question
                Console.WriteLine("Test Question: 5 + 3");
                Console.Write("Your answer: ");
                if (double.TryParse(Console.ReadLine(), out _))
                {
                    // Discard the answer as highlighted by _ 
                }

                // Process answers for 20 questions
                for (int i = 0; i < 20; i++)
                {
                    string question = (i < 10) ? questionGenerator.GenerateSimpleQuestion() : questionGenerator.GenerateBODMASQuestion();
                    double answer;
                    while (true)
                    {
                        Console.WriteLine("Question " + (i + 1) + ": " + question);
                        Console.Write("Your answer: ");
                        if (double.TryParse(Console.ReadLine(), out answer))
                        {
                            break; // valid input
                        }
                        else
                        {
                            Console.WriteLine("Please only use numbers when attempting this test.");
                        }
                    }
                    scoringSystem.SubmitAnswer(question, answer, i < 10);
                }

                // Display student results
                Console.WriteLine("\nResults for " + studentName + ":");
                Console.WriteLine(scoringSystem.GetResults());

                // Generate and save the report for the teacher
                try
                {
                    scoringSystem.GenerateReport(studentName);
                    Console.WriteLine("Teacher's report generated for " + studentName + ".");
                }
                catch (IOException e)
                {
                    Console.WriteLine("An error occurred while generating the report: " + e.Message);
                }
            }
        }
    }






/*
 * Main that has navigation but no report generation and has error where it cannot finish the test
 * 
 * 
 * static void Main(string[] args)
{
    Console.Write("Enter your name: ");
    string studentName = Console.ReadLine();

    QuestionGenerator questionGenerator = new QuestionGenerator();
    ScoringSystem scoringSystem = new ScoringSystem();

    var questions = new List<string>();
    var answers = new Dictionary<int, double>();
    for (int i = 0; i < 20; i++)
    {
        string question = (i < 10) ? questionGenerator.GenerateSimpleQuestion() : questionGenerator.GenerateBODMASQuestion();
        questions.Add(question);
    }

    int currentQuestion = 0;
    while (currentQuestion < questions.Count)
    {
        Console.Clear();
        Console.WriteLine($"Question {currentQuestion + 1}/{questions.Count}: {questions[currentQuestion]}");
        Console.Write("Your answer (leave blank to skip): ");
        string input = Console.ReadLine();

        if (double.TryParse(input, out double answer))
        {
            answers[currentQuestion] = answer;
        }

        Console.WriteLine("Press N for next question, P for previous question, or any other key to stay.");
        var key = Console.ReadKey(true).Key;

        if (key == ConsoleKey.N && currentQuestion < questions.Count - 1)
            currentQuestion++;
        else if (key == ConsoleKey.P && currentQuestion > 0)
            currentQuestion--;
    }

    // Process answers
    for (int i = 0; i < questions.Count; i++)
    {
        if (answers.ContainsKey(i))
        {
            scoringSystem.SubmitAnswer(questions[i], answers[i], i < 10);
        }
    }

    // Display student results
    Console.Clear();
    Console.WriteLine("\nResults for " + studentName + ":");
    Console.WriteLine(scoringSystem.GetResults());

    // Generate and save the report for the teacher
    try
    {
        scoringSystem.GenerateReport(studentName);
        Console.WriteLine("Teacher's report generated for " + studentName + ".");
    }
    catch (IOException e)
    {
        Console.WriteLine("An error occurred while generating the report: " + e.Message);
    }

    Console.WriteLine("Press any key to exit.");
    Console.ReadKey();
}

 * */

