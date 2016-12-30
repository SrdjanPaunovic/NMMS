using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Common.Entities
{
    public class Company
    {
        private int id;
        private string name;
		private Common.Entities.State.CompanyState state;

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
         
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

		public Common.Entities.State.CompanyState State
		{
			get { return state; }
			set { state = value; }
		}

		
    }
}
