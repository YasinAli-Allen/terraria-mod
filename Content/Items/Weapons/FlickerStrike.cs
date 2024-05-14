using ExampleMod.Content.Rarities;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExampleMod.Content.Items.Weapons
{
	public class FlickerStrike : ModItem
	{
		public override void SetDefaults() {
			Item.width = 40; // The item texture's width.
			Item.height = 40; // The item texture's height.

			Item.useStyle = ItemUseStyleID.Swing; // The useStyle of the Item.
			Item.useTime = 10; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 10; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
			Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button.

			Item.DamageType = DamageClass.Melee; // Whether your item is part of the melee class.
			Item.damage = 69; // The damage your item deals.
			Item.knockBack = 6; // The force of knockback of the weapon. Maximum is 20
			Item.crit = 6; // The critical strike chance the weapon has. The player, by default, has a 4% critical strike chance.

			Item.value = Item.buyPrice(gold: 1); // The value of the weapon in copper coins.
			Item.rare = ModContent.RarityType<ExampleModRarity>(); // Give this item our custom rarity.
			Item.UseSound = SoundID.Item1; // The sound when the weapon is being used.
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame) {
			// Grappling Hooks auto-removed if style != 10
			// player.RemoveAllGrapplingHooks();
			if (player.nearbyActiveNPCs > 0)
			{
				foreach (var npc in Main.ActiveNPCs)
				{
					if (!npc.friendly)
					{
						Vector2 temp = npc.Center - Main.MouseWorld;
						if (temp.Length() < 100.0f)
						{
							// calculate teleport position based on player direction
							float teleportX = player.direction == 1 ? npc.TopLeft.X - player.width * 2 : npc.TopRight.X + player.width * 2;
							float teleportY = npc.Center.Y - player.height;
							Vector2 teleportDestination = new Vector2(teleportX, teleportY);

							if (!Collision.SolidCollision(teleportDestination, player.width, player.height))
							{
								player.Teleport(teleportDestination, 0, 0);
								player.direction *= -1;
							}
						}
					}
				}
			}
				
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) {
			// Inflict the OnFire debuff for 1 second onto any NPC/Monster that this hits.
			// 60 frames = 1 second
			// target.AddBuff(BuffID.OnFire, 60);
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<ExampleItem>()
				.AddTile<Tiles.Furniture.ExampleWorkbench>()
				.Register();
		}
	}
}
