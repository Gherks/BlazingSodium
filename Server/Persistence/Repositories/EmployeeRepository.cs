using BlazingSodium.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazingSodium.Server.Persistence.Repositories
{
    public class EmployeeRepository : EmployeeRepositoryInterface, IDisposable
    {
        private readonly BlazingSodiumContext _appDbContext;

        public EmployeeRepository(BlazingSodiumContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Dispose()
        {
        }

        public Employee CreateEmployee(string name, int age)
        {
            if (GetEmployee(name) == null)
            {
                Employee employee = Employee.Create(name, age);
                _appDbContext.Add(employee);
                return employee;
            }

            return null;
        }

        public bool DeleteEmployee(Guid id)
        {
            Employee employee = GetEmployee(id);

            if (employee != null)
            {
                _appDbContext.Remove(employee);
                return true;
            }

            return false;
        }

        public Employee GetEmployee(Guid id)
        {
            return _appDbContext.Employees.FirstOrDefault(employee => employee.Id == id);
        }

        public Employee GetEmployee(string name)
        {
            return _appDbContext.Employees.FirstOrDefault(employee => employee.Name == name);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _appDbContext.Employees;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
