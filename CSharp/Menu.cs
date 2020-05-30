using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace MaliceRT {
    class Menu {
        ContentManager content;
        GraphicsDevice graphics;
        SpriteBatch spriteBatch;
        ShopKeeper Shopkeeper;
        FileManager Filemanager;
        CollisionCourse Collisioncourse;

        float core_screen_constant_width  = 0;
        float core_screen_constant_height = 0;
        float scaleX = 0;
        float scaleY = 0;

        int score1;
        int score2;
        int score3;
        int score4;
        int score5;

        Vector2 grid_chain_position_main   = new Vector2(0,0);
        Vector2 grid_chain_position_upper1 = new Vector2(0,0);
        Vector2 grid_chain_position_upper2 = new Vector2(0,0);
        Vector2 grid_chain_position_lower1 = new Vector2(0,0);
        Vector2 grid_chain_position_lower2 = new Vector2(0,0);
        Vector2 grid_selector_position_1   = new Vector2(0,0);
        Vector2 grid_selector_position_2   = new Vector2(0,0);
        Vector2 grid_selector_position_3   = new Vector2(0,0);
        Vector2 grid_selector_position_4   = new Vector2(0,0);
        Vector2 grid_selector_position_5   = new Vector2(0,0);

        Texture2D texture_background_title_0;
        Texture2D texture_background_title_1;
        Texture2D texture_background_title_2;
        Texture2D texture_background_title_3;
        Texture2D texture_background_menu_0;
        Texture2D texture_background_menu_1;
        Texture2D texture_background_menu_2;
        Texture2D texture_background_menu_3;

        Texture2D texture_grid_bump;
        Texture2D texture_grid_options;
        Texture2D texture_grid_editor;
        Texture2D texture_grid_endless;
        Texture2D texture_grid_grass;
        Texture2D texture_grid_vulcano;
        Texture2D texture_grid_sky;
        Texture2D texture_grid_desert;
        Texture2D texture_grid_beach;
        Texture2D texture_grid_snow;
        Texture2D texture_grid_mountain;
        Texture2D texture_grid_mashine;
        Texture2D texture_grid_forest;
        Texture2D texture_grid_aurora;
        Texture2D texture_grid_space;
        Texture2D texture_grid_selector;

        Texture2D texture_button_editor;
        Texture2D texture_button_highscore;

        Texture2D texture_name_act1;
        Texture2D texture_name_act2;
        Texture2D texture_name_act3;
        Texture2D texture_name_act4;
        Texture2D texture_name_act5;
        Texture2D texture_name_options;
        Texture2D texture_name_editor;
        Texture2D texture_name_endless;
        Texture2D texture_name_grass;
        Texture2D texture_name_vulcano;
        Texture2D texture_name_sky;
        Texture2D texture_name_desert;
        Texture2D texture_name_beach;
        Texture2D texture_name_snow;
        Texture2D texture_name_mountain;
        Texture2D texture_name_mashine;
        Texture2D texture_name_forest;
        Texture2D texture_name_aurora;
        Texture2D texture_name_space;
        Texture2D texture_name_unknown;

        int background_position_3 = 0;
        int background_position_2 = 0;
        int background_position_1 = 0;

        string grid_chain_content_upper2 = "space";
        string grid_chain_content_upper1 = "editor";
        string grid_chain_content_main   = "grass";
        string grid_chain_content_lower1 = "vulcano";
        string grid_chain_content_lower2 = "sky";

        int grid_selector_position = 1;

        bool active_title;

        public SpriteFont fontScore;

        KeyboardState control_keyboard_new;
        KeyboardState control_keyboard_old;
        MouseState control_mouse_new;
        MouseState control_mouse_old;
        TouchCollection control_touch;

        RenderTarget2D renderTarget;

        public Menu(ContentManager _content, GraphicsDevice _graphics, SpriteBatch _spritebatch, ShopKeeper _shopkeeper, FileManager _filemanager, float _const_width, float _const_height, float _scale_width, float _scale_height) {
            content = _content;
            graphics = _graphics;
            spriteBatch = _spritebatch;
            Shopkeeper = _shopkeeper;
            Filemanager = _filemanager;
            Collisioncourse = new CollisionCourse();
            TouchPanel.EnabledGestures = GestureType.Tap;
            core_screen_constant_width = _const_width;
            core_screen_constant_height = _const_height;
            scaleX = _scale_width;
            scaleY = _scale_height;
            active_title = true;
            fontScore = content.Load<SpriteFont>(Shopkeeper.font_score);
            grid_chain_position_main = Shopkeeper.grid_chain_position_main;
            grid_chain_position_upper1 = Shopkeeper.grid_chain_position_upper1;
            grid_chain_position_upper2 = Shopkeeper.grid_chain_position_upper2;
            grid_chain_position_lower1 = Shopkeeper.grid_chain_position_lower1;
            grid_chain_position_lower2 = Shopkeeper.grid_chain_position_lower2;
            grid_selector_position_1 = Shopkeeper.grid_selector_position_1;
            grid_selector_position_2 = Shopkeeper.grid_selector_position_2;
            grid_selector_position_3 = Shopkeeper.grid_selector_position_3;
            grid_selector_position_4 = Shopkeeper.grid_selector_position_4;
            grid_selector_position_5 = Shopkeeper.grid_selector_position_5;
            texture_background_title_0 = content.Load<Texture2D>(Shopkeeper.texture_background_title0);
            texture_background_title_1 = content.Load<Texture2D>(Shopkeeper.texture_background_title1);
            texture_background_title_2 = content.Load<Texture2D>(Shopkeeper.texture_background_title2);
            texture_background_title_3 = content.Load<Texture2D>(Shopkeeper.texture_background_title3);
            texture_background_menu_0 = content.Load<Texture2D>(Shopkeeper.texture_background_menu0);
            texture_background_menu_1 = content.Load<Texture2D>(Shopkeeper.texture_background_menu1);
            texture_background_menu_2 = content.Load<Texture2D>(Shopkeeper.texture_background_menu2);
            texture_background_menu_3 = content.Load<Texture2D>(Shopkeeper.texture_background_menu3);
            texture_grid_bump = content.Load<Texture2D>(Shopkeeper.texture_grid_bump);
            texture_grid_options = content.Load<Texture2D>(Shopkeeper.texture_grid_options);
            texture_grid_editor = content.Load<Texture2D>(Shopkeeper.texture_grid_editor);
            texture_grid_endless = content.Load<Texture2D>(Shopkeeper.texture_grid_endless);
            texture_grid_grass = content.Load<Texture2D>(Shopkeeper.texture_grid_grass);
            texture_grid_vulcano = content.Load<Texture2D>(Shopkeeper.texture_grid_vulcano);
            texture_grid_sky = content.Load<Texture2D>(Shopkeeper.texture_grid_sky);
            texture_grid_desert = content.Load<Texture2D>(Shopkeeper.texture_grid_desert);
            texture_grid_beach = content.Load<Texture2D>(Shopkeeper.texture_grid_beach);
            texture_grid_snow = content.Load<Texture2D>(Shopkeeper.texture_grid_snow);
            texture_grid_mountain = content.Load<Texture2D>(Shopkeeper.texture_grid_mountain);
            texture_grid_mashine = content.Load<Texture2D>(Shopkeeper.texture_grid_mashine);
            texture_grid_forest = content.Load<Texture2D>(Shopkeeper.texture_grid_forest);
            texture_grid_aurora = content.Load<Texture2D>(Shopkeeper.texture_grid_aurora);
            texture_grid_space = content.Load<Texture2D>(Shopkeeper.texture_grid_space);
            texture_grid_selector = content.Load<Texture2D>(Shopkeeper.texture_grid_selector);

            texture_button_editor = content.Load<Texture2D>(Shopkeeper.texture_menu_button_editor);
            texture_button_highscore = content.Load<Texture2D>(Shopkeeper.texture_menu_button_highscore);

            texture_name_act1 = content.Load<Texture2D>(Shopkeeper.texture_menu_name_act1);
            texture_name_act2 = content.Load<Texture2D>(Shopkeeper.texture_menu_name_act2);
            texture_name_act3 = content.Load<Texture2D>(Shopkeeper.texture_menu_name_act3);
            texture_name_act4 = content.Load<Texture2D>(Shopkeeper.texture_menu_name_act4);
            texture_name_act5 = content.Load<Texture2D>(Shopkeeper.texture_menu_name_act5);
            texture_name_options = content.Load<Texture2D>(Shopkeeper.texture_menu_name_options);
            texture_name_editor = content.Load<Texture2D>(Shopkeeper.texture_menu_name_editor);
            texture_name_endless = content.Load<Texture2D>(Shopkeeper.texture_menu_name_endless);
            texture_name_grass = content.Load<Texture2D>(Shopkeeper.texture_menu_name_grass);
            texture_name_vulcano = content.Load<Texture2D>(Shopkeeper.texture_menu_name_vulcano);
            texture_name_sky = content.Load<Texture2D>(Shopkeeper.texture_menu_name_sky);
            texture_name_desert = content.Load<Texture2D>(Shopkeeper.texture_menu_name_desert);
            texture_name_beach = content.Load<Texture2D>(Shopkeeper.texture_menu_name_beach);
            texture_name_snow = content.Load<Texture2D>(Shopkeeper.texture_menu_name_snow);
            texture_name_mountain = content.Load<Texture2D>(Shopkeeper.texture_menu_name_mountain);
            texture_name_mashine = content.Load<Texture2D>(Shopkeeper.texture_menu_name_mashine);
            texture_name_forest = content.Load<Texture2D>(Shopkeeper.texture_menu_name_forest);
            texture_name_aurora = content.Load<Texture2D>(Shopkeeper.texture_menu_name_aurora);
            texture_name_space = content.Load<Texture2D>(Shopkeeper.texture_menu_name_space);
            texture_name_unknown = content.Load<Texture2D>(Shopkeeper.texture_menu_name_unknown);

            Set_Score();

            renderTarget = new RenderTarget2D(graphics, (int)core_screen_constant_width, (int)core_screen_constant_height);
            Collisioncourse.InserGreymark(texture_grid_bump, (int)(grid_selector_position_1.Y), (int)((grid_selector_position_2.Y + texture_grid_selector.Height)),
                                            new Vector2(grid_selector_position_1.X, grid_selector_position_1.Y),
                                            new Vector2(grid_selector_position_2.X, grid_selector_position_2.Y),
                                            new Vector2(grid_selector_position_3.X, grid_selector_position_3.Y),
                                            new Vector2(grid_selector_position_4.X, grid_selector_position_4.Y),
                                            new Vector2(grid_selector_position_5.X, grid_selector_position_5.Y));
        }

        public void Resize(float x, float y) {
            scaleX = x;
            scaleY = y;
        }

        public string Update(GameTime gameTime) {
            control_keyboard_new = Keyboard.GetState();
            control_mouse_new = Mouse.GetState();
            control_touch = TouchPanel.GetState();

            //Controls
            if(active_title) {
                if(control_keyboard_new.IsKeyDown(Keys.Enter) && control_keyboard_old.IsKeyUp(Keys.Enter)) { active_title = false; }
                if(control_keyboard_new.IsKeyDown(Keys.Space) && control_keyboard_old.IsKeyUp(Keys.Space)) { active_title = false; }
            } else {
                if(control_keyboard_new.IsKeyDown(Keys.Enter) && control_keyboard_old.IsKeyUp(Keys.Enter)) {
                    return "level";
                }

                if(control_keyboard_new.IsKeyDown(Keys.Up) && control_keyboard_old.IsKeyUp(Keys.Up)) { Command_Change_Grid_Content(false); }
                if(control_keyboard_new.IsKeyDown(Keys.Left) && control_keyboard_old.IsKeyUp(Keys.Left)) { if(grid_selector_position != 1) { grid_selector_position--; Set_Score(); } }
                if(control_keyboard_new.IsKeyDown(Keys.Right) && control_keyboard_old.IsKeyUp(Keys.Right)) { if(grid_selector_position != 5) { grid_selector_position++; Set_Score(); } }
                if(control_keyboard_new.IsKeyDown(Keys.Down) && control_keyboard_old.IsKeyUp(Keys.Down)) { Command_Change_Grid_Content(true); }
            }

            if(control_mouse_new.LeftButton == ButtonState.Pressed && control_mouse_old.LeftButton == ButtonState.Released) {
                if(active_title) {
                    active_title = false;
                } else {
                    if(12 < control_mouse_new.Position.X / scaleX && control_mouse_new.Position.X / scaleX < 403 && 537 < control_mouse_new.Position.Y / scaleY && control_mouse_new.Position.Y / scaleY < 710) {
                        if(grid_chain_content_main == "editor") {
                            return "editor";
                        } else {
                            Filemanager.Reset_Highscore(grid_chain_content_main, grid_selector_position);
                        }
                    }
                    string temp_mouse = Collisioncourse.CollisionGrid((int)(control_mouse_new.Position.X/scaleX), (int)(control_mouse_new.Position.Y/scaleY));
                    switch(temp_mouse) {
                        case "movingUP": Command_Change_Grid_Content(false); break;
                        case "movingDOWN": Command_Change_Grid_Content(true); break;
                        case "Position1": if(grid_selector_position == 1) { return "level"; } else { grid_selector_position = 1; Set_Score(); } break;
                        case "Position2": if(grid_selector_position == 2) { return "level"; } else { grid_selector_position = 2; Set_Score(); } break;
                        case "Position3": if(grid_selector_position == 3) { return "level"; } else { grid_selector_position = 3; Set_Score(); } break;
                        case "Position4": if(grid_selector_position == 4) { return "level"; } else { grid_selector_position = 4; Set_Score(); } break;
                        case "Position5": if(grid_selector_position == 5) { return "level"; } else { grid_selector_position = 5; Set_Score(); } break;
                    }
                }

            }
            if(control_mouse_new.LeftButton == ButtonState.Released && control_mouse_old.LeftButton == ButtonState.Released) {
                string temp_mouseHover = Collisioncourse.CollisionGrid((int)(control_mouse_new.Position.X/scaleX), (int)(control_mouse_new.Position.Y/scaleY));
                switch(temp_mouseHover) {
                    case "Position1": grid_selector_position = 1; Set_Score(); break;
                    case "Position2": grid_selector_position = 2; Set_Score(); break;
                    case "Position3": grid_selector_position = 3; Set_Score(); break;
                    case "Position4": grid_selector_position = 4; Set_Score(); break;
                    case "Position5": grid_selector_position = 5; Set_Score(); break;
                }
            }
            if(control_mouse_new.ScrollWheelValue < control_mouse_old.ScrollWheelValue) { Command_Change_Grid_Content(false); }
            if(control_mouse_new.ScrollWheelValue > control_mouse_old.ScrollWheelValue) { Command_Change_Grid_Content(true); }
            while(TouchPanel.IsGestureAvailable) {
                var gesture = TouchPanel.ReadGesture();
                if(gesture.GestureType == GestureType.Tap) {
                    if(active_title) {
                        active_title = false;
                    } else {
                        if(12 < gesture.Position.X / scaleX && gesture.Position.X / scaleX < 403 && 537 < gesture.Position.Y / scaleY && gesture.Position.Y / scaleY < 710) {
                            if(grid_chain_content_main == "editor") {
                                return "editor";
                            } else {
                                Filemanager.Reset_Highscore(grid_chain_content_main, grid_selector_position);
                            }
                        }
                        string temp_touch = Collisioncourse.CollisionGrid((int)(gesture.Position.X/scaleX), (int)(gesture.Position.Y/scaleY));
                        switch(temp_touch) {
                            case "movingUP": Command_Change_Grid_Content(false); break;
                            case "movingDOWN": Command_Change_Grid_Content(true); break;
                            case "Position1": if(grid_selector_position == 1) { return "level"; } else { grid_selector_position = 1; Set_Score(); } break;
                            case "Position2": if(grid_selector_position == 2) { return "level"; } else { grid_selector_position = 2; Set_Score(); } break;
                            case "Position3": if(grid_selector_position == 3) { return "level"; } else { grid_selector_position = 3; Set_Score(); } break;
                            case "Position4": if(grid_selector_position == 4) { return "level"; } else { grid_selector_position = 4; Set_Score(); } break;
                            case "Position5": if(grid_selector_position == 5) { return "level"; } else { grid_selector_position = 5; Set_Score(); } break;
                        }
                    }

                }
            }

            //Moving Background
            //background_position_3 = background_position_3 + 1;
            //background_position_2 = background_position_2 + 2;
            //background_position_1 = background_position_1 + 3;
            //if(background_position_3 >= core_screen_constant_width) background_position_3 = 0;
            //if(background_position_2 >= core_screen_constant_width) background_position_2 = 0;
            //if(background_position_1 >= core_screen_constant_width) background_position_1 = 0;

            control_keyboard_old = control_keyboard_new;
            control_mouse_old = control_mouse_new;
            return "null";
        }

        public RenderTarget2D Get_RenderTarget() {
            return renderTarget;
        }

        public void Draw(GameTime gameTime) {
            graphics.SetRenderTarget(renderTarget);
            spriteBatch.Begin();
            graphics.Clear(Color.CornflowerBlue);
            if(active_title) {
                spriteBatch.Draw(texture_background_title_3, new Rectangle(-background_position_3, 0, texture_background_title_3.Width, texture_background_title_3.Height), Color.White);
                //spriteBatch.Draw(texture_background_title_3, new Rectangle((int)core_screen_constant_width -background_position_3 , 0, texture_background_title_3.Width, texture_background_title_3.Height), Color.White);
                spriteBatch.Draw(texture_background_title_2, new Rectangle(-background_position_2, 0, texture_background_title_2.Width, texture_background_title_2.Height), Color.White);
                //spriteBatch.Draw(texture_background_title_2, new Rectangle((int)core_screen_constant_width -background_position_2 , 0, texture_background_title_2.Width, texture_background_title_2.Height), Color.White);
                spriteBatch.Draw(texture_background_title_1, new Rectangle(-background_position_1, 0, texture_background_title_1.Width, texture_background_title_1.Height), Color.White);
                //spriteBatch.Draw(texture_background_title_1, new Rectangle((int)core_screen_constant_width -background_position_1 , 0, texture_background_title_1.Width, texture_background_title_1.Height), Color.White);
            } else {
                spriteBatch.Draw(texture_background_menu_3, new Rectangle(-background_position_3, 0, texture_background_menu_3.Width, texture_background_menu_3.Height), Color.White);
                //spriteBatch.Draw(texture_background_menu_3, new Rectangle((int)core_screen_constant_width -background_position_3 , 0, texture_background_menu_3.Width, texture_background_menu_3.Height), Color.White);
                spriteBatch.Draw(texture_background_menu_2, new Rectangle(-background_position_2, 0, texture_background_menu_2.Width, texture_background_menu_2.Height), Color.White);
                //spriteBatch.Draw(texture_background_menu_2, new Rectangle((int)core_screen_constant_width -background_position_2 , 0, texture_background_menu_2.Width, texture_background_menu_2.Height), Color.White);
                spriteBatch.Draw(texture_background_menu_1, new Rectangle(-background_position_1, 0, texture_background_menu_1.Width, texture_background_menu_1.Height), Color.White);
                //spriteBatch.Draw(texture_background_menu_1, new Rectangle((int)core_screen_constant_width -background_position_1 , 0, texture_background_menu_1.Width, texture_background_menu_1.Height), Color.White);

                spriteBatch.Draw(Get_GridTexture(grid_chain_content_upper2), new Rectangle((int)grid_chain_position_upper2.X, (int)grid_chain_position_upper2.Y, texture_grid_editor.Width, texture_grid_editor.Height), Color.White);
                spriteBatch.Draw(Get_GridTexture(grid_chain_content_upper1), new Rectangle((int)grid_chain_position_upper1.X, (int)grid_chain_position_upper1.Y, texture_grid_editor.Width, texture_grid_editor.Height), Color.White);
                spriteBatch.Draw(Get_GridTexture(grid_chain_content_main), new Rectangle((int)grid_chain_position_main.X, (int)grid_chain_position_main.Y, texture_grid_editor.Width, texture_grid_editor.Height), Color.White);
                spriteBatch.Draw(Get_GridTexture(grid_chain_content_lower1), new Rectangle((int)grid_chain_position_lower1.X, (int)grid_chain_position_lower1.Y, texture_grid_editor.Width, texture_grid_editor.Height), Color.White);
                spriteBatch.Draw(Get_GridTexture(grid_chain_content_lower2), new Rectangle((int)grid_chain_position_lower2.X, (int)grid_chain_position_lower2.Y, texture_grid_editor.Width, texture_grid_editor.Height), Color.White);

                switch(grid_selector_position) {
                    case 1: spriteBatch.Draw(texture_grid_selector, new Rectangle((int)grid_selector_position_1.X, (int)grid_selector_position_1.Y, texture_grid_selector.Width, texture_grid_selector.Height), Color.White); break;
                    case 2: spriteBatch.Draw(texture_grid_selector, new Rectangle((int)grid_selector_position_2.X, (int)grid_selector_position_2.Y, texture_grid_selector.Width, texture_grid_selector.Height), Color.White); break;
                    case 3: spriteBatch.Draw(texture_grid_selector, new Rectangle((int)grid_selector_position_3.X, (int)grid_selector_position_3.Y, texture_grid_selector.Width, texture_grid_selector.Height), Color.White); break;
                    case 4: spriteBatch.Draw(texture_grid_selector, new Rectangle((int)grid_selector_position_4.X, (int)grid_selector_position_4.Y, texture_grid_selector.Width, texture_grid_selector.Height), Color.White); break;
                    case 5: spriteBatch.Draw(texture_grid_selector, new Rectangle((int)grid_selector_position_5.X, (int)grid_selector_position_5.Y, texture_grid_selector.Width, texture_grid_selector.Height), Color.White); break;
                }
                switch(grid_chain_content_main) {
                    case "options": spriteBatch.Draw(texture_name_options, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "editor": spriteBatch.Draw(texture_name_editor, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "endless": spriteBatch.Draw(texture_name_endless, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "grass": spriteBatch.Draw(texture_name_grass, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "vulcano": spriteBatch.Draw(texture_name_vulcano, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "sky": spriteBatch.Draw(texture_name_sky, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "desert": spriteBatch.Draw(texture_name_desert, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "beach": spriteBatch.Draw(texture_name_beach, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "snow": spriteBatch.Draw(texture_name_snow, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "mountain": spriteBatch.Draw(texture_name_mountain, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "mashine": spriteBatch.Draw(texture_name_mashine, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "forest": spriteBatch.Draw(texture_name_forest, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "aurora": spriteBatch.Draw(texture_name_aurora, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "space": spriteBatch.Draw(texture_name_space, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                    case "unknown": spriteBatch.Draw(texture_name_unknown, new Rectangle(0, 0, texture_name_editor.Width, texture_name_editor.Height), Color.White); break;
                }
                switch(grid_selector_position) {
                    case 1: spriteBatch.Draw(texture_name_act1, new Rectangle(0, 0, texture_name_act1.Width, texture_name_act1.Height), Color.White); break;
                    case 2: spriteBatch.Draw(texture_name_act2, new Rectangle(0, 0, texture_name_act2.Width, texture_name_act2.Height), Color.White); break;
                    case 3: spriteBatch.Draw(texture_name_act3, new Rectangle(0, 0, texture_name_act3.Width, texture_name_act3.Height), Color.White); break;
                    case 4: spriteBatch.Draw(texture_name_act4, new Rectangle(0, 0, texture_name_act4.Width, texture_name_act4.Height), Color.White); break;
                    case 5: spriteBatch.Draw(texture_name_act5, new Rectangle(0, 0, texture_name_act5.Width, texture_name_act5.Height), Color.White); break;
                }

                if(grid_chain_content_main == "editor") {
                    spriteBatch.Draw(texture_button_editor, new Rectangle(0, 0, (int)core_screen_constant_width, (int)core_screen_constant_height), Color.White);
                } else {
                    spriteBatch.Draw(texture_button_highscore, new Rectangle(0, 0, (int)core_screen_constant_width, (int)core_screen_constant_height), Color.White);
                }

                spriteBatch.DrawString(fontScore, "Highscore:", new Vector2(75, 260), Color.Black);
                spriteBatch.DrawString(fontScore, "1. " + score1, new Vector2(80, 290), Color.Black);
                spriteBatch.DrawString(fontScore, "2. " + score2, new Vector2(80, 320), Color.Black);
                spriteBatch.DrawString(fontScore, "3. " + score3, new Vector2(80, 350), Color.Black);
                spriteBatch.DrawString(fontScore, "4. " + score4, new Vector2(80, 380), Color.Black);
                spriteBatch.DrawString(fontScore, "5. " + score5, new Vector2(80, 410), Color.Black);

            }

            if(active_title) {
                spriteBatch.Draw(texture_background_title_0, new Rectangle(0, 0, texture_background_title_0.Width, texture_background_title_0.Height), Color.White);
            } else {
                spriteBatch.Draw(texture_background_menu_0, new Rectangle(0, 0, texture_background_menu_0.Width, texture_background_menu_0.Height), Color.White);
            }
            spriteBatch.End();
            graphics.SetRenderTarget(null);
        }

        private Texture2D Get_GridTexture(string s) {
            if(s == "options") return texture_grid_options;
            if(s == "editor") return texture_grid_editor;
            if(s == "endless") return texture_grid_endless;
            if(s == "grass") return texture_grid_grass;
            if(s == "vulcano") return texture_grid_vulcano;
            if(s == "sky") return texture_grid_sky;
            if(s == "desert") return texture_grid_desert;
            if(s == "beach") return texture_grid_beach;
            if(s == "snow") return texture_grid_snow;
            if(s == "mountain") return texture_grid_mountain;
            if(s == "mashine") return texture_grid_mashine;
            if(s == "forest") return texture_grid_forest;
            if(s == "aurora") return texture_grid_aurora;
            if(s == "space") return texture_grid_space;
            return texture_grid_options;
        }

        private void Command_Change_Grid_Content(bool b) {
            grid_chain_content_main = Command_Change_Grid_Content2(grid_chain_content_main, b);
            grid_chain_content_upper1 = Command_Change_Grid_Content2(grid_chain_content_upper1, b);
            grid_chain_content_upper2 = Command_Change_Grid_Content2(grid_chain_content_upper2, b);
            grid_chain_content_lower1 = Command_Change_Grid_Content2(grid_chain_content_lower1, b);
            grid_chain_content_lower2 = Command_Change_Grid_Content2(grid_chain_content_lower2, b);
            Set_Score();
        }

        //private string Command_Change_Grid_Content2(string s, bool b) {
        //    if(s == "options")  if(b) { return "editor";   } else { return "space";    }
        //    if(s == "editor")   if(b) { return "endless";  } else { return "options";  }
        //    if(s == "endless")  if(b) { return "grass";    } else { return "editor";   }
        //    if(s == "grass")    if(b) { return "vulcano";  } else { return "endless";  }
        //    if(s == "vulcano")  if(b) { return "sky";      } else { return "grass";    }
        //    if(s == "sky")      if(b) { return "desert";   } else { return "vulcano";  }
        //    if(s == "desert")   if(b) { return "beach";    } else { return "sky";      }
        //    if(s == "beach")    if(b) { return "snow";     } else { return "desert";   }
        //    if(s == "snow")     if(b) { return "mountain"; } else { return "beach";    }
        //    if(s == "mountain") if(b) { return "mashine";  } else { return "snow";     }
        //    if(s == "mashine")  if(b) { return "forest";   } else { return "mountain"; }
        //    if(s == "forest")   if(b) { return "aurora";   } else { return "mashine";  }
        //    if(s == "aurora")   if(b) { return "space";    } else { return "forest";   }
        //    if(s == "space")    if(b) { return "options";  } else { return "aurora";   }
        //    return s;
        //}

        private string Command_Change_Grid_Content2(string s, bool b) {
            if(s == "editor") if(b) { return "grass"; } else { return "space"; }
            if(s == "grass") if(b) { return "vulcano"; } else { return "editor"; }
            if(s == "vulcano") if(b) { return "sky"; } else { return "grass"; }
            if(s == "sky") if(b) { return "desert"; } else { return "vulcano"; }
            if(s == "desert") if(b) { return "beach"; } else { return "sky"; }
            if(s == "beach") if(b) { return "snow"; } else { return "desert"; }
            if(s == "snow") if(b) { return "mountain"; } else { return "beach"; }
            if(s == "mountain") if(b) { return "mashine"; } else { return "snow"; }
            if(s == "mashine") if(b) { return "forest"; } else { return "mountain"; }
            if(s == "forest") if(b) { return "aurora"; } else { return "mashine"; }
            if(s == "aurora") if(b) { return "space"; } else { return "forest"; }
            if(s == "space") if(b) { return "editor"; } else { return "aurora"; }
            return s;
        }

        public string Get_Level_World() {
            return grid_chain_content_main;
        }

        public int Get_Level_Act() {
            return grid_selector_position;
        }

        public void Set_Score() {
            score1 = Filemanager.Get_Highscore(grid_chain_content_main, grid_selector_position, 0);
            score2 = Filemanager.Get_Highscore(grid_chain_content_main, grid_selector_position, 1);
            score3 = Filemanager.Get_Highscore(grid_chain_content_main, grid_selector_position, 2);
            score4 = Filemanager.Get_Highscore(grid_chain_content_main, grid_selector_position, 3);
            score5 = Filemanager.Get_Highscore(grid_chain_content_main, grid_selector_position, 4);
        }
    }
}
