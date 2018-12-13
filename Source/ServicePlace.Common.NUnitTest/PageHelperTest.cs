using NUnit.Framework;

namespace ServicePlace.Common.NUnitTest
{
    [TestFixture]
    public class PageHelperTest
    {
        [Test]
        public void GetPagesCount_CountOfPages()
        {
            var pageHelper = new PageHelper();
            var expected = 4;
            var count = 20;
            var perPage = 5;
            var actual = pageHelper.GetPagesCount(count, perPage);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPageRange_MinimalAndMaximalPage()
        {
            var pageHelper = new PageHelper();
            var page = 4;
            var pagesCount = 20;
            var expected = new[] { 3, 7 };
            var actual = pageHelper.GetPageRange(page, pagesCount);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
