using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KFCTop100.Models;
using KFCSharedData;
using KFCSharedData.Service;
using System.Security.Permissions;

namespace KFCTop100.Controllers
{
    public class RecordsController : Controller
    {
        private readonly IKFCService service;

        public RecordsController(IKFCService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            int i = 0;
            return View(new RecordViewModel { items = service.LoadTop100().Select(x => new LeaderBoardItem { Id = x.Id, Date = x.Date, Name = x.Name, Place = ++i, Population = x.Population }).ToList() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string? searchString)
        {
            int i = 0;
            if (searchString is null || searchString == "") return RedirectToAction(nameof(Index));
            Console.WriteLine("Search string: "+searchString);
            return View(new RecordViewModel
            {
                items = service.LoadTop100()
                .Select(x => new LeaderBoardItem { Id = x.Id, Date = x.Date, Name = x.Name, Place = ++i, Population = x.Population })
                .Where(x => x.Name!.ToLower().Contains(searchString!.Trim().ToLower()))
                .ToList(),
                SearchString = searchString!
            });
        }

        public IActionResult Details(int id)
        {
            try
            {
                Record item = service.LoadById(id);
                return View(item);
            }
            catch
            {
                return NotFound();
            }
        }
        public IActionResult Error()
        {
            return View("404");
        }

        public IActionResult? DisplayImage(int id)
        {
            var item = service.LoadById(id);
            if (item != null && item.Picture != null)
            {
                return File(item.Picture, "image/png");
            }
            return null;
        }

        public IActionResult? DisplayDefaultImage()
        {
            byte[]? item = service.LoadDefaultPicture();
            return File(item!, "image/png");
        }
    }
}
