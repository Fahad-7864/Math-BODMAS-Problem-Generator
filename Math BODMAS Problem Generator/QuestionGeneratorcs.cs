using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*
   QuestionGenerator class is responsible for generating arithmetic questions.
   It provides a mix of simple and BODMAS questions, with adjustments to ensure
   whole number results for division operations.
 */

namespace Math_BODMAS_Problem_Generator
{
    public class QuestionGenerator
    {
        private Random random = new Random();
        private Queue<string> operationsQueue = new Queue<string>();

        // Constructor prepares the operation queue to ensure a balanced mix of operations.
        public QuestionGenerator()
        {
            // Ensure at least 2 of each operation type in the first 10 questions
            foreach (string op in new string[] { "+", "-", "*", "/" })
            {
                operationsQueue.Enqueue(op);
                operationsQueue.Enqueue(op);
            }
        }


        /*
            Generates a simple arithmetic question with operation in the range of 1-30.
            Ensures at least 2 questions of each operation type (+, -, *, /) in the first 10 questions.
            For division, adjusts the operands to ensure the result is a whole number.
            return A string representing a simple arithmetic question.
        */
        public string GenerateSimpleQuestion()
        {
            int num1, num2;
            string operation;

            if (operationsQueue.Count > 0)
            {
                operation = operationsQueue.Dequeue();
            }
            else
            {
                string[] operations = { "+", "-", "*", "/" };
                operation = operations[random.Next(operations.Length)];
            }

            do
            {
                num1 = random.Next(1, 31); // Range 1-30
                num2 = random.Next(1, 31); // Range 1-30

                if (operation == "/")
                {
                    // Adjust num1 to be a multiple of num2 for division, ensuring whole number result
                    num1 = num2 * (random.Next(1, 16)); // Range 1-15 for divisor, adjusting dividend
                }
            } while (num1 < 10 && num2 < 10 && num1 == num2); // Ensures different numbers for values under 10

            return num1 + " " + operation + " " + num2;
        }



         /*
           Generates a random arithmetic question following the BODMAS rule.
           Ensures division operations produce whole numbers and numbers are within the range of 1 to 50.
           This returns A string representing a BODMAS arithmetic question.
         */
        public string GenerateBODMASQuestion()
        {
            int num1, num2, num3;
            string operation1, operation2;

            string[] operations = { "+", "-", "*", "/" };
            operation1 = operations[random.Next(operations.Length)];
            operation2 = operations[random.Next(operations.Length)];
            // Adjust for division to ensure whole number result and within range

            do
            {
                num1 = random.Next(1, 51);  // Range 1-50
                num2 = random.Next(1, 51);  // Range 1-50
                num3 = random.Next(1, 51);  // Range 1-50

                if (operation1 == "/")
                {
                    num1 = AdjustForDivision(num1, num2); // Adjust num1
                }
                if (operation2 == "/")
                {
                    if (operation1 == "*" || operation1 == "/")
                    {
                        num2 = AdjustForDivision(num2, num3);
                    }
                    else
                    {
                        num3 = AdjustForDivision(num3, num2);
                    }
                }
            } while (num1 > 50 || num2 > 50 || num3 > 50);

            return num1 + " " + operation1 + " " + num2 + " " + operation2 + " " + num3;
        }
        /*
         * The adjustForDivision method is introduced to adjust the dividend
           so that the division operation results in a whole number,
           and the dividend does not exceed 50.
*/
        private int AdjustForDivision(int dividend, int divisor)
        {
            while (dividend % divisor != 0 || dividend > 50)
            {
                dividend = random.Next(1, 51);
            }
            return dividend;
        }
    }
}
