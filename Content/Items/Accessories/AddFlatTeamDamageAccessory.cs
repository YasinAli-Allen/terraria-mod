using ExampleMod.Common.Players;
using ExampleMod.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExampleMod.Content.Items.Accessories
{
	/// <summary>
	/// AOE buff which gives teammates 1000 flat hit damage (like an aura from poe)
	/// </summary>
	[AutoloadEquip(EquipType.Waist)]
	public class AddFlatTeamDamageAccessory : ModItem
	{
		public static float DamageMultiplier = 1000.0f;

		// 50 tiles is 800 world units. (50 * 16 == 800)
		public static readonly int BuffRange = 800;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageMultiplier);

		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 24;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(0, 30, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<ExampleDamageModificationPlayer>().addFlatTeamDamageEffect = true;

			// Remember that UpdateAccessory runs for all players on all clients. Only check every 10 ticks
			if (player.whoAmI != Main.myPlayer && player.miscCounter % 10 == 0) {
				Player localPlayer = Main.player[Main.myPlayer];
				if (localPlayer.team == player.team && player.team != 0 && player.Distance(localPlayer.Center) <= BuffRange) {
					localPlayer.AddBuff(ModContent.BuffType<AddFlatTeamDamageBuff>(), 60);
				}
			}
		}
	}
}
