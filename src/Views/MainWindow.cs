using Blish_HUD;
using Blish_HUD.Content;
using Blish_HUD.Controls;
using Japyx.Modules.Core.Controls;
using Japyx.Modules.Core.Models;
using Japyx.RotationHelper.Controls;
using Japyx.RotationHelper.Models;
using Japyx.RotationHelper.Services;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static Japyx.RotationHelper.Services.TextureManager;
using FlowPanel = Japyx.Modules.Core.Controls.FlowPanel;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using StandardWindow = Japyx.Modules.Core.Views.StandardWindow;
using TextBox = Japyx.Modules.Core.Controls.TextBox;

namespace Japyx.RotationHelper.Views
{
    public class MainWindow : StandardWindow
    {
        private readonly SettingsModel _settings;
        private readonly TextureManager _textureManager;
        private readonly ObservableCollection<RotationModel> _rotationModels;
        private readonly Data _data;
        private readonly AsyncTexture2D _windowEmblem = AsyncTexture2D.FromAssetId(156015); //TODO: changer icone

        private readonly ImageButton _addRotationButton;

        private readonly bool _created;





        public FlowPanel ContentPanel { get; private set; }
        public FlowPanel RotationPanel { get; private set; }


        public List<RotationCard> RotationCards { get; } = new();


        private readonly FlowPanel _dropdownPanel;
        private readonly TextBox _filterBox;
        private readonly FlowPanel _buttonPanel;

        public MainWindow(AsyncTexture2D background, Rectangle windowRegion, Rectangle contentRegion, SettingsModel settings, TextureManager textureManager, ObservableCollection<RotationModel> rotationModels, Data data) 
            : base(background, windowRegion, contentRegion)
        {
            _settings = settings;
            _textureManager = textureManager;
            _rotationModels= rotationModels;
            _data = data;
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

            //TODO: actions du filtre

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
                Texture = _textureManager.GetControlTexture(ControlTextures.Plus_Button),
                HoveredTexture = _textureManager.GetControlTexture(ControlTextures.Plus_Button_Hovered),
                SetLocalizedTooltip = () => "Ajouter une rotation",
                ClickAction = (m) => RotationWindow.ToggleWindow()
            };


            CreateRotationControls(_rotationModels);
            _created = true;
        }

        public RotationWindow RotationWindow { get; set; }




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
                    _filterBox.Width = _dropdownPanel.Width - _buttonPanel.Width - 2;
                }

                if (e.CurrentSize.Y < 135)
                {
                    Size = new Point(Size.X, 135);
                }

                //_settings.WindowSize.Value = Size;
            }
        }

        public void CreateRotationControls(IEnumerable<RotationModel> models)
        {
            foreach (RotationModel r in models)
            {
                if (RotationCards.Find(e => e.Rotation.Name == r.Name) == null)
                {
                    RotationCards.Add(new RotationCard(r, _textureManager, _data, this, _settings)
                    {
                        Parent = RotationPanel
                    });
                }
            }
        }
    }
}
