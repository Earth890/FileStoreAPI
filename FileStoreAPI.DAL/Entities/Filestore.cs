using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStoreAPI.DAL.Entities
{
    public class Filestore
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public DateTime UploadedDate { get; set; }
        public int Version { get; set; }
    }

}
