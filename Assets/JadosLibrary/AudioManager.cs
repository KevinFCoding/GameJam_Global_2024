using UnityEngine;

// Ce script est le script AudioManager
// Il est a mettre sur un game object � part avec une audioSource

// Il faut remplir la liste avec une ou plusieurs musiques
// L'indexe � remplir est la premi�re musiquue a jouer 
// Si l'indexe n'est pas dans la liste c'est la musique a l'indexe 0 qui se lance
// Si la musique ne loop pas, elle se joue dans l'ordre de la liste et reboute a l'indexe 0
public class AudioManager : MonoBehaviour
{
    [Header("Liste des musiques")]
    [SerializeField] AudioClip[] songs;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] int _indexFirstMusic;
    [SerializeField] float volume;
    [SerializeField] bool _loop;

    private void Start()
    {
        if(_indexFirstMusic > songs.Length) { _audioSource.clip = songs[0]; } 
        else { _audioSource.clip = songs[_indexFirstMusic]; }
    }
    private void Update()
    {
        if (!_loop && !_audioSource.loop && _audioSource.timeSamples > _audioSource.clip.samples - 3800)
        {
            NextSong();
        }
    }

    public void NextSong()
    {
        _audioSource.volume = volume;
        if (_loop)
        {
            _audioSource.loop = true;
        }
        else
        {
                _indexFirstMusic++;
                if (_indexFirstMusic > songs.Length - 1)
                {
                    _indexFirstMusic = 0;
                }
                else
                {
                    _audioSource.clip = songs[_indexFirstMusic];
                }
            
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}
