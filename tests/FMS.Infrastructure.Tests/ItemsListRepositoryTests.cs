using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Repositories;
using TestHelpers.SimpleRepository;
using Xunit;

namespace FMS.Infrastructure.Tests
{
    public class ItemsListRepositoryTests
    {
        [Fact]
        public async Task GetItemList_ReturnsAllActive()
        {
            using var repository = new SimpleRepositoryHelper().GetItemsListRepository();
            var result = await repository.GetBudgetCodesItemListAsync();
            var expected = SimpleRepositoryData.BudgetCodes
                .Where(e => e.Active)
                .Select(e => new ListItem() {Id = e.Id, Name = e.Name});
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetItemList_WithInactive_ReturnsAll()
        {
            using var repository = new SimpleRepositoryHelper().GetItemsListRepository();
            var result = await repository.GetBudgetCodesItemListAsync(true);
            var expected = SimpleRepositoryData.BudgetCodes
                .Select(e => new ListItem() {Id = e.Id, Name = e.Name});
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetItemName_ReturnsName()
        {
            using var repository = new SimpleRepositoryHelper().GetItemsListRepository();
            var bc = SimpleRepositoryData.BudgetCodes[0];
            var result = await repository.GetBudgetCodeNameAsync(bc.Id);
            result.Should().Be(bc.Name);
        }

        [Fact]
        public async Task GetItemName_NullId_ReturnsNull()
        {
            using var repository = new SimpleRepositoryHelper().GetItemsListRepository();
            var result = await repository.GetBudgetCodeNameAsync(null);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetItemName_InvalidId_ReturnsNull()
        {
            using var repository = new SimpleRepositoryHelper().GetItemsListRepository();
            var result = await repository.GetBudgetCodeNameAsync(Guid.NewGuid());
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetItemName_EmptyList_ReturnsNull()
        {
            using var repository = new SimpleRepositoryHelper().GetItemsListRepository();
            var result = await repository.GetComplianceOfficerNameAsync(Guid.Empty);
            result.Should().BeNull();
        }
    }
}