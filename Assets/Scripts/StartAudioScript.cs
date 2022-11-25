using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class StartAudioScript : MonoBehaviour
{

    AudioClip _audioClip;
    AudioSource audioSource;
    int CurrentClip = 0;

    DateTime timeEnd;
    string[] ListAllMp3;

    [SerializeField] float TimeToChangeMp3 = 7;

    void Start()
    {
        int CurrentClip = 0;

        //  timeEnd = DateTime.Now;
        audioSource = GetComponent<AudioSource>();
        AudioLibMp3 audioLibMp3 = new AudioLibMp3();
       // string PathToFolderMp3 = "I:\\data1\\mp3.eng\\HarryPotter";
        string PathToFolderMp3 = "U:\\_cs5\\No Copyright Library";


        ListAllMp3 = audioLibMp3.Mp3FilesToArray(PathToFolderMp3);

        StartCoroutine(ListPlay());
    }



    IEnumerator ListPlay()
    {
        double dellta = (timeEnd - DateTime.Now).TotalSeconds;
        Debug.Log(" ListPlay Seconds " + dellta.ToString() + " timeEnd > DateTime.Now " + (timeEnd > DateTime.Now).ToString());

        while (timeEnd > DateTime.Now)
            yield return new WaitForSeconds((float)dellta);
        StartCoroutine(LoadAudioClip(ListAllMp3[CurrentClip++]));
    }




    IEnumerator LoadAudioClip(string PtsPath)
    {
        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip((PtsPath), AudioType.MPEG))
        {
            yield return uwr.SendWebRequest();
            _audioClip = DownloadHandlerAudioClip.GetContent(uwr);
            audioSource.clip = _audioClip;
            audioSource.Play();

            timeEnd = DateTime.Now.AddSeconds(_audioClip.length);
            if (TimeToChangeMp3 > 0)  // fast change for debug and etc 
                timeEnd = DateTime.Now.AddSeconds(TimeToChangeMp3);
            Debug.Log("TotalSeconds to change track = " + (timeEnd - DateTime.Now).TotalSeconds);

            StartCoroutine(ListPlay());

        }
    }


}



