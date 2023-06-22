using System.Text.Json.Serialization;

namespace EmployeeSolution.Models
{
    public class Employee
    {
        public int? EmployeeIDPK { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }=string.Empty;
        public string DepartMent { get; set; } = string.Empty;
        public string Username { get; set; }

       // [JsonIgnore]
        public string Password { get; set; }
    }
}
