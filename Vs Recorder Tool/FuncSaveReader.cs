using System;
using System.CodeDom;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Vs_Recorder_Tool;

public static class SaveReader
{
    
    public static void OpenUserFile(RadioButton[] VideoButtons, PictureBox[] BattlePartySlots)
    {
        string[] SeparatedFileName;
        //Step 1: Get file location and check the file's type
        OpenFileDialog BattleVideoFile = new OpenFileDialog
        {
            Filter = "Save File (*.sav)|*.sav|Gen 4 Encrypted Battle Video (*.ebv4)|*.ebv4|Gen 4 Decrypted Battle Video (*.dbv4)|*.dbv4|Gen 5 Encrypted Battle Video (*.ebv5)|*.ebv5|Gen 5 Decrypted Battle Video (*.dbv5)|*.dbv5|All files (*.*)|*.*"
        };

        BattleVideoFile.ShowDialog();

        if (BattleVideoFile.FileName == "") { return; }

        SeparatedFileName = BattleVideoFile.FileName.Split('.');


        switch (SeparatedFileName[SeparatedFileName.Length - 1])
        {
            //Save file -- Ask which game, track down ebv data and read it, decrypt it, view it
            case "sav":
                FormGameSelect PopUp = new FormGameSelect();
                PopUp.ShowDialog();

                ReadFromSaveFile(VideoButtons, BattleVideoFile);
                DecryptBattleVideos();
                break;

            //EBV4 -- decrypt it, view it
            case "ebv4":
                Functions.Gen5Mode = false;
                VideoButtons[0].Checked = false;
                VideoButtons[0].Enabled = true;
                VideoButtons[1].Enabled = false;
                VideoButtons[2].Enabled = false;
                VideoButtons[3].Enabled = false;
                Stream EncryptedVideoFile4 = BattleVideoFile.OpenFile();
                Functions.EncryptedVideoList[0] = new byte[0x1D5A];
                Functions.DecryptedVideoList[0] = new byte[0x1D5A];
                EncryptedVideoFile4.Read(Functions.EncryptedVideoList[0], 0, 0x1D5A);
                DecryptBattleVideos();
                VideoButtons[0].Checked = true;
                break;

            //EBV5
            case "ebv5":
                Functions.Gen5Mode = true;
                VideoButtons[0].Checked = false;
                VideoButtons[0].Enabled = true;
                VideoButtons[1].Enabled = false;
                VideoButtons[2].Enabled = false;
                VideoButtons[3].Enabled = false;
                Stream EncryptedVideoFile5 = BattleVideoFile.OpenFile();
                Functions.EncryptedVideoList[0] = new byte[0x1A00];
                Functions.DecryptedVideoList[0] = new byte[0x1A00];
                EncryptedVideoFile5.Read(Functions.EncryptedVideoList[0], 0, 0x1A00);
                DecryptBattleVideos();
                VideoButtons[0].Checked = true;
                break;


            //DBV4 -- view it
            case "dbv4":
                Functions.Gen5Mode = false;
                VideoButtons[0].Checked = false;
                VideoButtons[0].Enabled = true;
                VideoButtons[1].Enabled = false;
                VideoButtons[2].Enabled = false;
                VideoButtons[3].Enabled = false;
                Stream DecryptedVideoFile4 = BattleVideoFile.OpenFile();
                Functions.DecryptedVideoList[0] = new byte[0x1D5A];
                DecryptedVideoFile4.Read(Functions.DecryptedVideoList[0], 0, 0x1D5A);
                VideoButtons[0].Checked = true;
                break;

            //DBV5 -- view it
            case "dbv5":
                Functions.Gen5Mode = true;
                VideoButtons[0].Checked = false;
                VideoButtons[0].Enabled = true;
                VideoButtons[1].Enabled = false;
                VideoButtons[2].Enabled = false;
                VideoButtons[3].Enabled = false;
                Stream DecryptedVideoFile5 = BattleVideoFile.OpenFile();
                Functions.DecryptedVideoList[0] = new byte[0x1A00];
                DecryptedVideoFile5.Read(Functions.DecryptedVideoList[0], 0, 0x1A00);
                VideoButtons[0].Checked = true;
                break;
        }
    }



    public static void ReadFromSaveFile(RadioButton[] VideoButtons, OpenFileDialog SaveFile)
    {
        int SaveCounterOffset = 0;
        int TableOffset = 0;
        int VideoOffset = 0;
        int BlockOffset = 0;

        Stream SF = SaveFile.OpenFile();

        switch (Functions.CurrentGame)
        {
            //Platinum
            case 0:
                SaveCounterOffset = 0xCF1C;
                TableOffset = 0x2824;
                VideoOffset = 0x24000;
                BlockOffset = 0x40000;
                Functions.Gen5Mode = false;
                break;


            //HeartGold / SoulSilver
            case 1:
                SaveCounterOffset = 0xCF1C;
                TableOffset = 0x2310;
                VideoOffset = 0x27000;
                BlockOffset = 0x40000;
                Functions.Gen5Mode = false;
                break;


            //Black / White
            case 2:
                VideoOffset = 0x4A600;
                Functions.Gen5Mode = true;
                break;


            //Black 2 / White 2
            case 3:
                VideoOffset = 0x4C000;
                Functions.Gen5Mode = true;
                break;
        }

        //Step 2: Read the battle videos from the save file and store them in the encrypted buffer.
        //Gen 5 Logic
        if (Functions.Gen5Mode)
        {
            SF.Position = VideoOffset;

            for (int Counter = 0; Counter < 4; Counter++)
            {
                VideoButtons[Counter].Checked = false;
                VideoButtons[Counter].Enabled = false;

                Functions.EncryptedVideoList[Counter] = new byte[0x1A00];

                SF.Read(Functions.EncryptedVideoList[Counter], 0, 0x1A00);

                if ((BitConverter.ToUInt16(Functions.EncryptedVideoList[Counter], 0x80) != 0x0) & (BitConverter.ToUInt16(Functions.EncryptedVideoList[Counter], 0x80) != 0xFFFF))
                {
                    VideoButtons[Counter].Enabled = true;
                }
            }
        }


        //Gen 4 Logic
        else
        {
            uint[] VideoIDTable = { 0, 0, 0, 0 };
            byte[] Buffer = new byte[16];
            uint BlockASaveNumber, BlockBSaveNumber;

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
                Functions.EncryptedVideoList[Counter] = new byte[0x1D5A];

                VideoButtons[Counter].Checked = false;
                VideoButtons[Counter].Enabled = false;

                if (VideoIDTable[Counter] == 0xFFFFFFFF)
                { SF.Position = VideoOffset + 0x2000 * (Counter + 1); continue; }


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



                VideoButtons[Counter].Enabled = true;
                SF.Position = VideoOffset + 0x2000 * (Counter + 1);
            }
        }

        SF.Close();
    }



    //Decrypts Battle Videos
    public static void DecryptBattleVideos()
    {
        uint Magic1 = 0x41C64E6D;
        uint Magic2 = 0x6073;
        uint Key = 0;
        uint CurrentSeed = 0;
        int StartIndex = 0xE4;
        int Length = 0x1C64;
        int KeyOffset = 0x1D48;

        if (Functions.Gen5Mode)
        {
            StartIndex = 0xC4;
            Length = 0x17DC;
            KeyOffset = 0x18A0;
        }


        int Index = StartIndex;

        for (int CurrentVideo = 0; CurrentVideo < 4; CurrentVideo++)
        {
            if (Functions.EncryptedVideoList[CurrentVideo] == null || (BitConverter.ToUInt16(Functions.EncryptedVideoList[CurrentVideo], 0x80) == 0x0) || (BitConverter.ToUInt16(Functions.EncryptedVideoList[CurrentVideo], 0x80) == 0xFFFF))
            { continue; }

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



    public static void ExportBattleVideo(int BattleVideo)
    {
        int VideoLength = 0x1D5A;
        byte[] ChosenVideo = Functions.EncryptedVideoList[BattleVideo];
        string Filter = "Encrypted Battle Video (*.ebv4)|*.ebv4|Decrypted Battle Video (*.dbv4)|*.dbv4";

        if (Functions.Gen5Mode == true)
        {
            VideoLength = 0x1A00;
            Filter = "Encrypted Battle Video (*.ebv5)|*.ebv5|Decrypted Battle Video (*.dbv5)|*.dbv5";
        }

        SaveFileDialog BattleVideoExport = new SaveFileDialog
        {
            Filter = Filter
        };

        BattleVideoExport.ShowDialog();


        if (BattleVideoExport.FileName == "") { return; }


        if (BattleVideoExport.FilterIndex == 2)
        {
            ChosenVideo = Functions.DecryptedVideoList[BattleVideo];
        }

        Stream Gamer = BattleVideoExport.OpenFile();


        Gamer.Write(ChosenVideo, 0, VideoLength);
        Gamer.Close();
    }
}