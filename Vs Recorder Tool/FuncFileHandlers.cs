using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vs_Recorder_Tool;

public static class FileHandlers
{

    public static void OpenUserFile()
    {
        //Step 1: Get file location and check the file's type
        OpenFileDialog BattleVideoFile = new OpenFileDialog
        {
            Filter = "Save File (*.sav)|*.sav|Gen 4 Encrypted Battle Video (*.ebv4)|*.ebv4|Gen 4 Decrypted Battle Video (*.dbv4)|*.dbv4|Gen 5 Encrypted Battle Video (*.ebv5)|*.ebv5|Gen 5 Decrypted Battle Video (*.dbv5)|*.dbv5|All files (*.*)|*.*"
        };

        BattleVideoFile.ShowDialog();

        if (BattleVideoFile.FileName == "") { return; }

        Functions.CurrentFilePath = BattleVideoFile.FileName;

        string FileExtension = Functions.CurrentFilePath.Split('.').Last();
        if (FileExtension == "sav")
        {
            FormGameSelect PopUp = new FormGameSelect();
            PopUp.ShowDialog();
        }
    }


    //slop
    public static void ExportBattleVideo(int BattleVideo)
    {
        int VideoLength = 0x1D5A;
        byte[] ChosenVideo = Functions.EncryptedVideoList[BattleVideo];
        string Filter = "Encrypted Battle Video (*.ebv4)|*.ebv4|Decrypted Battle Video (*.dbv4)|*.dbv4";

        if (Functions.CurrentGame >= Game.BlackWhite)
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

