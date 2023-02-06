using Blish_HUD;
using Blish_HUD.Content;
using Blish_HUD.Controls;
using Gw2Sharp.WebApi.V2.Models;
using Japyx.Modules.Core.Controls;
using Japyx.RotationHelper.Controls;
//using Japyx.RotationHelper.Controls.ImageToggle;
using Japyx.RotationHelper.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using static Japyx.RotationHelper.Services.TextureManager;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using FlowPanel = Japyx.Modules.Core.Controls.FlowPanel;
using TextBox = Japyx.Modules.Core.Controls.TextBox;
using Japyx.Modules.Core.Models;

namespace Japyx.RotationHelper.Views
{
    public class MainWindow : StandardWindow
    {
        public FlowPanel ContentPanel { get; private set; }
        public FlowPanel RotationPanel { get; private set; }


        public List<RotationControl> RotationControls { get; set; } = new();


        private readonly bool _created;

        private readonly FlowPanel _dropdownPanel;
        private readonly TextBox _filterBox;
        private readonly FlowPanel _buttonPanel;
        private readonly ImageButton _addRotationButton;

        public MainWindow(AsyncTexture2D background, Rectangle windowRegion, Rectangle contentRegion, SettingsModel settings) : base(background, windowRegion, contentRegion)
        {
            Services.TextureManager tM = RotationHelper.Instance.TextureManager;

            Parent = GameService.Graphics.SpriteScreen;
            Title = "Rotation Helper";
            SavesPosition = true;
            Id = $"MainWindow";
            CanResize = true;
            Size = new Point(385, 920);

            ContentPanel = new FlowPanel()
            {
                Parent = this,
                Location = new Point(0, 35),
                WidthSizingMode = SizingMode.Fill,
                HeightSizingMode = SizingMode.Fill,
                AutoSizePadding = new Point(5, 5),
            };

            _ = new Dummy()
            {
                Parent = ContentPanel,
                Width = ContentPanel.Width,
                Height = 3,
            };

            RotationPanel = new FlowPanel()
            {
                Parent = ContentPanel,
                Size = Size,
                ControlPadding = new Vector2(2, 4),
                WidthSizingMode = SizingMode.Fill,
                HeightSizingMode = SizingMode.Fill,
                CanScroll = true,
            };

            _dropdownPanel = new FlowPanel()
            {
                Parent = this,
                Location = new Point(0, 2),
                FlowDirection = ControlFlowDirection.SingleLeftToRight,
                WidthSizingMode = SizingMode.Fill,
                HeightSizingMode = SizingMode.AutoSize,
                ControlPadding = new Vector2(5, 0),
            };

            _filterBox = new TextBox()
            {
                Parent = _dropdownPanel,
                PlaceholderText = "Rechercher",
                Width = 100
            };

            _buttonPanel = new FlowPanel()
            {
                Parent = _dropdownPanel,
                FlowDirection = ControlFlowDirection.SingleLeftToRight,
                WidthSizingMode = SizingMode.AutoSize,
                HeightSizingMode = SizingMode.AutoSize,
                Padding = new(15)
            };
            _buttonPanel.Resized += ButtonPanel_Resized;

            _addRotationButton = new()
            {
                Parent = _buttonPanel,
                Size = new(25, 25),
                Location = new(),
                Texture = tM.GetControlTexture(ControlTextures.Plus_Button),
                HoveredTexture = tM.GetControlTexture(ControlTextures.Plus_Button_Hovered),
                SetLocalizedTooltip = () => "Ajouter une rotation",
                ClickAction = (m) => RotationHelper.Instance.RotationWindow.ToggleWindow()
            };


            CreateRotationControls(RotationHelper.Instance.RotationModels);
            _created = true;
        }

        private void ButtonPanel_Resized(object sender, ResizedEventArgs e)
        {
            _filterBox.Width = _dropdownPanel.Width - _buttonPanel.Width - 2;
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            base.OnResized(e);

            if (_created)
            {
                if (ContentPanel != null)
                {
                    ContentPanel.Size = new Point(ContentRegion.Size.X, ContentRegion.Size.Y - 35);
                }

                if (_dropdownPanel != null)
                {
                    //_dropdownPanel.Size = new Point(ContentRegion.Size.X, 31);
                    _filterBox.Width = _dropdownPanel.Width - _buttonPanel.Width - 2;
                    //_clearButton.Location = new Point(_filterBox.LocalBounds.Right - 23, _filterBox.LocalBounds.Top + 6);
                }

                if (e.CurrentSize.Y < 135)
                {
                    Size = new Point(Size.X, 135);
                }

                //Settings.WindowSize.Value = Size;
                //SetSideMenuPosition();
            }
        }

        public void CreateRotationControls(IEnumerable<RotationModel> models)
        {
            foreach (RotationModel c in models)
            {
                if (RotationControls.Find(e => e.Rotation.Name == c.Name) == null)
                {
                    RotationControls.Add(new RotationControl()
                    {
                        Rotation = c,
                        Parent = RotationPanel
                    });
                }
            }
        }
    }
}
