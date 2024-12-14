using Application.Interfaces.Repositories.FMS;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories;

namespace Application.Features.User.Commands
{
    public class CreateUser : IRequest<Response<long>>
    {
        public string EmployeeCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public virtual DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool AllowMultipleUser { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public string TrxnUnit { get; set; }
        public string Remark { get; set; }
    }
    public class CreateUserHandler : IRequestHandler<CreateUser, Response<long>>
    {
        private readonly IUserRepositoryAsync _UserRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _UserRepositoryAsync = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<long>> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            // Check if a user with the same username already exists
            var existingUserByUsername = await _UserRepositoryAsync.GetUserByUserName(request.UserName);
            if (existingUserByUsername != null)
            {
                return new Response<long>(0, "A user with this username already exists.");
            }

            // Check if a user with the same employee code already exists
            var existingUserByEmployeeCode = await _UserRepositoryAsync.GetUserByEmployeeCode(request.EmployeeCode);
            if (existingUserByEmployeeCode != null)
            {
                return new Response<long>(0, "A user with this employee code already exists.");
            }

            // If both checks pass, create a new user
            var entity = _mapper.Map<Domain.Entities.User>(request);
            await _UserRepositoryAsync.AddAsync(entity);

            return new Response<long>(entity.Code, "The user was saved successfully.");
        }
    }

}
