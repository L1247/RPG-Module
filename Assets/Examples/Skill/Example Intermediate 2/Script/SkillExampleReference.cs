#region

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Modules.Skill.Example.Intermediate1
{
    public class SkillExampleReference : MonoBehaviour
    {
    #region Public Variables

        public Animator   enemyAnimator;
        public Button     execute;
        public Button     tick;
        public Button     use;
        public Image      coolDownImage;
        public Projectile projectilePrefab;
        public TMP_Text   info;
        public Transform  shootPoint;

    #endregion
    }
}