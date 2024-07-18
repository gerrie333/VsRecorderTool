using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vs_Recorder_Tool
{
    public static class FuncFormMain
    {
        //Action functions (for complex actions)
        public static void OpenFileButton_Action(RadioButton[] VideoButtons, PictureBox[] BattlePartySlots, Button[] FileButtons)
        {
            FileHandlers.OpenUserFile();

            string FileExtension = Functions.CurrentFilePath.Split('.').Last();
            switch (FileExtension)
            {
                //Save file -- Ask which game, track down ebv data and read it, decrypt it, view it
                case "sav":
                    if ((Functions.CurrentGame == Game.Platinum)||(Functions.CurrentGame == Game.HeartgoldSoulsilver)) { SaveReader.ReadFromSaveFile4(); } else { SaveReader.ReadFromSaveFile5(); };
                    SaveReader.DecryptBattleVideos(0xF);
                    GuiFormMain.UpdateBattleVideoSlots(VideoButtons);
                    GuiFormMain.ClearPreviewPokemon(BattlePartySlots);
                    FileButtons[1].Enabled = true;
                    if ((Functions.CurrentGame == Game.Black2White2) || (Functions.CurrentGame == Game.BlackWhite)) { FileButtons[2].Enabled = true; } else { FileButtons[2].Enabled = false; };
                    break;

                //EBV4 -- decrypt it, view it
                case "ebv4":
                    Functions.CurrentGame = Game.HeartgoldSoulsilver;

                    if (Functions.EncryptedVideoList[0] == null) { Functions.EncryptedVideoList[0] = new byte[Functions.BattleVideoLength4];  }
                    SaveReader.ReadBattleVideoFile(ref Functions.EncryptedVideoList[0]);

                    SaveReader.DecryptBattleVideos(0x1);
                    GuiFormMain.UpdateBattleVideoSlots(VideoButtons);
                    VideoButtons[0].Checked = true; //checks the button and triggers the UpdatePreviewPokemon function
                    FileButtons[1].Enabled = true;
                    FileButtons[2].Enabled = false;
                    break;

                //EBV5
                case "ebv5":
                    Functions.CurrentGame = Game.Black2White2;

                    if (Functions.EncryptedVideoList[0] == null) { Functions.EncryptedVideoList[0] = new byte[Functions.BattleVideoLength5]; }
                    SaveReader.ReadBattleVideoFile(ref Functions.EncryptedVideoList[0]);

                    SaveReader.DecryptBattleVideos(0x1);
                    GuiFormMain.UpdateBattleVideoSlots(VideoButtons);
                    VideoButtons[0].Checked = true; //checks the button and triggers the UpdatePreviewPokemon function
                    FileButtons[1].Enabled = true;
                    FileButtons[2].Enabled = false;
                    break;


                //DBV4 -- view it
                case "dbv4":
                    Functions.CurrentGame = Game.HeartgoldSoulsilver;

                    if (Functions.DecryptedVideoList[0] == null) { Functions.DecryptedVideoList[0] = new byte[Functions.BattleVideoLength4]; }
                    SaveReader.ReadBattleVideoFile(ref Functions.DecryptedVideoList[0]);

                    GuiFormMain.UpdateBattleVideoSlots(VideoButtons);
                    VideoButtons[0].Checked = true; //checks the button and triggers the UpdatePreviewPokemon function
                    FileButtons[1].Enabled = false;
                    FileButtons[2].Enabled = false;
                    break;

                //DBV5 -- view it
                case "dbv5":
                    Functions.CurrentGame = Game.Black2White2;

                    if (Functions.DecryptedVideoList[0] == null) { Functions.DecryptedVideoList[0] = new byte[Functions.BattleVideoLength5]; }
                    SaveReader.ReadBattleVideoFile(ref Functions.DecryptedVideoList[0]);

                    GuiFormMain.UpdateBattleVideoSlots(VideoButtons);
                    VideoButtons[0].Checked = true; //checks the button and triggers the UpdatePreviewPokemon function
                    FileButtons[1].Enabled = false;
                    FileButtons[2].Enabled = false;
                    break;

                case "ebv":
                    Functions.CurrentGame = Game.Black2White2;

                    if (Functions.EncryptedVideoList[0] == null) { Functions.EncryptedVideoList[0] = new byte[Functions.BattleVideoLength5]; }
                    SaveReader.ReadBattleVideoFile(ref Functions.EncryptedVideoList[0]);

                    SaveReader.ExtendLegacyEBV(0x0);
                    SaveReader.DecryptBattleVideos(0x1);
                    GuiFormMain.UpdateBattleVideoSlots(VideoButtons);
                    VideoButtons[0].Checked = true; //checks the button and triggers the UpdatePreviewPokemon function
                    FileButtons[1].Enabled = true;
                    FileButtons[2].Enabled = false;
                    break;
            }
        }

        public static void VideoRadioButton_Action(RadioButton VideoButton, int VideoNumber, PictureBox[] BattlePartySlots)
        {
            if (VideoButton.Checked == false) { return; }

            Functions.CurrentBattleVideo = VideoNumber;
            PokemonParser.ParseBattleVideoPokemon(VideoNumber);
            GuiFormMain.UpdatePreviewPokemon(BattlePartySlots);
        }
    }
}
