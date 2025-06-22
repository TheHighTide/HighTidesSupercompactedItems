using HighTide.SupercompactedItems.Items.Compacted;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using System.Linq;
using static CraftData;

namespace HighTide.SupercompactedItems.Items.Decompacted
{
    internal class DecompactedCopper
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("DecompactedCopper", "Copper Ore", "Cu. Essential wiring component.")
            .WithIcon(SpriteManager.Get(TechType.Copper));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate template = new CloneTemplate(prefab.Info, TechType.Copper);
            prefab.SetGameObject(template);

            RecipeData recipeData = new RecipeData
            {
                craftAmount = 0,
                Ingredients =
                {
                    new Ingredient(SupercompactedCopper.Info.TechType, 1)
                },
                LinkedItems = Enumerable.Repeat(TechType.Copper, 10).ToList()
            };

            prefab.SetRecipe(recipeData)
                .WithCraftingTime(2.0f);

            prefab.SetUnlock(TechType.Copper, fragmentsToScan: 0);

            prefab.Register();
        }
    }
}
