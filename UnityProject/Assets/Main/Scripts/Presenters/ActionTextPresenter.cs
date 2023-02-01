using UnityEngine;

namespace CueHome.Presenters
{
    /// <summary>
    /// マスで起こるアクションの Presenter です。
    /// </summary>
    public class ActionTextPresenter : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public void OnFinishAnimation()
        {
            Destroy(gameObject);
        }
    }
}
