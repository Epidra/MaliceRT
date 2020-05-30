using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MaliceRT {
    class Block {
        Vector2 id;
        int posX;
        int posY;
        int is_solid;

        // 0 - Full Block
        // 1 - Jump Through
        // 2 - Decoration

        public Block(int _id, int _posX, int _posY, int _solid) {
            string s = "";
            if(_id == 0) {
                id = new Vector2(0, 0);
            } else if(_id < 10) {
                s = "000" + _id;
                id = new Vector2(int.Parse(s.Substring(2, 2)), int.Parse(s.Substring(0, 2)));
            } else if(_id < 100) {
                s = "00" + _id;
                id = new Vector2(int.Parse(s.Substring(2, 2)), int.Parse(s.Substring(0, 2)));
            } else if(_id < 1000) {
                s = "0" + _id;
                id = new Vector2(int.Parse(s.Substring(2, 2)), int.Parse(s.Substring(0, 2)));
            } else {
                s = "" + _id;
                id = new Vector2(int.Parse(s.Substring(2, 2)), int.Parse(s.Substring(0, 2)));
            }
            posX = _posX * 32;
            posY = _posY * 32;
            is_solid = _solid;
        }

        public Vector2 Get_ID() {
            return id;
        }

        public int Get_PosX() {
            return posX;
        }

        public int Get_PosY() {
            return posY;
        }

        public int Get_Is_Solid() {
            return is_solid;
        }
    }
}
