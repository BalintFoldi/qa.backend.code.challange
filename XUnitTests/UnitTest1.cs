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

        [Fact]
        public async Task DepositFundsAsync_ShouldIncreaseBalance_ByDepositAmount()
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
            var deposit = new Deposit { Amount = 30m };

            //Act
            Balance newBalance = await walletService.DepositFundsAsync(deposit);

            //Assert
            Assert.Equal(180m, newBalance.Amount); // 150 + 30 = 180
            Assert.Single(fakeRepository.Entries); // Ensure a new entry was added
            Assert.Equal(30m, fakeRepository.Entries[0].Amount); // The deposit amount
        }

        [Fact]
        public async Task WithdrawFundsAsync_ShouldDecreaseBalance_ByWithdrawalAmount()
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
            var withdrawal = new Withdrawal { Amount = 20m };

            //Act
            Balance newBalance = await walletService.WithdrawFundsAsync(withdrawal);

            //Assert
            Assert.Equal(130m, newBalance.Amount); // 150 - 20 = 130
            Assert.Single(fakeRepository.Entries); //Ensure the new entry was added
            Assert.Equal(-20m, fakeRepository.Entries[0].Amount); // The withdrawal amount should be negative
        }

        [Fact]
        public async Task WithdrawFundsAsync_ShouldThrowException_WhenInsufficientBalance()
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
            var withdrawal = new Withdrawal { Amount = 200m }; // More than available balance

            //Act & Assert
            await Assert.ThrowsAsync<InsufficientBalanceException>(() => walletService.WithdrawFundsAsync(withdrawal));
        }
    }
}