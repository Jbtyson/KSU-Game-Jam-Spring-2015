using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RkoOuttaNowhere.Images;

namespace RkoOuttaNowhere.Ui
{
    public class LevelSelectGui : Gui
    {
        private List<Button> _nodes;
        private Action _nodeHandler;
        private int _currentLevel;
        private Image _tip;

        public LevelSelectGui()
            : base()
        {
            _nodes = new List<Button>();
            _tip = new Image();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _tip.Text = "Press U to go to upgrades, or select a level.";
            _tip.Position = new Vector2(5, 5);
            _tip.Path = "transparent";
            _tip.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            _tip.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (Button b in _nodes)
                b.Update(gameTime);

            _tip.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (Button b in _nodes)
                b.Draw(spriteBatch);

            _tip.Draw(spriteBatch);
        }

        public override void LoadNodes(List<Point> points, Action handler, string path)
        {
            _nodes.Clear();

            _nodeHandler = handler;

            int count = 0;
            foreach (Point p in points)
            {
                Button b = new Button();
                b.LoadContent(path, new Vector2(p.X, p.Y), HandleNodeClick);
                b.CenterImages();
                b.Images[0].ActivateEffect("SpriteSheetEffect");
                b.Images[0].SpriteSheetEffect.AmountOfFrames = new Vector2(2, 1);
                b.Images[0].SpriteSheetEffect.CurrentFrame = Vector2.Zero;
                b.Value = count++;
                b.HitBox = new Rectangle((int)b.Images[0].Position.X, (int)b.Images[0].Position.Y, 32, 32);
                _nodes.Add(b);
            }
        }

        public void HandleNodeClick(object sender, EventArgs e)
        {
            _numClicked = (int)((Button)(sender)).Value;
            _nodeHandler();
        }

        public override void AnimateButton(int index)
        {
            // Clear last image
            _nodes[_currentLevel].Images[0].IsActive = false;

            // Animate new one
            _currentLevel = index;
            _nodes[index].Images[0].IsActive = true;
            
        }
    }
}
