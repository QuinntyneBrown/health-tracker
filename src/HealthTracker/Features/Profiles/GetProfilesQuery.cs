using MediatR;
using HealthTracker.Data;
using HealthTracker.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace HealthTracker.Features.Profiles
{
    public class GetProfilesQuery
    {
        public class GetProfilesRequest : IRequest<GetProfilesResponse> { 
            public int? TenantId { get; set; }        
        }

        public class GetProfilesResponse
        {
            public ICollection<ProfileApiModel> Profiles { get; set; } = new HashSet<ProfileApiModel>();
        }

        public class GetProfilesHandler : IAsyncRequestHandler<GetProfilesRequest, GetProfilesResponse>
        {
            public GetProfilesHandler(HealthTrackerContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetProfilesResponse> Handle(GetProfilesRequest request)
            {
                var profiles = await _context.Profiles
                    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetProfilesResponse()
                {
                    Profiles = profiles.Select(x => ProfileApiModel.FromProfile(x)).ToList()
                };
            }

            private readonly HealthTrackerContext _context;
            private readonly ICache _cache;
        }

    }

}
