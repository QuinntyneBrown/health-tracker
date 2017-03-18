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
    public class AddOrUpdateProfileCommand
    {
        public class AddOrUpdateProfileRequest : IRequest<AddOrUpdateProfileResponse>
        {
            public ProfileApiModel Profile { get; set; }
            public int? TenantId { get; set; }
        }

        public class AddOrUpdateProfileResponse { }

        public class AddOrUpdateProfileHandler : IAsyncRequestHandler<AddOrUpdateProfileRequest, AddOrUpdateProfileResponse>
        {
            public AddOrUpdateProfileHandler(HealthTrackerContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateProfileResponse> Handle(AddOrUpdateProfileRequest request)
            {
                var entity = await _context.Profiles
                    .SingleOrDefaultAsync(x => x.Id == request.Profile.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Profiles.Add(entity = new Profile());
                entity.Name = request.Profile.Name;
                entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateProfileResponse();
            }

            private readonly HealthTrackerContext _context;
            private readonly ICache _cache;
        }

    }

}
