namespace Systems.EnemySystem
{
    using UnityEngine;

    
    public class EnemyHitFeedback : MonoBehaviour
    {
        private Animator animator;
        private AnimationClip hitClip;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Init(AnimationClip clip)
        {
            hitClip = clip;
        }

        public void Play()
        {
            if (hitClip == null)
                return;
            if (!gameObject.activeInHierarchy)
                return;

            animator.Play(hitClip.name, 0, 0f);
        }
    }

}