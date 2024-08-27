using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[System.Serializable]public class Sound{

    public AudioMixer audioMixer;
    public string name;
    public AudioClip clip;
    public AudioSource source;
    private float volume = 5f;
 
    public void AssignMixer(AudioSource source)
    {
        source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
    }
    public void SetSource(AudioSource _source){
        source = _source;
        source.clip = clip;
        AssignMixer(source);
    }

    public void Play(){
        source.Play();
        source.volume = volume;
    }

    public void Stop(){
        source.Stop();
    }

}

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static AudioManager instance;
    public Slider slider;
    
    [SerializeField]Sound sound;

    void Awake(){
        if(instance != null)
        {
            Debug.Log("Troppi AudioManager nella scena");
        }else
        {
            instance = this;
        }
    }


    void Start(){
        GameObject _go = new GameObject("Sound_" + sound.name);
        sound.SetSource(_go.AddComponent<AudioSource>());
        PlaySound(sound.name);
        if(PlayerPrefs.HasKey("Volume")){
            audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        }else{
            audioMixer.SetFloat("Volume", -10);
            PlayerPrefs.SetFloat("Volume", -10);
        }
        slider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void PlaySound(string name){
            if(sound.name == name){
                sound.Play();
                return;
            }
        
    }
    public void SetVolume(float volume){
            if(volume == -60){
                volume = -80;
            }
            audioMixer.SetFloat("Volume", volume);
            PlayerPrefs.SetFloat("Volume", volume);
            slider.value = volume;
    }
}