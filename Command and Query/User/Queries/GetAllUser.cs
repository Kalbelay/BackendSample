using Application.Features.User.Queries;
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

namespace Application.Features.User.Queries
{
    public class GetAllUser : IRequest<PagedResponse<IEnumerable<UserDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllUserHandler : IRequestHandler<GetAllUser, PagedResponse<IEnumerable<UserDto>>>
    {
        private readonly IUserRepositoryAsync _UserRepository;
        private readonly IMapper _mapper;
        public GetAllUserHandler(IUserRepositoryAsync UserRepository, IMapper mapper)
        {
            _UserRepository = UserRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<UserDto>>> Handle(GetAllUser request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllUserParameter>(request);
            var User = await _UserRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var UserViewModel = _mapper.Map<IEnumerable<UserDto>>(User);
            return new PagedResponse<IEnumerable<UserDto>>(UserViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
