using EmployeesWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeesWebApplication.Services.Impl
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly List<Employee> _employees;

        private int _maxFreeId;

        public EmployeesRepository()
        {
            _employees = Enumerable.Range(1, 5)
                .Select(x => new Employee
                {
                    Id = x,
                    LastName = $"LastName{x}",
                    FirstName = $"FirstName{x}",
                    Patronymic = $"Patronymic{x}",
                    Birthday = DateTime.Now.AddYears(-18 - x)
                })
                .ToList();
            _maxFreeId = _employees.Max(x => x.Id) + 1;
        }
        
        public int Add(Employee employee)
        {
            employee.Id = _maxFreeId;
            _maxFreeId++;
            _employees.Add(employee);
            return employee.Id;
        }

        public bool Edit(Employee employee)
        {
            var db_employee = GetById(employee.Id);
            if (db_employee == null)
                return false;

            db_employee.FirstName = employee.FirstName;
            db_employee.LastName = employee.LastName;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.Birthday = employee.Birthday;

            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employees;
        }

        public Employee GetById(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }

        public bool RemoveById(int id)
        {
            var db_employee = GetById(id);
            if (db_employee == null)
                return false;

            _employees.Remove(db_employee);
            return true;
        }
    }
}
