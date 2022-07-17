#region

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace rStar.Modules.Skill.Example2
{
    public class SkillExample2Reference : MonoBehaviour
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