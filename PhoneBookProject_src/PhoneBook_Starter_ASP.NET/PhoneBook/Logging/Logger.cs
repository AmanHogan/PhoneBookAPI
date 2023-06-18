using System.IO;

namespace PhoneBook.Logging
{
   
    public static class Logger
    {
        public static string log_path = Path.Combine(Environment.CurrentDirectory, "LogFile.txt");

        public static void WriteLog(string message) 
        {

            using (StreamWriter writer = new StreamWriter(log_path, true)) 
            {
                writer.WriteLine($"{DateTime.Now} : { message}");
            
            }
        
        }

    }
}
