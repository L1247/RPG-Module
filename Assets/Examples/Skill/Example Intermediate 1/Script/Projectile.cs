#region

using UnityEngine;

#endregion

namespace Modules.Skill.Example.Intermediate1
{
    public class Projectile : MonoBehaviour
    {
    #region Unity events

        private void Start()
        {
            Destroy(gameObject , 3);
        }

        private void Update()
        {
            transform.position += Time.deltaTime * 5 * Vector3.right;
        }

    #endregion
    }
}