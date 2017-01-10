using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
using Service.Access;
using Common;
using Common.Entities;
using System.ServiceModel;


namespace Service
{
    public class OutsourcingCompanyService : IOutsourcingCompanyService
    {



        public bool AddUser(OcUser user)
        {
			LogHelper.GetLogger().Info("Call AddUser method.");

            return OutsourcingCompanyDB.Instance.AddUser(user);
        }

        public bool AddCompany(Company company)
        {
			LogHelper.GetLogger().Info("Call AddCompany method.");
			return OutsourcingCompanyDB.Instance.AddCompany(company);
        }

        public bool AddProject(OcProject project)
        {
			LogHelper.GetLogger().Info("Call AddProject method.");
			return OutsourcingCompanyDB.Instance.AddProject(project);
        }

        public bool AddUserStory(UserStory userStory)
        {
			LogHelper.GetLogger().Info("Call AddUserStory method.");
			return OutsourcingCompanyDB.Instance.AddUserStory(userStory);
        }

        public bool AddTask(Common.Entities.Task task)
        {
			LogHelper.GetLogger().Info("Call AddTask method.");
			return OutsourcingCompanyDB.Instance.AddTask(task);
        }

        public bool AddTeam(Team team)
        {
			LogHelper.GetLogger().Info("Call AddTeam method.");
			return OutsourcingCompanyDB.Instance.AddTeam(team);
        }

        public bool LogIn(string username, string password)
        {
			LogHelper.GetLogger().Info("Call Login method.");
			return OutsourcingCompanyDB.Instance.LogIn(username, password);
        }

        public bool LogOut(string username)
        {
			LogHelper.GetLogger().Info("Call LogOut method.");
			return OutsourcingCompanyDB.Instance.LogOut(username);
        }

        public bool UserRegister(OcUser user)
        {
			LogHelper.GetLogger().Info("Call UserRegister method.");
			return OutsourcingCompanyDB.Instance.UserRegister(user);
        }

        public List<OcUser> LoginUsersOverview()
        {
			LogHelper.GetLogger().Info("Call LoginUsersOverview method.");
			return OutsourcingCompanyDB.Instance.LoginUsersOverview();
        }

        public List<Company> GetAllCompanies()
        {
			LogHelper.GetLogger().Info("Call GetAllCompanies method.");
			return OutsourcingCompanyDB.Instance.GetAllCompanies();
        }


        public bool AnswerToRequest(Company company)
        {
            try
            {
                string ipAdress = OutSurce2HiringProxy.hiringAdress[company.Name];
                Program.factory = new DuplexChannelFactory<IHiring2OutSourceContract>(Program.instanceContext, new NetTcpBinding(SecurityMode.None), new EndpointAddress(ipAdress));
                IHiring2OutSourceContract proxy1 = Program.factory.CreateChannel();
                Program.myOutSourceCompany.State = company.State;
                proxy1.AnswerToRequest(Program.myOutSourceCompany);
                return true;
            }
            catch
            {
                return false;
            }
            
        }


		public List<OcProject> GetAllProjects()
		{
			LogHelper.GetLogger().Info("Call GetAllProjects method.");
			return OutsourcingCompanyDB.Instance.GetAllProjects();
		}

		public OcUser GetUser(string username)
		{
			//throw new NotImplementedException();
			LogHelper.GetLogger().Info("Call GetUser method.");
			return OutsourcingCompanyDB.Instance.GetUser(username);           ///////////////////////odje
		}

		public bool UpdateUser(OcUser user)
		{
			//throw new NotImplementedException();
			LogHelper.GetLogger().Info("Call UpdateUser method.");
			return OutsourcingCompanyDB.Instance.UpdateUser(user);
		}





		public bool SendUserStory(Company company, UserStory userStrory, Project project)
		{
			return OutsourcingCompanyDB.Instance.AddUserStory(userStrory);   // promeniti u bazi (modyfy)
		}

		public bool AnswerToProject(Company company, Project project)
		{
			try
			{
				string ipAdress = OutSurce2HiringProxy.hiringAdress[company.Name];
				Program.factory = new DuplexChannelFactory<IHiring2OutSourceContract>(Program.instanceContext, new NetTcpBinding(SecurityMode.None), new EndpointAddress(ipAdress));
				IHiring2OutSourceContract proxy1 = Program.factory.CreateChannel();
				proxy1.AnswerToProject(Program.myOutSourceCompany, project);
				return true;

			}
			catch (Exception)
			{
				return false;
				throw;
			}
			
		}


        public bool ModifyCompany(Company company)
        {
            return OutsourcingCompanyDB.Instance.ModifyCompanyToPartner(company);
        }


        public bool ChangeCompanyState(Company company, State.CompanyState state)
        {
            return OutsourcingCompanyDB.Instance.ChangeCompanyState(company, state);
        }


        public bool RemoveCompany(Company company)
        {
            return OutsourcingCompanyDB.Instance.RemoveCompany(company);
        }
    }
}
