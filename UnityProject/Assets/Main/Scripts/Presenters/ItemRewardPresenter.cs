using CueHome.Models;
using System.Linq;
using UnityEngine;

namespace CueHome.Presenters
{
    /// <summary>
    /// アイテム報酬のポップアップの Presenter です。
    /// </summary>
    public class ItemRewardPresenter : MonoBehaviour
    {
        public ItemPresenter[] ItemPresenters;

        private Main model;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        public void Initialize(Main _model)
        {
            model = _model;

            for(var i = 0; i < ItemPresenters.Length; i++)
            {
                ItemPresenters[i].Initialize(_model.ItemRepository, Close);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            gameObject.SetActive(true);

            var items = model.Items
                .Where(x => x.IsReward)
                .OrderBy(x => Random.Range(0, int.MaxValue))
                .GroupBy(x => x.Group)
                .ToDictionary(x => x.Key, x => x.FirstOrDefault());

            for (var i = 0; i < ItemPresenters.Length; i++)
                ItemPresenters[i].Open(model, items[i + 1]);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
