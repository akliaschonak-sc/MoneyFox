﻿using System;
using System.Diagnostics.CodeAnalysis;
using MoneyFox.Foundation;
using MoneyFox.ServiceLayer.ViewModels;
using Should;
using Xunit;

namespace MoneyFox.ServiceLayer.Tests.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class RecurringPaymentViewModelTests
    {
        [Fact]
        public void Ctor_SetDefaults()
        {
            // Arrange

            // Act
            var vm = new RecurringPaymentViewModel();

            // Assert
            vm.Recurrence.ShouldEqual(PaymentRecurrence.Daily);
            vm.EndDate.Value.Date.ShouldEqual(DateTime.Today);
        }
    }
}
