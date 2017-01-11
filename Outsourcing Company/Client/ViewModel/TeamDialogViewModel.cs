using Common;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModel
{
    public class TeamDialogViewModel
    {
        public TeamDialogViewModel(Team team = null)
        {
            if (team == null)
            {
                Team = new Team();
            }
            else
            {
                Team = team;
            }
            using (OutSClientProxy proxy = ((App)Application.Current).Proxy)
            {
                List<OcUser> users = proxy.GetAllUsers();
                foreach (OcUser user in users)
                {
                    if (user.Role == Role.developer && user.Team == null)
                    {
                        Developers.Add(user);
                    }
                }
            }
        }

        #region fields
        private Team team;
        private List<OcUser> developers = new List<OcUser>();
        private ICommand closeCommand;
        private ICommand saveCommand;
        #endregion

        #region properties
        public List<OcUser> Developers
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
            //TODO save team
            (param as Window).Close();
        }
        #endregion
    }
}
