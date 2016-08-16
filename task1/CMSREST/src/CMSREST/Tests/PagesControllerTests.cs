using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CMSREST.Models;
using CMSREST.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CMSREST.Tests
{
    public class HomeControllerTests
    {
       [Fact]
        public async Task IndexPost_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockRepo = new Mock<PageRepository>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetTestPages());
            var controller = new PagesController(mockRepo.Object);
            controller.ModelState.AddModelError("SessionName", "Required");

            // Act
            var result = await Task.FromResult(controller.List(null,null,null));

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }





      [Fact]
        public async Task IndexPost_AddsPage_WhenModelStateIsValid()
        {
            // Arrange
            var mockRepo = new Mock<IPageRepository>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetTestPages());
         //   mockRepo.Setup(repo=>)
            var controller = new PagesController(mockRepo.Object);

          await  Task.FromResult(controller.Create(new Page()
            {
                Content = "testcont3",
                Description = "testdescr3",
                PageId = 3,
                Title = "test3",
                UrlName = "testurl3"
            }));

            Assert.True(controller.pageRepository.GetAll().Where(m => m.PageId == 3).Count() == 1);
        }
        [Fact]
        public async Task IndexPost_DeletesPage_WhenModelStateIsValid()
        {
            // Arrange
            var mockRepo = new Mock<IPageRepository>();
            var controller = new PagesController(mockRepo.Object);

            await Task.FromResult(controller.Delete(1));

            Assert.True(controller.pageRepository.GetAll().Where(m => m.PageId == 3).Count() == 1);
        }
        List<Page> pages;
        private IEnumerable<Page> GetTestPages()
        {
            pages = new List<Page>();
            pages.Add(new Page()
            {
               Content="testcont1",
               Description="testdescr1",
               PageId=1,
               Title="test1",
               UrlName="testurl1"
            });
            pages.Add(new Page()
            {
                Content = "testcont2",
                Description = "testdescr2",
                PageId = 2,
                Title = "test2",
                UrlName = "testurl2"
            });
            return pages;
        }
    }
}
