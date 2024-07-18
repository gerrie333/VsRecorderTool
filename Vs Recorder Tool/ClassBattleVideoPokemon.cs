using System;

public class BattleVideoPokemon
{
    public ushort SpeciesID = 0;
    public string Nickname = "";
    public uint PID = 0;
    public byte NatureByte = 0;
    public byte FormeByte = 0;
    public ushort HoldItemID = 0;
    public byte PokeBall = 0;
    public byte Nationality = 0;
    public byte Level = 0;
    public uint Experience = 0;
    public byte Friendship = 0;
    public uint AbilityID = 0;
    public ushort[] Stats = new ushort[6];
    public byte[] EVs = new byte[6];
    public byte[] IVs = new byte[6];
    public ushort[] Moves = new ushort[4];
    public byte[] PP = new byte[4];
    public byte[] PPUps = new byte[4];
    public uint TrainerID = 0;
    public uint SecretID = 0;
    public string OTName = "";


    public string GetSpecies()
    {
        switch (Functions.CurrentGame)
        {
            case Game.Platinum:
                if ((SpeciesID >= 0x0) && (SpeciesID <= 0x1ED)) { return Functions.SpeciesList[SpeciesID]; }
                if (SpeciesID == 0x1EE) { return "Egg"; }
                if (SpeciesID == 0x1EF) { return "Manaphy Egg"; }
                if ((SpeciesID >= 0x8000) && (SpeciesID <= 0x81ED)) { return Functions.SpeciesList[SpeciesID & 0x7FFF] + "-hybrid"; }
                if (SpeciesID == 0x81EE) { return "Egg-hybrid"; }
                if (SpeciesID == 0x81EF) { return "Manaphy Egg-hybrid"; }
                break;

            case Game.HeartgoldSoulsilver:
                if ((SpeciesID >= 0x0) && (SpeciesID <= 0x1ED)) { return Functions.SpeciesList[SpeciesID]; }
                if (SpeciesID == 0x1EE) { return "Egg"; }
                if (SpeciesID == 0x1EF) { return "Manaphy Egg"; }
                if ((SpeciesID >= 0x8000) && (SpeciesID <= 0x81ED)) { return Functions.SpeciesList[SpeciesID & 0x7FFF] + "-hybrid"; }
                if (SpeciesID == 0x81EE) { return "Egg-hybrid"; }
                if (SpeciesID == 0x81EF) { return "Manaphy Egg-hybrid"; }
                break;

            case Game.BlackWhite:
                if ((SpeciesID >= 0x0) && (SpeciesID <= 0x289)) { return Functions.SpeciesList[SpeciesID]; }
                string[] GlitchMonsBW = {"Egg", "Bad Egg", "??????????(0x28C)", "??????????(0x28D)", "??????????(0x28E)", "?????????둬(0x28F)", "??????????(0x290)", "??????????(0x291)", "??????????(0x292)", "??????????(0x293)", "???????´??(0x294)", "??????????(0x295)", "???????功??(0x296)", "?動???????견(0x297)", "???????염?量(0x298)", "????折?????(0x299)", "촙??r??????(0x29A)", "??????????(0x29B)" };
                if ((SpeciesID > 649) && (SpeciesID <= 649 + GlitchMonsBW.Length)) { return GlitchMonsBW[SpeciesID - 649 - 1]; }
                break;

            case Game.Black2White2:
                if ((SpeciesID >= 0x0) && (SpeciesID <= 0x289)) { return Functions.SpeciesList[SpeciesID]; }
                string[] GlitchMonsB2W2 = { "Egg", "Bad Egg", "UFO", "Brycen-Man", "MT", "MT2", "Transport", "Monica", "Humanoid", "Monster", "F-00", "Majin", "White Door", "Black Door", "Prop U1", "Prop U2", "Prop H1", "Prop M1", "Prop M2", "Prop T1", "Prop O1", "Prop O2", "Prop C1", "Prop C2", "Prop G1", "Prop W1", "Prop W2", "Prop R1", "Prop R2", "Prop K1", "UFO 2", "Monica", "F-00", "Black Belt", "Smeargle", "Egg(0x2AD)", "Egg(0x2AE)", "??????????(0x2AF)", "?????????둬(0x2B0)", "??????????(0x2B1)", "??????????(0x2B2)", "??????????(0x2B3)", "??????????(0x2B4)", "???????´??(0x2B5)", "??????????(0x2B6)", "???????功??(0x2B7)", "?動???????견(0x2B8)", "???????염?量(0x2B9)", "????折?????(0x2BA)", "촙??r??????(0x2BB)", "??????????(0x2BC)", "??????????(0x2BD)", "??????????(0x2BE)", "??????○???(0x2BF)", "?????????女(0x2C0)", "????????뺨?(0x2C1)", "피?????????(0x2C2)", "??????????(0x2C3) ", "?????約ケ???(0x2C4)" };
                if ((SpeciesID > 0x289) && (SpeciesID <= 0x289 + GlitchMonsB2W2.Length))  { return GlitchMonsB2W2[SpeciesID - 649 - 1]; }
                break;
        }

        return "?";
    }


    public string GetNature()
    {
        if ((Functions.CurrentGame == Game.HeartgoldSoulsilver) || (Functions.CurrentGame == Game.Platinum))
        {
            return Functions.NatureList[PID % 25];
        }

        return Functions.NatureList[NatureByte >> 3];
    }


    public string GetAbility()
    {
        if (AbilityID >= Functions.AbilityList.Length) { return "?"; }  

        return Functions.AbilityList[AbilityID];
    }


    //Calculates whether a pokemon is shiny or not based on the PID and the trainer IDs.
    public bool IsShiny()
    {
        if ((((PID >> 16) ^ TrainerID) ^ ((PID & 0xFFFF) ^ SecretID)) < 8)
        {
            return true;
        }

        return false;
    }


    //Calculates the IVs based on the number made from the 4 IV bytes.
    public void CalculateIVs(uint IVNumber)
    {
        for (int Counter = 0; Counter < 6; Counter++)
        {
            IVs[Counter] = (byte)(IVNumber & 0x1F);
            IVNumber >>= 5;
        }
    }




    public string GetHoldItem()
    {
        if (HoldItemID >= Functions.HoldItemList.Length) { return "?"; }

        return Functions.HoldItemList[HoldItemID];
    }


    public string GetMove(int MoveSlot)
    {
        if (Moves[MoveSlot & 0x3] >= Functions.MoveList.Length) { return "?"; }

        return Functions.MoveList[Moves[MoveSlot & 0x3]];
    }
}
