using Application.Features.User.Queries;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.User.Queries
{
    public class GetAllUsers : IRequest<PagedResponse<List<Domain.Entities.Security.User>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllUsersHandler : IRequestHandler<GetAllUsers, PagedResponse<List<Domain.Entities.Security.User>>>
    {
        private readonly IUserRepositoryAsync _UserRepository;
        private readonly IMapper _mapper;
        public GetAllUsersHandler(IUserRepositoryAsync UserRepository, IMapper mapper)
        {
            _UserRepository = UserRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<Domain.Entities.Security.User>>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllUserParameter>(request);
            var User = await _UserRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var UserViewModel = _mapper.Map<List<Domain.Entities.Security.User>>(User);
            return new PagedResponse<List<Domain.Entities.Security.User>>(UserViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
