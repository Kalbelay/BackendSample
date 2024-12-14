using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Security
{
    [Table("User", Schema = "Security")]
    public class User
    {
        [Key]
        public virtual long Code { get; set; }
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "User Name is mandatory.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is mandatory.")]
        public string Password { get; set; }
        public string LoggedInStatus { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string TrxnUnit { get; set; }
        public string Remark { get; set; }
    }
}
