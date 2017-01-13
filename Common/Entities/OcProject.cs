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
    public class OcProject:Project
    {
        [DataMember]
        public Team Team { get; set; }

		public OcProject()
		{

		}
		public OcProject(Project proj):base(proj)
		{
			
		} 
	
    }
}
