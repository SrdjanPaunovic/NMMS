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
        public string Name { get; set; }

        [DataMember]
        public List<OcUser> Developers { get; set; }

        public OcUser TeamLead
        {
            get
            {
                return Developers.FirstOrDefault(x => x.Role == Role.TL);
            }
            set
            {
                if (value.Role == Role.TL)
                {
                    var currentTL = Developers.FirstOrDefault(x => x.Role == Role.TL);
                    if (currentTL != null)
                    {
                        Developers.Remove(currentTL);
                    }
                    Developers.Add(value);
                }
            }
        }

        [DataMember]
        public List<OcProject> Projects { get; set; }
    }
}
