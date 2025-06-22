using HighTide.SupercompactedItems.Items.Compacted;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using System.Linq;
using static CraftData;

namespace HighTide.SupercompactedItems.Items.Decompacted
{
    internal class DecompactedQuartz
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("DecompactedQuartz", "Quartz", "SiO<sup>4</sup>. Silica in crystalline form.")
            .WithIcon(SpriteManager.Get(TechType.Quartz));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate template = new CloneTemplate(prefab.Info, TechType.Quartz);
            prefab.SetGameObject(template);

            RecipeData recipeData = new RecipeData
            {
                craftAmount = 0,
                Ingredients =
                {
                    new Ingredient(SupercompactedQuartz.Info.TechType, 1)
                },
                LinkedItems = Enumerable.Repeat(TechType.Quartz, 10).ToList()
            };

            prefab.SetRecipe(recipeData)
                .WithCraftingTime(2.0f);

            prefab.SetUnlock(TechType.Quartz, fragmentsToScan: 0);

            prefab.Register();
        }
    }
}
