namespace API.Helpers
{
    public class Log  //Class to write to Log Name of Developer
    {
        private readonly string _entryUser;
        public Log(string entryUser)
        {
            _entryUser = entryUser;
        }
        public void WriteToLog(string message) => Console.WriteLine($"[{DateTime.Now}] {_entryUser}: {message}");

    }
}
