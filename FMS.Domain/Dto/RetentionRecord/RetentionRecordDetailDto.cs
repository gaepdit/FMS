using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class RetentionRecordDetailDto
    {
        public RetentionRecordDetailDto(RetentionRecord record)
        {
            StartYear = record.StartYear;
            EndYear = record.EndYear;
            ConsignmentNumber = record.ConsignmentNumber;
            BoxNumber = record.BoxNumber;
            ShelfNumber = record.ShelfNumber;
            RetentionSchedule = record.RetentionSchedule;
        }

        [Display(Name = "Start year")]
        public int StartYear { get; set; }

        [Display(Name = "End year")]
        public int EndYear { get; set; }

        [Display(Name = "Consignment Number")]
        public string ConsignmentNumber { get; set; }

        [Display(Name = "Box Number")]
        public string BoxNumber { get; set; }

        [Display(Name = "Shelf Number")]
        public string ShelfNumber { get; set; }

        [Display(Name = "Retention Schedule Number")]
        public string RetentionSchedule { get; set; }
    }
}
