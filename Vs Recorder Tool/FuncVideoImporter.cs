using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vs_Recorder_Tool;

public static class VideoImporter
{
    //Slop
    //Also, the copy-paste method doesn't work on gen 4 battle videos
    //needs more research
    public static void ImportBattleVideo5()
    {
        string Filter = "Encrypted Battle Video (*.ebv5)|*.ebv5";
        int VideoOffset = 0;
        byte[] TargetArray = Functions.EncryptedVideoList[Functions.CurrentBattleVideo];


        switch (Functions.CurrentGame)
        {
            case Game.BlackWhite:
                VideoOffset = 0x4A600;
                break;

            case Game.Black2White2:
                VideoOffset = 0x4C000;
                break;
        }

        if (Functions.CurrentGame <= Game.BlackWhite) { return; }

        OpenFileDialog BattleVideoImport = new OpenFileDialog
        {
            Filter = Filter
        };

        BattleVideoImport.ShowDialog();

        if (BattleVideoImport.FileName == "") { return; }

        Stream Importer = BattleVideoImport.OpenFile();

        Importer.Read(TargetArray, 0, Functions.BattleVideoLength5);
        Importer.Close();

        int VideoLength = Functions.BattleVideoLength5;
        if(BattleVideoImport.FileName.Split('.').Last() == " .ebv") {VideoLength = 0x18A4; SaveReader.ExtendLegacyEBV(Functions.CurrentBattleVideo); }

        if (File.Exists(Functions.CurrentFilePath + ".bak"))
        {
            File.Delete(Functions.CurrentFilePath + ".bak");

        }

        File.Copy(Functions.CurrentFilePath, Functions.CurrentFilePath + ".bak");



        Stream Savefile = File.OpenWrite(Functions.CurrentFilePath);


        Savefile.Position = VideoOffset + Functions.BattleVideoLength5 * Functions.CurrentBattleVideo;
        Savefile.Write(Functions.EncryptedVideoList[Functions.CurrentBattleVideo], 0, VideoLength);
        Savefile.Close();

        SaveReader.DecryptBattleVideos(0x1 << Functions.CurrentBattleVideo);

    }
}
