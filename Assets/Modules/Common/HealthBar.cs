#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Modules.Common
{
    public class HealthBar : MonoBehaviour
    {
    #region Private Variables

        private int max     = 1;
        private int current = 1;

        [SerializeField]
        private Image front;

    #endregion

    #region Public Methods

        public void SetCurrent(int value)
        {
            current          = value;
            front.fillAmount = current / (float)max;
        }

        public void SetMax(int value)
        {
            max              = value;
            front.fillAmount = current / (float)max;
        }

    #endregion
    }
}