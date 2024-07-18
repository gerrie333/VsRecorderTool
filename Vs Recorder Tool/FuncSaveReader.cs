using System;
using System.CodeDom;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Vs_Recorder_Tool;

public static class SaveReader
{
    //Gen 4 Save Reader
    //Empty slots get marked with null
    public static void ReadFromSaveFile4()
    {
        int SaveCounterOffset = 0;
        int TableOffset = 0;
        int VideoOffset = 0;
        int BlockOffset = 0;
        uint[] VideoIDTable = { 0, 0, 0, 0 };
        byte[] Buffer = new byte[16];
        uint BlockASaveNumber, BlockBSaveNumber;

        Stream SF = File.OpenRead(Functions.CurrentFilePath);

        switch (Functions.CurrentGame)
        {
            //Platinum
            case Game.Platinum:
                SaveCounterOffset = 0xCF1C;
                TableOffset = 0x2824;
                VideoOffset = 0x24000;
                BlockOffset = 0x40000;
                break;


            //HeartGold / SoulSilver
            case Game.HeartgoldSoulsilver:
                SaveCounterOffset = 0xCF1C;
                TableOffset = 0x2310;
                VideoOffset = 0x27000;
                BlockOffset = 0x40000;
                break;

        }


            
            
            //Gets the number of saves from both small blocks
            SF.Position = SaveCounterOffset;
            SF.Read(Buffer, 0, 4);
            BlockASaveNumber = BitConverter.ToUInt32(Buffer, 0);
            SF.Position = SaveCounterOffset + BlockOffset;
            SF.Read(Buffer, 0, 4);
            BlockBSaveNumber = BitConverter.ToUInt32(Buffer, 0);


            //Decides which small block to read the list from
            SF.Position = TableOffset;
            if (BlockBSaveNumber > BlockASaveNumber)
            {
                SF.Position += BlockOffset;
            }

            SF.Read(Buffer, 0, 16);

            for (int Counter = 0; Counter < 4; Counter++)
            {
                VideoIDTable[Counter] = BitConverter.ToUInt32(Buffer, 4 * Counter);

            }


            //Reads the encrypted battle video data from the save file 
            SF.Position = VideoOffset;


            for (int Counter = 0; Counter < 4; Counter++)
            {
                if (VideoIDTable[Counter] == 0xFFFFFFFF)
                {
                    Functions.EncryptedVideoList[Counter] = null;
                    SF.Position = VideoOffset + 0x2000 * (Counter + 1);
                    continue;
                }



                Functions.EncryptedVideoList[Counter] = new byte[0x1D5A];


                SF.Read(Buffer, 0, 4);

                if (BitConverter.ToUInt32(Buffer, 0) == VideoIDTable[Counter])
                {
                    SF.Read(Functions.EncryptedVideoList[Counter], 0, 0x1D5A);
                }

                else
                {
                    SF.Position += 0x40000;
                    SF.Read(Functions.EncryptedVideoList[Counter], 0, 0x1D5A);
                    SF.Position -= 0x40000;
                }



                SF.Position = VideoOffset + 0x2000 * (Counter + 1);
            }
            SF.Close();
     }


    //Gen 5 Save Reader
    //Empty slots get marked with null
    public static void ReadFromSaveFile5()
    {
        int VideoOffset = 0;

        Stream SF = File.OpenRead(Functions.CurrentFilePath);

        switch (Functions.CurrentGame)
        {
            case Game.BlackWhite:
                VideoOffset = 0x4A600;
                break;

            //Black 2 / White 2
            case Game.Black2White2:
                VideoOffset = 0x4C000;
                break;
        }


        SF.Position = VideoOffset;

        for (int Counter = 0; Counter < 4; Counter++)
        {

            Functions.EncryptedVideoList[Counter] = new byte[Functions.BattleVideoLength5];

            SF.Read(Functions.EncryptedVideoList[Counter], 0, Functions.BattleVideoLength5);

            if ((BitConverter.ToUInt16(Functions.EncryptedVideoList[Counter], 0x80) == 0x0) || (BitConverter.ToUInt16(Functions.EncryptedVideoList[Counter], 0x80) == 0xFFFF))
            {
                Functions.EncryptedVideoList[Counter] = null;
            }
        }

        SF.Close();
    }

    public static void ReadBattleVideoFile(ref byte[] BattleVideoArray)
    {
        string FileExtension = Functions.CurrentFilePath.Split('.').Last();
        int BattleVideoLength = Functions.BattleVideoLength4;
        if ((Functions.CurrentGame == Game.BlackWhite) || Functions.CurrentGame == Game.Black2White2) { BattleVideoLength = Functions.BattleVideoLength5; }
        

        Stream BattleVideoFile = File.OpenRead(Functions.CurrentFilePath);
        BattleVideoArray = new byte[BattleVideoLength];

        if (FileExtension == "ebv") { BattleVideoLength = 0x18A4; }
        BattleVideoFile.Read(BattleVideoArray, 0, BattleVideoLength);



        for (int Counter = 1; Counter < Functions.EncryptedVideoList.Length; Counter++)
        {
            Functions.EncryptedVideoList[Counter] = null;
        }
    }


    //Decrypts Battle Videos
    public static void DecryptBattleVideos(int VideoSlotBitmask)
    {
        uint Magic1 = 0x41C64E6D;
        uint Magic2 = 0x6073;
        uint Key = 0;
        uint CurrentSeed = 0;
        int StartIndex = 0xE4;
        int Length = 0x1C64;
        int KeyOffset = 0x1D48;

        if ((Functions.CurrentGame == Game.BlackWhite) || (Functions.CurrentGame == Game.Black2White2))
        {
            StartIndex = 0xC4;
            Length = 0x17DC;
            KeyOffset = 0x18A0;
        }


        int Index = StartIndex;

        for (int CurrentVideo = 0; CurrentVideo < 4; CurrentVideo++)
        {
            //Empty slot check
            if (Functions.EncryptedVideoList[CurrentVideo] == null) { Functions.DecryptedVideoList[CurrentVideo] = null; continue; }

            //video slot selected in Bitmask check
            if (((VideoSlotBitmask >> CurrentVideo) & 0x1) == 0)    { continue; }

            Functions.DecryptedVideoList[CurrentVideo] = Functions.EncryptedVideoList[CurrentVideo].ToArray();

            Key = BitConverter.ToUInt16(Functions.EncryptedVideoList[CurrentVideo], KeyOffset);
            CurrentSeed = ((Key ^ 0xFFFF) << 16) + Key;

            for (int Counter = 0; Counter < Length; Counter += 2)
            {


                CurrentSeed = (Magic1 * CurrentSeed) + Magic2;
                Functions.DecryptedVideoList[CurrentVideo][Index] ^= (byte)((CurrentSeed >> 16) & 0xFF);
                Functions.DecryptedVideoList[CurrentVideo][Index + 1] ^= (byte)(CurrentSeed >> 24);


                Index += 2;
            }

            Index = StartIndex;

        }
    }

    //Legacy decryption goes as follows:
    //Read the ENCRYPTED bytes at range 0xCC-0xD5
    //Treat these bytes as pairs of two and XOR them with the magic constants a1-5 (the decrypted bytes at that range are the magic constants)
    //Find the seed that starts at 0x(a1xor)0000 which generates 4 consecutive values that have the other results in their upper bytes
    //Use the found seed as the seed for the 2nd rng algorithm and use the 5th result as the decryption seed
    //
    //Kaphotics' battle video manager uses this process to decrypt the videos.
    //However, you can just use the bytes at 0x18A0-1 to reconstruct the decryption seed
    //I accidentally forgot that legacy EBVs do actually include these two bytes even though they don't include the rest of the footer
    //This function still includes the legacy decryption code but doesn't actually do anything with the end result
    //It just adds back the footer which is like 90% 0xFF anyways
    public static void ExtendLegacyEBV(int SelectedVideo)
    {
        //legacy decryption
        uint a1 = BitConverter.ToUInt16(Functions.EncryptedVideoList[SelectedVideo], 0xCC) ^ (uint)0x8965;
        uint a2 = BitConverter.ToUInt16(Functions.EncryptedVideoList[SelectedVideo], 0xCE) ^ (uint)0x6C07;
        uint a3 = BitConverter.ToUInt16(Functions.EncryptedVideoList[SelectedVideo], 0xD0) ^ (uint)0x8B65;
        uint a4 = BitConverter.ToUInt16(Functions.EncryptedVideoList[SelectedVideo], 0xD2) ^ (uint)0x5D58;
        uint a5 = BitConverter.ToUInt16(Functions.EncryptedVideoList[SelectedVideo], 0xD4) ^ (uint)0x9EC3;

        uint Seed = 0 ;
        uint RandomCall;

        for (uint Counter = 0; Counter <= 0xFFFF; Counter++)
        {
            Seed = (a1 << 16) + Counter;

            RandomCall = (0x41C64E6D * Seed) + 0x6073;
            if ((RandomCall >> 16) != a2) { continue; }

            RandomCall = (0x41C64E6D * RandomCall) + 0x6073;
            if ((RandomCall >> 16) != a3) { continue; }

            RandomCall = (0x41C64E6D * RandomCall) + 0x6073;
            if ((RandomCall >> 16) != a4) { continue; }

            RandomCall = (0x41C64E6D * RandomCall) + 0x6073;
            if ((RandomCall >> 16) != a5) { continue; }

            break;
        }


        RandomCall = (0xEEB9EB65 * Seed) + 0x0A3561A1;
        for (int Counter = 0; Counter < 4; Counter++)
        {
            RandomCall = (0xEEB9EB65 * RandomCall) + 0x0A3561A1;
        }


        //restoring footer
        byte ByteToWrite;
        for (int Counter = 0x18A2; Counter <= 0x19FF; Counter++)
        {
            switch (Counter)
            {
                case 0x18A0: ByteToWrite = (byte)((RandomCall >> 16) & 0xFF); break;
                case 0x18A1: ByteToWrite = (byte)(RandomCall >> 24); break;
                case 0x18A2: ByteToWrite = 0x0; break;
                case 0x18A3: ByteToWrite = 0x0; break;
                case 0x18A4: ByteToWrite = 0x31; break;
                case 0x18A5: ByteToWrite = 0x0; break;

                case 0x18A6: ByteToWrite = 0xB0; break;
                case 0x18A7: ByteToWrite = 0x0B; break;
                case 0x1900: ByteToWrite = 0xB0; break;
                case 0x1901: ByteToWrite = 0x0B; break;

                case 0x1902: ByteToWrite = 0x0; break;
                case 0x1903: ByteToWrite = 0x0; break;
                case 0x1904: ByteToWrite = 0x1; break;
                case 0x1905: ByteToWrite = 0x0; break;
                case 0x1906: ByteToWrite = 0x0; break;
                case 0x1907: ByteToWrite = 0x0; break;
                case 0x1908: ByteToWrite = 0x14; break;
                case 0x1909: ByteToWrite = 0x19; break;
                case 0x190A: ByteToWrite = 0x0; break;
                case 0x190B: ByteToWrite = 0x0; break;
                case 0x190C: ByteToWrite = 0x27; break;
                case 0x190D: ByteToWrite = 0x35; break;
                case 0x190E: ByteToWrite = 0x5; break;
                case 0x190F: ByteToWrite = 0x31; break;
                case 0x1910: ByteToWrite = 0x0; break;
                case 0x1911: ByteToWrite = 0x0; break;

                case 0x1912: ByteToWrite = 0x12; break;
                case 0x1913: ByteToWrite = 0x34; break;

                default: ByteToWrite = 0xFF; break;               
            }

            Functions.EncryptedVideoList[SelectedVideo][Counter] = ByteToWrite;
        }

    }



}