using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
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
            var folder = _dbContext.Folders
                .Include(x=>x.Owner)
                .Include(x => x.Files).ThenInclude(x=>x.Owner)
                .Include(x => x.Folders).ThenInclude(x=>x.Owner)
                .First(x => x.ParentFolderId == null);
            
            return View(new FolderViewModel
            {
                Folder = folder,
                Path = GetPath(folder)
            });
        }

        [HttpGet("[Controller]/{id}")]
        public IActionResult GetFolder(Guid id)
        {
            if (ModelState.IsValid)
            {
                var folder = _dbContext.Folders
                    .Include(x=>x.Owner)
                    .Include(x=>x.Files).ThenInclude(x=>x.Owner)
                    .Include(x=>x.Folders).ThenInclude(x=>x.Owner)
                    .Include(x=>x.ParentFolder).ThenInclude(x=>x.Owner)
                    .FirstOrDefault(x => x.Id == id);
                
                if (folder is null)
                    return NotFound();

                return View("Index", new FolderViewModel
                {
                    Folder = folder,
                    Path = GetPath(folder)
                });
            }

            return BadRequest();
        }

        private List<(string name, Guid id)> GetPath(Folder folder)
        {
            var stack = new Stack<(string name, Guid id)>();
            stack.Push((folder.Name, folder.Id));
            while (folder.ParentFolderId != null)
            {
                folder = _dbContext.Folders.First(x => x.Id == folder.ParentFolderId);
                stack.Push((folder.Name, folder.Id));
            }
            
            return stack.ToList();
        }
    }
}