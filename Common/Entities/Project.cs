using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Common.Entities
{
	[DataContract]
	public class Project
	{

		public Project()
		{
			UserStories = new ObservableCollection<UserStory>();
			StartTime = DateTime.Now;
			EndTime = DateTime.Now;
			IsAproved = false;
			IsAccepted = false;
			IsProjectRequestSent = false;
		}
		public Project(Project proj)
		{
			this.UpdateProperties(proj);
			this.UserStories = proj.UserStories;
			this.DevelopCompany = proj.DevelopCompany;
			this.HiringCompany = proj.HiringCompany;
		}
		public Project(OcProject proj)
		{
			this.UpdateProperties(proj);
			this.UserStories = proj.UserStories;
			this.DevelopCompany = proj.DevelopCompany;
			this.HiringCompany = proj.HiringCompany;
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
		public ObservableCollection<UserStory> UserStories { get; set; }

		[DataMember]
		public bool IsAproved { get; set; }

		[DataMember]
		public bool IsAccepted { get; set; }

		[DataMember]
		public string HiringCompany { get; set; }

		[DataMember]
		public bool IsProjectRequestSent { get; set; }

		public void UpdateProperties(Project project)
		{
			this.Name = project.Name;
			//this.ProductOwner = project.ProductOwner;
			this.StartTime = project.StartTime;
			this.EndTime = project.EndTime;
			this.Description = project.Description;
			this.IsProjectRequestSent = project.IsProjectRequestSent;
			this.IsAccepted = project.IsAccepted;
			this.IsAproved = project.IsAproved;


		}
	}
}
