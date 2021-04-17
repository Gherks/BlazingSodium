using BlazingSodium.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazingSodium.Client.Services
{
    public interface EmployeeDataServiceInterface
    {
        Task CreateEmployee(string name, int age);
        Task<bool> DeleteEmployee(Guid id);
        Task<Employee> GetEmployee(Guid id);
        Task<Employee> GetEmployee(string name);
        Task<IEnumerable<Employee>> GetEmployees();
    }
}
