using System;
using System.Collections.Generic;

namespace LNPizza.DataAccess
{
    public partial class UserAddress
    {
        public int UserAddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ProvidenceState { get; set; }
        public string PostalCode { get; set; }
        public string CountryAbrev { get; set; }

        public virtual UserTbl UserAddressNavigation { get; set; }
    }
}
