# Employee Management System (EMS)
Project focuses on creating and managing the employees.

# Requirements
1. Windows machine
2. MongoDb latest version
3. Enabled localhost certificate for SSL
4. Visual Studio Code 2019

# Packages Required
1. MongoDb .NET library
2. HTML Sanitizer library
3. Log4Net library

# Steps to Setup EMS Project
1. In the code, there are few changes that needs to be done. You need to create an admin user and a manager user. To do that, go to PlatformHelpers.cs file -> CreateUser() -> string role = EMSUserRoles.Employee.ToString(); -> change this line to "string role = EMSUserRoles.Manager.ToString();" for manager and "string role = EMSUserRoles.ITDepartment.ToString();" for Admin after every run.
2. Run the application and create the above two new users. One user for every run.
3. Once the manager and admins are created, revert the code change to "string role = EMSUserRoles.Employee.ToString();".
4. Make sure to refactor the location of log files mentioned in log4net.config file to the correct location you have cloned the repository from. It should be something like "C:\path_to_EMS\EMS\Employee Management System\Logs\". Also, make the same change in the PlatformHelpers.cs file, under the method ReachedLoginAttemps method.
5. Now you can run the application as and test around.

# Bugs
Add the bugs below if you find any:
- The status change of a task is not reflected and needs to be fixed.
