using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePlace.Common
{
    public class PageHelper
    {
        public int[] GetPageRange(int page, int pagesCount)
        {
            int minPage = page > 1 ? page - 1 : 1;
            int maxPage = page + 3 > pagesCount ? pagesCount : page + 3;
            return new int[]{ minPage, maxPage };
        }
    }
}
