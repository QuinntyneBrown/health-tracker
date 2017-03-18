using MediatR;
using HealthTracker.Data;
using HealthTracker.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace HealthTracker.Features.Profiles
{
    public class GetProfileByIdQuery
    {
        public class GetProfileByIdRequest : IRequest<GetProfileByIdResponse> { 
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetProfileByIdResponse
        {
            public ProfileApiModel Profile { get; set; } 
        }

        public class GetProfileByIdHandler : IAsyncRequestHandler<GetProfileByIdRequest, GetProfileByIdResponse>
        {
            public GetProfileByIdHandler(HealthTrackerContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetProfileByIdResponse> Handle(GetProfileByIdRequest request)
            {                
                return new GetProfileByIdResponse()
                {
                    Profile = ProfileApiModel.FromProfile(await _context.Profiles.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly HealthTrackerContext _context;
            private readonly ICache _cache;
        }

    }

}
