using System.Text.RegularExpressions;

namespace webbanhang.Libs
{
    public class RegexPassword
    {
        public static bool Validation(string input)
        {
            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            if (validateGuidRegex.IsMatch(input))
            {
                return true;
            }
            return false;
        }
    }
}
