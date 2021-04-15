using Autofac.Extras.Moq;
using LibraryManager.Models;
using LibraryManager.Repositories;
using LibraryManager.Services;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace LibraryManager.Tests
{
    public class CategoryServiceTests
    {
        [Fact]
        public void DeleteItemAsync_ShouldReturnTrue()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ILibraryDbRepository<LibraryItem>>()
                    .Setup(x => x.AnyAsync(It.IsAny<Expression<Func<LibraryItem, bool>>>()))
                    .ReturnsAsync((Expression<Func<LibraryItem, bool>> predicate) => { return false; });

                var cls = mock.Create<CategoryService>();
                var actual = cls.DeleteCategoryAsync(It.IsAny<Category>());

                mock.Mock<ILibraryDbRepository<Category>>()
                    .Verify(x => x.DeleteAsync(It.IsAny<Category>()), Times.Once());

                Assert.True(actual.Result);
            }
        }

        [Fact]
        public void DeleteItemAsync_ShouldReturnFalse()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ILibraryDbRepository<LibraryItem>>()
                    .Setup(x => x.AnyAsync(It.IsAny<Expression<Func<LibraryItem, bool>>>()))
                    .ReturnsAsync((Expression<Func<LibraryItem, bool>> predicate) => { return true; });

                var cls = mock.Create<CategoryService>();
                var actual = cls.DeleteCategoryAsync(It.IsAny<Category>());

                mock.Mock<ILibraryDbRepository<Category>>()
                    .Verify(x => x.DeleteAsync(It.IsAny<Category>()), Times.Never());

                Assert.False(actual.Result);
            }
        }

        [Fact]
        public void GetCategoryByIdAsync_ShouldReturnCategory()
        {
            using (var mock = AutoMock.GetStrict())
            {
                mock.Mock<ILibraryDbRepository<Category>>()
                    .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(new Category
                    {
                        CategoryName = "MyCategory",
                        Id = 1337
                    });

                var expected = new Category
                {
                    CategoryName = "MyCategory",
                    Id = 1337
                };

                var cls = mock.Create<CategoryService>();
                var actual = cls.GetCategoryByIdAsync(It.IsAny<int>());

                mock.Mock<ILibraryDbRepository<Category>>()
                    .Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once());

                Assert.Equal(expected.Id, actual.Result.Id);
            }
        }
        [Fact]
        public void GetCategoryByIdAsync_ShouldReturnNull()
        {
            using (var mock = AutoMock.GetStrict())
            {
                mock.Mock<ILibraryDbRepository<Category>>()
                    .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((Category)null);


                var cls = mock.Create<CategoryService>();
                var actual = cls.GetCategoryByIdAsync(It.IsAny<int>());

                mock.Mock<ILibraryDbRepository<Category>>()
                    .Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once());

                Assert.Null(actual.Result);
            }
        }
    }
}
