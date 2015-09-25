﻿using Cirrious.MvvmCross.Test.Core;
using MoneyManager.Core.DataAccess;
using MoneyManager.Core.Manager;
using MoneyManager.Core.Repositories;
using MoneyManager.Core.ViewModels;
using MoneyManager.Foundation;
using MoneyManager.Foundation.Model;
using MoneyManager.Foundation.OperationContracts;
using Moq;
using Xunit;

namespace MoneyManager.Core.Tests.ViewModels
{
    public class ModifyTransactionViewModelTest : MvxIoCSupportingTest
    {
        [Theory]
        [InlineData(TransactionType.Spending, "Edit Spending")]
        [InlineData(TransactionType.Income, "Edit Income")]
        [InlineData(TransactionType.Transfer, "Edit Transfer")]
        public void Title_EditTransactionType_CorrectTitle(TransactionType type, string result)
        {
            var dbHelper = new Mock<IDbHelper>().Object;
            var transactionRepository = new TransactionRepository(new TransactionDataAccess(dbHelper),
                new RecurringTransactionDataAccess(dbHelper))
            {
                Selected = new FinancialTransaction {Type = (int) type}
            };

            var transactionManager = new TransactionManager(transactionRepository,
                new Mock<IRepository<Account>>().Object,
                new Mock<IDialogService>().Object);

            var defaultManager = new DefaultManager(new Mock<IRepository<Account>>().Object,
                new SettingDataAccess(new Mock<IRoamingSettings>().Object));

            var viewModel = new ModifyTransactionViewModel(transactionRepository,
                new AccountRepository(new AccountDataAccess(dbHelper)),
                new Mock<IDialogService>().Object,
                transactionManager,
                defaultManager)
            {
                IsEdit = true,
                IsTransfer = true
            };

            viewModel.Title.ShouldBe(result);
        }

        [Theory]
        [InlineData(TransactionType.Spending, "Add Spending")]
        [InlineData(TransactionType.Income, "Add Income")]
        [InlineData(TransactionType.Transfer, "Add Transfer")]
        public void Title_AddTransactionType_CorrectTitle(TransactionType type, string result)
        {
            var dbHelper = new Mock<IDbHelper>().Object;

            var transactionRepository = new TransactionRepository(new TransactionDataAccess(dbHelper),
                new RecurringTransactionDataAccess(dbHelper))
            {
                Selected = new FinancialTransaction {Type = (int) type}
            };

            var transactionManager = new TransactionManager(transactionRepository,
                new Mock<IRepository<Account>>().Object,
                new Mock<IDialogService>().Object);

            var defaultManager = new DefaultManager(new Mock<IRepository<Account>>().Object,
                new SettingDataAccess(new Mock<IRoamingSettings>().Object));

            var viewModel = new ModifyTransactionViewModel(transactionRepository,
                new AccountRepository(new AccountDataAccess(dbHelper)),
                new Mock<IDialogService>().Object,
                transactionManager,
                defaultManager) {IsEdit = false};

            viewModel.Title.ShouldBe(result);
        }
    }
}