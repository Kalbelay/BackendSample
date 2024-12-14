//using Application.Features.User.Queries;
//using Application.Interfaces.Repositories;
//using Application.Wrappers;
//using AutoMapper;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using System.Threading;

//namespace Application.Features.User.Queries
//{
//    public class GetUserByEmployeeCode : IRequest<PagedResponse<List<Domain.Entities.Security.User>>>
//    {
//        public string EmployeeCode { get; set; }
//    }
//    public class GetUserByEmployeeCodeHandler : IRequestHandler<GetUserByEmployeeCode, PagedResponse<List<Domain.Entities.Security.User>>>
//    {
//        private readonly IUserRepositoryAsync _UserRepository;
//        public GetUserByEmployeeCodeHandler(IUserRepositoryAsync UserRepository)
//        {
//            _UserRepository = UserRepository;
//        }

//        public async task<pagedresponse<list<domain.entities.security.user>>> handle(getuserbyemployeecode request, cancellationtoken cancellationtoken)
//        {
//            var listofusers = await _userrepository.getuserbyemployeecode(request.employeecode);
//            return new pagedresponse<list<domain.entities.security.user>>(listofusers, 1, listofusers.count);
//        }
//    }
//}
