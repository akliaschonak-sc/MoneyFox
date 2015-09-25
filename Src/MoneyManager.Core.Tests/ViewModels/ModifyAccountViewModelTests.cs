﻿using System.Globalization;
using MoneyManager.Core.DataAccess;
using MoneyManager.Core.ViewModels;
using MoneyManager.Foundation;
using MoneyManager.Foundation.Model;
using MoneyManager.Foundation.OperationContracts;
using MoneyManager.Localization;
using Moq;
using Xunit;

namespace MoneyManager.Core.Tests.ViewModels
{
    public class ModifyAccountViewModelTests
    {
        [Fact]
        public void Title_EditAccount_CorrectTitle()
        {
            Strings.Culture = new CultureInfo("en-US");
            var accountname = "Sparkonto";

            var accountRepositorySetup = new Mock<IRepository<Account>>();
            accountRepositorySetup.SetupGet(x => x.Selected).Returns(new Account {Id = 2, Name = accountname});

            var viewmodel = new ModifyAccountViewModel(accountRepositorySetup.Object, new BalanceViewModel(
                accountRepositorySetup.Object, new Mock<ITransactionRepository>().Object,
                new SettingDataAccess(new Mock<IRoamingSettings>().Object))) {IsEdit = true};

            viewmodel.Title.ShouldBe("Edit " + accountname);
        }

        [Fact]
        public void Title_AddAccount_CorrectTitle()
        {
            Strings.Culture = new CultureInfo("en-US");
            var accountname = "Sparkonto";

            var accountRepositorySetup = new Mock<IRepository<Account>>();
            accountRepositorySetup.SetupGet(x => x.Selected).Returns(new Account {Id = 2, Name = accountname});

            var viewmodel = new ModifyAccountViewModel(accountRepositorySetup.Object, new BalanceViewModel(
                accountRepositorySetup.Object, new Mock<ITransactionRepository>().Object,
                new SettingDataAccess(new Mock<IRoamingSettings>().Object)))
            {IsEdit = false};

            viewmodel.Title.ShouldBe(Strings.AddAccountTitle);
        }
    }
}