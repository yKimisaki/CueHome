using CueHome.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CueHome.Presenters
{
    /// <summary>
    /// 盤のマスを表す Presenter です。
    /// </summary>
    public class SlotElementPresenter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private SlotElement model;
        private ItemRepository repository;

        public Image IconImage;
        public Sprite NoneIconSprite;

        public Animator Animator;

        public ActionTextPresenter ActionTextPrefab;

        public int X;
        public int Y;

        private UnityAction<Item, RectTransform> onOpenDesctiption;
        private UnityAction onCloseDesctiption;

        /// <summary>
        /// このマスのアイテムが効果を及ぼしたアイテム一覧
        /// </summary>
        public Item CurrentItem => model.CurrentItem;
        public IEnumerable<Item> LatestTargetItems => model.CurrentItem?.Effect?.LatestTargetItems ?? Array.Empty<Item>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        public void Initialize(SlotElement _model, ItemRepository _repository, UnityAction<Item, RectTransform> _onOpenDesctiption, UnityAction _onCloseDescription)
        {
            model = _model;
            repository = _repository;
            onOpenDesctiption = _onOpenDesctiption;
            onCloseDesctiption = _onCloseDescription;
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
            actionText.transform.localScale = Vector3.one * Mathf.Clamp((effectedAmount) / 30f, 0.5f, 1.5f);
            actionText.text = effectedAmount >= 0 ? $"+{effectedAmount}" : $"{effectedAmount}";
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowEffectAction()
        {
            if (model.CurrentItem is null || model.CurrentItem.Character is not null)
            {
                return;
            }

            Animator.Play("SlotElementShakeAnimation");
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnEndEffectAction()
        {
            if (model.CurrentItem is not null && !repository.HasItem(model.CurrentItem))
            {
                IconImage.sprite = NoneIconSprite;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowEffectedAction()
        {
            if (model.CurrentItem?.Character is null)
            {
                return;
            }

            Animator.Play("SlotElementScaleAnimation");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (model.CurrentItem is not null)
                onOpenDesctiption.Invoke(model.CurrentItem, GetComponent<RectTransform>());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onCloseDesctiption.Invoke();
        }
    }
}
