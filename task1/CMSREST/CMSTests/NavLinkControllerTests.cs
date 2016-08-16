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
    public class NavLinksControllerTests
    {






        [Fact]
        public async Task Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange
            var mockRepo = new Mock<INavLinkRepository>();
            var controller = new NavLinkController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await Task.FromResult(controller.Create(null));

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtRoute()
        {
            // Arrange
            var mockRepo = new Mock<INavLinkRepository>();
            var controller = new NavLinkController(mockRepo.Object);

            // Act
            var result = await Task.FromResult(controller.Create(new NavLink()));

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }


        [Fact]
        public async Task Delete_ReturnsNotFound_GivenInvalidID()
        {
            // Arrange
            var mockRepo = new Mock<INavLinkRepository>();
            var controller = new NavLinkController(mockRepo.Object);

            var result = await Task.FromResult(controller.Delete(10));

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange
            var mockRepo = new Mock<INavLinkRepository>();
            var controller = new NavLinkController(mockRepo.Object);

            var result = await Task.FromResult(controller.Update(null, 2));

            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task Update_ReturnsNotFound_GivenInvalidID()
        {
            // Arrange
            var mockRepo = new Mock<INavLinkRepository>();
            var controller = new NavLinkController(mockRepo.Object);

            var result = await Task.FromResult(controller.Update(new NavLink { NavLinkId = 2 }, 3));

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Update_ReturnsNoContentResult()
        {
            // Arrange
            var mockRepo = new Mock<INavLinkRepository>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetTestNavLinks());
            var controller = new NavLinkController(mockRepo.Object);

            var result = await Task.FromResult(controller.Update(new NavLink { NavLinkId = 2 }, 2));

            Assert.IsType<NoContentResult>(result);
        }
       
        [Fact]
        public async Task GetByID_ReturnsNotFound_GivenInvalidModel()
        {
            // ArrangeNa
            var mockRepo = new Mock<INavLinkRepository>();
            var controller = new NavLinkController(mockRepo.Object);

            var result = await Task.FromResult(controller.GetById(3));

            Assert.IsType<NotFoundResult>(result);
        }

        List<NavLink> NavLinks;
        private IEnumerable<NavLink> GetTestNavLinks()
        {
            NavLinks = new List<NavLink>();
            NavLinks.Add(new NavLink()
            {
                NavLinkId = 1,
                PageId =1,
                Position="t1",              
                Title = "test1",
            });
            NavLinks.Add(new NavLink()
            {
                NavLinkId = 2,
                PageId = 2,
                Position = "t2",               
                Title = "test2",
            });
            return NavLinks;
        }
    }
}
