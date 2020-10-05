using System;
using System.Collections;
using System.Collections.Generic;
using FMS.Domain.Utils;

namespace FMS.Domain.Dto.PaginatedList
{
    public class PaginatedList<T> : IPaginatedResult
    {
        public IList Items { get; }
        public int TotalCount { get; }
        public int PageNumber { get; }
        private int PageSize { get; }

        public PaginatedList(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            TotalCount = Prevent.Negative(totalCount, nameof(totalCount));
            PageNumber = Prevent.NegativeOrZero(pageNumber, nameof(pageNumber));
            PageSize = Prevent.NegativeOrZero(pageSize, nameof(pageSize));
            var itemsList = new List<T>();
            itemsList.AddRange(items);
            Items = itemsList;
        }

        public int TotalPages => (int) Math.Ceiling(TotalCount / (double) PageSize);
        public int CurrentCount => Items.Count;
        public int FirstItemIndex => Math.Min(PageSize * (PageNumber - 1) + 1, TotalCount);
        public int LastItemIndex => Math.Min(PageSize * PageNumber, TotalCount);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }

    public interface IPaginatedResult
    {
        IList Items { get; }
        int TotalCount { get; }
        int PageNumber { get; }
        int TotalPages { get; }
        int CurrentCount { get; }
        int FirstItemIndex { get; }
        int LastItemIndex { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}