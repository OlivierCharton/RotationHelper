using Blish_HUD;

namespace Japyx.Modules.Core.Interfaces {
    public interface ILocalizable {
        void UserLocale_SettingChanged(object sender, ValueChangedEventArgs<Gw2Sharp.WebApi.Locale> e);
    }
}
