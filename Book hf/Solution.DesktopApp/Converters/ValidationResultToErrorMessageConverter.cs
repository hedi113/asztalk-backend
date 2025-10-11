
namespace Solution.DesktopApp.Converters;

public class ValidationResultToErrorMessageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo cultureInfo)
    {
        if(value is not ValidationResult validationResult || validationResult.IsValid)
        {
            return null;
        }
        if(parameter == null)
        {
            return null;
        }

        var property = parameter as string;
        var errorMessage = validationResult.Errors.Where(x => x.PropertyName == property)
                                                  .Select(x => x.ErrorMessage);

        return string.Join(Environment.NewLine, errorMessage);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo cultureInfo)
    {
        throw new NotImplementedException("ConvertBack not implemented for the converter.");
    }
}
