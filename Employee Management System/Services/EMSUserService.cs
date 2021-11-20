using Employee_Management_System.Models;
using MongoDB.Driver;
using Employee_Management_System.Constants;
using System.Collections.Generic;
using System;

namespace Employee_Management_System.Services
{
    public class EMSUserService
    {
        private readonly IMongoCollection<EMSUser> EMSUserCollection;

        // Initialize the EMS User Service and setup database connection.
        public EMSUserService()
        {
            try
            {
                MongoClient client = new MongoClient(Database.EMSDbConnection);
                IMongoDatabase database = client.GetDatabase(Database.EMSDb);
                EMSUserCollection = database.GetCollection<EMSUser>(Database.EMSUsers);

                // Create index
                var options = new CreateIndexOptions() { Unique = true };
                var field = new StringFieldDefinition<EMSUser>(EMSModels.EmailId);
                var indexDefinition = new IndexKeysDefinitionBuilder<EMSUser>().Ascending(field);
                var indexModel = new CreateIndexModel<EMSUser>(indexDefinition, options);
                EMSUserCollection.Indexes.CreateOne(indexModel);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error occurred while creating connection: " + ex.Message + "\nStacktrace:\n" + ex.StackTrace);
                throw new Exception("DB Connection Error in EMS User Service.");
            }
        }

        public EMSUser CreateEMSUser(EMSUser newUser)
        {
            EMSUserCollection.InsertOne(newUser);
            return GetEMSUserByEmail(newUser.EmailId);
        }

        public EMSUser GetEMSUserByEmail(string EmailId)
        {
            return EMSUserCollection.Find(EMSUser => EMSUser.EmailId == EmailId).FirstOrDefault();
        }

        public EMSUser GetEMSUserById(string EmployeeId)
        {
            return EMSUserCollection.Find(EMSUser => EMSUser.EmployeeId == EmployeeId).FirstOrDefault();
        }

        public List<EMSUser> GetAllEMSUsers(string emailId = null)
        {
            if (emailId == null) return EMSUserCollection.Find(EMSUser => true).ToList();
            return EMSUserCollection.Find(EMSUser => EMSUser.EmailId != emailId).ToList();
        }

        public List<EMSUser> GetAvailableUsers(string managerEmailId)
        {
            // The query is:
            // 1. Manager email value in the user model should not be null
            // 2. Should not be own id
            // 3. Role should be of employee
            return EMSUserCollection.Find(
                EMSUser => EMSUser.ManagerEmailId == null && EMSUser.EmailId != managerEmailId && EMSUser.Role == EMSUserRoles.Employee.ToString()
            ).ToList();
        }

        public List<EMSUser> GetEMSUsersByManager(string managerEmailId, bool onlyEmails = false)
        {
            if (onlyEmails)
            {
                var fields = Builders<EMSUser>
                    .Projection
                    .Include(p => p.EmailId);
                return EMSUserCollection.Find(EMSUser => EMSUser.ManagerEmailId == managerEmailId).Project<EMSUser>(fields).ToList();
            }
            return EMSUserCollection.Find(EMSUser => EMSUser.ManagerEmailId == managerEmailId).ToList();
        }

        public void AddUserForManager(string managerEmailId, string employeeEmailId)
        {
            // The "Builders" class is used to generte an update dictionary
            // that will be used by the mongo to reference a document's
            // keys and apply the requested operation. In our case, operation
            // is Set the manager email id for the requested user and 
            // update the timestamp of updatedAt.
            var update = Builders<EMSUser>
                .Update
                .Set(EMSUser => EMSUser.ManagerEmailId, managerEmailId)
                .CurrentDate(EMSUser => EMSUser.UpdatedAt);
            EMSUserCollection.UpdateOne(EMSUser => EMSUser.EmailId == employeeEmailId, update);
        }

        public void RemoveManagerFromUsers(string managerEmailId)
        {
            var update = Builders<EMSUser>
                .Update
                .Unset(EMSUser => EMSUser.ManagerEmailId)
                .CurrentDate(EMSUser => EMSUser.UpdatedAt);
            EMSUserCollection.UpdateMany(EMSUser => EMSUser.ManagerEmailId == managerEmailId, update);
        }

        public void RemoveEMSUserByEmail(string emailId)
        {
            EMSUserCollection.DeleteOne(EMSUser => EMSUser.EmailId == emailId);
        }

        public void RemoveEMSUserById(string employeeId)
        {
            EMSUserCollection.DeleteOne(EMSUser => EMSUser.EmployeeId == employeeId);
        }
    }
}
