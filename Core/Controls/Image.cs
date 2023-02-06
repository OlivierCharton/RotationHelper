﻿using Blish_HUD;
using Gw2Sharp.WebApi;
using Japyx.Modules.Core.Interfaces;
using System;

namespace Japyx.Modules.Core.Controls {
    public class Image : Blish_HUD.Controls.Image, ILocalizable {
        private Func<string> _setLocalizedTooltip;

        public Image() {
            GameService.Overlay.UserLocale.SettingChanged += UserLocale_SettingChanged;
            UserLocale_SettingChanged(null, null);
        }

        public Func<string> SetLocalizedTooltip {
            get => _setLocalizedTooltip;
            set {
                _setLocalizedTooltip = value;
                BasicTooltipText = value?.Invoke();
            }
        }

        public void UserLocale_SettingChanged(object sender, ValueChangedEventArgs<Locale> e) {
            if (SetLocalizedTooltip != null) BasicTooltipText = SetLocalizedTooltip?.Invoke();
        }

        protected override void DisposeControl() {
            base.DisposeControl();

            GameService.Overlay.UserLocale.SettingChanged -= UserLocale_SettingChanged;
        }
    }
}