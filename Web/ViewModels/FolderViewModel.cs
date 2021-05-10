using System;
using System.Collections.Generic;
using Web.Models;

namespace Web.ViewModels
{
    public class FolderViewModel
    {
        public Guid? Id { get; set; }

        public List<(string, Guid)> Path { get; set; }
        
        public string Name { get; set; }
        
        public ICollection<Folder> Folders { get; set; }
        
        public ICollection<File> Files { get; set; }
    }
}