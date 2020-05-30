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
    class Level {
        ContentManager content;
        GraphicsDevice graphics;
        SpriteBatch spriteBatch;
        ShopKeeper Shopkeeper;
        FileManager Filemanager;
        CollisionCourse Collisioncourse;
        MapRoom Maproom;

        float buttonanimation_alpha;
        float buttonanimation_timer;
        float buttonanimation_scale;

        System.Random random = new System.Random();

        bool editored = false;

        bool dragging = false;

        bool level_won1 = false;
        bool level_won2 = false;
        bool score_submitted = false;

        bool malice_looking_up = false;
        bool malice_looking_down = false;

        bool jumper = false;
        int jumper_point = 0;
        int jumper_height = 0;
        bool malice_damaged = false;

        int score_final = 0;
        int score_killed = 0;
        int score_coins = 0;
        double score_time_now = 0;
        double score_time_started = 200;
        double score_time_system = 0;

        bool timer = true;

        bool show_touch = true;

        int shots_left = 20;

        int malice_sprite = 0;

        int camera_position_X = 0;
        int camera_position_Y = 0;

        string world = "grass";
        int act = 1;

        bool exitreached = false;

        string music;

        Monster Malice;
        Entity  Exit;
        List<Monster> ListMonster     = new List<Monster>();
        List<Entity>  ListCoin        = new List<Entity>();
        List<Block>   ListBlockNear   = new List<Block>();
        List<Block>   ListBlockMiddle = new List<Block>();
        List<Block>   ListBlockFar    = new List<Block>();
        List<Block>   ListStopper     = new List<Block>();
        List<Block>   ListSpike       = new List<Block>();
        List<Monster> ListPlatform    = new List<Monster>();
        List<Entity>  ListBeam        = new List<Entity>();
        List<Entity>  ListTransponder = new List<Entity>();
        List<Monster> ListMino        = new List<Monster>();
        List<Entity>  ListExplosion   = new List<Entity>();
        List<Monster> ListShot        = new List<Monster>();

        float core_time_current = 0;
        float core_time_frame = 0.07f; // 0.1f
        int core_frame_index = 0;

        float core_screen_constant_width  = 0;
        float core_screen_constant_height = 0;
        float scaleX = 0;
        float scaleY = 0;

        bool[] active_beam;

        Texture2D texture_background_0;
        Texture2D texture_background_1;
        Texture2D texture_background_2;
        Texture2D texture_background_3;
        Texture2D texture_tileset;

        Texture2D texture_hud_button_fire;
        Texture2D texture_hud_button_jump;
        Texture2D texture_hud_button_pad;
        Texture2D texture_hud_hitpoint1;
        Texture2D texture_hud_hitpoint2;
        Texture2D texture_hud_hitpoointB;
        Texture2D texture_hud_score;
        Texture2D texture_hud_pause;
        Texture2D texture_spritesheet_bat;
        Texture2D texture_spritesheet_beam;
        Texture2D texture_spritesheet_coin;
        Texture2D texture_spritesheet_detector;
        Texture2D texture_spritesheet_driller;
        Texture2D texture_spritesheet_exit;
        Texture2D texture_spritesheet_explosion;
        Texture2D texture_spritesheet_eyeball;
        Texture2D texture_spritesheet_flybot;
        Texture2D texture_spritesheet_lizard;
        Texture2D texture_spritesheet_malice;
        Texture2D texture_spritesheet_mino_air;
        Texture2D texture_spritesheet_mino_blank;
        Texture2D texture_spritesheet_mino_dark;
        Texture2D texture_spritesheet_mino_earth;
        Texture2D texture_spritesheet_mino_fire;
        Texture2D texture_spritesheet_mino_ice;
        Texture2D texture_spritesheet_mino_light;
        Texture2D texture_spritesheet_mino_metal;
        Texture2D texture_spritesheet_mino_nature;
        Texture2D texture_spritesheet_mino_thunder;
        Texture2D texture_spritesheet_mino_water;
        Texture2D texture_spritesheet_platform;
        Texture2D texture_spritesheet_sentry;
        Texture2D texture_spritesheet_shot;
        Texture2D texture_spritesheet_snake;
        Texture2D texture_spritesheet_zylon;

        public SpriteFont fontScore;

        KeyboardState control_keyboard_new;
        KeyboardState control_keyboard_old;
        MouseState control_mouse_new;
        MouseState control_mouse_old;
        TouchCollection control_touch;

        RenderTarget2D renderTarget;
        RenderTarget2D renderTargetResult;

        Rectangle collision_bat;
        Rectangle collision_beam;
        Rectangle collision_coin;
        Rectangle collision_driller;
        Rectangle collision_exit;
        Rectangle collision_eyeball;
        Rectangle collision_flybot;
        Rectangle collision_lizard;
        Rectangle collision_malice;
        Rectangle collision_mino;
        Rectangle collision_platform;
        Rectangle collision_sentry;
        Rectangle collision_shot;
        Rectangle collision_snake;
        Rectangle collision_zylon;

        SoundEffect sound_coin;
        SoundEffect sound_damage;
        SoundEffect sound_explosion;
        SoundEffect sound_jump;
        SoundEffect sound_pew;

        Texture2D colorcode;

        Color[] TextureData;

        public Level(ContentManager _content, GraphicsDevice _graphics, SpriteBatch _spritebatch, ShopKeeper _shopkeeper, FileManager _filemanager, float _const_width, float _const_height, float _scale_width, float _scale_height) {
            content = _content;
            graphics = _graphics;
            spriteBatch = _spritebatch;
            Shopkeeper = _shopkeeper;
            Filemanager = _filemanager;
            core_screen_constant_width = _const_width;
            core_screen_constant_height = _const_height;
            scaleX = _scale_width;
            scaleY = _scale_height;
            Collisioncourse = new CollisionCourse();
            Maproom = new MapRoom();
            music = "grass";
            TouchPanel.EnabledGestures = GestureType.Pinch | GestureType.PinchComplete;
            active_beam = new bool[11];
            for(int i = 0; i < 11; i++) {
                active_beam[i] = true;
            }
            renderTarget = new RenderTarget2D(graphics, (int)core_screen_constant_width / 2, (int)core_screen_constant_height / 2);
            renderTargetResult = new RenderTarget2D(graphics, (int)core_screen_constant_width, (int)core_screen_constant_height);
            fontScore = content.Load<SpriteFont>(Shopkeeper.font_score);
            texture_tileset = content.Load<Texture2D>(Shopkeeper.texture_tileset_grass);
            texture_background_0 = content.Load<Texture2D>(Shopkeeper.texture_background_grass0);
            texture_background_1 = content.Load<Texture2D>(Shopkeeper.texture_background_grass1);
            texture_background_2 = content.Load<Texture2D>(Shopkeeper.texture_background_grass2);
            texture_background_3 = content.Load<Texture2D>(Shopkeeper.texture_background_grass3);
            texture_hud_button_fire = content.Load<Texture2D>(Shopkeeper.texture_hud_button_fire);
            texture_hud_button_jump = content.Load<Texture2D>(Shopkeeper.texture_hud_button_jump);
            texture_hud_button_pad = content.Load<Texture2D>(Shopkeeper.texture_hud_button_pad);
            texture_hud_hitpoint1 = content.Load<Texture2D>(Shopkeeper.texture_hud_hitpoint1);
            texture_hud_hitpoint2 = content.Load<Texture2D>(Shopkeeper.texture_hud_hitpoint2);
            texture_hud_hitpoointB = content.Load<Texture2D>(Shopkeeper.texture_hud_hitpointbackground);
            texture_hud_score = content.Load<Texture2D>(Shopkeeper.texture_hud_score);
            texture_hud_pause = content.Load<Texture2D>(Shopkeeper.texture_hud_pause);
            texture_spritesheet_bat = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_bat);
            texture_spritesheet_beam = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_beam);
            texture_spritesheet_coin = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_coin);
            texture_spritesheet_detector = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_detector);
            texture_spritesheet_driller = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_driller);
            texture_spritesheet_exit = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_exit);
            texture_spritesheet_explosion = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_explosion);
            texture_spritesheet_eyeball = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_eyeball);
            texture_spritesheet_flybot = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_flybot);
            texture_spritesheet_lizard = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_lizard);
            texture_spritesheet_malice = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_malice);
            texture_spritesheet_mino_air = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_mino_air);
            texture_spritesheet_mino_blank = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_mino_blank);
            texture_spritesheet_mino_dark = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_mino_dark);
            texture_spritesheet_mino_earth = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_mino_earth);
            texture_spritesheet_mino_fire = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_mino_fire);
            texture_spritesheet_mino_ice = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_mino_ice);
            texture_spritesheet_mino_light = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_mino_light);
            texture_spritesheet_mino_metal = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_mino_metal);
            texture_spritesheet_mino_nature = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_mino_nature);
            texture_spritesheet_mino_thunder = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_mino_thunder);
            texture_spritesheet_mino_water = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_mino_water);
            texture_spritesheet_platform = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_platform);
            texture_spritesheet_sentry = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_sentry);
            texture_spritesheet_shot = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_shot);
            texture_spritesheet_snake = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_snake);
            texture_spritesheet_zylon = content.Load<Texture2D>(Shopkeeper.texture_spritesheet_zylon);
            sound_coin = content.Load<SoundEffect>(Shopkeeper.audio_sound_coin);
            sound_damage = content.Load<SoundEffect>(Shopkeeper.audio_sound_damage);
            sound_explosion = content.Load<SoundEffect>(Shopkeeper.audio_sound_explosion);
            sound_jump = content.Load<SoundEffect>(Shopkeeper.audio_sound_jump);
            sound_pew = content.Load<SoundEffect>(Shopkeeper.audio_sound_pew);
            colorcode = content.Load<Texture2D>(Shopkeeper.texture_hud_colormap);
            Malice = new Monster("malice", 2, 0, 0, 1, 0, 0, 0, false);
            Exit = new Entity("exit", 0, 0, 1);
            collision_bat = Shopkeeper.collision_bat;
            collision_beam = Shopkeeper.collision_beam;
            collision_coin = Shopkeeper.collision_coin;
            collision_driller = Shopkeeper.collision_driller;
            collision_exit = Shopkeeper.collision_exit;
            collision_eyeball = Shopkeeper.collision_eyeball;
            collision_flybot = Shopkeeper.collision_flybot;
            collision_lizard = Shopkeeper.collision_lizard;
            collision_malice = Shopkeeper.collision_malice;
            collision_mino = Shopkeeper.collision_mino;
            collision_platform = Shopkeeper.collision_platform;
            collision_sentry = Shopkeeper.collision_sentry;
            collision_shot = Shopkeeper.collision_shot;
            collision_snake = Shopkeeper.collision_snake;
            collision_zylon = Shopkeeper.collision_zylon;
            jumper_height = Shopkeeper.malice_jump_height;
            malice_sprite = 0;
            camera_position_X = 0;
            camera_position_Y = 0;
            dragging = false;
            buttonanimation_alpha = 1.00f;
            buttonanimation_scale = 300;
            buttonanimation_timer = 100;
            TextureData = new Color[(int)(colorcode.Width) * (int)(colorcode.Height)];
            colorcode.GetData(TextureData);
            Texture2D tileset = content.Load<Texture2D>(Shopkeeper.Get_Tileset(world));
            //Collisioncourse.InsertSpritesheet(texture_spritesheet_malice, texture_spritesheet_nutcrab, texture_spritesheet_turret, texture_spritesheet_coin, texture_spritesheet_exit, texture_spritesheet_irrlicht, tileset);
        }

        public string Get_Music() {
            return music;
        }

        private string Transmute(int i) {
            if(i == 0) return "grass";
            if(i == 1) return "vulcano";
            if(i == 2) return "sky";
            if(i == 3) return "desert";
            if(i == 4) return "beach";
            if(i == 5) return "snow";
            if(i == 6) return "mountain";
            if(i == 7) return "mashine";
            if(i == 8) return "forest";
            if(i == 9) return "aurora";
            if(i == 10) return "space";
            return "void";
        }

        public void Resize(float x, float y) {
            scaleX = x;
            scaleY = y;
        }

        public void Start(string _world, int _act) {
            editored = false;
            world = _world;
            act = _act;

            level_won1 = false;
            level_won2 = false;

            malice_looking_up = false;
            malice_looking_down = false;

            score_submitted = false;
            exitreached = false;

            score_coins = 0;
            score_killed = 0;
            score_time_started = 200;

            shots_left = 20;

            ListMonster.RemoveRange(0, ListMonster.Count);
            ListCoin.RemoveRange(0, ListCoin.Count);
            ListBlockNear.RemoveRange(0, ListBlockNear.Count);
            ListBlockMiddle.RemoveRange(0, ListBlockMiddle.Count);
            ListBlockFar.RemoveRange(0, ListBlockFar.Count);
            ListStopper.RemoveRange(0, ListStopper.Count);
            ListSpike.RemoveRange(0, ListSpike.Count);
            ListPlatform.RemoveRange(0, ListPlatform.Count);
            ListBeam.RemoveRange(0, ListBeam.Count);
            ListTransponder.RemoveRange(0, ListTransponder.Count);
            ListMino.RemoveRange(0, ListMino.Count);
            ListExplosion.RemoveRange(0, ListExplosion.Count);
            ListShot.RemoveRange(0, ListShot.Count);

            for(int i = 0; i < 11; i++) {
                active_beam[i] = true;
            }

            List<List<int>> tileMap1 = new List<List<int>>();
            List<List<int>> tileMap2 = new List<List<int>>();
            List<List<int>> tileMap3 = new List<List<int>>();
            List<List<int>> tileMap4 = new List<List<int>>();
            List<string>    Row     = new List<string>();

            if(world == "editor") {
                editored = true;
                string[] temp = Filemanager.LoadList(act);
                for(int i = 0; i < 166; i++) { Row.Add(temp[i]); }
            } else {
                Row = Maproom.Get_TileMap(world, act);
            }

            world = Transmute(int.Parse(Row[0])); Row.RemoveAt(0);
            texture_tileset = content.Load<Texture2D>(Shopkeeper.Get_Tileset(world));
            music = Transmute(int.Parse(Row[0])); Row.RemoveAt(0);
            texture_background_0 = content.Load<Texture2D>(Shopkeeper.Get_Background(Transmute(int.Parse(Row[0])), 0)); Row.RemoveAt(0);
            texture_background_1 = content.Load<Texture2D>(Shopkeeper.Get_Background(Transmute(int.Parse(Row[0])), 1)); Row.RemoveAt(0);
            texture_background_2 = content.Load<Texture2D>(Shopkeeper.Get_Background(Transmute(int.Parse(Row[0])), 2)); Row.RemoveAt(0);
            texture_background_3 = content.Load<Texture2D>(Shopkeeper.Get_Background(Transmute(int.Parse(Row[0])), 3)); Row.RemoveAt(0);

            for(int i = 0; i < 40; i++) {
                string[] split = Row[0].Split(':');
                List<int> tempTileMap = new List<int>();
                foreach(string s in split) {
                    int value;
                    if(s != String.Empty) {
                        value = int.Parse(s.Substring(0));
                    } else {
                        value = 0;
                    }
                    tempTileMap.Add(value);
                }
                tileMap1.Add(tempTileMap);
                Row.RemoveAt(0);
            }

            for(int i = 0; i < 40; i++) {
                string[] split = Row[0].Split(':');
                List<int> tempTileMap = new List<int>();
                foreach(string s in split) {
                    int value;
                    if(s != String.Empty) {
                        value = int.Parse(s.Substring(0));
                    } else {
                        value = 0;
                    }
                    tempTileMap.Add(value);
                }
                tileMap2.Add(tempTileMap);
                Row.RemoveAt(0);
            }

            for(int i = 0; i < 40; i++) {
                string[] split = Row[0].Split(':');
                List<int> tempTileMap = new List<int>();
                foreach(string s in split) {
                    int value;
                    if(s != String.Empty) {
                        value = int.Parse(s.Substring(0));
                    } else {
                        value = 0;
                    }
                    tempTileMap.Add(value);
                }
                tileMap3.Add(tempTileMap);
                Row.RemoveAt(0);
            }

            for(int i = 0; i < 40; i++) {
                string[] split = Row[0].Split(':');
                List<int> tempTileMap = new List<int>();
                foreach(string s in split) {
                    int value;
                    if(s != String.Empty) {
                        value = int.Parse(s.Substring(0));
                    } else {
                        value = 0;
                    }
                    tempTileMap.Add(value);
                }
                tileMap4.Add(tempTileMap);
                Row.RemoveAt(0);
            }

            int y = 0;
            int x = 0;

            foreach(List<int> row in tileMap1) {
                x = 0;
                foreach(int id in row) {
                    if(0000 <= id && id <= 0000) { }
                    if(0001 <= id && id <= 0001) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0002 <= id && id <= 0006) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(0007 <= id && id <= 0007) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0008 <= id && id <= 0019) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(0100 <= id && id <= 0100) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0101 <= id && id <= 0119) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(0200 <= id && id <= 0200) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0201 <= id && id <= 0206) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(0207 <= id && id <= 0207) ListBlockFar.Add(new Block(id, x, y, 1));
                    if(0208 <= id && id <= 0219) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(0300 <= id && id <= 0300) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0301 <= id && id <= 0304) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(0305 <= id && id <= 0307) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0308 <= id && id <= 0319) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(0400 <= id && id <= 0401) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0402 <= id && id <= 0406) ListBlockFar.Add(new Block(id, x, y, 1));
                    if(0407 <= id && id <= 0407) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0408 <= id && id <= 0419) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(0500 <= id && id <= 0500) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0501 <= id && id <= 0501) ListBlockFar.Add(new Block(id, x, y, 1));
                    if(0502 <= id && id <= 0505) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0506 <= id && id <= 0507) ListBlockFar.Add(new Block(id, x, y, 1));
                    if(0508 <= id && id <= 0519) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(0600 <= id && id <= 0600) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(0601 <= id && id <= 0605) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0606 <= id && id <= 0606) ListBlockFar.Add(new Block(id, x, y, 1));
                    if(0607 <= id && id <= 0607) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0608 <= id && id <= 0619) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(0700 <= id && id <= 0700) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(0701 <= id && id <= 0701) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0702 <= id && id <= 0702) ListBlockFar.Add(new Block(id, x, y, 1));
                    if(0703 <= id && id <= 0707) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(0708 <= id && id <= 1307) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(1308 <= id && id <= 1311) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(1312 <= id && id <= 1406) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(1407 <= id && id <= 1411) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(1412 <= id && id <= 1506) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(1507 <= id && id <= 1507) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(1508 <= id && id <= 1511) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(1512 <= id && id <= 1607) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(1608 <= id && id <= 1611) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(1612 <= id && id <= 1707) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(1708 <= id && id <= 1709) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(1712 <= id && id <= 1807) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(1808 <= id && id <= 1809) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(1812 <= id && id <= 1814) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(1816 <= id && id <= 1818) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(1900 <= id && id <= 1907) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(1908 <= id && id <= 1909) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(1913 <= id && id <= 1914) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(1917 <= id && id <= 1918) ListBlockFar.Add(new Block(id, x, y, 2));
                    //Tree
                    if(2000 <= id && id <= 2002) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2005 <= id && id <= 2008) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2016 <= id && id <= 2019) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(2100 <= id && id <= 2102) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2103 <= id && id <= 2103) ListBlockFar.Add(new Block(id, x, y, 1));
                    if(2105 <= id && id <= 2108) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2116 <= id && id <= 2119) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(2200 <= id && id <= 2202) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2203 <= id && id <= 2204) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(2205 <= id && id <= 2208) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2216 <= id && id <= 2219) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(2300 <= id && id <= 2302) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2303 <= id && id <= 2304) ListBlockFar.Add(new Block(id, x, y, 1));
                    if(2305 <= id && id <= 2308) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2316 <= id && id <= 2319) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(2400 <= id && id <= 2401) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2402 <= id && id <= 2402) ListBlockFar.Add(new Block(id, x, y, 1));
                    if(2403 <= id && id <= 2408) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2416 <= id && id <= 2419) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(2500 <= id && id <= 2501) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2502 <= id && id <= 2504) ListBlockFar.Add(new Block(id, x, y, 1));
                    if(2505 <= id && id <= 2508) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2518 <= id && id <= 2519) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(2600 <= id && id <= 2601) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2602 <= id && id <= 2604) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(2605 <= id && id <= 2608) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2610 <= id && id <= 2610) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2611 <= id && id <= 2611) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2612 <= id && id <= 2612) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2613 <= id && id <= 2613) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2618 <= id && id <= 2619) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(2700 <= id && id <= 2701) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2702 <= id && id <= 2704) ListBlockFar.Add(new Block(id, x, y, 1));
                    if(2705 <= id && id <= 2708) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2718 <= id && id <= 2719) ListBlockFar.Add(new Block(id, x, y, 2));
                    if(2800 <= id && id <= 2819) ListBlockFar.Add(new Block(id, x, y, 0));
                    if(2900 <= id && id <= 2904) ListBlockFar.Add(new Block(id, x, y, 1));
                    if(2905 <= id && id <= 2919) ListBlockFar.Add(new Block(id, x, y, 0));
                    x++;
                }
                y++;
            }

            y = 0;
            foreach(List<int> row in tileMap2) {
                x = 0;
                foreach(int id in row) {
                    if(0000 <= id && id <= 0000) { }
                    if(0001 <= id && id <= 0001) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0002 <= id && id <= 0006) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(0007 <= id && id <= 0007) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0008 <= id && id <= 0019) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(0100 <= id && id <= 0100) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0101 <= id && id <= 0119) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(0200 <= id && id <= 0200) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0201 <= id && id <= 0206) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(0207 <= id && id <= 0207) ListBlockMiddle.Add(new Block(id, x, y, 1));
                    if(0208 <= id && id <= 0219) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(0300 <= id && id <= 0300) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0301 <= id && id <= 0304) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(0305 <= id && id <= 0307) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0308 <= id && id <= 0319) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(0400 <= id && id <= 0401) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0402 <= id && id <= 0406) ListBlockMiddle.Add(new Block(id, x, y, 1));
                    if(0407 <= id && id <= 0407) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0408 <= id && id <= 0419) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(0500 <= id && id <= 0500) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0501 <= id && id <= 0501) ListBlockMiddle.Add(new Block(id, x, y, 1));
                    if(0502 <= id && id <= 0505) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0506 <= id && id <= 0507) ListBlockMiddle.Add(new Block(id, x, y, 1));
                    if(0508 <= id && id <= 0519) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(0600 <= id && id <= 0600) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(0601 <= id && id <= 0605) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0606 <= id && id <= 0606) ListBlockMiddle.Add(new Block(id, x, y, 1));
                    if(0607 <= id && id <= 0607) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0608 <= id && id <= 0619) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(0700 <= id && id <= 0700) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(0701 <= id && id <= 0701) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0702 <= id && id <= 0702) ListBlockMiddle.Add(new Block(id, x, y, 1));
                    if(0703 <= id && id <= 0707) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(0708 <= id && id <= 1307) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(1308 <= id && id <= 1311) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(1312 <= id && id <= 1406) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(1407 <= id && id <= 1411) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(1412 <= id && id <= 1506) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(1507 <= id && id <= 1507) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(1508 <= id && id <= 1511) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(1512 <= id && id <= 1607) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(1608 <= id && id <= 1611) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(1612 <= id && id <= 1707) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(1708 <= id && id <= 1709) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(1712 <= id && id <= 1807) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(1808 <= id && id <= 1809) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(1812 <= id && id <= 1814) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(1816 <= id && id <= 1818) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(1900 <= id && id <= 1907) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(1908 <= id && id <= 1909) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(1913 <= id && id <= 1914) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(1917 <= id && id <= 1918) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    //Tree
                    if(2000 <= id && id <= 2002) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2005 <= id && id <= 2008) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2016 <= id && id <= 2019) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(2100 <= id && id <= 2102) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2103 <= id && id <= 2103) ListBlockMiddle.Add(new Block(id, x, y, 1));
                    if(2105 <= id && id <= 2108) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2116 <= id && id <= 2119) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(2200 <= id && id <= 2202) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2203 <= id && id <= 2204) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(2205 <= id && id <= 2208) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2216 <= id && id <= 2219) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(2300 <= id && id <= 2302) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2303 <= id && id <= 2304) ListBlockMiddle.Add(new Block(id, x, y, 1));
                    if(2305 <= id && id <= 2308) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2316 <= id && id <= 2319) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(2400 <= id && id <= 2401) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2402 <= id && id <= 2402) ListBlockMiddle.Add(new Block(id, x, y, 1));
                    if(2403 <= id && id <= 2408) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2416 <= id && id <= 2419) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(2500 <= id && id <= 2501) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2502 <= id && id <= 2504) ListBlockMiddle.Add(new Block(id, x, y, 1));
                    if(2505 <= id && id <= 2508) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2518 <= id && id <= 2519) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(2600 <= id && id <= 2601) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2602 <= id && id <= 2604) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(2605 <= id && id <= 2608) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2610 <= id && id <= 2610) ListSpike.Add(new Block(id, x, y, 0));
                    if(2611 <= id && id <= 2611) ListSpike.Add(new Block(id, x, y, 1));
                    if(2612 <= id && id <= 2612) ListSpike.Add(new Block(id, x, y, 2));
                    if(2613 <= id && id <= 2613) ListSpike.Add(new Block(id, x, y, 3));
                    if(2618 <= id && id <= 2619) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(2700 <= id && id <= 2701) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2702 <= id && id <= 2704) ListBlockMiddle.Add(new Block(id, x, y, 1));
                    if(2705 <= id && id <= 2708) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2718 <= id && id <= 2719) ListBlockMiddle.Add(new Block(id, x, y, 2));
                    if(2800 <= id && id <= 2819) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    if(2900 <= id && id <= 2904) ListBlockMiddle.Add(new Block(id, x, y, 1));
                    if(2905 <= id && id <= 2919) ListBlockMiddle.Add(new Block(id, x, y, 0));
                    x++;
                }
                y++;
            }

            y = 0;
            foreach(List<int> row in tileMap3) {
                x = 0;
                foreach(int id in row) {
                    if(0000 <= id && id <= 0000) { }
                    if(0001 <= id && id <= 0001) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0002 <= id && id <= 0006) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(0007 <= id && id <= 0007) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0008 <= id && id <= 0019) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(0100 <= id && id <= 0100) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0101 <= id && id <= 0119) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(0200 <= id && id <= 0200) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0201 <= id && id <= 0206) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(0207 <= id && id <= 0207) ListBlockNear.Add(new Block(id, x, y, 1));
                    if(0208 <= id && id <= 0219) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(0300 <= id && id <= 0300) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0301 <= id && id <= 0304) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(0305 <= id && id <= 0307) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0308 <= id && id <= 0319) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(0400 <= id && id <= 0401) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0402 <= id && id <= 0406) ListBlockNear.Add(new Block(id, x, y, 1));
                    if(0407 <= id && id <= 0407) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0408 <= id && id <= 0419) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(0500 <= id && id <= 0500) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0501 <= id && id <= 0501) ListBlockNear.Add(new Block(id, x, y, 1));
                    if(0502 <= id && id <= 0505) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0506 <= id && id <= 0507) ListBlockNear.Add(new Block(id, x, y, 1));
                    if(0508 <= id && id <= 0519) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(0600 <= id && id <= 0600) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(0601 <= id && id <= 0605) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0606 <= id && id <= 0606) ListBlockNear.Add(new Block(id, x, y, 1));
                    if(0607 <= id && id <= 0607) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0608 <= id && id <= 0619) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(0700 <= id && id <= 0700) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(0701 <= id && id <= 0701) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0702 <= id && id <= 0702) ListBlockNear.Add(new Block(id, x, y, 1));
                    if(0703 <= id && id <= 0707) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(0708 <= id && id <= 1307) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(1308 <= id && id <= 1311) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(1312 <= id && id <= 1406) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(1407 <= id && id <= 1411) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(1412 <= id && id <= 1506) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(1507 <= id && id <= 1507) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(1508 <= id && id <= 1511) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(1512 <= id && id <= 1607) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(1608 <= id && id <= 1611) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(1612 <= id && id <= 1707) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(1708 <= id && id <= 1709) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(1712 <= id && id <= 1807) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(1808 <= id && id <= 1809) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(1812 <= id && id <= 1814) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(1816 <= id && id <= 1818) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(1900 <= id && id <= 1907) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(1908 <= id && id <= 1909) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(1913 <= id && id <= 1914) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(1917 <= id && id <= 1918) ListBlockNear.Add(new Block(id, x, y, 2));
                    //Tree
                    if(2000 <= id && id <= 2002) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2005 <= id && id <= 2008) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2016 <= id && id <= 2019) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(2100 <= id && id <= 2102) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2103 <= id && id <= 2103) ListBlockNear.Add(new Block(id, x, y, 1));
                    if(2105 <= id && id <= 2108) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2116 <= id && id <= 2119) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(2200 <= id && id <= 2202) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2203 <= id && id <= 2204) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(2205 <= id && id <= 2208) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2216 <= id && id <= 2219) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(2300 <= id && id <= 2302) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2303 <= id && id <= 2304) ListBlockNear.Add(new Block(id, x, y, 1));
                    if(2305 <= id && id <= 2308) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2316 <= id && id <= 2319) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(2400 <= id && id <= 2401) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2402 <= id && id <= 2402) ListBlockNear.Add(new Block(id, x, y, 1));
                    if(2403 <= id && id <= 2408) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2416 <= id && id <= 2419) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(2500 <= id && id <= 2501) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2502 <= id && id <= 2504) ListBlockNear.Add(new Block(id, x, y, 1));
                    if(2505 <= id && id <= 2508) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2518 <= id && id <= 2519) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(2600 <= id && id <= 2601) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2602 <= id && id <= 2604) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(2605 <= id && id <= 2608) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2610 <= id && id <= 2610) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2611 <= id && id <= 2611) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2612 <= id && id <= 2612) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2613 <= id && id <= 2613) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2618 <= id && id <= 2619) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(2700 <= id && id <= 2701) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2702 <= id && id <= 2704) ListBlockNear.Add(new Block(id, x, y, 1));
                    if(2705 <= id && id <= 2708) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2718 <= id && id <= 2719) ListBlockNear.Add(new Block(id, x, y, 2));
                    if(2800 <= id && id <= 2819) ListBlockNear.Add(new Block(id, x, y, 0));
                    if(2900 <= id && id <= 2904) ListBlockNear.Add(new Block(id, x, y, 1));
                    if(2905 <= id && id <= 2919) ListBlockNear.Add(new Block(id, x, y, 0));
                    x++;
                }
                y++;
            }

            y = 0;
            foreach(List<int> row in tileMap4) {
                x = 0;
                foreach(int id in row) {
                    if(id == 0000) { }
                    if(id == 0001)
                        Malice = new Monster("malice", 2, x, y, 100, Shopkeeper.speed_walk, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false);

                    if(id == 0002) Exit = new Entity("exit", x, y, 100);
                    if(id == 0003) ListCoin.Add(new Entity("coin", x, y, 1));
                    if(id == 0004) ListPlatform.Add(new Monster("platform", 1, x, y, 0, Shopkeeper.speed_walk, Shopkeeper.speed_fall, Shopkeeper.speed_jump, true));
                    if(id == 0005) ListPlatform.Add(new Monster("platform", 1, x, y, 1, Shopkeeper.speed_walk, Shopkeeper.speed_fall, Shopkeeper.speed_jump, true));
                    if(id == 0010) ListStopper.Add(new Block(0, x, y, 2));

                    if(id == 0100) ListMonster.Add(new Monster("bat", 1, x, y, 10, Shopkeeper.speed_walk, Shopkeeper.speed_fall, Shopkeeper.speed_jump, true));
                    if(id == 0101) ListMonster.Add(new Monster("snake", 1, x, y, 10, Shopkeeper.speed_walk, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    if(id == 0102) ListMonster.Add(new Monster("lizard", 1, x, y, 10, Shopkeeper.speed_walk, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    if(id == 0103) ListMonster.Add(new Monster("eyeball", 1, x, y, 10, Shopkeeper.speed_walk, Shopkeeper.speed_fall, Shopkeeper.speed_jump, true));
                    if(id == 0104) ListMonster.Add(new Monster("driller", 1, x, y, 10, Shopkeeper.speed_walk, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    if(id == 0105) ListMonster.Add(new Monster("flybot", 1, x, y, 10, Shopkeeper.speed_walk, Shopkeeper.speed_fall, Shopkeeper.speed_jump, true));
                    if(id == 0106) ListMonster.Add(new Monster("zylon", 1, x, y, 10, Shopkeeper.speed_walk, Shopkeeper.speed_fall, Shopkeeper.speed_jump, true));
                    if(id == 0107) ListMonster.Add(new Monster("sentry", 1, x, y, 10, Shopkeeper.speed_walk, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));

                    if(id == 0200) ListBeam.Add(new Entity("beam", x, y, 1));
                    if(id == 0201) ListBeam.Add(new Entity("beam", x, y, 2));
                    if(id == 0202) ListBeam.Add(new Entity("beam", x, y, 3));
                    if(id == 0203) ListBeam.Add(new Entity("beam", x, y, 4));
                    if(id == 0204) ListBeam.Add(new Entity("beam", x, y, 5));
                    if(id == 0205) ListBeam.Add(new Entity("beam", x, y, 6));
                    if(id == 0206) ListBeam.Add(new Entity("beam", x, y, 7));
                    if(id == 0207) ListBeam.Add(new Entity("beam", x, y, 8));
                    if(id == 0208) ListBeam.Add(new Entity("beam", x, y, 9));
                    if(id == 0209) ListBeam.Add(new Entity("beam", x, y, 10));
                    if(id == 0210) ListBeam.Add(new Entity("beam", x, y, 11));

                    if(id == 0300) ListTransponder.Add(new Entity("transponder", x, y, 1));
                    if(id == 0301) ListTransponder.Add(new Entity("transponder", x, y, 2));
                    if(id == 0302) ListTransponder.Add(new Entity("transponder", x, y, 3));
                    if(id == 0303) ListTransponder.Add(new Entity("transponder", x, y, 4));
                    if(id == 0304) ListTransponder.Add(new Entity("transponder", x, y, 5));
                    if(id == 0305) ListTransponder.Add(new Entity("transponder", x, y, 6));
                    if(id == 0306) ListTransponder.Add(new Entity("transponder", x, y, 7));
                    if(id == 0307) ListTransponder.Add(new Entity("transponder", x, y, 8));
                    if(id == 0308) ListTransponder.Add(new Entity("transponder", x, y, 9));
                    if(id == 0309) ListTransponder.Add(new Entity("transponder", x, y, 10));
                    if(id == 0310) ListTransponder.Add(new Entity("transponder", x, y, 11));

                    if(id == 0400) ListMino.Add(new Monster("mino", 2, x, y, 1, 0, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    if(id == 0401) ListMino.Add(new Monster("mino", 2, x, y, 2, 0, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    if(id == 0402) ListMino.Add(new Monster("mino", 2, x, y, 3, 0, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    if(id == 0403) ListMino.Add(new Monster("mino", 2, x, y, 4, 0, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    if(id == 0404) ListMino.Add(new Monster("mino", 2, x, y, 5, 0, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    if(id == 0405) ListMino.Add(new Monster("mino", 2, x, y, 6, 0, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    if(id == 0406) ListMino.Add(new Monster("mino", 2, x, y, 7, 0, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    if(id == 0407) ListMino.Add(new Monster("mino", 2, x, y, 8, 0, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    if(id == 0408) ListMino.Add(new Monster("mino", 2, x, y, 9, 0, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    if(id == 0409) ListMino.Add(new Monster("mino", 2, x, y, 10, 0, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    if(id == 0410) ListMino.Add(new Monster("mino", 2, x, y, 11, 0, Shopkeeper.speed_fall, Shopkeeper.speed_jump, false));
                    x++;
                }
                y++;
            }
            timer = true;
        }

        private void Time(GameTime gameTime) {
            score_time_system = gameTime.TotalGameTime.TotalSeconds;
            timer = false;
        }

        public string Update(GameTime gameTime) {
            if(level_won2 && !score_submitted && exitreached) {
                if(editored) {
                    Filemanager.Set_Highscore("editor", act, score_final + (int)score_time_now * 10);
                } else {
                    Filemanager.Set_Highscore(world, act, score_final + (int)score_time_now * 10);
                }
                score_submitted = true;
            }
            if(timer) Time(gameTime);
            control_keyboard_new = Keyboard.GetState();
            control_mouse_new = Mouse.GetState();
            control_touch = TouchPanel.GetState();
            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.Pinch | GestureType.PinchComplete;
            dragging = false;
            if(!level_won1 && !level_won2) {
                if(control_keyboard_new.IsKeyDown(Keys.F4)) {
                    return "exit";
                }
                if(control_keyboard_new.IsKeyDown(Keys.Escape)) {
                    return "menu";
                }
                if(control_keyboard_new.IsKeyDown(Keys.Left)) {
                    if(show_touch) show_touch = false;
                    Malice.set_Movement("left");
                    Malice.set_Facing_Right(false);
                } else if(control_keyboard_new.IsKeyDown(Keys.Right)) {
                    if(show_touch) show_touch = false;
                    Malice.set_Movement("right");
                    Malice.set_Facing_Right(true);
                } else {
                    Malice.set_Movement("still");
                }
                if(control_keyboard_new.IsKeyDown(Keys.Down)) {
                    if(show_touch) show_touch = false;
                    malice_looking_down = true;
                    malice_looking_up = false;
                } else
                if(control_keyboard_new.IsKeyDown(Keys.Up)) {
                    if(show_touch) show_touch = false;
                    malice_looking_down = false;
                    malice_looking_up = true;
                }// else {
                 // malice_looking_down = false;
                 // malice_looking_up   = false;
                 //}
                if(control_keyboard_new.IsKeyDown(Keys.Space) && !Malice.get_is_Jumping() && !Malice.get_is_Falling()) {
                    if(show_touch) show_touch = false;
                    Malice.set_is_Jumping(true);
                    jumper_point = Malice.Get_PosY();
                    sound_jump.Play();
                }
                if(control_keyboard_new.IsKeyDown(Keys.LeftControl) && control_keyboard_old.IsKeyUp(Keys.LeftControl)) {
                    if(show_touch) show_touch = false;
                    Spawn_Shot(Malice);
                }
                if(control_keyboard_new.IsKeyDown(Keys.RightControl) && control_keyboard_old.IsKeyUp(Keys.RightControl)) {
                    if(show_touch) show_touch = false;
                    Spawn_Shot(Malice);
                }
            }

            if(control_mouse_new.Position != control_mouse_old.Position) {
                if(!show_touch) show_touch = true;
                buttonanimation_timer = 100;
                buttonanimation_alpha = 1.0f;
            }

            if(control_mouse_new.ScrollWheelValue < control_mouse_old.ScrollWheelValue) {
                if(buttonanimation_scale > 100) buttonanimation_scale--;
            }

            if(control_mouse_new.ScrollWheelValue > control_mouse_old.ScrollWheelValue) {
                if(buttonanimation_scale < 500) buttonanimation_scale++;
            }

            if(control_mouse_new.LeftButton == ButtonState.Pressed) {
                Command_Cursor(control_mouse_new.Position.X / scaleX, control_mouse_new.Position.Y / scaleY, "move");
            }

            if(level_won2) {
                if(control_keyboard_new.IsKeyDown(Keys.Enter))
                    return "menu";
                if(control_keyboard_new.IsKeyDown(Keys.Escape))
                    return "menu";
                if(control_keyboard_new.IsKeyDown(Keys.Space))
                    return "menu";
                if(control_mouse_new.LeftButton == ButtonState.Pressed)
                    return "menu";
            }

            TouchCollection touches = TouchPanel.GetState();

            for(int i = 0; i < touches.Count; i++) {
                Vector2 position = touches[i].Position;
                int mX = (int)(position.X / scaleX);
                int mY = (int)(position.Y / scaleY);
                if(!show_touch) show_touch = true;
                buttonanimation_timer = 100;
                buttonanimation_alpha = 1.0f;
                Command_Cursor(mX, mY, "move");
            }

            while(TouchPanel.IsGestureAvailable) {
                var gesture = TouchPanel.ReadGesture();
                if(gesture.GestureType == GestureType.Pinch) {
                    Vector2 oldPosition1 = gesture.Position - gesture.Delta;
                    Vector2 oldPosition2 = gesture.Position2 - gesture.Delta2;
                    float newDistance = Vector2.Distance(gesture.Position, gesture.Position2);
                    float oldDistance = Vector2.Distance(oldPosition1, oldPosition2);
                    float scaleFactor = newDistance / oldDistance;
                    if(scaleFactor < 1.0F) {
                        if(buttonanimation_scale > 100) buttonanimation_scale--;
                    }
                    if(scaleFactor > 1.0F) {
                        if(buttonanimation_scale < 500) buttonanimation_scale++;
                    }
                }
                if(gesture.GestureType == GestureType.PinchComplete) {

                }
                if(gesture.GestureType == GestureType.Tap) {
                    if(level_won2) {
                        return "menu";
                    } else {
                        int mX = (int)(gesture.Position.X / scaleX);
                        int mY = (int)(gesture.Position.Y / scaleY);
                        Command_Cursor(mX, mY, "fire");
                    }
                }
            }

            for(int i = 0; i < 11; i++) {
                active_beam[i] = true;
            }

            foreach(Entity x in ListTransponder) {
                foreach(Monster mino in ListMino) {
                    if(x.Get_Value() == mino.Get_Value()) {
                        if(mino.Get_PosX() > x.Get_PosX() - 16 && x.Get_PosX() + 16 > mino.Get_PosX()) {
                            if(mino.Get_PosY() > x.Get_PosY() - 16 && x.Get_PosY() + 16 > mino.Get_PosY()) {
                                active_beam[mino.Get_Value() - 1] = false;
                            }
                        }
                    }
                }
            }

            foreach(Monster mino in ListMino) {
                mino.Set_Touch_Wall("void");
                bool fallbreak0 = false;
                Rectangle rect = new Rectangle(0,0,64,64);
                if(mino.Get_PosY() > 40 * 32 + 100) {
                    ListMino.Remove(mino);
                    break;
                }
                foreach(Monster platform in ListPlatform) {
                    int i = Collisioncourse.Detection_Mino(Malice.Get_ID(),(int)Malice.Get_PosX(),(int)Malice.Get_PosY(), collision_malice, platform.Get_ID(),(int)platform.Get_PosX(),(int)platform.Get_PosY(),collision_platform, Malice.get_is_Falling());
                    if(i == 10) {
                        fallbreak0 = true;
                    }
                    if(i > 10) {
                        fallbreak0 = true;
                        mino.Set_PosY(mino.Get_PosY() - (i - 10 - 2));
                    }
                }
                foreach(Monster mino2 in ListMino) {
                    if(mino.Get_PosX() != mino2.Get_PosX()) {
                        int i = Collisioncourse.Detection_Mino(mino.Get_ID(),(int)mino.Get_PosX(),(int)mino.Get_PosY(), rect, mino2.Get_ID(),(int)mino2.Get_PosX(),(int)mino2.Get_PosY(), rect, mino.get_is_Falling());
                        if(i == 3) {
                            mino.Set_Touch_Wall("left");
                        }
                        if(i == 4) {
                            mino.Set_Touch_Wall("right");
                        }
                        if(i == 10) {
                            fallbreak0 = true;
                        }
                        if(i > 10) {
                            fallbreak0 = true;
                            mino.Set_PosY(mino.Get_PosY() - (i - 10 - 2));
                        }
                    }
                }
                foreach(Block spike in ListSpike) {
                    int i = Collisioncourse.Detection_Tile(mino.Get_ID(),(int)mino.Get_PosX(),(int)mino.Get_PosY(), rect, spike.Get_ID(),(int)spike.Get_PosX(),(int)spike.Get_PosY(), mino.get_is_Falling());

                    if(i == 2) {

                    }
                    if(i == 3) {
                        mino.Set_Touch_Wall("left");
                    }
                    if(i == 4) {
                        mino.Set_Touch_Wall("right");
                    }
                    if(spike.Get_Is_Solid() == 0 || spike.Get_Is_Solid() == 1) {
                        if(i == 10) {
                            fallbreak0 = true;
                        }
                        if(i > 10) {
                            fallbreak0 = true;
                            mino.Set_PosY(mino.Get_PosY() - (i - 10 - 2));
                        }
                    }
                }
                foreach(Entity beam in ListBeam) {
                    if(Get_BeamColor(beam.Get_Value()) != 0) {
                        int i = Collisioncourse.Detection_Mino(Malice.Get_ID(),(int)mino.Get_PosX(),(int)mino.Get_PosY(), rect, beam.Get_ID(),(int)beam.Get_PosX(),(int)beam.Get_PosY(), collision_beam, Malice.get_is_Falling());
                        if(i == 2) {
                            jumper_point = 9999;
                        }
                        if(i == 3) {
                            mino.Set_Touch_Wall("left");
                        }
                        if(i == 4) {
                            mino.Set_Touch_Wall("right");
                        }
                        if(i == 10) {
                            fallbreak0 = true;
                        }
                        if(i > 10) {
                            fallbreak0 = true;
                            mino.Set_PosY(mino.Get_PosY() - (i - 10 - 2));
                        }
                    }
                }
                foreach(Block block in ListBlockMiddle) {
                    int i = Collisioncourse.Detection_Tile(mino.Get_ID(),(int)mino.Get_PosX(),(int)mino.Get_PosY(), rect, block.Get_ID(),(int)block.Get_PosX(),(int)block.Get_PosY(), mino.get_is_Falling());

                    if(i == 2) {

                    }
                    if(i == 3) {
                        if(block.Get_Is_Solid() == 0) {
                            mino.Set_Touch_Wall("left");
                        }
                    }
                    if(i == 4) {
                        if(block.Get_Is_Solid() == 0) {
                            mino.Set_Touch_Wall("right");
                        }
                    }
                    if(block.Get_Is_Solid() == 0 || block.Get_Is_Solid() == 1) {
                        if(i == 10) {
                            fallbreak0 = true;
                        }
                        if(i > 10) {
                            fallbreak0 = true;
                            mino.Set_PosY(mino.Get_PosY() - (i - 10 - 2));
                        }
                    }
                }
                if(fallbreak0) {
                    mino.set_is_Falling(false);
                } else {
                    mino.set_is_Falling(true);
                }
                mino.Update();
            }

            foreach(Monster x in ListPlatform) {
                Rectangle rect = collision_platform;
                foreach(Monster mino in ListMino) {
                    int i = Collisioncourse.Detection_Mino(x.Get_ID(),(int)x.Get_PosX(),(int)x.Get_PosY(), rect, mino.Get_ID(),(int)mino.Get_PosX(),(int)mino.Get_PosY(), collision_mino, Malice.get_is_Falling());
                    if(i == 2) { if(!x.get_Facing_Right()) { x.set_Facing_Right(true); x.set_Movement("right"); } }
                    if(i == 3) { if(!x.get_Facing_Right()) { x.set_Facing_Right(true); x.set_Movement("right"); } }
                    if(i == 4) { if(x.get_Facing_Right()) { x.set_Facing_Right(false); x.set_Movement("left"); } }
                    if(i >= 10) { if(x.get_Facing_Right()) { x.set_Facing_Right(false); x.set_Movement("left"); } }
                }
                foreach(Block e in ListStopper) {
                    if(Collisioncourse.Detection_Entity("stopper", (int)e.Get_PosX(), (int)e.Get_PosY(), 0, new Rectangle(0, 0, 64, 64), x.Get_ID(), (int)x.Get_PosX(), (int)x.Get_PosY(), 0, rect, core_frame_index)) {
                        if(x.get_Facing_Right() && x.Get_PosX() < e.Get_PosX()) {
                            x.set_Movement("left");
                            x.set_Facing_Right(false);
                        } else if(!x.get_Facing_Right() && x.Get_PosX() > e.Get_PosX()) {
                            x.set_Movement("right");
                            x.set_Facing_Right(true);
                        } else if(x.get_Facing_Right() && x.Get_PosY() < e.Get_PosY()) {
                            x.set_Movement("left");
                            x.set_Facing_Right(false);
                        } else if(!x.get_Facing_Right() && x.Get_PosY() > e.Get_PosY()) {
                            x.set_Movement("right");
                            x.set_Facing_Right(true);
                        }
                    }
                }
                foreach(Entity beam in ListBeam) {
                    if(Get_BeamColor(beam.Get_Value()) != 0) {
                        int i = Collisioncourse.Detection_Mino(Malice.Get_ID(),(int)x.Get_PosX(),(int)x.Get_PosY(), rect, beam.Get_ID(),(int)beam.Get_PosX(),(int)beam.Get_PosY(), collision_beam, Malice.get_is_Falling());
                        if(i == 2) { if(!x.get_Facing_Right()) { x.set_Facing_Right(true); x.set_Movement("right"); } }
                        if(i == 3) { if(!x.get_Facing_Right()) { x.set_Facing_Right(true); x.set_Movement("right"); } }
                        if(i == 4) { if(x.get_Facing_Right()) { x.set_Facing_Right(false); x.set_Movement("left"); } }
                        if(i >= 10) { if(x.get_Facing_Right()) { x.set_Facing_Right(false); x.set_Movement("left"); } }
                    }
                }
                //foreach(Block spike in ListSpike) {
                //    int i = Collisioncourse.Detection_Tile(x.Get_ID(),(int)x.Get_PosX(),(int)x.Get_PosY(), rect, spike.Get_ID(),(int)spike.Get_PosX(),(int)spike.Get_PosY(), x.get_is_Falling());
                //    if(i == 2) { if(!x.get_Facing_Right()) { x.set_Facing_Right(true); x.set_Movement("right"); } }
                //    if(i == 3) { if(!x.get_Facing_Right()) { x.set_Facing_Right(true); x.set_Movement("right"); } }
                //    if(i == 4) { if(x.get_Facing_Right()) { x.set_Facing_Right(false); x.set_Movement("left"); } }
                //    if(i >= 10) { if(x.get_Facing_Right()) { x.set_Facing_Right(false); x.set_Movement("left"); } }
                //}
                //foreach(Block block in ListBlockMiddle) {
                //    int i = Collisioncourse.Detection_Tile(x.Get_ID(),(int)x.Get_PosX(),(int)x.Get_PosY(), rect, block.Get_ID(),(int)block.Get_PosX(),(int)block.Get_PosY(), x.get_is_Falling());
                //    if(i == 2) { if(!x.get_Facing_Right()) { x.set_Facing_Right(true); x.set_Movement("right"); } }
                //    if(i == 3) { if(!x.get_Facing_Right()) { x.set_Facing_Right(true); x.set_Movement("right"); } }
                //    if(i == 4) { if(x.get_Facing_Right()) { x.set_Facing_Right(false); x.set_Movement("left"); } }
                //    if(i >= 10) { if(x.get_Facing_Right()) { x.set_Facing_Right(false); x.set_Movement("left"); } }
                //}
                x.Update();
            }

            bool fallbreak1 = false;
            Malice.Set_Draggin(false);
            foreach(Block block in ListBlockMiddle) {
                if(Malice.Get_HP() > 0 && block.Get_Is_Solid() != 2) {
                    int i = Collisioncourse.Detection_Tile(Malice.Get_ID(),(int)Malice.Get_PosX(),(int)Malice.Get_PosY(), collision_malice, block.Get_ID(),(int)block.Get_PosX(),(int)block.Get_PosY(), Malice.get_is_Falling());
                    if(i == 2) {
                        if(block.Get_Is_Solid() == 0) {
                            jumper_point = 9999;
                        }
                    }
                    if(i == 3) {
                        if(block.Get_Is_Solid() == 0) {
                            if(Malice.get_Movement() == "left")
                                Malice.set_Movement("still");
                        }
                    }
                    if(i == 4) {
                        if(block.Get_Is_Solid() == 0) {
                            if(Malice.get_Movement() == "right")
                                Malice.set_Movement("still");
                        }
                    }
                    if(i == 10) {
                        fallbreak1 = true;
                    }
                    if(i > 10) {
                        fallbreak1 = true;
                        Malice.Set_PosY(Malice.Get_PosY() - (i - 10 - 2));
                    }
                }
            }
            foreach(Monster platform in ListPlatform) {
                int i = Collisioncourse.Detection_Platform(Malice.Get_ID(),(int)Malice.Get_PosX(),(int)Malice.Get_PosY(), collision_malice, platform.Get_ID(),(int)platform.Get_PosX(),(int)platform.Get_PosY(),collision_platform, Malice.get_is_Falling());
                if(i >= 10) {
                    fallbreak1 = true;
                    Malice.Set_PosY(Malice.Get_PosY() - (i - 10 - 2));
                    if(platform.get_Movement() == "left" && platform.Get_Value() == 0) {
                        Malice.Set_PosY(Malice.Get_PosY() - 6);
                    } else if(platform.get_Movement() == "right" && platform.Get_Value() == 0) {
                        Malice.Set_PosY(Malice.Get_PosY() + 6);
                    } else if(platform.get_Movement() == "left" && platform.Get_Value() == 1) {
                        Malice.Set_PosX(Malice.Get_PosX() - 6);
                    } else if(platform.get_Movement() == "right" && platform.Get_Value() == 1) {
                        Malice.Set_PosX(Malice.Get_PosX() + 6);
                    }
                }
            }
            foreach(Monster mino in ListMino) {
                int i = Collisioncourse.Detection_Mino(Malice.Get_ID(),(int)Malice.Get_PosX(),(int)Malice.Get_PosY(), collision_malice, mino.Get_ID(),(int)mino.Get_PosX(),(int)mino.Get_PosY(), collision_mino, Malice.get_is_Falling());
                if(i == 2) {
                    jumper_point = 9999;
                }
                if(i == 3) {
                    if(Malice.get_Movement() == "left") {
                        dragging = true;
                        if(mino.Get_Touch_Wall() == "left") {
                            Malice.set_Movement("still");
                        } else {
                            mino.Move("left", 1);
                            Malice.Set_Draggin(true);
                        }
                    }
                }
                if(i == 4) {
                    if(Malice.get_Movement() == "right") {
                        dragging = true;
                        if(mino.Get_Touch_Wall() == "right") {
                            Malice.set_Movement("still");
                        } else {
                            mino.Move("right", 1);
                            Malice.Set_Draggin(true);
                        }
                    }
                }
                if(i == 10) {
                    fallbreak1 = true;
                }
                if(i > 10) {
                    fallbreak1 = true;
                    Malice.Set_PosY(Malice.Get_PosY() - (i - 10 - 2));
                }
            }

            foreach(Entity beam in ListBeam) {
                if(Get_BeamColor(beam.Get_Value()) != 0) {
                    int i = Collisioncourse.Detection_Mino(Malice.Get_ID(),(int)Malice.Get_PosX(),(int)Malice.Get_PosY(), collision_malice, beam.Get_ID(),(int)beam.Get_PosX(),(int)beam.Get_PosY(), collision_beam, Malice.get_is_Falling());
                    if(i == 2) {
                        jumper_point = 9999;
                    }
                    if(i == 3) {
                        if(Malice.get_Movement() == "left")
                            Malice.set_Movement("still");
                    }
                    if(i == 4) {
                        if(Malice.get_Movement() == "right")
                            Malice.set_Movement("still");
                    }
                    if(i == 10) {
                        fallbreak1 = true;
                    }
                    if(i > 10) {
                        fallbreak1 = true;
                        Malice.Set_PosY(Malice.Get_PosY() - (i - 10 - 2));
                    }
                }
            }

            // 0 - Down
            // 1 - Left
            // 2 - Up
            // 3 - Right
            foreach(Block spike in ListSpike) {
                int i = Collisioncourse.Detection_Mino(Malice.Get_ID(),(int)Malice.Get_PosX(),(int)Malice.Get_PosY(), collision_malice, "spike",(int)spike.Get_PosX(),(int)spike.Get_PosY(), new Rectangle(0,0,32,32), Malice.get_is_Falling());
                if(i == 2) {
                    if(spike.Get_Is_Solid() == 0) {
                        if(!malice_damaged) {
                            malice_damaged = true;
                            Malice.Set_HP(Malice.Get_HP() - 1);
                            Malice.set_is_Jumping(true);
                            jumper_point = Malice.Get_PosY();
                            sound_damage.Play();
                        }
                    } else {
                        jumper_point = 9999;
                    }
                }
                if(i == 3) {
                    if(spike.Get_Is_Solid() == 3) {
                        if(!malice_damaged) {
                            malice_damaged = true;
                            Malice.set_Movement("right");
                            Malice.Set_HP(Malice.Get_HP() - 1);
                            Malice.set_is_Jumping(true);
                            jumper_point = Malice.Get_PosY();
                            sound_damage.Play();
                        }
                    } else {
                        if(Malice.get_Movement() == "left")
                            Malice.set_Movement("still");
                    }
                }
                if(i == 4) {
                    if(spike.Get_Is_Solid() == 1) {
                        if(!malice_damaged) {
                            malice_damaged = true;
                            Malice.set_Movement("left");
                            Malice.Set_HP(Malice.Get_HP() - 1);
                            Malice.set_is_Jumping(true);
                            jumper_point = Malice.Get_PosY();
                            sound_damage.Play();
                        }
                    } else {
                        if(Malice.get_Movement() == "right")
                            Malice.set_Movement("still");
                    }
                }
                if(i == 10) {
                    if(spike.Get_Is_Solid() == 2) {
                        if(!malice_damaged) {
                            malice_damaged = true;
                            Malice.Set_HP(Malice.Get_HP() - 1);
                            Malice.set_is_Jumping(true);
                            jumper_point = Malice.Get_PosY();
                            sound_damage.Play();
                        }
                    } else {
                        fallbreak1 = true;
                    }
                }
                if(i > 10) {
                    if(spike.Get_Is_Solid() == 2) {
                        if(!malice_damaged) {
                            malice_damaged = true;
                            Malice.Set_HP(Malice.Get_HP() - 1);
                            Malice.set_is_Jumping(true);
                            jumper_point = Malice.Get_PosY();
                            sound_damage.Play();
                        }
                    } else {
                        fallbreak1 = true;
                        Malice.Set_PosY(Malice.Get_PosY() - (i - 10 - 2));
                    }
                }
            }
            if(fallbreak1) {
                if(level_won1) {
                    level_won2 = true;
                }
                Malice.set_is_Falling(false);
            } else {
                Malice.set_is_Falling(true);
            }
            if(Malice.Get_PosY() < jumper_point - jumper_height) {
                Malice.set_is_Jumping(false);
                malice_damaged = false;
            }
            //else {
            //    Malice.set_is_Falling(false);
            //}
            //if(Malice.Get_PosY() > 720) {
            //    level_won2 = true;
            //}
            Malice.Update();

            foreach(Monster x in ListShot) {
                Rectangle rect = collision_shot;
                if(x.Get_ID() == "shot_red" && !malice_damaged && !level_won2 && Collisioncourse.Detection_Entity("malice", (int)Malice.Get_PosX(), (int)Malice.Get_PosY(), malice_sprite, collision_malice, x.Get_ID(), (int)x.Get_PosX(), (int)x.Get_PosY(), 0, rect, core_frame_index)) {
                    malice_damaged = true;
                    Malice.Set_HP(Malice.Get_HP() - 1);
                    Malice.set_is_Jumping(true);
                    jumper_point = Malice.Get_PosY();
                    sound_damage.Play();
                    if(Malice.get_Facing_Right()) {
                        Malice.set_Movement("right");
                    } else {
                        Malice.set_Movement("left");
                    }
                    ListShot.Remove(x);
                    break;
                }
                x.Update();
            }

            foreach(Monster x in ListMonster) {
                bool fallbreak3 = false;
                Rectangle rect = new Rectangle(0,0,0,0);
                if(x.Get_ID() == "bat") rect = collision_bat;
                if(x.Get_ID() == "driller") rect = collision_driller;
                if(x.Get_ID() == "eyeball") rect = collision_eyeball;
                if(x.Get_ID() == "flybot") rect = collision_flybot;
                if(x.Get_ID() == "lizard") rect = collision_lizard;
                if(x.Get_ID() == "sentry") rect = collision_sentry;
                if(x.Get_ID() == "shot_violett") rect = collision_shot;
                if(x.Get_ID() == "shot_red") rect = collision_shot;
                if(x.Get_ID() == "snake") rect = collision_snake;
                if(x.Get_ID() == "zylon") rect = collision_zylon;
                if(x.Get_PosY() > 40 * 32 + 100) {
                    ListMonster.Remove(x);
                    break;
                }
                if(!malice_damaged && !level_won2 && Collisioncourse.Detection_Entity("malice", (int)Malice.Get_PosX(), (int)Malice.Get_PosY(), malice_sprite, collision_malice, x.Get_ID(), (int)x.Get_PosX(), (int)x.Get_PosY(), 0, rect, core_frame_index)) {
                    malice_damaged = true;
                    Malice.Set_HP(Malice.Get_HP() - 1);
                    Malice.set_is_Jumping(true);
                    jumper_point = Malice.Get_PosY();
                    sound_damage.Play();
                    if(Malice.get_Facing_Right()) {
                        Malice.set_Movement("right");
                    } else {
                        Malice.set_Movement("left");
                    }
                }
                foreach(Monster platform in ListPlatform) {
                    int i = Collisioncourse.Detection_Mino(x.Get_ID(),(int)x.Get_PosX(),(int)x.Get_PosY(), rect, platform.Get_ID(),(int)platform.Get_PosX(),(int)platform.Get_PosY(),collision_platform, Malice.get_is_Falling());
                    if(i == 10) {
                        fallbreak3 = true;
                    }
                    if(i > 10) {
                        fallbreak3 = true;
                        x.Set_PosY(x.Get_PosY() - (i - 10 - 2));
                    }
                }
                foreach(Monster mino in ListMino) {
                    int i = Collisioncourse.Detection_Mino(x.Get_ID(),(int)x.Get_PosX(),(int)x.Get_PosY(), rect, mino.Get_ID(),(int)mino.Get_PosX(),(int)mino.Get_PosY(), collision_mino, Malice.get_is_Falling());
                    if(i == 2) {
                        jumper_point = 9999;
                    }
                    if(i == 3) {
                        if(!x.get_Facing_Right()) {
                            x.set_Facing_Right(true);
                            x.set_Movement("right");
                        }
                    }
                    if(i == 4) {
                        if(x.get_Facing_Right()) {
                            x.set_Facing_Right(false);
                            x.set_Movement("left");
                        }
                    }
                    if(i == 10) {
                        fallbreak1 = true;
                    }
                    if(i > 10) {
                        fallbreak1 = true;
                        x.Set_PosY(x.Get_PosY() - (i - 10 - 2));
                    }
                }
                foreach(Block e in ListStopper) {
                    if(Collisioncourse.Detection_Entity("stopper", (int)e.Get_PosX(), (int)e.Get_PosY(), 0, new Rectangle(0, 0, 64, 64), x.Get_ID(), (int)x.Get_PosX(), (int)x.Get_PosY(), 0, rect, core_frame_index)) {
                        if(x.get_Facing_Right() && x.Get_PosX() < e.Get_PosX()) {
                            x.set_Movement("left");
                            x.set_Facing_Right(false);
                        } else if(!x.get_Facing_Right() && x.Get_PosX() > e.Get_PosX()) {
                            x.set_Movement("right");
                            x.set_Facing_Right(true);
                        }
                        break;
                    }
                }
                foreach(Entity beam in ListBeam) {
                    if(Get_BeamColor(beam.Get_Value()) != 0) {
                        int i = Collisioncourse.Detection_Mino(Malice.Get_ID(),(int)x.Get_PosX(),(int)x.Get_PosY(), rect, beam.Get_ID(),(int)beam.Get_PosX(),(int)beam.Get_PosY(), collision_beam, Malice.get_is_Falling());
                        if(i == 2) {
                            jumper_point = 9999;
                        }
                        if(i == 3) {
                            if(!x.get_Facing_Right()) {
                                x.set_Facing_Right(true);
                                x.set_Movement("right");
                            }
                        }
                        if(i == 4) {
                            if(x.get_Facing_Right()) {
                                x.set_Facing_Right(false);
                                x.set_Movement("left");
                            }
                        }
                        if(i == 10) {
                            fallbreak3 = true;
                        }
                        if(i > 10) {
                            fallbreak3 = true;
                            x.Set_PosY(x.Get_PosY() - (i - 10 - 2));
                        }
                    }
                }
                foreach(Monster z in ListShot) {
                    if(z.Get_ID() == "shot_violett") {
                        if(!level_won2 && Collisioncourse.Detection_Entity("shot", (int)z.Get_PosX(), (int)z.Get_PosY(), 0, collision_shot, x.Get_ID(), (int)x.Get_PosX(), (int)x.Get_PosY(), 0, rect, core_frame_index)) {
                            ListShot.Remove(z);
                            x.Change_HP(-1);
                            break;
                        }
                    }
                }

                if(x.Get_HP() <= 0) {
                    ListExplosion.Add(new Entity("explosion", x.Get_PosX(), x.Get_PosY(), 0));
                    ListMonster.Remove(x);
                    sound_explosion.Play();
                    break;
                }
                foreach(Block spike in ListSpike) {
                    int i = Collisioncourse.Detection_Tile(x.Get_ID(),(int)x.Get_PosX(),(int)x.Get_PosY(), rect, spike.Get_ID(),(int)spike.Get_PosX(),(int)spike.Get_PosY(), x.get_is_Falling());

                    if(i == 2) {

                    }
                    if(i == 3) {
                        if(!x.get_Facing_Right() && spike.Get_Is_Solid() == 0) {
                            x.set_Facing_Right(true);
                            x.set_Movement("right");
                        }
                    }
                    if(i == 4) {
                        if(x.get_Facing_Right() && spike.Get_Is_Solid() == 0) {
                            x.set_Facing_Right(false);
                            x.set_Movement("left");
                        }
                    }
                    if(spike.Get_Is_Solid() == 0 || spike.Get_Is_Solid() == 1) {
                        if(i == 10) {
                            fallbreak3 = true;
                        }
                        if(i > 10) {
                            fallbreak3 = true;
                            x.Set_PosY(x.Get_PosY() - (i - 10 - 2));
                        }
                    }
                }
                foreach(Block block in ListBlockMiddle) {
                    int i = Collisioncourse.Detection_Tile(x.Get_ID(),(int)x.Get_PosX(),(int)x.Get_PosY(), rect, block.Get_ID(),(int)block.Get_PosX(),(int)block.Get_PosY(), x.get_is_Falling());

                    if(i == 2) {

                    }
                    if(i == 3) {
                        if(!x.get_Facing_Right() && block.Get_Is_Solid() == 0) {
                            x.set_Facing_Right(true);
                            x.set_Movement("right");
                        }
                    }
                    if(i == 4) {
                        if(x.get_Facing_Right() && block.Get_Is_Solid() == 0) {
                            x.set_Facing_Right(false);
                            x.set_Movement("left");
                        }
                    }
                    if(block.Get_Is_Solid() == 0 || block.Get_Is_Solid() == 1) {
                        if(i == 10) {
                            fallbreak3 = true;
                        }
                        if(i > 10) {
                            fallbreak3 = true;
                            x.Set_PosY(x.Get_PosY() - (i - 10 - 2));
                        }
                    }
                }
                if(fallbreak3) {
                    x.set_is_Falling(false);
                } else {
                    x.set_is_Falling(true);
                }
                Spawn_Shot(x);
                x.Update();
            }

            foreach(Entity x in ListCoin) {
                if(Collisioncourse.Detection_Entity("malice", (int)Malice.Get_PosX(), (int)Malice.Get_PosY(), malice_sprite, collision_malice, x.Get_ID(), (int)x.Get_PosX(), (int)x.Get_PosY(), 0, collision_coin, core_frame_index)) {
                    score_coins++;
                    ListCoin.Remove(x);
                    sound_coin.Play();
                    break;
                }
                x.Update();
            }

            if(Collisioncourse.Detection_Entity("malice", (int)Malice.Get_PosX(), (int)Malice.Get_PosY(), malice_sprite, collision_malice, "exit", (int)Exit.Get_PosX(), (int)Exit.Get_PosY(), 0, collision_exit, core_frame_index)) {
                Malice.set_Movement("still");
                level_won1 = true;
                exitreached = true;
            }
            Exit.Update();

            //foreach(Monster i in ListShot) {
            //    i.Update();
            //}

            foreach(Entity e in ListExplosion) {
                if(e.Get_Value() > 15) {
                    ListExplosion.Remove(e);
                    break;
                }
                e.Update();
            }

            core_time_current += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while(core_time_current > core_time_frame) {
                //Play the next frame in the SpriteSheet
                core_frame_index++;
                //reset elapsed time
                core_time_current = 0f;
            }
            if(core_frame_index > 7) {
                core_frame_index = 0;
            }

            if(Malice.Get_HP() != 0 && Malice.Get_PosY() > 40 * 32) {
                Malice.Set_HP(0);
            }

            if(Malice.Get_HP() == 0) {
                level_won2 = true;
                Malice.set_Movement("still");
            }

            // 00 - Idle
            // 01 - Walking
            // 02 - Running
            // 03 - Stopping
            // 04 - Pressing
            // 05 - Climbing
            // 06 - Looking Up
            // 07 - Looking Down
            // 08 - Jumping
            // 09 - Falling
            // 10 - Hurt
            // 11 - Death
            // 12 - Victory

            if(Malice.Get_HP() <= 0) {
                malice_sprite = 11;
            } else if(level_won2) {
                malice_sprite = 12;
            } else if(malice_damaged) {
                malice_sprite = 10;
            } else if(Malice.get_is_Jumping()) {
                malice_sprite = 08;
            } else if(Malice.get_is_Falling()) {
                malice_sprite = 09;
            } else if(dragging) {
                malice_sprite = 04;
            } else if(Malice.get_Movement() != "still") {
                malice_sprite = 01;
            } else if(control_keyboard_new.IsKeyDown(Keys.Down) && !Malice.get_is_Falling()) {
                malice_sprite = 07;
            } else if(control_keyboard_new.IsKeyDown(Keys.Up) && !Malice.get_is_Falling()) {
                malice_sprite = 06;
            } else {
                malice_sprite = 00;
            }

            if(!level_won2 && Malice.Get_HP() != 0) {
                camera_position_X = (int)((Malice.Get_PosX() - 320));
                if(camera_position_X < 0) {
                    camera_position_X = 0;
                }
                if(camera_position_X > 32 * 320 - (1280 / 2)) {
                    camera_position_X = 32 * 320 - (1280 / 2);
                }

                camera_position_Y = (int)((Malice.Get_PosY() - 180));
                if(camera_position_Y < 0) {
                    camera_position_Y = 0;
                }
                if(camera_position_Y > 32 * 40 - (720 / 2)) {
                    camera_position_Y = 32 * 40 - (720 / 2);
                }
            }


            if(!level_won1 && !level_won2)
                score_time_now = score_time_started - (int)(gameTime.TotalGameTime.TotalSeconds - score_time_system);

            if(score_time_now <= 0) Malice.Set_HP(0);

            score_final = score_killed * 20 + score_coins * 10;

            buttonanimation_timer--;
            if(buttonanimation_timer < 0) {
                if(buttonanimation_alpha > 0) buttonanimation_alpha = buttonanimation_alpha - 0.01f;
            }

            control_keyboard_old = control_keyboard_new;
            control_mouse_old = control_mouse_new;
            return "null";
        }

        public RenderTarget2D Get_RenderTargetResult() {
            return renderTargetResult;
        }

        private SpriteEffects Get_SpriteEffect(bool right) {
            if(right) {
                return SpriteEffects.None;
            } else {
                return SpriteEffects.FlipHorizontally;
            }
        }

        private int Explosion_Sprite(int i) {
            if(i == 0) return 0;
            if(i == 1) return 0;
            if(i == 2) return 1;
            if(i == 3) return 1;
            if(i == 4) return 2;
            if(i == 5) return 2;
            if(i == 6) return 3;
            if(i == 7) return 3;
            if(i == 8) return 4;
            if(i == 9) return 4;
            if(i == 10) return 5;
            if(i == 11) return 5;
            if(i == 12) return 6;
            if(i == 13) return 6;
            if(i == 14) return 7;
            if(i == 15) return 7;
            return 0;
        }

        public void DrawResultLayer() {

            graphics.SetRenderTarget(renderTargetResult);
            spriteBatch.Begin();
            // Draw the scene
            graphics.Clear(Color.Transparent);
            //DrawModel(model, world, view, projection);

            spriteBatch.Draw(texture_hud_hitpoointB, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.Draw(texture_hud_score, new Rectangle(0, 0, 1280, 720), Color.White);
            if(show_touch) {
                spriteBatch.Draw(texture_hud_button_pad, new Rectangle(0, (int)(core_screen_constant_height - buttonanimation_scale), (int)buttonanimation_scale, (int)buttonanimation_scale), Color.White * buttonanimation_alpha);
                spriteBatch.Draw(texture_hud_button_jump, new Rectangle((int)(core_screen_constant_width - buttonanimation_scale), (int)(core_screen_constant_height - buttonanimation_scale), (int)buttonanimation_scale, (int)buttonanimation_scale), Color.White * buttonanimation_alpha);
                spriteBatch.Draw(texture_hud_button_fire, new Rectangle((int)(core_screen_constant_width - buttonanimation_scale), (int)(core_screen_constant_height - buttonanimation_scale), (int)buttonanimation_scale, (int)buttonanimation_scale), Color.White * buttonanimation_alpha);
            }

            if(Malice.Get_HP() > 1) spriteBatch.Draw(texture_hud_hitpoint2, new Rectangle(0, 0, 1280, 720), Color.White);
            if(Malice.Get_HP() > 0) spriteBatch.Draw(texture_hud_hitpoint1, new Rectangle(0, 0, 1280, 720), Color.White);

            spriteBatch.DrawString(fontScore, "" + score_final, new Vector2(1260 - fontScore.MeasureString("" + score_final).Length(), 20), Color.White);
            spriteBatch.DrawString(fontScore, "" + shots_left, new Vector2(1100, 75), Color.White);
            spriteBatch.DrawString(fontScore, "" + score_time_now, new Vector2(1190, 75), Color.White);

            if(level_won2) {
                spriteBatch.DrawString(fontScore, "Game Over", new Vector2(1280 / 2 - fontScore.MeasureString("Game Over").Length() / 2, 280), Color.White);
                if(exitreached) {
                    int score = score_final + (int)score_time_now*10;
                    spriteBatch.DrawString(fontScore, "Final Score", new Vector2(1280 / 2 - fontScore.MeasureString("Final Score").Length() / 2, 320), Color.White);
                    spriteBatch.DrawString(fontScore, "" + score, new Vector2(1280 / 2 - fontScore.MeasureString("" + score).Length() / 2, 340), Color.White);
                }
                spriteBatch.DrawString(fontScore, "Tap to Continue", new Vector2(1280 / 2 - fontScore.MeasureString("Tap to Continue").Length() / 2, 400), Color.White);
            }
            spriteBatch.End();
            // Drop the render target
            graphics.SetRenderTarget(null);
        }

        public void Draw() {

            DrawResultLayer();

            graphics.SetRenderTarget(renderTarget);

            graphics.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

            spriteBatch.Begin();

            graphics.Clear(Color.Transparent);

            int camX1 = camera_position_X;
            while(camX1 > 1280 / 2)
                camX1 = camX1 - 1280 / 2;

            int camX2 = camera_position_X;
            while(camX2 > 1280 / 2 / 2)
                camX2 = camX2 - 1280 / 2 / 2;

            int camX3 = camera_position_X;
            while(camX3 > 1280 / 2 / 3)
                camX3 = camX3 - 1280 / 2 / 3;

            if(texture_background_3.Width != 1) spriteBatch.Draw(texture_background_3, new Rectangle(0, 0, 1280 / 2, 720 / 2), Color.White);
            if(texture_background_2.Width != 1) spriteBatch.Draw(texture_background_2, new Rectangle(-camX1 * 1, 0, 1280 / 2, 720 / 2), Color.White);
            if(texture_background_2.Width != 1) spriteBatch.Draw(texture_background_2, new Rectangle(-camX1 * 1 + 1280 / 2, 0, 1280 / 2, 720 / 2), Color.White);
            if(texture_background_1.Width != 1) spriteBatch.Draw(texture_background_1, new Rectangle(-camX2 * 2, 0, 1280 / 2, 720 / 2), Color.White);
            if(texture_background_1.Width != 1) spriteBatch.Draw(texture_background_1, new Rectangle(-camX2 * 2 + 1280 / 2, 0, 1280 / 2, 720 / 2), Color.White);

            int camX = (int)(camera_position_X);
            int camY = (int)(camera_position_Y);

            int drawX = camera_position_X; if(camera_position_X == 0) drawX = 0;
            int drawY = camera_position_Y; if(camera_position_Y == 0) drawY = 0;

            foreach(Block block in ListBlockFar) {
                if(drawX - 32 <= block.Get_PosX() && block.Get_PosX() <= drawX + 1280 && drawY - 32 <= block.Get_PosY() && block.Get_PosY() <= drawY + 720) {
                    spriteBatch.Draw(texture_tileset, new Vector2(block.Get_PosX() - drawX, block.Get_PosY() - drawY), new Rectangle(1 + (int)(33 * block.Get_ID().X), 1 + (int)(33 * block.Get_ID().Y), 32, 32), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                }
            }
            foreach(Block block in ListBlockMiddle) {
                if(drawX - 32 <= block.Get_PosX() && block.Get_PosX() <= drawX + 1280 && drawY - 32 <= block.Get_PosY() && block.Get_PosY() <= drawY + 720) {
                    spriteBatch.Draw(texture_tileset, new Vector2(block.Get_PosX() - drawX, block.Get_PosY() - drawY), new Rectangle(1 + (int)(33 * block.Get_ID().X), 1 + (int)(33 * block.Get_ID().Y), 32, 32), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                }
            }


            foreach(Entity x in ListTransponder) { spriteBatch.Draw(Get_Spritesheet(x.Get_ID()), new Vector2(x.Get_PosX() - camX, x.Get_PosY() - camY), new Rectangle(1 + core_frame_index * (64 + 1), 1 + x.Get_Value() * (64 + 1), 64, 64), Color.White, 0.0f, new Vector2(0, 0), 1, SpriteEffects.None, 0.0f); }
            foreach(Entity x in ListBeam) { spriteBatch.Draw(Get_Spritesheet(x.Get_ID()), new Vector2(x.Get_PosX() - camX, x.Get_PosY() - camY), new Rectangle(1 + core_frame_index * (64 + 1), 1 + Get_BeamColor(x.Get_Value()) * (64 + 1), 64, 64), Color.White, 0.0f, new Vector2(0, 0), 1, SpriteEffects.None, 0.0f); }
            foreach(Block x in ListSpike) { spriteBatch.Draw(texture_tileset, new Vector2(x.Get_PosX() - camX, x.Get_PosY() - camY), new Rectangle(1 + (int)(33 * x.Get_ID().X), 1 + (int)(33 * x.Get_ID().Y), 32, 32), Color.White, 0.0f, new Vector2(0, 0), 1, SpriteEffects.None, 0.0f); }
            /* Exit Sign */
            spriteBatch.Draw(texture_spritesheet_exit, new Vector2(Exit.Get_PosX() - camX, Exit.Get_PosY() - camY), new Rectangle(1 + core_frame_index * (64 + 1), 1 + 0 * (64 + 1), 64, 64), Color.White, 0.0f, new Vector2(0, 0), 1, SpriteEffects.None, 0.0f);
            /* Malice    */
            spriteBatch.Draw(texture_spritesheet_malice, new Vector2(Malice.Get_PosX() - camX, Malice.Get_PosY() - camY), new Rectangle(1 + core_frame_index * (64 + 1), 1 + malice_sprite * (64 + 1), 64, 64), Color.White, 0.0f, new Vector2(0, 0), 1, Get_SpriteEffect(Malice.get_Facing_Right()), 0.0f);
            foreach(Monster x in ListMonster) { spriteBatch.Draw(Get_Spritesheet(x.Get_ID()), new Vector2(x.Get_PosX() - camX, x.Get_PosY() - camY), new Rectangle(1 + core_frame_index * (64 + 1), 1 + 1 * (64 + 1), 64, 64), Color.White, 0.0f, new Vector2(0, 0), 1, Get_SpriteEffect(x.get_Facing_Right()), 0.0f); }
            foreach(Monster x in ListPlatform) { spriteBatch.Draw(texture_spritesheet_platform, new Vector2(x.Get_PosX() - camX, x.Get_PosY() - camY), new Rectangle(1 + core_frame_index * (64 + 1), 1 + 0 * (64 + 1), 64, 64), Color.White, 0.0f, new Vector2(0, 0), 1, Get_SpriteEffect(x.get_Facing_Right()), 0.0f); }
            foreach(Entity x in ListCoin) { spriteBatch.Draw(texture_spritesheet_coin, new Vector2(x.Get_PosX() - camX, x.Get_PosY() + 16 - camY), new Rectangle(1 + core_frame_index * (64 + 1), 1 + 0 * (64 + 1), 64, 64), Color.White, 0.0f, new Vector2(0, 0), 1, SpriteEffects.None, 0.0f); }
            foreach(Monster x in ListMino) { spriteBatch.Draw(Get_Spritesheet(x.Get_Value()), new Vector2(x.Get_PosX() - camX, x.Get_PosY() - camY), new Rectangle(1 + core_frame_index * (64 + 1), 1 + 0 * (64 + 1), 64, 64), Color.White, 0.0f, new Vector2(0, 0), 1, SpriteEffects.None, 0.0f); }
            foreach(Monster x in ListShot) {
                Color c = Color.White; if(x.Get_ID() == "shot_red") c = Color.Red;
                spriteBatch.Draw(texture_spritesheet_shot, new Vector2(x.Get_PosX() - camX, x.Get_PosY() - camY), new Rectangle(1 + core_frame_index * (64 + 1), 1 + 0 * (64 + 1), 64, 64), c, 0.0f, new Vector2(0, 0), 1, Get_SpriteEffect(x.get_Facing_Right()), 0.0f);
            }
            foreach(Entity x in ListExplosion) {
                spriteBatch.Draw(texture_spritesheet_explosion, new Vector2(x.Get_PosX() - camX, x.Get_PosY() - camY), new Rectangle(1 + Explosion_Sprite(x.Get_Value()) * (64 + 1), 1, 64, 64), Color.White, 0.0f, new Vector2(0, 0), 1, SpriteEffects.None, 0.0f); x.Change_Value(1);
            }
            foreach(Block block in ListBlockNear) {
                if(drawX - 32 <= block.Get_PosX() && block.Get_PosX() <= drawX + 1280 && drawY - 32 <= block.Get_PosY() && block.Get_PosY() <= drawY + 720) {
                    spriteBatch.Draw(texture_tileset, new Vector2(block.Get_PosX() - drawX, block.Get_PosY() - drawY), new Rectangle(1 + (int)(33 * block.Get_ID().X), 1 + (int)(33 * block.Get_ID().Y), 32, 32), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                }
            }

            if(texture_background_0.Width != 1) spriteBatch.Draw(texture_background_0, new Rectangle(-camX3 * 3, 0, 1280 / 2, 720 / 2), Color.White);
            if(texture_background_0.Width != 1) spriteBatch.Draw(texture_background_0, new Rectangle(-camX3 * 3 + 1280 / 2, 0, 1280 / 2, 720 / 2), Color.White);
            spriteBatch.End();
            graphics.SetRenderTarget(null);
        }

        public RenderTarget2D Get_RenderTarget() {
            return renderTarget;
        }

        private void Spawn_Shot(Monster x) {
            if(x.Get_ID() == "malice") {
                if(shots_left > 0) {
                    shots_left--;
                    sound_pew.Play();
                    if(x.get_Facing_Right()) {
                        ListShot.Add(new Monster("shot_violett", 1, x.Get_PosX() + 16, x.Get_PosY(), 1, -9, 0, 0, true));
                    } else {
                        ListShot.Add(new Monster("shot_violett", 1, x.Get_PosX() - 16, x.Get_PosY(), 1, +9, 0, 0, true));
                    }
                }
            }
            if(x.Get_ID() == "sentry") {
                if(Malice.Get_PosY() > x.Get_PosY() - 10 && Malice.Get_PosY() < x.Get_PosY() + 10) {
                    if(0 == random.Next(100)) {
                        sound_pew.Play();
                        if(x.get_Facing_Right()) {
                            ListShot.Add(new Monster("shot_red", 1, x.Get_PosX() + 16, x.Get_PosY(), 1, -9, 0, 0, true));
                        } else {
                            ListShot.Add(new Monster("shot_red", 1, x.Get_PosX() - 16, x.Get_PosY(), 1, +9, 0, 0, true));
                        }
                    }
                }
            }
        }

        private int Get_BeamColor(int id) {
            if(!active_beam[id - 1]) return 0;
            return id;
        }

        private Texture2D Get_Spritesheet(string id) {
            if(id == "bat") return texture_spritesheet_bat;
            if(id == "driller") return texture_spritesheet_driller;
            if(id == "eyeball") return texture_spritesheet_eyeball;
            if(id == "flybot") return texture_spritesheet_flybot;
            if(id == "lizard") return texture_spritesheet_lizard;
            if(id == "sentry") return texture_spritesheet_sentry;
            if(id == "snake") return texture_spritesheet_snake;
            if(id == "zylon") return texture_spritesheet_zylon;
            if(id == "beam") return texture_spritesheet_beam;
            if(id == "transponder") return texture_spritesheet_detector;
            if(id == "platform") return texture_spritesheet_platform;
            return texture_spritesheet_malice;
        }

        private Texture2D Get_Spritesheet(int id) {
            if(id == 1) return texture_spritesheet_mino_blank;
            if(id == 2) return texture_spritesheet_mino_fire;
            if(id == 3) return texture_spritesheet_mino_air;
            if(id == 4) return texture_spritesheet_mino_thunder;
            if(id == 5) return texture_spritesheet_mino_water;
            if(id == 6) return texture_spritesheet_mino_ice;
            if(id == 7) return texture_spritesheet_mino_earth;
            if(id == 8) return texture_spritesheet_mino_metal;
            if(id == 9) return texture_spritesheet_mino_nature;
            if(id == 10) return texture_spritesheet_mino_light;
            if(id == 11) return texture_spritesheet_mino_dark;
            return texture_spritesheet_malice;
        }

        private bool Command_Cursor(float cursorX, float cursorY, string command) {
            if(0 < cursorX && cursorX < buttonanimation_scale && core_screen_constant_height - buttonanimation_scale < cursorY && cursorY < core_screen_constant_height) {
                int x = (int)(cursorX + (500-buttonanimation_scale)/2);
                int y = (int)(cursorY + (500-buttonanimation_scale)/2 - (core_screen_constant_height - buttonanimation_scale));
                Color temp = TextureData[500*y  + x];
                if(temp == new Color(new Vector4(255, 0, 0, 255))) {
                    malice_looking_down = false;
                    malice_looking_up = true;
                } else
                if(temp == new Color(new Vector4(0, 255, 0, 255))) {
                    malice_looking_down = true;
                    malice_looking_up = false;
                } else
                if(temp == new Color(new Vector4(255, 0, 255, 255))) {
                    Malice.set_Movement("left");
                    Malice.set_Facing_Right(false);
                } else
                if(temp == new Color(new Vector4(0, 255, 255, 255))) {
                    Malice.set_Movement("right");
                    Malice.set_Facing_Right(true);
                } else {
                    Malice.set_Movement("still");
                }
            }
            if(core_screen_constant_width - buttonanimation_scale < cursorX && cursorX < core_screen_constant_width && core_screen_constant_height - buttonanimation_scale < cursorY && cursorY < core_screen_constant_height) {
                int x = (int)(cursorX - (core_screen_constant_width-buttonanimation_scale));
                int y = (int)(cursorY - (core_screen_constant_height-buttonanimation_scale));
                if(buttonanimation_scale / 4 * 1 < x && x < buttonanimation_scale / 4 * 3) {
                    if(y < buttonanimation_scale / 2 && command == "fire") {
                        Spawn_Shot(Malice);
                    }
                    if(y > buttonanimation_scale / 2) {
                        if(!Malice.get_is_Jumping() && !Malice.get_is_Falling()) {
                            Malice.set_is_Jumping(true);
                            jumper_point = Malice.Get_PosY();
                        }
                    }
                }
            }
            return false;
        }
    }
}
