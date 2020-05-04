using System.Text.RegularExpressions;

namespace Points.Domain.Validations
{
    public class EmailValidation
    {
        public static bool Validate(string email)
        {
            return email != null &&
                Regex.IsMatch(
                email,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase);
        }
    }
}
