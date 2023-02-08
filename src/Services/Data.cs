using Blish_HUD;
using Blish_HUD.Content;
using Gw2Sharp.Models;
using Japyx.RotationHelper.Enums;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Color = Microsoft.Xna.Framework.Color;
using Locale = Gw2Sharp.WebApi.Locale;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Japyx.RotationHelper.Services
{
    public enum ArmorWeight
    {
        Heavy,
        Medium,
        Light,
    }

    public class Data
    {
        public Dictionary<ProfessionType, Profession> Professions { get; set; } = new Dictionary<ProfessionType, Profession>()
        {
            // Guardian
            {
                ProfessionType.Guardian,
                new Profession()
                {
                    Icon = AsyncTexture2D.FromAssetId(156634),
                    IconBig = AsyncTexture2D.FromAssetId(156633),
                    Id = 1,
                    APIId = "Guardian",
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Wächter" },
                        { Locale.English, "Guardian" },
                        { Locale.Spanish, "Guardián" },
                        { Locale.French, "Gardien" },
                    },
                    Color = new Color(0, 180, 255),
                    WeightClass = ArmorWeight.Heavy,
                }
            },

            // Warrior
            {
                ProfessionType.Warrior,
                new Profession()
                {
                    Icon = AsyncTexture2D.FromAssetId(156643),
                    IconBig = AsyncTexture2D.FromAssetId(156642),
                    Id = 2,
                    APIId = "Warrior",
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Krieger" },
                        { Locale.English, "Warrior" },
                        { Locale.Spanish, "Guerrero" },
                        { Locale.French, "Guerrier" },
                    },
                    Color = new Color(247, 157, 0),
                    WeightClass = ArmorWeight.Heavy,
                }
            },

            // Engineer
            {
                ProfessionType.Engineer,
                new Profession()
                {
                    Icon = AsyncTexture2D.FromAssetId(156632),
                    IconBig = AsyncTexture2D.FromAssetId(156631),
                    Id = 3,
                    APIId = "Engineer",
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Ingenieur" },
                        { Locale.English, "Engineer" },
                        { Locale.Spanish, "Ingeniero" },
                        { Locale.French, "Ingénieur" },
                    },
                    Color = new Color(255, 222, 0),
                    WeightClass = ArmorWeight.Medium,
                }
            },

            // Ranger
            {
                ProfessionType.Ranger,
                new Profession()
                {
                    Icon = AsyncTexture2D.FromAssetId(156640),
                    IconBig = AsyncTexture2D.FromAssetId(156639),
                    Id = 4,
                    APIId = "Ranger",
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Waldläufer" },
                        { Locale.English, "Ranger" },
                        { Locale.Spanish, "Guardabosques" },
                        { Locale.French, "Rôdeur" },
                    },
                    Color = new Color(234, 255, 0),
                    WeightClass = ArmorWeight.Medium,
                }
            },

            // Thief
            {
                ProfessionType.Thief,
                new Profession()
                {
                    Icon = AsyncTexture2D.FromAssetId(156641),
                    IconBig = AsyncTexture2D.FromAssetId(103581),
                    Id = 5,
                    APIId = "Thief",
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Dieb" },
                        { Locale.English, "Thief" },
                        { Locale.Spanish, "Ladrón" },
                        { Locale.French, "Voleur" },
                    },
                    Color = new Color(255, 83, 0),
                    WeightClass = ArmorWeight.Medium,
                }
            },

            // Elementalist
            {
                ProfessionType.Elementalist,
                new Profession()
                {
                    Icon = AsyncTexture2D.FromAssetId(156630),
                    IconBig = AsyncTexture2D.FromAssetId(156629),
                    Id = 6,
                    APIId = "Elementalist",
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Elementarmagier" },
                        { Locale.English, "Elementalist" },
                        { Locale.Spanish, "Elementalista" },
                        { Locale.French, "Élémentaliste" },
                    },
                    Color = new Color(247, 0, 116),
                    WeightClass = ArmorWeight.Light,
                }
            },

            // Mesmer
            {
                ProfessionType.Mesmer,
                new Profession()
                {
                    Icon = AsyncTexture2D.FromAssetId(156636),
                    IconBig = AsyncTexture2D.FromAssetId(156635),
                    Id = 7,
                    APIId = "Mesmer",
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Mesmer" },
                        { Locale.English, "Mesmer" },
                        { Locale.Spanish, "Hipnotizador" },
                        { Locale.French, "Envoûteur" },
                    },
                    Color = new Color(255, 0, 240),
                    WeightClass = ArmorWeight.Light,
                }
            },

            // Necromancer
            {
                ProfessionType.Necromancer,
                new Profession()
                {
                    Icon = AsyncTexture2D.FromAssetId(156638),
                    IconBig = AsyncTexture2D.FromAssetId(156637),
                    Id = 8,
                    APIId = "Necromancer",
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Nekromant" },
                        { Locale.English, "Necromancer" },
                        { Locale.Spanish, "Nigromante" },
                        { Locale.French, "Nécromant" },
                    },
                    Color = new Color(192, 255, 0),
                    WeightClass = ArmorWeight.Light,
                }
            },

            // Revenant
            {
                ProfessionType.Revenant,
                new Profession()
                {
                    Icon = AsyncTexture2D.FromAssetId(961390),
                    IconBig = AsyncTexture2D.FromAssetId(965717),
                    Id = 9,
                    APIId = "Revenant",
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Widergänger" },
                        { Locale.English, "Revenant" },
                        { Locale.Spanish, "Retornado" },
                        { Locale.French, "Revenant" },
                    },
                    Color = new Color(255, 0, 0),
                    WeightClass = ArmorWeight.Heavy,
                }
            },
        };

        public Dictionary<SpecializationType, Specialization> Specializations { get; set; } = new Dictionary<SpecializationType, Specialization>()
        {
            // Druid
            {
                SpecializationType.Druid,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1128574),
                    Icon = AsyncTexture2D.FromAssetId(1128575),
                    Id = 5,
                    Profession = ProfessionType.Ranger,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Druide" },
                        { Locale.English, "Druid" },
                        { Locale.Spanish, "Druida" },
                        { Locale.French, "Druide" },
                    },
                }
            },

            // Daredevil
            {
                SpecializationType.Daredevil,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1128570),
                    Icon = AsyncTexture2D.FromAssetId(1128571),
                    Id = 7,
                    Profession = ProfessionType.Thief,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Draufgänger" },
                        { Locale.English, "Daredevil" },
                        { Locale.Spanish, "Temerario" },
                        { Locale.French, "Fracasseur" },
                    },
                }
            },

            // Berserker
            {
                SpecializationType.Berserker,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1128566),
                    Icon = AsyncTexture2D.FromAssetId(1128567),
                    Id = 18,
                    Profession = ProfessionType.Warrior,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Berserker" },
                        { Locale.English, "Berserker" },
                        { Locale.Spanish, "Berserker" },
                        { Locale.French, "Berserker" },
                    },
                }
            },

            // Dragonhunter
            {
                SpecializationType.Dragonhunter,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1128572),
                    Icon = AsyncTexture2D.FromAssetId(1128573),
                    Id = 27,
                    Profession = ProfessionType.Guardian,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Drachenjäger" },
                        { Locale.English, "Dragonhunter" },
                        { Locale.Spanish, "Cazadragones" },
                        { Locale.French, "Draconnier" },
                    },
                }
            },

            // Reaper
            {
                SpecializationType.Reaper,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1128578),
                    Icon = AsyncTexture2D.FromAssetId(1128579),
                    Id = 34,
                    Profession = ProfessionType.Necromancer,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Schnitter" },
                        { Locale.English, "Reaper" },
                        { Locale.Spanish, "Segador" },
                        { Locale.French, "Faucheur" },
                    },
                }
            },

            // Chronomancer
            {
                SpecializationType.Chronomancer,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1128568),
                    Icon = AsyncTexture2D.FromAssetId(1128569),
                    Id = 40,
                    Profession = ProfessionType.Mesmer,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Chronomant" },
                        { Locale.English, "Chronomancer" },
                        { Locale.Spanish, "Cronomante" },
                        { Locale.French, "Chronomancien" },
                    },
                }
            },

            // Scrapper
            {
                SpecializationType.Scrapper,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1128580),
                    Icon = AsyncTexture2D.FromAssetId(1128581),
                    Id = 43,
                    Profession = ProfessionType.Engineer,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Schrotter" },
                        { Locale.English, "Scrapper" },
                        { Locale.Spanish, "Chatarrero" },
                        { Locale.French, "Mécatronicien" },
                    },
                }
            },

            // Tempest
            {
                SpecializationType.Tempest,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1128582),
                    Icon = AsyncTexture2D.FromAssetId(1128583),
                    Id = 48,
                    Profession = ProfessionType.Elementalist,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Sturmbote" },
                        { Locale.English, "Tempest" },
                        { Locale.Spanish, "Tempestad" },
                        { Locale.French, "Cataclyste" },
                    },
                }
            },

            // Herald
            {
                SpecializationType.Herald,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1128576),
                    Icon = AsyncTexture2D.FromAssetId(1128577),
                    Id = 52,
                    Profession = ProfessionType.Revenant,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Herold" },
                        { Locale.English, "Herald" },
                        { Locale.Spanish, "Heraldo" },
                        { Locale.French, "Héraut" },
                    },
                }
            },

            // Soulbeast
            {
                SpecializationType.Soulbeast,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1770214),
                    Icon = AsyncTexture2D.FromAssetId(1770215),
                    Id = 55,
                    Profession = ProfessionType.Ranger,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Seelenwandler" },
                        { Locale.English, "Soulbeast" },
                        { Locale.Spanish, "Bestialma" },
                        { Locale.French, "Animorphe" },
                    },
                }
            },

            // Weaver
            {
                SpecializationType.Weaver,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1670505),
                    Icon = AsyncTexture2D.FromAssetId(1670506),
                    Id = 56,
                    Profession = ProfessionType.Elementalist,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Weber" },
                        { Locale.English, "Weaver" },
                        { Locale.Spanish, "Tejedor" },
                        { Locale.French, "Tissesort" },
                    },
                }
            },

            // Holosmith
            {
                SpecializationType.Holosmith,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1770224),
                    Icon = AsyncTexture2D.FromAssetId(1770225),
                    Id = 57,
                    Profession = ProfessionType.Engineer,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Holoschmied" },
                        { Locale.English, "Holosmith" },
                        { Locale.Spanish, "Holoartesano" },
                        { Locale.French, "Holographiste" },
                    },
                }
            },

            // Deadeye
            {
                SpecializationType.Deadeye,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1770212),
                    Icon = AsyncTexture2D.FromAssetId(1770213),
                    Id = 58,
                    Profession = ProfessionType.Thief,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Scharfschütze" },
                        { Locale.English, "Deadeye" },
                        { Locale.Spanish, "Certero" },
                        { Locale.French, "Sniper" },
                    },
                }
            },

            // Mirage
            {
                SpecializationType.Mirage,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1770216),
                    Icon = AsyncTexture2D.FromAssetId(1770217),
                    Id = 59,
                    Profession = ProfessionType.Mesmer,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Illusionist" },
                        { Locale.English, "Mirage" },
                        { Locale.Spanish, "Quimérico" },
                        { Locale.French, "Mirage" },
                    },
                }
            },

            // Scourge
            {
                SpecializationType.Scourge,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1770220),
                    Icon = AsyncTexture2D.FromAssetId(1770221),
                    Id = 60,
                    Profession = ProfessionType.Necromancer,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Pestbringer" },
                        { Locale.English, "Scourge" },
                        { Locale.Spanish, "Azotador" },
                        { Locale.French, "Fléau" },
                    },
                }
            },

            // Spellbreaker
            {
                SpecializationType.Spellbreaker,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1770222),
                    Icon = AsyncTexture2D.FromAssetId(1770223),
                    Id = 61,
                    Profession = ProfessionType.Warrior,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Bannbrecher" },
                        { Locale.English, "Spellbreaker" },
                        { Locale.Spanish, "Rompehechizos" },
                        { Locale.French, "Brisesort" },
                    },
                }
            },

            // Firebrand
            {
                SpecializationType.Firebrand,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1770210),
                    Icon = AsyncTexture2D.FromAssetId(1770211),
                    Id = 62,
                    Profession = ProfessionType.Guardian,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Aufwiegler" },
                        { Locale.English, "Firebrand" },
                        { Locale.Spanish, "Abrasador" },
                        { Locale.French, "Incendiaire" },
                    },
                }
            },

            // Renegade
            {
                SpecializationType.Renegade,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(1770218),
                    Icon = AsyncTexture2D.FromAssetId(1770219),
                    Id = 63,
                    Profession = ProfessionType.Revenant,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Abtrünniger" },
                        { Locale.English, "Renegade" },
                        { Locale.Spanish, "Renegado" },
                        { Locale.French, "Renégat" },
                    },
                }
            },

            // Harbinger
            {
                SpecializationType.Harbinger,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(2479359),
                    Icon = AsyncTexture2D.FromAssetId(2479361),
                    Id = 64,
                    Profession = ProfessionType.Necromancer,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Vorbote" },
                        { Locale.English, "Harbinger" },
                        { Locale.Spanish, "Augurador" },
                        { Locale.French, "Augure" },
                    },
                }
            },

            // Willbender
            {
                SpecializationType.Willbender,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(2479351),
                    Icon = AsyncTexture2D.FromAssetId(2479353),
                    Id = 65,
                    Profession = ProfessionType.Guardian,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Willensverdreher" },
                        { Locale.English, "Willbender" },
                        { Locale.Spanish, "Subyugador" },
                        { Locale.French, "Subjugueur" },
                    },
                }
            },

            // Virtuoso
            {
                SpecializationType.Virtuoso,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(2479355),
                    Icon = AsyncTexture2D.FromAssetId(2479357),
                    Id = 66,
                    Profession = ProfessionType.Mesmer,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Virtuose" },
                        { Locale.English, "Virtuoso" },
                        { Locale.Spanish, "Virtuoso" },
                        { Locale.French, "Virtuose" },
                    },
                }
            },

            // Catalyst
            {
                SpecializationType.Catalyst,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(2491555),
                    Icon = AsyncTexture2D.FromAssetId(2491557),
                    Id = 67,
                    Profession = ProfessionType.Elementalist,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Katalysierer" },
                        { Locale.English, "Catalyst" },
                        { Locale.Spanish, "Catalizador" },
                        { Locale.French, "Catalyseur" },
                    },
                }
            },

            // Bladesworn
            {
                SpecializationType.Bladesworn,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(2491563),
                    Icon = AsyncTexture2D.FromAssetId(2491565),
                    Id = 68,
                    Profession = ProfessionType.Warrior,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Klingengeschworener" },
                        { Locale.English, "Bladesworn" },
                        { Locale.Spanish, "Jurafilos" },
                        { Locale.French, "Jurelame" },
                    },
                }
            },

            // Vindicator
            {
                SpecializationType.Vindicator,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(2491559),
                    Icon = AsyncTexture2D.FromAssetId(2491561),
                    Id = 69,
                    Profession = ProfessionType.Revenant,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Rechtssuchender" },
                        { Locale.English, "Vindicator" },
                        { Locale.Spanish, "Justiciero" },
                        { Locale.French, "Justicier" },
                    },
                }
            },

            // Mechanist
            {
                SpecializationType.Mechanist,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(2503656),
                    Icon = AsyncTexture2D.FromAssetId(2503658),
                    Id = 70,
                    Profession = ProfessionType.Engineer,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Mech-Lenker" },
                        { Locale.English, "Mechanist" },
                        { Locale.Spanish, "Mechanista" },
                        { Locale.French, "Méchamancien" },
                    },
                }
            },

            // Specter
            {
                SpecializationType.Specter,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(2503664),
                    Icon = AsyncTexture2D.FromAssetId(2503666),
                    Id = 71,
                    Profession = ProfessionType.Thief,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Phantom" },
                        { Locale.English, "Specter" },
                        { Locale.Spanish, "Espectro" },
                        { Locale.French, "Spectre" },
                    },
                }
            },

            // Untamed
            {
                SpecializationType.Untamed,
                new Specialization()
                {
                    IconBig = AsyncTexture2D.FromAssetId(2503660),
                    Icon = AsyncTexture2D.FromAssetId(2503662),
                    Id = 72,
                    Profession = ProfessionType.Ranger,
                    Names = new Dictionary<Locale, string>()
                    {
                        { Locale.German, "Ungezähmter" },
                        { Locale.English, "Untamed" },
                        { Locale.Spanish, "Indómito" },
                        { Locale.French, "Indomptable" },
                    },
                }
            },
        };

        public class Profession
        {
            private AsyncTexture2D _icon;

            private AsyncTexture2D _iconBig;

            public Profession()
            {
            }

            public Color Color { get; set; }

            public ArmorWeight WeightClass { get; set; }

            public int Id { get; set; }

            public string APIId { get; set; }

            public AsyncTexture2D Icon
            {
                get => _icon;
                set
                {
                    _icon = value;
                    if (_icon != null)
                    {
                        _icon.TextureSwapped += Icon_TextureSwapped;
                    }
                }
            }

            public AsyncTexture2D IconBig
            {
                get => _iconBig;
                set
                {
                    _iconBig = value;
                    if (_iconBig != null)
                    {
                        _iconBig.TextureSwapped += IconBig_TextureSwapped;
                    }
                }
            }

            public Dictionary<Locale, string> Names { get; set; } = new Dictionary<Locale, string>();

            public string Name
            {
                get
                {
                    Locale locale = GameService.Overlay.UserLocale.Value is not Locale.Korean and not Locale.Chinese ? GameService.Overlay.UserLocale.Value : Locale.English;

                    return Names.TryGetValue(locale, out string name) ? name : "No Name Set.";
                }
            }

            private void IconBig_TextureSwapped(object sender, ValueChangedEventArgs<Texture2D> e)
            {
                if (e.NewValue != null)
                {
                    IconBig.TextureSwapped -= IconBig_TextureSwapped;
                    IconBig.SwapTexture(IconBig.Texture.GetRegion(new Rectangle(5, 5, IconBig.Width - 10, IconBig.Height - 10)));
                }
            }

            private void Icon_TextureSwapped(object sender, ValueChangedEventArgs<Texture2D> e)
            {
                if (e.NewValue != null)
                {
                    Icon.TextureSwapped -= Icon_TextureSwapped;
                    Icon.SwapTexture(Icon.Texture.GetRegion(new Rectangle(5, 5, Icon.Width - 10, Icon.Height - 10)));
                }
            }
        }

        public class Specialization
        {
            public int Id { get; set; }

            public int APIId { get; set; }

            public ProfessionType Profession { get; set; }

            public AsyncTexture2D Icon { get; set; }

            public AsyncTexture2D IconBig { get; set; }

            public Dictionary<Locale, string> Names { get; set; } = new Dictionary<Locale, string>();

            public string Name
            {
                get
                {
                    Locale locale = GameService.Overlay.UserLocale.Value is not Locale.Korean and not Locale.Chinese ? GameService.Overlay.UserLocale.Value : Locale.English;

                    return Names.TryGetValue(locale, out string name) ? name : "No Name Set.";
                }
            }
        }
    }
}
