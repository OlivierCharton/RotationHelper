using Blish_HUD;

namespace Japyx.Modules.Core.Utility {
    public class Common {
        public static double Now() {
            return GameService.Overlay.CurrentGameTime.TotalGameTime.TotalMilliseconds;
        }
    }
}
