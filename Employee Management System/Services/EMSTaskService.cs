using System.Collections.Generic;
using Employee_Management_System.Models;
using MongoDB.Driver;
using Employee_Management_System.Constants;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System;

namespace Employee_Management_System.Services
{
    public class EMSTaskService
    {
        private readonly IMongoCollection<EMSTask> EMSTaskCollection;

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
                System.Diagnostics.Debug.WriteLine("Error occurred while creating connection: " + ex.Message + "\nStacktrace:\n" + ex.StackTrace);
                throw new Exception("DB Connection Error in EMS Task Service.");
            }
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

        public void UpdateTaskById(string taskId, string taskStatus)
        {
            var update = Builders<EMSTask>.Update.Set(EMSTask => EMSTask.Status, taskStatus).CurrentDate(EMSTask => EMSTask.UpdatedAt);
            EMSTaskCollection.UpdateOne(EMSTask => EMSTask.TaskId == taskId, update);
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
