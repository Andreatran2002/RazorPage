using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models{
    // [Table("posts")]
    public class Location{
        [Key]
        public string location_id { get; set; }
        [StringLength(255,MinimumLength =5,ErrorMessage="{0} phải dài từ {2} đến {1}")]
        [Required(ErrorMessage ="{0} phải nhập")]
        [Display(Name="Đường")]
        [Column(TypeName = "nvarchar")]
        public string street_address{ get; set; }
        [StringLength(255,MinimumLength =5,ErrorMessage="{0} phải dài từ {2} đến {1}")]
        [Display(Name="Postal Code")]
        [Column(TypeName = "nvarchar")]
        public string postal_code { get; set; }
        [StringLength(255,MinimumLength =5,ErrorMessage="{0} phải dài từ {2} đến {1}")]
        [Display(Name="Thành phố")]
        [Column(TypeName = "nvarchar")]
        public string city { get; set; }
        
        
        

    }
}