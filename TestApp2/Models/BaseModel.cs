using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace TestApp2.Models
{
    public class BaseModel
    {
        [Key]
        public Guid GUID { get; set; }

        [DefaultValue(typeof(DateTime), "")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "s")]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
    }
}
