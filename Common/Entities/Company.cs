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
        

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

        public string Name { get; set; }


        public Common.Entities.State.CompanyState State { get; set; }
		
		
    }
}
