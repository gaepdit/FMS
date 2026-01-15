using FMS.Domain.Repositories;
using FMS.TestData.SeedData;
using static FMS.Infrastructure.Tests.RepositoryHelper;

namespace FMS.Infrastructure.Tests
{
    public class ItemsListRepositoryTests
    {
        [Test]
        public async Task GetItemList_ReturnsAllActive()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetItemsListRepository();
            var result = await repository.GetBudgetCodesItemListAsync();
            var expected = SeedData.GetBudgetCodes()
                .Where(e => e.Active)
                .Select(e => new ListItem { Id = e.Id, Name = e.Name });
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetItemList_WithInactive_ReturnsAll()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetItemsListRepository();
            var result = await repository.GetBudgetCodesItemListAsync(true);
            var expected = SeedData.GetBudgetCodes()
                .Select(e => new ListItem { Id = e.Id, Name = e.Name });
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetItemName_ReturnsName()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetItemsListRepository();
            var bc = SeedData.GetBudgetCodes()[0];
            var result = await repository.GetBudgetCodeNameAsync(bc.Id);
            result.Should().Be(bc.Name);
        }

        [Test]
        public async Task GetItemName_NullId_ReturnsNull()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetItemsListRepository();
            var result = await repository.GetBudgetCodeNameAsync(null);
            result.Should().BeNull();
        }

        [Test]
        public async Task GetItemName_InvalidId_ReturnsNull()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetItemsListRepository();
            var result = await repository.GetBudgetCodeNameAsync(Guid.NewGuid());
            result.Should().BeNull();
        }

        [Test]
        public async Task GetItemName_EmptyList_ReturnsNull()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetItemsListRepository();
            var result = await repository.GetComplianceOfficerNameAsync(Guid.Empty);
            result.Should().BeNull();
        }
    }
}