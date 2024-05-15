using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExampleMod.Content.Projectiles
{
	// This Example show how to implement simple homing projectile
	// Can be tested with ExampleCustomAmmoGun
	public class TornadoShotProjectile : ModProjectile
	{
		// Setting the default parameters of the projectile
		// You can check most of Fields and Properties here https://github.com/tModLoader/tModLoader/wiki/Projectile-Class-Documentation
		public override void SetDefaults() {
			Projectile.width = 8; // The width of projectile hitbox
			Projectile.height = 8; // The height of projectile hitbox

			Projectile.aiStyle = 0; // The ai style of the projectile (0 means custom AI). For more please reference the source code of Terraria
			Projectile.DamageType = DamageClass.Ranged; // What type of damage does this projectile affect?
			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.light = 1f; // How much light emit around the projectile
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
		}

		// Custom AI
		public override void AI() {
			Projectile.ai[0] += 1;
			if (Projectile.ai[0] == 30)
			{
				float numberProjectiles = 5; 
				float rotation = MathHelper.ToRadians(72); // 360/5
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = Projectile.velocity.RotatedBy(rotation * i); // Watch out for dividing by 0 if there is only 1 projectile.
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, perturbedSpeed, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0]);
					Dust.NewDustDirect(Projectile.position + new Vector2(8, 8), 102, 32, DustID.Cloud, 0.0f, 0.0f, 100, Color.WhiteSmoke, 10.0f);
				}
			}
		}
	}
}
