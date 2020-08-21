using System.IO;

namespace RestWithAPS_NETUdemy.Business.Implementations
{
    public class FileBusiness : IFileBusiness
    {
        public byte[] GetPDFFile()
        {
            string path = Directory.GetCurrentDirectory();
            string fullPath = path + "\\Other\\Guia+de+Básico+de+Docker.pdf";
            return File.ReadAllBytes(fullPath);
        }
    }
}
