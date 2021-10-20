using Employee_Management_System.Models;
using MongoDB.Driver;
using Employee_Management_System.Constants;
using System.Collections.Generic;

namespace Employee_Management_System.Services
{
    public class EMSUserService
    {
        private readonly IMongoCollection<EMSUser> EMSUserCollection;

        // Initialize the EMS User Service and setup database connection.
        public EMSUserService()
        {
            IMongoDatabase database = DatabaseConfigurations.GetCollection();
            EMSUserCollection = database.GetCollection<EMSUser>(Database.EMSUsers);
        }

        public EMSUser CreateEMSUser(EMSUser NewUser)
        {
            EMSUserCollection.InsertOne(NewUser);
            return NewUser;
        }

        public EMSUser GetEMSUserByEmail(string EmailId)
        {
            return EMSUserCollection.Find(EMSUser => EMSUser.EmailId == EmailId).FirstOrDefault();
        }

        public EMSUser GetEMSUserById(string EmployeeId)
        {
            return EMSUserCollection.Find(EMSUser => EMSUser.EmployeeId == EmployeeId).FirstOrDefault();
        }

        public List<EMSUser> GetAllEMSUsers()
        {
            return EMSUserCollection.Find(EMSUser => true).ToList();
        }

        public long GetEMSUsersCount()
        {
            return EMSUserCollection.CountDocuments(EMSUser => true);
        }

        public List<EMSUser> GetEMSUsersByManager(string ManagerEmail)
        {
            return EMSUserCollection.Find(EMSUser => EMSUser.ManagerEmail == ManagerEmail).ToList();
        }

        public void UpdateEMSUserByEmail(string EmailId, Dictionary<string, object> Fields, Dictionary<string, object> UpdateHash)
        { }

        public void UpdateEMSUserById(string Id, Dictionary<string, object> Fields, Dictionary<string, object> UpdateHash)
        { }

        private void UpdateEMSUser(Dictionary<string, object> UpdateHash)
        {
            // TODO
        }

        public void RemoveEMSUserByEmail(string EmailId)
        {
            EMSUserCollection.DeleteOne(EMSUser => EMSUser.EmailId == EmailId);
        }

        public void RemoveEMSUserById(string EmployeeId)
        {
            EMSUserCollection.DeleteOne(EMSUser => EMSUser.EmployeeId == EmployeeId);
        }
    }
}
