using Blish_HUD.Modules.Managers;
using System.IO;

namespace Japyx.Modules.Core.Models {
#nullable enable
    public class PathCollection {
        private readonly string? _moduleName = null;
        private readonly DirectoriesManager _directoriesManager;

        public PathCollection(DirectoriesManager directoriesManager, string moduleName) {
            _directoriesManager = directoriesManager;
            _moduleName = moduleName.Replace(' ', '_').ToLower();

            BasePath = _directoriesManager.GetFullDirectoryPath("japyx");

            if (!Directory.Exists(ModulePath)) {
                _ = Directory.CreateDirectory(ModulePath);
            }
        }

        public string BasePath { get; }

        public string ModulePath => $@"{BasePath}\{_moduleName}\";

        public string SharedSettingsPath => $@"{BasePath}\shared_settings.json";
    }
#nullable disable
}
