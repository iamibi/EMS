using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;

namespace Employee_Management_System.Platform
{
    public class Util
    {
        public static bool IsEmailValid(string emailId)
        {
            try
            {
                // The MailAddress method takes care of the email format validation.
                _ = new MailAddress(emailId);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (ArgumentNullException)
            {
                // If the email is null, return false
                return false;
            }
        }

        public static bool IsNameValid(string name)
        {
            if (Regex.IsMatch(name, @"^[\p{L}\p{M}' \.\-]+$", RegexOptions.None, TimeSpan.FromSeconds(5))) return true;
            return false;
        }

        public static string ExceptionWithBacktrace(string message, Exception ex)
        {
            return $"{message}\n{ex.Message}\nStacktrace: {(ex.InnerException != null ? ex.InnerException.StackTrace : ex.StackTrace)}";
        }

        public static bool IsPasswordSecure(string password)
        {
            return PasswordStrengthValidator.IsValidPassword(password, new PasswordOptions());
        }
    }
}
