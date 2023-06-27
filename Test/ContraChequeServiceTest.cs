using Application.Dto;
using Application.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class ContraChequeServiceTest
    {
        private ContraChequeService _service;
        private Mock<IEmployeeRepository> _repository = new Mock<IEmployeeRepository>();

        public ContraChequeServiceTest()
        {
            _service = new ContraChequeService(_repository.Object);
        }

        [Test]
        [TestCase(true, true, true, 1000.02)]
        [TestCase(true, false, true, 3000.02)]
        [TestCase(true, true, false, 4000.02)]
        [TestCase(false, true, false, 5000.02)]
        [TestCase(false, false, true, 6000.02)]
        [TestCase(false, false, false, 8000.02)]
        public async Task ShouldShowContraChequeEmployeeById_Sucess(bool transporte, bool saude, bool dental, decimal salario)
        {
            var employee = new Employee
            {
                Id = 1,
                DescontoDental = dental,
                DescontoSaude = saude,
                DescontoVale = transporte,
                SalarioBruto = salario,
                Nome = "Teste"
            };

            _repository.Setup(x => x.GetEmployeeById(1)).ReturnsAsync(employee);

            var result = await _service.GetContraChequeByFuncionarioId(1);

            Assert.True(result.IsSucess);
        }

        [Test]
        [TestCase(false, false, false, 8000.02)]
        public async Task ShouldShowContraChequeEmployeeById_Fail(bool transporte, bool saude, bool dental, decimal salario)
        {
            var employee = new Employee
            {
                Id = 1,
                DescontoDental = dental,
                DescontoSaude = saude,
                DescontoVale = transporte,
                SalarioBruto = salario,
                Nome = "Teste"
            };

            _repository.Setup(x => x.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(employee);

            var result = await _service.GetContraChequeByFuncionarioId(It.IsAny<int>());

            Assert.False(result.IsSucess);
        }
    }
}
