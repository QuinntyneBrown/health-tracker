using MediatR;
using HealthTracker.Data;
using HealthTracker.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace HealthTracker.Features.Profiles
{
    public class GetAccountsQuery
    {
        public class GetAccountsRequest : IRequest<GetAccountsResponse> { 
            public int? TenantId { get; set; }        
        }

        public class GetAccountsResponse
        {
            public ICollection<AccountApiModel> Accounts { get; set; } = new HashSet<AccountApiModel>();
        }

        public class GetAccountsHandler : IAsyncRequestHandler<GetAccountsRequest, GetAccountsResponse>
        {
            public GetAccountsHandler(HealthTrackerContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetAccountsResponse> Handle(GetAccountsRequest request)
            {
                var accounts = await _context.Accounts
                    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetAccountsResponse()
                {
                    Accounts = accounts.Select(x => AccountApiModel.FromAccount(x)).ToList()
                };
            }

            private readonly HealthTrackerContext _context;
            private readonly ICache _cache;
        }

    }

}
