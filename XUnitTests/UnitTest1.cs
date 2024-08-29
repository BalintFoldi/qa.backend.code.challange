using Betsson.OnlineWallets.Data.Models;
using Betsson.OnlineWallets.Services;
using Betsson.OnlineWallets.Data.Repositories;
using Betsson.OnlineWallets.Exceptions;
using Betsson.OnlineWallets.Models;

namespace Betsson.OnlineWallets.Services
{
    public class OnlineWalletServiceTests
    {
        [Fact]
        public async Task GetBalanceAsync_ShouldReturnCorrectBalance_WhenTransactionsExist()
        {
            //Arrange
            var fakeRepository = new FakeOnlineWalletRepository
            {
                LastEntry = new OnlineWalletEntry
                {
                    BalanceBefore = 100m,
                    Amount = 50m
                }
            };

            var walletService = new OnlineWalletService(fakeRepository);

            //Act
            Balance result = await walletService.GetBalanceAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(150m, result.Amount); // 100 + 50 = 150
        }

        [Fact]
        public async Task GetBalanceAsync_ShouldReturnZeroBalance_WhenNoTransactionsExist()
        {
            // Arrange
            var fakeRepository = new FakeOnlineWalletRepository
            {
                LastEntry = null  // No transaction
            };

            var walletService = new OnlineWalletService(fakeRepository);

            // Act
            Balance result = await walletService.GetBalanceAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0m, result.Amount);  // No transactions, so balance should be 0
        }
    }
}