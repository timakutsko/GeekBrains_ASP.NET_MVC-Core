using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3
{
    internal class Sample02
    {
        static void Main(string[] args)
        {
            List<BaseEmployee> employees = new List<BaseEmployee>()
            {
                new ConcreteEmployeeCreator().CreateEmployee(Employees.Monthly).Name("Смит").Surname("Смитов").CurrentSalary(300).SetResultSalary(),
                new ConcreteEmployeeCreator().CreateEmployee(Employees.Hourly).Name("Кенни").Surname("Ворк").CurrentSalary(4).SetResultSalary(),
                new ConcreteEmployeeCreator().CreateEmployee(Employees.Hourly).Name("Мистер").Surname("Раб").CurrentSalary(0.05).SetResultSalary(),
            };

            foreach (var employee in employees)
            {
                Console.WriteLine($"Привет, {employee.Name} {employee.Surname}! Т.к. вы являетесь нашим сотрудником, " +
                    $"который работает по типу контракта \"{employee.CurrentEmployeeType.Description}\", и размер Вашей " +
                    $"зарплаты составляет {employee.CurrentSalary} {employee.CurrentEmployeeType.SalaryPerTimeDescription}, " +
                    $"то за этот месяц вы получите {employee.ResultSalary} шмеклей.");
            }

            Console.ReadKey(true);
        }

        #region Factory method
        internal enum Employees
        {
            Hourly,
            Monthly 
        }

        internal abstract class EmployeeCreator
        {
            private BaseEmployee _employee;

            /// <summary>
            /// Фабричный метод (factory method)
            /// </summary>
            public BaseEmployee CreateEmployee(Employees employees)
            {
                _employee = CreateEmployeeInstance(employees);
                _employee.SetEmployeeType();
                // По заданию зп нужно указывать через методы расширения. В таком случае - расчет результирующей
                // зп затруднен тем, что изначально не известен уровень зп для сотрудника. Если
                // устанавиливать уровень зп в конструкторе - то расчет лучше делать тут:
                //_employee.CountResultSalary();
                return _employee;
            }

            protected private abstract BaseEmployee CreateEmployeeInstance(Employees employees);
        }

        internal sealed class ConcreteEmployeeCreator : EmployeeCreator
        {
            protected private override BaseEmployee CreateEmployeeInstance(Employees employees)
            {
                switch (employees)
                {
                    case Employees.Hourly: 
                        return new HourlyEmployee();
                    case Employees.Monthly:
                        return new MonthlyEmployee();
                }

                throw new NotImplementedException();
            }
        }
        #endregion
    }

    #region Extensions method
    internal static class EmployeeBuilder
    {
        public static BaseEmployee Name(this BaseEmployee baseEmployee, string name)
        {
            baseEmployee.Name = name;
            return baseEmployee;
        }

        public static BaseEmployee Surname(this BaseEmployee baseEmployee, string surname)
        {
            baseEmployee.Surname = surname;
            return baseEmployee;
        }

        public static BaseEmployee CurrentSalary(this BaseEmployee baseEmployee, double currentSalary)
        {
            baseEmployee.CurrentSalary = currentSalary;
            return baseEmployee;
        }

        public static BaseEmployee SetResultSalary(this BaseEmployee baseEmployee)
        {
            baseEmployee.CountResultSalary();
            return baseEmployee;
        }
    }
    #endregion
}
