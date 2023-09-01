using FileStorageAPI.BLL;
using FileStoreAPI.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FileStorageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileInformationRepository _repository;

        public FileController(IFileInformationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var files = _repository.GetAll();
            return Ok(files);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var file = _repository.GetById(id);
            if (file == null)
            {
                return NotFound(new { Message = "File not found" });
            }
            return Ok(file);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddFile(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var fileInformation = new FileInformation
            {
                FileName = file.FileName
            };

            _repository.AddFile(fileInformation, memoryStream.ToArray());

            return Ok(new { Message = "File added successfully.", FileId = fileInformation.Id });
        }

        [HttpPut("update/{id}/{version?}")]
        public async Task<IActionResult> UpdateFile(int id, IFormFile file, int? version = null)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            _repository.UpdateFile(id, memoryStream.ToArray(), version);

            return Ok(new { Message = "File updated successfully." });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok(new { Message = "File deleted successfully." });
        }
    }



}
