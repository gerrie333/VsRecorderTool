using System;
using System.Drawing;
using System.Windows.Forms;
using Vs_Recorder_Tool;

public class GuiFormMain
{
    //Graphics Updating Functions
    //
    //Updates the available battle video slots
    public static void UpdateBattleVideoSlots(RadioButton[] VideoButtons)
    {
        for (int Counter = 0; Counter < VideoButtons.Length; Counter++)
        {
            VideoButtons[Counter].Enabled = false;
            VideoButtons[Counter].Checked = false;

            if (Functions.DecryptedVideoList[Counter] == null) { continue; }
            VideoButtons[Counter].Enabled = true;
        }
    }




    //Updates Pokemon Team Previews
    public static void UpdatePreviewPokemon(PictureBox[] BattlePartySlots)
    {

        for (int Counter = 0; Counter < 12; Counter++)
        {
            BattlePartySlots[Counter].Enabled = false;
            BattlePartySlots[Counter].Image = PokemonSprites.MenuSprite(Functions.PokemonList[Counter]);

            if (Functions.PokemonList[Counter].SpeciesID != 0)
            {
                BattlePartySlots[Counter].Enabled = true;
            }
        }
    }

    public static void ClearPreviewPokemon(PictureBox[] BattlePartySlots)
    {
        for (int Counter = 0; Counter < 12; Counter++)
        {
            BattlePartySlots[Counter].Enabled = false;
            BattlePartySlots[Counter].Image = null;
        }
    }


    //Fills out the info boxes with the specified pokemon's data.
    public static void UpdateCurrentPokemon(int PokemonSlot, PictureBox[] Panel1Sprites, Label[] Panel1Text, Label[] Panel2Text, Label[] Panel3Text, Label[] Panel4Text)
    {
        //Panel 1: Pokemon Overview
        Panel1Text[0].Text = Functions.PokemonList[PokemonSlot].Nickname;
        Panel1Text[1].Text = Functions.PokemonList[PokemonSlot].GetSpecies();
        Panel1Text[2].Text = Functions.PokemonList[PokemonSlot].GetHoldItem();
        Panel1Sprites[0].BackgroundImage = PokemonSprites.FrontSprite(Functions.PokemonList[PokemonSlot]);
        Panel1Sprites[0].Image = PokemonSprites.HoldItemSprite(Functions.PokemonList[PokemonSlot].HoldItemID);
        Panel1Sprites[1].Image = PokemonSprites.PokeBallSprite(Functions.PokemonList[PokemonSlot].PokeBall);
        Panel1Sprites[2].Image = (Bitmap)ResIcons.ResourceManager.GetObject("_flags_" + Convert.ToString(Functions.PokemonList[PokemonSlot].Nationality));


        //Panel 2; Summary info
        Panel2Text[0].Text = Functions.PokemonList[PokemonSlot].PID.ToString("X8");
        Panel2Text[1].Text = Functions.PokemonList[PokemonSlot].GetNature();
        Panel2Text[2].Text = "Level " + Functions.PokemonList[PokemonSlot].Level.ToString();
        Panel2Text[3].Text = "Experience: " + Functions.PokemonList[PokemonSlot].Experience.ToString() ;
        Panel2Text[4].Text = "Ability: " + Functions.PokemonList[PokemonSlot].GetAbility();
        Panel2Text[5].Text = "Friendship: " + Functions.PokemonList[PokemonSlot].Friendship.ToString();
        Panel2Text[6].Text = Functions.PokemonList[PokemonSlot].OTName + " " + Functions.PokemonList[PokemonSlot].TrainerID.ToString() + "/" + Functions.PokemonList[PokemonSlot].SecretID.ToString();


        //Panel 3: Stats, EVs and IVs
        for (int Counter = 0; Counter < 6; Counter++)
        {
            Panel3Text[Counter].Text = Functions.PokemonList[PokemonSlot].Stats[Counter].ToString();
            Panel3Text[Counter + 6].Text = Functions.PokemonList[PokemonSlot].EVs[Counter].ToString();
            Panel3Text[Counter + 12].Text = Functions.PokemonList[PokemonSlot].IVs[Counter].ToString();
        }


        //Panel 4: Moves and PP
        for (int Counter = 0; Counter < 4; Counter++)
        {
            Panel4Text[Counter].Text = Functions.PokemonList[PokemonSlot].GetMove(Counter);
            Panel4Text[Counter + 4].Text = Functions.PokemonList[PokemonSlot].PP[Counter].ToString() + "/" + Functions.PokemonList[PokemonSlot].PPUps[Counter].ToString();
        }

    }

}