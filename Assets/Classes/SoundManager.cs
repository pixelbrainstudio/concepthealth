using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    #region Unity references
    [SerializeField]
    private AudioSource soundSource;

    [SerializeField]
    private AudioClip sfxBounce;
    #endregion

    #region Public methods
    public void PlaySfx()
    {
        if (!ReferenceEquals(soundSource, null) && !ReferenceEquals(sfxBounce, null))
        {
            soundSource.PlayOneShot(sfxBounce);
        }
    }
    #endregion    
}
