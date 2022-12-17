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
                employee.SetEmployeeType();

                Console.WriteLine($"Привет, {employee.Name} {employee.Surname}! Т.к. вы являетесь нашим сотрудником, " +
                    $"который работает по типу контракта \"{employee.SetEmployeeType().Description}\", и размер Вашей " +
                    $"зарплаты составляет {employee.CurrentSalary} {employee.SetEmployeeType().SalaryPerTimeDescription}, " +
                    $"то за этот месяц вы получите {employee.ResultSalary} шмеклей.");
            }

            Console.ReadKey(true);
        }
    }

    #region Классы типов сотрудников (шаблонный метод)
    public abstract class BaseEmployee
    {
        public BaseEmployee(string name, string surname, double currentSalary)
        {
            Name = name;
            Surname = surname;
            CurrentSalary = currentSalary;
        }
        
        public string Name { get; protected set; }

        public string Surname { get; protected set; }

        public double CurrentSalary { get; protected set; }

        public double ResultSalary { get; protected set; }

        public abstract EmployeeType SetEmployeeType();

        public abstract double CountResultSalary();
    }

    public class MonthlyEmployee : BaseEmployee
    {
        public MonthlyEmployee(string name, string surname, double currentSalary) : base(name, surname, currentSalary)
        {
        }

        public override EmployeeType SetEmployeeType()
        {
            return MonthlyType.Instance;
        }

        public override double CountResultSalary()
        {
            ResultSalary = CurrentSalary;
            return ResultSalary;
        }
    }

    public class HourlyEmployee : BaseEmployee
    {
        public HourlyEmployee(string name, string surname, double currentSalary) : base(name, surname, currentSalary)
        {
        }

        public override EmployeeType SetEmployeeType()
        {
            return HourlyType.Instance;
        }

        public override double CountResultSalary()
        {
            ResultSalary = 20.8 * 8 * CurrentSalary;
            return ResultSalary;
        }
    }
    #endregion

    #region Расширяемое описание типа сотрудника (синглтон + шаблонный метод)
    public abstract class EmployeeType
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

    public class HourlyType : EmployeeType
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

    public class MonthlyType : EmployeeType
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
