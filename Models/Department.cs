using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models{
    // [Table("posts")]
    public class Department{
        [Key]
        [Display(Name="Mã phòng ban")]
        public string Department_id { get; set; }

        [StringLength(255,MinimumLength =5,ErrorMessage="{0} phải dài từ {2} đến {1}")]
        [Required(ErrorMessage ="{0} phải nhập")]
        [Display(Name="Tên phòng ban")]
        [Column(TypeName = "nvarchar")]
        public string Department_name { get; set; }

        [StringLength(255,MinimumLength =5,ErrorMessage="{0} phải dài từ {2} đến {1}")]
        [Display(Name="Địa chỉ")]
        [Column(TypeName = "nvarchar")]
        public string HomeAddress { get; set; }
        
    
        [Display(Name="Điện thoại")]
        [Phone]
        public string Phone { get; set; }
        

    }
}