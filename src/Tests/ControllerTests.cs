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

namespace Tests
{

    public class ControllerTests
    {
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
        public void PostShouldDoAddNewObjectAndReturnOk()
        {
            //arrange
            DateTime currentDateTime = DateTime.Now;
            var sovaUserPost = new SovaUserModel { SovaUserCreationDate = currentDateTime };

            var dataServiceMock = new Mock<IDataService<SovaUser>>();
            dataServiceMock.Setup(su => su.Add(It.IsAny<SovaUser>()));

            var controller = new SovaUserController(dataServiceMock.Object);
            controller.Url = Mock.Of<IUrlHelper>(x => x.IsLocalUrl(It.IsAny<string>()) == false);

            //act
            var postSovaUser = controller.Post(sovaUserPost);

            //assert
            var returnsRightObject = Assert.IsType<OkObjectResult>(postSovaUser);
            dataServiceMock.Verify(su => su.Add(It.IsAny<SovaUser>()));
        }

        [Fact]
        public void PutShouldUpdateObjectAndReturnOk()
        {
            //arrange
            DateTime currentDateTime = DateTime.Now;
            var sovaUserPut = new SovaUserModel { SovaUserCreationDate = currentDateTime };
            var dataServiceMock = new Mock<IDataService<SovaUser>>();
            dataServiceMock.Setup(su => su.Update(It.IsAny<SovaUser>())).Returns(true);
            var controller = new SovaUserController(dataServiceMock.Object);

            controller.Url = Mock.Of<IUrlHelper>(x => x.IsLocalUrl(It.IsAny<string>()) == false);

            //act
            var putSovaUser = controller.Put(666, sovaUserPut);

            //assert
            var returnsRightObject = Assert.IsType<OkResult>(putSovaUser);
            dataServiceMock.Verify(su => su.Update(It.IsAny<SovaUser>()), Times.Once);
        }

        /*
        [Fact]
        public void GetSearchResultsShouldReturnOkObjectResult()
        {
            
        }*/

    }
}
