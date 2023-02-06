using Blish_HUD;
using Blish_HUD.Controls;
using Japyx.Modules.Core.Controls;
using Japyx.RotationHelper.Models;
using Microsoft.Xna.Framework;
using System;
using static Japyx.RotationHelper.Services.TextureManager;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Label = Japyx.Modules.Core.Controls.Label;
using FlowPanel = Japyx.Modules.Core.Controls.FlowPanel;

namespace Japyx.RotationHelper.Controls
{
    public class RotationControl : Panel
    {
        private readonly FlowPanel _contentPanel;
        private readonly Dummy _iconDummy;
        private readonly Label _name;
        private readonly ImageButton _delete;

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
                //_characterTooltip.Character = value;

                if (value != null)
                {
                    _rotation.Updated += ApplyRotation;
                    _rotation.Deleted += RotationDeleted;
                    ApplyRotation(null, null);
                }
            }
        }

        private RotationModel _rotation;

        public RotationControl()
        {
            Services.TextureManager tM = RotationHelper.Instance.TextureManager;

            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.Fill;

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

            _name = new()
            {
                Text = Rotation?.Name ?? "DummyText",
                Parent = _contentPanel,
                TextColor = ContentService.Colors.ColonialWhite,
                Font = GameService.Content.DefaultFont16,
                AutoSizeWidth = true,
                Location = new(5, 5),
            };

            _delete = new()
            {
                Parent = this,
                Size = new(20, 20),
                Location = new Point(_name.Right + 5 + 2, 0),
                Texture = tM.GetControlTexture(ControlTextures.Delete_Button),
                HoveredTexture = tM.GetControlTexture(ControlTextures.Delete_Button_Hovered),
                TextureRectangle = new Rectangle(7, 7, 20, 20),
                SetLocalizedTooltip = () => "Supprimer",
                ClickAction = (m) => Delete_Click()
            };


        }

        private void ApplyRotation(object sender, EventArgs e)
        {
            _name.Text = Rotation.Name;
        }

        private void Delete_Click()
        {
            Rotation.Delete();
            Rotation = null;
            Hide();
        }

        private void RotationDeleted(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
