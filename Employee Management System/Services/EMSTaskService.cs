using System.Collections.Generic;
using Employee_Management_System.Models;
using MongoDB.Driver;
using Employee_Management_System.Constants;
using System.Linq;
using System;
using Employee_Management_System.Platform;

namespace Employee_Management_System.Services
{
    public class EMSTaskService
    {
        private readonly IMongoCollection<EMSTask> EMSTaskCollection;
        private static readonly ILoggerManager logger = new LoggerManager();

        public EMSTaskService()
        {
            try
            {
                MongoClient client = new MongoClient(Database.EMSDbConnection);
                IMongoDatabase database = client.GetDatabase(Database.EMSDb);
                EMSTaskCollection = database.GetCollection<EMSTask>(Database.EMSTasks);
            }
            catch (Exception ex)
            {
                logger.LogError(Util.ExceptionWithBacktrace("Error occurred while trying to open a connection to the database.", ex));
                throw new Exception("DB Connection Error in EMS Task Service.");
            }
        }

        public List<EMSTask> GetEMSTasksForEMSUser(string EmployeeId)
        {
            return EMSTaskCollection.Find(EMSTask => EMSTask.EmployeeId == EmployeeId).ToList();
        }

        public long GetCompletedTaskCountForEMSUser(string EmployeeId)
        {
            long count = EMSTaskCollection.CountDocuments(EMSTask => EMSTask.EmployeeId == EmployeeId && EMSTask.Status == EMSTaskStatus.Completed.ToString());
            return count;
        }

        public EMSTask CreateTask(EMSTask task)
        {
            EMSTaskCollection.InsertOne(task);
            return task;
        }

        public void UpdateTaskById(string taskId, string taskStatus)
        {
            var update = Builders<EMSTask>
                .Update
                .Set(EMSTask => EMSTask.Status, taskStatus)
                .CurrentDate(EMSTask => EMSTask.UpdatedAt);
            EMSTaskCollection.UpdateOne(EMSTask => EMSTask.TaskId == taskId, update);
        }

        public async void RemoveAllTask(List<EMSTask> Tasks)
        {
            var filter = Builders<EMSTask>.Filter.In("_id", Tasks.Select(i => i.TaskId));
            await EMSTaskCollection.DeleteManyAsync(filter);
        }
    }
}
