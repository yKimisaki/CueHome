using CueHome.Data;
using CueHome.Models;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CueHome.Presenters
{
    /// <summary>
    /// アイテム取得の Presenter です。
    /// </summary>
    public class ItemPresenter : MonoBehaviour
    {
        public Image IconImage;
        public TMP_Text NameText;
        public TMP_Text DescriptionText;

        private ItemRepository model;
        private UnityAction onSelected;

        private Item currentItem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        /// <param name="_onSelected"></param>
        public void Initialize(ItemRepository _model, UnityAction _onSelected)
        {
            model = _model;
            onSelected = _onSelected;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Open(Main model, Item item)
        {
            IconImage.sprite = Resources.Load<Sprite>(IconPath.ItemByName[item.Name]);
            NameText.text = item.Name;
            DescriptionText.text = item.Effect.GetDescription(item.Name, model);

            currentItem = item;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Select()
        {
            if (currentItem is not null)
                model.AddItem(currentItem);

            onSelected.Invoke();
        }
    }
}
