using FluentEmail.Core;
using InsuranceAPI.Auth;
using InsuranceAPI.Data;
using InsuranceAPI.DTOs;
using InsuranceAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Repository
{
    public record LoginRequest (string Email, string Password);
    public class AdminRepository(InsuranceDbContext context, Token token, IFluentEmail fluentEmail)
    {

        public async Task<Admin> Register(AdminDTO admin)
        {
            var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(admin.Password, 13);

            var newAdmin = context.Admins.Add(new Admin {
                Username = admin.Username,
                Email = admin.Email,
                PasswordHash = hashedPassword,
                Role = "admin"
            }).Entity;

            await context.SaveChangesAsync();
            await fluentEmail
                .To(newAdmin.Email)
                .Subject("EMAIL VERIFICATION FOR DDINSURANCE")
                .Body("To verify your account, click here")
                .SendAsync();

            return newAdmin;
        }

        public async Task<string> Login(LoginRequest request)
        {
            Admin admin = await context.Admins.FirstOrDefaultAsync(a => a.Email == request.Email)!;
            if(admin is null) 
            {
                throw new Exception("Email not found");
            }
            bool verified = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, admin.PasswordHash);
            if (!verified)
            {
                throw new Exception("Password is incorrect");
            }
            string accessToken = token.Create(admin);
            return accessToken;
        }
    }
}
