namespace ExceptionAndFiles;
class Program
{
    public static void Main(string[] args)
    {
        string fileName = "test.txt";
        string destinationFile = "default.txt";
        string destinationPath = @"c:\Temp\";
        string content = """
            This is a test file
            It has multiple lines
            and it is create using C#!
            """;

        CreateFile(fileName, content);
        ReadFile(fileName);
        // DeleteFile(fileName);
        CopyFile(fileName, destinationFile);
        MoveFile(fileName, destinationPath+fileName);

        Console.WriteLine("Finished!");
    }

    private static void MoveFile(string fileName, string destinationPath)
    {
        // Check if file exists
        if (!File.Exists(fileName) || File.Exists(destinationPath))
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File Does not exist!");

            }
            else
            {
                Console.WriteLine("Destination already exists!");
            }
            return;
        }
        else
        {
            // Moving a file requires a destination path 
            // and the name of the file.
            File.Move(fileName, destinationPath);
        }
    }

    private static void CopyFile(string fileName, string destinationFile)
    {
        // Check if file exists
        if (!File.Exists(fileName) || File.Exists(destinationFile))
        {
            Console.WriteLine("Copy aborted!");
            return;
        }
        else
        {
            // Copying a file
            File.Copy(fileName, destinationFile);
        }
    }

    private static void DeleteFile(string fileName)
    {
        // Check if file exists
        if (!File.Exists(fileName))
        {
            Console.WriteLine("File Does not exist!");
            return;
        }
        else
        {
            // Delete the file (fileName)
            // Should NOT do this!!!!
            // Check with the user that the file
            // should be deleted first!
            File.Delete(fileName);
            Console.WriteLine("File deleted!");
        }
    }

    private static void ReadFile(string fileName)
    {
        // Read a file for demo purposes
        if (!File.Exists(fileName))
        {
            Console.WriteLine("File does not exist!");
            return;
        }
        else
        {
            // Open the file to read from
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }
        }
    }

    private static void CreateFile(string path, string content)
    {
        // Create file for demo purposes
        if (!File.Exists(path)) 
        {
            Console.WriteLine("File does not exist.");
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(content);
            }
        }
        else
        {
            Console.WriteLine("File exists!");
        }
    }

    static void CreateDirectory(string path)
    {
        // Create a directory
        Directory.CreateDirectory(path);
        // List all files in a directory
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            Console.WriteLine(file);
        }

        // List all directories in a directory
        string[] directories = Directory.GetDirectories(path);
        foreach (string directory in directories)
        {
            Console.WriteLine(directory);
        }
    }

    static void DeleteDirectory(string path)
    {
        // Delete a directory
        Directory.Delete(path);
    }

    static void MoveDirectory(string sourcePath, string destinationPath)
    {
        // Move a directory
        Directory.Move(sourcePath, destinationPath);
    }

    static void CopyDirectory(string sourcePath, string destinationPath)
    {
        // Copy a directory
        Directory.CreateDirectory(destinationPath);
        string[] files = Directory.GetFiles(sourcePath);
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(destinationPath, fileName);
            File.Copy(file, destFile, true);
        }
        string[] directories = Directory.GetDirectories(sourcePath);
        foreach (string directory in directories)
        {
            string dirName = Path.GetFileName(directory);
            string destDir = Path.Combine(destinationPath, dirName);
            CopyDirectory(directory, destDir);
        }
    }
}