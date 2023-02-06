using Japyx.Modules.Core.Services;
using System;

namespace Japyx.Modules.Core.Models {
    public class GameStateChangedEventArgs : EventArgs {
        public GameStatus OldStatus;
        public GameStatus Status;
    }
}
