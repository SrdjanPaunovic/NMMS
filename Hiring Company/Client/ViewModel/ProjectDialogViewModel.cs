using Common;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.ViewModel
{
	public class ProjectDialogViewModel : INotifyPropertyChanged
	{

		#region Fields
		//TODO: INTGR change classes
		private Project project;
		private bool isEditing;

		#endregion Fields

		public ProjectDialogViewModel()
		{
			isEditing = false;
			Project = new Project()
			{
				Name = "New Project"
			};
		}

		public ProjectDialogViewModel(Project project)
		{
			Project = project;

			using (HiringClientProxy proxy = ((App)App.Current).Proxy)
			{
				List<UserStory> userStories = proxy.GetUserStoryFromProject(project);

				Project.UserStories = new ObservableCollection<UserStory>(userStories);
			}

			isEditing = true;
		}

		#region Commands
		private ICommand addStoryCommand;
		private ICommand cancelCommand;
		private ICommand saveCommand;
		private ICommand editStoryCommand;
		private ICommand deleteStoryCommand;

		public ICommand CancelCommand
		{
			get
			{
				return cancelCommand ?? (cancelCommand = new RelayCommand((param) => this.CancelClick(param)));
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				return saveCommand ?? (saveCommand = new RelayCommand((param) => this.SaveClick(param)));
			}
		}

		public ICommand AddStoryCommand
		{
			get
			{
				return addStoryCommand ?? (addStoryCommand = new RelayCommand((param) => this.AddStoryClick(param)));
			}
		}

		public ICommand EditStoryCommand
		{
			get
			{
				return editStoryCommand ?? (editStoryCommand = new RelayCommand((param) => this.EditStoryClick(param)));
			}
		}

		public ICommand DeleteStoryCommand
		{
			get
			{
				return deleteStoryCommand ?? (deleteStoryCommand = new RelayCommand((param) => this.DeleteStoryClick(param)));
			}
		}
		#endregion Commands

		#region Properties
		public Project Project
		{
			get
			{
				return project;
			}

			set
			{
				project = value;
				OnPropertyChanged("Project");
			}
		}
		#endregion Properties

		#region Methods

		private void CancelClick(object param)
		{
			var userControl = param as UserControl;
			Window parentWindow = Window.GetWindow(userControl);
			parentWindow.Close();
		}

		private void SaveClick(object param)
		{
			var userControl = param as UserControl;
			Window parentWindow = Window.GetWindow(userControl);

			using (HiringClientProxy proxy = ((App)App.Current).Proxy)
			{
				bool success = false;
				if (isEditing)
				{
					//TODO proxy.UpdateProject(Project);
				}
				else
				{
					proxy.AddProject(Project);
				}
				if (success)
				{
					//TODO Logger
					parentWindow.Close();
				}
			}
		}

		private void AddStoryClick(object param)
		{
			var name = param as string;
			if(name == String.Empty)
			{
				return;
			}

			UserStory us = new UserStory
			{
				Name = name
			};
			Project.UserStories.Add(us);
		}

		private void EditStoryClick(object param)
		{
			var story = param as UserStory;
			
			
		}

		private void DeleteStoryClick(object param)
		{
			var story = param as UserStory;
			Project.UserStories.Remove(story);

		}
		#endregion Methods

		#region PropertyChangedNotifier
		protected virtual void OnPropertyChanged(string name)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}
