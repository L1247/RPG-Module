#region

using TMPro;
using UnityEngine;

#endregion

namespace rStar.RPGModules.Combat.Example.Beginner1
{
    public class DamageText : MonoBehaviour
    {
    #region Private Variables

        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private TMP_Text tmpText;

    #endregion

    #region Unity events

        private void Update()
        {
            canvasGroup.alpha -= 0.005f;
            if (canvasGroup.alpha == 0) Destroy(gameObject);
        }

    #endregion

    #region Public Methods

        public void SetText(string content)
        {
            tmpText.text = content;
        }

    #endregion
    }
}