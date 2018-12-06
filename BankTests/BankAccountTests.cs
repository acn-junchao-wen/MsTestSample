using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;

namespace BankTests
{
    [TestClass]
    public class BankAccountTests
    {
        
        [TestMethod]
        // In this case it put description of the test in to the name of test method.
        // we can also simply name the test method the method name + i, and put description in comment.
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange: prepare test data
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act: run method
            account.Debit(debitAmount);

            // Assert: check result
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
            // in this case, we compare 2 double, so we set 0.001 as a delta.
            // in most cases, we simply use Assert.AreEqual(expected, actual), which throw an Exception if failed.
        }

        [TestMethod]
        // this case shows us a way to test exception case.
        // if we can't catch the Exception, test method will go to Assert.Fail, which will throw an exception and let the method fail result.
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // Assert
                // check the Exception's Message to see whether it's the one we expected.
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }
            // if haven't catch Exception, go fail
            Assert.Fail("The expected exception was not thrown.");
        }
    }
}
