using HighTide.SupercompactedItems.Items.Compacted;
using HighTide.SupercompactedItems.Items.Decompacted;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Utility;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace HighTide.SupercompactedItems
{
    internal class CustomCrafting
    {
        public static CraftTree.Type RegisterCompactionCraftingTree(CustomPrefab prefab)
        {
            string executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assetsPath = Path.Combine(executingPath, "Assets");
            string fabUISpritePath = Path.Combine(assetsPath, "Sprites", "UI", "Fabricator");
            Texture2D supercompactionTexture = ImageUtils.LoadTextureFromFile(Path.Combine(fabUISpritePath, "SupercompactionFabIcon.png"));
            Texture2D decompactionTexture = ImageUtils.LoadTextureFromFile(Path.Combine(fabUISpritePath, "SupercompactionFabIcon.png"));

            Atlas.Sprite supercompactionSprite = ImageUtils.LoadSpriteFromTexture(supercompactionTexture);
            Atlas.Sprite decompactionSprite = ImageUtils.LoadSpriteFromTexture(decompactionTexture);

            prefab.CreateFabricator(out CraftTree.Type treeType)
                .AddTabNode("compact", "Compaction", supercompactionSprite)
                .AddTabNode("tier1compact", "Tier 1 Compaction", supercompactionSprite, parentTabId: "compact")
                .AddCraftNode(SupercompactedTitanium.Info.TechType, "tier1compact")
                .AddCraftNode(SupercompactedCopper.Info.TechType, "tier1compact")
                .AddCraftNode(SupercompactedQuartz.Info.TechType, "tier1compact")
                .AddCraftNode(SupercompactedUraninite.Info.TechType, "tier1compact")
                .AddCraftNode(SupercompactedNickel.Info.TechType, "tier1compact")
                .AddCraftNode(SupercompactedKyanite.Info.TechType, "tier1compact")
                .AddTabNode("decompact", "Decompaction", decompactionSprite)
                .AddTabNode("tier1decompact", "Tier 1 Decompaction", decompactionSprite, parentTabId: "decompact")
                .AddCraftNode(DecompactedTitanium.Info.TechType, "tier1decompact")
                .AddCraftNode(DecompactedCopper.Info.TechType, "tier1decompact")
                .AddCraftNode(DecompactedQuartz.Info.TechType, "tier1decompact")
                .AddCraftNode(DecompactedUraninite.Info.TechType, "tier1decompact")
                .AddCraftNode(DecompactedNickel.Info.TechType, "tier1decompact")
                .AddCraftNode(DecompactedKyanite.Info.TechType, "tier1decompact");

            return treeType;
        }
    }
}
