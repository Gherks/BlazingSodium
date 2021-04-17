using System;

namespace BlazingSodium.Shared
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        private Employee()
        {
            Id = Guid.NewGuid();
        }

        public static Employee Create(string name, int age)
        {
            return new Employee
            {
                Name = name,
                Age = age
            };
        }
    }
}
