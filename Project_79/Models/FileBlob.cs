using System.IO;

namespace Project_79.Models
{
    public class FileBlob
    {
        public string Name { get; set; }
        public string LastModified { get; set; }
        public string Extension { get; set; }
        public FileBlob(string name, string lastModified) { 
            Name = name;
            LastModified = lastModified;
            string extension = Path.GetExtension(name);
            if (extension == null) Extension = "none";
            else Extension = extension;
        }
    }
}
