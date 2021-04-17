using BlazingSodium.Client.Services;
using BlazingSodium.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingSodium.Client.Pages
{
    [Authorize]
    public partial class EmployeeOverview
    {
        public List<Employee> Employees { get; private set; } = new List<Employee>();
        public string InputEmployeeName { get; set; }
        public int InputEmployeeAge { get; set; }

        [Inject]
        public EmployeeDataServiceInterface EmployeeDataService { get; set; }

        public async void Submit_CreateNewEmployee()
        {
            try
            {
                if (InputEmployeeName.Trim().Length > 0 && InputEmployeeAge > 0)
                {
                    await EmployeeDataService.CreateEmployee(InputEmployeeName, InputEmployeeAge);
                    Employees = (await EmployeeDataService.GetEmployees()).ToList();

                    StateHasChanged();
                }
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }

        public async void Remove_Employee(Guid id)
        {
            try
            {
                await EmployeeDataService.DeleteEmployee(id);
                Employees = (await EmployeeDataService.GetEmployees()).ToList();

                StateHasChanged();
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Employees = (await EmployeeDataService.GetEmployees()).ToList();
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }
    }
}
