using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceContract
{
	[DataContract]
	public class OSUserAction
	{
		[DataMember]
		private int id;
		[DataMember]
		private string username = string.Empty;
		[DataMember]
		private string password = string.Empty;

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		public OSUserAction(string _username, string _password)
		{
			this.username = _username;
			this.password = _password;
		}

		public OSUserAction()
		{
			// TODO: Complete member initialization
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
	}
}
