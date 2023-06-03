using MelonLoader;
using BTD_Mod_Helper;
using PathsPlusPlus;
using Il2CppAssets.Scripts.Models.Towers;
using BTD_Mod_Helper.Api.Enums;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using JetBrains.Annotations;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppSystem.IO;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Utils;
using System.Collections.Generic;
using System.Linq;
using Il2CppAssets.Scripts.Models.TowerSets;
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api.Display;
using UnityEngine;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Simulation.SMath;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using System.Runtime.CompilerServices;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

using AlchemistFourthPath;

[assembly: MelonInfo(typeof(AlchemistFourthPath.AlchemistFourthPath), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace AlchemistFourthPath;

public class AlchemistFourthPath : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<AlchemistFourthPath>("AlchemistFourthPath loaded!");
    }
    public class FourthPath2 : PathPlusPlus
    {
        public override string Tower => TowerType.Alchemist;
        public override int UpgradeCount => 5;

    }
    public class LongRangePotions : UpgradePlusPlus<FourthPath2>
    {
        public override int Cost => 250;
        public override int Tier => 1;
        public override string Icon => VanillaSprites.GoingTheDistance;

        public override string Description => "More range for the alchemist.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.range *= 1.5f;
            attackModel.range *= 1.5f;
        }
    }
    public class StickySituation : UpgradePlusPlus<FourthPath2>
    {
        public override int Cost => 550;
        public override int Tier => 2;
        public override string Icon => VanillaSprites.StickierGlueUpgradeIcon;

        public override string Description => "Potions no longer explode but slows bloons rapidly, popping them overtime.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var proj = Game.instance.model.GetTowerFromId("GlueGunner-210").GetAttackModel().weapons[0].projectile.Duplicate();
            attackModel.weapons[0].rate *= 0.5f;
            attackModel.weapons[0].projectile = proj;
            attackModel.weapons[0].projectile.display = Game.instance.model.GetTowerFromId("Alchemist-020").GetAttackModel().weapons[0].projectile.display;
            attackModel.weapons[0].projectile.GetBehavior<SlowModel>().multiplier *= 1.3f;
            attackModel.weapons[0].projectile.pierce += 5f;
            attackModel.weapons[0].projectile.scale *= 2;
        }
    }
    public class BloonWeakness : UpgradePlusPlus<FourthPath2>
    {
        public override int Cost => 1275;
        public override int Tier => 3;
        public override string Icon => VanillaSprites.RelentlessGlueUpgradeIcon;

        public override string Description => "Potions weaken bloon defenses, making them take more damage.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].projectile.AddBehavior(new AddBonusDamagePerHitToBloonModel("ouchyNerd", "_ouchyNerd", 10f, 6f, 7, true, true, false));
        }
    }
    public class SuperStickyConction : UpgradePlusPlus<FourthPath2>
    {
        public override int Cost => 4500;
        public override int Tier => 4;
        public override string Icon => VanillaSprites.GlueSplatterUpgradeIcon;

        public override string Description => "Super sticky substances, made with help from the Glue Gunner, allows alot of bloons to slowed at a time.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var proj = Game.instance.model.GetTowerFromId("GlueGunner-204").GetAttackModel().weapons[0].projectile.Duplicate();
            attackModel.weapons[0].projectile.AddBehavior(new CreateProjectileOnContactModel("Projectile_Create23", proj, new ArcEmissionModel("ArcEmissionModel_", 4, 0.0f, 360.0f, null, false), true, false, false));
        }
    }
    public class TheBloonBreakerBrew : UpgradePlusPlus<FourthPath2>
    {
        public override int Cost => 48362;
        public override int Tier => 5;
        public override string Icon => VanillaSprites.GlueStrikeUpgradeIcon;

        public override string Description => "The bloon breaker brew breaks bloons for maximum damage and slow.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].rate *= 0.4f;
            attackModel.weapons[0].projectile.GetBehavior<SlowModel>().multiplier *= 0.4f;
            attackModel.weapons[0].projectile.GetBehavior<AddBonusDamagePerHitToBloonModel>().perHitDamageAddition *= 3;
            attackModel.weapons[0].projectile.AddBehavior(new WindModel("breakerbloonbrew", 56, 300, 90, true, null, 0, null, 1));
        }
    }
}