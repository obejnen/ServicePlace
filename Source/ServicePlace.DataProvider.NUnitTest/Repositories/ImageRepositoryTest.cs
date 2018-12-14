using System;
using System.Linq;
using NUnit.Framework;
using ServicePlace.Common;
using ServicePlace.Model.DataModels;
using ServicePlace.DataInitializer;
using System.Collections.Generic;

namespace ServicePlace.DataProvider.NUnitTest.Repositories
{
    [TestFixture]
    public class ImageRepositoryTest
    {
        private Initializer _initializer;

        [SetUp]
        public void Init()
        {
            _initializer = new Initializer(10);
            _initializer.InitializeDb();
        }

        [TearDown]
        public void Dispose()
        {
            _initializer.ClearDb();
        }

        [Test]
        public void Create_CreatedImage_ThrowsNoException()
        {
            var image = new Image
            {
                Url = "imageUrl"
            };

            _initializer.ImageRepository.Create(image);
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Nothing);
        }

        [Test]
        public void Create_ImageNotValid_ThrowsException()
        {
            var image = new Image();

            _initializer.ImageRepository.Create(image);
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Exception);
        }

        [Test]
        public void Update_UpdatedImage_Success()
        {
            var expected = _initializer.ImageRepository.GetAll().FirstOrDefault();
            expected.Url = "UpdatedUrl";
            _initializer.ImageRepository.Update(expected);
            _initializer.CommitProvider.CommitChanges();
            var actual = _initializer.ImageRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_ImageNotValid_ThrowsException()
        {
            var image = _initializer.ImageRepository.GetAll().FirstOrDefault();
            image.Url = null;
            _initializer.ImageRepository.Update(image);
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Exception);
        }

        [Test]
        public void Delete_DeletedImage_Success()
        {
            var imageToDelete = _initializer.ImageRepository.GetAll().FirstOrDefault();
            _initializer.ImageRepository.Delete(imageToDelete);
            _initializer.CommitProvider.CommitChanges();
            foreach (var actual in _initializer.ImageRepository.GetAll())
                Assert.AreNotEqual(imageToDelete, actual);
        }

        [Test]
        public void Delete_ImageNotFound_ThrowsException()
        {
            var image = new Image();
            Assert.That(() => _initializer.ImageRepository.Delete(image), Throws.Exception);
        }

        [Test]
        public void GetBy_FoundImage_FirstImageById()
        {
            var expected = _initializer.ImageRepository.GetAll().OrderBy(x => x.Id).FirstOrDefault();
            var actual = _initializer.ImageRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_FoundImage_LastImageById()
        {
            var expected = _initializer.ImageRepository.GetAll().OrderBy(x => x.Id).LastOrDefault();
            var actual = _initializer.ImageRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAll_AllImages_ListOfAllImages()
        {
            _initializer = new Initializer(3);
            _initializer.InitializeDb();
            var expected = new List<Image>
            {
                _initializer.ImageRepository.GetBy(x => x.Url == "Url1").FirstOrDefault(),
                _initializer.ImageRepository.GetBy(x => x.Url == "Url2").FirstOrDefault(),
                _initializer.ImageRepository.GetBy(x => x.Url == "Url3").FirstOrDefault()
            };

            var actual = _initializer.ImageRepository.GetAll().ToList();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
