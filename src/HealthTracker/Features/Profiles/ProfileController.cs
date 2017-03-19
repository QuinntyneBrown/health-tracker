using HealthTracker.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static HealthTracker.Features.Profiles.AddOrUpdateProfileCommand;
using static HealthTracker.Features.Profiles.GetProfilesQuery;
using static HealthTracker.Features.Profiles.GetProfileByIdQuery;
using static HealthTracker.Features.Profiles.RemoveProfileCommand;
using static HealthTracker.Features.Profiles.GetMyProfileQuery;

namespace HealthTracker.Features.Profiles
{
    [Authorize]
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        public ProfileController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateProfileResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateProfileRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateProfileResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateProfileRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetProfilesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetProfilesRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getmyprofile")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetMyProfileResponse))]
        public async Task<IHttpActionResult> GetMyProfile()
        {
            var request = new GetMyProfileRequest();
            //request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetProfileByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetProfileByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveProfileResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveProfileRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
