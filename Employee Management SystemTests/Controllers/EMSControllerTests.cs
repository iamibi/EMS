using Microsoft.VisualStudio.TestTools.UnitTesting;
using Employee_Management_System.Platform;
using Employee_Management_System.Models;
using Employee_Management_System.Constants;
using System.Collections.Generic;



namespace Employee_Management_System.Controllers.Tests
{
    internal class CreateHelpers
    {
        public const string testUserEmailId = "miketest@abc.com";
        public const string testUserPassword = "Password123!";

        public static EMSUser CreateEMSUserObject(string emailId, EMSUserRoles role)
        {
            if (string.IsNullOrWhiteSpace(emailId)) emailId = testUserEmailId;

            PasswordHasherUtil pwHash = new PasswordHasherUtil(testUserPassword);

            EMSUser testUser = new()
            {
                FirstName = "Mike",
                LastName = "Test",
                PhoneNumber = "1234567891",
                Role = role.ToString(),
                PasswordHash = pwHash.Digest,
                Salt = pwHash.Salt,
                EmailId = emailId
            };

            return testUser;
        }

        public static EMSUser CreateTestUser(string emailId = null, EMSUserRoles role = EMSUserRoles.Employee)
        {
            EMSUser testUser = CreateEMSUserObject(emailId, role);
            PlatformServices.UserService.CreateEMSUser(testUser);
            return testUser;
        }

        public static void CleanUpEMSUserFromDb(string emailId)
        {
            EMSUser testUser = PlatformServices.UserService.GetEMSUserByEmail(emailId);
            if (testUser == null) throw new System.Exception();

            if (testUser.Role == EMSUserRoles.Employee.ToString())
            {
                List<EMSTask> testUserTasks = PlatformServices.TaskService.GetEMSTasksForEMSUser(testUser.EmployeeId);
                PlatformServices.TaskService.RemoveAllTask(testUserTasks);
            }
            PlatformServices.UserService.RemoveEMSUserById(testUser.EmployeeId);
        }

        public static RegisterViewModel RegisterModel()
        {
            RegisterViewModel newUserModel = new()
            {
                FirstName = "Mike",
                LastName = "Test",
                Email = "miketest1@abc.com",
                PhoneNumber = "1234567891",
                Password = "Password123!"
            };

            return newUserModel;
        }
    }

    [TestClass()]
    public class EMSPlatformHelperTests
    {
        private readonly PlatformHelpers platformHelpers = new();

        [TestMethod()]
        public void LoginTest()
        {
            EMSUser testUser = CreateHelpers.CreateTestUser();

            // Correct Credentials
            bool result = platformHelpers.ValidateEMSUserCredentials(testUser.EmailId, CreateHelpers.testUserPassword);
            Assert.AreEqual(true, result);

            // User doesn't exist on the platform but has valid credentials
            result = platformHelpers.ValidateEMSUserCredentials("mikedab@abc.com", CreateHelpers.testUserPassword);
            Assert.AreEqual(false, result);

            // User enters wrong password
            result = platformHelpers.ValidateEMSUserCredentials(CreateHelpers.testUserEmailId, "Password321!");
            Assert.AreEqual(false, result);

            // User input's malicious code as part of the email field
            result = platformHelpers.ValidateEMSUserCredentials("<script>alert('hello');</script>", CreateHelpers.testUserPassword);
            Assert.AreEqual(false, result);

            // User input's malicious code as part of the password field
            result = platformHelpers.ValidateEMSUserCredentials(CreateHelpers.testUserEmailId, "<script>alert('hello')</script>");
            Assert.AreEqual(false, result);

            // User input's spaces as part of email field
            result = platformHelpers.ValidateEMSUserCredentials("         ", CreateHelpers.testUserPassword);
            Assert.AreEqual(false, result);

            // User input's spaces as part of password field
            result = platformHelpers.ValidateEMSUserCredentials(CreateHelpers.testUserEmailId, "            ");
            Assert.AreEqual(false, result);

            CreateHelpers.CleanUpEMSUserFromDb(CreateHelpers.testUserEmailId);
        }

        [TestMethod()]
        public void GetUserTest()
        {
            EMSUser testUser = CreateHelpers.CreateTestUser();

            // Get the user from the database
            EMSUser storedUser = platformHelpers.GetUser(CreateHelpers.testUserEmailId);
            Assert.AreNotEqual(null, storedUser);
            Assert.AreEqual("Mike", storedUser.FirstName);
            Assert.AreEqual("Test", storedUser.LastName);
            Assert.AreEqual("1234567891", storedUser.PhoneNumber);
            Assert.AreEqual(EMSUserRoles.Employee.ToString(), storedUser.Role);
            Assert.AreNotEqual(null, storedUser.PasswordHash);
            Assert.AreNotEqual("", storedUser.PasswordHash);
            Assert.AreNotEqual(null, storedUser.Salt);
            Assert.AreNotEqual("", storedUser.Salt);
            Assert.AreNotEqual(null, storedUser.EmployeeId);
            Assert.AreNotEqual("", storedUser.EmployeeId);

            // Check the password hash
            PasswordHasherUtil pwHash = new(CreateHelpers.testUserPassword, storedUser.Salt);
            Assert.AreEqual(pwHash.Digest, storedUser.PasswordHash);

            CreateHelpers.CleanUpEMSUserFromDb(CreateHelpers.testUserEmailId);
        }

        [TestMethod()]
        public void IsAdminTest()
        {
            EMSUser testUser = CreateHelpers.CreateTestUser();
            EMSUser testAdminUser = CreateHelpers.CreateTestUser("admin@abc.com", EMSUserRoles.IT_Department);

            // Regular user
            bool isAdmin = platformHelpers.IsAdmin(testUser.EmailId);
            Assert.AreEqual(false, isAdmin);

            // Admin user
            isAdmin = platformHelpers.IsAdmin(testAdminUser.EmailId);
            Assert.AreEqual(true, isAdmin);

            CreateHelpers.CleanUpEMSUserFromDb(testUser.EmailId);
            CreateHelpers.CleanUpEMSUserFromDb(testAdminUser.EmailId);
        }

        [TestMethod()]
        public void RegisterNewUserTest()
        {
            var newUserModel = CreateHelpers.RegisterModel();

            // Valid user is created
            bool isUserCreated = platformHelpers.RegisterNewUser(newUserModel);
            Assert.AreEqual(true, isUserCreated);
            CreateHelpers.CleanUpEMSUserFromDb(newUserModel.Email);

            // Invalid email address
            var invalidUser = CreateHelpers.RegisterModel();
            invalidUser.Email = "mike1234<script>alert('hello');</script>";
            Assert.ThrowsException<UserCreationFailed>(() => platformHelpers.RegisterNewUser(invalidUser));

            // Weak password
            invalidUser = CreateHelpers.RegisterModel();
            invalidUser.Password = "abcd123";
            Assert.ThrowsException<ValidationFailed>(() => platformHelpers.RegisterNewUser(invalidUser));

            // Invalid phone number
            invalidUser = CreateHelpers.RegisterModel();
            invalidUser.PhoneNumber = "+abcd123456742";
            Assert.ThrowsException<ValidationFailed>(() => platformHelpers.RegisterNewUser(invalidUser));

            // Invalid First name
            invalidUser = CreateHelpers.RegisterModel();
            invalidUser.FirstName = "mike1234";
            Assert.ThrowsException<ValidationFailed>(() => platformHelpers.RegisterNewUser(invalidUser));

            // Invalid Last name
            invalidUser = CreateHelpers.RegisterModel();
            invalidUser.LastName = "mike1234";
            Assert.ThrowsException<ValidationFailed>(() => platformHelpers.RegisterNewUser(invalidUser));
        }
    }
}