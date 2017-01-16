﻿using Client.ViewModel;
using Common.Entities;
using System;
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
using System.Windows.Shapes;
using Common;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for UserStoryViewDialog.xaml
    /// </summary>
    public partial class UserStoryViewDialog : Window
    {
        public UserStoryViewDialog(UserStory userStory)
        {
            DataContext = new UserStoryViewModel(userStory);
            InitializeComponent();
            LogHelper.GetLogger().Info(" User Story dialog initialized for ." + userStory.Name);

        }
    }
}
