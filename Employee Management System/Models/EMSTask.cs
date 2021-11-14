using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using Employee_Management_System.Constants;

namespace Employee_Management_System.Models
{
    public class EMSTask
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string TaskId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement(EMSModels.EmployeeId)]
        [Required]
        public string EmployeeId { get; set; }

        [BsonElement(EMSModels.TaskDescription)]
        [Required]
        public string TaskDescription { get; set; }

        [BsonElement(EMSModels.Status)]
        [Required]
        public string Status { get; set; }

        [BsonElement(EMSModels.UpdatedAt)]
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
