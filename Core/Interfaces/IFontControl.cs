using MonoGame.Extended.BitmapFonts;

namespace Japyx.Modules.Core.Interfaces {
    public interface IFontControl {
        BitmapFont Font { get; set; }

        string Text { get; set; }
    }
}
