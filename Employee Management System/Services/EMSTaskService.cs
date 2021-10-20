using System.Collections.Generic;
using Employee_Management_System.Models;
using MongoDB.Driver;
using Employee_Management_System.Constants;
using System.Linq;

namespace Employee_Management_System.Services
{
    public class EMSTaskService
    {
        private readonly IMongoCollection<EMSTask> EMSTaskCollection;

        public EMSTaskService()
        {
            IMongoDatabase database = DatabaseConfigurations.GetCollection();
            EMSTaskCollection = database.GetCollection<EMSTask>(Database.EMSTasks);
        }

        public EMSTask GetEMSTaskById(string TaskId)
        {
            return EMSTaskCollection.Find(EMSTask => EMSTask.TaskId == TaskId).FirstOrDefault();
        }

        public List<EMSTask> GetEMSTasksForEMSUser(string EmployeeId)
        {
            return EMSTaskCollection.Find(EMSTask => EMSTask.EmployeeId == EmployeeId).ToList();
        }

        public long GetTaskCountForEMSUser(string EmployeeId)
        {
            long count = EMSTaskCollection.CountDocuments(EMSTask => EMSTask.EmployeeId == EmployeeId);
            return count;
        }

        public EMSTask CreateTask(EMSTask task)
        {
            EMSTaskCollection.InsertOne(task);
            return task;
        }

        public void UpdateTaskById(string TaskId, Dictionary<string, object> UpdateDictionary)
        {
        }

        public void RemoveTaskById(string TaskId)
        { }

        public async void RemoveAllTask(List<EMSTask> Tasks)
        {
            var filter = Builders<EMSTask>.Filter.In("_id", Tasks.Select(i => i.TaskId));
            await EMSTaskCollection.DeleteManyAsync(filter);
        }
    }
}
