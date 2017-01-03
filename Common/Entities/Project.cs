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
    public class Project
    {

        public Project() {
            UserStories = new List<UserStory>();
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
        }


        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember]
        public User ProductOwner { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public Company DevelopCompany { get; set; }

        [DataMember]
        public List<UserStory> UserStories{ get; set; }


    }
}
