using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    [DataContract]
    public class UserStory
    {
        public UserStory()
		{
			StartTime = DateTime.Now;
			EndTime = DateTime.Now;
		}

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string AcceptanceCriteria { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public Project Project { get; set; }

        [DataMember]
        public StoryState State { get; set; }

        [DataMember]
        public int StoryPoints { get; set; }

        [DataMember]
        public List<Task> Tasks { get; set; }
    }
}
