using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace HighTide.SupercompactedItems
{
    internal class CustomBuildables
    {
        public static void RegisterCustomBuildables()
        {
            Plugin.SendLog("Now attempting to register all custom buildables.", BepInEx.Logging.LogLevel.Warning);
            Plugin.SendLog("Registering \"Supercompactor\".");
            SupercompactorBuildable.Register();
            Plugin.SendLog("Registered \"Supercompactor\"!");
            Plugin.SendLog("All custom buildables should've been registered!");
        }
    }

    public static class SupercompactorBuildable
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("Supercompactor", "Supercompactor", "A device for supercompacting resources.");

        public static void Register()
        {
            string executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assetsPath = Path.Combine(executingPath, "Assets");
            string itemSpritePath = Path.Combine(assetsPath, "Sprites", "Buildables");
            Texture2D itemTexture = ImageUtils.LoadTextureFromFile(Path.Combine(itemSpritePath, "Supercompactor.png"));

            Atlas.Sprite itemSprite = ImageUtils.LoadSpriteFromTexture(itemTexture);

            PrefabInfo prefabInfo = Info;
            prefabInfo.WithIcon(itemSprite);

            CustomPrefab prefab = new CustomPrefab(Info);

            CraftTree.Type treeType = CustomCrafting.RegisterCompactionCraftingTree(prefab);

            FabricatorTemplate template = new FabricatorTemplate(prefab.Info, treeType)
            {
                FabricatorModel = FabricatorTemplate.Model.Fabricator,
                ModifyPrefab = obj =>
                {
                    string t_executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string t_assetsPath = Path.Combine(t_executingPath, "Assets");
                    string t_itemSpritePath = Path.Combine(t_assetsPath, "Textures", "Buildables");
                    Texture2D t_itemTexture = ImageUtils.LoadTextureFromFile(Path.Combine(t_itemSpritePath, "SupercompactorTexture.png"));

                    SkinnedMeshRenderer skinnedMeshRenderer = obj.GetComponentInChildren<SkinnedMeshRenderer>();
                    skinnedMeshRenderer.material.mainTexture = t_itemTexture;
                }
            };

            prefab.SetGameObject(template);

            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients =
                {
                    new CraftData.Ingredient(TechType.Titanium, 2),
                }
            };

            prefab.SetRecipe(recipeData)
                .WithCraftingTime(2f);

            prefab.SetUnlock(TechType.Builder, fragmentsToScan: 0)
                .WithPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);

            prefab.Register();
        }
    }
}
