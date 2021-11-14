using System;
using Employee_Management_System.Models;
using System.Collections.Generic;
using Employee_Management_System.Constants;

namespace Employee_Management_System.Platform
{
    public class PlatformHelpers
    {
        public bool ValidateEMSUserCredentials(string emailId, string password)
        {
            if (emailId == null || password == null || emailId == "" || password == "" || !Util.IsEmailValid(emailId)) return false;

            // TODO: Check the log file for failed attempts by that user.

            EMSUser user = PlatformServices.UserService.GetEMSUserByEmail(emailId);
            if (user == null) return false;

            // Check the password hash.
            PasswordHasherUtil PwHash = new PasswordHasherUtil(password, user.Salt);
            if (PwHash.Digest != user.PasswordHash) return false;

            return true;
        }

        public List<EMSUser> GetAllUsers(Dictionary<string, object> accessContext)
        {
            try
            {
                return PlatformServices.UserService.GetAllEMSUsers();
            }
            catch
            {
                // Log here
                throw new System.InvalidOperationException();
            }
        }

        public List<EMSTask> GetAllTasksForUser(Dictionary<string, object> accessContext, string employeeId)
        {
            try
            {
                return PlatformServices.TaskService.GetEMSTasksForEMSUser(employeeId);
            }
            catch
            {
                // Log here
                throw new System.InvalidOperationException();
            }
        }

        public void UpdateTaskStatusOfUser(Dictionary<string, object> accessContext, string employeeId, EMSTaskStatus taskStatus)
        {
            
        }

        public Dictionary<string, long> GetCompletedTaskCount(Dictionary<string, object> accessContext, string managerEmailId)
        {
            Dictionary<string, long> taskCounts = new Dictionary<string, long>();

            try
            {
                List<EMSUser> EMSUsersList = PlatformServices.UserService.GetEMSUsersByManager(managerEmailId);

                foreach(EMSUser Iter in EMSUsersList)
                    taskCounts[Iter.EmailId] = PlatformServices.TaskService.GetTaskCountForEMSUser(Iter.EmployeeId);

                return taskCounts;
            }
            catch
            {
                // Log here
                throw new Exception();
            }
        }

        public void RemoveUser(Dictionary<string, object> accessContext, string employeeId)
        {
            try
            {
                List<EMSTask> EmployeeTasks = PlatformServices.TaskService.GetEMSTasksForEMSUser(employeeId);
                PlatformServices.UserService.RemoveEMSUserById(employeeId);
                PlatformServices.TaskService.RemoveAllTask(EmployeeTasks);
            }
            catch
            {
                // Log here
                throw new System.InvalidOperationException();
            }
        }

        public bool RegisterNewUser(RegisterViewModel registerUser)
        {
            
            // Validate the user's email and verify that the user doesn't already exist on the platform.
            if (registerUser.Email == null || registerUser.Email.Trim() == "") return false;
            EMSUser user;

            try
            {
                user = PlatformServices.UserService.GetEMSUserByEmail(registerUser.Email.Trim());
            }
            catch (Exception ex)
            {
                throw new DbFetchFailed(ex.Message, ex.InnerException);
            }

            if (user != null)
                throw new UserAlreadyExists();

            try
            {
                CreateUser(registerUser);
                return true;
            }
            catch (PasswordNotStrongEnough pw)
            {
                throw new PasswordNotStrongEnough("Please improve the password strength.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error occurred: " + ex.Message + "\nStacktrace: " + ex.StackTrace);
                // Log Here
                throw new Exception("Unable to create a new user.");
            }
        }

        private EMSUser CreateUser(RegisterViewModel userParams)
        {
            EMSUser newUser = new EMSUser();
            DateTime currentTime = DateTime.UtcNow;

            newUser.FirstName = userParams.FirstName;
            newUser.LastName = userParams.LastName;
            newUser.CreatedAt = currentTime;
            newUser.UpdatedAt = currentTime;
            newUser.PhoneNumber = userParams.PhoneNumber;

            // Check whether the email id is valid or not.
            string emailId = userParams.Email.Trim();
            if (!Util.IsEmailValid(emailId))
                throw new Exception("Invalid Email Id Passed.");
            newUser.EmailId = emailId;

            // Verify that the role is present in the ENUM.
            string role = EMSUserRoles.Employee.ToString();
            if (!Enum.IsDefined(typeof(EMSUserRoles), role))
                throw new Exception("Invalid Role Passed");
            newUser.Role = role;

            // Check the password string.
            if (!Util.IsPasswordSecure(userParams.Password.Trim()))
                throw new PasswordNotStrongEnough();
            PasswordHasherUtil PwHash = new PasswordHasherUtil(userParams.Password);
            userParams.Password = string.Empty;

            // Store the password digest.
            newUser.PasswordHash = PwHash.Digest;
            newUser.Salt = PwHash.Salt;

            return PlatformServices.UserService.CreateEMSUser(newUser);
        }
    }
}
