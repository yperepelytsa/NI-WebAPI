using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CMSREST.Models;
using CMSREST.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CMSTests
{
    public class PagesControllerTests
    {
        





        [Fact]
        public async Task Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange
            var mockRepo = new Mock<IPageRepository>();
            var controller = new PagesController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result =await  Task.FromResult(controller.Create(null));

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtRoute()
        {
            // Arrange
            var mockRepo = new Mock<IPageRepository>();
            var controller = new PagesController(mockRepo.Object);

            // Act
            var result = await Task.FromResult(controller.Create(new Page()));

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_GivenInvalidID()
        {
            // Arrange
            var mockRepo = new Mock<IPageRepository>();
            var controller = new PagesController(mockRepo.Object);

            var result = await Task.FromResult(controller.Delete(10));

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange
            var mockRepo = new Mock<IPageRepository>();
            var controller = new PagesController(mockRepo.Object);

            var result = await Task.FromResult(controller.Update(null,2));

            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task Update_ReturnsNotFound_GivenInvalidID()
        {
            // Arrange
            var mockRepo = new Mock<IPageRepository>();
            var controller = new PagesController(mockRepo.Object);

            var result = await Task.FromResult(controller.Update(new Page { PageId=2}, 3));

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Update_ReturnsNoContentResult()
        {
            // Arrange
            var mockRepo = new Mock<IPageRepository>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetTestPages());
           var controller = new PagesController(mockRepo.Object);

            var result = await Task.FromResult(controller.Update(new Page { PageId = 2 }, 2));

            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task List_ReturnsObjectResult()
        {
            // Arrange
            var mockRepo = new Mock<IPageRepository>();  
            var controller = new PagesController(mockRepo.Object);

            var result = await Task.FromResult(controller.List(null,null,null));

            Assert.IsType<ObjectResult>(result);
        }
        [Fact]
        public async Task GetByID_ReturnsNotFound_GivenInvalidModel()
        {
            // Arrange
            var mockRepo = new Mock<IPageRepository>();
            var controller = new PagesController(mockRepo.Object);

            var result = await Task.FromResult(controller.GetById(3));

            Assert.IsType<NotFoundResult>(result);
        }

        List<Page> pages;
        private IEnumerable<Page> GetTestPages()
        {
            pages = new List<Page>();
            pages.Add(new Page()
            {
                Content = "testcont1",
                Description = "testdescr1",
                PageId = 1,
                Title = "test1",
                UrlName = "testurl1"
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
