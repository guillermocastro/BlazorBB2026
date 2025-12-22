using BlazorBB2026.Data;
//using BlazorBB2026.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Drawing;
//using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BB21.Controllers
{

    [Authorize]
    [DisableRequestSizeLimit]
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public UploadController(IWebHostEnvironment environment, IConfiguration _config, ApplicationDbContext context)
        {
            _environment = environment;
            _configuration = _config;
            _context = context;
        }
        //[HttpGet("/api/file/{id}")]
        ////[HttpGet("{id}")] //Ojo Refactorizar
        //public IActionResult Get(int id)
        //{
        //    var file = _context.DocFile.FirstOrDefault(f => f.Id == id);
        //    if (file == null)
        //    {
        //        return NotFound("File not found.");
        //    }
        //    return File(file.File, file.ContentType);
        //}
        public IActionResult Get2(int id)
        {
            try
            {
                // Simulate fetching a file by ID
                // In a real application, you would retrieve the file from a database or file system
                var filePath = Path.Combine(_environment.WebRootPath, "uploads", $"file{id}.txt");
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/octet-stream", $"file{id}.txt");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("upload/{FileName}")]
        public async Task<IActionResult> DocFileInboxAsync(IFormFile file, string FileName)
        {
            const long maxFileSize = 10 * 1024 * 1024;
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            else
            {
                if (file != null && file.Length > 0)
                {
                    MemoryStream ms = new MemoryStream();
                    await file.OpenReadStream().CopyToAsync(ms);
                    var bytes = ms.ToArray();
                    var fileSize = file.Length;
                    var originalName = file.FileName;
                    var ContentType = file.ContentType;
                    var fileType = file.ContentType;
                    var docDate = DateTime.Now;
                    using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    //using (var conn = new SqlConnection("Server=5dcab9d.online-server.cloud,1433;Database=BB30PRD;User ID=sa;Password=t*5_N6$r#9;Trust Server Certificate=true"))
                    {
                        var query = "INSERT INTO [vk].[DocFile] ([Filename],[File], [OriginalName], [FileSize],[DocNo],[CompanyNo],[ContentType],[Prefix],[Reference],[Delete],[DocDate]) VALUES (@Filename, @File, @OriginalName, @FileSize,'','',@ContentType,'','','FALSE',@DocDate)";
                        var command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@File", bytes);
                        command.Parameters.AddWithValue("@OriginalName", originalName);
                        command.Parameters.AddWithValue("@Filename", originalName);
                        command.Parameters.AddWithValue("@FileSize", fileSize);
                        command.Parameters.AddWithValue("@ContentType", ContentType);
                        command.Parameters.AddWithValue("@DocDate", docDate);
                        try
                        {
                            conn.Open();
                            await command.ExecuteNonQueryAsync();
                            //conn.Close();
                            return Ok(new { Message = "File uploaded successfully." });
                        }
                        catch (SqlException ex)
                        {
                            return StatusCode(500, $"SQL Error: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            return StatusCode(500, $"Error: {ex.Message}");
                        }
                    }
                }
                else {
                    return BadRequest("Empty file.");
                }
            }
        }
        [HttpPost("uploadbak/FC/{FileName}")]
        public async Task<IActionResult> DocFileFCAsync(IFormFile file, string FileName)
        {
            //string newfile = FileName;
            try
            {
                //UploadFile(file,"SocSec");
                if (file != null && file.Length > 0)
                {
                    var imagePath = @"\FC";
                    var uploadPath = _environment.WebRootPath + imagePath;
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    var fullPath = Path.Combine(uploadPath, FileName);
                    using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    //Copy file
                    var filename = uploadPath + $@"\{FileName}";
                    if (!System.IO.File.Exists(filename))
                    {
                        using (FileStream fs = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                    //Delete file
                    if (System.IO.File.Exists(fullPath))

                    {

                        //System.IO.File.Delete(fullPath);

                    }
                }
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("upload/NO/{FileName}")]
        public async Task<IActionResult> DocFileNOAsync(IFormFile file, string FileName)
        {
            string newfile = FileName;
            try
            {
                //UploadFile(file,"SocSec");
                if (file != null && file.Length > 0)
                {
                    var imagePath = @"\NO";
                    var uploadPath = _environment.WebRootPath + imagePath;
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    var fullPath = Path.Combine(uploadPath, FileName);
                    using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    //Copy file
                    var filename = uploadPath + $@"\{FileName}";
                    if (!System.IO.File.Exists(filename))
                    {
                        using (FileStream fs = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                    //Delete file
                    if (System.IO.File.Exists(fullPath))

                    {

                        //System.IO.File.Delete(fullPath);

                    }
                }
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("upload/multiple")]
        public IActionResult Multiple(IFormFile[] files)
        {
            try
            {
                foreach (var file in files)
                {
                    UploadFile(file, "Procurement");
                }
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task UploadFile(IFormFile file,string folder)
        {
            if (file != null && file.Length > 0)
            {
                var imagePath = @"\" + folder;
                var uploadPath = _environment.WebRootPath + imagePath;
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var fullPath = Path.Combine(uploadPath, file.FileName);
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }

        [HttpPost("upload/EC/{FileName}")]
        public async Task<IActionResult> DocFileECAsync(IFormFile file, string FileName)
        {
            string newfile = FileName;
            try
            {
                //UploadFile(file,"SocSec");
                if (file != null && file.Length > 0)
                {
                    var imagePath = @"\EC";
                    var uploadPath = _environment.WebRootPath + imagePath;
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    var fullPath = Path.Combine(uploadPath, file.FileName);
                    using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    //Copy file
                    var filename = uploadPath + $@"\{FileName}";
                    if (!System.IO.File.Exists(filename))
                    {
                        using (FileStream fs = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                    //Delete file
                    if (System.IO.File.Exists(fullPath))

                    {

                        System.IO.File.Delete(fullPath);

                    }
                }
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("upload/TK/{FileName}")]
        public async Task<IActionResult> DocFileTKAsync(IFormFile file, string FileName)
        {
            string newfile = FileName;
            try
            {
                //UploadFile(file,"SocSec");
                if (file != null && file.Length > 0)
                {
                    var imagePath = @"\TK";
                    var uploadPath = _environment.WebRootPath + imagePath;
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    var fullPath = Path.Combine(uploadPath, file.FileName);
                    using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    //Copy file
                    var filename = uploadPath + $@"\{FileName}";
                    if (!System.IO.File.Exists(filename))
                    {
                        using (FileStream fs = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                    //Delete file
                    if (System.IO.File.Exists(fullPath))

                    {

                        System.IO.File.Delete(fullPath);

                    }
                }
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //----------------------------------------------- Upload into SQL Server
        [HttpPost("upload/DocFile/{FileName}")]
        public async Task<IActionResult> DocFileAsync(IFormFile file, string FileName)
        {
            string newfile = FileName;
            try
            {
                if (file != null && file.Length > 0)
                {
                    byte[] fileContent=new byte[file.Length];
                    var reader = await new StreamReader(file.OpenReadStream()).ReadToEndAsync();
                }
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
