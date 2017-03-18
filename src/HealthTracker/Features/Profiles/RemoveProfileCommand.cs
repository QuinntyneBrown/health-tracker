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
    public class RemoveProfileCommand
    {
        public class RemoveProfileRequest : IRequest<RemoveProfileResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveProfileResponse { }

        public class RemoveProfileHandler : IAsyncRequestHandler<RemoveProfileRequest, RemoveProfileResponse>
        {
            public RemoveProfileHandler(HealthTrackerContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveProfileResponse> Handle(RemoveProfileRequest request)
            {
                var profile = await _context.Profiles.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                profile.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveProfileResponse();
            }

            private readonly HealthTrackerContext _context;
            private readonly ICache _cache;
        }
    }
}
