using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using System.Runtime.Serialization;


namespace Common.Entities
{
    [DataContract]
    public class Company
    {
        [DataMember]
        private int id;
        [DataMember]
        private string name;
        [DataMember]
        private Common.Entities.State.CompanyState state;

        public Company() { }

        public Company(string name)
        {
            this.name = name;
            this.state = Common.Entities.State.CompanyState.NoPartner;

        }

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
