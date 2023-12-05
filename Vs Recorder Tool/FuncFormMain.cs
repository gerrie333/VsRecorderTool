using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vs_Recorder_Tool
{
    public static class FuncFormMain
    {
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


        //Fills out the info boxes with the specified pokemon's data.
        public static void SetCurrentPokemon(int PokemonSlot, PictureBox[] Panel1Sprites, Label[] Panel1Text, Label[] Panel2Text, Label[] Panel3Text, Label[] Panel4Text)
        {
            //Panel 1: Pokemon Overview
            Panel1Text[0].Text = Functions.PokemonList[PokemonSlot].Nickname;
            Panel1Text[1].Text = Functions.PokemonList[PokemonSlot].Species;
            Panel1Text[2].Text = Functions.PokemonList[PokemonSlot].HoldItem;
            Panel1Sprites[0].BackgroundImage = PokemonSprites.FrontSprite(Functions.PokemonList[PokemonSlot]);
            Panel1Sprites[0].Image = PokemonSprites.HoldItemSprite(Functions.PokemonList[PokemonSlot].HoldItemID);
            Panel1Sprites[1].Image = PokemonSprites.PokeBallSprite(Functions.PokemonList[PokemonSlot].PokeBall);
            Panel1Sprites[2].Image = (Bitmap)ResIcons.ResourceManager.GetObject("_flags_" + Convert.ToString(Functions.PokemonList[PokemonSlot].Nationality));


            //Panel 2; Summary info
            Panel2Text[0].Text = Functions.PokemonList[PokemonSlot].PIDBytes;
            Panel2Text[1].Text = Functions.PokemonList[PokemonSlot].Nature;
            Panel2Text[2].Text = "Level " + Functions.PokemonList[PokemonSlot].Level;
            Panel2Text[3].Text = "Experience: " + Functions.PokemonList[PokemonSlot].Experience;
            Panel2Text[4].Text = "Ability: " + Functions.PokemonList[PokemonSlot].Ability;
            Panel2Text[5].Text = "Friendship: " + Functions.PokemonList[PokemonSlot].Friendship;
            Panel2Text[6].Text = Convert.ToString(Functions.PokemonList[PokemonSlot].OTName + " " + Functions.PokemonList[PokemonSlot].TrainerID) + "/" + Convert.ToString(Functions.PokemonList[PokemonSlot].SecretID);


            //Panel 3: Stats, EVs and IVs
            for (int Counter = 0; Counter < 6; Counter++)
            {
                Panel3Text[Counter].Text = Functions.PokemonList[PokemonSlot].Stats[Counter];
                Panel3Text[Counter + 6].Text = Functions.PokemonList[PokemonSlot].EVs[Counter];
                Panel3Text[Counter + 12].Text = Functions.PokemonList[PokemonSlot].IVs[Counter];
            }


            //Panel 4: Moves and PP
            for (int Counter = 0; Counter < 4; Counter++)
            {
                Panel4Text[Counter].Text = Functions.PokemonList[PokemonSlot].Moves[Counter];
                Panel4Text[Counter + 4].Text = Functions.PokemonList[PokemonSlot].PP[Counter] + "/" + Functions.PokemonList[PokemonSlot].PPUps[Counter];
            }

        }
    }
}
