using System.Diagnostics;

namespace Game2048.Utils
{
    public class DebugOutputLogger : ILogger
    {
        public void Write(string format, params object[] args)
        {
            Debug.Write(string.Format(format, args));
        }

        public void WriteLine(string format, params object[] args)
        {
            this.Write(format, args);
            this.WriteLine();
        }

        public void WriteLine()
        {
            Debug.WriteLine("");
        }
    }
}