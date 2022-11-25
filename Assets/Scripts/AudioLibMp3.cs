using System;
using System.IO;
using System.Linq;
using Random = System.Random;

public class AudioLibMp3
{
    public string[] Mp3FilesToArray(string PathToFolder)
    {
        string[] allfoldersTmp = Directory.GetFiles(PathToFolder, "*.mp3", SearchOption.AllDirectories);
        string[] allfolders_mp3 = new string[allfoldersTmp.Length];

        var rnd2 = new Random();
        int[] a = Enumerable.Range(0, allfoldersTmp.Length).OrderBy(num => rnd2.Next()).ToArray();

        for (int i = 0; i < allfoldersTmp.Length; i++)
            allfolders_mp3[i] = allfoldersTmp[a[i]];
        return allfolders_mp3;
    }

}
