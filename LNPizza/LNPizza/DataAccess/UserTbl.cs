using System;
using System.Collections.Generic;

namespace LNPizza.DataAccess
{
    public partial class UserTbl
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual UserAddress UserAddress { get; set; }
    }
}
