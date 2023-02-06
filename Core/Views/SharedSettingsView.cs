using Blish_HUD;
using Blish_HUD.Content;
using Blish_HUD.Controls;
using Japyx.Modules.Core.Controls;
using Japyx.Modules.Core.Extensions;
using Japyx.Modules.Core.Services;
using Microsoft.Xna.Framework;
using System.IO;
using static Japyx.Modules.Core.Utility.User32Dll;
using Bitmap = System.Drawing.Bitmap;
using Color = Microsoft.Xna.Framework.Color;
using FlowPanel = Japyx.Modules.Core.Controls.FlowPanel;
using Image = Japyx.Modules.Core.Controls.Image;
using Label = Japyx.Modules.Core.Controls.Label;

namespace Japyx.Modules.Core.Views {
    public class SharedSettingsView : BaseTab {
        private NumberBox _topOffsetBox;
        private NumberBox _leftOffsetBox;
        private NumberBox _rightOffsetBox;
        private NumberBox _bottomOffsetBox;

        private Image _topLeftImage;
        private Image _topRightImage;
        private Image _bottomLeftImage;
        private Image _bottomRightImage;

        public SharedSettingsView(SharedSettings sharedSettings, ClientWindowService clientWindowService) {
            SharedSettings = sharedSettings;
            ClientWindowService = clientWindowService;

            Icon = AsyncTexture2D.FromAssetId(156736);
            Name = "Paramètres généraux";
            Priority = 0;
        }

        public SharedSettings SharedSettings { get; }

        public ClientWindowService ClientWindowService { get; }

        public override void CreateLayout(Container p, int? width = null) {
            ContentContainer = p;
            //ContentContainer = parent;

            #region Layout
            //Panel p;
            //p = new Panel()
            //{
            //    Parent = parent,
            //    Width = width ?? parent.Width,
            //    HeightSizingMode = SizingMode.AutoSize,
            //    ShowBorder = true,
            //};

            var mcFP = new FlowPanel() {
                Parent = p,
                HeightSizingMode = SizingMode.AutoSize,
                WidthSizingMode = SizingMode.Fill,
                FlowDirection = ControlFlowDirection.SingleTopToBottom,
            };

            _ = new TitleHeader() {
                SetLocalizedTitle = () => "WindowBorders",
                SetLocalizedTooltip = () => "WindowBorder_Tooltip",
                Height = 25,
                Width = (width ?? p.Width) - 0,
                Parent = mcFP,
            };

            var cFP = new FlowPanel() {
                Parent = mcFP,
                HeightSizingMode = SizingMode.AutoSize,
                WidthSizingMode = SizingMode.Fill,
                FlowDirection = ControlFlowDirection.SingleLeftToRight,
                ControlPadding = new(3, 3),
                OuterControlPadding = new(5),
            };

            var cP = new FlowPanel() {
                Parent = cFP,
                HeightSizingMode = SizingMode.AutoSize,
                Width = (width ?? p.Width) - 20 - 225,
                FlowDirection = ControlFlowDirection.SingleTopToBottom,
                ControlPadding = new(3, 3),
            };

            var pp = new FlowPanel() {
                Parent = cP,
                WidthSizingMode = SizingMode.Fill,
                HeightSizingMode = SizingMode.AutoSize,
                FlowDirection = ControlFlowDirection.SingleLeftToRight,
            };

            _ = new Label() {
                Parent = pp,
                Width = 165,
                Location = new(35, 0),
                Height = 20,
                SetLocalizedText = () => "TopOffset",
            };
            _topOffsetBox = new() {
                Parent = pp,
                MinValue = -50,
                MaxValue = 50,
                Value = SharedSettings.WindowOffset.Top,
                SetLocalizedTooltip = () => "TopOffset",
                ValueChangedAction = (num) => UpdateOffset(),
            };

            pp = new FlowPanel() {
                Parent = cP,
                WidthSizingMode = SizingMode.Fill,
                HeightSizingMode = SizingMode.AutoSize,
                FlowDirection = ControlFlowDirection.SingleLeftToRight,
            };
            _ = new Label() {
                Parent = pp,
                Width = 165,
                Location = new(35, 0),
                Height = 20,
                SetLocalizedText = () => "LeftOffset"
            };
            _leftOffsetBox = new() {
                Parent = pp,
                MinValue = -50,
                MaxValue = 50,
                Value = SharedSettings.WindowOffset.Left,
                SetLocalizedTooltip = () => "LeftOffset",
                ValueChangedAction = (num) => UpdateOffset(),
            };

            pp = new FlowPanel() {
                Parent = cP,
                WidthSizingMode = SizingMode.Fill,
                HeightSizingMode = SizingMode.AutoSize,
                FlowDirection = ControlFlowDirection.SingleLeftToRight,
            };
            _ = new Label() {
                Parent = pp,
                Width = 165,
                Location = new(35, 0),
                Height = 20,
                SetLocalizedText = () => "BottomOffset",
            };
            _bottomOffsetBox = new() {
                Parent = pp,
                MinValue = -50,
                MaxValue = 50,
                Value = SharedSettings.WindowOffset.Bottom,
                SetLocalizedTooltip = () => "BottomOffset",
                ValueChangedAction = (num) => UpdateOffset(),
            };

            pp = new FlowPanel() {
                Parent = cP,
                WidthSizingMode = SizingMode.Fill,
                HeightSizingMode = SizingMode.AutoSize,
                FlowDirection = ControlFlowDirection.SingleLeftToRight,
            };
            _ = new Label() {
                Parent = pp,
                Width = 165,
                Location = new(35, 0),
                Height = 20,
                SetLocalizedText = () => "RightOffset",
            };
            _rightOffsetBox = new() {
                Parent = pp,
                MinValue = -50,
                MaxValue = 50,
                Value = SharedSettings.WindowOffset.Right,
                SetLocalizedTooltip = () => "RightOffset",
                ValueChangedAction = (num) => UpdateOffset(),
            };

            var subCP = new FlowPanel() {
                Parent = cFP,
                HeightSizingMode = SizingMode.AutoSize,
                WidthSizingMode = SizingMode.Fill,
                FlowDirection = ControlFlowDirection.SingleLeftToRight,
                ControlPadding = new(5, 5),
            };

            cP = new FlowPanel() {
                Parent = subCP,
                HeightSizingMode = SizingMode.AutoSize,
                Width = 125,
                FlowDirection = ControlFlowDirection.SingleTopToBottom,
                ControlPadding = new(5, 5),
            };
            _ = new Label() {
                Parent = cP,
                SetLocalizedText = () => "TopLeftCorner",
                AutoSizeWidth = true,
                Visible = false,
            };
            _topLeftImage = new() {
                Parent = cP,
                BackgroundColor = Color.White,
                Size = new(100, _rightOffsetBox.Height * 2),
                SetLocalizedTooltip = () => "TopLeftCorner",
            };
            _ = new Label() {
                Parent = cP,
                SetLocalizedText = () => "BottomLeftCorner",
                AutoSizeWidth = true,
                Visible = false,
            };
            _bottomLeftImage = new() {
                Parent = cP,
                BackgroundColor = Color.White,
                Size = new(100, _rightOffsetBox.Height * 2),
                SetLocalizedTooltip = () => "BottomLeftCorner",
            };

            cP = new FlowPanel() {
                Parent = subCP,
                HeightSizingMode = SizingMode.AutoSize,
                Width = 125,
                FlowDirection = ControlFlowDirection.SingleTopToBottom,
                ControlPadding = new(5, 5),
            };
            _ = new Label() {
                Parent = cP,
                SetLocalizedText = () => "TopRightCorner",
                AutoSizeWidth = true,
                Visible = false,
            };
            _topRightImage = new() {
                Parent = cP,
                BackgroundColor = Color.White,
                Size = new(100, _rightOffsetBox.Height * 2),
                SetLocalizedTooltip = () => "TopRightCorner",
            };
            _ = new Label() {
                Parent = cP,
                SetLocalizedText = () => "BottomRightCorner",
                AutoSizeWidth = true,
                Visible = false,
            };
            _bottomRightImage = new() {
                Parent = cP,
                BackgroundColor = Color.White,
                Size = new(100, _rightOffsetBox.Height * 2),
                SetLocalizedTooltip = () => "BottomRightCorner",
            };

            #endregion
        }

        public void UpdateOffset() {
            if (_leftOffsetBox != null) {
                SharedSettings.WindowOffset = new(_leftOffsetBox.Value, _topOffsetBox.Value, _rightOffsetBox.Value, _bottomOffsetBox.Value);

                SetTopLeftImage();
                SetTopRightImage();

                SetBottomLeftImage();
                SetBottomRightImage();
            }
        }

        private void SetTopLeftImage() {
            RECT wndBounds = ClientWindowService.WindowBounds;

            bool windowed = GameService.GameIntegration.GfxSettings.ScreenMode == Blish_HUD.GameIntegration.GfxSettings.ScreenModeSetting.Windowed;
            Point p = windowed ? new(SharedSettings.WindowOffset.Left, SharedSettings.WindowOffset.Top) : Point.Zero;

            using Bitmap bitmap = new(_topLeftImage.Width, _topLeftImage.Height);
            using var g = System.Drawing.Graphics.FromImage(bitmap);
            using MemoryStream s = new();
            g.CopyFromScreen(new System.Drawing.Point(wndBounds.Left + p.X, wndBounds.Top + p.Y), System.Drawing.Point.Empty, new(_topLeftImage.Width, _topLeftImage.Height));
            bitmap.Save(s, System.Drawing.Imaging.ImageFormat.Bmp);
            _topLeftImage.Texture = s.CreateTexture2D();
        }

        private void SetBottomLeftImage() {
            RECT wndBounds = ClientWindowService.WindowBounds;

            bool windowed = GameService.GameIntegration.GfxSettings.ScreenMode == Blish_HUD.GameIntegration.GfxSettings.ScreenModeSetting.Windowed;
            Point p = windowed ? new(SharedSettings.WindowOffset.Left, SharedSettings.WindowOffset.Bottom) : Point.Zero;

            using Bitmap bitmap = new(_bottomLeftImage.Width, _bottomLeftImage.Height);
            using var g = System.Drawing.Graphics.FromImage(bitmap);
            using MemoryStream s = new();
            g.CopyFromScreen(new System.Drawing.Point(wndBounds.Left + p.X, wndBounds.Bottom - _bottomLeftImage.Height + p.Y), System.Drawing.Point.Empty, new(_bottomLeftImage.Width, _bottomLeftImage.Height));
            bitmap.Save(s, System.Drawing.Imaging.ImageFormat.Bmp);
            _bottomLeftImage.Texture = s.CreateTexture2D();
        }

        private void SetTopRightImage() {
            RECT wndBounds = ClientWindowService.WindowBounds;

            bool windowed = GameService.GameIntegration.GfxSettings.ScreenMode == Blish_HUD.GameIntegration.GfxSettings.ScreenModeSetting.Windowed;
            Point p = windowed ? new(SharedSettings.WindowOffset.Right, SharedSettings.WindowOffset.Top) : Point.Zero;

            using Bitmap bitmap = new(_topRightImage.Width, _topRightImage.Height);
            using var g = System.Drawing.Graphics.FromImage(bitmap);
            using MemoryStream s = new();
            g.CopyFromScreen(new System.Drawing.Point(wndBounds.Right - _topRightImage.Width + p.X, wndBounds.Top + p.Y), System.Drawing.Point.Empty, new(_topRightImage.Width, _topRightImage.Height));
            bitmap.Save(s, System.Drawing.Imaging.ImageFormat.Bmp);
            _topRightImage.Texture = s.CreateTexture2D();

        }

        private void SetBottomRightImage() {
            RECT wndBounds = ClientWindowService.WindowBounds;

            bool windowed = GameService.GameIntegration.GfxSettings.ScreenMode == Blish_HUD.GameIntegration.GfxSettings.ScreenModeSetting.Windowed;
            Point p = windowed ? new(SharedSettings.WindowOffset.Right, SharedSettings.WindowOffset.Bottom) : Point.Zero;

            using Bitmap bitmap = new(_bottomLeftImage.Width, _bottomLeftImage.Height);
            using var g = System.Drawing.Graphics.FromImage(bitmap);
            using MemoryStream s = new();
            g.CopyFromScreen(new System.Drawing.Point(wndBounds.Right - _bottomRightImage.Width + p.X, wndBounds.Bottom - _bottomRightImage.Height + p.Y), System.Drawing.Point.Empty, new(_bottomRightImage.Width, _bottomRightImage.Height));
            bitmap.Save(s, System.Drawing.Imaging.ImageFormat.Bmp);
            _bottomRightImage.Texture = s.CreateTexture2D();

        }
    }
}
