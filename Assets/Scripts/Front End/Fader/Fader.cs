using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace FrontEnd
{
    public class Fader : MonoBehaviour
    {
        [Header("Component References")]
        [SerializeField] private Volume volume;

        private const float FADE_DURATION = 0.5f;

        /// <summary>
        /// Fade the screen to a certain saturation value.
        /// </summary>
        /// <param name="saturation"></param>
        public async void Fade(float saturation)
        {
            volume.gameObject.SetActive(true);
            DOTween.To(() => volume.weight, w => volume.weight = w, saturation, FADE_DURATION).SetUpdate(true);

            await Task.Delay((int)FADE_DURATION * 1000);

            volume.gameObject.SetActive(saturation != 0);
        }
    }
}