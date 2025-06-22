using HighTide.SupercompactedItems.Items.Compacted;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using System.Linq;
using static CraftData;

namespace HighTide.SupercompactedItems.Items.Decompacted
{
    internal class DecompactedNickel
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("DecompressedNickel", "Nickel Ore", "Ni. Versatile alloy ingredient required for advanced fabrication.")
            .WithIcon(SpriteManager.Get(TechType.Nickel));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate template = new CloneTemplate(prefab.Info, TechType.Nickel);
            prefab.SetGameObject(template);

            RecipeData recipeData = new RecipeData
            {
                craftAmount = 0,
                Ingredients =
                {
                    new Ingredient(SupercompactedNickel.Info.TechType, 1)
                },
                LinkedItems = Enumerable.Repeat(TechType.Nickel, 10).ToList()
            };

            prefab.SetRecipe(recipeData)
                .WithCraftingTime(0.5f);

            prefab.SetUnlock(TechType.Nickel, fragmentsToScan: 0);

            prefab.Register();
        }
    }
}
