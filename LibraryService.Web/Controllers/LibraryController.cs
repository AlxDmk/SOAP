﻿using LibraryService.Web.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceReference1;
using System.Diagnostics;

namespace LibraryService.Web.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILogger<LibraryController> _logger;

        public LibraryController(ILogger<LibraryController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(SearchType searchType, string searchString)
        {
            LibraryWebServiceSoapClient client = new LibraryWebServiceSoapClient(LibraryWebServiceSoapClient.EndpointConfiguration.LibraryWebServiceSoap);
            try
            {
                if (!string.IsNullOrEmpty(searchString) && searchString.Length >= 3)
                {
                    switch (searchType)
                    {
                        case SearchType.Title:
                            return View(new BookCategoryViewModel
                            {
                                Books = client.GetBookByTitle(searchString)
                            });
                        case SearchType.Author:
                            return View(new BookCategoryViewModel { Books = client.GetBookByName(searchString) });

                        case SearchType.Category:
                            return View(new BookCategoryViewModel { Books = client.GetBookByCategory(searchString) });

                    }
                }                

            }
            catch (Exception e)
            {

                _logger.LogError(e, "Error");
            }

            return View(new BookCategoryViewModel { Books = new Book[] { } });

        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}