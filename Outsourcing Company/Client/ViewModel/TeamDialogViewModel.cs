using Common;
using Common.Entities;
using ServiceContract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModel
{
    public class TeamDialogViewModel
    {

        private IOutsourcingContract proxy;

        public TeamDialogViewModel(Team t = null)
        {
            Proxy = App.Proxy;

            if (team == null)
     
            {
                Team = new Team();
                IsEditing = false;
            }
            else
            {
                IsEditing = true;
                Team = t;
                TeamLead = t.TeamLead;
            }

            if (team.Developers == null)
            {
                teamDevelopers = new ObservableCollection<OcUser>();
            }
            else
            {
                foreach (var developer in team.Developers)
                {
                    teamDevelopers.Add(developer);
                }
            }




            List<OcUser> users = Proxy.GetAllUsersWithoutTeam();
            foreach (OcUser user in users)
            {
                if (user.Role == Role.developer && user.Team == null)
                {
                    Developers.Add(user);
                }
                if (user.Role == Role.TL && user.Team == null)
                {
                    TeamLeads.Add(user);
                }

            }

           }

        #region fields
        private Team team;
        private OcUser teamLead;
        private ObservableCollection<OcUser> developers = new ObservableCollection<OcUser>();
        private ObservableCollection<OcUser> teamLeads = new ObservableCollection<OcUser>();
        private ObservableCollection<OcUser> teamDevelopers = new ObservableCollection<OcUser>();
        private ICommand closeCommand;
        private ICommand saveCommand;
        #endregion

        #region properties
        public ObservableCollection<OcUser> Developers
        {
            get
            {
                return developers;
            }
            set
            {
                developers = value;
            }
        }

        public ObservableCollection<OcUser> TeamDevelopers
        {
            get
            {
                return teamDevelopers;
            }
            set
            {
                teamDevelopers = value;
            }
        }

        public ObservableCollection<OcUser> TeamLeads
        {
            get
            {
                return teamLeads;
            }
            set
            {
                teamLeads = value;
            }
        }

        public OcUser TeamLead
        {
            get
            {
                return teamLead;
            }
            set
            {
                teamLead = value;
            }
        }

        public Team Team
        {
            get
            {
                return team;
            }
            set
            {
                team = value;
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return closeCommand ?? (closeCommand = new RelayCommand(param => this.Close(param)));
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(param => this.Save(param)));
            }
        }
        #endregion

        #region methods
        private void Close(object param)
        {
            (param as Window).Close();
        }

        private void Save(object param)
        {

            if (team.Name == null)
            {
                MessageBox.Show("Please enter the name of Team");
                return;
            }

            if (Team.Developers == null)
            {
                Team.Developers = new List<OcUser>();
            }
            Team.Developers.Clear();
            foreach (var developer in teamDevelopers)
            {
                Team.Developers.Add(developer);
            }
            Team.TeamLead = TeamLead;

            if (team.TeamLead == null)
            {
                MessageBox.Show("TeamLead must be seleceted");
                return;
            }

            if (IsEditing)
            {
               // Proxy.UpdateTeam(Team);
            }
            else
            {
                Proxy.AddTeam(Team);
            }


            (param as Window).Close();
        }
        #endregion

        public bool IsEditing { get; set; }

        public IOutsourcingContract Proxy
        {
            get
            {
                return proxy;
            }

            set
            {
                proxy = value;
            }
        }
    }
}
