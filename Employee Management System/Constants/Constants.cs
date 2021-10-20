namespace Employee_Management_System.Constants
{
    public class Database
    {
        public const string EMSDb = "EMSDb";
        public const string EMSUsers = "EMSUsers";
        public const string EMSTasks = "EMSTasks";
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
    }

    public enum EMSTaskStatus
    {
        Open,
        InProgress,
        Completed
    }
}
