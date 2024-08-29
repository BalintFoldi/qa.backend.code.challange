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
            Assert.Equal(150m, result.Amount);
        }
    }
}