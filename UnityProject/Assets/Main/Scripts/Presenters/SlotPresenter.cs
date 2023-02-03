using CueHome.Models;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CueHome.Presenters
{
    /// <summary>
    /// 盤を表す Presenter です。
    /// </summary>
    public class SlotPresenter : MonoBehaviour
    {
        private Slot model;

        /// <summary>
        /// 
        /// </summary>
        public SlotElementPresenter[] SlotElements;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        public void Initialize(Slot _model, ItemRepository repository, UnityAction<Item, RectTransform> onOpenDesctiption, UnityAction onCloseDescription)
        {
            model = _model;

            foreach (var slotElement in SlotElements)
                slotElement.Initialize(model.GetElement(slotElement.X, slotElement.Y), repository, onOpenDesctiption, onCloseDescription);

            foreach (var slotElement in SlotElements)
                slotElement.UpdateItemImage();
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateItemImage()
        {
            foreach (var slotElement in SlotElements)
            {
                slotElement.UpdateItemImage();

                slotElement.ShowEffectedAmountAction();

                if (slotElement.LatestTargetItems.Any())
                    slotElement.ShowEffectAction();
            }
        }
    }
}
