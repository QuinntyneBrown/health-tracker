using MediatR;
using HealthTracker.Data;
using HealthTracker.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HealthTracker.Features.Profiles
{
    public class GetMyProfileQuery
    {
        public class GetMyProfileRequest : IRequest<GetMyProfileResponse> {
            public int? TenantId { get; set; }
            public int? ProfileId { get; set; } = 1;
        }

        public class GetMyProfileResponse
        {            
            public ProfileApiModel Profile { get; set; }
        }

        public class GetMyProfileHandler : IAsyncRequestHandler<GetMyProfileRequest, GetMyProfileResponse>
        {
            public GetMyProfileHandler(HealthTrackerContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetMyProfileResponse> Handle(GetMyProfileRequest request)
            {
                var profile = await _context.Profiles.Include(x => x.WeightSnapShots)
                    .FirstAsync();

                return new GetMyProfileResponse()
                {
                    Profile = ProfileApiModel.FromProfile(profile)
                };
            }

            private readonly HealthTrackerContext _context;
            private readonly ICache _cache;
        }

    }

}
