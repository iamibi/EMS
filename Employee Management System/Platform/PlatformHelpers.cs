using System;
using Employee_Management_System.Models;
using System.Collections.Generic;
using Employee_Management_System.Constants;
using System.Security;
using System.Net;

namespace Employee_Management_System.Platform
{
    public class PlatformHelpers
    {
        public List<EMSUser> GetAllUsers(Dictionary<string, object> AccessContext)
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

        public List<EMSTask> GetAllTasksForUser(Dictionary<string, object> AccessContext, string EmployeeId)
        {
            try
            {
                return PlatformServices.TaskService.GetEMSTasksForEMSUser(EmployeeId);
            }
            catch
            {
                // Log here
                throw new System.InvalidOperationException();
            }
        }

        public void UpdateTaskStatusOfUser(Dictionary<string, object> AccessContext, string EmployeeId, EMSTaskStatus TaskStatus)
        {
            
        }

        public Dictionary<string, long> GetCompletedTaskCount(Dictionary<string, object> AccessContext, string ManagerEmailId)
        {
            Dictionary<string, long> TaskCounts = new Dictionary<string, long>();

            try
            {
                List<EMSUser> EMSUsersList = PlatformServices.UserService.GetEMSUsersByManager(ManagerEmailId);

                foreach(EMSUser Iter in EMSUsersList)
                    TaskCounts[Iter.EmailId] = PlatformServices.TaskService.GetTaskCountForEMSUser(Iter.EmployeeId);

                return TaskCounts;
            }
            catch
            {
                // Log here
                throw new Exception();
            }
        }

        public void RemoveUser(Dictionary<string, object> AccessContext, string EmployeeId)
        {
            try
            {
                List<EMSTask> EmployeeTasks = PlatformServices.TaskService.GetEMSTasksForEMSUser(EmployeeId);
                PlatformServices.UserService.RemoveEMSUserById(EmployeeId);
                PlatformServices.TaskService.RemoveAllTask(EmployeeTasks);
            }
            catch
            {
                // Log here
                throw new System.InvalidOperationException();
            }
        }

        public void RegisterNewUser(Dictionary<string, object> AccessContext, Dictionary<string, object> UserParams)
        {
            try
            {
                EMSUser NewUser = new EMSUser();
                DateTime CurrentTime = DateTime.UtcNow;

                NewUser.FirstName = UserParams["first_name"] as string;
                NewUser.LastName = UserParams["last_name"] as string;
                NewUser.CreatedAt = CurrentTime;
                NewUser.UpdatedAt = CurrentTime;
                NewUser.PhoneNumber = UserParams["phone_number"] as string;

                string EmailId = UserParams["email_id"] as string;
                if (!Util.IsEmailValid(EmailId))
                    throw new Exception("Invalid Email Id Passed.");
                NewUser.EmailId = EmailId;

                string Role = UserParams["role"] as string;
                if (!Enum.IsDefined(typeof(EMSUserRoles), Role))
                    throw new Exception("Invalid Role Passed");
                NewUser.Role = Role;

                // Get the password as SecureString.
                SecureString Password = new NetworkCredential("", UserParams["password"] as string).SecurePassword;
                if (!Util.IsPasswordSecure(Password))
                    throw new Exception("Insecure Password.");
                PasswordHasherUtil PwHash = new PasswordHasherUtil(Password);
                Password.Dispose();

                // Store the password digest.
                NewUser.PasswordHash = PwHash.Digest;
                NewUser.Salt = PwHash.Salt;
                
                PlatformServices.UserService.CreateEMSUser(NewUser);
            }
            catch
            {
                // Log Here
                throw new Exception();
            }
        }
    }
}
