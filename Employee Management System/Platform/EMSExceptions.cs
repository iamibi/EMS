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
}
