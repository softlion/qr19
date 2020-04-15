using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qr19.Lib.Models
{
    public class QrViewModel
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateLeaveHome { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string BirthInfo { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Address { get; set; }
        public List<string> Reasons { get; set; }

        public TimeSpan LeaveDuration => DateTime.Now - DateLeaveHome;
        public int LeaveDurationTotalMinutes => (int)Math.Ceiling(LeaveDuration.TotalMinutes);
        public int Age => (int)((DateTime.Now - BirthDate).TotalDays/365);
        public string ReasonString => Reasons.Aggregate(new StringBuilder(), (sb, s) => sb.Append(s).Append(", "), sb => sb.Remove(sb.Length - 2, 2).ToString());

        ////TODO
        //public int HomeDistanceMeter { get; set; }
        //public bool IsHomeDistanceMeterReady { get; set; }
    }
}
