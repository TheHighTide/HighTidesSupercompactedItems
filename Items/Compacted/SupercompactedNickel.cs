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
    internal class SupercompactedNickel
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("SupercompactedNickel", "Supercompacted Nickel Ore", "Pure supercompacted Ni.");

        public static void Register()
        {
            string executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assetsPath = Path.Combine(executingPath, "Assets");
            string itemSpritePath = Path.Combine(assetsPath, "Sprites", "Items");
            string itemModelPath = Path.Combine(assetsPath, "Bundles", "Items");
            Texture2D itemTexture = ImageUtils.LoadTextureFromFile(Path.Combine(itemSpritePath, "CompressedNickel.png"));
            var assetPack = AssetBundle.LoadFromFile(Path.Combine(itemModelPath, "SupercompactedNickel"));
            var prefabAsset = assetPack.LoadAsset<GameObject>("SupercompactedNickel");

            Atlas.Sprite itemSprite = ImageUtils.LoadSpriteFromTexture(itemTexture);

            PrefabInfo prefabInfo = Info;
            prefabInfo.WithIcon(itemSprite);
            CustomPrefab prefab = new CustomPrefab(prefabInfo);

            CloneTemplate template = new CloneTemplate(prefabInfo, TechType.Nickel);

            template.ModifyPrefab += obj =>
            {
                GameObject model = obj.transform.Find("Niсkel_ore_small").gameObject;
                obj.GetComponent<CapsuleCollider>().enabled = false;
                model.SetActive(false);
                GameObject newModel = GameObject.Instantiate(prefabAsset, obj.transform);
                newModel.transform.localPosition = Vector3.zero;
                newModel.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            };

            prefab.SetGameObject(template);

            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients =
                {
                    new CraftData.Ingredient(TechType.Nickel, 10)
                }
            };

            prefab.SetRecipe(recipeData)
                .WithCraftingTime(1.25f);

            prefab.SetUnlock(TechType.Nickel, fragmentsToScan: 0)
                .WithPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);

            prefab.Register();
        }
    }
}
