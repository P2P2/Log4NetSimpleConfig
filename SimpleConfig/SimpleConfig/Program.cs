using System;

namespace SimpleConfig
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            //ConfigureLoggingToConsole();
            //ConfigureLoggingToFile(@"C:\temp\log.txt");
            ConfigureLoggingToFileAndConsole(@"C:\temp\log.txt");

            log.Info("Hello, world.");
            log.Debug("I like saying hello.");
        }

        private static void ConfigureLoggingToConsole()
        {
            var layout = new log4net.Layout.PatternLayout(@"%date{HH:mm:ss.fff} %8timestamp %-5level [%thread] |%ndc| %logger{2} %message%newline%exception");
            layout.ActivateOptions();

            var appender = new log4net.Appender.ConsoleAppender
            {
                Layout = layout,
                Threshold = log4net.Core.Level.Debug
            };
            appender.ActivateOptions();

            log4net.Config.BasicConfigurator.Configure(appender);
        }

        private static void ConfigureLoggingToFile(string fileName)
        {
            var layout = new log4net.Layout.PatternLayout
            {
                ConversionPattern = @"%date{HH:mm:ss.fff} %8timestamp %-5level [%thread] |%ndc| %logger{2} %message%newline%exception",
                Header = new log4net.Util.PatternString(@"%appdomain run by %username at %date{HH:mm:ss.fff}%newline").Format(),
                Footer = new string('-', 80) + Environment.NewLine
            };
            layout.ActivateOptions();

            var appender = new log4net.Appender.RollingFileAppender
            {
                AppendToFile = true,
                CountDirection = 1,
                File = fileName,
                Layout = layout,
                MaximumFileSize = "200MB",
                MaxSizeRollBackups = 20,
                RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Size,
                Threshold = log4net.Core.Level.Debug
            };
            appender.ActivateOptions();

            log4net.Config.BasicConfigurator.Configure(appender);
        }

        private static void ConfigureLoggingToFileAndConsole(string fileName)
        {
            var fileLayout = new log4net.Layout.PatternLayout
            {
                ConversionPattern = @"%date{HH:mm:ss.fff} %8timestamp %-5level [%thread] |%ndc| %logger{2} %message%newline%exception",
                Header = new log4net.Util.PatternString(@"%appdomain run by %username at %date{HH:mm:ss.fff}%newline").Format(),
                Footer = new string('-', 80) + Environment.NewLine
            };
            fileLayout.ActivateOptions();

            var fileAppender = new log4net.Appender.RollingFileAppender
            {
                AppendToFile = true,
                CountDirection = 1,
                File = fileName,
                Layout = fileLayout,
                MaximumFileSize = "200MB",
                MaxSizeRollBackups = 20,
                RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Size,
                Threshold = log4net.Core.Level.Debug
            };
            fileAppender.ActivateOptions();

            var consoleLayout = new log4net.Layout.PatternLayout(@"%date{HH:mm:ss.fff} %8timestamp %-5level [%thread] |%ndc| %logger{2} %message%newline%exception");
            consoleLayout.ActivateOptions();

            var consoleAppender = new log4net.Appender.ConsoleAppender
            {
                Layout = consoleLayout,
                Threshold = log4net.Core.Level.Info
            };
            consoleAppender.ActivateOptions();

            log4net.Config.BasicConfigurator.Configure(fileAppender, consoleAppender);
        }
    }
}
