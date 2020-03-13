using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public bool isPlayed;
    public bool flag;
    void Start()
    {
        flag =false;
        isPlayed = false;
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    // Update is called once per frame

    public void playAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        //isPlayed = false;
        //print("flag: " + flag);
        //print("isPlayed: " + isPlayed);
        if (flag == true && isPlayed == false)
        {   
            if(!audioSource.isPlaying){

                audioSource.Play();
                flag = false;
                //isPlayed = true;
            }
        }
        if (isPlayed == true && flag == true)
        {
            //audioSource.Stop();
            flag = false;
            isPlayed = false;
        }

    }
}
