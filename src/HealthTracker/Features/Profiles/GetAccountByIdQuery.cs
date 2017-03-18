using MediatR;
using HealthTracker.Data;
using HealthTracker.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace HealthTracker.Features.Profiles
{
    public class GetAccountByIdQuery
    {
        public class GetAccountByIdRequest : IRequest<GetAccountByIdResponse> { 
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetAccountByIdResponse
        {
            public AccountApiModel Account { get; set; } 
        }

        public class GetAccountByIdHandler : IAsyncRequestHandler<GetAccountByIdRequest, GetAccountByIdResponse>
        {
            public GetAccountByIdHandler(HealthTrackerContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetAccountByIdResponse> Handle(GetAccountByIdRequest request)
            {                
                return new GetAccountByIdResponse()
                {
                    Account = AccountApiModel.FromAccount(await _context.Accounts.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly HealthTrackerContext _context;
            private readonly ICache _cache;
        }

    }

}
