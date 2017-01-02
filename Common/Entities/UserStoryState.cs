using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
   public  class UserStoryState
    {

       public enum StoryState
       {
           New=1,
           Active=2,
           Resolved=3,
           Closed=4,
       }
    }
}
