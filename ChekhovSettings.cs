using ChekhovsUtil.log;
using System.Diagnostics.CodeAnalysis;

namespace ChekhovsUtil
{
    public class ChekhovSettings
    {
        private static ChekhovSettings? _settings;
        private static Logger? _logger;

        [NotNull]
        public string GameName { get; private set; }

        private ChekhovSettings(string gameName)
        {
            GameName = gameName;
        }

        public static void Initialize(string gameName)
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            CreateDirectoryIfNonExistent(Path.Combine(appDataPath, gameName));
            CreateDirectoryIfNonExistent(Path.Combine(appDataPath, gameName, "Data"));
            CreateDirectoryIfNonExistent(Path.Combine(appDataPath, gameName, "Log"));

            _settings ??= new ChekhovSettings(gameName);

            // Logger must be intialized after _settings
            _logger = new Logger(typeof(ChekhovSettings));
        }

        public static ChekhovSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _logger.Error("Attempted to access CoelacanthSettings before initialization.");
                    throw new InvalidOperationException();
                }
                return _settings;
            }
        }

        private static void CreateDirectoryIfNonExistent(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
