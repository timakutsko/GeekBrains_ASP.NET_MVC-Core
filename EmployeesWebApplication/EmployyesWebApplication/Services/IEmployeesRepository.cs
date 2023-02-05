using EmployeesWebApplication.Models;
using System.Collections.Generic;

namespace EmployeesWebApplication.Services
{
    public interface IEmployeesRepository
    {
        IEnumerable<Employee> GetAll();

        Employee GetById(int id);

        int Add(Employee employee);

        bool Edit(Employee employee); 

        bool RemoveById(int id);
    }
}
