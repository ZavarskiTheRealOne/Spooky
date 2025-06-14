using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using Spooky.Content.Tiles.SpookyBiome;

namespace Spooky.Core
{
    public class RecipeGlobal : ModSystem
    {
        public static int AnyDemoniteBarGroup { get; private set; }

        public override void AddRecipeGroups()
        {
            //old wood counts as a vanilla wood type
            RecipeGroup wood = RecipeGroup.recipeGroups[RecipeGroupID.Wood];
            wood.ValidItems.Add(ModContent.ItemType<SpookyWoodItem>());

			RecipeGroup BaseGroup(object GroupName, int[] Items)
			{
				string Name = "";
				Name += GroupName switch
				{
					//modcontent items
					int i => Lang.GetItemNameValue((int)GroupName),
					//vanilla item ids
					short s => Lang.GetItemNameValue((short)GroupName),
					//custom group names
					_ => GroupName.ToString(),
				};
				return new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Name, Items);
			}

			RecipeGroup.RegisterGroup("SpookyMod:DemoniteBars", BaseGroup(ItemID.DemoniteBar, new int[] { ItemID.DemoniteBar, ItemID.CrimtaneBar }));
            RecipeGroup.RegisterGroup("SpookyMod:ShadowScales", BaseGroup(ItemID.ShadowScale, new int[] { ItemID.ShadowScale, ItemID.TissueSample }));
		}

        public override void AddRecipes()
        {
            //furnace crafting recipe with mossy stone from the spooky forest
            Recipe furnaceRecipe = Recipe.Create(ItemID.Furnace);
            furnaceRecipe.AddIngredient(ModContent.ItemType<SpookyStoneItem>(), 20);
            furnaceRecipe.AddRecipeGroup(RecipeGroupID.Wood, 4);
            furnaceRecipe.AddIngredient(ItemID.Torch, 3);
            furnaceRecipe.AddTile(TileID.WorkBenches);
            furnaceRecipe.Register();
        }
    }
}