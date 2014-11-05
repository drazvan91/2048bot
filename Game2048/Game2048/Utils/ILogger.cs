namespace Game2048.Utils
{
    public interface ILogger
    {
        void Write(string format, params object[] args);
        void WriteLine(string format, params object[] args);
        void WriteLine();
    }
}