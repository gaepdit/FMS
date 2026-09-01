using FMS.Domain.Dto;

namespace FMS.Helpers
{
    public static class EventSortHelper
    {
        public static IList<EventSummaryDto> SortEvents(IList<EventSummaryDto> events, EventSort sortBy)
        {
            var sortedList = new List<EventSummaryDto>();

            // Get top-level parents
            var topLevelParents = events.Where(e => e.ParentId == Guid.Empty || e.ParentId == null).OrderEventQuery(sortBy);

            foreach (var parent in topLevelParents)
            {
                sortedList.Add(parent);
                AddChildrenRecursively(parent, events, sortedList, sortBy);
            }

            return sortedList;
        }

        private static void AddChildrenRecursively(EventSummaryDto parent, IList<EventSummaryDto> allEvents, List<EventSummaryDto> sortedList, EventSort sortBy)
        {
            var children = allEvents.Where(c => c.ParentId == parent.Id).OrderEventQuery(sortBy);
            foreach (var child in children)
            {
                sortedList.Add(child);
                AddChildrenRecursively(child, allEvents, sortedList, sortBy);
            }
        }

        public static IEnumerable<EventSummaryDto> OrderEventQuery(
            this IEnumerable<EventSummaryDto> events, EventSort sortBy) =>
            sortBy switch
            {
                EventSort.EventType => events.OrderByDescending(e => e.Active).ThenBy(e => e.EventType.Name)
                    .ThenByDescending(e => e.StartDate),
                EventSort.EventTypeDesc => events.OrderByDescending(e => e.Active).ThenByDescending(e => e.EventType.Name)
                    .ThenByDescending(e => e.StartDate),
                EventSort.ActionTaken => events.OrderByDescending(e => e.Active).ThenBy(e => e.ActionTaken.Name)
                    .ThenBy(e => e.StartDate),
                EventSort.ActionTakenDesc => events.OrderByDescending(e => e.Active).ThenByDescending(e => e.ActionTaken.Name)
                    .ThenByDescending(e => e.StartDate),
                EventSort.StartDateDesc => events.OrderByDescending(e => e.Active).ThenByDescending(e => e.StartDate)
                    .ThenByDescending(e => e.DueDate),
                EventSort.DueDate => events.OrderByDescending(e => e.Active).ThenBy(e => e.DueDate)
                    .ThenBy(e => e.StartDate),
                EventSort.DueDateDesc => events.OrderByDescending(e => e.Active).ThenByDescending(e => e.DueDate)
                    .ThenBy(e => e.StartDate),
                EventSort.CompletionDate => events.OrderByDescending(e => e.Active).ThenByDescending(e => e.CompletionDate)
                    .ThenBy(e => e.EventType.Name),
                EventSort.CompletionDateDesc => events.OrderByDescending(e => e.Active).ThenBy(e => e.CompletionDate)
                    .ThenBy(e => e.EventType.Name),
                EventSort.ComplianceOfficer => events.OrderByDescending(e => e.Active).ThenBy(e => e.ComplianceOfficer.Name)
                    .ThenBy(e => e.EventType.Name),
                EventSort.ComplianceOfficerDesc => events.OrderByDescending(e => e.Active).ThenByDescending(e => e.ComplianceOfficer.Name)
                    .ThenBy(e => e.EventType.Name),
                EventSort.EventAmount => events.OrderByDescending(e => e.Active).ThenBy(e => e.EventAmount)
                    .ThenBy(e => e.EventType.Name),
                EventSort.EventAmountDesc => events.OrderByDescending(e => e.Active).ThenByDescending(e => e.EventAmount)
                    .ThenByDescending(e => e.EventType.Name),
                EventSort.EventContractor => events.OrderByDescending(e => e.Active).ThenByDescending(e => e.EventContractor?.Name)
                    .ThenBy(e => e.EventType.Name),
                EventSort.EventContractorDesc => events.OrderByDescending(e => e.Active).ThenBy(e => e.EventContractor?.Name)
                    .ThenBy(e => e.EventType.Name),
                // EventSort.StartDate
                _ => events.OrderByDescending(e => e.Active).ThenBy(e => e.StartDate)
                    .ThenByDescending(e => e.DueDate)
            };


        public static IEnumerable<EventReportDto> OrderReportEventQuery(
            this IEnumerable<EventReportDto> events, 
            EventReportSort sortBy, 
            DateOnly? startDate = null, 
            DateOnly? endDate = null) =>
            sortBy switch
            {
                EventReportSort.EventPending => events
                    .Where(e => e.CompletionDate == null)
                    .OrderBy(e => e.OrganizationalUnit?.Name)
                    .ThenBy(e => e.ComplianceOfficer?.Name)
                    .ThenBy(e => e.StartDate)
                    .ThenBy(e => e.DueDate)
                    .ToList(),
                EventReportSort.EventCompleted => events
                    .Where(e => e.CompletionDate != null
                        && e.CompletionDate >= startDate.GetValueOrDefault()
                        && e.CompletionDate <= endDate.GetValueOrDefault())
                    .OrderBy(e => e.OrganizationalUnit?.Name)
                    .ThenBy(e => e.DoneBy?.Name)
                    .ThenBy(e => e.FacilityNumber)
                    .ThenBy(e => e.CompletionDate)
                    .ToList(),
                EventReportSort.EventCompliance => events
                    .Where(e => e.CompletionDate != null
                        && e.CompletionDate >= startDate.GetValueOrDefault()
                        && e.CompletionDate <= endDate.GetValueOrDefault())
                    .OrderBy(e => e.OrganizationalUnit?.Name)
                    .ThenBy(e => e.CompletionDate)
                    .ToList(),
                EventReportSort.EventCompletedOutstanding => events
                    .Where(e => e.CompletionDate == null
                        || (e.CompletionDate >= startDate.GetValueOrDefault()
                        && e.CompletionDate <= endDate.GetValueOrDefault()))
                    .OrderBy(e => e.OrganizationalUnit?.Name)
                    .ThenBy(e => e.DoneBy?.Name)
                    .ThenBy(e => e.FacilityNumber)
                    .ThenBy(e => e.CompletionDate)
                    .ToList(),
                EventReportSort.EventOutstanding => events
                    .Where(e => e.CompletionDate == null)
                    .OrderBy(e => e.OrganizationalUnit?.Name)
                    .ThenBy(e => e.DoneBy?.Name)
                    .ThenBy(e => e.FacilityNumber)
                    .ThenBy(e => e.DueDate)
                    .ToList(),
                _ => events
                    .OrderBy(e => e.StartDate)
                    .ThenByDescending(e => e.DueDate)
                    .ToList()
            };
    }
}
