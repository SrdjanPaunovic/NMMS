using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Project
    {
        private int id;
		private List<User> develpers;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        

        public List<User> Develpers
        {
            get { return develpers; }
            set { develpers = value; }
        }


    }
}
