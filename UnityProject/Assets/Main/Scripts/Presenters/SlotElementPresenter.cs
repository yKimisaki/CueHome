using CueHome.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CueHome.Presenters
{
    /// <summary>
    /// 盤のマスを表す Presenter です。
    /// </summary>
    public class SlotElementPresenter : MonoBehaviour
    {
        private SlotElement model;

        public Image IconImage;
        public Sprite NoneIconSprite;

        public TMP_Text ActionTextPrefab;

        public int X;
        public int Y;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        public void Initialize(SlotElement _model)
        {
            model = _model;
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateItemImage()
        {
            if (model.CurrentItem is not null)
                IconImage.sprite = Resources.Load<Sprite>(model.CurrentItem.IconPath);
            else
                IconImage.sprite = NoneIconSprite;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowEffectedAmountAction()
        {
            if (model.CurrentItem?.Character is null)
            {
                return;
            }

            var actionText = Instantiate(ActionTextPrefab, transform);
            var effectedAmount = model.CurrentItem?.Character?.LatestCommittedCoinAmount ?? 0;
            actionText.text = effectedAmount >= 0 ? $"+{effectedAmount}" : $"{effectedAmount}";
        }
    }
}
