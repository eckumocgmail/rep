

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCommon.CommonResources
{
    [Label("Файловый ресурс")]
    
    public class FileResource: TypeFile
    {

        public FileResource(){}
        public FileResource(TypeFile file)
        {
            Mime = file.Mime;
            Name = file.Name;
            Data = file.Data;
            Changed = file.Changed;
        }

 
        [Label("Каталог")]
        public int CatalogID { get; set; }
        public virtual FileCatalog Catalog { get; set; }
        
    }
}
