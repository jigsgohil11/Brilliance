using Brilliance.Infrastructure;
using PetaPoco;
using System;
using System.ComponentModel.DataAnnotations;

namespace Brilliance.Models.Entity
{
    [TableName("mst_Product")]
    [PrimaryKey("ProductID")]
    [Sort("SeqNo", "ASC")]
    public class Product
    {
        [Key]
        [Required]
        public Guid ProductID { get; set; }
        [MaxLength(7)]
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }
        [MaxLength(167)]
        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Product Type is required.")]
        public Guid? ProductTypeID { get; set; }
        [Required(ErrorMessage = "Company is required.")]
        public Guid? CompanyID { get; set; }
        [Required(ErrorMessage = "Organization is required.")]
        public Guid? ClientID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsBlock { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        [ResultColumn]
        public string Company { get; set; }
        [ResultColumn]
        public string Organization { get; set; }

        [Ignore]
        public string EncryptedProductID { get { return ProductID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(ProductID)) : null; } }

        [Ignore]
        public bool IsEdit { get { return ProductID != Guid.Empty ? true : false; } }
    }
}