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
            IProposalService proposalService)
        {
            _logger = logger;
            _proposalService = proposalService;
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

        [HttpGet("ChangeLogs/{id}")]
        public async Task<Result> GetChangeLogs([FromRoute] int id)
        {
            var changeLogs = await _proposalService.GetChangeLogsAsync(id);

            return Result.Ok(changeLogs);
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

            if (!await _proposalService.UpdateAsync(model))
            {
                return Result.Error($"Something went wrong when updating proposal #{id}. Please try again.");
            }

            return Result.Ok($"Updated proposal #{id} successful.");
        }


        [HttpPatch("ToggleFlag/{id}")]
        public async Task<Result> ToggleFlag([FromRoute] int id, [FromBody] FlagToggleDto model)
        {
            // Make sure proposal ID is from the route
            model.Id = id;

            if (!await _proposalService.ToggleFlagAsync(model))
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

            if (!await _proposalService.ReviewAsync(model))
            {
                return Result.Error($"Something went wrong when updating proposal #{id}. Please try again.");
            }

            return Result.Ok($"{(model.IsApproved ? "Approved" : "Cancelled")} proposal #{id} successful.");
        }

        [HttpPatch("ScheduleSurgery/{id}")]
        public async Task<Result> ScheduleSurgery([FromRoute] int id, [FromBody] ProposalScheduleSurgeryDto model)
        {
            // Make sure proposal ID is from the route
            model.Id = id;

            var validator = new ProposalScheduleDtoValidator(_proposalService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _proposalService.ScheduleSurgeryAsync(model))
            {
                return Result.Error($"Something went wrong when scheduling surgery for proposal #{id}. Please try again.");
            }

            return Result.Ok($"Scheduled surgery for proposal #{id} successful.");
        }

        [HttpPatch("SetStatus/{id}")]
        public async Task<Result> SetStatus([FromRoute] int id, [FromBody] ProposalSetStatusDto model)
        {
            // Make sure proposal ID is from the route
            model.Id = id;

            var validator = new ProposalSetStatusDtoValidator(_proposalService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _proposalService.SetStatusAsync(model))
            {
                return Result.Error($"Something went wrong when updating proposal #{id}. Please try again.");
            }

            return Result.Ok($"Updated proposal #{id} status to {(PotentialProcedureStatus)model.Status} successful.");
        }
    }
}
