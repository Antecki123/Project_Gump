using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace FrontEnd
{
    public class Widget : MonoBehaviour
    {
        [Header("Panel Settings")]
        [SerializeField] private Vector2 startPosition;
        [SerializeField] private Vector2 targetPosition;
        [Space]
        [SerializeField] private Transition transition;

        private const float ANIMATION_TIME = 0.75f;
        private bool state;

        public void SetActive(bool state)
        {
            this.state = state;

            if (state) EnablePanel();
            else DisablePanel();
        }

        private void EnablePanel()
        {
            if (transition == Transition.None)
            {
                gameObject.SetActive(true);
            }

            else if (transition == Transition.Slide)
            {
                gameObject.SetActive(true);
                transform.DOLocalMove(targetPosition, ANIMATION_TIME).SetUpdate(true);
            }

            else if (transition == Transition.PopUp)
            {
                gameObject.SetActive(true);
                transform.DOScale(1.0f, ANIMATION_TIME).SetUpdate(true);
            }

            else if (transition == Transition.PopUpSlide)
            {
                gameObject.SetActive(true);
                transform.DOScale(1.0f, ANIMATION_TIME).SetUpdate(true);
                transform.DOLocalMove(targetPosition, ANIMATION_TIME).SetUpdate(true);
            }
        }

        private async void DisablePanel()
        {
            if (transition == Transition.None)
            {
                gameObject.SetActive(state);
            }

            else if (transition == Transition.Slide)
            {
                transform.DOLocalMove(startPosition, ANIMATION_TIME).SetUpdate(true);

                await Task.Delay((int)(ANIMATION_TIME * 1000));
                gameObject.SetActive(state);
            }

            else if (transition == Transition.PopUp)
            {
                transform.DOScale(0.0f, ANIMATION_TIME).SetUpdate(true);

                await Task.Delay((int)(ANIMATION_TIME * 1000));
                gameObject.SetActive(state);
            }

            else if (transition == Transition.PopUpSlide)
            {
                transform.DOScale(0.0f, ANIMATION_TIME).SetUpdate(true);
                transform.DOLocalMove(startPosition, ANIMATION_TIME).SetUpdate(true);

                await Task.Delay((int)(ANIMATION_TIME * 1000));
                gameObject.SetActive(state);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Set Start Position")]
        public void SetStartPosition() => startPosition = transform.localPosition;

        [ContextMenu("Set Target Position")]
        public void SetTargetPosition() => targetPosition = transform.localPosition;
#endif

        public enum Transition { None, Slide, PopUp, PopUpSlide }
    }
}