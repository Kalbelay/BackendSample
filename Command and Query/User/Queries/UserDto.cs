using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries
{
    public class UserDto
    {
        public long Code { get; set; }
        public string EmployeeCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public virtual DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool AllowMultipleUser { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public string TrxnUnit { get; set; }
        public string Remark { get; set; }
    }
}
