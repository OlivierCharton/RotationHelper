using Blish_HUD.Settings;

namespace Japyx.Modules.Core.Models {
    public class SettingsModel : BaseSettingsModel {
        public SettingEntry<string> Version { get; set; }

        public SettingsModel(SettingCollection settings) {
        }
    }
}
