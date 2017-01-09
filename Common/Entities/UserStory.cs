using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace Common.Entities
{
    [DataContract]
    public class UserStory
    {
        public UserStory()
		{
			StartTime = DateTime.Now;
			EndTime = DateTime.Now;
            Tasks = new ObservableCollection<Task>();
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
        public ObservableCollection<Task> Tasks { get; set; }

        public void UpdateProperties(UserStory userStory)
        {
            this.Name = userStory.Name;
            this.State = userStory.State;
            this.StartTime = userStory.StartTime;
            this.StoryPoints = userStory.StoryPoints;
            this.EndTime = userStory.EndTime;
            this.AcceptanceCriteria = userStory.AcceptanceCriteria;
            this.Tasks = userStory.Tasks;
        }
    }
}
