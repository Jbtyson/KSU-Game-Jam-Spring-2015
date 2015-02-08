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

        private int _damageCost, _moneyCost, _healthCost;

        private Button damageMod, healthMod, moneyMod;

        private Image _tip, _money, _moneyIcon;
        private Image _healthCostImg, _moneyCostImg, _damageCostImg;

        public UpgradeScreen()
            : base()
        {
            damageMod = new Button();
            healthMod = new Button();
            moneyMod = new Button();
            _tip = new Image();
            _money = new Image();
            _healthCostImg = new Image();
            _moneyCostImg = new Image();
            _damageCostImg = new Image();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _damageCost = 200;
            _healthCost = 500;
            _moneyCost = 1000;

            _backgroundImage.Path = "backgrounds/upgrade";
            _backgroundImage.LoadContent();
            damageMod.LoadContent("ui/upgrades/damage", new Vector2(100, 350), upgradeDamage);
            healthMod.LoadContent("ui/upgrades/health", new Vector2(100, 450), upgradeHealth);
            moneyMod.LoadContent("ui/upgrades/money", new Vector2(100, 550), upgradeMoney);

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

            _damageCostImg.Text = _damageCost.ToString();
            _damageCostImg.Position = new Vector2(475, 350);
            _damageCostImg.Scale = new Vector2(2, 2);
            _damageCostImg.Path = "transparent";
            _damageCostImg.LoadContent();
            
            _healthCostImg.Text = _healthCost.ToString();
            _healthCostImg.Position = new Vector2(475, 450);
            _healthCostImg.Scale = new Vector2(2, 2);
            _healthCostImg.Path = "transparent";
            _healthCostImg.LoadContent();

            _moneyCostImg.Text = _moneyCost.ToString();
            _moneyCostImg.Position = new Vector2(475, 550);
            _moneyCostImg.Scale = new Vector2(2, 2);
            _moneyCostImg.Path = "transparent";
            _moneyCostImg.LoadContent();
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

            _healthCostImg.Text = _healthCost.ToString();
            _healthCostImg.Update(gameTime);
            _moneyCostImg.Text = _moneyCost.ToString();
            _moneyCostImg.Update(gameTime);
            _damageCostImg.Text = _damageCost.ToString();
            _damageCostImg.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            damageMod.Draw(spriteBatch);
            healthMod.Draw(spriteBatch);
            moneyMod.Draw(spriteBatch);
            _tip.Draw(spriteBatch);

            _money.RedrawText(spriteBatch, Color.White);
            _healthCostImg.RedrawText(spriteBatch, Color.White);
            _moneyCostImg.RedrawText(spriteBatch, Color.White);
            _damageCostImg.RedrawText(spriteBatch, Color.White);

            _moneyIcon.Draw(spriteBatch);
        }


        private void upgradeDamage(Object o, EventArgs e)
        {
            if (RKOGame.Instance.getCurrency >= _damageCost)
            {
                Upgrade.DamageBoost += .25f;
                RKOGame.Instance.getCurrency = RKOGame.Instance.getCurrency - _damageCost;
                _damageCost += _damageCost / 10;
            }
        }

        private void upgradeHealth(Object o, EventArgs e)
        {
            if (RKOGame.Instance.getCurrency >= _healthCost)
            {
                Upgrade.HealthIncrease += 25;
                RKOGame.Instance.getHealth += (int)Upgrade.HealthIncrease;
                RKOGame.Instance.getCurrency = RKOGame.Instance.getCurrency - _healthCost;
                _healthCost += _healthCost / 10;
            }
        }

        private void upgradeMoney(Object o, EventArgs e)
        {
            if (RKOGame.Instance.getCurrency >= _moneyCost)
            {
                Upgrade.MoneyBoost += .25f;
                RKOGame.Instance.getCurrency = RKOGame.Instance.getCurrency - _moneyCost;
                _moneyCost += _moneyCost / 10;
            }
        }







    }
}
