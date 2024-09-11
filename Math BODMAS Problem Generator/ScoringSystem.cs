using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

/*
   Handles scoring and reporting for the arithmetic quiz application.
   Tracks the number of correct answers for both simple and advanced questions,
   and generates a report file with the student's results and grade.
 */

namespace Math_BODMAS_Problem_Generator
{
    public class ScoringSystem
    {
        private int correctSimpleAnswers = 0;
        private int correctAdvancedAnswers = 0;
        private Dictionary<string, int> correctAnswersByType = new Dictionary<string, int>();
        
        
        //Initialises the correct answer count for each arithmetic operation.
        public ScoringSystem()
        {
            foreach (string op in new string[] { "+", "-", "*", "/" })
            {
                correctAnswersByType[op] = 0;
            }
        }


        /*
            Processes student answers, updates scores, and classifies answers
            by operation type.
        */
        public void SubmitAnswer(string question, double studentAnswer, bool isBasic)
        {
            double correctAnswer = CalculateAnswer(question);
            if (Math.Abs(studentAnswer - correctAnswer) < 0.01)
            {
                if (isBasic)
                {
                    correctSimpleAnswers++;
                    string operation = question.Split(' ')[1];
                    correctAnswersByType[operation]++;
                }
                else
                {
                    correctAdvancedAnswers++;
                }
            }
        }


        /*
            Calculates and returns the overall grade based on the total number of correct answers.
            Grades are categorised as 'Fail', 'Pass', 'Merit', or 'Distinction'.
        */
        public string GetGrade()
        {
            int totalCorrect = correctSimpleAnswers + correctAdvancedAnswers;
            if (totalCorrect <= 4) return "Fail";
            else if (totalCorrect <= 10) return "Pass";
            else if (totalCorrect <= 16) return "Merit";
            else return "Distinction";
        }

        // Generates and saves a report as a text file
        public void GenerateReport(string studentName)
        {
            string reportContent = "Results for " + studentName + ":\n" + GetResults() + "\nGrade: " + GetGrade();
            File.WriteAllText(studentName + "_report.txt", reportContent);
        }



        //  Calculates the correct answer for a given arithmetic question.
        //  The function parses the question, processes it according to arithmetic rules,
        //  and returns the calculated answer.
        private double CalculateAnswer(string question)
        {
            // Tokenize the expression
            string[] tokens = question.Split(' ');
            List<double> numbers = new List<double>();
            List<char> operations = new List<char>();

            // Separate numbers and operations
            foreach (string token in tokens)
            {
                if (Regex.IsMatch(token, @"-?\d+(\.\d+)?")) // Regex to check if token is a number
                {
                    numbers.Add(double.Parse(token));
                }
                else
                {
                    operations.Add(token[0]);
                }
            }

            // First pass for multiplication and division
            for (int i = 0; i < operations.Count;)
            {
                if (operations[i] == '*' || operations[i] == '/')
                {
                    double result = operations[i] == '*' ? numbers[i] * numbers[i + 1] : numbers[i] / numbers[i + 1];
                    numbers[i] = result;
                    numbers.RemoveAt(i + 1);
                    operations.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            // Second pass for addition and subtraction
            double answer = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                answer = operations[i - 1] == '+' ? answer + numbers[i] : answer - numbers[i];
            }

            return answer;
        }

        public string GetResults()
        {
            StringBuilder results = new StringBuilder();
            results.Append("Total Correct Answers: ").Append(correctSimpleAnswers + correctAdvancedAnswers)
                   .Append("\nCorrect Simple Answers: ").Append(correctSimpleAnswers)
                   .Append("\nCorrect Advanced Answers: ").Append(correctAdvancedAnswers)
                   .Append("\nDetailed Results:\n");
            foreach (var entry in correctAnswersByType)
            {
                results.Append("Correct ").Append(entry.Key).Append(" Answers: ").Append(entry.Value).Append("\n");
            }
            return results.ToString();
        }
    }
}