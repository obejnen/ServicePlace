namespace ServicePlace.Common
{
    public class PageHelper
    {
        public int[] GetPageRange(int page, int pagesCount)
        {
            int minPage = page > 1 ? page - 1 : 1;
            int maxPage = page + 3 > pagesCount ? pagesCount : page + 3;
            return new []{ minPage, maxPage };
        }

        public int GetPagesCount(int size, int perPage)
        {
            var count = size / perPage;
            return count * perPage == size ? count : count + 1;
        }
    }
}
