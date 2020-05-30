using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Audio;

namespace MaliceRT {
    class Editor {

        ContentManager content;
        GraphicsDevice graphics;
        SpriteBatch    spriteBatch;
        ShopKeeper     shopkeeper;
        FileManager    filemanager;

        Vector2 screensize;

        float core_screen_constant_width  = 0;
        float core_screen_constant_height = 0;
        float scaleX = 0;
        float scaleY = 0;

        int act;

        Vector2 renderDistanceField   = new Vector2( 106, 100);
        Vector2 renderDistancePalette = new Vector2(1141, 296);

        Texture2D tileset_grass;
        Texture2D tileset_vulcano;
        Texture2D tileset_sky;
        Texture2D tileset_desert;
        Texture2D tileset_beach;
        Texture2D tileset_snow;
        Texture2D tileset_mountain;
        Texture2D tileset_mashine;
        Texture2D tileset_forest;
        Texture2D tileset_aurora;
        Texture2D tileset_space;
        Texture2D tileset_event;
        Texture2D tileset_world;
        Texture2D texture_background;
        Texture2D texture_overlay1;
        Texture2D texture_overlay2;
        Texture2D texture_selector;
        Texture2D texture_pixel;
        Texture2D texture_shadow;

        SoundEffect coin;

        int active_tileset;
        string active_palette;
        string active_field;
        int active_music;
        int active_background0;
        int active_background1;
        int active_background2;
        int active_background3;

        bool active_transition;
        bool active_overlay;

        int transition;

        Vector2 grid_shift;
        Vector2 grid;

        SpriteFont font;

        Vector2[,] fieldfar    = new Vector2[320,40];
        Vector2[,] fieldmiddle = new Vector2[320,40];
        Vector2[,] fieldnear   = new Vector2[320,40];
        Vector2[,] fieldevent  = new Vector2[320,40];

        Vector2 background_far;
        Vector2 background_middle;
        Vector2 background_near;

        Vector2 selector_tileset;
        Vector2 selector_tileset_max;
        Vector2 selector_event;
        Vector2 selector_event_max;
        Vector2 selector_grid;
        Vector2 selector_overlay;
        Vector2 selector_overlay_max;
        Vector2 selector_overlay0;
        int selector_overlay1;
        int selector_overlay2;
        int selector_overlay3;
        int selector_overlay4;
        int selector_overlay5;
        int selector_overlay6;

        KeyboardState   control_keyboard_new;
        KeyboardState   control_keyboard_old;
        MouseState      control_mouse_new;
        MouseState      control_mouse_old;
        TouchCollection control_touch;

        RenderTarget2D renderTargetMain;
        RenderTarget2D renderTargetField;
        RenderTarget2D renderTargetPalette;

        public Editor(ContentManager _content, GraphicsDevice _graphics, SpriteBatch _spritebatch, ShopKeeper _shopkeeper, FileManager _filemanager, float _const_width, float _const_height, float _scale_width, float _scale_height) {
            content = _content;
            graphics = _graphics;
            spriteBatch = _spritebatch;
            shopkeeper = _shopkeeper;
            filemanager = _filemanager;
            screensize = new Vector2(shopkeeper.gamescreen_width, shopkeeper.gamescreen_height);
            core_screen_constant_width = _const_width;
            core_screen_constant_height = _const_height;
            scaleX = _scale_width;
            scaleY = _scale_height;
            coin = content.Load<SoundEffect>(shopkeeper.audio_sound_coin);
            Load_Content();
            TouchPanel.EnabledGestures = GestureType.Tap;
        }

        public void Resize(float x, float y) {
            scaleX = x;
            scaleY = y;
        }

        public void Load_Content() {
            renderTargetMain = new RenderTarget2D(graphics, (int)shopkeeper.gamescreen_width, (int)shopkeeper.gamescreen_height);
            renderTargetField = new RenderTarget2D(graphics, (int)shopkeeper.gamescreen_width, (int)shopkeeper.gamescreen_height);
            renderTargetPalette = new RenderTarget2D(graphics, (int)shopkeeper.gamescreen_width, (int)shopkeeper.gamescreen_height);
            texture_background = content.Load<Texture2D>(shopkeeper.texture_editor_background);
            texture_overlay1 = content.Load<Texture2D>(shopkeeper.texture_editor_overlay1);
            texture_overlay2 = content.Load<Texture2D>(shopkeeper.texture_editor_overlay2);
            texture_selector = content.Load<Texture2D>(shopkeeper.texture_editor_selector);
            texture_pixel = content.Load<Texture2D>(shopkeeper.texture_editor_pixel);
            texture_shadow = content.Load<Texture2D>(shopkeeper.texture_editor_shadow);
            tileset_grass = content.Load<Texture2D>(shopkeeper.texture_tileset_grass);
            tileset_vulcano = content.Load<Texture2D>(shopkeeper.texture_tileset_vulcano);
            tileset_sky = content.Load<Texture2D>(shopkeeper.texture_tileset_sky);
            tileset_desert = content.Load<Texture2D>(shopkeeper.texture_tileset_desert);
            tileset_beach = content.Load<Texture2D>(shopkeeper.texture_tileset_beach);
            tileset_snow = content.Load<Texture2D>(shopkeeper.texture_tileset_snow);
            tileset_mountain = content.Load<Texture2D>(shopkeeper.texture_tileset_mountain);
            tileset_mashine = content.Load<Texture2D>(shopkeeper.texture_tileset_mashine);
            tileset_forest = content.Load<Texture2D>(shopkeeper.texture_tileset_forest);
            tileset_aurora = content.Load<Texture2D>(shopkeeper.texture_tileset_aurora);
            tileset_space = content.Load<Texture2D>(shopkeeper.texture_tileset_space);
            tileset_event = content.Load<Texture2D>(shopkeeper.texture_tileset_event);
            tileset_world = content.Load<Texture2D>(shopkeeper.texture_tileset_world);
            font = content.Load<SpriteFont>(shopkeeper.font_score);
            act = 1;
            active_tileset = 0;
            active_palette = "tileset";
            active_field = "middle";
            active_music = 0;
            active_background0 = 0;
            active_background1 = 0;
            active_background2 = 0;
            active_background3 = 0;
            background_far = new Vector2(0, 0);
            background_middle = new Vector2(0, 0);
            background_near = new Vector2(0, 0);
            selector_tileset = new Vector2(0, 0);
            selector_event = new Vector2(0, 0);
            selector_tileset_max = new Vector2(19, 29);
            selector_event_max = new Vector2(10, 4);
            selector_grid = new Vector2(0, 0);
            selector_overlay = new Vector2(0, 0);
            selector_overlay_max = new Vector2(1, 2);
            selector_overlay1 = 0;
            selector_overlay2 = 0;
            selector_overlay3 = 0;
            selector_overlay4 = 0;
            selector_overlay5 = 0;
            selector_overlay6 = 0;
            grid = new Vector2(320, 40);
            grid_shift = new Vector2(0, 0);
            Reset_Field();
            Create_Field();
            Create_Palette();
            transition = 0;
            active_transition = false;
            active_overlay = false;
        }

        public void Set_Act(int i) {
            act = i;
        }

        public void New() {
            active_tileset = 0;
            active_palette = "tileset";
            active_field = "middle";
            active_music = 0;
            active_background0 = 0;
            active_background1 = 0;
            active_background2 = 0;
            active_background3 = 0;
            selector_overlay1 = 0;
            selector_overlay2 = 0;
            selector_overlay3 = 0;
            selector_overlay4 = 0;
            selector_overlay5 = 0;
            selector_overlay6 = 0;
            grid_shift = new Vector2(0, 0);
            Reset_Field();
            Create_Field();
            Create_Palette();
            transition = 0;
            active_transition = false;
            active_overlay = false;
        }

        public void Load() {
            List<string> Row = new List<string>();
            string[] temp = filemanager.LoadList(act);
            for(int i = 0; i < 166; i++) { Row.Add(temp[i]); }
            active_tileset = int.Parse(Row[0]); selector_overlay1 = int.Parse(Row[0]); Row.RemoveAt(0);
            active_music = int.Parse(Row[0]); selector_overlay2 = int.Parse(Row[0]); Row.RemoveAt(0);
            active_background0 = int.Parse(Row[0]); selector_overlay3 = int.Parse(Row[0]); Row.RemoveAt(0);
            active_background1 = int.Parse(Row[0]); selector_overlay4 = int.Parse(Row[0]); Row.RemoveAt(0);
            active_background2 = int.Parse(Row[0]); selector_overlay5 = int.Parse(Row[0]); Row.RemoveAt(0);
            active_background3 = int.Parse(Row[0]); selector_overlay6 = int.Parse(Row[0]); Row.RemoveAt(0);
            for(int y = 0; y < 40; y++) { for(int x = 0; x < 320; x++) { fieldfar[x, y] = new Vector2(int.Parse(Row[0].Substring(0 + 5 * x + 2, 2)), int.Parse(Row[0].Substring(0 + 5 * x, 2))); } Row.RemoveAt(0); }
            for(int y = 0; y < 40; y++) { for(int x = 0; x < 320; x++) { fieldmiddle[x, y] = new Vector2(int.Parse(Row[0].Substring(0 + 5 * x + 2, 2)), int.Parse(Row[0].Substring(0 + 5 * x, 2))); } Row.RemoveAt(0); }
            for(int y = 0; y < 40; y++) { for(int x = 0; x < 320; x++) { fieldnear[x, y] = new Vector2(int.Parse(Row[0].Substring(0 + 5 * x + 2, 2)), int.Parse(Row[0].Substring(0 + 5 * x, 2))); } Row.RemoveAt(0); }
            for(int y = 0; y < 40; y++) { for(int x = 0; x < 320; x++) { fieldevent[x, y] = new Vector2(int.Parse(Row[0].Substring(0 + 5 * x + 2, 2)), int.Parse(Row[0].Substring(0 + 5 * x, 2))); } Row.RemoveAt(0); }
            Create_Field();
        }

        public void Save() {
            List<string> list = new List<string>();
            list.Add("" + active_tileset); Debug.WriteLine("" + active_tileset);
            list.Add("" + active_music); Debug.WriteLine("" + active_music);
            list.Add("" + active_background0); Debug.WriteLine("" + active_background0);
            list.Add("" + active_background1); Debug.WriteLine("" + active_background1);
            list.Add("" + active_background2); Debug.WriteLine("" + active_background2);
            list.Add("" + active_background3); Debug.WriteLine("" + active_background3);
            for(int y = 0; y < 40; y++) { string s = ""; for(int x = 0; x < 320; x++) { s = s.Insert(s.Length, Parse(fieldfar[x, y].Y, fieldfar[x, y].X)); } list.Add(s); Debug.WriteLine(s); }
            for(int y = 0; y < 40; y++) { string s = ""; for(int x = 0; x < 320; x++) { s = s.Insert(s.Length, Parse(fieldmiddle[x, y].Y, fieldmiddle[x, y].X)); } list.Add(s); Debug.WriteLine(s); }
            for(int y = 0; y < 40; y++) { string s = ""; for(int x = 0; x < 320; x++) { s = s.Insert(s.Length, Parse(fieldnear[x, y].Y, fieldnear[x, y].X)); } list.Add(s); Debug.WriteLine(s); }
            for(int y = 0; y < 40; y++) { string s = ""; for(int x = 0; x < 320; x++) { s = s.Insert(s.Length, Parse(fieldevent[x, y].Y, fieldevent[x, y].X)); } list.Add(s); Debug.WriteLine(s); }
            filemanager.SaveWorld(list, act);
            filemanager.SaveList(list, act);
            filemanager.Reset_Highscore("editor", act);
            Debug.WriteLine("STOP");
            coin.Play();
        }

        private string Parse(float i, float j) {
            string s = "";
            if(i < 10 && j < 10) {
                s = "0" + i + "0" + j + ":";
            } else if(i < 10 && j >= 10) {
                s = "0" + i + "" + j + ":";
            } else if(i >= 10 && j < 10) {
                s = "" + i + "0" + j + ":";
            } else if(i >= 10 && j >= 10) {
                s = "" + i + "" + j + ":";
            }
            return s;
        }

        private void Reset_Field() {
            fieldfar = new Vector2[320, 40];
            fieldmiddle = new Vector2[320, 40];
            fieldnear = new Vector2[320, 40];
            fieldevent = new Vector2[320, 40];
            for(int yi = 0; yi < 40; yi++) {
                for(int xi = 0; xi < 320; xi++) {
                    fieldfar[xi, yi] = new Vector2(0, 0);
                    fieldmiddle[xi, yi] = new Vector2(0, 0);
                    fieldnear[xi, yi] = new Vector2(0, 0);
                    fieldevent[xi, yi] = new Vector2(0, 0);
                }
            }
        }

        public void Unload_Content() {

        }

        public string Update(GameTime gameTime) {
            control_keyboard_new = Keyboard.GetState();
            control_mouse_new = Mouse.GetState();
            control_touch = TouchPanel.GetState();

            string temp = "void";

            if(active_transition) {
                if(active_overlay) {
                    transition = transition - 5;
                    if(transition == 0) {
                        if(active_palette != "tileset" && active_palette != "event") {
                            active_palette = "tileset";
                        }
                        Create_Field();
                        Create_Palette();
                        active_overlay = false;
                        active_transition = false;
                    }
                } else {
                    transition = transition + 5;
                    if(transition == 100) {
                        active_overlay = true;
                        active_transition = false;
                    }
                }
            } else if(active_overlay) {
                if(control_mouse_new.ScrollWheelValue != control_mouse_old.ScrollWheelValue) {
                    Command_Mousewheel(control_mouse_new.ScrollWheelValue < control_mouse_old.ScrollWheelValue);
                }
                if(control_keyboard_new.IsKeyDown(Keys.Enter) && control_keyboard_old.IsKeyUp(Keys.Enter)) {
                    if(active_palette == "tileset") {
                        selector_tileset = new Vector2(selector_overlay0.X + 10 * selector_overlay.X, selector_overlay0.Y + 10 * selector_overlay.Y);
                    } else if(active_palette == "event") {
                        selector_event = selector_overlay0;
                    } else {
                        active_tileset = selector_overlay1;
                        active_music = selector_overlay2;
                        active_background0 = selector_overlay3;
                        active_background1 = selector_overlay4;
                        active_background2 = selector_overlay5;
                        active_background3 = selector_overlay6;
                    }
                    active_transition = true;
                }
                if(control_mouse_new.MiddleButton == ButtonState.Pressed && control_mouse_old.MiddleButton == ButtonState.Released) {
                    if(active_palette == "tileset") {
                        selector_tileset = new Vector2(selector_overlay0.X + 10 * selector_overlay.X, selector_overlay0.Y + 10 * selector_overlay.Y);
                    } else if(active_palette == "event") {
                        selector_event = selector_overlay0;
                    } else {
                        active_tileset = selector_overlay1;
                        active_music = selector_overlay2;
                        active_background0 = selector_overlay3;
                        active_background1 = selector_overlay4;
                        active_background2 = selector_overlay5;
                        active_background3 = selector_overlay6;
                    }
                    active_transition = true;
                }
                if(control_mouse_new.LeftButton == ButtonState.Pressed && control_mouse_old.LeftButton == ButtonState.Released) {
                    temp = Command_Cursor(control_mouse_new.Position.X / scaleX, control_mouse_new.Position.Y / scaleY, "click");
                    if(temp == "menu") return "menu";
                }
                while(TouchPanel.IsGestureAvailable) {
                    var gesture = TouchPanel.ReadGesture();
                    if(gesture.GestureType == GestureType.Tap) {
                        Command_Cursor(gesture.Position.X, gesture.Position.Y, "touch");
                    }
                }
            } else {
                if(control_mouse_new.MiddleButton == ButtonState.Pressed && control_mouse_old.MiddleButton == ButtonState.Released) { active_transition = true; }
                if(control_keyboard_new.IsKeyDown(Keys.Enter) && control_keyboard_old.IsKeyUp(Keys.Enter)) { active_transition = true; }
                if(control_keyboard_new.IsKeyDown(Keys.Up) && control_keyboard_old.IsKeyUp(Keys.Up)) { Command_Grid_Shift("up"); }
                if(control_keyboard_new.IsKeyDown(Keys.Down) && control_keyboard_old.IsKeyUp(Keys.Down)) { Command_Grid_Shift("down"); }
                if(control_keyboard_new.IsKeyDown(Keys.Left) && control_keyboard_old.IsKeyUp(Keys.Left)) { Command_Grid_Shift("left"); }
                if(control_keyboard_new.IsKeyDown(Keys.Right) && control_keyboard_old.IsKeyUp(Keys.Right)) { Command_Grid_Shift("right"); }

                if(control_mouse_new.LeftButton == ButtonState.Released && control_mouse_old.LeftButton == ButtonState.Released) {
                    temp = Command_Cursor(control_mouse_new.Position.X / scaleX, control_mouse_new.Position.Y / scaleY, "hover");
                }

                if(control_mouse_new.LeftButton == ButtonState.Pressed && control_mouse_old.LeftButton == ButtonState.Released) {
                    temp = Command_Cursor(control_mouse_new.Position.X / scaleX, control_mouse_new.Position.Y / scaleY, "click");
                    if(temp == "menu") return "menu";
                }

                if(control_mouse_new.RightButton == ButtonState.Pressed && control_mouse_old.RightButton == ButtonState.Released) {
                    temp = Command_Cursor(control_mouse_new.Position.X / scaleX, control_mouse_new.Position.Y / scaleY, "rightclick");
                }

                if(control_mouse_new.LeftButton == ButtonState.Pressed) {
                    temp = Command_Cursor(control_mouse_new.Position.X / scaleX, control_mouse_new.Position.Y / scaleY, "drag");
                }

                if(control_mouse_new.ScrollWheelValue != control_mouse_old.ScrollWheelValue) {
                    Command_Mousewheel(control_mouse_new.ScrollWheelValue < control_mouse_old.ScrollWheelValue);
                }

                while(TouchPanel.IsGestureAvailable) {
                    var gesture = TouchPanel.ReadGesture();
                    if(gesture.GestureType == GestureType.Tap) {
                        Command_Cursor(gesture.Position.X, gesture.Position.Y, "touch");
                    }
                }
            }

            control_keyboard_old = control_keyboard_new;
            control_mouse_old = control_mouse_new;

            return temp;
        }

        public void Draw() {
            graphics.SetRenderTarget(renderTargetMain);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            graphics.Clear(Color.CornflowerBlue);

            spriteBatch.Draw(texture_background, new Rectangle(0, 0, texture_background.Width, texture_background.Height), Color.White);

            spriteBatch.Draw(renderTargetField, new Rectangle(0, 0, (int)screensize.X, (int)screensize.Y), Color.White);
            spriteBatch.Draw(renderTargetPalette, new Rectangle(0, 0, (int)screensize.X, (int)screensize.Y), Color.White);

            if(active_palette == "tileset" || active_palette == "event")
                spriteBatch.Draw(texture_selector, new Rectangle((int)(renderDistanceField.X + selector_grid.X * 32), (int)(renderDistanceField.Y + selector_grid.Y * 32), 32, 32), Color.White);

            if(active_palette == "tileset") {
                if(active_field == "far") spriteBatch.Draw(texture_pixel, new Rectangle(880, 645, 50, 50), Color.White);
                if(active_field == "middle") spriteBatch.Draw(texture_pixel, new Rectangle(955, 645, 50, 50), Color.White);
                if(active_field == "near") spriteBatch.Draw(texture_pixel, new Rectangle(1030, 645, 50, 50), Color.White);
            }

            spriteBatch.Draw(texture_shadow, new Rectangle(0, 0, texture_shadow.Width, texture_shadow.Height), Color.White);

            if(active_palette == "tileset") {
                spriteBatch.Draw(texture_overlay1, new Rectangle(0, 0, texture_overlay1.Width, texture_overlay1.Height), Color.White * (transition / 100));
                int j = (int)selector_overlay.X;
                int i = (int)selector_overlay.Y;
                spriteBatch.Draw(Get_Tileset(), new Vector2(315, 35), new Rectangle(0 + 330 * j, 0 + 330 * i, 330, 330), Color.White * (transition / 100), 0.0f, new Vector2(0, 0), 2F, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(texture_pixel, new Rectangle(315 + 1 + 65 * (int)selector_overlay0.X, 35 + 1 + 65 * (int)selector_overlay0.Y, 64, 64), Color.Yellow * (transition / 100));
            } else if(active_palette == "event") {
                spriteBatch.Draw(texture_overlay2, new Rectangle(0, 0, texture_overlay2.Width, texture_overlay2.Height), Color.White * (transition / 100));
                spriteBatch.Draw(tileset_event, new Vector2(251, 35), new Rectangle(0, 0, tileset_event.Width, tileset_event.Height), Color.White * (transition / 100), 0.0f, new Vector2(0, 0), 1.0F, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(texture_pixel, new Rectangle(251 + 1 + 65 * (int)selector_overlay0.X, 35 + 1 + 65 * (int)selector_overlay0.Y, 64, 64), Color.Yellow * (transition / 100));
            } else {
                spriteBatch.Draw(texture_overlay2, new Rectangle(0, 0, texture_overlay2.Width, texture_overlay2.Height), Color.White * (transition / 100));
                int j = (int)selector_overlay.X;
                int i = (int)selector_overlay.Y;
                spriteBatch.Draw(tileset_world, new Vector2(251, 35), new Rectangle(0, 0, tileset_world.Width, tileset_world.Height), Color.White * (transition / 100), 0.0f, new Vector2(0, 0), 1.0F, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(texture_pixel, new Rectangle(251 + 1 + 65 * selector_overlay1, 35 + 1 + 65 * 0, 64, 64), Color.Yellow * (transition / 100));
                spriteBatch.Draw(texture_pixel, new Rectangle(251 + 1 + 65 * selector_overlay2, 35 + 1 + 65 * 1, 64, 64), Color.Yellow * (transition / 100));
                spriteBatch.Draw(texture_pixel, new Rectangle(251 + 1 + 65 * selector_overlay3, 35 + 1 + 65 * 2, 64, 64), Color.Yellow * (transition / 100));
                spriteBatch.Draw(texture_pixel, new Rectangle(251 + 1 + 65 * selector_overlay4, 35 + 1 + 65 * 3, 64, 64), Color.Yellow * (transition / 100));
                spriteBatch.Draw(texture_pixel, new Rectangle(251 + 1 + 65 * selector_overlay5, 35 + 1 + 65 * 4, 64, 64), Color.Yellow * (transition / 100));
                spriteBatch.Draw(texture_pixel, new Rectangle(251 + 1 + 65 * selector_overlay6, 35 + 1 + 65 * 5, 64, 64), Color.Yellow * (transition / 100));
            }

            spriteBatch.End();
            graphics.SetRenderTarget(null);
        }

        private Texture2D Get_Tileset() {
            if(active_palette == "tileset") {
                if(active_tileset == 0) return tileset_grass;
                if(active_tileset == 1) return tileset_vulcano;
                if(active_tileset == 2) return tileset_sky;
                if(active_tileset == 3) return tileset_desert;
                if(active_tileset == 4) return tileset_beach;
                if(active_tileset == 5) return tileset_snow;
                if(active_tileset == 6) return tileset_mountain;
                if(active_tileset == 7) return tileset_mashine;
                if(active_tileset == 8) return tileset_forest;
                if(active_tileset == 9) return tileset_aurora;
                if(active_tileset == 10) return tileset_space;
            }
            if(active_palette == "event") return tileset_event;
            if(active_palette == "world") return tileset_world;
            return tileset_grass;
        }

        public RenderTarget2D Get_RenderTarget() {
            return renderTargetMain;
        }

        private void Create_Field() {
            Texture2D tempset = tileset_grass;
            graphics.SetRenderTarget(renderTargetField);
            if(active_tileset == 0) tempset = tileset_grass;
            if(active_tileset == 1) tempset = tileset_vulcano;
            if(active_tileset == 2) tempset = tileset_sky;
            if(active_tileset == 3) tempset = tileset_desert;
            if(active_tileset == 4) tempset = tileset_beach;
            if(active_tileset == 5) tempset = tileset_snow;
            if(active_tileset == 6) tempset = tileset_mountain;
            if(active_tileset == 7) tempset = tileset_mashine;
            if(active_tileset == 8) tempset = tileset_forest;
            if(active_tileset == 9) tempset = tileset_aurora;
            if(active_tileset == 10) tempset = tileset_space;
            spriteBatch.Begin();
            graphics.Clear(Color.Transparent);
            for(int i = 0; i < 16; i++) {
                for(int j = 0; j < 32; j++) {
                    spriteBatch.Draw(tempset, new Vector2(renderDistanceField.X + j * 32, renderDistanceField.Y + i * 32), new Rectangle(1 + (int)((1 + 32) * fieldfar[j + (int)grid_shift.X, i + (int)grid_shift.Y].X), 1 + (int)((1 + 32) * fieldfar[j + (int)grid_shift.X, i + (int)grid_shift.Y].Y), 32, 32), Color.White, 0.0f, new Vector2(0, 0), 1F, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(tempset, new Vector2(renderDistanceField.X + j * 32, renderDistanceField.Y + i * 32), new Rectangle(1 + (int)((1 + 32) * fieldmiddle[j + (int)grid_shift.X, i + (int)grid_shift.Y].X), 1 + (int)((1 + 32) * fieldmiddle[j + (int)grid_shift.X, i + (int)grid_shift.Y].Y), 32, 32), Color.White, 0.0f, new Vector2(0, 0), 1F, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(tempset, new Vector2(renderDistanceField.X + j * 32, renderDistanceField.Y + i * 32), new Rectangle(1 + (int)((1 + 32) * fieldnear[j + (int)grid_shift.X, i + (int)grid_shift.Y].X), 1 + (int)((1 + 32) * fieldnear[j + (int)grid_shift.X, i + (int)grid_shift.Y].Y), 32, 32), Color.White, 0.0f, new Vector2(0, 0), 1F, SpriteEffects.None, 0.0f);
                }
            }
            for(int i = 0; i < 16; i++) {
                for(int j = 0; j < 32; j++) {
                    spriteBatch.Draw(tileset_event, new Vector2(renderDistanceField.X + j * 32, renderDistanceField.Y + i * 32), new Rectangle(1 + (int)((1 + 64) * fieldevent[j + (int)grid_shift.X, i + (int)grid_shift.Y].X), 1 + (int)((1 + 64) * fieldevent[j + (int)grid_shift.X, i + (int)grid_shift.Y].Y), 64, 64), Color.White, 0.0f, new Vector2(0, 0), 1F, SpriteEffects.None, 0.0f);
                }
            }
            spriteBatch.End();
            graphics.SetRenderTarget(null);
        }

        private void Create_Palette() {
            Texture2D tempset    = tileset_grass;
            Vector2 selector     = new Vector2(0,0);
            Vector2 selector_max = new Vector2(0,0);
            graphics.SetRenderTarget(renderTargetPalette);
            if(active_palette == "tileset") {
                if(active_tileset == 0) tempset = tileset_grass;
                if(active_tileset == 1) tempset = tileset_vulcano;
                if(active_tileset == 2) tempset = tileset_sky;
                if(active_tileset == 3) tempset = tileset_desert;
                if(active_tileset == 4) tempset = tileset_beach;
                if(active_tileset == 5) tempset = tileset_snow;
                if(active_tileset == 6) tempset = tileset_mountain;
                if(active_tileset == 7) tempset = tileset_mashine;
                if(active_tileset == 8) tempset = tileset_forest;
                if(active_tileset == 9) tempset = tileset_aurora;
                if(active_tileset == 10) tempset = tileset_space;
                selector = selector_tileset;
                selector_max = selector_tileset_max;
            }
            if(active_palette == "event") {
                tempset = tileset_event;
                selector = selector_event;
                selector_max = selector_event_max;
            }
            int x = (int)selector.X;
            int y = (int)selector.Y;
            spriteBatch.Begin();
            graphics.Clear(Color.Transparent);
            spriteBatch.Draw(tempset, new Vector2(renderDistancePalette.X, renderDistancePalette.Y), new Rectangle(1 + (int)((1 + 32) * x), 1 + (int)((1 + 32) * y), 32, 32), Color.White, 0.0f, new Vector2(0, 0), 4F, SpriteEffects.None, 0.0f);

            x--; if(x < 0) { x = (int)selector_max.X; y--; if(y < 0) y = (int)selector_max.Y; }
            spriteBatch.Draw(tempset, new Vector2(renderDistancePalette.X, renderDistancePalette.Y - 130 * 1), new Rectangle(1 + (int)((1 + 32) * x), 1 + (int)((1 + 32) * y), 32, 32), Color.White, 0.0f, new Vector2(0, 0), 4F, SpriteEffects.None, 0.0f);
            x--; if(x < 0) { x = (int)selector_max.X; y--; if(y < 0) y = (int)selector_max.Y; }
            spriteBatch.Draw(tempset, new Vector2(renderDistancePalette.X, renderDistancePalette.Y - 130 * 2), new Rectangle(1 + (int)((1 + 32) * x), 1 + (int)((1 + 32) * y), 32, 32), Color.White, 0.0f, new Vector2(0, 0), 4F, SpriteEffects.None, 0.0f);
            x--; if(x < 0) { x = (int)selector_max.X; y--; if(y < 0) y = (int)selector_max.Y; }
            spriteBatch.Draw(tempset, new Vector2(renderDistancePalette.X, renderDistancePalette.Y - 130 * 3), new Rectangle(1 + (int)((1 + 32) * x), 1 + (int)((1 + 32) * y), 32, 32), Color.White, 0.0f, new Vector2(0, 0), 4F, SpriteEffects.None, 0.0f);

            x = (int)selector.X;
            y = (int)selector.Y;
            x++; if(x > selector_max.X) { x = 0; y++; if(y > selector_max.Y) y = 0; }
            spriteBatch.Draw(tempset, new Vector2(renderDistancePalette.X, renderDistancePalette.Y + 130 * 1), new Rectangle(1 + (int)((1 + 32) * x), 1 + (int)((1 + 32) * y), 32, 32), Color.White, 0.0f, new Vector2(0, 0), 4F, SpriteEffects.None, 0.0f);
            x++; if(x > selector_max.X) { x = 0; y++; if(y > selector_max.Y) y = 0; }
            spriteBatch.Draw(tempset, new Vector2(renderDistancePalette.X, renderDistancePalette.Y + 130 * 2), new Rectangle(1 + (int)((1 + 32) * x), 1 + (int)((1 + 32) * y), 32, 32), Color.White, 0.0f, new Vector2(0, 0), 4F, SpriteEffects.None, 0.0f);
            x++; if(x > selector_max.X) { x = 0; y++; if(y < selector_max.Y) y = 0; }
            spriteBatch.Draw(tempset, new Vector2(renderDistancePalette.X, renderDistancePalette.Y + 130 * 3), new Rectangle(1 + (int)((1 + 32) * x), 1 + (int)((1 + 32) * y), 32, 32), Color.White, 0.0f, new Vector2(0, 0), 4F, SpriteEffects.None, 0.0f);


            spriteBatch.End();
            graphics.SetRenderTarget(null);
        }

        private string Command_Cursor(float cursorX, float cursorY, string command) {
            if(command == "void") {

            } else {
                if(command == "hover") {
                    int gridX = (int)renderDistanceField.X;
                    int gridY = (int)renderDistanceField.Y;
                    for(int y = 0; y < 16; y++) {
                        bool temp = false;
                        for(int x = 0; x < 32; x++) {
                            if(gridX + x * 32 < cursorX && cursorX < gridX + x * 32 + 32 && gridY + y * 32 < cursorY && cursorY < gridY + y * 32 + 32) {
                                selector_grid = new Vector2(x, y);
                                temp = true;
                                break;
                            }
                        }
                        if(temp) break;
                    }
                }
                if(command == "drag") {
                    if(renderDistanceField.X < cursorX && cursorX < renderDistanceField.X + 32 * 32 && renderDistanceField.Y < cursorY && cursorY < renderDistanceField.Y + 32 * 16) {
                        int gridX = (int)renderDistanceField.X;
                        int gridY = (int)renderDistanceField.Y;
                        for(int y = 0; y < 16; y++) {
                            bool temp = false;
                            for(int x = 0; x < 32; x++) {
                                if(gridX + x * 32 < cursorX && cursorX < gridX + x * 32 + 32 && gridY + y * 32 < cursorY && cursorY < gridY + y * 32 + 32) {
                                    selector_grid = new Vector2(x, y);
                                    temp = true;
                                    break;
                                }
                            }
                            if(temp) break;
                        }
                        if(active_palette == "tileset" || active_palette == "event") {
                            gridX = (int)renderDistanceField.X;
                            gridY = (int)renderDistanceField.Y;
                            for(int y = 0; y < 16; y++) {
                                bool temp = false;
                                for(int x = 0; x < 32; x++) {
                                    if(gridX + x * 32 < cursorX && cursorX < gridX + x * 32 + 32 && gridY + y * 32 < cursorY && cursorY < gridY + y * 32 + 32) {
                                        if(active_palette == "tileset") {
                                            if(active_field == "far") fieldfar[x + (int)grid_shift.X, y + (int)grid_shift.Y] = selector_tileset;
                                            if(active_field == "middle") fieldmiddle[x + (int)grid_shift.X, y + (int)grid_shift.Y] = selector_tileset;
                                            if(active_field == "near") fieldnear[x + (int)grid_shift.X, y + (int)grid_shift.Y] = selector_tileset;
                                        }
                                        if(active_palette == "event") {
                                            fieldevent[x + (int)grid_shift.X, y + (int)grid_shift.Y] = selector_event;
                                        }
                                        Create_Field();
                                        temp = true;
                                        break;
                                    }
                                }
                                if(temp) break;
                            }
                        }
                    }
                }
                if(command == "rightclick") {
                    if(!active_overlay) {
                        int gridX = (int)renderDistanceField.X;
                        int gridY = (int)renderDistanceField.Y;
                        for(int y = 0; y < 16; y++) {
                            bool temp = false;
                            for(int x = 0; x < 32; x++) {
                                if(gridX + x * 32 < cursorX && cursorX < gridX + x * 32 + 32 && gridY + y * 32 < cursorY && cursorY < gridY + y * 32 + 32) {
                                    if(active_palette == "tileset") {
                                        if(active_field == "far") selector_tileset = fieldfar[x + (int)grid_shift.X, y + (int)grid_shift.Y];
                                        if(active_field == "middle") selector_tileset = fieldmiddle[x + (int)grid_shift.X, y + (int)grid_shift.Y];
                                        if(active_field == "near") selector_tileset = fieldnear[x + (int)grid_shift.X, y + (int)grid_shift.Y];
                                    }
                                    if(active_palette == "event") {
                                        selector_event = fieldevent[x, y];
                                    }
                                    Create_Palette();
                                    temp = true;
                                    break;
                                }
                            }
                            if(temp) break;
                        }
                    }
                }
                if(command == "click" || command == "touch") {
                    if(active_overlay) {
                        if(active_palette == "tileset") {
                            if(315 < cursorX && cursorX < 965 && 35 < cursorY && cursorY < 685) { // Overlay 1
                                for(int y = 0; y < 10; y++) {
                                    for(int x = 0; x < 10; x++) {
                                        if(315 + 1 + 65 * x < cursorX && cursorX < 315 + 1 + 64 + 65 * x && 35 + 1 + 65 * y < cursorY && cursorY < 35 + 1 + 64 + 65 * y) {
                                            selector_overlay0 = new Vector2(x, y);
                                        }
                                    }
                                }
                            }
                        } else if(active_palette == "event") {
                            if(251 < cursorX && cursorX < 965 && 35 < cursorY && cursorY < 685) { // Overlay 2
                                for(int y = 0; y < 5; y++) {
                                    for(int x = 0; x < 11; x++) {
                                        if(251 + 1 + 65 * x < cursorX && cursorX < 251 + 1 + 64 + 65 * x && 35 + 1 + 65 * y < cursorY && cursorY < 35 + 1 + 64 + 65 * y) {
                                            selector_overlay0 = new Vector2(x, y);
                                        }
                                    }
                                }
                            }
                        } else {
                            if(251 < cursorX && cursorX < 965 && 35 < cursorY && cursorY < 685) { // Overlay 2
                                for(int x = 0; x < 11; x++) {
                                    if(251 + 1 + 65 * x < cursorX && cursorX < 251 + 1 + 64 + 65 * x && 35 + 1 + 65 * 0 < cursorY && cursorY < 35 + 1 + 64 + 65 * 0) { selector_overlay1 = x; }
                                    if(251 + 1 + 65 * x < cursorX && cursorX < 251 + 1 + 64 + 65 * x && 35 + 1 + 65 * 1 < cursorY && cursorY < 35 + 1 + 64 + 65 * 1) { selector_overlay2 = x; }
                                    if(251 + 1 + 65 * x < cursorX && cursorX < 251 + 1 + 64 + 65 * x && 35 + 1 + 65 * 2 < cursorY && cursorY < 35 + 1 + 64 + 65 * 2) { selector_overlay3 = x; }
                                    if(251 + 1 + 65 * x < cursorX && cursorX < 251 + 1 + 64 + 65 * x && 35 + 1 + 65 * 3 < cursorY && cursorY < 35 + 1 + 64 + 65 * 3) { selector_overlay4 = x; }
                                    if(251 + 1 + 65 * x < cursorX && cursorX < 251 + 1 + 64 + 65 * x && 35 + 1 + 65 * 4 < cursorY && cursorY < 35 + 1 + 64 + 65 * 4) { selector_overlay5 = x; }
                                    if(251 + 1 + 65 * x < cursorX && cursorX < 251 + 1 + 64 + 65 * x && 35 + 1 + 65 * 5 < cursorY && cursorY < 35 + 1 + 64 + 65 * 5) { selector_overlay6 = x; }
                                }
                            }
                        }
                        if(974 < cursorX && cursorX < 1074 && 310 < cursorY && cursorY < 410) { // Exit Overlay
                            if(active_palette == "tileset") {
                                selector_tileset = new Vector2(selector_overlay0.X + 10 * selector_overlay.X, selector_overlay0.Y + 10 * selector_overlay.Y);
                            } else if(active_palette == "event") {
                                selector_event = selector_overlay0;
                            } else {
                                active_tileset = selector_overlay1;
                                active_music = selector_overlay2;
                                active_background0 = selector_overlay3;
                                active_background1 = selector_overlay4;
                                active_background2 = selector_overlay5;
                                active_background3 = selector_overlay6;
                            }
                            active_transition = true;
                        }
                        if(974 < cursorX && cursorX < 1074 && 210 < cursorY && cursorY < 310) { // Up Overlay
                            if(active_palette == "tileset") {
                                selector_overlay.X--;
                                if(selector_overlay.X < 0) {
                                    selector_overlay.X = selector_overlay_max.X;
                                    selector_overlay.Y--;
                                    if(selector_overlay.Y < 0) {
                                        selector_overlay.Y = selector_overlay_max.Y;
                                    }
                                }
                            }
                        }
                        if(974 < cursorX && cursorX < 1074 && 410 < cursorY && cursorY < 510) { // Down Overlay
                            if(active_palette == "tileset") {
                                selector_overlay.X++;
                                if(selector_overlay.X > selector_overlay_max.X) {
                                    selector_overlay.X = 0;
                                    selector_overlay.Y++;
                                    if(selector_overlay.Y > selector_overlay_max.Y) {
                                        selector_overlay.Y = 0;
                                    }
                                }
                            }
                        }
                    } else {
                        if(30 < cursorX && cursorX < 130 && 15 < cursorY && cursorY < 75) { // Exit
                            return "menu";
                        }

                        if(5 < cursorX && cursorX < 130 && 115 < cursorY && cursorY < 152) { // New
                            New();
                        }
                        if(5 < cursorX && cursorX < 100 && 173 < cursorY && cursorY < 210) { // Load
                            Load();
                        }
                        if(5 < cursorX && cursorX < 100 && 230 < cursorY && cursorY < 268) { // Save
                            Save();
                        }

                        if(20 < cursorX && cursorX < 100 && 626 < cursorY && cursorY < 705) { // Tileset
                            active_palette = "tileset";
                            Create_Palette();
                            active_transition = true;
                        }
                        if(150 < cursorX && cursorX < 230 && 626 < cursorY && cursorY < 705) { // Event
                            active_palette = "event";
                            Create_Palette();
                            active_transition = true;
                        }
                        if(280 < cursorX && cursorX < 360 && 626 < cursorY && cursorY < 705) { // World
                            active_palette = "world";
                            Create_Palette();
                            active_transition = true;
                        }

                        if(37 < cursorX && cursorX < 68 && 491 < cursorY && cursorY < 522) { // Up
                            Command_Grid_Shift("up");
                        }
                        if(37 < cursorX && cursorX < 68 && 555 < cursorY && cursorY < 586) { // Down
                            Command_Grid_Shift("down");
                        }
                        if(5 < cursorX && cursorX < 36 && 523 < cursorY && cursorY < 554) { // Left
                            Command_Grid_Shift("left");
                        }
                        if(69 < cursorX && cursorX < 100 && 523 < cursorY && cursorY < 554) { // Right
                            Command_Grid_Shift("right");
                        }

                        if(active_palette == "tileset" || active_palette == "background") {
                            if(880 < cursorX && cursorX < 930 && 645 < cursorY && cursorY < 695) { // Far
                                active_field = "far";
                            }
                            if(955 < cursorX && cursorX < 1005 && 645 < cursorY && cursorY < 695) { // Middle
                                active_field = "middle";
                            }
                            if(1030 < cursorX && cursorX < 1080 && 645 < cursorY && cursorY < 695) { // Near
                                active_field = "near";
                            }
                        }

                        if(1130 < cursorX && cursorY < 330) {
                            Command_Mousewheel(true);
                        }
                        if(1130 < cursorX && cursorY > 390) {
                            Command_Mousewheel(false);
                        }
                    }
                }
            }
            return "void";
        }

        private void Command_Mousewheel(bool direction) {
            Vector2 selector     = new Vector2(0,0);
            Vector2 selector_max = new Vector2(0,0);
            if(active_overlay) {
                if(direction) {
                    if(active_palette == "tileset") {
                        selector_overlay.X--;
                        if(selector_overlay.X < 0) {
                            selector_overlay.X = selector_overlay_max.X;
                            selector_overlay.Y--;
                            if(selector_overlay.Y < 0) {
                                selector_overlay.Y = selector_overlay_max.Y;
                            }
                        }
                    }
                } else {
                    if(active_palette == "tileset") {
                        selector_overlay.X++;
                        if(selector_overlay.X > selector_overlay_max.X) {
                            selector_overlay.X = 0;
                            selector_overlay.Y++;
                            if(selector_overlay.Y > selector_overlay_max.Y) {
                                selector_overlay.Y = 0;
                            }
                        }
                    }
                }
            } else {
                if(active_palette == "tileset") { selector = selector_tileset; selector_max = selector_tileset_max; }
                if(active_palette == "event") { selector = selector_event; selector_max = selector_event_max; }
                if(direction) {
                    selector.X--;
                    if(selector.X < 0) {
                        selector.X = selector_max.X;
                        selector.Y--;
                        if(selector.Y < 0) {
                            selector.Y = selector_max.Y;
                        }
                    }
                }
                if(!direction) {
                    selector.X++;
                    if(selector.X > selector_max.X) {
                        selector.X = 0;
                        selector.Y++;
                        if(selector.Y > selector_max.Y) {
                            selector.Y = 0;
                        }
                    }
                }
                if(active_palette == "tileset") { selector_tileset = selector; }
                if(active_palette == "event") { selector_event = selector; }
                Create_Palette();
            }
        }

        private void Command_Grid_Shift(string direction) {
            if(direction == "up") {
                if(grid_shift.Y != 0) {
                    grid_shift.Y = grid_shift.Y - 8;
                    Create_Field();
                }
            }
            if(direction == "down") {
                if(grid_shift.Y != grid.Y - 16) {
                    grid_shift.Y = grid_shift.Y + 8;
                    Create_Field();
                }
            }
            if(direction == "left") {
                if(grid_shift.X != 0) {
                    grid_shift.X = grid_shift.X - 16;
                    Create_Field();
                }
            }
            if(direction == "right") {
                if(grid_shift.X != grid.X - 32) {
                    grid_shift.X = grid_shift.X + 16;
                    Create_Field();
                }
            }
        }

    }
}
