using NUnit.Framework;

namespace ServicePlace.Common.NUnitTest
{
    [TestFixture]
    public class PageHelperTest
    {
        private PageHelper _pageHelper;

        [SetUp]
        public void Init()
        {
            _pageHelper = new PageHelper();
        }
        [Test]
        public void GetPagesCount_FullPagesCount_CountOfPages()
        {
            var expected = 4;
            var count = 20;
            var perPage = 5;
            var actual = _pageHelper.GetPagesCount(count, perPage);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPagesCount_PartOfPagesCount_CountOfPages()
        {
            var expected = 5;
            var count = 21;
            var perPage = 5;
            var actual = _pageHelper.GetPagesCount(count, perPage);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPageRange_PageRangeOnFirstPage_RangeOfPages()
        {
            var page = 1;
            var pagesCount = 20;
            var expected = new[] {1, 4};
            var actual = _pageHelper.GetPageRange(page, pagesCount);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPageRange_PageRangeOnMiddlePage_RangeOfPages()
        {
            var page = 10;
            var pagesCount = 20;
            var expected = new[] {9, 13};
            var actual = _pageHelper.GetPageRange(page, pagesCount);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPageRange_pageRangeOnLastPage_RangeOfPages()
        {
            var page = 20;
            var pagesCount = 20;
            var expected = new[] {19, 20};
            var actual = _pageHelper.GetPageRange(page, pagesCount);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
