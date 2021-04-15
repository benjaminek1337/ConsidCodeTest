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

                Assert.IsType<BadRequestResult>(actual);
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

                Assert.IsType<NotFoundResult>(actual);
            }
        }

        [Fact]
        public void DeleteConfirmed_ShouldReturnRedirectToActionIndex()
        {
            using (var mock = AutoMock.GetStrict())
            {
                mock.Mock<ICategoryService>()
                    .Setup(x => x.GetCategoryByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(new Category
                    {
                        CategoryName = "TestCategory",
                        Id = 42
                    });

                mock.Mock<ICategoryService>()
                    .Setup(x => x.DeleteCategoryAsync(It.IsAny<Category>()))
                    .ReturnsAsync(true);

                var ctrl = mock.Create<CategoriesController>();
                var actual = ctrl.DeleteConfirmed(It.IsAny<int>());

                var actionResult = Assert.IsType<RedirectToActionResult>(actual.Result);

                Assert.Equal("Index", actionResult.ActionName);

            }
        }

        [Fact]
        public void DeleteConfirmed_ShouldReturnRedirectToActionDelete()
        {
            using (var mock = AutoMock.GetStrict())
            {
                mock.Mock<ICategoryService>()
                    .Setup(x => x.GetCategoryByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((Category)null);

                mock.Mock<ICategoryService>()
                    .Setup(x => x.DeleteCategoryAsync(It.IsAny<Category>()))
                    .ReturnsAsync(false);

                var ctrl = mock.Create<CategoriesController>();
                var actual = ctrl.DeleteConfirmed(It.IsAny<int>());

                var actionResult = Assert.IsType<RedirectToActionResult>(actual.Result);

                Assert.Equal("Delete", actionResult.ActionName);
                Assert.True(actionResult.RouteValues.Values.Contains(true));
                Assert.True(actionResult.RouteValues.Values.Contains(It.IsAny<int>()));

            }
        }
    }
}
