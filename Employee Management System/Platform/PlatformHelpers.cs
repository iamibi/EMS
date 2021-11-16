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

        public bool ValidateUser(string emailId)
        {
            if (emailId == null || emailId.Trim() == string.Empty || !Util.IsEmailValid(emailId)) return false;
            EMSUser user = GetUser(emailId);
            if (user == null) return false;
            return true;
        }

        public EMSUser GetUser(string emailId)
        {
            try
            {
                return PlatformServices.UserService.GetEMSUserByEmail(emailId);
            }
            catch (Exception ex)
            {
                throw new DbFetchFailed("", ex);
            }
        }

        public bool IsAdmin(string emailId)
        {
            if (!Util.IsEmailValid(emailId)) return false;

            EMSUser user;
            try
            {
                 user = PlatformServices.UserService.GetEMSUserByEmail(emailId);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(Util.ExceptionWithBacktrace("", ex));
                throw new DbFetchFailed();
            }

            if (user == null) return false;
            if (user.Role != EMSUserRoles.IT_Department.ToString()) return false;

            return true;
        }

        public List<EMSUser> GetAllUsers(string adminEmailId)
        {
            if (!Util.IsEmailValid(adminEmailId))
                throw new InvalidOperationException();

            try
            {
                return PlatformServices.UserService.GetAllEMSUsers(adminEmailId);
            }
            catch
            {
                // Log here
                throw new DbFetchFailed();
            }
        }

        public List<EMSTask> GetAllTasksForUser(string emailId)
        {
            if (!Util.IsEmailValid(emailId))
                throw new InvalidOperationException();

            try
            {
                EMSUser user = PlatformServices.UserService.GetEMSUserByEmail(emailId);
                return PlatformServices.TaskService.GetEMSTasksForEMSUser(user.EmployeeId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(Util.ExceptionWithBacktrace("", ex));
                // Log here
                throw new DbFetchFailed();
            }
        }

        public List<EMSUser> GetAvailableUsers(string managerEmailId)
        {
            if (!Util.IsEmailValid(managerEmailId)) throw new InvalidOperationException();

            EMSUser user = PlatformServices.UserService.GetEMSUserByEmail(managerEmailId);
            if (user.Role != EMSUserRoles.Manager.ToString()) throw new Exception("Invalid access");

            try
            {
                return PlatformServices.UserService.GetAvailableUsers(managerEmailId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(Util.ExceptionWithBacktrace("", ex));
                // Log here
                throw new DbFetchFailed();
            }
        }

        public bool AddUserForManager(string managerEmailId, string employeeEmailId)
        {
            if (!Util.IsEmailValid(managerEmailId) || !Util.IsEmailValid(employeeEmailId)) return false;

            try
            {
                PlatformServices.UserService.AddUserForManager(managerEmailId, employeeEmailId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(Util.ExceptionWithBacktrace("", ex));
                throw new DbFetchFailed(ex.Message, ex);
            }
        }

        public bool UpdateTaskStatusOfUser(string emailId, EmployeeViewModel employeeVM)
        {
            if (!Util.IsEmailValid(emailId)) return false;

            try
            {
                PlatformServices.TaskService.UpdateTaskById(employeeVM.TaskId, employeeVM.TaskStatus.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(Util.ExceptionWithBacktrace("", ex));
                throw new DbFetchFailed("Error occurred while performing action on database.");
            }

            return true;
        }

        public Dictionary<string, long> GetCompletedTaskCount(string managerEmailId)
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

        public void RemoveUser(string adminEmailId, string emailId)
        {
            if (!Util.IsEmailValid(emailId)) throw new InvalidOperationException("The email id is not valid.");

            try
            {
                EMSUser user = PlatformServices.UserService.GetEMSUserByEmail(emailId);

                if (user.Role == EMSUserRoles.Manager.ToString())
                {
                    List<EMSUser> usersWithManager = PlatformServices.UserService.GetEMSUsersByManager(emailId, true);
                    PlatformServices.UserService.RemoveManagerFromUsers(emailId);
                    return;
                }
                List<EMSTask> EmployeeTasks = PlatformServices.TaskService.GetEMSTasksForEMSUser(user.EmployeeId);
                PlatformServices.UserService.RemoveEMSUserById(user.EmployeeId);
                PlatformServices.TaskService.RemoveAllTask(EmployeeTasks);
            }
            catch
            {
                // Log here
                throw new DbFetchFailed();
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

            EMSUser storedUser;
            try
            {
                storedUser = CreateUser(registerUser);
            }
            catch (PasswordNotStrongEnough pw)
            {
                throw new UserCreationFailed("Please improve the password strength.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(Util.ExceptionWithBacktrace("", ex));
                // Log Here
                throw new UserCreationFailed("Unable to create a new user.");
            }

            if (storedUser == null)
                throw new DbFetchFailed("Unable to fetch the user.");

            // Return early if the role is not employee.
            if (storedUser.Role != EMSUserRoles.Employee.ToString()) return true;

            try
            {
                // Create initial task for the user.
                CreateInitialTasksForUser(storedUser);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(Util.ExceptionWithBacktrace("", ex));

                // Remove the user from the database as task creation failed.
                // This makes sure that we don't have inconsistent data in the database.
                PlatformServices.UserService.RemoveEMSUserByEmail(storedUser.EmailId);

                throw new TaskCreationFailed(ex.Message);
            }

            return true;
        }

        private EMSTask[] CreateInitialTasksForUser(EMSUser storedUser)
        {
            int taskCount = 5;
            DateTime currentTime = DateTime.UtcNow;
            EMSTask[] tasks = new EMSTask[taskCount];

            for (int i = 0; i < taskCount; i++)
            {
                EMSTask task = new EMSTask();
                task.EmployeeId = storedUser.EmployeeId.ToString();
                task.Status = EMSTaskStatus.Open.ToString();
                task.TaskDescription = $"Task number: {i}";
                task.UpdatedAt = currentTime;

                try
                {
                    EMSTask storedTask = PlatformServices.TaskService.CreateTask(task);
                    tasks[i] = storedTask;
                }
                catch (Exception ex)
                {
                    throw new DbFetchFailed(ex.Message);
                }
            }

            return tasks;
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
