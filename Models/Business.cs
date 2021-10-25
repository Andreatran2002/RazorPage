using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace App.Models{

    public class Business{
        [Key]
        public string BusinessId { get; set; }

        [Display(Name="Ngày bắt đầu  ")]

        [DataType(DataType.Date)]
        public DateTime BusinessFrom{ get; set; }

        [Display(Name="Ngày kết thúc  ")]

       [DataType(DataType.Date)]
        public DateTime BusinessTo{ get; set; }


        public string JobId {set; get;}
        [Display(Name="Chức vụ")]
        [ForeignKey("JobId")]
        public Job JobName{ get; set; }


        [Display(Name="Ghi chú")]

        public string Note{ get; set; }


        public string UserId {set; get;}
        [ForeignKey("UserId")]
        [Display(Name="Nhân viên")]
        public AppUser User { get; set;}

    }
}