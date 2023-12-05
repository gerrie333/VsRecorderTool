using System;
using System.Drawing;
using System.Net.Http.Headers;
using System.Resources;
using Vs_Recorder_Tool;

public static class PokemonSprites
{
//Returns the front sprite of a pokemon based on the species, shininess and forme.
    public static Image FrontSprite(BattleVideoPokemon Pokemon)
    {
        ResourceManager RM; 
        string FrontSprite = "_";
        uint[] FormeList = { 386, 422, 423, 479, 487, 492, 493, 641, 642, 645, 646, 647 };
        int Counter = 0;

        if ((Functions.Gen5Mode == true) & (Pokemon.Shiny == true))
        {
            RM = ResFrontSprites5Shiny.ResourceManager;
        }

        else if ((Functions.Gen5Mode == true) & (Pokemon.Shiny == false))
        {
            RM = ResFrontSprites5.ResourceManager;
        }

        else if (Pokemon.Shiny == true)
        {
            RM = ResFrontSprites4Shiny.ResourceManager;
        }

        else
        {
            RM = ResFrontSprites4.ResourceManager;
        }

        FrontSprite += Convert.ToString(Pokemon.SpeciesID);

        while (Counter < FormeList.Length)
        {
            if (FormeList[Counter] == Pokemon.SpeciesID)
            {
                FrontSprite += "_" + Convert.ToString(Pokemon.FormeByte >> 3); //make 8 for battle videos
                break;
            }

            Counter++;
        }

        return (Bitmap)RM.GetObject(FrontSprite);
    }



//Returns the menu sprite based on the species and forme.
    public static Image MenuSprite(BattleVideoPokemon Pokemon)
    {
        uint[] FormeList = { 386, 422, 423, 479, 487, 492, 641, 642, 645, 646, 647 };
        string MenuSprite = "_";
        
        if (Pokemon.SpeciesID == 0)
        { return null; }
        

        MenuSprite += Convert.ToString(Pokemon.SpeciesID);


        for (int Counter = 0; Counter < FormeList.Length; Counter++)
        {
            if (FormeList[Counter] == Pokemon.SpeciesID)
            {
                MenuSprite += "_" + Convert.ToString(Pokemon.FormeByte >> 3);
                break;
            }
        }


        return (Bitmap)ResMenuSprites.ResourceManager.GetObject(MenuSprite);
    }



//Returns the Poke Ball sprite based on the given ID
    public static Image PokeBallSprite(uint PokeBallID)
    {
        if (PokeBallID == 25)
        {
            return ResItemSprites._576;
        }

        else if ((PokeBallID >= 17) & (PokeBallID <= 24))
        {
            return (Bitmap)ResItemSprites.ResourceManager.GetObject("_" + Convert.ToString(PokeBallID + 475));
        }

        else
        {
            return (Bitmap)ResItemSprites.ResourceManager.GetObject("_" + Convert.ToString(PokeBallID));
        }
    }



//Returns the Hold Item sprite based on the ID (and generation, Thanks gamefreak)
    public static Image HoldItemSprite(uint HoldItemID)
    {
        //The question mark items, genesect drives and Sweet Heart
        if ((HoldItemID >= 113) & (HoldItemID <= 134))
        {
            if ((Functions.Gen5Mode) & ((HoldItemID >= 116 & HoldItemID <= 119) || HoldItemID == 134))
            {
                return (Bitmap)ResItemSprites.ResourceManager.GetObject("_" + Convert.ToString(HoldItemID) + "_1");
            }

            return ResItemSprites._113_134;

        }
        
        //Mail
        else if ((HoldItemID >= 137) & (HoldItemID <= 148) & Functions.Gen5Mode == true)
        {
            return (Bitmap)ResItemSprites.ResourceManager.GetObject("_" + Convert.ToString(HoldItemID) + "_1");
        }

        //TMs and HMs
        else if ((HoldItemID >= 328) & (HoldItemID <= 425))
        {
            Byte[] TMTypes;

            if (Functions.Gen5Mode)
            { 
                TMTypes = new byte[] { 17, 16, 14, 14, 0, 3, 15, 1, 3, 0, 10, 17, 15, 15, 0, 14, 0, 11, 14, 0, 0, 12, 5, 13, 13, 4, 0, 4, 14, 7, 1, 0, 14, 3, 10, 3, 5, 10, 5, 2, 17, 0, 10, 14, 0, 17, 1, 0, 0, 10, 14, 1, 12, 0, 11, 17, 13, 2, 10, 17, 10, 2, 17, 0, 7, 17, 0, 0, 5, 0, 5, 13, 13, 8, 0, 6, 0, 4, 15, 5, 6, 16, 0, 3, 14, 12, 0, 2, 6, 0, 8, 14, 0, 2, 11, 0, 11, 11, 11, 0};
            }

            else
            {
                TMTypes = new byte[] { 1, 16, 11, 14, 0, 3, 15, 1, 12, 0, 13, 17, 15, 15, 0, 14, 0, 11, 12, 0, 0, 12, 8, 13, 13, 4, 0, 4, 14, 7, 1, 0, 14, 13, 10, 3, 5, 10, 5, 2, 17, 0, 0, 14, 0, 17, 8, 14, 17, 10, 2, 1, 12, 0, 11, 17, 13, 0, 16, 1, 10, 6, 17, 0, 7, 17, 0, 0, 5, 0, 5, 15, 13, 8, 0, 5, 0, 0, 17, 5, 6, 0, 0, 3, 14, 12, 0, 2, 6, 0, 8, 14, 0, 2, 11, 0, 11, 1, 11, 0 };
            }

            return (Bitmap)ResItemSprites.ResourceManager.GetObject("_TM_" + Convert.ToString(TMTypes[HoldItemID - 328]));
        }

        else
        {
            return (Bitmap)ResItemSprites.ResourceManager.GetObject("_" + Convert.ToString(HoldItemID));
        }
    }

}