using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Common.Entities
{
     [DataContract]
    public class Team
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember]
        public List<OcUser> Developers { get; set; }

        [DataMember]
        public OcUser TeamLead { get; set; }

        [DataMember]
        public List<OcProject> Projects { get; set; }



    }
}
