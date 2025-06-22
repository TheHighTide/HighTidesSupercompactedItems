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
    internal class SupercompactedCopper
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("SupercompactedCopper", "Supercompacted Copper", "Copper that has been supercompacted one time.");

        public static void Register()
        {
            string executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assetsPath = Path.Combine(executingPath, "Assets");
            string itemSpritePath = Path.Combine(assetsPath, "Sprites", "Items");
            string itemModelPath = Path.Combine(assetsPath, "Bundles", "Items");
            Texture2D itemTexture = ImageUtils.LoadTextureFromFile(Path.Combine(itemSpritePath, "CompressedCopper.png"));
            var assetPack = AssetBundle.LoadFromFile(Path.Combine(itemModelPath, "SupercompactedCopper"));
            var prefabAsset = assetPack.LoadAsset<GameObject>("SupercompactedCopper");

            Atlas.Sprite itemSprite = ImageUtils.LoadSpriteFromTexture(itemTexture);

            PrefabInfo compactedCopperInfo = Info;
            compactedCopperInfo.WithIcon(itemSprite);
            CustomPrefab compactedCopper = new CustomPrefab(compactedCopperInfo);

            CloneTemplate compactedCopperTemplate = new CloneTemplate(compactedCopperInfo, TechType.Copper);

            compactedCopperTemplate.ModifyPrefab += obj =>
            {
                GameObject model = obj.transform.Find("copper_small").gameObject;
                model.SetActive(false);
                GameObject newModel = GameObject.Instantiate(prefabAsset, obj.transform);
                newModel.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            };

            compactedCopper.SetGameObject(compactedCopperTemplate);

            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients =
                {
                    new CraftData.Ingredient(TechType.Copper, 10)
                }
            };

            compactedCopper.SetRecipe(recipeData)
                .WithCraftingTime(3.0f);

            compactedCopper.SetUnlock(TechType.Copper, fragmentsToScan: 0)
                .WithPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);

            compactedCopper.Register();
        }
    }
}
