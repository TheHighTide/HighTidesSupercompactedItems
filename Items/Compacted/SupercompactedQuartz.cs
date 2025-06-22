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
    internal class SupercompactedQuartz
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("SupercompactedQuartz", "Supercompacted Quartz", "Quartz that has been supercompacted one time.");

        public static void Register()
        {
            string executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assetsPath = Path.Combine(executingPath, "Assets");
            string itemSpritePath = Path.Combine(assetsPath, "Sprites", "Items");
            string itemModelPath = Path.Combine(assetsPath, "Bundles", "Items");
            Texture2D itemTexture = ImageUtils.LoadTextureFromFile(Path.Combine(itemSpritePath, "CompressedQuartz.png"));
            var assetPack = AssetBundle.LoadFromFile(Path.Combine(itemModelPath, "SupercompactedQuartz"));
            var prefabAsset = assetPack.LoadAsset<GameObject>("SupercompactedQuartz");

            Atlas.Sprite itemSprite = ImageUtils.LoadSpriteFromTexture(itemTexture);

            PrefabInfo prefabInfo = Info;
            prefabInfo.WithIcon(itemSprite);
            CustomPrefab prefab = new CustomPrefab(prefabInfo);

            CloneTemplate template = new CloneTemplate(prefab.Info, TechType.Quartz);

            template.ModifyPrefab += obj =>
            {
                GameObject model = obj.transform.Find("Quartz_small").gameObject;
                //obj.GetComponent<BoxCollider>().enabled = false;
                model.SetActive(false);
                GameObject newModel = GameObject.Instantiate(prefabAsset, obj.transform);
                //newModel.transform.localPosition = Vector3.zero;
                newModel.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                SphereCollider newCollider = newModel.GetComponent<SphereCollider>();
                if (newCollider != null) newCollider.enabled = false;
            };

            prefab.SetGameObject(template);

            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients =
                {
                    new CraftData.Ingredient(TechType.Quartz, 10)
                }
            };

            prefab.SetRecipe(recipeData)
                .WithCraftingTime(3.5f);

            prefab.SetUnlock(TechType.Quartz, fragmentsToScan: 0)
                .WithPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);

            prefab.Register();
        }
    }
}
