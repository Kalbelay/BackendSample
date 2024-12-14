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
    public class UpdateUser : IRequest<Response<long>>
    {
        public long Code { get; set; }
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

    public class UpdateUserHandler : IRequestHandler<UpdateUser, Response<long>>
    {
        private readonly IUserRepositoryAsync _UserRepository;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserRepositoryAsync UserRepository, IMapper mapper)
        {
            _UserRepository = UserRepository;
            _mapper = mapper;
        }

        public async Task<Response<long>> Handle(UpdateUser command, CancellationToken cancellationToken)
        {
            // Fetch the existing user entity by ID
            var entity = await _UserRepository.GetByIdAsync(command.Code);
            if (entity == null)
            {
                return new Response<long>(message: $"User Not Found.") { Succeeded = false, Message = $"User Not Found." };
            }

            // Check if another user with the same UserName exists (excluding the current user)
            var existingUserByUsername = await _UserRepository.GetUserByUserName(command.UserName);
            if (existingUserByUsername != null && existingUserByUsername.Code != command.Code)
            {
                return new Response<long>(message: "A user with this username already exists.") { Succeeded = false };
            }

            // Map the command data to the existing entity
            entity = _mapper.Map(command, entity);

            // Perform the update
            await _UserRepository.UpdateAsync(entity);

            return new Response<long>(entity.Code, "The user was updated successfully.");
        }
    }

}
