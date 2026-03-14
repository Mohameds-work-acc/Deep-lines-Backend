using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.EmailDTOs;
using Deep_lines_Backend.BLL.DTOs.SystemDTOs;
using Deep_lines_Backend.BLL.DTOs.UserEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IWebHostEnvironment env;
        private readonly IEmailService emailService;
        private readonly IConfiguration config;
        private readonly IGenericRepo<Employee> repo;
        private readonly IMapper mapper;
        private readonly PasswordHasher<object> passwordHasher = new();

        public EmployeeService(IWebHostEnvironment env, IEmailService emailService, IGenericRepo<Employee> repo, IMapper mapper , IConfiguration config)
        {
            this.env = env;
            this.emailService = emailService;
            this.repo = repo;
            this.mapper = mapper;
            this.config = config;
        }

        public async Task<failResponse>? AddUser(AddUserDTO userDTO)
        {
            var mapped = mapper.Map<Employee>(userDTO);

            var recorded = GetByEmail(userDTO.Email).Result;
            if (recorded != null)
            {
                var failResponse = new failResponse
                {
                   code = 400,
                   message = "Email already exists"
                };
                return failResponse;
            }

            mapped.Password = passwordHasher.HashPassword(null, mapped.Password);

            await repo.AddAsync(mapped);

            var path = Path.Combine(
                        env.ContentRootPath,
                        "Templates",
                        "employee-registration.html"
                    );

            var template = System.IO.File.ReadAllText(path);

            var emailBody = template
           .Replace("{{company_name}}", "Deep-Lines")
           .Replace("{{employee_name}}", mapped.Name)
           .Replace("{{employee_email}}", mapped.Email)
           .Replace("{{department}}", mapped.department)
           .Replace("{{jopTitle}}", mapped.jopTitle)
           .Replace("{{start_date}}", mapped.joinedDate.ToString("MMMM dd, yyyy"))
           .Replace("{{temporary_password}}", "12345678")
           .Replace("{{portal_link}}", "#")
           .Replace("{{hr_email}}", "test@Deep-lines.com")
           .Replace("{{hr_phone}}", "test")
           .Replace("{{company_address}}", "test.test")
           .Replace("{{company_phone}}", "0123546788")
           .Replace("{{current_year}}", DateTime.Now.Year.ToString());



            var emailDTO = new sendEmailDTO {
                Email = mapped.Email,
                Body = emailBody,
                Subject = "Welcome Aboard: Your Registration with Deep-lines is Complete"
            };
            await emailService.sendEmail(emailDTO);
            return null;
        }



        public async Task<bool> DeleteUser(int id)
        {
            var user = await repo.GetByIdAsync(id);
            if (user.Email == config["DefaultAdmin:Email"])
            {
                return false;
            }
            if (user == null) return false;
            await repo.DeleteAsync(user);
            return true;
        }

        public async Task<List<Employee>> GetAll()
        {
            var users = repo.GetAllAsync().Result;
            var removedAdmin = users.Where(u => u.Email != config["DefaultAdmin:Email"]).ToList();
            return removedAdmin;
        }

        public Task<Employee> GetByEmail(string email)
        {
            
            var users = repo.GetAllAsync().Result;

            return Task.FromResult(users.FirstOrDefault(u => u.Email == email));

        }

        public async Task<Employee> GetById(int id)
        {
            return await repo.GetByIdAsync(id);
        }

        public async Task<List<Employee>> GetByIds(List<int> ids)
        {
            var allEmployees = repo.GetAllAsync().Result;
            var employees = allEmployees.Where(e => ids.Contains(e.Id)).ToList();
            return employees;
        }

        public async Task<bool> UpdateUser(AddUserDTO userDTO, int id)
        {
            var recorded = await repo.GetByIdAsync(id);
            if (recorded == null) return false;
            var existingEmail = recorded.Email;
            mapper.Map(userDTO, recorded);
            recorded.Email = existingEmail;
            repo.UpdateAsync(recorded);
            return true;
        }
    }
}
