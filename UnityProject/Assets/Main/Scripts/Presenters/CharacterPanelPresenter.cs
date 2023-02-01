using CueHome.Data;
using CueHome.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CueHome.Presenters
{
    /// <summary>
    /// 声優が保持しているコインを表す Presenter です。
    /// </summary>
    public class CharacterPanelPresenter : MonoBehaviour
    {
        public Image IconImage;
        public TMP_Text NameText;
        public TMP_Text CoinAmountText;

        private Character model;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        public void Initialize(Character _model)
        {
            model = _model;

            IconImage.sprite = Resources.Load<Sprite>(IconPath.CharacterByName[model.Name]);
            NameText.text = model.Name;
            CoinAmountText.text = $"{model.CoinAmount} コイン";
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateCoinAmount()
        {
            if (model.IsRetired)
            {
                CoinAmountText.color = Color.red;
                CoinAmountText.text = $"退去済み({model.RetiredYear} 年目 {model.RetiredMonth} 月)";
                return;
            }

            CoinAmountText.text = $"{model.CoinAmount} コイン";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentAmount"></param>
        public void UpdateCoinAmountOnPayment(int paymentAmount)
        {
            if (model.IsRetired)
            {
                CoinAmountText.color = Color.red;
                CoinAmountText.text = $"退去済み({model.RetiredYear} 年目 {model.RetiredMonth} 月)";
                return;
            }

            if (model.CoinAmount - paymentAmount < 0)
                CoinAmountText.color = Color.red;
            else
                CoinAmountText.color = Color.black;

            CoinAmountText.text = $"{model.CoinAmount} -> {model.CoinAmount - paymentAmount} コイン";
        }
    }
}
