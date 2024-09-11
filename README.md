# Math-BODMAS-Problem-Generator
 A console application quiz created with C# and .NET that generates 20 random arithmetic questions. The first 10 focus on basic operations, while the last 10 introduce BODMAS concepts with more complex expressions. This tool can help primary school students practice math questions.


Discussing my approach for this application

My solution contains three classes, Program.cs, ScoringSystem.cs and QuestionGenerator.cs

The approach taken in the QuestionGenerator class for generating arithmetic questions is both systematic and thoughtful. For the first ten questions, the focus is on basic arithmetic operations: addition, subtraction, multiplication, and division. To ensure a balanced mix of these operations, the class uses a queue (operationsQueue) filled with each operation type, guaranteeing that at least two questions of each type are presented. The numbers used in these questions are randomly generated within the range of 1-30, using the Random class . Special attention is given to division questions to ensure that they result in whole numbers, making them more suitable for primary school students. This is achieeved by adjusting the dividend to be a multiple of the divisor.

The next ten questions delve into the BODMAS rule, Which requires a more calculated approach to ensure the arithmetic expressions are both challenging and within the capabilities of the students. The BODMAS questionss are designed to be more complex, involving three randomly generated numbers and two operations. The class again employs the Random class to ensure a diverse range of questions. A critical aspect of this design is the adjustment of numbers in division operations. The method adjustForDivision ensures that division results in whole numbers and that the operands do not exceed the maximum value of 50. 

The ScoringSystem class in this arithmetic quiz application is a crucial component designed to evaluate and report the performance of students.The first part of the class's functionality involves processing student answers. As students respond to each question, their answers are evaluated against the correct ones, calculated using the calculateAnswer method. This method is a piece of logic that interprets the arithmetic question strng, separates the numbers and operations, and computes the correct answer following the BODMAS rule, By ensuring that it first takes in the multiplication and division section and then the addition and subtraction operations it adheres to the rules set by BODMAS.

The Program class is responsible for running the console application and ensuring that students enter valid numeric responses. The class goes through 20 arithmetic questions generated by the QuestionGenerator, asking for and validating student responses using Console.ReadLine() and double.TryParse() for input validation. Following the quiz, it displays the results, including a breakdown of the student's performance and a final grade. Additionally, it generates a detailed report for teachers, which is saved to a file for further review.
