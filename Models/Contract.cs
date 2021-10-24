using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models{
    public class Contract{
        [Display(Name="Mã hợp đồng")]
        public string ContractId { get; set; }


        [Display(Name="Mã nhân viên")]
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public AppUser employee { get; set; }

        [Display(Name="Loại hoạt đồng")]
        public string ContractType { get; set; }
        

        [DataType(DataType.Date)]
        [Display(Name="Ngày bắt đầu")]
        public DateTime ContractFrom { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Ngày kết thúc")]
        public DateTime? ContractTo { get; set; }

        

    }
}