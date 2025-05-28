using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeePromotionApp
{
    class Employee : IComparable<Employee>
    {
        int id, age;
        string name;
        double salary;

        public Employee()
        {
        }

        public Employee(int id, int age, string name, double salary)
        {
            this.id = id;
            this.age = age;
            this.name = name;
            this.salary = salary;
        }

        public void TakeEmployeeDetailsFromUser()
        {
            Console.WriteLine("Please enter the employee ID");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter the employee name");
            name = Console.ReadLine();
            Console.WriteLine("Please enter the employee age");
            age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter the employee salary");
            salary = Convert.ToDouble(Console.ReadLine());
        }

        public override string ToString()
        {
            return "Employee ID : " + id + "\nName : " + name + "\nAge : " + age + "\nSalary : " + salary;
        }

        public int Id { get => id; set => id = value; }
        public int Age { get => age; set => age = value; }
        public string Name { get => name; set => name = value; }
        public double Salary { get => salary; set => salary = value; }

        public int CompareTo(Employee other)
        {
            if (other == null) return 1;
            return this.salary.CompareTo(other.salary);
        }
    }

    class EmployeePromotion
    {
        List<string> promotionList = new List<string>();

        Dictionary<int, Employee> employeeDict = new Dictionary<int, Employee>();

        public void InputPromotionList()
        {
            Console.WriteLine("Please enter the employee names in the order of their eligibility for promotion(Please enter blank to stop)");

            while (true)
            {
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                    break;
                promotionList.Add(name);
            }
        }

        public void FindPromotionPosition()
        {
            Console.WriteLine("Please enter the name of the employee to check promotion position");
            string searchName = Console.ReadLine();
            int index = promotionList.IndexOf(searchName);
            if (index == -1)
                Console.WriteLine($"Employee \"{searchName}\" not found in promotion list.");
            else
                Console.WriteLine($"\"{searchName}\" is the the position {index + 1} for promotion.");
        }

        public void TrimPromotionListCapacity()
        {
            Console.WriteLine($"The current size of the collection is {promotionList.Capacity}");
            promotionList.TrimExcess();
            Console.WriteLine($"The size after removing the extra space is {promotionList.Capacity}");
        }

        public void PrintPromotionListSorted()
        {
            Console.WriteLine("Promoted employee list:");
            promotionList.Sort(); 
            foreach (var name in promotionList)
                Console.WriteLine(name);
        }

        public void InputEmployeesToDictionary()
        {
            Console.WriteLine("Enter employee details (ID must be unique). Enter 0 for ID to stop.");
            while (true)
            {
                Employee emp = new Employee();
                Console.WriteLine("Enter Employee ID (0 to stop):");
                int id;
                bool isValidId = int.TryParse(Console.ReadLine(), out id);
                if (!isValidId || id < 0)
                {
                    Console.WriteLine("Invalid ID, try again.");
                    continue;
                }
                if (id == 0) break;
                if (employeeDict.ContainsKey(id))
                {
                    Console.WriteLine("Employee ID already exists, try again.");
                    continue;
                }
                emp.Id = id;

                Console.WriteLine("Enter Employee Name:");
                emp.Name = Console.ReadLine();

                Console.WriteLine("Enter Employee Age:");
                int age;
                if (!int.TryParse(Console.ReadLine(), out age))
                {
                    Console.WriteLine("Invalid age, try again.");
                    continue;
                }
                emp.Age = age;

                Console.WriteLine("Enter Employee Salary:");
                double salary;
                if (!double.TryParse(Console.ReadLine(), out salary))
                {
                    Console.WriteLine("Invalid salary, try again.");
                    continue;
                }
                emp.Salary = salary;

                employeeDict.Add(id, emp);
            }
        }

        public List<Employee> GetEmployeesSortedBySalary()
        {
            List<Employee> empList = employeeDict.Values.ToList();
            empList.Sort(); 
            return empList;
        }

        public void FindEmployeeById()
        {
            Console.WriteLine("Enter the employee ID to find:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            var emp = employeeDict.Values.Where(e => e.Id == id).FirstOrDefault();
            if (emp == null)
                Console.WriteLine($"Employee with ID {id} not found.");
            else
                Console.WriteLine(emp);
        }

        public void FindEmployeesByName()
        {
            Console.WriteLine("Enter the employee name to search:");
            string name = Console.ReadLine();
            var employees = employeeDict.Values.Where(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (employees.Count == 0)
                Console.WriteLine($"No employees found with the name \"{name}\".");
            else
            {
                Console.WriteLine($"Employees with name \"{name}\":");
                foreach (var emp in employees)
                    Console.WriteLine(emp + "\n");
            }
        }

        public void FindEmployeesElderThanGivenEmployee()
        {
            Console.WriteLine("Enter employee ID to find employees elder than this employee:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            if (!employeeDict.ContainsKey(id))
            {
                Console.WriteLine("Employee ID not found.");
                return;
            }

            int age = employeeDict[id].Age;
            var elders = employeeDict.Values.Where(e => e.Age > age).ToList();

            if (elders.Count == 0)
                Console.WriteLine($"No employees elder than employee with ID {id}.");
            else
            {
                Console.WriteLine($"Employees elder than employee with ID {id}:");
                foreach (var emp in elders)
                    Console.WriteLine(emp + "\n");
            }
        }

        // Menu-driven application to manage employee dictionary
        public void EmployeeManagementMenu()
        {
            while (true)
            {
                Console.WriteLine("\nEmployee Management Menu:");
                Console.WriteLine("1. Print all employee details");
                Console.WriteLine("2. Add an employee");
                Console.WriteLine("3. Modify employee details");
                Console.WriteLine("4. Print employee details by ID");
                Console.WriteLine("5. Delete an employee");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        PrintAllEmployees();
                        break;
                    case "2":
                        AddEmployee();
                        break;
                    case "3":
                        ModifyEmployee();
                        break;
                    case "4":
                        PrintEmployeeById();
                        break;
                    case "5":
                        DeleteEmployee();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        private void PrintAllEmployees()
        {
            if (employeeDict.Count == 0)
            {
                Console.WriteLine("No employees to display.");
                return;
            }
            Console.WriteLine("All Employee Details:");
            foreach (var emp in employeeDict.Values)
            {
                Console.WriteLine(emp);
                Console.WriteLine("------------------");
            }
        }

        private void AddEmployee()
        {
            Employee emp = new Employee();

            Console.WriteLine("Enter Employee ID:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
            {
                Console.WriteLine("Invalid ID.");
                return;
            }
            if (employeeDict.ContainsKey(id))
            {
                Console.WriteLine("Employee ID already exists.");
                return;
            }
            emp.Id = id;

            Console.WriteLine("Enter Employee Name:");
            emp.Name = Console.ReadLine();

            Console.WriteLine("Enter Employee Age:");
            int age;
            if (!int.TryParse(Console.ReadLine(), out age) || age <= 0)
            {
                Console.WriteLine("Invalid Age.");
                return;
            }
            emp.Age = age;

            Console.WriteLine("Enter Employee Salary:");
            double salary;
            if (!double.TryParse(Console.ReadLine(), out salary) || salary < 0)
            {
                Console.WriteLine("Invalid Salary.");
                return;
            }
            emp.Salary = salary;

            employeeDict.Add(id, emp);
            Console.WriteLine("Employee added successfully.");
        }

        private void ModifyEmployee()
        {
            Console.WriteLine("Enter Employee ID to modify:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }
            if (!employeeDict.ContainsKey(id))
            {
                Console.WriteLine("Employee ID not found.");
                return;
            }

            Employee emp = employeeDict[id];
            Console.WriteLine("Modify details for Employee (ID cannot be changed):");

            Console.WriteLine($"Current Name: {emp.Name}. Enter new name or press Enter to keep:");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                emp.Name = input;

            Console.WriteLine($"Current Age: {emp.Age}. Enter new age or press Enter to keep:");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (int.TryParse(input, out int newAge) && newAge > 0)
                    emp.Age = newAge;
                else
                {
                    Console.WriteLine("Invalid Age input. Keeping previous age.");
                }
            }

            Console.WriteLine($"Current Salary: {emp.Salary}. Enter new salary or press Enter to keep:");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (double.TryParse(input, out double newSalary) && newSalary >= 0)
                    emp.Salary = newSalary;
                else
                {
                    Console.WriteLine("Invalid Salary input. Keeping previous salary.");
                }
            }

            Console.WriteLine("Employee details updated.");
        }

        private void PrintEmployeeById()
        {
            Console.WriteLine("Enter Employee ID to print details:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }
            if (!employeeDict.ContainsKey(id))
            {
                Console.WriteLine("Employee ID not found.");
                return;
            }
            Console.WriteLine(employeeDict[id]);
        }

        private void DeleteEmployee()
        {
            Console.WriteLine("Enter Employee ID to delete:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }
            if (!employeeDict.ContainsKey(id))
            {
                Console.WriteLine("Employee ID not found.");
                return;
            }
            employeeDict.Remove(id);
            Console.WriteLine("Employee deleted successfully.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EmployeePromotion ep = new EmployeePromotion();

            ep.InputPromotionList();
            ep.FindPromotionPosition();
            ep.TrimPromotionListCapacity();
            ep.PrintPromotionListSorted();

            ep.InputEmployeesToDictionary();

            Console.WriteLine("\nEmployees sorted by salary:");
            var sortedList = ep.GetEmployeesSortedBySalary();
            foreach (var emp in sortedList)
                Console.WriteLine(emp + "\n");

            ep.FindEmployeeById();
            ep.FindEmployeesByName();
            ep.FindEmployeesElderThanGivenEmployee();

            ep.EmployeeManagementMenu();

            Console.WriteLine("Program ended.");
        }
    }
}
