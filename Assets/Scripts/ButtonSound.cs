using UnityEngine;
using UnityEngine.UI;
public class ButtonSound : MonoBehaviour
{
    [SerializeField] AudioClip sound;
    [SerializeField] AudioSource soundSource;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Sound);
    }
    private void Sound()
    {
        soundSource.PlayOneShot(sound);
    }
}
