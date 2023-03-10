using FoodSearchTutorial.ViewModel;
using NSubstitute;

namespace FoodSearchTestUnit
//namespace FoodSearchTutorial.Tests
{
    public class ProductsSearchViewModelTests
    {
        [Fact]
        public void ClearSearchTermsHistory_EmptyHistory_HistoryCleared()
        {
            // Arrange
            var viewModel = new ProductsSearchViewModel();

            // Act
            viewModel.ClearSearchTermsHistory();

            // Assert
            Assert.Empty(viewModel.SearchTermsHistory);
            Assert.True(viewModel.IsSearchTermsHistoryEmpty);
        }

        [Fact]
        public void ClearSearchTermsHistory_NonEmptyHistory_HistoryCleared()
        {
            // Arrange
            var viewModel = new ProductsSearchViewModel();
            viewModel.SearchTermsHistory.Add("term1");
            viewModel.SearchTermsHistory.Add("term2");
            viewModel.IsSearchTermsHistoryEmpty = false;

            // Act
            viewModel.ClearSearchTermsHistory();

            // Assert
            Assert.Empty(viewModel.SearchTermsHistory);
            Assert.True(viewModel.IsSearchTermsHistoryEmpty);
        }
    }
}
