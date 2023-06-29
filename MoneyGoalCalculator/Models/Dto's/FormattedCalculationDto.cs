using System.ComponentModel.DataAnnotations;

namespace MoneyGoalCalculator.Models.Dto_s;

/// <summary>
/// Data Transfer Object that contains formatted data (money properties as currency, formatted dates).
/// </summary>
public class FormattedCalculationDto
{
    private decimal _amount;
    private decimal _currentAmount;
    private decimal _installment;
    private DateTime _dateTo;
    private DateTime _calculationDate;

    /// <summary>
    /// Amount of money User already has saved.
    /// </summary>
    [Display(Name = "Current Money Amount")]
    public string CurrentMoneyAmount
    {
        get { return string.Format("{0:C}", _currentAmount); }
        set { _currentAmount = decimal.Parse(value); }
    }

    /// <summary>
    /// Amount of money User wants to save.
    /// </summary>
    [Display(Name = "Money Amount To Save")]
    public string MoneyAmount
    {
        get { return string.Format("{0:C}", _amount); }
        set { _amount = decimal.Parse(value); }
    }

    /// <summary>
    /// Amount of money User will have to pay to achieve his goal.
    /// </summary>
    [Display(Name = "Money Installment")]
    public string MoneyInstallment
    {
        get { return string.Format("{0:C}", _installment); }
        set { _installment = Convert.ToDecimal(value); }
    }

    /// <summary>
    /// Date until User wants to achieve his saving goal.
    /// </summary>
    [Display(Name = "Date To")]
    public string DateTo 
    {
        get { return _dateTo.ToString("yyyy-MM-dd"); }
        set { _dateTo = DateTime.Parse(value); }
    }

    /// <summary>
    /// Time each calculation was requested.
    /// </summary>
    [Display(Name = "Calculation Date")]
    public string CalculationDate
    {
        get { return _calculationDate.ToString("yyyy-MM-dd h:mm tt"); }
        set { _calculationDate = DateTime.Parse(value); }
    }

    /// <summary>
    /// Mapper: Collection of object type -> collection of Formatted Calculation DTO.
    /// </summary>
    /// <param name="entities">Collection of object type that will be mapped to Formatted Calculation DTO collection.</param>
    /// <returns>Collection of Formatted Calculation DTO with data from properties of the given object collection.</returns>
    public static IEnumerable<FormattedCalculationDto> ToCollectionOfFormattedCalculationDto(IEnumerable<object> objCollection)
    {
        var dtos = new List<FormattedCalculationDto>();

        foreach (var obj in objCollection)
        {
            Type type = obj.GetType();
            var properties = type.GetProperties();

            var dto = new FormattedCalculationDto()
            {
                CurrentMoneyAmount = (properties.FirstOrDefault(p => p.Name == "CurrentMoneyAmount").GetValue(obj).ToString()),
                MoneyAmount = properties.FirstOrDefault(p => p.Name == "MoneyAmount").GetValue(obj).ToString(),
                MoneyInstallment = properties.FirstOrDefault(p => p.Name == "MoneyInstallment").GetValue(obj).ToString(),
                DateTo = properties.FirstOrDefault(p => p.Name == "DateTo").GetValue(obj).ToString(),
                CalculationDate = properties.FirstOrDefault(p => p.Name == "CalculationDate").GetValue(obj).ToString()
            };

            dtos.Add(dto);
        }

        return dtos;
    }

    /// <summary>
    /// Mapper: object type -> Formatted Calculation DTO.
    /// </summary>
    /// <param name="obj">Object whose properties will be mapped to Formatted Calculation DTO.</param>
    /// <returns>Formatted Calculation DTO with data from properties of the given object.</returns>
    public static FormattedCalculationDto ToFormattedCalculationDto(object obj)
    {
        Type type = obj.GetType();
        var properties = type.GetProperties();

        var CurrentMoneyAmount = (properties.FirstOrDefault(p => p.Name == "CurrentMoneyAmount").GetValue(obj).ToString());
        var MoneyAmount = properties.FirstOrDefault(p => p.Name == "MoneyAmount").GetValue(obj).ToString();
        var MoneyInstallment = properties.FirstOrDefault(p => p.Name == "MoneyInstallment").GetValue(obj).ToString();
        var DateTo = properties.FirstOrDefault(p => p.Name == "DateTo").GetValue(obj).ToString();
        var CalculationDate = properties.FirstOrDefault(p => p.Name == "CalculationDate").GetValue(obj).ToString();

        return new FormattedCalculationDto()
        {
            CurrentMoneyAmount = (properties.FirstOrDefault(p => p.Name == "CurrentMoneyAmount").GetValue(obj).ToString()),
            MoneyAmount = properties.FirstOrDefault(p => p.Name == "MoneyAmount").GetValue(obj).ToString(),
            MoneyInstallment = properties.FirstOrDefault(p => p.Name == "MoneyInstallment").GetValue(obj).ToString(),
            DateTo = properties.FirstOrDefault(p => p.Name == "DateTo").GetValue(obj).ToString(),
            CalculationDate = properties.FirstOrDefault(p => p.Name == "CalculationDate").GetValue(obj).ToString()
        };
    }
}

