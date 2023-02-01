using CueHome.Models;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CueHome.Presenters
{
    /// <summary>
    /// 家賃の更新を表す Presenter です。
    /// </summary>
    public class RenewalPresenter : MonoBehaviour
    {
        public TMP_Text Text;

        private Main model;
        private UnityAction onRenewaled;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        /// <param name="_onRenewaled"></param>
        public void Initialize(Main _model, UnityAction _onRenewaled)
        {
            model = _model;
            onRenewaled = _onRenewaled;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            gameObject.SetActive(true);

            Text.text = $"{model.GetPaymentAmount(model.Year - 1)} → {model.GetPaymentAmount(model.Year)} コイン";
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            model.Renewal();
            onRenewaled.Invoke();

            gameObject.SetActive(false);
        }
    }
}
