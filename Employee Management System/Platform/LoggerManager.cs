using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Employee_Management_System.Platform
{
    public interface ILoggerManager
    {
        void LogInformation(string message);

        void LogError(string message);

        void LogWarning(string message);
    }

    public class LoggerManager : ILoggerManager
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(LoggerManager));

        public LoggerManager()
        {
            try
            {
                XmlDocument log4netConfig = new XmlDocument();

                using (FileStream fs = File.OpenRead("log4net.config"))
                {
                    log4netConfig.Load(fs);
                    var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
                    XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

                    // The first log to be written 
                    _logger.Info("Log System Initialized");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error Occurred while creating log config. " + ex.Message);
            }
        }

        public void LogInformation(string message)
        {
            _logger.Info(message);
        }

        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }
    }
}
