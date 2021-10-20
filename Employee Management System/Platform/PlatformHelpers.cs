using Employee_Management_System.Models;
using System.Collections.Generic;
using Employee_Management_System.Constants;

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

        public long GetCompletedTaskCount(Dictionary<string, object> AccessContext, string ManagerEmailId)
        { }

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
        { }
    }
}
