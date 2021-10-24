
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace App.Models
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName="nvarchar")]
        [StringLength(400)]
        public string HomeAddress{set;get;}

        [Column(TypeName="nvarchar")]
        [StringLength(400)]
        public string Hometown{set;get;}

        [DataType(DataType.Date)]
        public DateTime? BirthDate{set;get;}

        [DataType(DataType.Date)]
        public DateTime? HireDate{set;get;}

        [Display(Name="Giới tính")]
        [Column(TypeName = "nvarchar")]
        public string Sex { get; set; }

        [Display(Name="Tôn giáo")]
        [Column(TypeName="nvarchar")]
        [StringLength(400)]
        public string Religion{set;get;}

        [Column(TypeName="nvarchar")]
        [StringLength(450)]
        public string JobId{set;get;}

        [ForeignKey("JobId")]
        public Job job{set;get;}


        [Column(TypeName="nvarchar")]
        [StringLength(450)]
        public string DepartmentId{set;get;}

        [ForeignKey("DepartmentId")]
        public Department department{set;get;}
        
        [Column(TypeName="nvarchar")]
        [StringLength(450)]
        public string EducationId{set;get;}

        [ForeignKey("EducationId")]
        public Education education{set;get;}

        public string Avatar { get; set; }

        

        // [Column(TypeName="interger")]
        // public string? SalaryLevel{set;get;}

    }

}