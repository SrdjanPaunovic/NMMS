
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    [DataContract]
    public class User
    {

        public User() {
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            Password_changed = DateTime.Now;

        }

        public User(string username, string password, Role role):this()
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
        public Role Role { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        
        [DataMember]
        public DateTime EndTime { get; set; }
        

        [DataMember]
        public DateTime Password_changed { get; set; }


        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public bool IsAuthenticated { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        public void PasswordChange(string new_password){
            Password=new_password;
            Password_changed =DateTime.Now;
        }
    }
}
