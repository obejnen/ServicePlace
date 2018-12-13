using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServicePlace.Common.Test
{
    [TestClass]
    public class PageHelperTest
    {
        [TestMethod]
        public void GetPagesCount_CountOfPages_Return4()
        {
            var pageHelper = new PageHelper();
            var expected = 4;
            var count = 20;
            var perPage = 5;
            var actual = pageHelper.GetPagesCount(count, perPage);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPagesCount_CountOfPages_Return5()
        {
            var pageHelper = new PageHelper();
            var expected = 5;
            var count = 21;
            var perPage = 5;
            var actual = pageHelper.GetPagesCount(count, perPage);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPageRange_MinimalAndMaximalPage_ReturnArrayWith3And7()
        {
            var pageHelper = new PageHelper();
            var page = 4;
            var pagesCount = 20;
            var expected = new[] { 3, 7 };
            var actual = pageHelper.GetPageRange(page, pagesCount);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPageRange_MinimalAndMaximalPage_ReturnArrayWith2And6()
        {
            var pageHelper = new PageHelper();
            var page = 3;
            var pagesCount = 20;
            var expected = new[] { 2, 6 };
            var actual = pageHelper.GetPageRange(page, pagesCount);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
