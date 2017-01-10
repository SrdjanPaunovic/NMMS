using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Entities;
using Service.Access;
using System.ServiceModel;

namespace Service
{
	public class Hiring2OutSCompanyService : IHiring2OutSourceContract
	{
		public static Dictionary<string, IHiring2OutSourceContract_CallBack> companies = new Dictionary<string, IHiring2OutSourceContract_CallBack>();
		private static IHiring2OutSourceContract_CallBack callback;

		public bool Introduce(Company company)
		{

			callback = OperationContext.Current.GetCallbackChannel<IHiring2OutSourceContract_CallBack>();
			companies.Add(company.Name, callback);
			return HiringCompanyDB.Instance.AddCompany(company);

		}

		public bool AnswerToRequest(Company company)
		{
			// add to DB and change status to partner(modify current)
            if (company.State == State.CompanyState.NoPartner)
                return HiringCompanyDB.Instance.RemoveCompany(company);
            else
                return HiringCompanyDB.Instance.ModifyCompanyToPartner(company);
		}

		public static IHiring2OutSourceContract_CallBack Callback
		{
			get { return callback; }
			set { callback = value; }
		}


		public bool CloseCompany(Company company)
		{
			companies.Remove(company.Name);
			return HiringCompanyDB.Instance.RemoveCompany(company);
		}


		public bool SendUserStory(Company company, UserStory userStrory, Project project)
		{
			return HiringCompanyDB.Instance.AddUserStory(userStrory);   // nova prica ide u bazu
		}

		public bool AnswerToProject(Company company, Project project)
		{
			return HiringCompanyDB.Instance.AddProject(project);   // odgovor na projekat
		}
	}
}
