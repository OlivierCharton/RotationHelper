using Blish_HUD;
using Blish_HUD.Content;
using Blish_HUD.Gw2Mumble;
using Blish_HUD.Modules;
using Blish_HUD.Modules.Managers;
using Blish_HUD.Settings;
using Gw2Sharp.WebApi.V2;
using Gw2Sharp.WebApi.V2.Models;
using Japyx.Modules.Core.Models;
using Japyx.RotationHelper.Models;
using Japyx.RotationHelper.Services;
using Japyx.RotationHelper.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Japyx.RotationHelper.Services.TextureManager;
using CornerIcon = Japyx.Modules.Core.Controls.CornerIcon;
using File = System.IO.File;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Japyx.RotationHelper
{
    [Export(typeof(Module))]
    public class RotationHelper : BaseModule<RotationHelper, MainWindow, SettingsModel>
    {
        public readonly string BaseVersion;

        private readonly Ticks _ticks = new();

        private bool _saveRotations;
        private CornerIcon _cornerIcon;

        [ImportingConstructor]
        public RotationHelper([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters)
        {
            ModuleInstance = this;
            HasGUI = true;
            BaseVersion = "1.0";

            Data = new Data();
        }

        public RotationWindow RotationWindow { get; private set; }
        public TextureManager TextureManager { get; private set; }
        public ObservableCollection<RotationModel> RotationModels { get; set; } = new();
        public Data Data { get; private set; }
        public string GlobalAccountsPath { get; set; }
        public string RotationsPath { get; set; }
        public string AccountPath { get; set; }
        public GW2API_Handler GW2APIHandler { get; private set; }

        public void UpdateFolderPaths(string accountName, bool api_handled = true)
        {
            Paths.AccountName = accountName;

            string b = Paths.ModulePath;

            AccountPath = b + accountName;
            RotationsPath = b + accountName + @"\rotations.json";

            if (!Directory.Exists(AccountPath))
            {
                _ = Directory.CreateDirectory(AccountPath);
            }

            if (api_handled && RotationModels.Count == 0)
            {
                _ = LoadRotations();
            }
        }

        public bool LoadRotations()
        {
            PlayerCharacter player = GameService.Gw2Mumble.PlayerCharacter;

            if (player == null || string.IsNullOrEmpty(player.Name))
            {
                return false;
            }

            AccountSummary getAccount()
            {
                try
                {
                    string path = GlobalAccountsPath;
                    if (File.Exists(path))
                    {
                        string content = File.ReadAllText(path);
                        List<AccountSummary> accounts = JsonConvert.DeserializeObject<List<AccountSummary>>(content);
                        return accounts.Find(e => e.CharacterNames.Contains(player.Name));
                    }
                }
                catch { }

                return null;
            }

            AccountSummary account = getAccount();
            if (account != null)
            {
                Logger.Debug($"Found '{player.Name}' in a stored character list for '{account.AccountName}'. Loading characters of '{account.AccountName}'");
                UpdateFolderPaths(account.AccountName, false);
                return LoadRotationsFile();
            }

            return false;
        }

        public bool LoadRotationsFile()
        {
            try
            {
                if (File.Exists(RotationsPath))
                {
                    FileInfo infos = new(RotationsPath);
                    string content = File.ReadAllText(RotationsPath);
                    PlayerCharacter player = GameService.Gw2Mumble.PlayerCharacter;
                    List<RotationModel> rotations = JsonConvert.DeserializeObject<List<RotationModel>>(content);

                    if (rotations != null)
                    {
                        rotations.ForEach(c =>
                        {
                            if (!RotationModels.Contains(c))
                            {
                                RotationModels.Add(new(c.Name, Paths.ModulePath, RequestRotationSave, RotationModels, Data));
                            }
                        });

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, $"Failed to load the local characters from file '{RotationsPath}'.");
                File.Copy(RotationsPath, RotationsPath.Replace(".json", " [" + DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + "].corruped.json"));
                return false;
            }
        }

        public void SaveRotationList()
        {
            string json = JsonConvert.SerializeObject(RotationModels, Formatting.Indented);

            // write string to file
            File.WriteAllText(RotationsPath, json);
        }

        public void AddOrUpdateRotation(List<RotationModel> rotations)
        {
            var freshList = rotations.Select(c => new { c.Name }).ToList();
            var oldList = RotationModels.Select(c => new { c.Name }).ToList();

            for (int i = 0; i < RotationModels.Count; i++)
            {
                RotationModel c = RotationModels[i];
                if (!freshList.Contains(new { c.Name }))
                {
                    c.Delete();
                }
            }

            foreach (var c in rotations)
            {
                if (!oldList.Contains(new { c.Name }))
                {
                    RotationModels.Add(new(c.Name, Paths.ModulePath, RequestRotationSave, RotationModels, Data));
                }
                else
                {
                    var rotation = RotationModels.FirstOrDefault(e => e.Name == c.Name);
                    rotation?.UpdateRotation(c.Name);
                }
            }

            SaveRotationList();
        }

        protected override void Initialize()
        {
            base.Initialize();

            Logger.Info($"Starting {Name} v.{BaseVersion}");

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            };

            GlobalAccountsPath = Paths.ModulePath + @"\accounts.json";

            Gw2ApiManager.SubtokenUpdated += Gw2ApiManager_SubtokenUpdated;

            Settings.Version.Value = BaseVersion;

            GW2APIHandler = new GW2API_Handler(Gw2ApiManager, AddOrUpdateRotation, GlobalAccountsPath, UpdateFolderPaths);
        }

        protected override void DefineSettings(SettingCollection settings)
        {
            base.DefineSettings(settings);

            Settings = new SettingsModel(settings);
            //Settings.ShowCornerIcon.SettingChanged += ShowCornerIcon_SettingChanged;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _ticks.APIUpdate += gameTime.ElapsedGameTime.TotalSeconds;
            _ticks.Save += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_ticks.APIUpdate > 300)
            {
                _ticks.APIUpdate = 0;

                _ = GW2APIHandler.CheckAPI();
            }

            if (_ticks.Save > 25 && _saveRotations)
            {
                _ticks.Save = 0;

                SaveRotationList();
                _saveRotations = false;
            }
        }

        public void RequestRotationSave()
        {
            _saveRotations = true;
        }

        protected override async Task LoadAsync()
        {
            await base.LoadAsync();
        }

        protected override void OnModuleLoaded(EventArgs e)
        {
            // Base handler must be called
            base.OnModuleLoaded(e);

            TextureManager = new TextureManager();

            CreateCornerIcons();

            RotationModels.CollectionChanged += OnRotationCollectionChanged;

            _ = LoadRotations();
        }

        protected override void Unload()
        {
            Settings?.Dispose();
            //RotationWindow?.Dispose();
            MainWindow?.Dispose();
            _cornerIcon?.Dispose();

            TextureManager?.Dispose();
            TextureManager = null;

            DeleteCornerIcons();

            RotationModels.CollectionChanged -= OnRotationCollectionChanged;

            base.Unload();
        }

        private void OnRotationCollectionChanged(object sender, EventArgs e)
        {
            MainWindow?.CreateRotationControls(RotationModels);
        }

        private void Gw2ApiManager_SubtokenUpdated(object sender, ValueEventArgs<IEnumerable<TokenPermission>> e)
        {
            _ = GW2APIHandler.CheckAPI();
        }

        private void CreateCornerIcons()
        {
            DeleteCornerIcons();

            _cornerIcon = new CornerIcon()
            {
                Icon = AsyncTexture2D.FromAssetId(156678),
                HoverIcon = AsyncTexture2D.FromAssetId(156679),
                SetLocalizedTooltip = () => "Rotation Helper",
                Parent = GameService.Graphics.SpriteScreen,
                Visible = true,
                ClickAction = () => MainWindow?.ToggleWindow()
            };
        }

        private void DeleteCornerIcons()
        {
            _cornerIcon?.Dispose();
            _cornerIcon = null;
        }

        protected override void LoadGUI()
        {
            base.LoadGUI();

            //var rotationsBg = AsyncTexture2D.FromAssetId(155997);
            //Texture2D cutRotationsBg = rotationsBg.Texture.GetRegion(0, 0, rotationsBg.Width - 482, rotationsBg.Height - 390);
            //RotationWindow = new(
            //    rotationsBg,
            //    new Rectangle(30, 30, cutRotationsBg.Width + 10, cutRotationsBg.Height),
            //    new Rectangle(30, 35, cutRotationsBg.Width - 5, cutRotationsBg.Height - 15),
            //    SharedSettingsView)
            //{
            //    Parent = GameService.Graphics.SpriteScreen,
            //    Title = "Ajouter une rotation",
            //    SavesPosition = true,
            //    Id = $"RotationWindow",
            //};

            Texture2D bg = TextureManager.GetBackground(Backgrounds.MainWindow);
            Texture2D cutBg = bg.GetRegion(25, 25, bg.Width - 100, bg.Height - 325);

            MainWindow = new(
            bg,
            new Rectangle(25, 25, cutBg.Width + 10, cutBg.Height),
            new Rectangle(35, 14, cutBg.Width - 10, cutBg.Height - 10),
            Settings)
            {
                Parent = GameService.Graphics.SpriteScreen,
                Title = "Rotation Helper",
                SavesPosition = true,
                Id = $"MainWindow",
                CanResize = true,
                Size = new Point(385, 920)
                //Size = Settings.WindowSize.Value,
            };
        }

        protected override void UnloadGUI()
        {
            base.UnloadGUI();

            //RotationWindow?.Dispose();
            MainWindow?.Dispose();
        }

    }
}
