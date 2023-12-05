using System;
using System.IO;
using System.Windows.Forms;

public class BoxUpload
{
    public static void OpenBoxUploadFile()
    {
        byte[] RawData = new byte[1000];
        int Counter = 0;
        int Offset = 0;
        OpenFileDialog BoxUploadFile = new OpenFileDialog();

        BoxUploadFile.ShowDialog();
        Stream BoxUploadData = BoxUploadFile.OpenFile();
        BoxUploadData.Read(RawData, 0, 1000);

        while (Counter < 30)
        {
            Functions.BoxUploadList[Counter] = new BoxUploadPokemon();
            Functions.BoxUploadList[Counter].SpeciesID = BitConverter.ToUInt16(RawData, 40 + (2 * Offset));
            Functions.BoxUploadList[Counter].Species = Functions.SpeciesList[BitConverter.ToUInt16(RawData, 40 + (2 * Offset))];
            Functions.BoxUploadList[Counter].PIDint = BitConverter.ToUInt32(RawData, 100 + (4 * Offset));
            Functions.BoxUploadList[Counter].PIDBytes = BitConverter.ToString(Functions.LoopReader(RawData, 100 + (4 * Offset), 4));
            Functions.BoxUploadList[Counter].TrainerID = BitConverter.ToUInt16(RawData, 220 + (4 * Offset));
            Functions.BoxUploadList[Counter].SecretID = BitConverter.ToUInt16(RawData, 222 + (4 * Offset));
            Functions.BoxUploadList[Counter].FormeByte = RawData[344 + Offset];
            //Functions.BoxUploadList[Counter].IsShiny = FuncPokemonData.IsShiny(Functions.BoxUploadList[Counter].PIDint, Functions.BoxUploadList[Counter].TrainerID, Functions.BoxUploadList[Counter].SecretID);

            Offset++;
            Counter++;
        }

        Functions.BoxName = TextParser.TextConverter(Functions.LoopReader(RawData, 0, 18));
    }
}