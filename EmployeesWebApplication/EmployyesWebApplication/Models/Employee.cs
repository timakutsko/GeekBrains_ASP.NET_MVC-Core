using System;

namespace EmployeesWebApplication.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        public DateTime Birthday { get; set; }
    }
}
