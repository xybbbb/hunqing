using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WeddingProject.Models
{
    public static class PagerHelper
    {
        public static MvcHtmlString Pager(int totalItems, int currentPage, int pageSize, Func<int, string> pageUrl, int maxPagesToShow = 5)
        {
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            StringBuilder sb = new StringBuilder();
            if (currentPage > 1)
            {
                sb.Append(String.Format("<li class='page-item'><a class='page-link' href='{0}'>上一页</a></li>", pageUrl(currentPage - 1)));
            }

            int startPage = Math.Max(currentPage - (maxPagesToShow / 2), 1);
            int endPage = Math.Min(startPage + maxPagesToShow - 1, totalPages);

            for (int i = startPage; i <= endPage; i++)
            {
                if (i == currentPage)
                {
                    sb.Append(String.Format("<li class='page-item  active' aria-current='page'><span>{0}</span></li>", i));
                }
                else
                {
                    sb.Append(String.Format(" <li class='page-item'><a class='page-link' href='{0}'>{1}</a></li>", pageUrl(i), i));
                }
            }


            if (currentPage < totalPages)
            {
                sb.Append(String.Format("<li class='page-item'><a class='page-link' href='{0}'>下一页</a></li>", pageUrl(currentPage + 1)));
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}