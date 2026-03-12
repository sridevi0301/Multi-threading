using System;
public class FileHandler<T>
{
    public T ReadFile(string path)
    {
        try
        {
            string content = System.IO.File.ReadAllText(path);
            return (T)Convert.ChangeType(content, typeof(T));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error File not found");
            return default(T);
        }
    }

    public void WriteFile(string path, T obj)
    {
        System.IO.File.WriteAllText(path, obj.ToString());
        Console.WriteLine($"Data written to {path}");
    }
}

public class Program
{
    public static async Task Main()
    {
        FileHandler<string> file1 = new FileHandler<string>();
        Task<string> readTask = Task.Run(() => { return file1.ReadFile("C:\\Users\\chand\\source\\repos\\Multi threading\\Multi threading\\result.txt"); });
        Task writeTask = readTask.ContinueWith(task =>
        {
            if (task.Result != null)
            {
                string processed = task.Result.ToUpper();
                file1.WriteFile("result.txt", processed);
            }
        });
        await Task.WhenAll(readTask, writeTask);
        Console.WriteLine("File Processing completed");
    }
}
