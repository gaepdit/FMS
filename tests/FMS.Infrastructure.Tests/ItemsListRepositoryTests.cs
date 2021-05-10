using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Repositories;
using TestHelpers;
using Xunit;
using static TestHelpers.RepositoryHelper;

namespace FMS.Infrastructure.Tests
{
    public class ItemsListRepositoryTests
    {
        [Fact]
        public async Task GetItemList_ReturnsAllActive()
        {
            using var repository = CreateRepositoryHelper().GetItemsListRepository();
            var result = await repository.GetBudgetCodesItemListAsync();
            var expected = RepositoryData.BudgetCodes
                .Where(e => e.Active)
                .Select(e => new ListItem() {Id = e.Id, Name = e.Name});
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetItemList_WithInactive_ReturnsAll()
        {
            using var repository = CreateRepositoryHelper().GetItemsListRepository();
            var result = await repository.GetBudgetCodesItemListAsync(true);
            var expected = RepositoryData.BudgetCodes
                .Select(e => new ListItem() {Id = e.Id, Name = e.Name});
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetItemName_ReturnsName()
        {
            using var repository = CreateRepositoryHelper().GetItemsListRepository();
            var bc = RepositoryData.BudgetCodes[0];
            var result = await repository.GetBudgetCodeNameAsync(bc.Id);
            result.Should().Be(bc.Name);
        }

        [Fact]
        public async Task GetItemName_NullId_ReturnsNull()
        {
            using var repository = CreateRepositoryHelper().GetItemsListRepository();
            var result = await repository.GetBudgetCodeNameAsync(null);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetItemName_InvalidId_ReturnsNull()
        {
            using var repository = CreateRepositoryHelper().GetItemsListRepository();
            var result = await repository.GetBudgetCodeNameAsync(Guid.NewGuid());
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetItemName_EmptyList_ReturnsNull()
        {
            using var repository = CreateRepositoryHelper().GetItemsListRepository();
            var result = await repository.GetComplianceOfficerNameAsync(Guid.Empty);
            result.Should().BeNull();
        }
    }
}