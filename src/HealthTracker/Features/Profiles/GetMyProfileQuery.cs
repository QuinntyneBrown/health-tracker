using MediatR;
using HealthTracker.Data;
using HealthTracker.Features.Core;
using System.Threading.Tasks;

namespace HealthTracker.Features.Profiles
{
    public class GetMyProfileQuery
    {
        public class GetMyProfileRequest : IRequest<GetMyProfileResponse> { }

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
                throw new System.NotImplementedException();
            }

            private readonly HealthTrackerContext _context;
            private readonly ICache _cache;
        }

    }

}
