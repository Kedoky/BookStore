using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Models;

namespace WebUI.HtmlHelpers
{
    public class BooksListViewModel
    {
        public IEnumerable<Book> Books { get; set; }

        public PagingInfo PadingInfo { get; set; }

        public string CurrentGenre { get; set; }
    }
}