using Application.Features.Person.Queries;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepositoryAsync : GenericRepositoryAsync<User>, IUserRepositoryAsync
    {
        private readonly DbSet<User> _User;
        public UserRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _User = dbContext.Set<User>();
        }

        public async Task<User> GetUserByEmployeeCode(string employeeCode)
        {
            return await _User.FirstOrDefaultAsync(c => c.EmployeeCode == employeeCode);
        }
        public async Task<User> GetUserByUserName(string userName)
        {
            return await _User.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
