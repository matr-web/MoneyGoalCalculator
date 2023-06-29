using MoneyGoalCalculator.db;
using MoneyGoalCalculator.Entities;
using MoneyGoalCalculator.Interfaces;
using MoneyGoalCalculator.Models.Dto_s;
using System.Linq.Expressions;

namespace MoneyGoalCalculator.Services;

/// <summary>
/// Calculation Service.
/// </summary>
public class CalculationService : ICalculationService
{
    public CalculationService(MoneyGoalContext context)
    {
        _context = context;
    }

    private readonly MoneyGoalContext _context;

    public async Task<CalculationDto> GetCalculationByIdAsync(int id)
    {
        // Get Calculation Entity with has given Id.
        var calculationEntity = await _context.Calculations.FindAsync(id);

        // Map Entity to DTO.
        var calculationDto = CalculationDto.ToCalculationDto(calculationEntity);

        // Return DTO.
        return calculationDto;
    }

    public Task<IEnumerable<CalculationDto>> GetCalculations(Expression<Func<CalculationEntity, bool>> filterExpression = null)
    {
        // Create an empty Calculation Entities collection variable.
        IQueryable<CalculationEntity> calculationEntities = null;

        // Assign elements to the variable based on if the filterExpression is null or not null.
        calculationEntities = (filterExpression != null) ? _context.Calculations.Where(filterExpression) : _context.Calculations.AsQueryable();  

        // Map collection of Entities to Collection of DTO's.
        var calculationDtos = CalculationDto.ToCollectionOfCalculationDto(calculationEntities);

        // Return DTO's.
        return Task.FromResult(calculationDtos);
    }

    public async Task InsertCalculationAsync(CalculationDto calculationDto)
    {
        // Create a new Calculation Entity with the data of the given DTO.
        var calculationEntity = new CalculationEntity()
        {
            CurrentMoneyAmount = calculationDto.CurrentMoneyAmount,
            MoneyAmount = calculationDto.MoneyAmount,
            MoneyInstallment = calculationDto.MoneyInstallment,
            DateTo = calculationDto.DateTo,
            CalculationDate = calculationDto.CalculationDate
        };

        // Add the Calculation Entity to Database and save the changes.
        await _context.Calculations.AddAsync(calculationEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCalculationAsync(int id)
    {
        // Get Calculation Entity with has given Id. 
        var calculationEntity = _context.Calculations.Find(id);

        // Remove the Calculation from Database and save the changes.
        _context.Calculations.Remove(calculationEntity);
        await _context.SaveChangesAsync();
    }

    public CalculationDto CalculateMoneyInstallment(CreateCalculationDto createCalculationDto)
    {
        // Calculate the money installment.
        // How high the installment must be, so the given amount of money will be raised before the date expires.
        /// Calculate how many months there are left to raise the money.
        /// https://stackoverflow.com/questions/4638993/difference-in-months-between-two-dates
        var monthsLeft = ((createCalculationDto.DateTo.Year - DateTime.Now.Year) * 12) + createCalculationDto.DateTo.Month - DateTime.Now.Month;

        /// Convert the current amount of money from string to decimal.
        var currentMoneyAmountDec = decimal.Parse(createCalculationDto.CurrentMoneyAmount);

        /// Convert the given amount of money that has to be raised from string to decimal.
        var moneyAmountDec = decimal.Parse(createCalculationDto.MoneyAmount);

        /// Calculate difference between current amount of money and that one that has to be saved.
        var differenceInAmount = moneyAmountDec - currentMoneyAmountDec; 

        /// Calculate the installment amount.
        var moneyInstallment = differenceInAmount / monthsLeft;

        // Create new Calculation DTO with all data.
        var calculationDto = new CalculationDto()
        {
            CurrentMoneyAmount = createCalculationDto.CurrentMoneyAmount,
            MoneyAmount = createCalculationDto.MoneyAmount,
            DateTo = createCalculationDto.DateTo,
            MoneyInstallment = moneyInstallment.ToString(),
            CalculationDate = DateTime.Now
        };

        // Return the Calculation DTO.
        return calculationDto;
    }
}
