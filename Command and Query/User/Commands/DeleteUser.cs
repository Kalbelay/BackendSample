using Application.Interfaces.Repositories.FMS;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories;

namespace Application.Features.User.Commands
{
    public class DeleteUser : IRequest<Response<long>>
    {
        public long Code { get; set; }
    }

    public class DeleteUserHandler : IRequestHandler<DeleteUser, Response<long>>
    {
        private readonly IUserRepositoryAsync _UserRepository;
        public DeleteUserHandler(IUserRepositoryAsync UserRepository)
        {
            _UserRepository = UserRepository;
        }
        public async Task<Response<long>> Handle(DeleteUser command, CancellationToken cancellationToken)
        {
            var entity = await _UserRepository.GetByIdAsync(command.Code);
            if (entity == null) return new Response<long>(message: $"User Not Found.") { Succeeded = false, Message = $"User Not Found." };
            await _UserRepository.DeleteAsync(entity);

            return new Response<long>(entity.Code);

        }
    }
}
