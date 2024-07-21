using ChekhovsUtil.log;
using System.Diagnostics.CodeAnalysis;

namespace ChekhovsUtil
{
    public class ChekhovSettings
    {
        private static ChekhovSettings? _settings;
        private static readonly Logger _logger = new Logger(typeof(ChekhovSettings));

        [NotNull]
        public string GameName { get; private set; }

        private ChekhovSettings(string gameName)
        {
            GameName = gameName;
        }

        public static ChekhovSettings Initialize(string gameName)
        {
            CreateDirectoryIfNonExistent(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), gameName));
            CreateDirectoryIfNonExistent(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), gameName, "Data"));
            CreateDirectoryIfNonExistent(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), gameName, "Log"));

            return _settings ??= new ChekhovSettings(gameName);
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
