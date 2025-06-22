using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace HighTide.SupercompactedItems.Items.Compacted
{
    internal class SupercompactedUraninite
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("SupercompactedUraninite", "Supercompacted Uraninite", "Uraninite that has been made heavily compacted and also much more radioactive.");

        public static void Register()
        {
            string executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assetsPath = Path.Combine(executingPath, "Assets");
            string itemSpritePath = Path.Combine(assetsPath, "Sprites", "Items");
            Texture2D itemTexture = ImageUtils.LoadTextureFromFile(Path.Combine(itemSpritePath, "CompressedUraninite.png"));

            Atlas.Sprite itemSprite = ImageUtils.LoadSpriteFromTexture(itemTexture);

            PrefabInfo prefabInfo = Info;
            prefabInfo.WithIcon(itemSprite);
            CustomPrefab prefab = new CustomPrefab(prefabInfo);

            CloneTemplate template = new CloneTemplate(prefab.Info, TechType.UraniniteCrystal);
            prefab.SetGameObject(template);

            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients =
                {
                    new CraftData.Ingredient(TechType.UraniniteCrystal, 10)
                }
            };

            prefab.SetRecipe(recipeData)
                .WithCraftingTime(6.0f);

            prefab.SetUnlock(TechType.UraniniteCrystal, fragmentsToScan: 0)
                .WithPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);

            prefab.Register();
        }
    }
}
