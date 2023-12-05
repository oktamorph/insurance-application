using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NuGet.Frameworks;
using Storage.API.Controllers;
using Storage.API.Models;
using Storage.API.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.API.Tests.Controllers
{
    public class StorageControllerTests
    {
        private readonly Mock<IStorageService> _mockService;
        private readonly StorageController _controller;
        public StorageControllerTests()
        {
            _mockService = new Mock<IStorageService>();
            _controller = new StorageController(_mockService.Object);
        }
        [Fact]
        public async void Insurance_Get_By_Id_And_Customer_Number()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var customerNumber = "123456781234";

            // Act            
            var insurance = await _controller.Get(guid, customerNumber);

            // Assert
            Assert.IsType<ActionResult<StorageItem>>(insurance);
        }
        [Fact]
        public async void Insurance_Create()
        {
            // Arrange
            StorageItem item = null;

            _mockService.Setup(x => x.Add(It.IsAny<StorageItem>()))
                .Callback<StorageItem>(x=> item = x);

            var storageItem = new StorageItem
            {
                InsuranceGuid = Guid.NewGuid(),
                CustomerNumber = "123456781234",
                FirstName = "Adam",
                LastName = "Olafsson"
            };

            // Act
            await _controller.Post(storageItem);
            _mockService.Verify(x => x.Add(It.IsAny<StorageItem>()) , Times.Once());

            // Assert
            Assert.Equal(item?.InsuranceGuid, storageItem.InsuranceGuid);
            Assert.Equal(item?.CustomerNumber, storageItem.CustomerNumber);
            Assert.Equal(item?.FirstName, storageItem.FirstName);
            Assert.Equal(item?.LastName, storageItem.LastName);
        }
        [Fact]
        public async void Insurance_Delete()
        {
            // Arrange
            var fakeGuid = Guid.NewGuid();
            var fakeCustomerNumber = "123456781234";

            // Act
            var notFound = await _controller.Delete(fakeGuid, fakeCustomerNumber);

            // Assert
            Assert.IsType<ActionResult<StorageItem>>(notFound);
        }
    }
}
