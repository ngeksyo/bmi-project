using BmiProject.Infra;
using BmiProject.Service;
using BmiProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BmiProject.Controllers;

[ApiController]
[Route("/api/bmi")]
[ApiExceptionFilter]
public class BmiController : ControllerBase
{
    private readonly IBmiComputationService _bmiComputationService;

    public BmiController(IBmiComputationService bmiComputationService)
    {
        this._bmiComputationService = bmiComputationService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BmiResultModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public IActionResult Compute(BmiInputModel model)
    {
        var result = _bmiComputationService.Compute(model.Height, model.Weight);
        return Ok(new BmiResultModel
        {
            Category = result.Category,
            Bmi = result.Bmi
        });
    }
}
