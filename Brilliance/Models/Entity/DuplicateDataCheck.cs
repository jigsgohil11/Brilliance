using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brilliance.Models.Entity
{
    public class DuplicateDataCheck
    {
        [MaxLength(1000)]
        public string FieldName { get; set; }
        public string PrimariKey { get; set; }
        public string ColumnName { get; set; }
    }
}