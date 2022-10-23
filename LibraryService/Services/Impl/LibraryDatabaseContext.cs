using LibraryService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryService.Services.Impl
{
    public class LibraryDatabaseContext : ILibararyDatabaseContextService
    {
        private IList<Book> _libraryDatabase;
        public IList<Book> Books => _libraryDatabase;

        public LibraryDatabaseContext()
        {
            Initialize();
        }

        private void Initialize()
        {
            
            _libraryDatabase = (IList<Book>)JsonConvert.DeserializeObject(System.Text.Encoding.UTF8.GetString(Properties.Resources.books), typeof(IList<Book>));
        }
    }
}