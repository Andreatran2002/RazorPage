using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models{
    // [Table("posts")]
    public class Article{
        [Key]
        public int Id { get; set; }
        [StringLength(255,MinimumLength =5,ErrorMessage="{0} phải dài từ {2} đến {1}")]
        [Required(ErrorMessage ="{0} phải nhập")]
        [Display(Name="Tiêu đề")]
        [Column(TypeName = "nvarchar")]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Display(Name="Ngày tạo")]
        public DateTime Created { get; set; }
        [Column(TypeName = "ntext")]
        [Display(Name="Nội dung")]
        public string Content { get; set;}

        public string UserId {set; get;}
        [ForeignKey("UserId")]
        public AppUser User { get; set;}
    }
}