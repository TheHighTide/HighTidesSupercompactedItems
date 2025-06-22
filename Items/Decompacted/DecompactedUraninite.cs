using HighTide.SupercompactedItems.Items.Compacted;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using System.Linq;
using static CraftData;

namespace HighTide.SupercompactedItems.Items.Decompacted
{
    internal class DecompactedUraninite
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("DecompactedUraninite", "Uraninite Crystal", "U<sub>3</sub>O<sub>8</sub>. A highly radioactive material.")
            .WithIcon(SpriteManager.Get(TechType.UraniniteCrystal));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate template = new CloneTemplate(prefab.Info, TechType.UraniniteCrystal);
            prefab.SetGameObject(template);

            RecipeData recipeData = new RecipeData
            {
                craftAmount = 0,
                Ingredients =
                {
                    new Ingredient(SupercompactedUraninite.Info.TechType, 1)
                },
                LinkedItems = Enumerable.Repeat(TechType.UraniniteCrystal, 10).ToList()
            };

            prefab.SetRecipe(recipeData)
                .WithCraftingTime(2.0f);

            prefab.SetUnlock(TechType.UraniniteCrystal, fragmentsToScan: 0);

            prefab.Register();
        }
    }
}
