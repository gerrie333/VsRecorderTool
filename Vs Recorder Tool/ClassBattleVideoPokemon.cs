using System;

public class BattleVideoPokemon
{
    public uint SpeciesID = 0;
    public string Species = "";
    public string Nickname = "";
    public uint PIDint = 0;
    public string PIDBytes = "00000000";
    public byte NatureByte = 0;
    public string Nature = "Hardy";
    public byte FormeByte = 0;
    public string Forme = "Normal";
    public bool Shiny = false;
    public uint HoldItemID = 0;
    public string HoldItem = "None";
    public uint PokeBall = 0;
    public uint Nationality = 0;
    public string Level = "0";
    public string Experience = "0";
    public string Friendship = "0";
    public uint AbilityID = 0;
    public string Ability = "-";
    public string[] Stats = new string[6];
    public string[] EVs = new string[6];
    public string[] IVs = new string[6];
    public string[] Moves = new string[4];
    public string[] PP = new string[4];
    public string[] PPUps = new string[4];
    public uint TrainerID = 0;
    public uint SecretID = 0;
    public string OTName = "";

    //Calculates whether a pokemon is shiny or not based on the PID and the trainer IDs.
    public bool IsShiny()
    {
        if ((((PIDint >> 16) ^ TrainerID) ^ ((PIDint & 0xFFFF) ^ SecretID)) < 8)
        {
            Shiny = true;

            return true;
        }
        Shiny = false;

        return false;
    }

    public void GetNature()
    {
        byte NatureID = 0;

        if (Functions.Gen5Mode == true)
        {
            NatureID = (byte)(NatureByte >> 3);
        }

        else
        {
            NatureID = (byte)(PIDint % 25);
        }

        Nature = Functions.NatureList[NatureID];
    }


    //Calculates the IVs based on the number made from the 4 IV bytes.
    public void CalculateIVs(uint IVNumber)
    {
        int Counter = 0;

        while (Counter < 6)
        {
            IVs[Counter] = Convert.ToString(IVNumber & 0x1F);
            IVNumber >>= 5;

            Counter++;
        }

    }
}
