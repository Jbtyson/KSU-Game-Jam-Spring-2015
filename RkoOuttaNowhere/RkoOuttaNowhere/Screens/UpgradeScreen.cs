using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using RkoOuttaNowhere.Input;
using System.IO;
using RkoOuttaNowhere.Gameplay;
using RkoOuttaNowhere.Ui;
using RkoOuttaNowhere.Images;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace RkoOuttaNowhere.Screens
{
    public class UpgradeScreen : GameScreen
    {

        SoundEffect soundEngine;
        SoundEffectInstance soundEngineInstance;
        SoundEffect soundHyperspaceActivation;

        private Button damageMod, healthMod, moneyMod;

        private Image _tip, _money, _moneyIcon;

        public UpgradeScreen()
            : base()
        {
            damageMod = new Button();
            healthMod = new Button();
            moneyMod = new Button();
            _tip = new Image();
            _money = new Image();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _backgroundImage.Path = "backgrounds/upgrade";
            _backgroundImage.LoadContent();
            damageMod.LoadContent("ui/upgrades/damage", new Vector2(200.0f, 350.0f), upgradeDamage);
            healthMod.LoadContent("ui/upgrades/health", new Vector2(200.0f, 450.0f), upgradeHealth);
            moneyMod.LoadContent("ui/upgrades/money", new Vector2(200.0f, 550.0f), upgradeMoney);

            _tip.Text = "Press Space to go back";
            _tip.Position = new Vector2(5, 5);
            _tip.Path = "transparent";
            _tip.LoadContent();

            _money.Text = RKOGame.Instance.getCurrency.ToString();
            _money.Position = new Vector2(800, 5);
            _money.Scale = new Vector2(2, 2);
            _money.Path = "transparent";
            _money.LoadContent();

            _moneyIcon = new Image();
            _moneyIcon.Path = "ui/gameplay/money";
            _moneyIcon.Position = new Vector2(700, 5);
            _moneyIcon.LoadContent();
        }

       

        public override void UnloadContent()
        {
            base.UnloadContent();
            damageMod.UnloadContent();
            healthMod.UnloadContent();
            moneyMod.UnloadContent();
            _tip.UnloadContent();
            _money.UnloadContent();
            _moneyIcon.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (InputManager.Instance.KeyPressed(Keys.Space))
            {
                ScreenManager.Instance.ChangeScreens(RKOGame.Instance.LastScreen);
            }
            damageMod.Update(gameTime);
            healthMod.Update(gameTime);
            moneyMod.Update(gameTime);
            _tip.Update(gameTime);
            _money.Text = RKOGame.Instance.getCurrency.ToString();
            _money.Update(gameTime);
            _moneyIcon.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            damageMod.Draw(spriteBatch);
            healthMod.Draw(spriteBatch);
            moneyMod.Draw(spriteBatch);
            _tip.Draw(spriteBatch);
            _money.RedrawText(spriteBatch, Color.White);
            _moneyIcon.Draw(spriteBatch);
        }


        private void upgradeDamage(Object o, EventArgs e)
        {
            if (RKOGame.Instance.getCurrency >= 200)
            {
                Upgrade.DamageBoost += .25f;
                RKOGame.Instance.getCurrency = RKOGame.Instance.getCurrency - 200;
            }
        }

        private void upgradeHealth(Object o, EventArgs e)
        {
            if (RKOGame.Instance.getCurrency >= 500)
            {
                Upgrade.HealthIncrease += 25;
                RKOGame.Instance.getHealth += (int)Upgrade.HealthIncrease;
                RKOGame.Instance.getCurrency = RKOGame.Instance.getCurrency - 500;
            }
        }

        private void upgradeMoney(Object o, EventArgs e)
        {
            if (RKOGame.Instance.getCurrency >= 1000)
            {
                Upgrade.MoneyBoost += .25f;
                RKOGame.Instance.getCurrency = RKOGame.Instance.getCurrency - 1000;
            }
        }







    }
}
