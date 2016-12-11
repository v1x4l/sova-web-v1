using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using DatabaseService;
using WebApi.Controllers;
using WebApi.JsonModels;
using DomainModel;
using Newtonsoft.Json;

namespace MockingExample
{
    public class ControllerTests
    {
        //UnitOfWork__StateUnderTest__ExpectedBehavior
        [Fact]
        public void GetSovaUsersShouldReturnOkObjectResult()
        {
            //arrange
            var dataList = new List<SovaUser> { new SovaUser { SovaUserId = 666, SovaUserCreationDate = DateTime.Now } };
            var dataServiceMock = new Mock<IDataService<SovaUser>>();
            dataServiceMock.Setup(su => su.GetList(0, 10)).Returns(dataList);

            var controller = new SovaUserController(dataServiceMock.Object);
            //act
            var getUserList = controller.Get(0, 10);

            //assert
            var rightObjectListType = Assert.IsType<OkObjectResult>(getUserList);
            dataServiceMock.Verify(su => su.GetList(0, 10), Times.Once);
        }

        [Fact]
        public void DeleteShouldRemoveSovaUserOnlyIfFound()
        {
            //arrange
            var dataServiceMock = new Mock<IDataService<SovaUser>>();
            dataServiceMock.Setup(su => su.Delete(666)).Returns(true);
            dataServiceMock.Setup(su => su.Delete(667)).Returns(false);
            var dataService = dataServiceMock.Object;
            //act
            var controller = new SovaUserController(dataService);
            var deleteSovaUser = controller.Delete(666);
            var deleteSovaUserNotFound = controller.Delete(667);
            //assert
            Assert.IsType<OkResult>(deleteSovaUser);
            Assert.IsType<NotFoundResult>(deleteSovaUserNotFound);
            dataServiceMock.Verify(su => su.Delete(666), Times.Once);
            dataServiceMock.Verify(su => su.Delete(667), Times.Once);
        }


        [Fact]
        public void PostShouldDoSomething()
        {
            var sovaUser = new SovaUser { SovaUserId = 0, SovaUserCreationDate = DateTime.Now };
            var sovaUserModel = new SovaUserModel { SovaUserId = 0, SovaUserCreationDate = sovaUser.SovaUserCreationDate };
            var dataServiceMock = new Mock<IDataService<SovaUser>>();
            dataServiceMock.Setup(su => su.Add(sovaUser));
            //arrange
            var controller = new SovaUserController(dataServiceMock.Object);
            var postSovaUser = controller.Post(sovaUserModel);
            //act
            Assert.IsType<OkResult>(postSovaUser);
            //assert
            dataServiceMock.Verify(su => su.Add(sovaUser), Times.Once);
        }



    }

}
