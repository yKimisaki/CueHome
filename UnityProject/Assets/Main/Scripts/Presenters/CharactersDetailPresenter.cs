using CueHome.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CueHome.Presenters
{
    /// <summary>
    /// 声優の残高照会の Presenter です。
    /// </summary>
    public class CharactersDetailPresenter : MonoBehaviour
    {
        public CharacterPanelPresenter[] CharacterPanelPresenters;
        public Button PayButton;
        public Button BackButton;

        private Main model;
        private UnityAction onPaid;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        /// <param name="_onPaid"></param>
        public void Initialize(Main _model, UnityAction _onPaid)
        {
            model = _model;
            onPaid = _onPaid;

            for(var i = 0; i < CharacterPanelPresenters.Length; i++) 
                if (i < model.Characters.Count)
                    CharacterPanelPresenters[i].Initialize(model.Characters[i]);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            gameObject.SetActive(true);
            PayButton.gameObject.SetActive(false);
            BackButton.gameObject.SetActive(true);

            for (var i = 0; i < CharacterPanelPresenters.Length; i++)
                CharacterPanelPresenters[i].UpdateCoinAmount();
        }

        /// <summary>
        /// 
        /// </summary>
        public void OpenOnPayment()
        {
            gameObject.SetActive(true);
            PayButton.gameObject.SetActive(true);
            BackButton.gameObject.SetActive(false);

            for (var i = 0; i < CharacterPanelPresenters.Length; i++)
                CharacterPanelPresenters[i].UpdateCoinAmountOnPayment(model.CurrentPaymentAmount);
        }

        /// <summary>
        /// 
        /// </summary>
        public void OpenOnGameOver()
        {
            gameObject.SetActive(true);
            PayButton.gameObject.SetActive(false);
            BackButton.gameObject.SetActive(false);

            for (var i = 0; i < CharacterPanelPresenters.Length; i++)
                CharacterPanelPresenters[i].UpdateCoinAmountOnPayment(model.CurrentPaymentAmount);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Pay()
        {
            model.Pay();
            onPaid.Invoke();

            if (!model.IsGameOver)
                Close();
        }
    }
}
