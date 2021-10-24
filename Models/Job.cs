using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models{
    // [Table("Job")]
    public class Job{
        [Key]
        [Display(Name="Mã chức vụ")]
        public string JobId { get; set; }


        [StringLength(255,MinimumLength =5,ErrorMessage="{0} phải dài từ {2} đến {1}")]
        [Required(ErrorMessage ="{0} phải nhập")]
        [Display(Name="Tên nghề nghiệp")]
        [Column(TypeName = "nvarchar")]
        public string JobName { get; set; }

    }
}