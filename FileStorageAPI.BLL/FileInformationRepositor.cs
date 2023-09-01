using FileStoreAPI.DAL.DBContext;
using FileStoreAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageAPI.BLL
{
    public class FileInformationRepository : IFileInformationRepository
    {
        private readonly FileStoreDbContext _dbContext;

        public FileInformationRepository(FileStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<FileInformation> GetAll()
        {
            return _dbContext.FileInformation.ToList();
        }

        public FileInformation? GetById(int id)
        {
            return _dbContext.FileInformation.FirstOrDefault(fi => fi.Id == id);
        }

        public void AddFile(FileInformation fileInformation, byte[] content)
        {
            _dbContext.FileInformation.Add(fileInformation);
            _dbContext.SaveChanges();

            var filestore = new Filestore
            {
                Id = fileInformation.Id, 
                Content = content,
                UploadedDate = DateTime.UtcNow,
                Version = 1
            };
            _dbContext.Filestore.Add(filestore);
            _dbContext.SaveChanges();
        }

        public void UpdateFile(int id, byte[] content, int? version = null)
        {
            var filestore = version.HasValue
                ? _dbContext.Filestore.FirstOrDefault(fs => fs.Id == id && fs.Version == version.Value)
                : _dbContext.Filestore.OrderByDescending(fs => fs.Version).FirstOrDefault(fs => fs.Id == id);

            if (filestore != null)
            {
                filestore.Content = content;
                filestore.UploadedDate = DateTime.UtcNow;
                filestore.Version++;
                _dbContext.SaveChanges();
            }
            else
            {
                var newFilestore = new Filestore
                {
                    Id = id,
                    Content = content,
                    UploadedDate = DateTime.UtcNow,
                    Version = 1
                };
                _dbContext.Filestore.Add(newFilestore);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var fileInformation = _dbContext.FileInformation.FirstOrDefault(fi => fi.Id == id);
            var filestore = _dbContext.Filestore.FirstOrDefault(fs => fs.Id == id);

            if (filestore != null)
            {
                _dbContext.Filestore.Remove(filestore);
            }
            if (fileInformation != null)
            {
                _dbContext.FileInformation.Remove(fileInformation);
            }
            _dbContext.SaveChanges();
        }
    }


}
