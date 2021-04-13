using Autofac.Extras.Moq;
using LibraryManager.Models;
using LibraryManager.Repositories;
using LibraryManager.Services;
using Moq;
using System;
using Xunit;

namespace LibraryManager.Tests
{
    public class LibraryItemTests
    {
        [Fact]
        public void DeleteItemAsync_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ILibraryDbRepository<LibraryItem>>()
                    .Setup(x => x.DeleteAsync(It.IsAny<LibraryItem>()));

                var cls = mock.Create<LibraryItemService>();

                cls.DeleteItemAsync(It.IsAny<int>()).Wait();

                mock.Mock<ILibraryDbRepository<LibraryItem>>()
                    .Verify(x => x.DeleteAsync(It.IsAny<LibraryItem>()), Times.Exactly(1));
            }
        }
    }
}
