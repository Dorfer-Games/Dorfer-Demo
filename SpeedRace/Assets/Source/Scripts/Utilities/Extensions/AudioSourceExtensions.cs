using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.Extentions
{
    public static class AudioSourceExtensions
    {
        public static void PlayOneShot(this AudioSource source, IEnumerable<AudioClip> clips)
        {
            source.PlayOneShot(clips.GetRandom());
        }
    }
}