﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MoneyFox.BusinessDbAccess.QueryObjects;
using MoneyFox.DataLayer.Entities;
using MoneyFox.Foundation;
using Xunit;

namespace MoneyFox.BusinessDbAccess.Tests.QueryObjects
{
    [ExcludeFromCodeCoverage]
    public class PaymentQueryExtensionTests
    {
        [Fact]
        public void HasDateLargerEqualsThan()
        {
            // Arrange
            var paymentListQuery = new List<Payment>
                {
                    new Payment(DateTime.Now.AddDays(1), 12, PaymentType.Expense, new Account("d")),
                    new Payment(DateTime.Now, 13, PaymentType.Expense, new Account("d")),
                    new Payment(DateTime.Now.AddDays(-1), 14, PaymentType.Expense, new Account("d"))
                }
                .AsQueryable();

            // Act
            var resultList = paymentListQuery.HasDateLargerEqualsThan(DateTime.Now).ToList();

            // Assert
            Assert.Equal(2, resultList.Count);
            Assert.Equal(12, resultList[0].Amount);
            Assert.Equal(13, resultList[1].Amount);
        }

        [Fact]
        public void IsCleared()
        {
            // Arrange
            var paymentListQuery = new List<Payment>
                {
                    new Payment(DateTime.Now, 12, PaymentType.Expense, new Account("d")),
                    new Payment(DateTime.Now.AddDays(1), 13, PaymentType.Expense, new Account("d")),
                    new Payment(DateTime.Now.AddDays(1), 14, PaymentType.Expense, new Account("d"))
                }
                .AsQueryable();

            // Act
            var resultList = paymentListQuery.AreCleared().ToList();

            // Assert
            Assert.Single(resultList);
            Assert.Equal(12, resultList[0].Amount);
        }

        [Fact]
        public void IsNotCleared()
        {
            // Arrange
            var paymentListQuery = new List<Payment>
                {
                    new Payment(DateTime.Now, 12, PaymentType.Expense, new Account("d")),
                    new Payment(DateTime.Now.AddDays(1), 13, PaymentType.Expense, new Account("d")),
                    new Payment(DateTime.Now.AddDays(1), 14, PaymentType.Expense, new Account("d"))
                }
                .AsQueryable();

            // Act
            var resultList = paymentListQuery.AreNotCleared().ToList();

            // Assert
            Assert.Equal(2, resultList.Count);
            Assert.Equal(13, resultList[0].Amount);
            Assert.Equal(14, resultList[1].Amount);
        }

        [Fact]
        public void WithoutTransfers()
        {
            // Arrange
            var paymentListQuery = new List<Payment>
                {
                    new Payment(DateTime.Now, 12, PaymentType.Expense, new Account("d"), new Account("t")),
                    new Payment(DateTime.Now, 13, PaymentType.Income, new Account("d"), new Account("t")),
                    new Payment(DateTime.Now, 14, PaymentType.Transfer, new Account("d"), new Account("t")),
                    new Payment(DateTime.Now, 15, PaymentType.Income, new Account("d"), new Account("t")),
                    new Payment(DateTime.Now, 16, PaymentType.Transfer, new Account("d"), new Account("t"))
                }
                .AsQueryable();

            // Act
            var resultList = paymentListQuery.WithoutTransfers().ToList();

            // Assert
            Assert.Equal(3, resultList.Count);
            Assert.Equal(12, resultList[0].Amount);
            Assert.Equal(13, resultList[1].Amount);
            Assert.Equal(15, resultList[2].Amount);
        }
    }
}