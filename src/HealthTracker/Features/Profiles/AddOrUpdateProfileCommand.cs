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
                    .Include(x=>x.WeightSnapShots)
                    .SingleOrDefaultAsync(x => x.Id == request.Profile.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Profiles.Add(entity = new Profile());
                entity.Name = request.Profile.Name;
                entity.TenantId = request.TenantId;

                entity.WeightSnapShots.Clear();

                foreach(var weightSnapShot in request.Profile.WeightSnapShots)
                {
                    var wss = await _context.WeightSnapShots.Include(x => x.Profile).Where(x => x.Id == weightSnapShot.Id).SingleOrDefaultAsync();

                    if(wss == null) { wss = new WeightSnapShot();}
                    wss.Id = weightSnapShot.Id;
                    wss.Pounds = weightSnapShot.Pounds;
                    wss.WeighedOn = weightSnapShot.WeighedOn;

                    entity.WeightSnapShots.Add(wss);
                }

                await _context.SaveChangesAsync();

                return new AddOrUpdateProfileResponse();
            }

            private readonly HealthTrackerContext _context;
            private readonly ICache _cache;
        }

    }

}
