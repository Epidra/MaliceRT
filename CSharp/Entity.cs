using System;
using System.Collections.Generic;
using System.Text;

namespace MaliceRT {
    class Entity {
        string id;
        int posX;
        int posY;
        int value;

        public Entity(string _id, int _posX, int _posY, int _value) {
            id = _id;
            if(id == "explosion") {
                posX = _posX;
                posY = _posY;
            } else if(id == "coin") {
                posX = _posX * 32;
                posY = _posY * 32 - 8;
            } else {
                posX = _posX * 32;
                posY = _posY * 32;
            }
            value = _value;
        }

        public void Update() {

        }

        public string Get_ID() {
            return id;
        }

        public int Get_PosX() {
            return posX;
        }

        public int Get_PosY() {
            return posY;
        }

        public int Get_Value() {
            return value;
        }

        public void Change_Value(int i) {
            value = value + i;
        }
    }
}
