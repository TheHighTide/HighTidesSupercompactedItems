using HighTide.SupercompactedItems.Items.Compacted;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using System.Linq;
using static CraftData;

namespace HighTide.SupercompactedItems.Items.Decompacted
{
    internal class DecompactedKyanite
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("DecompactedKyanite", "Kyanite", "Al<sub>2</sub>SiO<sub>5</sub>. Deep blue, heat resistant crystal.")
            .WithIcon(SpriteManager.Get(TechType.Kyanite));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate template = new CloneTemplate(prefab.Info, TechType.Kyanite);
            prefab.SetGameObject(template);

            RecipeData recipeData = new RecipeData
            {
                craftAmount = 0,
                Ingredients =
                {
                    new Ingredient(SupercompactedKyanite.Info.TechType, 1)
                },
                LinkedItems = Enumerable.Repeat(TechType.Kyanite, 10).ToList()
            };

            prefab.SetRecipe(recipeData)
                .WithCraftingTime(0.5f);

            prefab.SetUnlock(TechType.Kyanite, fragmentsToScan: 0);

            prefab.Register();
        }
    }
}
