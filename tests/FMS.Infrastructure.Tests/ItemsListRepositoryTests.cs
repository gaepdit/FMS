using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using TestSupport.EfHelpers;
using Xunit;

namespace FMS.Infrastructure.Tests
{
    public class ItemsListRepositoryTests
    {
        private static readonly List<BudgetCode> BudgetCodeList = new List<BudgetCode>
        {
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC001"},
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC002"},
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC003", Active = false},
        };

        private static ItemsListRepository SimpleItemsListRepository()
        {
            var context = new FmsDbContext(SqliteInMemory.CreateOptions<FmsDbContext>());
            context.Database.EnsureCreated();
            context.BudgetCodes.AddRange(BudgetCodeList);
            context.SaveChanges();

            return new ItemsListRepository(context);
        }

        [Fact]
        public async Task GetItemList_ReturnsAllActive()
        {
            using var repository = SimpleItemsListRepository();
            var result = await repository.GetBudgetCodesItemListAsync();
            var expected = BudgetCodeList
                .Where(e => e.Active)
                .Select(e => new ListItem() {Id = e.Id, Name = e.Name});
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetItemList_WithInactive_ReturnsAll()
        {
            using var repository = SimpleItemsListRepository();
            var result = await repository.GetBudgetCodesItemListAsync(true);
            var expected = BudgetCodeList
                .Select(e => new ListItem() {Id = e.Id, Name = e.Name});
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetItemName_ReturnsName()
        {
            using var repository = SimpleItemsListRepository();
            var result = await repository.GetBudgetCodeNameAsync(BudgetCodeList[0].Id);
            result.Should().Be(BudgetCodeList[0].Name);
        }

        [Fact]
        public async Task GetItemName_InvalidId_ReturnsNull()
        {
            using var repository = SimpleItemsListRepository();
            var result = await repository.GetBudgetCodeNameAsync(Guid.Empty);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetItemName_EmptyList_ReturnsNull()
        {
            using var repository = SimpleItemsListRepository();
            var result = await repository.GetComplianceOfficerNameAsync(Guid.Empty);
            result.Should().BeNull();
        }
    }
}