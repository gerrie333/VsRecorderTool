using System;
using System.IO;
using System.Windows.Forms;

public class PokemonParser
{
    //Parses Pokemon data from a decrypted battle video.
    public static void ParseBattleVideoPokemon(int SelectedVideo)
    {
        const int PokemonOffset4 = 0x1238;
        const int PokemonOffset5 = 0xD00;
        const int PokemonDataSize = 0x70;
        const int PIDOffset = 0x0;
        const int NatureOffset = 0x4;
        const int SpeciesOffset = 0x6;
        const int HoldItemOffset = 0x8;
        const int TrainerIDOffset = 0xC;
        const int SecretIDOffset = 0xE;
        const int ExperienceOffset = 0x10;
        const int FriendshipOffset = 0x14;
        const int AbilityOffset = 0x15;
        const int EVOffset = 0x16;
        const int MovesOffset = 0x1C;
        const int PPOffset = 0x24;
        const int PPUpOffset = 0x28;
        const int IVOffset = 0x2C;
        const int FormeOffset = 0x30;
        const int NicknameOffset = 0x32;
        const int OTNameOffset = 0x48;
        const int PokeBallOffset = 0x58;
        const int NationalityOffset = 0x59;
        const int LevelOffset = 0x60;
        const int StatsOffset = 0x64;

        byte[] DecryptedData = Functions.DecryptedVideoList[SelectedVideo];
        
        int PokemonOffset = PokemonOffset4;

        if (Functions.CurrentGame >= Game.BlackWhite) { PokemonOffset = PokemonOffset5; }


        for (int CurrentPokemon = 0; CurrentPokemon < 12; CurrentPokemon++)
        {
            Functions.PokemonList[CurrentPokemon] = new BattleVideoPokemon();

            if (CurrentPokemon == 6) { PokemonOffset += 4; }
            if (BitConverter.ToUInt16(DecryptedData, 0x6 + PokemonOffset) == 0) { PokemonOffset += PokemonDataSize; continue; }


            Functions.PokemonList[CurrentPokemon].PID = BitConverter.ToUInt32(DecryptedData, PIDOffset + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].NatureByte = DecryptedData[NatureOffset + PokemonOffset];
            Functions.PokemonList[CurrentPokemon].SpeciesID = BitConverter.ToUInt16(DecryptedData, SpeciesOffset + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].HoldItemID = BitConverter.ToUInt16(DecryptedData, HoldItemOffset + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].TrainerID = BitConverter.ToUInt16(DecryptedData, TrainerIDOffset + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].SecretID = BitConverter.ToUInt16(DecryptedData, SecretIDOffset + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].Experience = BitConverter.ToUInt32(DecryptedData, ExperienceOffset + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].Friendship = DecryptedData[FriendshipOffset + PokemonOffset];
            Functions.PokemonList[CurrentPokemon].AbilityID = DecryptedData[AbilityOffset + PokemonOffset];

            Functions.PokemonList[CurrentPokemon].EVs = Functions.LoopReader(DecryptedData, EVOffset + PokemonOffset, 6);

            Functions.PokemonList[CurrentPokemon].Moves[0] = BitConverter.ToUInt16(DecryptedData, MovesOffset + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].Moves[1] = BitConverter.ToUInt16(DecryptedData, MovesOffset + 2 + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].Moves[2] = BitConverter.ToUInt16(DecryptedData, MovesOffset + 4 + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].Moves[3] = BitConverter.ToUInt16(DecryptedData, MovesOffset + 6 + PokemonOffset);

            Functions.PokemonList[CurrentPokemon].PP = Functions.LoopReader(DecryptedData, PPOffset + PokemonOffset, 4);
            Functions.PokemonList[CurrentPokemon].PPUps = Functions.LoopReader(DecryptedData, PPUpOffset + PokemonOffset, 4);

            Functions.PokemonList[CurrentPokemon].CalculateIVs(BitConverter.ToUInt32(DecryptedData, IVOffset + PokemonOffset));
            Functions.PokemonList[CurrentPokemon].FormeByte = DecryptedData[FormeOffset + PokemonOffset];
            Functions.PokemonList[CurrentPokemon].Nickname = TextParser.TextConverter(Functions.LoopReader(DecryptedData, NicknameOffset + PokemonOffset, 22));
            Functions.PokemonList[CurrentPokemon].OTName = TextParser.TextConverter(Functions.LoopReader(DecryptedData, OTNameOffset + PokemonOffset, 16));
            Functions.PokemonList[CurrentPokemon].PokeBall = DecryptedData[PokeBallOffset + PokemonOffset];
            Functions.PokemonList[CurrentPokemon].Nationality = DecryptedData[NationalityOffset + PokemonOffset];
            Functions.PokemonList[CurrentPokemon].Level = DecryptedData[LevelOffset + PokemonOffset];

            Functions.PokemonList[CurrentPokemon].Stats[0] = BitConverter.ToUInt16(DecryptedData, StatsOffset + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].Stats[1] = BitConverter.ToUInt16(DecryptedData, StatsOffset + 2 + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].Stats[2] = BitConverter.ToUInt16(DecryptedData, StatsOffset + 4 + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].Stats[3] = BitConverter.ToUInt16(DecryptedData, StatsOffset + 6 + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].Stats[4] = BitConverter.ToUInt16(DecryptedData, StatsOffset + 8 + PokemonOffset);
            Functions.PokemonList[CurrentPokemon].Stats[5] = BitConverter.ToUInt16(DecryptedData, StatsOffset + 10 + PokemonOffset);

            PokemonOffset += PokemonDataSize;
        }
    }
}