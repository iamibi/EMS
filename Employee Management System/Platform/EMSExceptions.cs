using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_System.Platform
{
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException(string message, string exceptionClass) : base($"{exceptionClass}: {message}\n") { }

        public BaseException(string message, string exceptionClass, Exception innerException) : base($"{exceptionClass}: {message}\n", innerException) { }
    }

    [Serializable]
    public class UserAlreadyExists : BaseException
    {
        private const string _classMessage = "User aleardy exists.";
        private const string _className = "UserAlreadyExists";

        public UserAlreadyExists() : base(_classMessage, _className) { }

        public UserAlreadyExists(string customMessage) : base($"{_classMessage}\n{customMessage}", _className) { }

        public UserAlreadyExists(string customMessage, Exception innerException) : base($"{_classMessage}\n{customMessage}", _className, innerException) { }
    }

    [Serializable]
    public class PasswordNotStrongEnough : BaseException
    {
        private const string _classMessage = "The password is not strong enough.";
        private const string _className = "PasswordNotStrongEnough";

        public PasswordNotStrongEnough() : base(_classMessage, _className) { }

        public PasswordNotStrongEnough(string customMessage) : base($"{_classMessage}\n{customMessage}", _className) { }

        public PasswordNotStrongEnough(string customMessage, Exception innerException) : base($"{_classMessage}\n{customMessage}", _className, innerException) { }
    }

    [Serializable]
    public class DbFetchFailed : BaseException
    {
        private const string _classMessage = "Database fetch failed.";
        private const string _className = "DbFetchFailed";

        public DbFetchFailed() : base(_classMessage, _className) { }

        public DbFetchFailed(string customMessage) : base($"{_classMessage}\n{customMessage}", _className) { }

        public DbFetchFailed(string customMessage, Exception innerException) : base($"{_classMessage}\n{customMessage}", _className, innerException) { }
    }

    [Serializable]
    public class DbStoreFailed : BaseException
    {
        private const string _classMessage = "Database store operation failed.";
        private const string _className = "DbStoreFailed";

        public DbStoreFailed() : base(_classMessage, _className) { }

        public DbStoreFailed(string customMessage) : base($"{_classMessage}\n{customMessage}", _className) { }

        public DbStoreFailed(string customMessage, Exception innerException) : base($"{_classMessage}\n{customMessage}", _className, innerException) { }
    }

    [Serializable]
    public class UserCreationFailed : BaseException
    {
        private const string _classMessage = "Failed to create the user.";
        private const string _className = "UserCreationFailed";

        public UserCreationFailed() : base(_classMessage, _className) { }

        public UserCreationFailed(string customMessage) : base($"{_classMessage}\n{customMessage}", _className) { }

        public UserCreationFailed(string customMessage, Exception innerException) : base($"{_classMessage}\n{customMessage}", _className, innerException) { }
    }

    [Serializable]
    public class TaskCreationFailed : BaseException
    {
        private const string _classMessage = "Failed to create the task.";
        private const string _className = "TaskCreationFailed";

        public TaskCreationFailed() : base(_classMessage, _className) { }

        public TaskCreationFailed(string customMessage) : base($"{_classMessage}\n{customMessage}", _className) { }

        public TaskCreationFailed(string customMessage, Exception innerException) : base($"{_classMessage}\n{customMessage}", _className, innerException) { }
    }

    [Serializable]
    public class InvalidEmail : BaseException
    {
        private const string _classMessage = "Invalid Email Passed.";
        private const string _className = "InvalidEmail";

        public InvalidEmail() : base(_classMessage, _className) { }

        public InvalidEmail(string customMessage) : base($"{_classMessage}\n{customMessage}", _className) { }

        public InvalidEmail(string customMessage, Exception innerException) : base($"{_classMessage}\n{customMessage}", _className, innerException) { }
    }

    [Serializable]
    public class InsufficientPrivileges : BaseException
    {
        private const string _classMessage = "The user doesn't have sufficient privileges to perform the operation.";
        private const string _className = "InsufficientPrivileges";

        public InsufficientPrivileges() : base(_classMessage, _className) { }

        public InsufficientPrivileges(string customMessage) : base($"{_classMessage}\n{customMessage}", _className) { }

        public InsufficientPrivileges(string customMessage, Exception innerException) : base($"{_classMessage}\n{customMessage}", _className, innerException) { }
    }

    [Serializable]
    public class InvalidUserRole : BaseException
    {
        private const string _classMessage = "Invalid user role passed.";
        private const string _className = "InvalidUserRole";

        public InvalidUserRole() : base(_classMessage, _className) { }

        public InvalidUserRole(string customMessage) : base($"{_classMessage}\n{customMessage}", _className) { }

        public InvalidUserRole(string customMessage, Exception innerException) : base($"{_classMessage}\n{customMessage}", _className, innerException) { }
    }

    [Serializable]
    public class InvalidPhoneNumber : BaseException
    {
        private const string _classMessage = "Invalid phone number passed.";
        private const string _className = "InvalidPhoneNumber";

        public InvalidPhoneNumber() : base(_classMessage, _className) { }

        public InvalidPhoneNumber(string customMessage) : base($"{_classMessage}\n{customMessage}", _className) { }

        public InvalidPhoneNumber(string customMessage, Exception innerException) : base($"{_classMessage}\n{customMessage}", _className, innerException) { }
    }

    [Serializable]
    public class InternalError : BaseException
    {
        private const string _classMessage = "Something went wrong.";
        private const string _className = "InternalError";

        public InternalError() : base(_classMessage, _className) { }

        public InternalError(string customMessage) : base($"{_classMessage}\n{customMessage}", _className) { }

        public InternalError(string customMessage, Exception innerException) : base($"{_classMessage}\n{customMessage}", _className, innerException) { }
    }

    [Serializable]
    public class RemoveUserFailed : BaseException
    {
        private const string _classMessage = "Unabled to remove the user.";
        private const string _className = "RemoveUserFailed";

        public RemoveUserFailed() : base(_classMessage, _className) { }

        public RemoveUserFailed(string customMessage) : base($"{_classMessage}\n{customMessage}", _className) { }

        public RemoveUserFailed(string customMessage, Exception innerException) : base($"{_classMessage}\n{customMessage}", _className, innerException) { }
    }

    [Serializable]
    public class ValidationFailed : BaseException
    {
        private const string _classMessage = "Unabled to validate the user.";
        private const string _className = "ValidationFailed";

        public ValidationFailed() : base(_classMessage, _className) { }

        public ValidationFailed(string customMessage) : base($"{_classMessage}\n{customMessage}", _className) { }

        public ValidationFailed(string customMessage, Exception innerException) : base($"{_classMessage}\n{customMessage}", _className, innerException) { }
    }
}
