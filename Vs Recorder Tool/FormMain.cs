using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vs_Recorder_Tool.Properties;

namespace Vs_Recorder_Tool
{
    public partial class FormMain : Form
    {
        RadioButton[] VideoRadioButtons;
        PictureBox[] BattlePartySlots;
        PictureBox[] PokemonPanel1Sprites;
        Label[] PokemonPanel1Text;
        Label[] PokemonPanel2Text;
        Label[] PokemonPanel3Text;
        Label[] PokemonPanel4Text;

        public FormMain()
        {
            InitializeComponent();
            VideoRadioButtons = new RadioButton[] { RadioButtonPersonalVideo, RadioButtonVideo1, RadioButtonVideo2, RadioButtonVideo3};
            BattlePartySlots = new PictureBox[] { PictureOpponentParty1, PictureOpponentParty2, PictureOpponentParty3, PictureOpponentParty4, PictureOpponentParty5 , PictureOpponentParty6, PicturePlayerParty1, PicturePlayerParty2 , PicturePlayerParty3, PicturePlayerParty4, PicturePlayerParty5, PicturePlayerParty6 };
            PokemonPanel1Sprites = new PictureBox[] { PictureFrontSprite, PicturePokeBall, PictureNationality };
            PokemonPanel1Text = new Label[] { TextNickname, TextSpecies, TextHoldItem };
            PokemonPanel2Text = new Label[] { TextPID, TextNature, TextLevel, TextExperience, TextAbility, TextFriendship, TextTrainer };
            PokemonPanel3Text = new Label[] { TextHPStat, TextAttackStat, TextDefenseStat, TextSpeedStat, TextSpecialAttackStat, TextSpecialDefenseStat, TextHPEVs, TextAttackEVs, TextDefenseEVs, TextSpeedEVs, TextSpecialAttackEVs, TextSpecialDefenseEVs, TextHPIVs, TextAttackIVs, TextDefenseIVs, TextSpeedIVs, TextSpecialAttackIVs, TextSpecialDefenseIVs };
            PokemonPanel4Text = new Label[] { TextMove1, TextMove2, TextMove3, TextMove4, TextMove1PP, TextMove2PP, TextMove3PP, TextMove4PP };
        }

        //Battle Video Pokemon Tab
        //Opens the Battle Video Pokemon file and loads the data.
        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            SaveReader.OpenUserFile(VideoRadioButtons, BattlePartySlots);
        }


        private void VideoExportButton_Click(object sender, EventArgs e)
        {
            SaveReader.ExportBattleVideo(Functions.CurrentBattleVideo);
        }

        //Selects Battle Video.
        private void PersonalVideoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Functions.CurrentBattleVideo = 0;
            BattleVideoPokemonParser.OpenBattleVideoPokemonFile(0);
            FuncFormMain.UpdatePreviewPokemon(BattlePartySlots);
        }

        private void Video1RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Functions.CurrentBattleVideo = 1;
            BattleVideoPokemonParser.OpenBattleVideoPokemonFile(1);
            FuncFormMain.UpdatePreviewPokemon(BattlePartySlots);
        }

        private void Video2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Functions.CurrentBattleVideo = 2;
            BattleVideoPokemonParser.OpenBattleVideoPokemonFile(2);
            FuncFormMain.UpdatePreviewPokemon(BattlePartySlots);
        }

        private void Video3RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Functions.CurrentBattleVideo = 3;
            BattleVideoPokemonParser.OpenBattleVideoPokemonFile(3);
            FuncFormMain.UpdatePreviewPokemon(BattlePartySlots);
        }

        //Selects the Pokemon
        private void OpponentPartySlot1_Click(object sender, EventArgs e)
        { FuncFormMain.SetCurrentPokemon(0, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void OpponentPartySlot2_Click(object sender, EventArgs e)
        { FuncFormMain.SetCurrentPokemon(1, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void OpponentPartySlot3_Click(object sender, EventArgs e)
        { FuncFormMain.SetCurrentPokemon(2, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void OpponentPartySlot4_Click(object sender, EventArgs e)
        { FuncFormMain.SetCurrentPokemon(3, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void OpponentPartySlot5_Click(object sender, EventArgs e)
        { FuncFormMain.SetCurrentPokemon(4, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void OpponentPartySlot6_Click(object sender, EventArgs e)
        { FuncFormMain.SetCurrentPokemon(5, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void PlayerPartySlot1_Click(object sender, EventArgs e)
        { FuncFormMain.SetCurrentPokemon(6, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void PlayerPartySlot2_Click(object sender, EventArgs e)
        { FuncFormMain.SetCurrentPokemon(7, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void PlayerPartySlot3_Click(object sender, EventArgs e)
        { FuncFormMain.SetCurrentPokemon(8, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void PlayerPartySlot4_Click(object sender, EventArgs e)
        { FuncFormMain.SetCurrentPokemon(9, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void PlayerPartySlot5_Click(object sender, EventArgs e)
        { FuncFormMain.SetCurrentPokemon(10, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void PlayerPartySlot6_Click(object sender, EventArgs e)
        { FuncFormMain.SetCurrentPokemon(11, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        //Battle Video Pokemon Tab End



        //Battle Video Manager Tab
        private void VideoManagerFileButton_Click(object sender, EventArgs e)
        {SaveReader.OpenUserFile(VideoRadioButtons, BattlePartySlots);}

        private void VideoManagerExportButton_Click(object sender, EventArgs e)
        { SaveReader.ExportBattleVideo(Functions.CurrentBattleVideo);}


        //Battle Video Manager Tab End



        //Box Upload Viewer Tab
        private void BoxUploadPokemon1_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(0);}

        private void BoxUploadPokemon2_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(1);}

        private void BoxUploadPokemon3_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(2);}

        private void BoxUploadPokemon4_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(3);}

        private void BoxUploadPokemon5_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(4);}

        private void BoxUploadPokemon6_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(5);}

        private void BoxUploadPokemon7_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(6);}

        private void BoxUploadPokemon8_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(7);}

        private void BoxUploadPokemon9_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(8);}

        private void BoxUploadPokemon10_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(9);}

        private void BoxUploadPokemon11_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(10);}

        private void BoxUploadPokemon12_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(11);}

        private void BoxUploadPokemon13_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(12);}

        private void BoxUploadPokemon14_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(13);}

        private void BoxUploadPokemon15_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(14);}

        private void BoxUploadPokemon16_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(15);}

        private void BoxUploadPokemon17_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(16);}

        private void BoxUploadPokemon18_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(17);}

        private void BoxUploadPokemon19_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(18);}

        private void BoxUploadPokemon20_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(19);}

        private void BoxUploadPokemon21_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(20);}

        private void BoxUploadPokemon22_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(21);}

        private void BoxUploadPokemon23_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(22);}

        private void BoxUploadPokemon24_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(23);}

        private void BoxUploadPokemon25_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(24);}

        private void BoxUploadPokemon26_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(25);}

        private void BoxUploadPokemon27_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(26);}

        private void BoxUploadPokemon28_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(27);}

        private void BoxUploadPokemon29_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(28);}

        private void BoxUploadPokemon30_Click(object sender, EventArgs e)
        {SetCurrentBoxUploadPokemon(29);}

        //Opens the Box Upload Data file and loads the data.
        private void BoxUploadOpenFileButton_Click(object sender, EventArgs e)
        {
            /*
            BoxUpload.OpenBoxUploadFile();
            BoxUploadBoxName.Text = Functions.BoxName;
            BoxUploadPokemon1.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[0].SpeciesID, Functions.BoxUploadList[0].FormeByte);
            BoxUploadPokemon2.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[1].SpeciesID, Functions.BoxUploadList[1].FormeByte);
            BoxUploadPokemon3.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[2].SpeciesID, Functions.BoxUploadList[2].FormeByte);
            BoxUploadPokemon4.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[3].SpeciesID, Functions.BoxUploadList[3].FormeByte);
            BoxUploadPokemon5.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[4].SpeciesID, Functions.BoxUploadList[4].FormeByte);
            BoxUploadPokemon6.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[5].SpeciesID, Functions.BoxUploadList[5].FormeByte);
            BoxUploadPokemon7.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[6].SpeciesID, Functions.BoxUploadList[6].FormeByte);
            BoxUploadPokemon8.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[7].SpeciesID, Functions.BoxUploadList[7].FormeByte);
            BoxUploadPokemon9.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[8].SpeciesID, Functions.BoxUploadList[8].FormeByte);
            BoxUploadPokemon10.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[9].SpeciesID, Functions.BoxUploadList[9].FormeByte);
            BoxUploadPokemon11.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[10].SpeciesID, Functions.BoxUploadList[10].FormeByte);
            BoxUploadPokemon12.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[11].SpeciesID, Functions.BoxUploadList[11].FormeByte);
            BoxUploadPokemon13.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[12].SpeciesID, Functions.BoxUploadList[12].FormeByte);
            BoxUploadPokemon14.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[13].SpeciesID, Functions.BoxUploadList[13].FormeByte);
            BoxUploadPokemon15.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[14].SpeciesID, Functions.BoxUploadList[14].FormeByte);
            BoxUploadPokemon16.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[15].SpeciesID, Functions.BoxUploadList[15].FormeByte);
            BoxUploadPokemon17.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[16].SpeciesID, Functions.BoxUploadList[16].FormeByte);
            BoxUploadPokemon18.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[17].SpeciesID, Functions.BoxUploadList[17].FormeByte);
            BoxUploadPokemon19.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[18].SpeciesID, Functions.BoxUploadList[18].FormeByte);
            BoxUploadPokemon20.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[19].SpeciesID, Functions.BoxUploadList[19].FormeByte);
            BoxUploadPokemon21.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[20].SpeciesID, Functions.BoxUploadList[20].FormeByte);
            BoxUploadPokemon22.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[21].SpeciesID, Functions.BoxUploadList[21].FormeByte);
            BoxUploadPokemon23.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[22].SpeciesID, Functions.BoxUploadList[22].FormeByte);
            BoxUploadPokemon24.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[23].SpeciesID, Functions.BoxUploadList[23].FormeByte);
            BoxUploadPokemon25.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[24].SpeciesID, Functions.BoxUploadList[24].FormeByte);
            BoxUploadPokemon26.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[25].SpeciesID, Functions.BoxUploadList[25].FormeByte);
            BoxUploadPokemon27.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[26].SpeciesID, Functions.BoxUploadList[26].FormeByte);
            BoxUploadPokemon28.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[27].SpeciesID, Functions.BoxUploadList[27].FormeByte);
            BoxUploadPokemon29.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[28].SpeciesID, Functions.BoxUploadList[28].FormeByte);
            BoxUploadPokemon30.Image = FuncPokemonSprites.MenuSpritePath(Functions.BoxUploadList[29].SpeciesID, Functions.BoxUploadList[29].FormeByte);
            */
        }

        
        //Sets the current Box Upload Pokemon preview.
        public void SetCurrentBoxUploadPokemon(int CurrentPokemon)
        {
            //BoxUploadFrontSprite.Image = PokemonSprites.FrontSprite(Functions.BoxUploadList[CurrentPokemon]);
            BoxUploadSpeciesTextBox.Text = Functions.BoxUploadList[CurrentPokemon].Species;
        }
        
        //Box Upload Viewer Tab



        //RNG Tab
        //Calculates RNG calls based on the given seed
        private void Button1_Click(object sender, EventArgs e)
        {
            int Counter = 0;
            byte[] CurrentSeedBytes = { 0, 0, 0, 0 };
            uint Magic1 = 0x41C64E6D;
            uint Magic2 = 0x6073;
            uint CurrentSeed = Convert.ToUInt32(textBox1.Text);

            richTextBox2.Text = "";

            while (Counter < 255)
            {
                CurrentSeed = (Magic1 * CurrentSeed) + Magic2;
                CurrentSeedBytes[3] = Convert.ToByte((CurrentSeed >> 24) & 0xFF);
                CurrentSeedBytes[2] = Convert.ToByte((CurrentSeed >> 16) & 0xFF);
                CurrentSeedBytes[1] = Convert.ToByte((CurrentSeed >> 8) & 0xFF);
                CurrentSeedBytes[0] = Convert.ToByte((CurrentSeed) & 0xFF);
                richTextBox2.AppendText(BitConverter.ToString(CurrentSeedBytes, 0, 2) + "   " + BitConverter.ToString(CurrentSeedBytes, 2, 2) + "\n");
                Counter++;
            }
        }

        //Converts battle video decryption keys to RNG seeds
        private void Button2_Click(object sender, EventArgs e)
        {
            uint Seed = 0;
            byte[] SeedBytes = { 0, 0, 0, 0 };
            Seed = ((Convert.ToUInt32(textBox2.Text) ^ 0xFFFF) << 16) + Convert.ToUInt32(textBox2.Text);
            SeedBytes[0] = Convert.ToByte((Seed >> 24) & 0xFF);
            SeedBytes[1] = Convert.ToByte((Seed >> 16) & 0xFF);
            SeedBytes[2] = Convert.ToByte((Seed >> 8) & 0xFF);
            SeedBytes[3] = Convert.ToByte((Seed) & 0xFF);

            label6.Text = Convert.ToString(Seed);
            label5.Text = BitConverter.ToString(SeedBytes, 0, 4);
        }


        //RNG Tab End
    }
}
