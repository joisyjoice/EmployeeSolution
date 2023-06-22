using EmployeeSolution.Helpers;
using EmployeeSolution.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeSolution.Services
{
    public interface IEmployeeService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
    }
    public class EmployeeService : IEmployeeService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<Employee> _users = new List<Employee>
    {
        new Employee { EmployeeIDPK = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
    };

        private readonly AppSettings _appSettings;

        public EmployeeService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        //public AuthenticateResponse Authenticate(AuthenticateRequest model)
        //{
        //    var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        //    // return null if user not found
        //    if (user == null) return null;

        //    // authentication successful so generate jwt token
        //    var token = generateJwtToken(user);

        //    return new AuthenticateResponse(user, token);
        //}

        //public IEnumerable<User> GetAll()
        //{
        //    return _users;
        //}

        //public User GetById(int id)
        //{
        //    return _users.FirstOrDefault(x => x.Id == id);
        //}

        // helper methods

        private string generateJwtToken(Employee user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.EmployeeIDPK.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {

            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _users;
        }

        public Employee GetById(int id)
        {
            return _users.FirstOrDefault(x => x.EmployeeIDPK == id);
        }
    }
}
