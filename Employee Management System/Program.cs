namespace Employee_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // list to store employee data
            List<Employee> employees = new List<Employee>();
            EmployeeManager employeeManager = new EmployeeManager(employees);

            // method to get user input for department choice
            static Department GetDepartmentChoice()
            {
                Console.WriteLine("Choose a department:\n[1] Finance\n[2] HR\n[3] IT");
                Console.Write("Enter the department number (1-3): ");

                int deptNumber;
                while (!int.TryParse(Console.ReadLine(), out deptNumber) || deptNumber < 1 || deptNumber > 3)
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid number.");
                    Console.Write("Enter the department number (1-3): ");
                }
                // convert department number to Department enum
                return (Department)deptNumber - 1;
            }

            // method to get employee information from user
            static Employee getEmployeeInfo()
            {
                Console.Write("\nEnter First Name: ");
                string firstName = Console.ReadLine();

                Console.Write("Enter Last Name: ");
                string lastName = Console.ReadLine();

                Console.Write("Enter salary: ");
                double salary;
                while (!double.TryParse(Console.ReadLine(), out salary))
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid salary.");
                    Console.Write("\nEnter salary: ");
                }

                // get department choice from user
                Department dept = GetDepartmentChoice();

                Console.WriteLine();
                // create and return a new Employee Object
                return new Employee(firstName, lastName, salary, dept);
            }

            int choice = 0;
            // main menu loop
            while (choice != 7)
            {
                Console.WriteLine("Employee Management System");
                Console.WriteLine("[1] Add Employee\n[2] Remove Employee\n[3] Display Employees\n[4] Display Total Salary\n[5] Assign Employee to Department\n[6] Display Total Employees\n[7] Exit");
                Console.Write("\nEnter choice: ");
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number (1-7).");
                    Console.Write("\nEnter choice: ");
                }
                
                switch (choice)
                {
                    case 1:
                        // add new employee
                        employeeManager.AddEmployee(getEmployeeInfo());
                        break;
                    case 2:
                        // remove employee
                        int id;
                        Console.Write("Enter Employee ID: ");
                        while(!int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                            Console.Write("\nEnter Employee ID: ");
                        }
                        Console.WriteLine();
                        employeeManager.RemoveEmployee(id);
                        break;
                    case 3:
                        // display all employees
                        employeeManager.DisplayEmployees();
                        break;
                    case 4:
                        // display total salary of all employees
                        employeeManager.DisplayTotalSalary();
                        break;
                    case 5:
                        // assign employee to a department
                        int empId;

                        Console.Write("Enter Employee ID: ");
                        while(!int.TryParse(Console.ReadLine(), out empId))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                            Console.Write("Enter Employee ID: ");
                        }

                        Department dept = GetDepartmentChoice();
                        Console.WriteLine();

                        employeeManager.AssignEmployeeToDepartment(empId, dept);
                        break;
                    case 6:
                        // display total number of employees
                        Console.WriteLine($"\nTotal employees: {Employee.GetNumberOfEmployees()}\n");
                        break;
                    case 7:
                        // exit the program
                        Console.WriteLine("\nBye!");
                        break;
                }
            }
        }

        // enum for different departments
        enum Department
        {
            Finance, HR, IT
        }

        // class representing a person
        class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        // interface for managing employees
        interface IManager
        {
            void AssignEmployeeToDepartment(Employee employee, Department department) { }
        }

        // class representing an employee
        class Employee : Person
        {
            public int EmployeeId { get; set; }
            public double Salary { get; set; }
            public Department Department { get; set; }
            // static variable to generate unique employee IDs
            private static int empId = 1; 
            // static variable to track total number of employees
            public static int NumberOfEmployees = 0;

            // constructor to initialize employee object
            public Employee(string firstName, string lastName, double salary, Department department)
            {
                EmployeeId = empId++;
                FirstName = firstName;
                LastName = lastName;
                Salary = salary;
                Department = department;
                NumberOfEmployees++;
            }

            // method to get total number of employees
            public static int GetNumberOfEmployees()
            {
                return NumberOfEmployees;
            }

            // destructor to display message when an employee object is destroyed
            ~Employee()
            {
                Console.WriteLine("Employee object destroyed.");
            }
        }

        // class for managing employees
        class EmployeeManager : IManager
        {
            List<Employee> employees;

            // constructor to initialize employee manager with a list of employees
            public EmployeeManager(List<Employee> employees)
            {
                this.employees = employees;
            }

            // method to add an employee to the list
            public void AddEmployee(Employee emp)
            {
                employees.Add(emp);
            }

            // method to remove an employee from the list
            public void RemoveEmployee(int id)
            {
                if (employees.Exists(employee => employee.EmployeeId == id))
                {
                    employees.RemoveAll(employee => employee.EmployeeId == id);
                    Employee.NumberOfEmployees--;
                }
                else
                {
                    Console.WriteLine($"\nEmployee with ID {id} does not exist.\n");
                }
            }

            // method to display all employees
            public void DisplayEmployees()
            {
                // display header for employee list
                Console.WriteLine("\nID\t\tName\t\t\tSalary\t\tDepartment\t\t");

                for (int i = 0; i < employees.Count; i++)
                {
                    Console.WriteLine($"{employees[i].EmployeeId}\t\t{employees[i].FirstName} {employees[i].LastName}\t\t{employees[i].Salary}\t\t{employees[i].Department}");
                }
                Console.WriteLine();
            }

            // method to display total salary of all employees
            public void DisplayTotalSalary()
            {
                double totalSalary = 0;

                for (int i = 0; i < employees.Count;i++)
                {
                    totalSalary += employees[i].Salary;
                }

                Console.WriteLine($"\nTotal Salary: {totalSalary}\n");
            }

            // method to assign an employee to a department
            public void AssignEmployeeToDepartment(int empId, Department dept)
            {
                Employee emp = employees.Find(employee => employee.EmployeeId == empId);
                if (emp != null)
                {
                    emp.Department = dept;
                }
                else
                {
                    Console.WriteLine($"Employee with ID {empId} does not exist.\n");
                }
            }

        }
    }
}