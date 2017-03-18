using MediatR;
using HealthTracker.Data;
using HealthTracker.Data.Model;
using HealthTracker.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace HealthTracker.Features.Profiles
{
    public class RemoveAccountCommand
    {
        public class RemoveAccountRequest : IRequest<RemoveAccountResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveAccountResponse { }

        public class RemoveAccountHandler : IAsyncRequestHandler<RemoveAccountRequest, RemoveAccountResponse>
        {
            public RemoveAccountHandler(HealthTrackerContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveAccountResponse> Handle(RemoveAccountRequest request)
            {
                var account = await _context.Accounts.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                account.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveAccountResponse();
            }

            private readonly HealthTrackerContext _context;
            private readonly ICache _cache;
        }
    }
}
