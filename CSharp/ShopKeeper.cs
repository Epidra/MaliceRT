using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MaliceRT {
    class ShopKeeper {

        //Screen Size
        public float gamescreen_width { get; } = 1280;
        public float gamescreen_height { get; } = 720;

        //Screenpositions Grid
        public Vector2 grid_chain_position_main { get; } = new Vector2(465, 230);
        public Vector2 grid_chain_position_upper1 { get; } = new Vector2(465, 48);
        public Vector2 grid_chain_position_upper2 { get; } = new Vector2(465, -136);
        public Vector2 grid_chain_position_lower1 { get; } = new Vector2(465, 413);
        public Vector2 grid_chain_position_lower2 { get; } = new Vector2(465, 599);
        public Vector2 grid_selector_position_1 { get; } = new Vector2(465, 218);
        public Vector2 grid_selector_position_2 { get; } = new Vector2(620, 311);
        public Vector2 grid_selector_position_3 { get; } = new Vector2(775, 218);
        public Vector2 grid_selector_position_4 { get; } = new Vector2(930, 311);
        public Vector2 grid_selector_position_5 { get; } = new Vector2(1085, 218);

        //Music
        public string audio_music_grass { get; } = "Audio/Music/Grass";
        public string audio_music_vulcano { get; } = "Audio/Music/Vulcano";
        public string audio_music_sky { get; } = "Audio/Music/Sky";
        public string audio_music_desert { get; } = "Audio/Music/Desert";
        public string audio_music_beach { get; } = "Audio/Music/Beach";
        public string audio_music_snow { get; } = "Audio/Music/Snow";
        public string audio_music_mountain { get; } = "Audio/Music/Mountain";
        public string audio_music_mashine { get; } = "Audio/Music/Mashine";
        public string audio_music_forest { get; } = "Audio/Music/Forest";
        public string audio_music_aurora { get; } = "Audio/Music/Aurora";
        public string audio_music_space { get; } = "Audio/Music/Space";

        //Sound
        public string audio_sound_coin { get; } = "Audio/Sound/Coin";
        public string audio_sound_damage { get; } = "Audio/Sound/Damage";
        public string audio_sound_explosion { get; } = "Audio/Sound/Explosion";
        public string audio_sound_jump { get; } = "Audio/Sound/Jump";
        public string audio_sound_pew { get; } = "Audio/Sound/Pew";

        //Background
        public string texture_background_title0 { get; } = "Graphics/Background/Title0";
        public string texture_background_title1 { get; } = "Graphics/Background/Title1";
        public string texture_background_title2 { get; } = "Graphics/Background/Title2";
        public string texture_background_title3 { get; } = "Graphics/Background/Title3";
        public string texture_background_menu0 { get; } = "Graphics/Background/Menu0";
        public string texture_background_menu1 { get; } = "Graphics/Background/Menu1";
        public string texture_background_menu2 { get; } = "Graphics/Background/Menu2";
        public string texture_background_menu3 { get; } = "Graphics/Background/Menu3";
        public string texture_background_grass0 { get; } = "Graphics/Background/Grass0";
        public string texture_background_grass1 { get; } = "Graphics/Background/Grass1";
        public string texture_background_grass2 { get; } = "Graphics/Background/Grass2";
        public string texture_background_grass3 { get; } = "Graphics/Background/Grass3";
        public string texture_background_vulcano0 { get; } = "Graphics/Background/Vulcano0";
        public string texture_background_vulcano1 { get; } = "Graphics/Background/Vulcano1";
        public string texture_background_vulcano2 { get; } = "Graphics/Background/Vulcano2";
        public string texture_background_vulcano3 { get; } = "Graphics/Background/Vulcano3";
        public string texture_background_sky0 { get; } = "Graphics/Background/Sky0";
        public string texture_background_sky1 { get; } = "Graphics/Background/Sky1";
        public string texture_background_sky2 { get; } = "Graphics/Background/Sky2";
        public string texture_background_sky3 { get; } = "Graphics/Background/Sky3";
        public string texture_background_desert0 { get; } = "Graphics/Background/Desert0";
        public string texture_background_desert1 { get; } = "Graphics/Background/Desert1";
        public string texture_background_desert2 { get; } = "Graphics/Background/Desert2";
        public string texture_background_desert3 { get; } = "Graphics/Background/Desert3";
        public string texture_background_beach0 { get; } = "Graphics/Background/Beach0";
        public string texture_background_beach1 { get; } = "Graphics/Background/Beach1";
        public string texture_background_beach2 { get; } = "Graphics/Background/Beach2";
        public string texture_background_beach3 { get; } = "Graphics/Background/Beach3";
        public string texture_background_snow0 { get; } = "Graphics/Background/Snow0";
        public string texture_background_snow1 { get; } = "Graphics/Background/Snow1";
        public string texture_background_snow2 { get; } = "Graphics/Background/Snow2";
        public string texture_background_snow3 { get; } = "Graphics/Background/Snow3";
        public string texture_background_mountain0 { get; } = "Graphics/Background/Mountain0";
        public string texture_background_mountain1 { get; } = "Graphics/Background/Mountain1";
        public string texture_background_mountain2 { get; } = "Graphics/Background/Mountain2";
        public string texture_background_mountain3 { get; } = "Graphics/Background/Mountain3";
        public string texture_background_mashine0 { get; } = "Graphics/Background/Mashine0";
        public string texture_background_mashine1 { get; } = "Graphics/Background/Mashine1";
        public string texture_background_mashine2 { get; } = "Graphics/Background/Mashine2";
        public string texture_background_mashine3 { get; } = "Graphics/Background/Mashine3";
        public string texture_background_forest0 { get; } = "Graphics/Background/Forest0";
        public string texture_background_forest1 { get; } = "Graphics/Background/Forest1";
        public string texture_background_forest2 { get; } = "Graphics/Background/Forest2";
        public string texture_background_forest3 { get; } = "Graphics/Background/Forest3";
        public string texture_background_aurora0 { get; } = "Graphics/Background/Aurora0";
        public string texture_background_aurora1 { get; } = "Graphics/Background/Aurora1";
        public string texture_background_aurora2 { get; } = "Graphics/Background/Aurora2";
        public string texture_background_aurora3 { get; } = "Graphics/Background/Aurora3";
        public string texture_background_space0 { get; } = "Graphics/Background/Space0";
        public string texture_background_space1 { get; } = "Graphics/Background/Space1";
        public string texture_background_space2 { get; } = "Graphics/Background/Space2";
        public string texture_background_space3 { get; } = "Graphics/Background/Space3";

        //HUD
        public string texture_hud_button_fire { get; } = "Graphics/HUD/ButtonFire";
        public string texture_hud_button_jump { get; } = "Graphics/HUD/ButtonJump";
        public string texture_hud_button_pad { get; } = "Graphics/HUD/ButtonPad";
        public string texture_hud_hitpoint1 { get; } = "Graphics/HUD/Hitpoint1";
        public string texture_hud_hitpoint2 { get; } = "Graphics/HUD/Hitpoint2";
        public string texture_hud_hitpointbackground { get; } = "Graphics/HUD/HitpointBackground";
        public string texture_hud_score { get; } = "Graphics/HUD/Score";
        public string texture_hud_pause { get; } = "Graphics/HUD/Pause";
        public string texture_hud_colormap { get; } = "Graphics/HUD/Colormap";

        //Menu
        public string texture_menu_button_editor { get; } = "Graphics/Menu/ButtonEditor";
        public string texture_menu_button_highscore { get; } = "Graphics/Menu/ButtonHighscore";
        public string texture_menu_name_act1 { get; } = "Graphics/Menu/NameAct1";
        public string texture_menu_name_act2 { get; } = "Graphics/Menu/NameAct2";
        public string texture_menu_name_act3 { get; } = "Graphics/Menu/NameAct3";
        public string texture_menu_name_act4 { get; } = "Graphics/Menu/NameAct4";
        public string texture_menu_name_act5 { get; } = "Graphics/Menu/NameAct5";
        public string texture_menu_name_options { get; } = "Graphics/Menu/NameOptions";
        public string texture_menu_name_editor { get; } = "Graphics/Menu/NameEditor";
        public string texture_menu_name_endless { get; } = "Graphics/Menu/NameEndless";
        public string texture_menu_name_grass { get; } = "Graphics/Menu/NameGrass";
        public string texture_menu_name_vulcano { get; } = "Graphics/Menu/NameVulcano";
        public string texture_menu_name_sky { get; } = "Graphics/Menu/NameSky";
        public string texture_menu_name_desert { get; } = "Graphics/Menu/NameDesert";
        public string texture_menu_name_beach { get; } = "Graphics/Menu/NameBeach";
        public string texture_menu_name_snow { get; } = "Graphics/Menu/NameSnow";
        public string texture_menu_name_mountain { get; } = "Graphics/Menu/NameMountain";
        public string texture_menu_name_mashine { get; } = "Graphics/Menu/NameMashine";
        public string texture_menu_name_forest { get; } = "Graphics/Menu/NameForest";
        public string texture_menu_name_aurora { get; } = "Graphics/Menu/NameAurora";
        public string texture_menu_name_space { get; } = "Graphics/Menu/NameSpace";
        public string texture_menu_name_unknown { get; } = "Graphics/Menu/NameUnknown";

        //Grid
        public string texture_grid_bump { get; } = "Graphics/Menu/Bump";
        public string texture_grid_options { get; } = "Graphics/Menu/GridOptions";
        public string texture_grid_editor { get; } = "Graphics/Menu/GridEditor";
        public string texture_grid_endless { get; } = "Graphics/Menu/GridEndless";
        public string texture_grid_grass { get; } = "Graphics/Menu/GridGrass";
        public string texture_grid_vulcano { get; } = "Graphics/Menu/GridVulcano";
        public string texture_grid_sky { get; } = "Graphics/Menu/GridSky";
        public string texture_grid_desert { get; } = "Graphics/Menu/GridDesert";
        public string texture_grid_beach { get; } = "Graphics/Menu/GridBeach";
        public string texture_grid_snow { get; } = "Graphics/Menu/GridSnow";
        public string texture_grid_mountain { get; } = "Graphics/Menu/GridMountain";
        public string texture_grid_mashine { get; } = "Graphics/Menu/GridMashine";
        public string texture_grid_forest { get; } = "Graphics/Menu/GridForest";
        public string texture_grid_aurora { get; } = "Graphics/Menu/GridAurora";
        public string texture_grid_space { get; } = "Graphics/Menu/GridSpace";
        public string texture_grid_selector { get; } = "Graphics/Menu/Selector";

        //Spritesheets
        public string texture_spritesheet_bat { get; } = "Graphics/Spritesheet/Bat";
        public string texture_spritesheet_beam { get; } = "Graphics/Spritesheet/Beam";
        public string texture_spritesheet_coin { get; } = "Graphics/Spritesheet/Coin";
        public string texture_spritesheet_detector { get; } = "Graphics/Spritesheet/Detector";
        public string texture_spritesheet_driller { get; } = "Graphics/Spritesheet/Driller";
        public string texture_spritesheet_exit { get; } = "Graphics/Spritesheet/Exit";
        public string texture_spritesheet_explosion { get; } = "Graphics/Spritesheet/Explosion";
        public string texture_spritesheet_eyeball { get; } = "Graphics/Spritesheet/Eyeball";
        public string texture_spritesheet_flybot { get; } = "Graphics/Spritesheet/Flybot";
        public string texture_spritesheet_lizard { get; } = "Graphics/Spritesheet/Lizard";
        public string texture_spritesheet_malice { get; } = "Graphics/Spritesheet/Malice";
        public string texture_spritesheet_mino_air { get; } = "Graphics/Spritesheet/MinoAir";
        public string texture_spritesheet_mino_blank { get; } = "Graphics/Spritesheet/MinoBlank";
        public string texture_spritesheet_mino_dark { get; } = "Graphics/Spritesheet/MinoDark";
        public string texture_spritesheet_mino_earth { get; } = "Graphics/Spritesheet/MinoEarth";
        public string texture_spritesheet_mino_fire { get; } = "Graphics/Spritesheet/MinoFire";
        public string texture_spritesheet_mino_ice { get; } = "Graphics/Spritesheet/MinoIce";
        public string texture_spritesheet_mino_light { get; } = "Graphics/Spritesheet/MinoLight";
        public string texture_spritesheet_mino_metal { get; } = "Graphics/Spritesheet/MinoMetal";
        public string texture_spritesheet_mino_nature { get; } = "Graphics/Spritesheet/MinoNature";
        public string texture_spritesheet_mino_thunder { get; } = "Graphics/Spritesheet/MinoThunder";
        public string texture_spritesheet_mino_water { get; } = "Graphics/Spritesheet/MinoWater";
        public string texture_spritesheet_platform { get; } = "Graphics/Spritesheet/Platform";
        public string texture_spritesheet_sentry { get; } = "Graphics/Spritesheet/Sentry";
        public string texture_spritesheet_shot { get; } = "Graphics/Spritesheet/Shot";
        public string texture_spritesheet_snake { get; } = "Graphics/Spritesheet/Snake";
        public string texture_spritesheet_zylon { get; } = "Graphics/Spritesheet/Zylon";

        //Tileset
        public string texture_tileset_grass { get; } = "Graphics/Tileset/Grass";
        public string texture_tileset_vulcano { get; } = "Graphics/Tileset/Vulcano";
        public string texture_tileset_sky { get; } = "Graphics/Tileset/Sky";
        public string texture_tileset_desert { get; } = "Graphics/Tileset/Desert";
        public string texture_tileset_beach { get; } = "Graphics/Tileset/Beach";
        public string texture_tileset_snow { get; } = "Graphics/Tileset/Snow";
        public string texture_tileset_mountain { get; } = "Graphics/Tileset/Mountain";
        public string texture_tileset_mashine { get; } = "Graphics/Tileset/Mashine";
        public string texture_tileset_forest { get; } = "Graphics/Tileset/Forest";
        public string texture_tileset_aurora { get; } = "Graphics/Tileset/Aurora";
        public string texture_tileset_space { get; } = "Graphics/Tileset/Space";
        public string texture_tileset_event { get; } = "Graphics/Tileset/Event";
        public string texture_tileset_world { get; } = "Graphics/Tileset/World";

        //Editor
        public string texture_editor_background { get; } = "Graphics/Editor/Background";
        public string texture_editor_overlay1 { get; } = "Graphics/Editor/Overlay1";
        public string texture_editor_overlay2 { get; } = "Graphics/Editor/Overlay2";
        public string texture_editor_pixel { get; } = "Graphics/Editor/Pixel";
        public string texture_editor_selector { get; } = "Graphics/Editor/Selector";
        public string texture_editor_shadow { get; } = "Graphics/Editor/Shadow";

        //Fonts
        public string font_score { get; } = "Fonts/FontSegoe";

        //Malice
        public int malice_jump_height { get; } = 100;//200;//150;

        public int speed_walk { get; } = 3;//6;//4;
        public int speed_fall { get; } = 4;//8;//6;
        public int speed_jump { get; } = 2;//5;//4;

        public Rectangle collision_bat { get; } = new Rectangle(22, 11, 18, 12); // (45,  23,  37,  15)
        public Rectangle collision_beam { get; } = new Rectangle(10, 0, 44, 64); // (20,   0,  88, 128)
        public Rectangle collision_coin { get; } = new Rectangle(23, 39, 18, 18); // (46,  78,  36,  36)
        public Rectangle collision_driller { get; } = new Rectangle(19, 46, 23, 18); // (38,  92,  46,  36)
        public Rectangle collision_exit { get; } = new Rectangle(17, 33, 29, 30); // (35,  67,  58,  61)
        public Rectangle collision_eyeball { get; } = new Rectangle(25, 40, 14, 11); // (50,  80,  28,  22)
        public Rectangle collision_flybot { get; } = new Rectangle(26, 4, 12, 23); // (52,   8,  24,  46)
        public Rectangle collision_lizard { get; } = new Rectangle(20, 45, 27, 18); // (40,  91,  54,  37)
        public Rectangle collision_malice { get; } = new Rectangle(14, 8, 34, 55); // (29,  17,  69, 111)
        public Rectangle collision_mino { get; } = new Rectangle(0, 0, 64, 64); // ( 0,   0, 128, 128)
        public Rectangle collision_platform { get; } = new Rectangle(0, 52, 64, 12); // ( 0, 104, 128,  24)
        public Rectangle collision_sentry { get; } = new Rectangle(9, 6, 46, 58); // (18,  12,  92, 116)
        public Rectangle collision_shot { get; } = new Rectangle(23, 23, 17, 17); // (47,  47,  34,  34)
        public Rectangle collision_snake { get; } = new Rectangle(24, 50, 16, 14); // (48, 100,  32,  28)
        public Rectangle collision_zylon { get; } = new Rectangle(24, 4, 15, 23); // (49,   9,  30,  46)

        public string Get_Background(string s, int i) {
            if(s == "grass" && i == 0) return texture_background_grass0;
            if(s == "grass" && i == 1) return texture_background_grass1;
            if(s == "grass" && i == 2) return texture_background_grass2;
            if(s == "grass" && i == 3) return texture_background_grass3;

            if(s == "vulcano" && i == 0) return texture_background_vulcano0;
            if(s == "vulcano" && i == 1) return texture_background_vulcano1;
            if(s == "vulcano" && i == 2) return texture_background_vulcano2;
            if(s == "vulcano" && i == 3) return texture_background_vulcano3;

            if(s == "sky" && i == 0) return texture_background_sky0;
            if(s == "sky" && i == 1) return texture_background_sky1;
            if(s == "sky" && i == 2) return texture_background_sky2;
            if(s == "sky" && i == 3) return texture_background_sky3;

            if(s == "desert" && i == 0) return texture_background_desert0;
            if(s == "desert" && i == 1) return texture_background_desert1;
            if(s == "desert" && i == 2) return texture_background_desert2;
            if(s == "desert" && i == 3) return texture_background_desert3;

            if(s == "beach" && i == 0) return texture_background_beach0;
            if(s == "beach" && i == 1) return texture_background_beach1;
            if(s == "beach" && i == 2) return texture_background_beach2;
            if(s == "beach" && i == 3) return texture_background_beach3;

            if(s == "snow" && i == 0) return texture_background_snow0;
            if(s == "snow" && i == 1) return texture_background_snow1;
            if(s == "snow" && i == 2) return texture_background_snow2;
            if(s == "snow" && i == 3) return texture_background_snow3;

            if(s == "mountain" && i == 0) return texture_background_mountain0;
            if(s == "mountain" && i == 1) return texture_background_mountain1;
            if(s == "mountain" && i == 2) return texture_background_mountain2;
            if(s == "mountain" && i == 3) return texture_background_mountain3;

            if(s == "mashine" && i == 0) return texture_background_mashine0;
            if(s == "mashine" && i == 1) return texture_background_mashine1;
            if(s == "mashine" && i == 2) return texture_background_mashine2;
            if(s == "mashine" && i == 3) return texture_background_mashine3;

            if(s == "forest" && i == 0) return texture_background_forest0;
            if(s == "forest" && i == 1) return texture_background_forest1;
            if(s == "forest" && i == 2) return texture_background_forest2;
            if(s == "forest" && i == 3) return texture_background_forest3;

            if(s == "aurora" && i == 0) return texture_background_aurora0;
            if(s == "aurora" && i == 1) return texture_background_aurora1;
            if(s == "aurora" && i == 2) return texture_background_aurora2;
            if(s == "aurora" && i == 3) return texture_background_aurora3;

            if(s == "space" && i == 0) return texture_background_space0;
            if(s == "space" && i == 1) return texture_background_space1;
            if(s == "space" && i == 2) return texture_background_space2;
            if(s == "space" && i == 3) return texture_background_space3;
            return texture_background_title0;
        }

        public string Get_Tileset(string s) {
            if(s == "grass") return texture_tileset_grass;
            if(s == "vulcano") return texture_tileset_grass; //return texture_tileset_vulcano;
            if(s == "sky") return texture_tileset_grass; //return texture_tileset_sky;
            if(s == "desert") return texture_tileset_grass; // return texture_tileset_desert;
            if(s == "beach") return texture_tileset_grass; //return texture_tileset_beach;
            if(s == "snow") return texture_tileset_grass; // return texture_tileset_snow;
            if(s == "mountain") return texture_tileset_grass; //return texture_tileset_mountain;
            if(s == "mashine") return texture_tileset_grass; // return texture_tileset_mashine;
            if(s == "forest") return texture_tileset_grass; // return texture_tileset_forest;
            if(s == "aurora") return texture_tileset_grass; // return texture_tileset_aurora;
            if(s == "space") return texture_tileset_grass; //return texture_tileset_space;
            return texture_tileset_grass;
        }

    }
}
