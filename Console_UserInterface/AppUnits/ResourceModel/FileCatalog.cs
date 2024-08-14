




using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCommon.CommonResources
{
    /// <summary>
    /// Модель файлового каталога
    /// </summary>
    [Label("Файловый каталог")]
    
    public class FileCatalog : HierTable<FileCatalog>
    {

        public FileCatalog()
        {
            Files = new List<FileResource>();
            Name = "D:\\";
        }

        public FileCatalog(string name)
        {
            Files = new List<FileResource>();
            Name = name;
        }

 

        public virtual List<FileResource> Files { get; set; }
    }
}
