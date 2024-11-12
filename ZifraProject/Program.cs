using System;
using System.Collections.Generic;
using System.IO;

public interface IFileService
{
    bool Exists(string filename);
    IEnumerable<string> ReadLines(string filename);
    void WriteLines(string filename, IEnumerable<string> lines);
}

public class FileService : IFileService
{
    public bool Exists(string filename) => File.Exists(filename);

    public IEnumerable<string> ReadLines(string filename) => File.ReadLines(filename);

    public void WriteLines(string filename, IEnumerable<string> lines)
    {
        using (var writer = new StreamWriter(filename))
        {
            foreach (var line in lines)
            {
                writer.WriteLine(line);
            }
        }
    }
}

public class Program
{
    private static IFileService _fileService = new FileService();

    static void Main()
    {
        string filename = "data.txt";
        Dictionary<string, string> data = Load(filename);

        if (data.Count == 0)
        {
            Create(filename);
            data = Load(filename);
        }

        Console.WriteLine("Данные:");
        foreach (var entry in data)
        {
            Console.WriteLine($"{entry.Key}={entry.Value}");
        }

        while (true)
        {
            Console.Write("Введите id для обновления или 'exit' для выхода: ");
            string idToUpdate = Console.ReadLine();

            if (idToUpdate.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (!data.ContainsKey(idToUpdate))
            {
                Console.WriteLine($"Id '{idToUpdate}' не найден. Пожалуйста, введите существующий Id.");
                continue;
            }

            Console.Write($"Введите новое значение для {idToUpdate}: ");
            string newValue = Console.ReadLine();
            data[idToUpdate] = newValue;
            Save(filename, data);

            Console.WriteLine($"Обновлено: {idToUpdate} = {newValue}. Данные сохранены.");
        }
    }

    public static Dictionary<string, string> Load(string filename)
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

    public static void Save(string filename, Dictionary<string, string> data)
    {
        var lines = new List<string>();
        foreach (var entry in data)
        {
            lines.Add($"{entry.Key}={entry.Value}");
        }
        _fileService.WriteLines(filename, lines);
    }

    public static void Create(string filename)
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

    public static void SetFileService(IFileService fileService)
    {
        _fileService = fileService;
    }
}
