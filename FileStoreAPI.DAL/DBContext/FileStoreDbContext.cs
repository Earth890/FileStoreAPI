using FileStoreAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStoreAPI.DAL.DBContext
{
    public class FileStoreDbContext : DbContext
    {
        public FileStoreDbContext(DbContextOptions<FileStoreDbContext> options) : base(options)
        {
        }


        public DbSet<FileInformation> FileInformation { get; set; }

        public DbSet<Filestore> Filestore { get; set; }
    }

}
