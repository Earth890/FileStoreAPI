using FileStoreAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageAPI.BLL
{
    public interface IFileInformationRepository
    {
        IEnumerable<FileInformation> GetAll();
        FileInformation? GetById(int id);
        void AddFile(FileInformation fileInformation, byte[] content);
        void UpdateFile(int id, byte[] content, int? version = null);
        void Delete(int id);
    }


}
