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
    internal class SupercompactedKyanite
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("SupercompactedKyanite", "Supercompacted Kyanite", "A supercompacted sphere of Al<sub>2</sub>SiO<sub>5</sub> with some crystal formations protruding.");

        public static void Register()
        {
            string executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assetsPath = Path.Combine(executingPath, "Assets");
            string itemSpritePath = Path.Combine(assetsPath, "Sprites", "Items");
            string itemModelPath = Path.Combine(assetsPath, "Bundles", "Items");
            Texture2D itemTexture = ImageUtils.LoadTextureFromFile(Path.Combine(itemSpritePath, "CompressedKyanite.png"));
            var assetPack = AssetBundle.LoadFromFile(Path.Combine(itemModelPath, "SupercompactedKyanite"));
            var prefabAsset = assetPack.LoadAsset<GameObject>("SupercompactedKyanite");

            Atlas.Sprite itemSprite = ImageUtils.LoadSpriteFromTexture(itemTexture);

            PrefabInfo prefabInfo = Info;
            prefabInfo.WithIcon(itemSprite);
            CustomPrefab prefab = new CustomPrefab(prefabInfo);

            CloneTemplate template = new CloneTemplate(prefab.Info, TechType.Kyanite);

            template.ModifyPrefab += obj =>
            {
                GameObject model = obj.transform.Find("kyanite_small_03").gameObject;
                //model.transform.Find("collision").GetComponent<CapsuleCollider>().enabled = false;
                model.SetActive(false);
                GameObject newModel = GameObject.Instantiate(prefabAsset, obj.transform);
                newModel.transform.localPosition = Vector3.zero;
                newModel.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                //SphereCollider newCollider = newModel.GetComponent<SphereCollider>();
                //if (newCollider != null) newCollider.enabled = false;
            };

            prefab.SetGameObject(template);

            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients =
                {
                    new CraftData.Ingredient(TechType.Kyanite, 10)
                }
            };

            prefab.SetRecipe(recipeData)
                .WithCraftingTime(7f);

            prefab.SetUnlock(TechType.Kyanite, fragmentsToScan: 0)
                .WithPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);

            prefab.Register();
        }
    }
}
