using System;
using ConsoleApp1.Data;
using ConsoleApp1.Operations;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using OnlineLearningContext context =
                new OnlineLearningContext();

            OnlineLearningOperations operations =
                new OnlineLearningOperations(context);

            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("    ONLINE LEARNING PLATFORM");
                Console.WriteLine("================================");

                Console.WriteLine("\nCREATE OPERATIONS");
                Console.WriteLine("1. Add Instructor");
                Console.WriteLine("2. Add Course");
                Console.WriteLine("3. Add Students");
                Console.WriteLine("4. Enroll Student");

                Console.WriteLine("\nREAD OPERATIONS");
                Console.WriteLine("5. Find Course");
                Console.WriteLine("6. Get Courses By Price");
                Console.WriteLine("7. Get Categories");
                Console.WriteLine("8. Get Course Students");

                Console.WriteLine("\nUPDATE OPERATIONS");
                Console.WriteLine("9. Update Course Price");
                Console.WriteLine("10. Update Enrollment Progress");
                Console.WriteLine("11. Publish Course");

                Console.WriteLine("\nDELETE OPERATIONS");
                Console.WriteLine("12. Unenroll Student");
                Console.WriteLine("13. Delete Category");
                Console.WriteLine("14. Delete Course");

                Console.WriteLine("\n0. Exit");

                Console.Write("\nEnter your choice: ");
                string choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1":
                        operations.AddInstructor();
                        break;

                    case "2":
                        operations.AddCourse();
                        break;

                    case "3":
                        operations.AddStudents();
                        break;

                    case "4":
                        operations.EnrollStudent();
                        break;

                    case "5":
                        operations.FindCourse();
                        break;

                    case "6":
                        operations.GetCoursesByPrice();
                        break;

                    case "7":
                        operations.GetCategories();
                        break;

                    case "8":
                        operations.GetCourseStudents();
                        break;

                    case "9":
                        operations.UpdateCoursePrice();
                        break;

                    case "10":
                        operations.UpdateEnrollmentProgress();
                        break;

                    case "11":
                        operations.PublishCourse();
                        break;

                    case "12":
                        operations.UnenrollStudent();
                        break;

                    case "13":
                        operations.DeleteCategory();
                        break;

                    case "14":
                        operations.DeleteCourse();
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Application closed.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
                if (running)
                {
                    Console.WriteLine("\nPress Enter to return to the menu...");
                    Console.ReadLine();
                }
            }
        }
    }
}




