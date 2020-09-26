using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SagarPractical.ViewModels
{
    public class clsPlayerViewModel
    {
        public int playerid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public Nullable<int> totalmatchesplayed { get; set; }
        public string contactnumber { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> dateofBirth { get; set; }
        public Nullable<decimal> playerheight { get; set; }
        public Nullable<decimal> playerweight { get; set; }
        public Nullable<int> role { get; set; }
        public Nullable<bool> isselected { get; set; }
        public Nullable<bool> flagdeleted { get; set; }
        public string password { get; set; }
    }
}