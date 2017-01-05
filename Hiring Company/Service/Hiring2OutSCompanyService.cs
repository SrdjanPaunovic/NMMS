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
            return HiringCompanyDB.Instance.AddCompany(company);
        }

        public static IHiring2OutSourceContract_CallBack Callback
        {
            get { return callback; }
            set { callback = value; }
        }
    }
}
