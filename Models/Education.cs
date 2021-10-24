using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models{
    // [Table("Education")]
    public class Education{
        [Key]
        [Display(Name="Mã trình độ giáo dục")]
        public string EducationId { get; set; }

        [StringLength(255,MinimumLength =5,ErrorMessage="{0} phải dài từ {2} đến {1}")]
        [Display(Name="Trình độ")]
        [Column(TypeName = "nvarchar")]
        public string EducationName { get; set; } [StringLength(255,MinimumLength =5,ErrorMessage="{0} phải dài từ {2} đến {1}")]


        [Display(Name="Chuyên ngành")]
        [Column(TypeName = "nvarchar")]
        public string Specialization { get; set; }

        

    }
}