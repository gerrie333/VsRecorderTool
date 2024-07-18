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
        ResourceManager[] ResourceManager4 = new ResourceManager[] { ResSpritesFrontNormal4.ResourceManager, ResSpritesFrontShiny4.ResourceManager };
        ResourceManager[] ResourceManager5 = new ResourceManager[] { ResSpritesFrontNormal5.ResourceManager, ResSpritesFrontShiny5.ResourceManager };
        ResourceManager[][] ResourceManager = new ResourceManager[][] { ResourceManager4, ResourceManager5 };
        uint[] FormeList = { 201, 386, 412, 413, 422, 423, 479, 487, 492, 493, 550, 585, 586, 641, 642, 645, 646, 647 };
        string FrontSprite = "_";

        //glitch handler
        if (Pokemon.SpeciesID > 649)
        {
            switch (Functions.CurrentGame)
            {
                case Game.Black2White2:
                    string[] PokestarTable = {"UFO", "BrycenMan","UFO", "BrycenMan", "MT", "MT2", "Transport", "Monica", "Humanoid", "Monster", "F00", "Majin", "WhiteDoor", "BlackDoor", "Prop"/*U1*/, "Prop"/*U2*/, "Prop"/*H1*/, "Prop"/*M1*/, "Prop"/*M2*/, "Prop"/*T1*/, "Prop"/*O1*/, "Prop"/*O2*/, "Prop"/*C1*/, "Prop"/*C2*/, "Prop"/*G1*/, "Prop"/*W1*/, "Prop"/*W2*/, "Prop"/*R1*/, "Prop"/*R2*/, "Prop"/*K1*/, "UFO", "Monica", "F00_Broken", "BlackBelt", "Smeargle" };
                    if ((Pokemon.SpeciesID >= 650) && (Pokemon.SpeciesID <= 684)) { return (Bitmap)ResSpritesSpecial.ResourceManager.GetObject("Pokestar_" + PokestarTable[Pokemon.SpeciesID - 650]); }
                    if (Pokemon.SpeciesID == 685) { return (Bitmap)ResSpritesSpecial.ResourceManager.GetObject("front_egg5"); }
                    if (Pokemon.SpeciesID == 686) { return (Bitmap)ResSpritesSpecial.ResourceManager.GetObject("front_egg5_manaphy"); }
                    if ((Pokemon.SpeciesID >= 687) && (Pokemon.SpeciesID <= 708)) { return (Bitmap)ResSpritesFrontNormal5.ResourceManager.GetObject("_201_" + (Pokemon.SpeciesID - 686).ToString()); }
                    return (Bitmap)null;
            }
        }


        //evil number construction = (gen) << 1 | IsShiny
        //int AssetIndex = ( ( (Functions.CurrentGame == Game.Black2White2) || (Functions.CurrentGame == Game.BlackWhite) ) << 1) | Pokemon.IsShiny();
        //this code still sucks, I really need to add something to organize games per generation
        //I hate resx files

        FrontSprite += Convert.ToString(Pokemon.SpeciesID);

        for (int Counter = 0; Counter < FormeList.Length; Counter++)
        {
            if (Pokemon.SpeciesID == FormeList[Counter])
            {
                FrontSprite += "_" + Convert.ToString(Pokemon.FormeByte >> 3);
                break;
            }
        }

        return (Bitmap)ResourceManager[Convert.ToInt16(Functions.CurrentGame >= Game.BlackWhite)][Convert.ToInt16(Pokemon.IsShiny())].GetObject(FrontSprite);
    }



//Returns the menu sprite based on the species and forme.
    public static Image MenuSprite(BattleVideoPokemon Pokemon)
    {
        uint[] FormeList = { 201, 386, 412, 413, 422, 423, 479, 487, 492, /*493,*/ 550, 585, 586, 641, 642, 645, 646, 647 };
        string MenuSprite = "_";
        
        if (Pokemon.SpeciesID == 0) { return null; }
        
        if (Pokemon.SpeciesID > 649)
        {
            switch (Functions.CurrentGame)
            {
                case Game.BlackWhite:
                    if (Pokemon.SpeciesID == 650) { return (Bitmap)ResSpritesSpecial.ResourceManager.GetObject("menu_egg"); }
                    if (Pokemon.SpeciesID == 651) { return (Bitmap)ResSpritesSpecial.ResourceManager.GetObject("menu_egg_manaphy"); }
                    if ((Pokemon.SpeciesID >= 652) && (Pokemon.SpeciesID <= 667)){ return (Bitmap)ResSpritesMenu.ResourceManager.GetObject("_201_" + (Pokemon.SpeciesID - 651).ToString()); }
                    return (Bitmap)ResSpritesMenu.ResourceManager.GetObject("_0");

                case Game.Black2White2:
                    if (Pokemon.SpeciesID == 683) { return (Bitmap)ResSpritesSpecial.ResourceManager.GetObject("menu_egg"); }
                    if (Pokemon.SpeciesID == 684) { return (Bitmap)ResSpritesSpecial.ResourceManager.GetObject("menu_egg_manaphy"); }
                    if ((Pokemon.SpeciesID >= 685) && (Pokemon.SpeciesID <= 708)) { return (Bitmap)ResSpritesMenu.ResourceManager.GetObject("_201_" + (Pokemon.SpeciesID - 684).ToString()); }
                    return (Bitmap)ResSpritesMenu.ResourceManager.GetObject("_0");
            }
        }

        MenuSprite += Convert.ToString(Pokemon.SpeciesID);


        for (int Counter = 0; Counter < FormeList.Length; Counter++)
        {
            if (FormeList[Counter] == Pokemon.SpeciesID)
            {
                MenuSprite += "_" + Convert.ToString(Pokemon.FormeByte >> 3);
                break;
            }
        }


        return (Bitmap)ResSpritesMenu.ResourceManager.GetObject(MenuSprite);
    }



//Returns the Poke Ball sprite based on the given ID
    public static Image PokeBallSprite(byte PokeBallID)
    {
        if ((PokeBallID >= 1) && (PokeBallID <= 16)){ return (Bitmap)ResSpritesItems.ResourceManager.GetObject("_" + PokeBallID.ToString()); }
        if ((PokeBallID >= 17) && (PokeBallID <= 24)) { return (Bitmap)ResSpritesItems.ResourceManager.GetObject("_" + (PokeBallID + 475).ToString()); }
        if (PokeBallID == 25) { return (Bitmap)ResSpritesItems.ResourceManager.GetObject("_576"); }

        return (Bitmap)ResSpritesItems.ResourceManager.GetObject("_4");
    }



//Returns the Hold Item sprite based on the ID (and generation, Thanks gamefreak)
    public static Image HoldItemSprite(uint HoldItemID)
    {
        //The question mark items, genesect drives and Sweet Heart
        if ((HoldItemID >= 113) & (HoldItemID <= 134))
        {
            if ((Functions.CurrentGame >= Game.BlackWhite) & ((HoldItemID >= 116 & HoldItemID <= 119) || HoldItemID == 134))
            {
                return (Bitmap)ResSpritesItems.ResourceManager.GetObject("_" + Convert.ToString(HoldItemID) + "_1");
            }

            return ResSpritesItems._113_134;

        }
        
        //Mail
        else if ((HoldItemID >= 137) & (HoldItemID <= 148) & (Functions.CurrentGame >= Game.BlackWhite))
        {
            return (Bitmap)ResSpritesItems.ResourceManager.GetObject("_" + Convert.ToString(HoldItemID) + "_1");
        }

        //TMs and HMs
        else if ((HoldItemID >= 328) & (HoldItemID <= 425))
        {
            Byte[] TMTypes;

            if (Functions.CurrentGame >= Game.BlackWhite)
            { 
                TMTypes = new byte[] { 17, 16, 14, 14, 0, 3, 15, 1, 3, 0, 10, 17, 15, 15, 0, 14, 0, 11, 14, 0, 0, 12, 5, 13, 13, 4, 0, 4, 14, 7, 1, 0, 14, 3, 10, 3, 5, 10, 5, 2, 17, 0, 10, 14, 0, 17, 1, 0, 0, 10, 14, 1, 12, 0, 11, 17, 13, 2, 10, 17, 10, 2, 17, 0, 7, 17, 0, 0, 5, 0, 5, 13, 13, 8, 0, 6, 0, 4, 15, 5, 6, 16, 0, 3, 14, 12, 0, 2, 6, 0, 8, 14, 0, 2, 11, 0, 11, 11, 11, 0};
            }

            else
            {
                TMTypes = new byte[] { 1, 16, 11, 14, 0, 3, 15, 1, 12, 0, 13, 17, 15, 15, 0, 14, 0, 11, 12, 0, 0, 12, 8, 13, 13, 4, 0, 4, 14, 7, 1, 0, 14, 13, 10, 3, 5, 10, 5, 2, 17, 0, 0, 14, 0, 17, 8, 14, 17, 10, 2, 1, 12, 0, 11, 17, 13, 0, 16, 1, 10, 6, 17, 0, 7, 17, 0, 0, 5, 0, 5, 15, 13, 8, 0, 5, 0, 0, 17, 5, 6, 0, 0, 3, 14, 12, 0, 2, 6, 0, 8, 14, 0, 2, 11, 0, 11, 1, 11, 0 };
            }

            return (Bitmap)ResSpritesItems.ResourceManager.GetObject("_TM_" + Convert.ToString(TMTypes[HoldItemID - 328]));
        }

        else
        {
            return (Bitmap)ResSpritesItems.ResourceManager.GetObject("_" + Convert.ToString(HoldItemID));
        }
    }

}