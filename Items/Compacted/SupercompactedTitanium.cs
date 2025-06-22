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
    internal class SupercompactedTitanium
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("SupercompactedTitanium", "Supercompacted Titanium", "Titanium that has been supercompacted one time.");

        public static void Register()
        {
            string executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assetsPath = Path.Combine(executingPath, "Assets");
            string itemSpritePath = Path.Combine(assetsPath, "Sprites", "Items");
            string itemModelPath = Path.Combine(assetsPath, "Bundles", "Items");
            Texture2D itemTexture = ImageUtils.LoadTextureFromFile(Path.Combine(itemSpritePath, "CompressedTitaniumIngot.png"));
            var assetPack = AssetBundle.LoadFromFile(Path.Combine(itemModelPath, "SupercompactedTitanium"));
            var prefabAsset = assetPack.LoadAsset<GameObject>("SupercompactedTitanium");

            Atlas.Sprite itemSprite = ImageUtils.LoadSpriteFromTexture(itemTexture);

            PrefabInfo compactedTitaniumInfo = Info
                .WithIcon(itemSprite);
            CustomPrefab compactedTitanium = new CustomPrefab(compactedTitaniumInfo);

            CloneTemplate compactedTitaniumTemplate = new CloneTemplate(compactedTitaniumInfo, TechType.TitaniumIngot);

            compactedTitaniumTemplate.ModifyPrefab += obj =>
            {
                GameObject model = obj.transform.Find("model").gameObject;
                model.SetActive(false);
                GameObject newModel = GameObject.Instantiate(prefabAsset, obj.transform);
                newModel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            };

            compactedTitanium.SetGameObject(compactedTitaniumTemplate);

            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients =
                {
                    new CraftData.Ingredient(TechType.TitaniumIngot, 10)
                }
            };

            compactedTitanium.SetRecipe(recipeData)
                .WithCraftingTime(5.0f);

            compactedTitanium.SetUnlock(TechType.Titanium, fragmentsToScan: 0)
                .WithPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);

            compactedTitanium.Register();
        }
    }
}
