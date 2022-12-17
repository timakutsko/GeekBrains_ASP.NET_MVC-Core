using System;
using System.Collections.Generic;

namespace Lesson3
{
    internal class Sample01
    {
        static void Main(string[] args)
        {
            List<BaseEmployee> employees = new List<BaseEmployee>()
            {
                new MonthlyEmployee("Смит", "Смитов", 500),
                new HourlyEmployee("Кенни", "Ворк", 2),
                new HourlyEmployee("Мистер", "Раб", 0.1),
            };

            foreach (var employee in employees)
            {
                employee.CountResultSalary();
                employee.SetEmployeeType();

                Console.WriteLine($"Привет, {employee.Name} {employee.Surname}! Т.к. вы являетесь нашим сотрудником, " +
                    $"который работает по типу контракта \"{employee.CurrentEmployeeType.Description}\", и размер Вашей " +
                    $"зарплаты составляет {employee.CurrentSalary} {employee.CurrentEmployeeType.SalaryPerTimeDescription}, " +
                    $"то за этот месяц вы получите {employee.ResultSalary} шмеклей.");
            }

            Console.ReadKey(true);
        }
    }

    #region Классы типов сотрудников (шаблонный метод)
    internal abstract class BaseEmployee
    {
        public BaseEmployee()
        {
        }

        public BaseEmployee(string name, string surname, double currentSalary) : this()
        {
            Name = name;
            Surname = surname;
            CurrentSalary = currentSalary;
        }
        
        public string Name { get; set; }

        public string Surname { get; set; }

        public double CurrentSalary { get; set; }

        public EmployeeType CurrentEmployeeType { get; protected set; }

        public double ResultSalary { get; protected set; }

        public abstract void SetEmployeeType();

        public abstract double CountResultSalary();
    }

    internal sealed class MonthlyEmployee : BaseEmployee
    {
        public MonthlyEmployee()
        {
        }
        
        public MonthlyEmployee(string name, string surname, double currentSalary) : base(name, surname, currentSalary)
        {
        }

        public override void SetEmployeeType()
        {
            CurrentEmployeeType = MonthlyType.Instance;
        }

        public override double CountResultSalary()
        {
            ResultSalary = CurrentSalary;
            return ResultSalary;
        }
    }

    internal sealed class HourlyEmployee : BaseEmployee
    {
        public HourlyEmployee()
        {
        }
        
        public HourlyEmployee(string name, string surname, double currentSalary) : base(name, surname, currentSalary)
        {
        }

        public override void SetEmployeeType()
        {
            CurrentEmployeeType = HourlyType.Instance;
        }

        public override double CountResultSalary()
        {
            ResultSalary = 20.8 * 8 * CurrentSalary;
            return ResultSalary;
        }
    }
    #endregion

    #region Расширяемое описание типа сотрудника (синглтон + шаблонный метод)
    internal abstract class EmployeeType
    {
        public int Id { get; }

        public string Description { get; }

        public string SalaryPerTimeDescription { get; }

        protected EmployeeType(int id, string descr, string salaryPerTime)
        {
            Id = id;
            Description = descr;
            SalaryPerTimeDescription = salaryPerTime;
        }
    }

    internal sealed class HourlyType : EmployeeType
    {
        private static HourlyType _employeeType;

        public HourlyType(int id, string descr, string salaryPerTime) : base(id, descr, salaryPerTime)
        {
        }

        public static HourlyType Instance
        {
            get
            {
                if (_employeeType == null)
                {
                    _employeeType = new HourlyType(0, "Почасовая оплата", "шмекля\\-ей в час");
                }
                return _employeeType;
            }
        }
    }

    internal sealed class MonthlyType : EmployeeType
    {
        private static MonthlyType _employeeType;

        public MonthlyType(int id, string descr, string salaryPerTime) : base(id, descr, salaryPerTime)
        {
        }

        public static MonthlyType Instance
        {
            get
            {
                if (_employeeType == null)
                {
                    _employeeType = new MonthlyType(1, "Ежемесячная оплата", "шмекля\\-ей за месяц");
                }
                return _employeeType;
            }
        }
    }
    #endregion
}
