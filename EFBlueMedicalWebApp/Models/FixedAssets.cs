using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFBlueMedicalWebApp.Models
{
    public class FixedAssets
    {
        [Key]
        public int AssetID { get; set; }
        public string AssetName { get; set; }
        public int DepartmentID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUsed { get; set; }
        public decimal UsefulLife { get; set; }
        public decimal Price { get; set; }
        public decimal DepreciationRate { get; set; }
        public decimal DepreciatedAmount { get; set; }
        public Department Department { get; set; }
    }
}
