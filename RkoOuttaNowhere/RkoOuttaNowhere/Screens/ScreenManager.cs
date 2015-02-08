// ScreenManager.cs
// James Tyson
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RkoOuttaNowhere.Images;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace RkoOuttaNowhere.Screens
{
    /// <summary>
    /// ScreenManager is a class to help facilitate screen transistions as well
    /// as manage the current screen
    /// </summary>
    public class ScreenManager
    {
        private static ScreenManager _instance;
        private GameScreen _currentScreen, _newScreen;
        private List<GameScreen> _screens;
        private Song _bgm;

        public Image _image;
        public Camera Camera;

        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }
        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;
        public bool IsTransitioning { get; private set; }

        /// <summary>
        /// Singleton class instance
        /// </summary>
        public static ScreenManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScreenManager();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ScreenManager()
        {
            Dimensions = new Vector2(1024, 768);
            Camera = new Camera();
            IsTransitioning = false;
            _screens = new List<GameScreen>();

            _image = new Image();
            _image.Path = "fade";
            _image.Scale = Dimensions;
            _image.Alpha = 0.5f;

            Initialize();
        }

        /// <summary>
        /// Initialize the screens
        /// </summary>
        public void Initialize()
        {
            // Create and add all of the screens
            _screens.Add(new SplashScreen());
            _screens.Add(new TitleScreen());
            _screens.Add(new LevelSelectScreen());
            _screens.Add(new GameplayScreen());
            _screens.Add(new UpgradeScreen());
            _screens.Add(new Vitcory());
            _screens.Add(new GameOverScreen());
            // Set the current screen to the splash screen
            _currentScreen = _screens[(int)ScreenType.Splash];
        }

        public void LoadAll()
        {
            _screens[(int)ScreenType.Title].LoadContent();
            _screens[(int)ScreenType.LevelSelect].LoadContent();
            _screens[(int)ScreenType.Gameplay].LoadContent();
            _screens[(int)ScreenType.Upgrade].LoadContent();
            _screens[(int)ScreenType.Victory].LoadContent();
            _screens[(int)ScreenType.GameOver].LoadContent();
        }

        public void ChangeFast(ScreenType type)
        {
            _currentScreen = _screens[(int)type];
        }

        /// <summary>
        /// Handles a screen changes
        /// </summary>
        /// <param name="screenName">name of the class to load</param>
        public void ChangeScreens(ScreenType type)
        {
            _newScreen = _screens[(int)type];
            _image.IsActive = true;
            _image.FadeEffect.Increase = true;
            _image.Alpha = 0.0f;
            _image.ActivateEffect("FadeEffect");
            IsTransitioning = true;
        }

        /// <summary>
        /// Handles a screen transition with a Fade Effect
        /// </summary>
        /// <param name="gameTime"></param>
        private void Transition(GameTime gameTime)
        {
            if (IsTransitioning)
            {
                _image.Update(gameTime);
                if (_image.Alpha == 1.0f)
                {
                    _currentScreen = _newScreen;
                    if (_currentScreen is GameplayScreen)
                    {
                        ((GameplayScreen)_currentScreen).Reload();
                    }
                }
                else if (_image.Alpha == 0.0f)
                {
                    _image.IsActive = false;
                    IsTransitioning = false;
                }
            }
        }

        /// <summary>
        /// Loads Content
        /// </summary>
        /// <param name="Content"></param>
        public void LoadContent(ContentManager Content)
        {
            this.Content = Content;
            _currentScreen.LoadContent();
            _image.LoadContent();

            //_bgm = Content.Load<Song>("sfx/bgm");
            //MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(_bgm);

            // Load And Play Music.
            try
            {
                SongCollection playlist = new SongCollection();
                playlist.Add(Content.Load<Song>("sfx/Robert del Naja - the shovel"));
                playlist.Add(Content.Load<Song>("sfx/Robert del Naja - HS"));
                playlist.Add(Content.Load<Song>("sfx/Robert del Naja - WS"));
                playlist.Add(Content.Load<Song>("sfx/Robert del Naja - DT3"));
                playlist.Add(Content.Load<Song>("sfx/Robert del Naja - BC"));
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(playlist);
            }
            catch (Exception e) { }
        }

        /// <summary>
        /// Unloads Content
        /// </summary>
        public void UnloadContent()
        {
            _currentScreen.UnloadContent();
            _image.UnloadContent();
        }

        /// <summary>
        /// Update the current screen, and if necessary updates the transition
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            _currentScreen.Update(gameTime);
            Transition(gameTime);

        }

        /// <summary>
        /// Draws the current screen, and if necessary draws the transitition
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _currentScreen.Draw(spriteBatch);
            if (IsTransitioning)
                _image.Draw(spriteBatch);
        }
    }
}

