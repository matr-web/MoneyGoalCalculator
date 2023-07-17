using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MoneyGoalCalculator.Models.Dto_s;

/// <summary>
/// Calculation Data Transfer Object.
/// </summary>
public class CalculationDto
{
    /// <summary>
    /// Calculation Primary Key.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Amount of money User already has saved.
    /// </summary>
    public string CurrentMoneyAmount { get; set; }

    /// <summary>
    /// Amount of money User wants to save.
    /// </summary>
    public string MoneyAmount { get; set; }

    /// <summary>
    /// Amount of money User will have to pay to achieve his goal.
    /// </summary>
    public string MoneyInstallment { get; set; }

    /// <summary>
    /// Date until User wants to achieve his saving goal.
    /// </summary>
    [DataType(DataType.Date)]
    public DateTime DateTo { get; set; }

    /// <summary>
    /// Time each calculation was requested.
    /// </summary>
    [Required]
    public DateTime CalculationDate { get; set; }

    /// <summary>
    /// Mapper: Collection of object type -> collection of Calculation DTO.
    /// </summary>
    /// <param name="entities">Collection of object type that will be mapped to Calculation DTO collection.</param>
    /// <returns>Collection of Calculation DTO with data from properties of the given object collection.</returns>
    public static IEnumerable<CalculationDto> ToCollectionOfCalculationDto(IEnumerable<object> objCollection)
    {
        var dtos = new List<CalculationDto>();

        foreach (var obj in objCollection)
        {
            Type type = obj.GetType();
            var properties = type.GetProperties();

            // Check if in given object the Id value is given.
            var idHasValue = properties.FirstOrDefault(p => p.Name == "Id") != null;

            var dto = new CalculationDto()
            {
                Id = idHasValue == true ? Convert.ToInt32(properties.FirstOrDefault(p => p.Name == "Id").GetValue(obj)) : null,
                CurrentMoneyAmount = RemoveCurrency(properties.FirstOrDefault(p => p.Name == "CurrentMoneyAmount").GetValue(obj).ToString()),
                MoneyAmount = RemoveCurrency(properties.FirstOrDefault(p => p.Name == "MoneyAmount").GetValue(obj).ToString()),
                MoneyInstallment = RemoveCurrency(properties.FirstOrDefault(p => p.Name == "MoneyInstallment").GetValue(obj).ToString()),
                DateTo = Convert.ToDateTime(properties.FirstOrDefault(p => p.Name == "DateTo").GetValue(obj)),
                CalculationDate = Convert.ToDateTime(properties.FirstOrDefault(p => p.Name == "CalculationDate").GetValue(obj))
            };

            dtos.Add(dto);
        }

        return dtos;
    }

    /// <summary>
    /// Mapper: object type -> Calculation DTO.
    /// </summary>
    /// <param name="obj">Object whose properties will be mapped to Calculation DTO.</param>
    /// <returns>Calculation DTO with data from properties of the given object.</returns>
    public static CalculationDto ToCalculationDto(object obj)
    {
        Type type = obj.GetType();
        var properties = type.GetProperties();

        // Check if in given object the Id value is given.
        var idHasValue = properties.FirstOrDefault(p => p.Name == "Id") != null;

        return new CalculationDto()
        {
            Id = idHasValue == true ? Convert.ToInt32(properties.FirstOrDefault(p => p.Name == "Id").GetValue(obj)) : null,
            CurrentMoneyAmount = RemoveCurrency(properties.FirstOrDefault(p => p.Name == "CurrentMoneyAmount").GetValue(obj).ToString()),
            MoneyAmount = RemoveCurrency(properties.FirstOrDefault(p => p.Name == "MoneyAmount").GetValue(obj).ToString()),
            MoneyInstallment = RemoveCurrency(properties.FirstOrDefault(p => p.Name == "MoneyInstallment").GetValue(obj).ToString()),
            DateTo =  Convert.ToDateTime(properties.FirstOrDefault(p => p.Name == "DateTo").GetValue(obj)),
            CalculationDate = Convert.ToDateTime(properties.FirstOrDefault(p => p.Name == "CalculationDate").GetValue(obj))
        };
    }

    /// <summary>
    /// Remove currency information from string.
    /// </summary>
    /// <param name="stringWithCurrency">String variable whose currency variable will be removed.</param>
    /// <returns>String variable with removed currency information or null if it was not possible to format given variable.</returns>
    private static string RemoveCurrency(string stringWithCurrency)
    {
        decimal value;
        bool flag = decimal.TryParse(stringWithCurrency, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out value);

        if(flag) 
        { 
            return value.ToString();
        }

        return null; 
    }
}
