using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Japyx.Modules.Core.Controls;
using Japyx.Modules.Core.Extensions;
using Japyx.Modules.Core.Interfaces;
using Japyx.Modules.Core.Models;
using Japyx.Modules.Core.Utility;
using Japyx.RotationHelper.Models;
using Japyx.RotationHelper.Services;
using Japyx.RotationHelper.Views;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;
using static Blish_HUD.ContentService;
using Color = Microsoft.Xna.Framework.Color;
using FlowPanel = Japyx.Modules.Core.Controls.FlowPanel;
using Panel = Japyx.Modules.Core.Controls.Panel;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Japyx.RotationHelper.Controls
{
    public class RotationCard : Panel
    {
        private readonly List<Control> _dataControls = new();

        private readonly IconLabel _nameLabel;

        private readonly FlowPanel _contentPanel;
        private readonly Dummy _iconDummy;

        private Rectangle _cogRect;
        private Rectangle _controlBounds = Rectangle.Empty;
        private Rectangle _textBounds;
        private Rectangle _iconRectangle;

        private readonly bool _created;

        private int _cogSize;
        private int _iconSize;

        private RotationModel _rotation;
        private readonly TextureManager _textureManager;
        private readonly Data _data;
        private readonly MainWindow _mainWindow;
        private readonly SettingsModel _settings;
        private double _lastUniform;



        private readonly ImageButton _delete;


        public RotationCard(RotationModel rotation, TextureManager textureManager, Data data, MainWindow mainWindow, SettingsModel settings)
        {
            _rotation = rotation;
            _textureManager = textureManager;
            _data = data;
            _mainWindow = mainWindow;
            _settings = settings;

            HeightSizingMode = SizingMode.AutoSize;
            //WidthSizingMode = SizingMode.Fill;

            BackgroundColor = new Color(0, 0, 0, 75);
            AutoSizePadding = new Point(0, 2);

            _contentPanel = new FlowPanel()
            {
                Parent = this,
                FlowDirection = ControlFlowDirection.SingleTopToBottom,
                OuterControlPadding = new Vector2(5, 5),
            };

            _iconDummy = new()
            {
                Parent = this,
                Size = Point.Zero,
            };

            _nameLabel = new IconLabel()
            {
                Parent = _contentPanel,
                AutoSizeWidth = true,
                AutoSizeHeight = true,
            };

            _dataControls = new()
            {
                _nameLabel
            };

            _created = true;
        }

        public BitmapFont NameFont { get; set; } = GameService.Content.DefaultFont14;
        public BitmapFont Font { get; set; } = GameService.Content.DefaultFont14;

        public RotationModel Rotation
        {
            get => _rotation;
            set
            {
                if (_rotation != null)
                {
                    _rotation.Updated -= ApplyRotation;
                    _rotation.Deleted -= RotationDeleted;
                }

                _rotation = value;

                if (value != null)
                {
                    _rotation.Updated += ApplyRotation;
                    _rotation.Deleted += RotationDeleted;
                    ApplyRotation(null, null);
                }
            }
        }

        protected override void OnClick(MouseEventArgs e)
        {
            base.OnClick(e);

            if (e.IsDoubleClick)
            {
                //TODO: open rotation window
                return;
            }
        }

        protected override void DisposeControl()
        {
            base.DisposeControl();

            if (_rotation != null)
            {
                _rotation.Updated -= ApplyRotation;
                _rotation.Deleted -= RotationDeleted;
            }

            _dataControls?.DisposeAll();
            _contentPanel?.Dispose();

            Children.DisposeAll();
            _ = _mainWindow.RotationCards.Remove(this);
        }

        private BitmapFont GetFont(bool nameFont = false)
        {
            FontSize fontSize = nameFont ? FontSize.Size16 : FontSize.Size12;

            return GameService.Content.GetFont(FontFace.Menomonia, fontSize, FontStyle.Regular);
        }

        private void RotationDeleted(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ApplyRotation(object sender, EventArgs e)
        {
            _nameLabel.Text = Rotation.Name;
            _nameLabel.TextColor = new Color(168 + 15 + 25, 143 + 20 + 25, 102 + 15 + 25, 255);

            UniformWithAttached();
        }

        public Rectangle ControlContentBounds
        {
            get => _controlBounds;
            set
            {
                _controlBounds = value;

                if (_controlBounds != null)
                {
                    AdaptNewBounds();
                }
            }
        }

        public void UniformWithAttached()
        {
            double now = Common.Now();

            if (_lastUniform != now)
            {
                
                    ControlContentBounds = CalculateLayout();
            }
        }

        public Rectangle CalculateLayout()
        {
            if (_created && Visible)
            {
                UpdateDataControlsVisibility();
                _contentPanel.Visible = true;

                IEnumerable<Control> controls = _dataControls.Where(e => e.Visible);
                Control firstControl = controls.Count() > 0 ? _dataControls.Where(e => e.Visible && e is IFontControl)?.FirstOrDefault() : null;
                bool anyVisible = _contentPanel.Visible && controls.Count() > 0;
                int width = anyVisible ? controls.Max(e => e.Width) + (int)(_contentPanel.OuterControlPadding.X * 2) : 0;
                int height = anyVisible ? controls.Aggregate((int)(_contentPanel.OuterControlPadding.Y * 2), (result, e) => result + e.Height + (int)_contentPanel.ControlPadding.Y) : 0;

                _iconSize = 64;

                _iconRectangle = new Rectangle(0, 0, _iconSize, _iconSize);

                _cogSize = Math.Max(20, (firstControl != null ? ((IFontControl)firstControl).Font.LineHeight : Font.LineHeight) - 4);
                _cogSize = !anyVisible ? _iconSize / 5 : _cogSize;

                if (firstControl != null && width < firstControl.Width + 5 + _cogSize)
                {
                    width += anyVisible ? 5 + _cogSize : 0;
                }

                _textBounds = new Rectangle(_iconRectangle.Right + (anyVisible && _iconSize > 0 ? 5 : 0), 0, width, height);

                _contentPanel.Location = _textBounds.Location;
                _contentPanel.Size = _textBounds.Size;

                _controlBounds = new(
                    _iconRectangle.Left,
                    _iconRectangle.Top,
                    _textBounds.Right - _iconRectangle.Left,
                    Math.Max(_textBounds.Height, _iconRectangle.Height)
                    );

                _cogRect = new Rectangle(_controlBounds.Width - _cogSize - 4, 4, _cogSize, _cogSize);
                int size = _iconSize > 0 ? Math.Min(56, _iconRectangle.Width - 8) : Math.Min(56, Math.Min(_textBounds.Width, _textBounds.Height) - 8);
                int pad = (_iconRectangle.Width - size) / 2;
            }

            return _controlBounds;
        }

        private void UpdateDataControlsVisibility()
        {
            NameFont = GetFont(true);
            Font = GetFont();

            _contentPanel.ControlPadding = new(Font.LineHeight / 10, Font.LineHeight / 10);

            _nameLabel.Visible = true;
            _nameLabel.Font = NameFont;
        }

        private void AdaptNewBounds()
        {
            if (Width != _controlBounds.Width + AutoSizePadding.X)
            {
                Width = _controlBounds.Width + AutoSizePadding.X;
            }

            if (Height != _controlBounds.Height + AutoSizePadding.Y)
            {
                _iconDummy.Height = _controlBounds.Height;
                //Height = _controlBounds.Height + AutoSizePadding.Y;
            }

            bool anyVisible = _contentPanel.Visible && _dataControls.Where(e => e.Visible)?.Count() > 0;

            _cogRect = new Rectangle(_controlBounds.Width - _cogSize - 4, 4, _cogSize, _cogSize);
            int size = _iconSize > 0 ? Math.Min(56, _iconRectangle.Width - 8) : Math.Min(56, Math.Min(_textBounds.Width, _textBounds.Height) - 8);
            int pad = (_iconRectangle.Width - size) / 2;
        }
    }
}
