using Betsson.OnlineWallets.Data.Models;

namespace Betsson.OnlineWallets.Services
{
    public class OnlineWalletServiceTests
    {
        [Fact]
        public void GetBalanceAsync_ShouldReturnCorrectBalance_WhenTransactionsExist()
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

            //Act

            //Assert
        }
    }
}