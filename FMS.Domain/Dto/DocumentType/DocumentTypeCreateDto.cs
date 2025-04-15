using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class DocumentTypeCreateDto
    {
        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "Document Type Name is required.")]
        public string Name { get; set; }
    }
}
