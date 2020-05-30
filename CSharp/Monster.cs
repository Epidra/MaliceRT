using System;
using System.Collections.Generic;
using System.Text;

namespace MaliceRT {
    class Monster {
        string id;
        int hp;
        int posX;
        int posY;
        int value;

        int speed_movement;
        int speed_fall;
        int speed_jump;

        string touching_wall;

        bool is_hurt;
        bool is_blinking;
        bool facing_right;
        bool is_jumping;
        bool is_falling;
        bool is_moving_left;
        bool is_moving_right;
        bool is_flying;
        bool drag;

        public Monster(string _id, int _hp, int _posX, int _posY, int _value, int _speedmovement, int _speedfall, int _speedjump, bool _isflying) {
            id = _id;
            hp = _hp;

            value = _value;
            speed_movement = _speedmovement;
            speed_fall = _speedfall;
            speed_jump = _speedjump;
            is_hurt = false;
            is_blinking = false;
            facing_right = false;
            is_jumping = false;
            is_falling = false;
            is_moving_left = false;
            is_moving_right = false;
            is_flying = _isflying;
            drag = false;
            touching_wall = "void";
            if(id == "shot_violett" || id == "shot_red") {
                posX = _posX;
                posY = _posY;
                if(_speedmovement > 0) { facing_right = false; is_moving_left = true; is_moving_right = false; }
                if(_speedmovement < 0) { facing_right = true; is_moving_left = false; is_moving_right = true; }
            } else if(id == "explosion") {
                posX = _posX;
                posY = _posY;
            } else {
                posX = _posX * 32;
                posY = _posY * 32;
            }
            if(id == "malice") facing_right = true;
            if(id != "malice") is_moving_left = true;
            if(id == "irrlicht_violett" || id == "irrlicht_red") {
                if(speed_movement < 0) {
                    is_moving_left = true;
                    facing_right = false;
                    speed_movement = speed_movement * -1;
                } else if(speed_movement > 0) {
                    is_moving_left = false;
                    is_moving_right = true;
                    facing_right = true;
                }
            }
        }

        public void Move(string direction, int x) {
            if(direction == "left") {
                if(drag) {
                    posX--;
                } else {
                    posX = -x + posX;
                }
            }
            if(direction == "right") {
                if(drag) {
                    posX++;
                } else {
                    posX = x + posX;
                }
            }
        }

        public void Update() {
            if(id == "platform") {
                if(is_moving_left && value == 0) {
                    posY = -speed_movement + posY;
                } else if(is_moving_right && value == 0) {
                    posY = speed_movement + posY;
                } else if(is_moving_left && value == 1) {
                    posX = -speed_movement + posX;
                } else if(is_moving_right && value == 1) {
                    posX = speed_movement + posX;
                }
            } else {
                if(is_moving_left) {
                    if(drag) {
                        posX--;
                    } else {
                        posX = -speed_movement + posX;
                    }
                } else if(is_moving_right) {
                    if(drag) {
                        posX++;
                    } else {
                        posX = speed_movement + posX;
                    }
                } else {

                }
                if(is_jumping) {
                    posY = -speed_jump + posY;
                } else if(is_falling) {
                    if(!is_flying)
                        posY = speed_fall + posY;
                }
            }
        }

        public bool get_Is_Flying() {
            return is_flying;
        }

        public bool get_Facing_Right() {
            return facing_right;
        }

        public void set_Facing_Right(bool b) {
            this.facing_right = b;
        }

        public string Get_ID() {
            return id;
        }

        public int Get_HP() {
            return hp;
        }

        public void Set_HP(int i) {
            hp = i;
        }

        public void Change_HP(int j) {
            hp = hp + j;
        }

        public int Get_PosX() {
            return posX;
        }

        public void Set_PosX(int i) {
            posX = i;
        }

        public int Get_PosY() {
            return posY;
        }

        public void Set_PosY(int i) {
            posY = i;
        }

        public bool get_is_Jumping() {
            return is_jumping;
        }

        public void set_is_Jumping(bool b) {
            is_jumping = b;
        }

        public bool get_is_Falling() {
            return is_falling;
        }

        public void set_is_Falling(bool b) {
            is_falling = b;
        }

        public string get_Movement() {
            if(is_moving_left) {
                return "left";
            } else if(is_moving_right) {
                return "right";
            } else {
                return "still";
            }
        }

        public void set_Movement(string z) {
            switch(z) {
                case "still": is_moving_left = false; is_moving_right = false; break;
                case "left": is_moving_left = true; is_moving_right = false; break;
                case "right": is_moving_left = false; is_moving_right = true; break;
            }
        }

        public int Get_Value() {
            return value;
        }

        public int Get_MovementI() {
            return speed_movement;
        }

        public void Set_Draggin(bool _drag) {
            drag = _drag;
        }

        public bool Get_Drag() {
            return drag;
        }

        public void Set_Touch_Wall(string s) {
            touching_wall = s;
        }

        public string Get_Touch_Wall() {
            return touching_wall;
        }

    }
}
