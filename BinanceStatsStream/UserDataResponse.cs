using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class UserDataResponse
    {
        public string accountAlias { get; set; }
        public string asset { get; set; }
        public string balance { get; set; }
        public string crossWalletBalance { get; set; }
        public string crossUnPnl { get; set; }
        public string availableBalance { get; set; }
        public string maxWithdrawAmount { get; set; }
    }
}
