using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MaliceRT {
    public class Main : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ShopKeeper  Shopkeeper;
        Level       Levelmanager;
        Menu        Menumanager;
        Editor      Editormanager;
        FileManager Filemanager;

        Song theme_grass;
        Song theme_vulcano;
        Song theme_sky;
        Song theme_desert;
        Song theme_beach;
        Song theme_snow;
        Song theme_mountain;
        Song theme_mashine;
        Song theme_forest;
        Song theme_aurora;
        Song theme_space;

        float core_screen_constant_width  = 0;
        float core_screen_constant_height = 0;
        float core_screen_scale_Width  = 0;
        float core_screen_scale_height = 0;

        string active_screen = "menu";

        public Main() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize() {
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
#if(!PLT_WINP)
            IsMouseVisible = true;
#endif
            Shopkeeper = new ShopKeeper();
            Filemanager = new FileManager();
            Filemanager.Initialize();
            core_screen_constant_width = Shopkeeper.gamescreen_width;
            core_screen_constant_height = Shopkeeper.gamescreen_height;
            core_screen_scale_Width = Window.ClientBounds.Width / core_screen_constant_width;
            core_screen_scale_height = Window.ClientBounds.Height / core_screen_constant_height;
            base.Initialize();
        }


        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Menumanager = new Menu(Content, GraphicsDevice, spriteBatch, Shopkeeper, Filemanager, core_screen_constant_width, core_screen_constant_height, core_screen_scale_Width, core_screen_scale_height);
            Levelmanager = new Level(Content, GraphicsDevice, spriteBatch, Shopkeeper, Filemanager, core_screen_constant_width, core_screen_constant_height, core_screen_scale_Width, core_screen_scale_height);
            Editormanager = new Editor(Content, GraphicsDevice, spriteBatch, Shopkeeper, Filemanager, core_screen_constant_width, core_screen_constant_height, core_screen_scale_Width, core_screen_scale_height);
            theme_grass = Content.Load<Song>(Shopkeeper.audio_music_grass);
            theme_vulcano = Content.Load<Song>(Shopkeeper.audio_music_vulcano);
            theme_sky = Content.Load<Song>(Shopkeeper.audio_music_sky);
            theme_desert = Content.Load<Song>(Shopkeeper.audio_music_desert);
            theme_beach = Content.Load<Song>(Shopkeeper.audio_music_beach);
            theme_snow = Content.Load<Song>(Shopkeeper.audio_music_snow);
            theme_mountain = Content.Load<Song>(Shopkeeper.audio_music_mountain);
            theme_mashine = Content.Load<Song>(Shopkeeper.audio_music_mashine);
            theme_forest = Content.Load<Song>(Shopkeeper.audio_music_forest);
            theme_aurora = Content.Load<Song>(Shopkeeper.audio_music_aurora);
            theme_space = Content.Load<Song>(Shopkeeper.audio_music_space);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.50f;
            JukeBox();
            Window.AllowUserResizing = true;
        }


        protected override void UnloadContent() {

        }



        protected override void Update(GameTime gameTime) {
            if(core_screen_scale_Width != Window.ClientBounds.Width / core_screen_constant_width || core_screen_scale_height != Window.ClientBounds.Height / core_screen_constant_height) {
                core_screen_scale_Width = Window.ClientBounds.Width / core_screen_constant_width;
                core_screen_scale_height = Window.ClientBounds.Height / core_screen_constant_height;
                Menumanager.Resize(core_screen_scale_Width, core_screen_scale_height);
                Levelmanager.Resize(core_screen_scale_Width, core_screen_scale_height);
                Editormanager.Resize(core_screen_scale_Width, core_screen_scale_height);
            }

            if(active_screen == "menu") {
                string call = Menumanager.Update(gameTime);
                if(call == "editor") {
                    active_screen = "editor";
                    Editormanager.Set_Act(Menumanager.Get_Level_Act());
                    Editormanager.Load();
                    JukeBox();
                }
                if(call == "level") {
                    active_screen = "level";
                    Levelmanager.Start(Menumanager.Get_Level_World(), Menumanager.Get_Level_Act());
                    JukeBox();
                }

            } else if(active_screen == "editor") {
                string call = Editormanager.Update(gameTime);
                if(call == "menu") {
                    active_screen = "menu";
                    JukeBox();
                }

            } else if(active_screen == "level") {
                string call = Levelmanager.Update(gameTime);
                if(call == "menu") {
                    active_screen = "menu";
                    Menumanager.Set_Score();
                    JukeBox();
                }
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if(active_screen == "menu") {
                Menumanager.Draw(gameTime);
                spriteBatch.Begin();
                spriteBatch.Draw(Menumanager.Get_RenderTarget(), new Rectangle(0, 0, (int)Window.ClientBounds.Width, (int)Window.ClientBounds.Height), Color.White);
                spriteBatch.End();
            } else if(active_screen == "editor") {
                Editormanager.Draw();
                spriteBatch.Begin();
                spriteBatch.Draw(Editormanager.Get_RenderTarget(), new Rectangle(0, 0, (int)Window.ClientBounds.Width, (int)Window.ClientBounds.Height), Color.White);
                spriteBatch.End();
            } else if(active_screen == "level") {
                Levelmanager.Draw();
                spriteBatch.Begin();
                spriteBatch.Draw(Levelmanager.Get_RenderTarget(), new Rectangle(0, 0, (int)Window.ClientBounds.Width, (int)Window.ClientBounds.Height), Color.White);
                spriteBatch.Draw(Levelmanager.Get_RenderTargetResult(), new Rectangle(0, 0, (int)Window.ClientBounds.Width, (int)Window.ClientBounds.Height), Color.White);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public void JukeBox() {
            MediaPlayer.Stop();
            if(active_screen == "menu") {
                System.Random random = new System.Random();
                int temp = random.Next(4);
                switch(temp) {
                    case 0: MediaPlayer.Play(theme_grass); break;
                    case 1: MediaPlayer.Play(theme_beach); break;
                    case 2: MediaPlayer.Play(theme_mountain); break;
                    case 3: MediaPlayer.Play(theme_forest); break;
                }
            } else if(active_screen == "editor") {

            } else if(active_screen == "level") {
                if(Levelmanager.Get_Music() == "grass") MediaPlayer.Play(theme_grass);
                if(Levelmanager.Get_Music() == "vulcano") MediaPlayer.Play(theme_vulcano);
                if(Levelmanager.Get_Music() == "sky") MediaPlayer.Play(theme_sky);
                if(Levelmanager.Get_Music() == "desert") MediaPlayer.Play(theme_desert);
                if(Levelmanager.Get_Music() == "beach") MediaPlayer.Play(theme_beach);
                if(Levelmanager.Get_Music() == "snow") MediaPlayer.Play(theme_snow);
                if(Levelmanager.Get_Music() == "mountain") MediaPlayer.Play(theme_mountain);
                if(Levelmanager.Get_Music() == "mashine") MediaPlayer.Play(theme_mashine);
                if(Levelmanager.Get_Music() == "forest") MediaPlayer.Play(theme_forest);
                if(Levelmanager.Get_Music() == "aurora") MediaPlayer.Play(theme_aurora);
                if(Levelmanager.Get_Music() == "space") MediaPlayer.Play(theme_space);
            }
        }

    }
}
