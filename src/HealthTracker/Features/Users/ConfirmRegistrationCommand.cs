using MediatR;
using HealthTracker.Data;
using HealthTracker.Features.Core;
using System.Threading.Tasks;

namespace HealthTracker.Features.Users
{
    public class ConfirmRegistrationCommand
    {
        public class ConfirmRegistrationRequest : IRequest<ConfirmRegistrationResponse>
        {
            public string Token { get; set; }
        }

        public class ConfirmRegistrationResponse
        {
            public ConfirmRegistrationResponse()
            {

            }
        }

        public class ConfirmRegistrationHandler : IAsyncRequestHandler<ConfirmRegistrationRequest, ConfirmRegistrationResponse>
        {
            public ConfirmRegistrationHandler(HealthTrackerContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<ConfirmRegistrationResponse> Handle(ConfirmRegistrationRequest request)
            {
                throw new System.NotImplementedException();
            }

            private readonly HealthTrackerContext _context;
            private readonly ICache _cache;
        }

    }

}
