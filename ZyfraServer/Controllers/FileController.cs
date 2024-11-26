using Microsoft.AspNetCore.Mvc;
using ZyfraServer.Interfaces;

namespace ZyfraServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("{filename}")]
        public ActionResult<Dictionary<string, string>> Get(string filename)
        {
            var data = Load(filename);
            if (data.Count == 0)
            {
                return NotFound("Файл не найден или пуст.");
            }
            return Ok(data);
        }

        [HttpPost("{filename}")]
        public ActionResult UpdateFile(string filename, [FromBody] KeyValuePair<string, string> entry)
        {
            var data = Load(filename);
            if (!data.ContainsKey(entry.Key))
            {
                return NotFound($"Id '{entry.Key}' не найден.");
            }

            data[entry.Key] = entry.Value;
            Save(filename, data);
            return Ok($"Обновлено: {entry.Key} = {entry.Value}");
        }

        [HttpPost("create/{filename}")]
        public ActionResult CreateFile(string filename)
        {
            Create(filename);
            return Ok("Файл создан с начальными данными.");
        }

        private Dictionary<string, string> Load(string filename)
        {
            var data = new Dictionary<string, string>();

            if (_fileService.Exists(filename))
            {
                foreach (var line in _fileService.ReadLines(filename))
                {
                    if (line.Contains("="))
                    {
                        var parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            data[parts[0].Trim()] = parts[1].Trim();
                        }
                    }
                }
            }

            return data;
        }

        private void Save(string filename, Dictionary<string, string> data)
        {
            var lines = new List<string>();
            foreach (var entry in data)
            {
                lines.Add($"{entry.Key}={entry.Value}");
            }
            _fileService.WriteLines(filename, lines);
        }

        private void Create(string filename)
        {
            var initialData = new List<string>
        {
            "0=0",
            "1=1",
            "2=2",
            "3=3",
            "4=4",
            "5=5",
            "6=6",
            "7=7",
            "8=8",
            "9=9"
        };
            _fileService.WriteLines(filename, initialData);
        }
    }
}