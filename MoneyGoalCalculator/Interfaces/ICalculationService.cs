using MoneyGoalCalculator.Entities;
using MoneyGoalCalculator.Models.Dto_s;
using System.Linq.Expressions;

namespace MoneyGoalCalculator.Interfaces;

/// <summary>
/// Provides methods for CalculationService.
/// </summary>
public interface ICalculationService
{
    /// <summary>
    /// Finds Calculation with given Primary Key (Id).
    /// </summary>
    /// <param name="id">The Id of the Calculation that will be returned.</param>
    /// <returns>DTO of Calculation with given Id.</returns>
    Task<CalculationDto> GetCalculationByIdAsync(int id);

    /// <summary>
    /// Finds all Calculations or those that match given filterExpression.
    /// </summary>
    /// <param name="filterExpression">Requirements which given Calculations have to fulfill to be return.</param>
    /// <returns>Collection of Calculation DTO's that fulfill given requirements.</returns>
    Task<IEnumerable<CalculationDto>> GetCalculations(Expression<Func<CalculationEntity, bool>> filterExpression = null);

    /// <summary>
    /// Insert's new Calculation.
    /// </summary>
    /// <param name="calculationDto">DTO that contains data of the Calculation that will be inserted.</param>
    Task InsertCalculationAsync(CalculationDto calculationDto);

    /// <summary>
    /// Delete's Calculation with given Primary Key (Id).
    /// </summary>
    /// <param name="id">The Id of the Calculation that will be deleted.</param>
    Task DeleteCalculationAsync(int id);

    /// <summary>
    /// Calculates money installment amount.
    /// </summary>
    /// <param name="createCalculationDto">DTO with data for given calculation.</param>
    /// <returns>DTO of Calculation with calculated money installment.</returns>
    CalculationDto CalculateMoneyInstallment(CreateCalculationDto createCalculationDto);
}
