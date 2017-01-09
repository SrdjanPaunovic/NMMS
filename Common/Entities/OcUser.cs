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
	public class OcUser : User
	{
		[DataMember]
		public Team Team { get; set; }
		public OcUser()
		{

		}
		public OcUser(string username, string password, Role role)
			: base(username, password, role)
		{

		}

	}
}
