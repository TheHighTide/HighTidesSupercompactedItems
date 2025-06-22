using HighTide.SupercompactedItems.Items.Compacted;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using System.Linq;
using static CraftData;

namespace HighTide.SupercompactedItems.Items.Decompacted
{
    internal class DecompactedTitanium
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("DecompressedTitanium", "Titanium Ingot", "Ti. Condensed titanium bar.")
            .WithIcon(SpriteManager.Get(TechType.TitaniumIngot));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate compactedTitaniumTemplate = new CloneTemplate(prefab.Info, TechType.TitaniumIngot);
            prefab.SetGameObject(compactedTitaniumTemplate);

            RecipeData recipeData = new RecipeData
            {
                craftAmount = 0,
                Ingredients =
                {
                    new Ingredient(SupercompactedTitanium.Info.TechType, 1)
                },
                LinkedItems = Enumerable.Repeat(TechType.TitaniumIngot, 10).ToList()
            };

            prefab.SetRecipe(recipeData)
                .WithCraftingTime(2.0f);

            prefab.SetUnlock(TechType.Titanium, fragmentsToScan: 0);

            prefab.Register();
        }
    }
}
