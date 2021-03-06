﻿using System.Threading.Tasks;
using BussinessLogic.Services;
using DomainModels.Model;
using DomainModels.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;

namespace BussinessLogic.Tests.ServicesTests
{
    [TestClass]
    public class DebtServiceTests
    {
        private readonly Mock<IRepository<Debt>> _debtRepoMock;
        private readonly Mock<IRepository<Account>> _accRepoMock;
        private readonly IDebtService _service;

        public DebtServiceTests()
        {
            _debtRepoMock = new Mock<IRepository<Debt>>();
            _accRepoMock = new Mock<IRepository<Account>>();
            _service = new DebtService(_debtRepoMock.Object, _accRepoMock.Object);
        }

        [TestMethod]
        [TestCategory("DebtServiceTests")]
        public async Task DeleteAsync()
        {
            await _service.DeleteAsync(It.IsAny<int>());

            _debtRepoMock.Verify(m => m.DeleteAsync(It.IsAny<int>()),Times.Exactly(1));
        }
    }
}
