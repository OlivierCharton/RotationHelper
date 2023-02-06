using Blish_HUD;
using Blish_HUD.Content;
using Japyx.Modules.Core.Models;
using Japyx.Modules.Core.Views;
using Japyx.RotationHelper.Models;
using Microsoft.Xna.Framework;
using System;
using FlowPanel = Japyx.Modules.Core.Controls.FlowPanel;
using Label = Japyx.Modules.Core.Controls.Label;
using Panel = Japyx.Modules.Core.Controls.Panel;
using StandardWindow = Japyx.Modules.Core.Views.StandardWindow;
using TextBox = Japyx.Modules.Core.Controls.TextBox;

namespace Japyx.RotationHelper.Views
{
    public class RotationWindow : StandardWindow
    {


        private readonly FlowPanel _contentPanel;
        private readonly SharedSettingsView _sharedSettingsView;
        private readonly SettingsModel _settings;
        private double _tick;


        public RotationWindow(AsyncTexture2D background, Rectangle windowRegion, Rectangle contentRegion, SharedSettingsView sharedSettingsView, SettingsModel settings) : base(background, windowRegion, contentRegion)
        {
            _sharedSettingsView = sharedSettingsView;
            _settings = settings;
            _contentPanel = new()
            {
                Parent = this,
                Width = ContentRegion.Width,
                Height = ContentRegion.Height,
                ControlPadding = new(0, 10),
                CanScroll = true,
            };

            CreateInfos();
        }

        private void CreateInfos()
        {
            var p = new Panel()
            {
                Parent = _contentPanel,
                Location = new(0, 5),
                Width = ContentRegion.Width - 20,
                Height = 355,
                ShowBorder = true,
            };

            var inputNameLabel = new Label()
            {
                Parent = p,
                AutoSizeWidth = true,
                Location = new(35, 0),
                Height = 30,
                Text = "Nom : "
            };

            var inputName = new TextBox()
            {
                Parent = p,
                PlaceholderText = "Nom",
                Width = 400,
                Location = new(75, 0),
                EnterPressedAction = (s) => AddRotation(s)
            };
        }

        private void AddRotation(string input)
        {
            var rotation = new RotationModel
            {
                Name = input
            };

            var i = 1;
            while (RotationHelper.Instance.RotationModels.Count(r => r.Name == rotation.Name) > 0)
            {
                rotation.Name = $"{inputName}({i})";
                i++;
            }

            RotationHelper.Instance.RotationModels.Add(rotation);

            rotation.Initialize();

            RotationHelper.Instance.SaveRotations = true;
        }
    }
}
