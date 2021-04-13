using Autofac.Extras.Moq;
using LibraryManager.Controllers;
using LibraryManager.Models;
using LibraryManager.Repositories;
using LibraryManager.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace LibraryManager.Tests
{
    public class CategoryControllerTests
    {
        [Fact]
        public void Details_ShouldReturnView()
        {
            using (var mock = AutoMock.GetStrict())
            {
                mock.Mock<ICategoryService>()
                    .Setup(x => x.GetCategoryByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(new Category
                    {
                        CategoryName ="TestCategory",
                        Id = 0
                    });


                var ctrl = mock.Create<CategoriesController>();
                var actual = ctrl.Details(It.IsAny<int>()).Result;

                var viewResult = Assert.IsType<ViewResult>(actual);

                Assert.IsAssignableFrom<Category>(viewResult.ViewData.Model);
            }
        }

        [Fact]
        public void Details_ShouldReturnBadRequest()
        {
            using (var mock = AutoMock.GetStrict())
            {
                mock.Mock<ICategoryService>()
                    .Setup(x => x.GetCategoryByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((Category)null);


                var ctrl = mock.Create<CategoriesController>();
                var actual = ctrl.Details(It.IsAny<int>()).Result;

                var viewResult = Assert.IsType<BadRequestResult>(actual);
            }
        }

        [Theory]
        [InlineData(100)]
        public void Details_ShouldReturnNotFound(int id)
        {
            using (var mock = AutoMock.GetStrict())
            {
                mock.Mock<ICategoryService>()
                    .Setup(x => x.GetCategoryByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((Category)null);


                var ctrl = mock.Create<CategoriesController>();
                var actual = ctrl.Details(id).Result;

                var viewResult = Assert.IsType<NotFoundResult>(actual);
            }
        }
    }
}
