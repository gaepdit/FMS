using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class RetentionRecordEditDto
    {
        public RetentionRecordEditDto()
        {
            // Required for EditRetentionRecord page
        }

        public RetentionRecordEditDto(RetentionRecordDetailDto record)
        {
            Active = record.Active;
            StartYear = record.StartYear;
            EndYear = record.EndYear;
            ConsignmentNumber = record.ConsignmentNumber;
            BoxNumber = record.BoxNumber;
            ShelfNumber = record.ShelfNumber;
            RetentionSchedule = record.RetentionSchedule;
        }

        [Display(Name = "Retained")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Start Year")]
        public int StartYear { get; set; }

        [Required]
        [Display(Name = "End Year")]
        public int EndYear { get; set; }

        [Required]
        [Display(Name = "Box Number")]
        public string BoxNumber { get; set; }

        [Display(Name = "Consignment Number")]
        public string ConsignmentNumber { get; set; }

        [Display(Name = "Shelf Number")]
        public string ShelfNumber { get; set; }

        [Display(Name = "Retention Schedule Number")]
        public string RetentionSchedule { get; set; }

        public void TrimAll()
        {
            BoxNumber = BoxNumber?.Trim();
            ConsignmentNumber = ConsignmentNumber?.Trim();
            ShelfNumber = ShelfNumber?.Trim();
            RetentionSchedule = RetentionSchedule?.Trim();
        }
    }
}