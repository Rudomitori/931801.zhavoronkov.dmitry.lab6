using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Web.Data;
using Web.ViewModels;

namespace Web.Controllers
{
    public class FoldersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public FoldersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IActionResult Index()
        {
            var folders = _dbContext.Folders.Include(x=>x.Owner).Where(x => x.ParentFolderId == null).ToList();
            var files = _dbContext.Files.Where(x => x.FolderId == null).ToList();
            
            return View(new FolderViewModel
            {
                Name = "Root",
                Folders = folders,
                Files = files,
                Path = new List<(string, Guid)>()
            });
        }

        private List<(string name, Guid id)> GetPath(Guid folderId)
        {
            throw new NotImplementedException();
        }
    }
}