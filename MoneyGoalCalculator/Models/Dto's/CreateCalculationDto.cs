using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MoneyGoalCalculator.Models.Dto_s;

/// <summary>
/// Data Transfer Object responsible for creating new Calculation.
/// </summary>
public class CreateCalculationDto
{
    private string _amount;
    private string _currentAmount;
    private DateTime _dateTo;

    /// <summary>
    /// Amount of money User already has saved.
    /// </summary>
    [Required]
    [DataType(DataType.Currency)]
    [RegularExpression(@"^[0-9,.]*$", ErrorMessage = "Enter a valid Number")] // Given value can be number, comma and dot.
    public string CurrentMoneyAmount
    {
        get { return _currentAmount; }
        set { _currentAmount = value.Replace('.', ','); }
    }

    /// <summary>
    /// Amount of money User wants to save.
    /// </summary>
    [Required]
    [DataType(DataType.Currency)]
    [RegularExpression(@"^[0-9,.]*$", ErrorMessage = "Enter a valid Number")] // Given value can be number, comma and dot.
    public string MoneyAmount
    {
        get { return _amount; }
        set { _amount = value.Replace('.', ','); }
    }

    /// <summary>
    /// Date until User wants to achieve his saving goal.
    /// </summary>
    [DataType(DataType.Date)]
    public DateTime DateTo
    {
        get { return _dateTo; }
        set 
        { 
            if(DateTime.Compare(value, DateTime.Now) <= 0) // Check if the given Date isn't in the past.
            {
                throw new ArgumentException("Enter a future Date");
            }

            _dateTo = value;
        }
    }
}
