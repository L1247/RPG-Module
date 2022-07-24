#region

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Modules.Skill.Example.Intermediate2
{
    public class SkillExampleReference : MonoBehaviour
    {
    #region Public Variables

        public Animator   enemyAnimator;
        public Image      coolDownImage;
        public Image      mainImage;
        public Projectile projectilePrefab;
        public TMP_Text   info;
        public Transform  shootPoint;

    #endregion
    }
}