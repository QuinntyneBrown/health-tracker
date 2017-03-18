using HealthTracker.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static HealthTracker.Features.Profiles.AddOrUpdateAccountCommand;
using static HealthTracker.Features.Profiles.GetAccountsQuery;
using static HealthTracker.Features.Profiles.GetAccountByIdQuery;
using static HealthTracker.Features.Profiles.RemoveAccountCommand;

namespace HealthTracker.Features.Profiles
{
    [Authorize]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        public AccountController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateAccountResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateAccountRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateAccountResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateAccountRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetAccountsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetAccountsRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetAccountByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetAccountByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveAccountResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveAccountRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
