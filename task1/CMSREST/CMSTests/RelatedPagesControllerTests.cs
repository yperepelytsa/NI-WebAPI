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
    public class RelatedPagessControllerTests
    {






        [Fact]
        public async Task Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange
            var mockRepo = new Mock<IRelatedPagesRepository>();
            var controller = new RelatedPagesController(mockRepo.Object);
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
            var mockRepo = new Mock<IRelatedPagesRepository>();
            var controller = new RelatedPagesController(mockRepo.Object);

            // Act
            var result = await Task.FromResult(controller.Create(new RelatedPages()));

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_GivenInvalidID()
        {
            // Arrange
            var mockRepo = new Mock<IRelatedPagesRepository>();
            var controller = new RelatedPagesController(mockRepo.Object);

            var result = await Task.FromResult(controller.Delete(10));

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange
            var mockRepo = new Mock<IRelatedPagesRepository>();
            var controller = new RelatedPagesController(mockRepo.Object);

            var result = await Task.FromResult(controller.Update(null, 2));

            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task Update_ReturnsNotFound_GivenInvalidID()
        {
            // Arrange
            var mockRepo = new Mock<IRelatedPagesRepository>();
            var controller = new RelatedPagesController(mockRepo.Object);

            var result = await Task.FromResult(controller.Update(new RelatedPages { RelatedPagesId = 2 }, 3));

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Update_ReturnsNoContentResult()
        {
            // Arrange
            var mockRepo = new Mock<IRelatedPagesRepository>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetTestRelatedPagess());
            var controller = new RelatedPagesController(mockRepo.Object);

            var result = await Task.FromResult(controller.Update(new RelatedPages { RelatedPagesId = 2 }, 2));

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetByID_ReturnsNotFound_GivenInvalidModel()
        {
            // ArrangeNa
            var mockRepo = new Mock<IRelatedPagesRepository>();
            var controller = new RelatedPagesController(mockRepo.Object);

            var result = await Task.FromResult(controller.GetById(3));

            Assert.IsType<NotFoundResult>(result);
        }

        List<RelatedPages> RelatedPagess;
        private IEnumerable<RelatedPages> GetTestRelatedPagess()
        {
            RelatedPagess = new List<RelatedPages>();
            RelatedPagess.Add(new RelatedPages()
            {
                RelatedPagesId = 1,
             Page1Id=1,
             Page2Id=2
            });
            RelatedPagess.Add(new RelatedPages()
            {
                RelatedPagesId = 2,
                Page1Id = 3,
                Page2Id = 4
            });
            return RelatedPagess;
        }
    }
}
