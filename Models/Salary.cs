using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace App.Models{
    // [Table("Salarys")]
    public class Salary{
        [Key]
        public string SalaryId { get; set; }

        [Display(Name="Bậc lương")]
        public int Salary_scale{ get; set; }
        
        [Required(ErrorMessage ="{0} phải nhập")]
        [Display(Name="Lương cơ bản")]
        [Column(TypeName = "nvarchar")]
        public double Basic_salary { get; set; }


        [Required]
        [Display(Name="Hệ số lương")]
        public double Coe_salary { get; set; }
        [Column(TypeName = "ntext")]
        [Display(Name="Hệ số phụ cấp ")]
        public double Allowance_coe { get; set;}  
        
        [Display(Name="Nhân viên")]
        public string UserId {set; get;}
        [ForeignKey("UserId")]
        public AppUser User { get; set;}

    }
}