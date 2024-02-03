using System.Collections.Generic;
using UnityEngine;

namespace _Game.Audios
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Audio Clip Refs", fileName = "New AudioClipRefs")]
    public class AudioClipRefsSO : ScriptableObject
    {
        #region Variables

        public AudioClip btnPress;
        public AudioClip battleStart;
        public AudioClip hitSound;
        public AudioClip baseDestroy;


        #endregion
    }
}