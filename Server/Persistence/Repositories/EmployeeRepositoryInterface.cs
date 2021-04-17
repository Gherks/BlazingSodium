using BlazingSodium.Shared;
using System;
using System.Collections.Generic;

namespace BlazingSodium.Server.Persistence.Repositories
{
    public interface EmployeeRepositoryInterface
    {
        Employee CreateEmployee(string name, int age);
        bool DeleteEmployee(Guid id);
        Employee GetEmployee(Guid id);
        Employee GetEmployee(string name);
        IEnumerable<Employee> GetEmployees();
        void Save();
    }
}
