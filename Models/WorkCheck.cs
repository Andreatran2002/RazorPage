using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace App.Models{

    public class WorkCheck{
        [Key]
        public string WorkCheckId { get; set; }

        [Display(Name="Ngày chấm công ")]

        [Column(TypeName="date")]
        public DateTime WorkDate{ get; set; }

        [Display(Name="Tác vụ")]

        public string WorkStatus{ get; set; }

        [Display(Name="Nhân viên")]
        public string UserId {set; get;}
        [ForeignKey("UserId")]
        public AppUser User { get; set;}

    }
}