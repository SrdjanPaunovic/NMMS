﻿using Client.View;
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
		private HiringClientProxy proxy = ((App)App.Current).Proxy;
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

			List<UserStory> userStories = proxy.GetUserStoryFromProject(project);
			Project.UserStories = new AsyncObservableCollection<UserStory>(userStories);

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
			LogHelper.GetLogger().Info("Cancel click occurred.");

			var userControl = param as UserControl;
			Window parentWindow = Window.GetWindow(userControl);
			LogHelper.GetLogger().Info(parentWindow.Name + " closed.");
			parentWindow.Close();
		}

		private void SaveClick(object param)
		{
			LogHelper.GetLogger().Info("Save click occurred.");

			var userControl = param as UserControl;
			Window parentWindow = Window.GetWindow(userControl);
           // Project.ProductOwner = proxy.GetUser();   // set Product owner
			bool success = false;
			if (isEditing)
			{
				success = proxy.UpdateProject(Project);
			}
			else
			{
				success = proxy.AddProject(Project);
			}

			if (success)
			{
				parentWindow.Close();
				LogHelper.GetLogger().Info(parentWindow.Name + " closed.");
			}
			else
			{
				LogHelper.GetLogger().Warn("AddProject failed.");
			}
		}

		private void AddStoryClick(object param)
		{
			LogHelper.GetLogger().Info("AddStoryClick called.");

			var name = param as string;
			if (name == String.Empty)
			{
				return;
			}

			UserStory us = new UserStory
			{
				Name = name,
                Project= Project
			};
			Project.UserStories.Add(us);
		}

		private void EditStoryClick(object param)
		{
			LogHelper.GetLogger().Info("EditStoryClick called.");

			var story = param as UserStory;

			UserStoryViewDialog usDialog = new UserStoryViewDialog(story);
			usDialog.ShowDialog();
			LogHelper.GetLogger().Info(usDialog.Name + "shown");

		}

		private void DeleteStoryClick(object param)
		{
			LogHelper.GetLogger().Info("DeleteStoryClick called.");
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
