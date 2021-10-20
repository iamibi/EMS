using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using Employee_Management_System.Constants;

namespace Employee_Management_System.Models
{
    public class EMSUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        [BsonElement(EMSModels.FirstName)]
        public string FirstName { get; set; }

        [Required]
        [BsonElement(EMSModels.LastName)]
        public string LastName { get; set; }

        [Required]
        [BsonElement(EMSModels.Role)]
        public string Role { get; set; }

        [Required]
        [BsonElement(EMSModels.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [BsonElement(EMSModels.EmailId)]
        public string EmailId { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [Required]
        [BsonElement(EMSModels.CreatedAt)]
        public DateTime CreatedAt { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [Required]
        [BsonElement(EMSModels.UpdatedAt)]
        public DateTime UpdatedAt { get; set; }

        [Required]
        [BsonElement(EMSModels.PasswordHash)]
        public string PasswordHash { get; set; }

        [Required]
        [BsonElement(EMSModels.Salt)]
        public string Salt { get; set; }

        [Required]
        [BsonElement(EMSModels.ManagerEmailId)]
        public string ManagerEmail { get; set; } = null;
    }
}
