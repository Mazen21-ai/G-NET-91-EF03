using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Data;
using ConsoleApp1.Models;
using ConsoleApp1.Enums;

namespace ConsoleApp1.Operations
{
    public class OnlineLearningOperations
    {
        private readonly OnlineLearningContext context;

        public OnlineLearningOperations(OnlineLearningContext context)
        {
            this.context = context;
        }

    
        public void AddInstructor()
        {
            Instructor instructor = new Instructor
            {
                FullName = "Mohamed Ahmed",
                Email = "mohamed@example.com",
                Bio = "C# and Programming Instructor",
                JoinedAt = DateTime.Now
            };

            context.Instructors.Add(instructor);
            context.SaveChanges();

            Console.WriteLine("Instructor Added Successfully");
            Console.WriteLine("Instructor ID: " + instructor.Id);
        }

        public void AddCourse()
        {
            Console.Write("Enter Instructor ID: ");
            int instructorId = int.Parse(Console.ReadLine());

            Console.Write("Enter Category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Instructor instructor = context.Instructors.Find(instructorId);
            Category category = context.Categories.Find(categoryId);

            if (instructor == null)
            {
                Console.WriteLine("Instructor Not Found");
                return;
            }

            if (category == null)
            {
                Console.WriteLine("Category Not Found");
                return;
            }

            Course course = new Course
            {
                Title = "ASP.NET Core",
                Description = "Learning ASP.NET Core",
                Price = 700,
                Level = CourseLevel.Beginner,
                IsPublished = false,
                InstructorId = instructorId,
                CategoryId = categoryId
            };

            context.Courses.Add(course);
            context.SaveChanges();

            Console.WriteLine("Course Added Successfully");
            Console.WriteLine("Course ID: " + course.Id);
        }

        public void AddStudents()
        {
            Student student1 = new Student
            {
                FullName = "Ali Hassan",
                Email = "ali2@example.com",
                DateOfBirth = new DateTime(2003, 5, 10)
            };

            Student student2 = new Student
            {
                FullName = "Omar Ahmed",
                Email = "omar2@example.com",
                DateOfBirth = new DateTime(2002, 8, 15)
            };

            Student student3 = new Student
            {
                FullName = "Mona Samir",
                Email = "mona2@example.com",
                DateOfBirth = new DateTime(2004, 3, 20)
            };

            context.Students.AddRange(student1, student2, student3);
            context.SaveChanges();

            Console.WriteLine("Three Students Added Successfully");

            Console.WriteLine("Student 1 ID: " + student1.Id);
            Console.WriteLine("Student 2 ID: " + student2.Id);
            Console.WriteLine("Student 3 ID: " + student3.Id);
        }

        public void EnrollStudent()
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            Student student = context.Students.Find(studentId);
            Course course = context.Courses.Find(courseId);

            if (student == null)
            {
                Console.WriteLine("Student Not Found");
                return;
            }

            if (course == null)
            {
                Console.WriteLine("Course Not Found");
                return;
            }
            if (!course.IsPublished)
            {
                Console.WriteLine("Cannot Enroll. Course Is Not Published");
                return;
            }

            Enrollment existingEnrollment =
                context.Enrollments.Find(studentId, courseId);

            if (existingEnrollment != null)
            {
                Console.WriteLine("Student Already Enrolled");
                return;
            }

            Enrollment enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now,
                Progress = 0,
                Status = EnrollmentStatus.Active
            };

            context.Enrollments.Add(enrollment);
            context.SaveChanges();

            Console.WriteLine("Student Enrolled Successfully");
        }

       
        public void FindCourse()
        {
            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            Course course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course Not Found");
                return;
            }

            Console.WriteLine("Course Title: " + course.Title);

            Console.Write("Enter Course Title: ");
            string title = Console.ReadLine();

            Course courseByTitle = context.Courses
                .FirstOrDefault(c => c.Title == title);

            if (courseByTitle == null)
            {
                Console.WriteLine("Course Not Found");
                return;
            }

            Console.WriteLine("Course ID: " + courseByTitle.Id);
            Console.WriteLine("Course Price: " + courseByTitle.Price);
        }

        public void GetCoursesByPrice()
        {
            Console.Write("Enter Minimum Price: ");
            decimal minPrice = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Maximum Price: ");
            decimal maxPrice = decimal.Parse(Console.ReadLine());

            var courses = context.Courses
                .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
                .OrderByDescending(c => c.Price)
                .ToList();

            if (courses.Count == 0)
            {
                Console.WriteLine("No Courses Found");
                return;
            }

            foreach (Course course in courses)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("Title: " + course.Title);
                Console.WriteLine("Level: " + course.Level);
                Console.WriteLine("Price: " + course.Price);
            }
        }

        public void GetCategories()
        {
            Console.WriteLine("Subcategories:");

            var subCategories = context.Categories
                .Where(c => c.ParentCategoryId != null)
                .ToList();

            foreach (Category category in subCategories)
            {
                Console.WriteLine(category.Name);
            }

            Console.WriteLine("--------------------");
            Console.WriteLine("Top-Level Categories:");

            var topCategories = context.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToList();

            foreach (Category category in topCategories)
            {
                Console.WriteLine(category.Name);
            }
        }

        public void GetCourseStudents()
        {
            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            Course course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course Not Found");
                return;
            }
            var studentIds = context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.StudentId)
                .ToList();

            var students = context.Students
                .Where(s => studentIds.Contains(s.Id))
                .ToList();

            Console.WriteLine("Total Students: " + students.Count);

            foreach (Student student in students)
            {
                Console.WriteLine(student.FullName);
            }
        }

        

        public void UpdateCoursePrice()
        {
            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            Course course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course Not Found");
                return;
            }

            Console.Write("Enter New Price: ");
            decimal newPrice = decimal.Parse(Console.ReadLine());

            course.Price = newPrice;

            context.SaveChanges();

            Console.WriteLine("Course Price Updated Successfully");
        }

        public void UpdateEnrollmentProgress()
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            Enrollment enrollment =
                context.Enrollments.Find(studentId, courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment Not Found");
                return;
            }

            Console.Write("Enter New Progress: ");
            decimal progress = decimal.Parse(Console.ReadLine());

            if (progress < 0 || progress > 100)
            {
                Console.WriteLine("Progress Must Be Between 0 And 100");
                return;
            }

            enrollment.Progress = progress;

            if (progress == 100)
            {
                enrollment.Status = EnrollmentStatus.Completed;
            }

            context.SaveChanges();

            Console.WriteLine("Enrollment Progress Updated Successfully");
        }

        public void PublishCourse()
        {
            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            Course course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course Not Found");
                return;
            }

            course.IsPublished = true;
            course.PublishedAt = DateTime.Now;

            context.SaveChanges();

            Console.WriteLine("Course Published Successfully");
        }

        public void UnenrollStudent()
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            Enrollment enrollment =
                context.Enrollments.Find(studentId, courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment Not Found");
                return;
            }

            context.Enrollments.Remove(enrollment);
            context.SaveChanges();

            Console.WriteLine("Student Unenrolled Successfully");
        }

        // Delete Category
        public void DeleteCategory()
        {
            Console.Write("Enter Category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Category category = context.Categories.Find(categoryId);
            if (category == null)
            {
                Console.WriteLine("Category Not Found");
                return;
            }

            try
            {
                context.Categories.Remove(category);
                context.SaveChanges();

                Console.WriteLine("Category Deleted Successfully");
            }
            catch (DbUpdateException)
            {
                context.Entry(category).State = EntityState.Unchanged;

                bool hasSubCategories = context.Categories
                    .Any(c => c.ParentCategoryId == categoryId);

                if (hasSubCategories)
                {
                    Console.WriteLine(
                        "Cannot Delete Category Because It Has Subcategories");
                }
                else
                {
                    Console.WriteLine(
                        "Category Cannot Be Deleted Because It Is Related To Other Records");
                }
            }
        }

      
        public void DeleteCourse()
        {
            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            Course course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course Not Found");
                return;
            }

            int enrollmentsBefore = context.Enrollments
                .Count(e => e.CourseId == courseId);

            Console.WriteLine(
                "Enrollments Before Delete: " + enrollmentsBefore);

            context.Courses.Remove(course);
            context.SaveChanges();

            int enrollmentsAfter = context.Enrollments
                .Count(e => e.CourseId == courseId);

            Console.WriteLine(
                "Enrollments After Delete: " + enrollmentsAfter);

            Console.WriteLine("Course Deleted Successfully");

            
        }
    }
}
