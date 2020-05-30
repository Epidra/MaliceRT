using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MaliceRT {
    class CollisionCourse {
        Color[] color_menu_hex;
        Color[] color_sprite_malice;
        Color[] color_sprite_nutcrab;
        Color[] color_sprite_turret;
        Color[] color_sprite_exit;
        Color[] color_sprite_coin;
        Color[] color_sprite_irrlicht;
        Color[] color_sprite_tileset;

        int upperBorder = 0;
        int lowerBorder = 0;
        Vector2 gridPosition1 = new Vector2(0,0);
        Vector2 gridPosition2 = new Vector2(0,0);
        Vector2 gridPosition3 = new Vector2(0,0);
        Vector2 gridPosition4 = new Vector2(0,0);
        Vector2 gridPosition5 = new Vector2(0,0);
        int grid_width  = 0;
        int grid_height = 0;

        int size_sprite  = 64;
        int size_tileset = 32;
        int grid_malice  = 2;
        int grid_sprite  = 1;
        int grid_tileset = 2;

        public CollisionCourse() {

        }

        public void InserGreymark(Texture2D _texture, int _upperBorder, int _lowerBorder, Vector2 _pos1, Vector2 _pos2, Vector2 _pos3, Vector2 _pos4, Vector2 _pos5) {

            color_menu_hex = new Color[_texture.Width * _texture.Height];
            _texture.GetData(color_menu_hex);


            upperBorder = _upperBorder;
            lowerBorder = _lowerBorder;
            gridPosition1 = _pos1;
            gridPosition2 = _pos2;
            gridPosition3 = _pos3;
            gridPosition4 = _pos4;
            gridPosition5 = _pos5;
            grid_width = _texture.Width;
            grid_height = _texture.Height;
        }

        public void InsertSpritesheet(Texture2D _malice, Texture2D _nutcrab, Texture2D _turret, Texture2D _coin, Texture2D _exit, Texture2D _irrlicht, Texture2D _tileset) {

            color_sprite_malice = new Color[_malice.Width * _malice.Height];
            _malice.GetData(color_sprite_malice);

            color_sprite_nutcrab = new Color[_nutcrab.Width * _nutcrab.Height];
            _nutcrab.GetData(color_sprite_nutcrab);

            color_sprite_turret = new Color[_turret.Width * _turret.Height];
            _turret.GetData(color_sprite_turret);

            color_sprite_coin = new Color[_coin.Width * _coin.Height];
            _coin.GetData(color_sprite_coin);

            color_sprite_exit = new Color[_exit.Width * _exit.Height];
            _exit.GetData(color_sprite_exit);

            color_sprite_irrlicht = new Color[_irrlicht.Width * _irrlicht.Height];
            _irrlicht.GetData(color_sprite_irrlicht);

            color_sprite_tileset = new Color[_tileset.Width * _tileset.Height];
            _tileset.GetData(color_sprite_tileset);

        }

        public string CollisionGrid(int _x, int _y) {
            int cursorX = _x;
            int cursorY = _y;
            if(cursorY < upperBorder) {
                return "movingUP";
            }
            if(cursorY > lowerBorder) {
                return "movingDOWN";
            }

            if(gridPosition1.X < cursorX && cursorX < gridPosition1.X + grid_width && gridPosition1.Y < cursorY && cursorY < gridPosition1.Y + grid_height) {
                if(color_menu_hex[(int)(cursorX - gridPosition1.X) * (int)(cursorY - gridPosition1.Y)] == Color.Black) {
                    return "Position1";
                }
            }
            if(gridPosition2.X < cursorX && cursorX < gridPosition2.X + grid_width && gridPosition2.Y < cursorY && cursorY < gridPosition2.Y + grid_height) {
                if(color_menu_hex[(int)(cursorX - gridPosition2.X) * (int)(cursorY - gridPosition2.Y)] == Color.Black) {
                    return "Position2";
                }
            }
            if(gridPosition3.X < cursorX && cursorX < gridPosition3.X + grid_width && gridPosition3.Y < cursorY && cursorY < gridPosition3.Y + grid_height) {
                if(color_menu_hex[(int)(cursorX - gridPosition3.X) * (int)(cursorY - gridPosition3.Y)] == Color.Black) {
                    return "Position3";
                }
            }
            if(gridPosition4.X < cursorX && cursorX < gridPosition4.X + grid_width && gridPosition4.Y < cursorY && cursorY < gridPosition4.Y + grid_height) {
                if(color_menu_hex[(int)(cursorX - gridPosition4.X) * (int)(cursorY - gridPosition4.Y)] == Color.Black) {
                    return "Position4";
                }
            }
            if(gridPosition5.X < cursorX && cursorX < gridPosition5.X + grid_width && gridPosition5.Y < cursorY && cursorY < gridPosition5.Y + grid_height) {
                if(color_menu_hex[(int)(cursorX - gridPosition5.X) * (int)(cursorY - gridPosition5.Y)] == Color.Black) {
                    return "Position5";
                }
            }
            return "null";
        }





        public bool Detection_Entity(string id1, int x1, int y1, int sprite1, Rectangle rect1, string id2, int x2, int y2, int sprite2, Rectangle rect2, int frame) {
            Rectangle rectangle1 = new Rectangle(x1 + rect1.X, y1 + rect1.Y, rect1.Width, rect1.Height);
            Rectangle rectangle2 = new Rectangle(x2 + rect2.X, y2 + rect2.Y, rect2.Width, rect2.Width);
            Rectangle collision;
            collision = Rectangle.Intersect(rectangle1, rectangle2);
            if(!collision.IsEmpty) {
                int spriteX1 = 0;
                int spriteY1 = 0;
                int spriteX2 = 0;
                int spriteY2 = 0;
                int m = 1;
                if(id1 == "malice")
                    m = 2;
                int size = 64;
                if(id2 == "coin") {
                    size = 32;
                }
                if(x1 < x2 && y1 < y2) { // upper left
                    spriteY1 = m + (64 + m) * sprite1 + 0;
                    spriteX1 = m + (64 + m) * frame + 0;
                    spriteY2 = 1 + (size + 1) * sprite2 + 64 - collision.Width;
                    spriteX2 = 1 + (size + 1) * frame + 64 - collision.Height;
                }
                if(x1 > x2 && y1 < y2) { // upper right
                    spriteY1 = m + (64 + m) * sprite1 + 64 - collision.Width;
                    spriteX1 = m + (64 + m) * frame + 0;
                    spriteY2 = 1 + (size + 1) * sprite2 + 0;
                    spriteX2 = 1 + (size + 1) * frame + 64 - collision.Height;
                }
                if(x1 < x2 && y1 > y2) { // lower left
                    spriteY1 = m + (64 + m) * sprite1 + 0;
                    spriteX1 = m + (64 + m) * frame + 64 - collision.Height;
                    spriteY2 = 1 + (size + 1) * sprite2 + 64 - collision.Width;
                    spriteX2 = 1 + (size + 1) * frame + 0;
                }
                if(x1 > x2 && y1 > y2) { // lower right
                    spriteY1 = m + (64 + m) * sprite1 + 64 - collision.Width;
                    spriteX1 = m + (64 + m) * frame + 64 - collision.Height;
                    spriteY2 = 1 + (size + 1) * sprite2 + 0;
                    spriteX2 = 1 + (size + 1) * frame + 0;
                }

                Color[] colorfield1 = Get_ColorField(id1);
                Color[] colorfield2 = Get_ColorField(id2);

                int length1 = (m + 64)*8;
                int length2 = (1 + size)*8;

                for(int y = 0; y < collision.Height; y++) {
                    for(int x = 0; x < collision.Width; x++) {
                        Color c1;
                        Color c2;
                        try {
                            c1 = colorfield1[length1 * (spriteY1 + y) + spriteX1 + x];
                            c2 = colorfield2[length2 * (spriteY2 + y) + spriteX2 + x];
                        } catch(Exception e) {
                            return true;
                        }
                        if(c1 != Color.Transparent && c2 != Color.Transparent) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private Color[] Get_ColorField(string id) {
            if(id == "malice") return color_sprite_malice;
            if(id == "nutcrab") return color_sprite_nutcrab;
            if(id == "turret") return color_sprite_turret;
            if(id == "coin") return color_sprite_coin;
            if(id == "exit") return color_sprite_exit;
            if(id == "irrlicht") return color_sprite_irrlicht;
            return color_sprite_tileset;
        }

        /*  public bool Detection_Monster(string _id1, int _x1, int _y1, Rectangle _rect1, string _id2, int _x2, int _y2, Rectangle _rect2) {
              string id1 = _id1;
              string id2 = _id2;
              int x1  = _x1;
              int x2  = _x2;
              int y1  = _y1;
              int y2  = _y2;
              int r1h = _rect1.Height;
              int r2h = _rect2.Height;
              int r1w = _rect1.Width;
              int r2w = _rect2.Width;
              Rectangle rect1 = new Rectangle(x1 + _rect1.X, y1 + _rect1.Y, r1w, r1h);
              Rectangle rect2 = new Rectangle(x2 + _rect2.X, y2 + _rect2.Y, r2w, r2h);
              Rectangle collision;
              collision = Rectangle.Intersect(rect1, rect2);
              if(!collision.IsEmpty) {
                  return true;
              }
              return false;
          }

          public bool Detection_Object(int _x1, int _y1, Rectangle _rect1, int _x2, int _y2, Rectangle _rect2) {
              int x1  = _x1;
              int x2  = _x2;
              int y1  = _y1;
              int y2  = _y2;
              int r1h = _rect1.Height;
              int r2h = _rect2.Height;
              int r1w = _rect1.Width;
              int r2w = _rect2.Width;
              Rectangle rect1 = new Rectangle(x1 + _rect1.X, y1 + _rect1.Y, r1w, r1h);
              Rectangle rect2 = new Rectangle(x2 + _rect2.X, y2 + _rect2.Y, r2w, r2h);
              Rectangle collision;
              collision = Rectangle.Intersect(rect1, rect2);
              if(!collision.IsEmpty) {
                  return true;
              } else {
                  return false;
              }
          }*/

        public int Detection_Tile(string _id1, int _x1, int _y1, Rectangle _rect1, Vector2 _id2, int _x2, int _y2, bool _is_falling) {
            string id1 = _id1;
            //int id2 = _id2;
            int x1  = _x1;
            int x2  = _x2;
            int y1  = _y1;
            int y2  = _y2;
            int r1h = _rect1.Height;
            int r2h = 32;
            int r1w = _rect1.Width;
            int r2w = 32;
            bool is_falling = _is_falling;
            Rectangle rect1 = new Rectangle(x1 + _rect1.X, y1 + _rect1.Y, r1w, r1h);
            Rectangle rect2 = new Rectangle(x2           , y2           , r2w, r2h);
            Rectangle collision;
            collision = Rectangle.Intersect(rect1, rect2);
            if(!collision.IsEmpty) {

                collision = Rectangle.Intersect(new Rectangle(rect1.X + 6, rect1.Y + rect1.Height - 5, rect1.Width - 12, 6), rect2); if(!collision.IsEmpty) return 10 + collision.Height;
                collision = Rectangle.Intersect(new Rectangle(rect1.X + 6, rect1.Y, rect1.Width - 12, 1), rect2); if(!collision.IsEmpty) return 2;
                collision = Rectangle.Intersect(new Rectangle(rect1.X + rect1.Width - 1, rect1.Y + 4, 1, rect1.Height - 8), rect2); if(!collision.IsEmpty) return 4;
                collision = Rectangle.Intersect(new Rectangle(rect1.X, rect1.Y + 4, 1, rect1.Height - 8), rect2); if(!collision.IsEmpty) return 3;



                //Rectangle rect1_1 = new Rectangle(x1       + 2, y1 + r1h    , r1w - 4, 1      );
                //Rectangle rect1_2 = new Rectangle(x1       + 2, y1          , r1w - 4, 1      );
                //Rectangle rect1_3 = new Rectangle(x1      , y1           + 2, 1      , r1h - 4);
                //Rectangle rect1_4 = new Rectangle(x1 + r1w, y1           + 2, 1      , r1h - 4);
                //Rectangle collision1 = Rectangle.Intersect(rect1_1, rect2);
                //Rectangle collision2 = Rectangle.Intersect(rect1_2, rect2);
                //Rectangle collision3 = Rectangle.Intersect(rect1_3, rect2);
                //Rectangle collision4 = Rectangle.Intersect(rect1_4, rect2);
                //if(!collision3.IsEmpty) {
                //    return 3;
                //}
                //if(!collision4.IsEmpty) {
                //    return 4;
                //}
                //if(!collision1.IsEmpty) {
                //    return 1;
                //}
                //if(!collision2.IsEmpty) {
                //    return 2;
                //}
            }
            return 0;
        }

        public int Detection_Platform(string _id1, int _x1, int _y1, Rectangle _rect1, string _id2, int _x2, int _y2, Rectangle _rect2, bool _is_falling) {
            string id1 = _id1;
            //int id2 = _id2;
            int x1  = _x1;
            int x2  = _x2;
            int y1  = _y1;
            int y2  = _y2;
            int r1h = _rect1.Height;
            int r2h = _rect2.Height;
            int r1w = _rect1.Width;
            int r2w = _rect2.Width;
            bool is_falling = _is_falling;
            Rectangle rect1 = new Rectangle(x1 + _rect1.X, y1 + _rect1.Y, r1w, r1h);
            Rectangle rect2 = new Rectangle(x2 + _rect2.X, y2 + _rect2.Y, r2w, r2h);
            Rectangle collision;
            collision = Rectangle.Intersect(rect1, rect2);
            if(!collision.IsEmpty) {
                collision = Rectangle.Intersect(new Rectangle(rect1.X + 5, rect1.Y + rect1.Height - 10, rect1.Width - 10, 11), rect2); if(!collision.IsEmpty) return 10 + collision.Height;
            }
            return 0;
        }

        public int Detection_Mino(string _id1, int _x1, int _y1, Rectangle _rect1, string _id2, int _x2, int _y2, Rectangle _rect2, bool _is_falling) {
            string id1 = _id1;
            //int id2 = _id2;
            int x1  = _x1;
            int x2  = _x2;
            int y1  = _y1;
            int y2  = _y2;
            int r1h = _rect1.Height;
            int r2h = _rect2.Height;
            int r1w = _rect1.Width;
            int r2w = _rect2.Width;
            bool is_falling = _is_falling;
            Rectangle rect1 = new Rectangle(x1 + _rect1.X, y1 + _rect1.Y, r1w, r1h);
            Rectangle rect2 = new Rectangle(x2 + _rect2.X, y2 + _rect2.Y, r2w, r2h);
            Rectangle collision;
            collision = Rectangle.Intersect(rect1, rect2);
            if(!collision.IsEmpty) {


                collision = Rectangle.Intersect(new Rectangle(rect1.X, rect1.Y + 6, 1, rect1.Height - 12), rect2); if(!collision.IsEmpty) return 3;
                collision = Rectangle.Intersect(new Rectangle(rect1.X + rect1.Width, rect1.Y + 6, 1, rect1.Height - 12), rect2); if(!collision.IsEmpty) return 4;
                collision = Rectangle.Intersect(new Rectangle(rect1.X + 5, rect1.Y, rect1.Width - 10, 1), rect2); if(!collision.IsEmpty) return 2;
                collision = Rectangle.Intersect(new Rectangle(rect1.X + 5, rect1.Y + rect1.Height - 5, rect1.Width - 10, 6), rect2); if(!collision.IsEmpty) return 10 + collision.Height;


                //Rectangle rect1_1 = new Rectangle(x1       + 2, y1 + r1h    , r1w - 4, 1      );
                //Rectangle rect1_2 = new Rectangle(x1       + 2, y1          , r1w - 4, 1      );
                //Rectangle rect1_3 = new Rectangle(x1      , y1           + 2, 1      , r1h - 4);
                //Rectangle rect1_4 = new Rectangle(x1 + r1w, y1           + 2, 1      , r1h - 4);
                //Rectangle collision1 = Rectangle.Intersect(rect1_1, rect2);
                //Rectangle collision2 = Rectangle.Intersect(rect1_2, rect2);
                //Rectangle collision3 = Rectangle.Intersect(rect1_3, rect2);
                //Rectangle collision4 = Rectangle.Intersect(rect1_4, rect2);
                //if(!collision3.IsEmpty) {
                //    return 3;
                //}
                //if(!collision4.IsEmpty) {
                //    return 4;
                //}
                //if(!collision1.IsEmpty) {
                //    return 1;
                //}
                //if(!collision2.IsEmpty) {
                //    return 2;
                //}
            }
            return 0;
        }

        public bool Detection_Menu(Vector2 _menupos, Texture2D _menutex, Vector2 _mousepos, float _screen_scale_height, float _screen_shift) {
            Rectangle menu = new Rectangle((int)(_menupos.X * _screen_scale_height + _screen_shift), (int)(_menupos.Y * _screen_scale_height), (int)(_menutex.Width * _screen_scale_height), (int)(_menutex.Height * _screen_scale_height));
            Vector2  mouse = _mousepos;
            if(menu.Location.X < mouse.X && mouse.X < menu.Location.X + menu.Width && menu.Location.Y < mouse.Y && mouse.Y < menu.Location.Y + menu.Height) {
                return true;
            }
            return false;
        }
    }
}
