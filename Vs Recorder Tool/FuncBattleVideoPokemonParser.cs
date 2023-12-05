using System;
using System.IO;
using System.Windows.Forms;

public class BattleVideoPokemonParser
{
    //Parses Pokemon data from a decrypted battle video.
    public static void OpenBattleVideoPokemonFile(int SelectedVideo)
    {
        int Offset = 0x1238;
        byte[] RawData = Functions.DecryptedVideoList[SelectedVideo];

        

        if (Functions.Gen5Mode == true)
        { Offset = 0xD00; }


        for (int CurrentPokemon = 0; CurrentPokemon < 12; CurrentPokemon++)
        {

            Functions.PokemonList[CurrentPokemon] = new BattleVideoPokemon();


            if (CurrentPokemon == 6)
            { Offset += 4; }


            if (BitConverter.ToUInt16(RawData, 0x6 + Offset) == 0)
            { Offset += 0x70; continue; }


            Functions.PokemonList[CurrentPokemon].PIDint = BitConverter.ToUInt32(RawData, 0x0 + Offset);
            Functions.PokemonList[CurrentPokemon].PIDBytes = BitConverter.ToString(Functions.LoopReader(RawData, 0x0 + Offset, 4));
            Functions.PokemonList[CurrentPokemon].NatureByte = RawData[0x4 + Offset];
            Functions.PokemonList[CurrentPokemon].GetNature();
            Functions.PokemonList[CurrentPokemon].SpeciesID = BitConverter.ToUInt16(RawData, 0x6 + Offset);
            Functions.PokemonList[CurrentPokemon].Species = Functions.SpeciesList[BitConverter.ToUInt16(RawData, 0x6 + Offset)];
            Functions.PokemonList[CurrentPokemon].HoldItemID = BitConverter.ToUInt16(RawData, 0x8 + Offset);
            Functions.PokemonList[CurrentPokemon].HoldItem = Functions.HoldItemList[BitConverter.ToUInt16(RawData, 0x8 + Offset)];
            Functions.PokemonList[CurrentPokemon].TrainerID = BitConverter.ToUInt16(RawData, 0xC + Offset);
            Functions.PokemonList[CurrentPokemon].SecretID = BitConverter.ToUInt16(RawData, 0xE + Offset);
            Functions.PokemonList[CurrentPokemon].IsShiny();
            Functions.PokemonList[CurrentPokemon].Experience = Convert.ToString(BitConverter.ToUInt32(RawData, 0x10 + Offset));
            Functions.PokemonList[CurrentPokemon].Friendship = Convert.ToString(RawData[0x14 + Offset]);
            Functions.PokemonList[CurrentPokemon].AbilityID = RawData[0x15 + Offset];
            Functions.PokemonList[CurrentPokemon].Ability = Functions.AbilityList[RawData[0x15 + Offset]];
            Functions.PokemonList[CurrentPokemon].EVs[0] = Convert.ToString(RawData[0x16 + Offset]);
            Functions.PokemonList[CurrentPokemon].EVs[1] = Convert.ToString(RawData[0x17 + Offset]);
            Functions.PokemonList[CurrentPokemon].EVs[2] = Convert.ToString(RawData[0x18 + Offset]);
            Functions.PokemonList[CurrentPokemon].EVs[3] = Convert.ToString(RawData[0x19 + Offset]);
            Functions.PokemonList[CurrentPokemon].EVs[4] = Convert.ToString(RawData[0x1A + Offset]);
            Functions.PokemonList[CurrentPokemon].EVs[5] = Convert.ToString(RawData[0x1B + Offset]);
            Functions.PokemonList[CurrentPokemon].Moves[0] = Functions.MoveList[BitConverter.ToUInt16(RawData, 0x1C + Offset)];
            Functions.PokemonList[CurrentPokemon].Moves[1] = Functions.MoveList[BitConverter.ToUInt16(RawData, 0x1E + Offset)];
            Functions.PokemonList[CurrentPokemon].Moves[2] = Functions.MoveList[BitConverter.ToUInt16(RawData, 0x20 + Offset)];
            Functions.PokemonList[CurrentPokemon].Moves[3] = Functions.MoveList[BitConverter.ToUInt16(RawData, 0x22 + Offset)];
            Functions.PokemonList[CurrentPokemon].PP[0] = Convert.ToString(RawData[0x24 + Offset]);
            Functions.PokemonList[CurrentPokemon].PP[1] = Convert.ToString(RawData[0x25 + Offset]);
            Functions.PokemonList[CurrentPokemon].PP[2] = Convert.ToString(RawData[0x26 + Offset]);
            Functions.PokemonList[CurrentPokemon].PP[3] = Convert.ToString(RawData[0x27 + Offset]);
            Functions.PokemonList[CurrentPokemon].PPUps[0] = Convert.ToString(RawData[0x28 + Offset]);
            Functions.PokemonList[CurrentPokemon].PPUps[1] = Convert.ToString(RawData[0x29 + Offset]);
            Functions.PokemonList[CurrentPokemon].PPUps[2] = Convert.ToString(RawData[0x2A + Offset]);
            Functions.PokemonList[CurrentPokemon].PPUps[3] = Convert.ToString(RawData[0x2B + Offset]);
            Functions.PokemonList[CurrentPokemon].CalculateIVs(BitConverter.ToUInt32(RawData, 0x2C + Offset));
            Functions.PokemonList[CurrentPokemon].FormeByte = RawData[0x30 + Offset];
            Functions.PokemonList[CurrentPokemon].Nickname = TextParser.TextConverter(Functions.LoopReader(RawData, 0x32 + Offset, 22));
            Functions.PokemonList[CurrentPokemon].OTName = TextParser.TextConverter(Functions.LoopReader(RawData, 0x48 + Offset, 16));
            Functions.PokemonList[CurrentPokemon].PokeBall = RawData[0x58 + Offset];
            Functions.PokemonList[CurrentPokemon].Nationality = RawData[0x59 + Offset];
            Functions.PokemonList[CurrentPokemon].Level = Convert.ToString(RawData[0x60 + Offset]);
            Functions.PokemonList[CurrentPokemon].Stats[0] = Convert.ToString(BitConverter.ToUInt16(RawData, 0x64 + Offset));
            Functions.PokemonList[CurrentPokemon].Stats[1] = Convert.ToString(BitConverter.ToUInt16(RawData, 0x66 + Offset));
            Functions.PokemonList[CurrentPokemon].Stats[2] = Convert.ToString(BitConverter.ToUInt16(RawData, 0x68 + Offset));
            Functions.PokemonList[CurrentPokemon].Stats[3] = Convert.ToString(BitConverter.ToUInt16(RawData, 0x6A + Offset));
            Functions.PokemonList[CurrentPokemon].Stats[4] = Convert.ToString(BitConverter.ToUInt16(RawData, 0x6C + Offset));
            Functions.PokemonList[CurrentPokemon].Stats[5] = Convert.ToString(BitConverter.ToUInt16(RawData, 0x6E + Offset));

            Offset += 0x70;
        }
    }
}