using TMPro;
using UnityEngine;

namespace CueHome.Presenters
{
    /// <summary>
    /// マスで起こるアクションの Presenter です。
    /// </summary>
    public class ActionTextPresenter : MonoBehaviour
    {
        public TMP_Text Text;

        public string text { set => Text.text = value; }

        /// <summary>
        /// 
        /// </summary>
        public void OnFinishAnimation()
        {
            Destroy(gameObject);
        }
    }
}
