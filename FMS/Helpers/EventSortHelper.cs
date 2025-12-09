using FMS.Domain.Dto;
using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
                EventSort.EventType => events.OrderBy(e => e.EventType.Name)
                    .ThenBy(e => e.StartDate),
                EventSort.EventTypeDesc => events.OrderByDescending(e => e.EventType.Name)
                    .ThenByDescending(e => e.StartDate),
                EventSort.ActionTaken => events.OrderBy(e => e.ActionTaken.Name)
                    .ThenBy(e => e.StartDate),
                EventSort.ActionTakenDesc => events.OrderByDescending(e => e.ActionTaken.Name)
                    .ThenByDescending(e => e.StartDate),
                EventSort.StartDateDesc => events.OrderByDescending(e => e.StartDate)
                    .ThenByDescending(e => e.EventType.Name),
                EventSort.DueDate => events.OrderBy(e => e.DueDate)
                    .ThenBy(e => e.EventType.Name),
                EventSort.DueDateDesc => events.OrderByDescending(e => e.DueDate)
                    .ThenByDescending(e => e.EventType.Name),
                EventSort.CompletionDate => events.OrderBy(e => e.CompletionDate)
                    .ThenBy(e => e.EventType.Name),
                EventSort.CompletionDateDesc => events.OrderByDescending(e => e.CompletionDate)
                    .ThenByDescending(e => e.EventType.Name),
                EventSort.ComplianceOfficer => events.OrderBy(e => e.ComplianceOfficer.Name)
                    .ThenBy(e => e.EventType.Name),
                EventSort.ComplianceOfficerDesc => events.OrderByDescending(e => e.ComplianceOfficer.Name)
                    .ThenByDescending(e => e.EventType.Name),
                EventSort.EventAmount => events.OrderBy(e => e.EventAmount)
                    .ThenBy(e => e.EventType.Name),
                EventSort.EventAmountDesc => events.OrderByDescending(e => e.EventAmount)
                    .ThenByDescending(e => e.EventType.Name),
                EventSort.EventContractor => events.OrderBy(e => e.EventContractor.Name)
                    .ThenBy(e => e.EventType.Name),
                EventSort.EventContractorDesc => events.OrderByDescending(e => e.EventContractor.Name)
                    .ThenByDescending(e => e.EventType.Name),
                // EventSort.StartDate
                _ => events.OrderBy(e => e.StartDate)
                    .ThenBy(e => e.EventType.Name)
            };
    }
}
