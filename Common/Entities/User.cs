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
        private int id;

        private string username;

		private string password;		
        private string name;
        private string surname;
        private bool isAuthenticated;
		private DateTime password_changed;
        private Roles.Role role;
		[DataMember]  
        public Project Project { get; set; }

		public User() { }

		public User(string username, string password, Roles.Role role)
		{
			this.username = username;
			this.password = password;
			password_changed = DateTime.Now;
			this.role = role;
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		[DataMember]
        public Roles.Role Role
        {
            get { return role; }
            set { role = value; }
        }

       

        
		[DataMember]
		public DateTime Password_changed
		{
			get { return password_changed; }
			set { password_changed = value; }
		}
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
            set { isAuthenticated = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
			set { password = value; }
        }
        public void PasswordChange(string newPassword){
            this.password_changed = DateTime.Now;
        }

    }
}
