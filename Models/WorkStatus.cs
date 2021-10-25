using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace App.Models{

    public class WorkStatus{
        [Key]
        public string WorkStatusId { get; set; }

        [Display(Name="Trạng thái làm việc")]
        public string Status{ get; set; }

    }
}