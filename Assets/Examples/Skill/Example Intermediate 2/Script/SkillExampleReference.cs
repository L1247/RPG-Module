#region

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace rStar.RPGModules.Skill.Example.Intermediate2
{
    public class SkillExampleReference : MonoBehaviour
    {
    #region Public Variables

        public Animator   enemyAnimator;
        public GameObject projectilePrefab;
        public Image      coolDownImage;
        public Image      mainImage;
        public TMP_Text   info;
        public Transform  shootPoint;

    #endregion
    }
}