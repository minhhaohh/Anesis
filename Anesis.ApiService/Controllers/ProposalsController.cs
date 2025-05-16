using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.PotentialProcedures;
using Anesis.ApiService.Extensions;
using Anesis.ApiService.Services.IServices;
using Anesis.ApiService.Validators.Proposals;
using Anesis.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class ProposalsController : ControllerBase
    {
        private readonly ILogger<ProposalsController> _logger;
        private readonly IProposalService _proposalService;

        public ProposalsController(
            ILogger<ProposalsController> logger, 
            IProposalService potentialProcService)
        {
            _logger = logger;
            _proposalService = potentialProcService;
        }

        [HttpGet]
        public async Task<Result> Search([FromQuery] ProposalFilterDto filter)
        {
            var pagedData = await _proposalService.SearchAsync(filter);

            return Result.Ok(pagedData);
        }

        [HttpGet("{id}")]
        public async Task<Result> GetById([FromRoute] int id)
        {
            var proposal = await _proposalService.GetByIdAsync(id);

            return proposal == null 
                ? Result.Error($"Could not find proposal with ID #{id}")
                : Result.Ok(proposal);
        }

        [HttpPost]
        public async Task<Result> Create([FromBody] ProposalEditDto model)
        {
            var validator = new ProposalEditDtoValidator(_proposalService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _proposalService.CreateAsync(model))
            {
                return Result.Error($"Something went wrong when creating proposal. Please try again.");
            }

            return Result.Ok("Created new proposal successful.");
        }

        [HttpPut("{id}")]
        public async Task<Result> Update([FromRoute]int id, [FromBody] ProposalEditDto model)
        {
            // Make sure proposal ID is from the route
            model.Id = id;

            var validator = new ProposalEditDtoValidator(_proposalService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _proposalService.UpdateAsync(id, model))
            {
                return Result.Error($"Something went wrong when updating proposal #{id}. Please try again.");
            }

            return Result.Ok($"Updated proposal #{id} successful.");
        }

        [HttpPatch("Review/{id}")]
        public async Task<Result> Review([FromRoute] int id, [FromBody] ProposalReviewDto model)
        {
            // Make sure proposal ID is from the route
            model.Id = id;

            var validator = new ProposalReviewDtoValidator(_proposalService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _proposalService.ReviewAsync(id, model))
            {
                return Result.Error($"Something went wrong when updating proposal #{id}. Please try again.");
            }

            return Result.Ok($"{(model.IsApproved ? "Approved" : "Cancelled")} proposal #{id} successful.");
        }

        [HttpPatch("MarkAsOrdered/{id}")]
        public async Task<Result> MarkAsOrdered([FromRoute] int id)
        {
            if (!await _proposalService.SetStatusAsync(id, PotentialProcedureStatus.Ordered))
            {
                return Result.Error($"Something went wrong when updating proposal #{id}. Please try again.");
            }

            return Result.Ok($"Marked proposal #{id} as ordered successful.");
        }

        [HttpPatch("MarkAsCompleted/{id}")]
        public async Task<Result> MarkAsCompleted([FromRoute] int id)
        {
            if (!await _proposalService.SetStatusAsync(id,PotentialProcedureStatus.Completed))
            {
                return Result.Error($"Something went wrong when updating proposal #{id}. Please try again.");
            }

            return Result.Ok($"Marked proposal #{id} as completed successful.");
        }

        [HttpPatch("MarkAsCancelled/{id}")]
        public async Task<Result> MarkAsCancelled([FromRoute] int id, [FromBody] string reason)
        {
            if (!await _proposalService.SetStatusAsync(id, PotentialProcedureStatus.Cancelled, reason))
            {
                return Result.Error($"Something went wrong when updating proposal #{id}. Please try again.");
            }

            return Result.Ok($"Marked proposal #{id} as cancelled successful.");
        }

        [HttpPatch("ScheduleSurgery/{id}")]
        public async Task<Result> ScheduleSurgery([FromRoute] int id, [FromBody] ProposalScheduleDto model)
        {
            // Make sure proposal ID is from the route
            model.Id = id;

            var validator = new ProposalScheduleDtoValidator(_proposalService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _proposalService.ScheduleSurgeryAsync(id, model))
            {
                return Result.Error($"Something went wrong when scheduling surgery for proposal #{id}. Please try again.");
            }

            return Result.Ok($"Scheduled surgery for proposal #{id} successful.");
        }
    }
}
