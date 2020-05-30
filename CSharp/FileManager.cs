using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MaliceRT {
    class FileManager {
        // settings
        string filename_HighScore        = "UserProfile";
        string filename_Level_Editor_1   = "MapEditor1";
        string filename_Level_Editor_2   = "MapEditor2";
        string filename_Level_Editor_3   = "MapEditor3";
        string filename_Level_Editor_4   = "MapEditor4";
        string filename_Level_Editor_5   = "MapEditor5";

        MapRoom maproom = new MapRoom();

        int[,] highscore = new int[25,12]; //300
        string[] editor_level1 = new string[326];
        string[] editor_level2 = new string[326];
        string[] editor_level3 = new string[326];
        string[] editor_level4 = new string[326];
        string[] editor_level5 = new string[326];

        Windows.Storage.StorageFile file_score;



        public async void Initialize() {
            string[] temp = new string[300];
            file_score = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(filename_HighScore, Windows.Storage.CreationCollisionOption.OpenIfExists);
            IList<string> list = await Windows.Storage.FileIO.ReadLinesAsync(file_score);
            if(list.Count != 300) {
                file_score = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(filename_HighScore, Windows.Storage.CreationCollisionOption.ReplaceExisting);
                List<string> list_temp0 = new List<string>();
                for(int i = 0; i < 300; i++) {
                    list_temp0.Add("0");
                }
                list_temp0.CopyTo(temp, 0);
                await Windows.Storage.FileIO.WriteLinesAsync(file_score, list_temp0);
            } else {
                list.CopyTo(temp, 0);
            }
            for(int y = 0; y < 12; y++) {
                for(int x = 0; x < 25; x++) {
                    highscore[x, y] = int.Parse(temp[25 * y + x]);
                }
            }

            List<string> list_temp = maproom.Get_TileMap_Blank();

            for(int i = 1; i < 6; i++) {
                file_score = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(Get_FileName(i), Windows.Storage.CreationCollisionOption.OpenIfExists);
                IList<string> listE = await Windows.Storage.FileIO.ReadLinesAsync(file_score);
                if(listE.Count != 326) {
                    file_score = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(Get_FileName(i), Windows.Storage.CreationCollisionOption.ReplaceExisting);
                    list_temp.CopyTo(Get_ELevel(i), 0);
                    await Windows.Storage.FileIO.WriteLinesAsync(file_score, list_temp);
                } else {
                    listE.CopyTo(Get_ELevel(i), 0);
                }
            }
            //await file_score.DeleteAsync();
        }

        public async void Save() {
            //file_score = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(filename_HighScore, Windows.Storage.CreationCollisionOption.ReplaceExisting);
            List<string> list = new List<string>();

            for(int y = 0; y < 12; y++) {
                for(int x = 0; x < 25; x++) {
                    list.Add("" + highscore[x, y]);
                }
            }

            Windows.Storage.FileIO.WriteLinesAsync(file_score, list);
            //await file_score.DeleteAsync();
        }

        public void SaveList(List<string> list, int i) {
            if(i == 1) list.CopyTo(editor_level1, 0); ;
            if(i == 2) list.CopyTo(editor_level2, 0); ;
            if(i == 3) list.CopyTo(editor_level3, 0); ;
            if(i == 4) list.CopyTo(editor_level4, 0); ;
            if(i == 5) list.CopyTo(editor_level5, 0); ;
        }

        public string[] LoadList(int i) {
            if(i == 1) return editor_level1;
            if(i == 2) return editor_level2;
            if(i == 3) return editor_level3;
            if(i == 4) return editor_level4;
            if(i == 5) return editor_level5;
            return editor_level1;
        }

        private string Get_FileName(int i) {
            if(i == 1) return filename_Level_Editor_1;
            if(i == 2) return filename_Level_Editor_2;
            if(i == 3) return filename_Level_Editor_3;
            if(i == 4) return filename_Level_Editor_4;
            if(i == 5) return filename_Level_Editor_5;
            return filename_Level_Editor_1;
        }

        private string[] Get_ELevel(int i) {
            if(i == 1) return editor_level1;
            if(i == 2) return editor_level2;
            if(i == 3) return editor_level3;
            if(i == 4) return editor_level4;
            if(i == 5) return editor_level5;
            return editor_level1;
        }

        public async void SaveWorld(List<string> list, int i) {
            Windows.Storage.StorageFile file;
            file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(Get_FileName(i), Windows.Storage.CreationCollisionOption.ReplaceExisting);
            Windows.Storage.FileIO.WriteLinesAsync(file, list);
            //await file_score.DeleteAsync();
        }

        public int Transmute(string s) {
            if(s == "grass") return 0;
            if(s == "vulcano") return 1;
            if(s == "sky") return 2;
            if(s == "desert") return 3;
            if(s == "beach") return 4;
            if(s == "snow") return 5;
            if(s == "mountain") return 6;
            if(s == "mashine") return 7;
            if(s == "forest") return 8;
            if(s == "aurora") return 9;
            if(s == "space") return 10;
            if(s == "editor") return 11;
            return 0;
        }

        public int Get_Highscore(string s, int i, int j) {
            int w = Transmute(s);

            return highscore[5 * (i - 1) + j, w];
        }

        public void Set_Highscore(string s, int i, int x) {
            int w = Transmute(s);

            int[] score = new int[5];
            score[0] = highscore[5 * (i - 1) + 0, w];
            score[1] = highscore[5 * (i - 1) + 1, w];
            score[2] = highscore[5 * (i - 1) + 2, w];
            score[3] = highscore[5 * (i - 1) + 3, w];
            score[4] = highscore[5 * (i - 1) + 4, w];

            if(score[0] < x) {
                score[4] = score[3];
                score[3] = score[2];
                score[2] = score[1];
                score[1] = score[0];
                score[0] = x;
            } else if(score[1] < x) {
                score[4] = score[3];
                score[3] = score[2];
                score[2] = score[1];
                score[1] = x;
            } else if(score[2] < x) {
                score[4] = score[3];
                score[3] = score[2];
                score[2] = x;
            } else if(score[3] < x) {
                score[4] = score[3];
                score[3] = x;
            } else if(score[4] < x) {
                score[4] = x;
            }

            highscore[5 * (i - 1) + 0, w] = score[0];
            highscore[5 * (i - 1) + 1, w] = score[1];
            highscore[5 * (i - 1) + 2, w] = score[2];
            highscore[5 * (i - 1) + 3, w] = score[3];
            highscore[5 * (i - 1) + 4, w] = score[4];
            Save();
        }

        public void Reset_Highscore(string s, int i) {
            int w = Transmute(s);
            highscore[5 * (i - 1) + 0, w] = 0;
            highscore[5 * (i - 1) + 1, w] = 0;
            highscore[5 * (i - 1) + 2, w] = 0;
            highscore[5 * (i - 1) + 3, w] = 0;
            highscore[5 * (i - 1) + 4, w] = 0;
        }

        private void Kill_Highscore(int[] i) {
            i[0] = 0;
            i[1] = 0;
            i[2] = 0;
            i[3] = 0;
            i[4] = 0;
        }
    }
}
