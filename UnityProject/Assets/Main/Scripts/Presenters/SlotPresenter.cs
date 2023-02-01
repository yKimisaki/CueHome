using CueHome.Models;
using TMPro;
using UnityEngine;

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
        public void Initialize(Slot _model)
        {
            model = _model;

            foreach (var slotElement in SlotElements)
                slotElement.Initialize(model.GetElement(slotElement.X, slotElement.Y));

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
            }
        }
    }
}
