using System;
using System.IO;

namespace Z.Fantasy.GenerateCode.Services
{
    public class LogHelper
    {
        private readonly string _logFile;
        private StreamWriter _writer;
        private FileStream _fileStream = null;

        public LogHelper()
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + @"/log/" + DateTime.Now.ToString("yyyy-MM-dd");
            _logFile = file;
            CreateDirectory(file);
        }

        public void Write(string info)
        {

            try
            {
                var fileInfo = new System.IO.FileInfo(_logFile + ".txt");
                _fileStream = !fileInfo.Exists ? fileInfo.Create() : fileInfo.Open(FileMode.Append, FileAccess.Write);

                _writer = new StreamWriter(_fileStream);
                _writer.WriteLine(DateTime.Now + ": " + info);
                _writer.WriteLine("");
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                if (_writer != null)
                {
                    _writer.Close();
                    _writer.Dispose();
                    _fileStream.Close();
                    _fileStream.Dispose();
                }
            }
        }

        public void Debug(string info)
        {

            try
            {
                var fileInfo = new System.IO.FileInfo(_logFile + "Debug.txt");
                _fileStream = !fileInfo.Exists ? fileInfo.Create() : fileInfo.Open(FileMode.Append, FileAccess.Write);

                _writer = new StreamWriter(_fileStream);
                _writer.WriteLine(DateTime.Now + ": " + info);
                _writer.WriteLine("");
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                if (_writer != null)
                {
                    _writer.Close();
                    _writer.Dispose();
                    _fileStream.Close();
                    _fileStream.Dispose();
                }
            }
        }

        private void CreateDirectory(string infoPath)
        {
            DirectoryInfo directoryInfo = Directory.GetParent(infoPath);
            if (directoryInfo is { Exists: false })
            {
                directoryInfo.Create();
            }
        }
    }
}
