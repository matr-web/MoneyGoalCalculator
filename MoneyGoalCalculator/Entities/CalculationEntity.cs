using System.ComponentModel.DataAnnotations;

namespace MoneyGoalCalculator.Entities;

/// <summary>
/// Entity, that contains data from each calculation.
/// </summary>
public class CalculationEntity
{
    private decimal _amount;
    private decimal _currentAmount;
    private decimal _installment;

    /// <summary>
    /// Calculation Primary Key.
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Amount of money User already has saved.
    /// </summary>
    [Required]
    [RegularExpression(@"^[0-9,]*$")]
    public string CurrentMoneyAmount
    {
        get { return _currentAmount.ToString(); }
        set { _currentAmount = decimal.Parse(value); }
    }

    /// <summary>
    /// Amount of money User wants to save.
    /// </summary>
    [Required]
    [RegularExpression(@"^[0-9,.]*$")]
    public string MoneyAmount
    {
        get { return _amount.ToString(); }
        set { _amount = decimal.Parse(value); }
    }

    /// <summary>
    /// Amount of money User will have to pay to achieve his goal.
    /// </summary>
    [Required]
    [RegularExpression(@"^[0-9,.]*$")]
    public string MoneyInstallment
    {
        get { return _installment.ToString(); }
        set { _installment = Convert.ToDecimal(value); }
    }

    /// <summary>
    /// Date until User wants to achieve his saving goal.
    /// </summary>
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateTo { get; set; }

    /// <summary>
    /// Time each calculation was requested.
    /// </summary>
    [Required]
    [DataType(DataType.Date)]
    public DateTime CalculationDate { get; set; } 
}
