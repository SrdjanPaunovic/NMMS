using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	[DataContract(Name = "Role")]
	public enum Role
	{
		[EnumMember]
		CEO = 0,
		[EnumMember]
		developer,
		[EnumMember]
		PO,
		[EnumMember]
		SM,
        [EnumMember]
        TL,
        [EnumMember]
        HR
	}

	[DataContract(Name = "StoryState")]
	public enum StoryState
	{
		[EnumMember]
		New = 0,
		[EnumMember]
		Active,
		[EnumMember]
		Resolved,
		[EnumMember]
		Closed
	}
	[DataContract(Name = "CompanyState")]
	public enum CompanyState
	{
		[EnumMember]
		NoPartner = 0,
		[EnumMember]
		RequestSent = 1,
		[EnumMember]
		Partner = 2
	}

    public enum WindowState
    {
        LOGIN,
        EMPLOYEES,
        COMPANIES,
        PROJECTS
    }

    public enum CompanyType
    {
        HIRING = 0,
        OUTSOURCING
    }
}
