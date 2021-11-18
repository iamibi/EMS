namespace Employee_Management_System.Constants
{
    public class Database
    {
        public const string EMSDb = "EMSDb";
        public const string EMSUsers = "EMSUsers";
        public const string EMSTasks = "EMSTasks";
        public const string EMSDbConnection = "mongodb://localhost:27017";
    }

    public class EMSModels
    {
        public const string FirstName = "fname";
        public const string LastName = "lname";
        public const string Role = "rl";
        public const string PhoneNumber = "pnum";
        public const string EmailId = "em";
        public const string CreatedAt = "cat";
        public const string UpdatedAt = "uat";
        public const string PasswordHash = "pwh";
        public const string Salt = "salt";
        public const string ManagerEmailId = "emng";
        public const string EmployeeId = "emp_id";
        public const string Status = "st";
        public const string TaskDescription = "td";
    }

    public enum EMSUserRoles
    {
        Employee,
        Manager,
        IT_Department
    }

    public enum EMSTaskStatus
    {
        Open,
        InProgress,
        Completed
    }

    public enum PasswordStrength
    {
        /// <summary>
        /// Blank Password (empty and/or space chars only)
        /// </summary>
        Blank = 0,
        /// <summary>
        /// Either too short (less than 5 chars), one-case letters only or digits only
        /// </summary>
        VeryWeak = 1,
        /// <summary>
        /// At least 5 characters, one strong condition met (>= 8 chars with 1 or more UC letters, LC letters, digits & special chars)
        /// </summary>
        Weak = 2,
        /// <summary>
        /// At least 5 characters, two strong conditions met (>= 8 chars with 1 or more UC letters, LC letters, digits & special chars)
        /// </summary>
        Medium = 3,
        /// <summary>
        /// At least 8 characters, three strong conditions met (>= 8 chars with 1 or more UC letters, LC letters, digits & special chars)
        /// </summary>
        Strong = 4,
        /// <summary>
        /// At least 8 characters, all strong conditions met (>= 8 chars with 1 or more UC letters, LC letters, digits & special chars)
        /// </summary>
        VeryStrong = 5
    }
}
