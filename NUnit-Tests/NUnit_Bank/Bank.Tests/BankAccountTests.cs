using NUnit.Framework;
using System;

namespace Bank.Tests
{
    [TestFixture]
    public class BankAccountTests
    {
        private BankAccount account;

        [SetUp]
        public void TestInit()
        {
            this.account = new BankAccount(2000M);
        }

        [Test]
        public void AcountInitialize_With_PositiveValue()
        {
            Assert.AreEqual(2000M, account.Amount);
        }

        [Test]
        public void BankAcountHaveCorrectAmount_When_Deposit()
        {
            account.Deposit(200);
            Assert.AreEqual(2200M, account.Amount);
        }

        [Test]
        public void ExceptionIsThrown_When_DepositNegativeAmount()
        {
            
            Assert.Throws<ArgumentException>(() => account.Deposit(-1));
        }

        [Test]
        public void BankAcountHaveCorrectAmount_When_WithDraw_Less_1000()
        {
            account.Withdraw(100);
            Assert.AreEqual(1895M, account.Amount);
        }

        [Test]
        public void BankAcountHaveCorrectAmount_When_WithDraw_Greater_1000()
        {
            account.Withdraw(1500);
            Assert.AreEqual(470M, account.Amount);
        }

        [Test]
        public void ExceptionIsThrow_When_TryInitialNegativeValue()
        {            
            Assert.Throws<ArgumentException>(() => this.account = new BankAccount(-300M));
        }
    }
}
