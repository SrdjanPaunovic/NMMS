﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;
using Common;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			LogHelper.GetLogger().Debug("Main window for outsourcing company initialized.");

			/*CollectionView companiesView = (CollectionView)CollectionViewSource.GetDefaultView(partnerCompanies.ItemsSource);
			PropertyGroupDescription companyGroupDescription = new PropertyGroupDescription("State");
			companiesView.GroupDescriptions.Add(companyGroupDescription);

			// Grouping projects
			CollectionView projectsView = (CollectionView)CollectionViewSource.GetDefaultView(projects.ItemsSource);
			PropertyGroupDescription projectGroupDescription = new PropertyGroupDescription("Status");
			projectsView.GroupDescriptions.Add(projectGroupDescription);
			LogHelper.GetLogger().Debug("asdsadasd");*/
		}
	}
}
