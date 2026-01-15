using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class RetentionRecordDetailDto
    {
        public RetentionRecordDetailDto(RetentionRecord record)
        {
            Id = record.Id;
            Active = record.Active;
            StartYear = record.StartYear;
            EndYear = record.EndYear;
            ConsignmentNumber = record.ConsignmentNumber;
            BoxNumber = record.BoxNumber;
            ShelfNumber = record.ShelfNumber;
            RetentionSchedule = record.RetentionSchedule;
        }

        public Guid Id { get; }
        
        [Display(Name = "Retained")]
        public bool Active { get; }

        [Display(Name = "Start year")]
        public int StartYear { get; }

        [Display(Name = "End year")]
        public int EndYear { get; }

        [Display(Name = "Consignment Number")]
        public string ConsignmentNumber { get; }

        [Display(Name = "Box Number")]
        public string BoxNumber { get; }

        [Display(Name = "Shelf Number")]
        public string ShelfNumber { get; }

        [Display(Name = "Retention Schedule Number")]
        public string RetentionSchedule { get; }

        public string Summary => string.Concat(StartYear, "–", EndYear, ": ", BoxNumber.Replace('-', '‑'));
    }
}