using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Entities;
using Service.Access;
namespace Service
{
    public class OutSurce2HiringProxy : IHiring2OutSourceContract_CallBack
    {
        private static Dictionary<string, string> hiringAdress = new Dictionary<string, string>();

        public static Dictionary<string, string> HiringAdress
        {
            get
            {
                return hiringAdress;
            }

            set
            {
                hiringAdress = value;
            }
        }

        public bool SendRequest(string ipAdress, Company company)
        {
            HiringAdress.Add(company.Name, ipAdress);
            company.State = State.CompanyState.NoPartner;
            return OutsourcingCompanyDB.Instance.AddCompany(company);
        }

        public bool SendProject(Company company, Project project)
        {
            OcProject proj = new OcProject(project);
            return OutsourcingCompanyDB.Instance.AddProject(proj);
        }

        public bool AnswerToUserStory(Company company, UserStory userStory, Project project)
        {
            if (userStory.IsUserStoryAccepted)
            {
                return OutsourcingCompanyDB.Instance.ModyfyUserStory(userStory);

            }
            else
            {
                return false;
            }
        }
    }
}
