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
        static RadioButton[] VideoRadioButtons;
        static PictureBox[] BattlePartySlots;
        static PictureBox[] PokemonPanel1Sprites;
        static Label[] PokemonPanel1Text;
        static Label[] PokemonPanel2Text;
        static Label[] PokemonPanel3Text;
        static Label[] PokemonPanel4Text;
        static Button[] FileButtons;

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
            FileButtons = new Button[] { ButtonOpenFile, ButtonExport, ButtonImport };
        }

        //Battle Video Pokemon Tab
        //Opens the Battle Video Pokemon file and loads the data.
        private void OpenFileButton_Click(object sender, EventArgs e)
        { FuncFormMain.OpenFileButton_Action(VideoRadioButtons, BattlePartySlots, FileButtons); }

        private void VideoExportButton_Click(object sender, EventArgs e)
        { FileHandlers.ExportBattleVideo(Functions.CurrentBattleVideo); }

        private void ButtonImport_Click(object sender, EventArgs e)
        { VideoImporter.ImportBattleVideo5(); }

        //Selects Battle Video.
        private void PersonalVideoRadioButton_CheckedChanged(object sender, EventArgs e)
        { FuncFormMain.VideoRadioButton_Action((RadioButton)sender, 0, BattlePartySlots); }

        private void Video1RadioButton_CheckedChanged(object sender, EventArgs e)
        { FuncFormMain.VideoRadioButton_Action((RadioButton)sender, 1, BattlePartySlots); }

        private void Video2RadioButton_CheckedChanged(object sender, EventArgs e)
        { FuncFormMain.VideoRadioButton_Action((RadioButton)sender, 2, BattlePartySlots); }

        private void Video3RadioButton_CheckedChanged(object sender, EventArgs e)
        { FuncFormMain.VideoRadioButton_Action((RadioButton)sender, 3, BattlePartySlots); }

        //Selects the Pokemon
        private void OpponentPartySlot1_Click(object sender, EventArgs e)
        { GuiFormMain.UpdateCurrentPokemon(0, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void OpponentPartySlot2_Click(object sender, EventArgs e)
        { GuiFormMain.UpdateCurrentPokemon(1, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void OpponentPartySlot3_Click(object sender, EventArgs e)
        { GuiFormMain.UpdateCurrentPokemon(2, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void OpponentPartySlot4_Click(object sender, EventArgs e)
        { GuiFormMain.UpdateCurrentPokemon(3, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void OpponentPartySlot5_Click(object sender, EventArgs e)
        { GuiFormMain.UpdateCurrentPokemon(4, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void OpponentPartySlot6_Click(object sender, EventArgs e)
        { GuiFormMain.UpdateCurrentPokemon(5, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void PlayerPartySlot1_Click(object sender, EventArgs e)
        { GuiFormMain.UpdateCurrentPokemon(6, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void PlayerPartySlot2_Click(object sender, EventArgs e)
        { GuiFormMain.UpdateCurrentPokemon(7, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void PlayerPartySlot3_Click(object sender, EventArgs e)
        { GuiFormMain.UpdateCurrentPokemon(8, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void PlayerPartySlot4_Click(object sender, EventArgs e)
        { GuiFormMain.UpdateCurrentPokemon(9, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void PlayerPartySlot5_Click(object sender, EventArgs e)
        { GuiFormMain.UpdateCurrentPokemon(10, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        private void PlayerPartySlot6_Click(object sender, EventArgs e)
        { GuiFormMain.UpdateCurrentPokemon(11, PokemonPanel1Sprites, PokemonPanel1Text, PokemonPanel2Text, PokemonPanel3Text, PokemonPanel4Text); }

        //Battle Video Pokemon Tab End



        //Battle Video Manager Tab
        private void VideoManagerFileButton_Click(object sender, EventArgs e)
        {FuncFormMain.OpenFileButton_Action(VideoRadioButtons, BattlePartySlots, FileButtons);}

        private void VideoManagerExportButton_Click(object sender, EventArgs e)
        { FileHandlers.ExportBattleVideo(Functions.CurrentBattleVideo);}


        //Battle Video Manager Tab End



        //RNG Tab
        //Calculates RNG calls based on the given seed
        private void Button1_Click(object sender, EventArgs e)
        {
            uint Magic1 = 0x41C64E6D;
            uint Magic2 = 0x6073;
            uint CurrentSeed = Convert.ToUInt32(textBox1.Text);

            richTextBox1.Text = "";

            for (int Counter = 0;  Counter < 255; Counter++)
            {
                CurrentSeed = (Magic1 * CurrentSeed) + Magic2;
                richTextBox1.AppendText(CurrentSeed.ToString("X8") + "\n");
            }
        }

        //Converts battle video decryption keys to RNG seeds
        private void Button2_Click(object sender, EventArgs e)
        {
            uint Seed = ((Convert.ToUInt32(textBox2.Text) ^ 0xFFFF) << 16) + Convert.ToUInt32(textBox2.Text);


            label6.Text = Seed.ToString();
            label5.Text = Seed.ToString("X8");
        }

        //kazo rng thing
        private void button3_Click(object sender, EventArgs e)
        {
            uint Magic1 = 0xEEB9EB65;
            uint Magic2 = 0x0A3561A1;
            uint CurrentSeed = Convert.ToUInt32(textBox3.Text);

            richTextBox1.Text = "";

            for (int Counter = 0; Counter < 255; Counter++)
            {
                CurrentSeed = (Magic1 * CurrentSeed) + Magic2;
                richTextBox2.AppendText(CurrentSeed.ToString("X8") + "\n");
            }
        }

        private uint rand32(uint seed)
        {
            seed *= 0x41C64E6D;
            seed += 0x00006073;
            seed &= 0xFFFFFFFF;
            return seed;
        }

        //decryption rng seed finder
        private void button4_Click(object sender, EventArgs e)
        {
            uint a1 = 0x71F8 ^ 0x8965;
            uint a2 = 0x3077 ^ 0x6C07;
            uint a3 = 0x8ECC ^ 0x8B65;
            uint a4 = 0xF254 ^ 0x5D58;
            uint a5 = 0x85EC ^ 0x9EC3;
            uint seed = 0;

            for (uint i = 0; i < 0x10000; i++)
            {
                seed = (a1 << 16) + i;
                if (((rand32(seed) >> 16) == a2) && ((rand32(rand32(seed)) >> 16) == a3) && ((rand32(rand32(rand32(seed))) >> 16) == a4) && ((rand32(rand32(rand32(rand32(seed)))) >> 16) == a5))
                {       
                    break;
                }
            }

            label8.Text = seed.ToString();
            label9.Text = seed.ToString("X8");
        }

        //RNG Tab End
    }
}
