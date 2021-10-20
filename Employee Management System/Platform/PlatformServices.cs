using Employee_Management_System.Services;

namespace Employee_Management_System.Platform
{
    public class PlatformServices
    {
        public static EMSUserService UserService = new EMSUserService();
        public static EMSTaskService TaskService = new EMSTaskService();
    }
}
