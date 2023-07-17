using Microsoft.AspNetCore.Mvc;
using MoneyGoalCalculator.Interfaces;
using MoneyGoalCalculator.Models.Dto_s;

namespace MoneyGoalCalculator.Controllers;

/// <summary>
/// Controller that holds all actions associated with Calculations.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CalculationController : ControllerBase
{
	public CalculationController(ICalculationService calculationService)
	{
		_calculationService = calculationService;
	}

	private readonly ICalculationService _calculationService;

    /// <summary>
    /// Get Collection of Calculations DTO.
    /// </summary>
    /// <returns>Response 200 OK with formatted Calculations.</returns>
    [HttpGet("GetCalculations")]
    public async Task<ActionResult<FormattedCalculationDto>> GetCalculations()
    {
        // Get Calculations DTO.
        var calculationsDto = await _calculationService.GetCalculations();

        // If there are no Calculations...
        if(calculationsDto == null) 
        {
            // Return 400 BadRequest.
            return BadRequest();
        }

        // Return 200 OK with Formatted Calculations DTO.
        return Ok(FormattedCalculationDto.ToCollectionOfFormattedCalculationDto(calculationsDto));
    }

    /// <summary>
    /// Get formatted dates (month + year) in which the User will have to pay given money installment to achive his goal.
    /// </summary>
    /// <param name="id">Calculation Id.</param>
    /// <returns>200 OK with calculated Dates.</returns>
    [HttpGet("GetDatesInCalculation")]
    public ActionResult<DateTime> GetDatesInCalculationCalculation([FromQuery] int id)
    {
        // Get the Dates.
        var datesInCalculation = _calculationService.GetMonthsInCalculation(id);

        // If there are no Dates...
        if (datesInCalculation == null)
        {
            // Return 400 BadRequest.
            return BadRequest();
        }

        // Return 200 OK with calculated Dates.
        return Ok(datesInCalculation); 
    }

    /// <summary>
    /// Calculates the monthly payment according to the amount and the date by which the sum must be collected.
	/// Add given Calculation do db.
    /// </summary>
    /// <param name="createCalculationDto">DTO that holds all required data for calculating the monthly payment and saving it to the db.</param>
    /// <returns>Response 200 OK with just created Calculation DTO.</returns>
    [HttpPost("CalculateMoneyInstallment")]
	public async Task<ActionResult<FormattedCalculationDto>> CalculateMoneyInstallment([FromBody] CreateCalculationDto createCalculationDto)
	{
		// Calculate monthly payment and get all information into DTO.
		var calculationDto = _calculationService.CalculateMoneyInstallment(createCalculationDto);

        // Save just created DTO in db.
        await _calculationService.InsertCalculationAsync(calculationDto);

        // Return 200 OK - with formatted just saved DTO.
        return Ok(FormattedCalculationDto.ToFormattedCalculationDto(calculationDto));
    }

    /// <summary>
    /// Delete Calculation with given Id.
    /// </summary>
    /// <param name="id">Id of Calculation with should be deleted.</param>
    /// <returns>204 NoContent.</returns>
    [HttpDelete("DeleteCalculation")]
    public async Task<ActionResult> DeleteCalculationAsync([FromQuery] int id)
    {
        // Get Calculation with given Id.
        var calculationDto = await _calculationService.GetCalculationByIdAsync(id);

        // Check if given Calculation exists.
        if(calculationDto == null) 
        {
            // Response with 404 NotFound.
            return NotFound();
        }

        // Delete Calculation.
        await _calculationService.DeleteCalculationAsync(id);

        // Return 204 NoContent.
        return NoContent();
    }

}
