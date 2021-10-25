using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace App.Models{

    public class WorkCheck{
        [Key]
        public string WorkCheckId { get; set; }

        [Display(Name="Ngày chấm công ")]

        [DataType(DataType.Date)]
        public DateTime WorkDate{ get; set; }

        [Display(Name="Trạng thái làm việc ")]
        public string WorkStatusId { get; set; }

        [ForeignKey("WorkStatusId")]
        [Display(Name="Nhân viên")]
        public WorkStatus WorkStatus{ get; set; }

        [Display(Name="Nhân viên")]
        public string UserId {set; get;}
        [ForeignKey("UserId")]
        public AppUser User { get; set;}

    }
}