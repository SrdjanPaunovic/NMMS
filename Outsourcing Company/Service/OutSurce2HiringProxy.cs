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
        public static Dictionary<string, string> hiringAdress = new Dictionary<string, string>();

        public bool SendRequest(string ipAdress, Company company)
        {
            hiringAdress.Add(company.Name, ipAdress);
            return OutsourcingCompanyDB.Instance.AddCompany(company);
        }
    }
}
