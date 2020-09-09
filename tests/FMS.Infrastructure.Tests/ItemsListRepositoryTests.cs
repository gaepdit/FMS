using FluentAssertions;
using FMS.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;
using TestHelpers;
using Xunit;

namespace FMS.Infrastructure.Tests
{
    public class ItemsListRepositoryTests
    {
        [Fact]
        public async Task GetCabinetsItemList_ReturnsAllActive()
        {
            using var repository = new RepositoryHelper().GetItemsListRepository();
            var result = await repository.GetCabinetsItemListAsync();
            var expected = DataHelpers.Cabinets.Where(e => e.Active)
                .Select(e => new ListItem() { Id = e.Id, Name = e.Name });
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetCabinetsItemList_WithInactive_ReturnsAll()
        {
            using var repository = new RepositoryHelper().GetItemsListRepository();
            var result = await repository.GetCabinetsItemListAsync(true);
            var expected = DataHelpers.Cabinets
                .Select(e => new ListItem() { Id = e.Id, Name = e.Name });
            result.Should().BeEquivalentTo(expected);
        }
    }
}
