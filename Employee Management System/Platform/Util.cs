using System;
using System.Security;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Employee_Management_System.Constants;

namespace Employee_Management_System.Platform
{
    public class Util
    {
        private const string RxDigits = @"/\d+/";
        private const string RxLowerCase = @"/[a-z]/";
        private const string RxUpperCase = @"/[A-Z]/";
        private const string RxSpecialChars = @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/";

        public static bool IsEmailValid(string EmailId)
        {
            try
            {
                _ = new MailAddress(EmailId);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsPasswordSecure(SecureString Password)
        {
            PasswordScore PasswordSc = CheckStrength(Password.ToString());
            switch (PasswordSc)
            {
                case PasswordScore.Blank:
                case PasswordScore.VeryWeak:
                case PasswordScore.Weak:
                    return false;
                case PasswordScore.Medium:
                case PasswordScore.Strong:
                case PasswordScore.VeryStrong:
                    // Password deemed strong enough
                    return true;
            }

            return false;
        }

        private static PasswordScore CheckStrength(string Password)
        {
            int Score = 0;

            if (Password.Length < 1)
                return PasswordScore.Blank;
            if (Password.Length < 4)
                return PasswordScore.VeryWeak;

            if (Password.Length >= 8)
                Score++;
            if (Password.Length >= 12)
                Score++;
            if (Regex.Match(Password, RxDigits, RegexOptions.ECMAScript).Success)
                Score++;
            if (Regex.Match(Password, RxLowerCase, RegexOptions.ECMAScript).Success && Regex.Match(Password, RxUpperCase, RegexOptions.ECMAScript).Success)
                Score++;
            if (Regex.Match(Password, RxSpecialChars, RegexOptions.ECMAScript).Success)
                Score++;

            return (PasswordScore)Score;
        }
    }
}
