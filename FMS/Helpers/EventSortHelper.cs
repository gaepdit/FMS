using FMS.Domain.Dto;
using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMS.Helpers
{
    public static class EventSortHelper
    {
        public static IList<EventSummaryDto> SortEvents(IList<EventSummaryDto> events)
        {
            var sortedList = new List<EventSummaryDto>();

            // Get top-level parents
            var topLevelParents = events.Where(e => e.ParentId == Guid.Empty || e.ParentId == null).OrderBy(e => e.StartDate).ThenBy(e => e.CompletionDate);

            foreach (var parent in topLevelParents)
            {
                sortedList.Add(parent);
                AddChildrenRecursively(parent, events, sortedList);
            }

            return sortedList;
        }

        private static void AddChildrenRecursively(EventSummaryDto parent, IList<EventSummaryDto> allEvents, List<EventSummaryDto> sortedList)
        {
            var children = allEvents.Where(c => c.ParentId == parent.Id).OrderBy(c => c.StartDate).ThenBy(e => e.CompletionDate);
            foreach (var child in children)
            {
                sortedList.Add(child);
                AddChildrenRecursively(child, allEvents, sortedList);
            }
        }

        public static IOrderedQueryable<Event> OrderEventQuery(
            IQueryable<Event> included, EventSort sortBy) =>
            sortBy switch
            {
                EventSort.EventType => included.OrderBy(e => e.EventType)
                   .ThenBy(e => e.StartDate),
                EventSort.EventTypeDesc => included.OrderByDescending(e => e.EventType)
                    .ThenByDescending(e => e.StartDate),
                EventSort.ActionTaken => included.OrderBy(e => e.ActionTaken)
                    .ThenBy(e => e.StartDate),
                EventSort.ActionTakenDesc => included.OrderByDescending(e => e.ActionTaken)
                    .ThenByDescending(e => e.StartDate),
                EventSort.StartDateDesc => included.OrderByDescending(e => e.StartDate)
                    .ThenByDescending(e => e.EventType),
                EventSort.DueDate => included.OrderBy(e => e.DueDate)
                    .ThenBy(e => e.EventType),
                EventSort.DueDateDesc => included.OrderByDescending(e => e.DueDate)
                    .ThenByDescending(e => e.EventType),
                EventSort.CompletionDate => included.OrderBy(e => e.CompletionDate)
                    .ThenBy(e => e.EventType),
                EventSort.CompletionDateDesc => included.OrderByDescending(e => e.CompletionDate)
                    .ThenByDescending(e => e.EventType),
                EventSort.ComplianceOfficer => included.OrderBy(e => e.ComplianceOfficer)
                    .ThenBy(e => e.EventType),
                EventSort.ComplianceOfficerDesc => included.OrderByDescending(e => e.ComplianceOfficer)
                    .ThenByDescending(e => e.EventType),
                EventSort.EventAmount => included.OrderBy(e => e.EventAmount)
                    .ThenBy(e => e.EventType),
                EventSort.EventAmountDesc => included.OrderByDescending(e => e.EventAmount)
                    .ThenByDescending(e => e.EventType),
                EventSort.EventContractor => included.OrderBy(e => e.EventContractor)
                    .ThenBy(e => e.EventType),
                EventSort.EventContractorDesc => included.OrderByDescending(e => e.EventContractor)
                    .ThenByDescending(e => e.EventType),
                // EventSort.StartDate
                _ => included.OrderBy(e => e.StartDate)
                    .ThenBy(e => e.EventType)
            };
    }
}
