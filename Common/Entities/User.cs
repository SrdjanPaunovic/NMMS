using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Common.Entities
{
    [DataContract]
    public class User
    {
        public User()
        {
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            PasswordChanged = DateTime.Now;
        }

        public User(string username, string password, Roles.Role role) : this()
        {
            Username = username;
            Password = password;
            Role = role;
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember]
        public Roles.Role Role { get; set; }
        /*
        [DataMember]
        public Project Project { get; set; }*/

        [DataMember]
        public DateTime StartTime { get; set; }


        [DataMember]
        public DateTime EndTime { get; set; }


        [DataMember]
        public DateTime PasswordChanged { get; set; }


        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public bool IsAuthenticated { get; set; }

        [DataMember]
      //  [Index(IsUnique = true)]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        public void PasswordChange(string newPassword)
        {
            Password = newPassword;
            PasswordChanged = DateTime.Now;
        }
    }
}