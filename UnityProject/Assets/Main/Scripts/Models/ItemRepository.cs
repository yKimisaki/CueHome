using CueHome.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CueHome.Models
{
    /// <summary>
    /// 抽選するアイテムの管理をします。
    /// </summary>
    public class ItemRepository
    {
        private Main model;

        /// <summary>
        /// すべての声優
        /// </summary>
        public Item[] AllCharacters { get; }

        private List<Item> itemBox = new();
        private List<Item> additionalItems = new();

        /// <summary>
        /// 最初に予め追加されているアイテム
        /// </summary>
        public Item[] DefaultItems { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="_defaultItems"></param>
        public ItemRepository(Main _model)
        {
            model = _model;

            AllCharacters = model.Characters.Select(x => new Item(x)).ToArray();
            DefaultItems = model.Items.Take(4).ToArray();
        }

        /// <summary>
        /// 抽選対象にアイテムを追加します。
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            additionalItems.Add(item);
        }

        /// <summary>
        /// 抽選対象からアイテムを除きます。
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(Item item)
        {
            additionalItems.Remove(item);
        }

        /// <summary>
        /// アイテムを所持しているかを確認します。
        /// </summary>
        /// <param name="item"></param>
        public bool HasItem(Item item) => CurrentAllItems.Contains(item);

        /// <summary>
        /// ボックスからアイテムを引きます。
        /// </summary>
        /// <returns></returns>
        public Item PickFromBox()
        {
            if (!itemBox.Any())
                return null;

            var index = Random.Range(0, itemBox.Count);
            var item = itemBox[index];
            itemBox.RemoveAt(index);
            return item;
        }

        /// <summary>
        /// ボックスを初期化し、有効な声優とアイテムを入れます。
        /// </summary>
        public void ResetBox()
        {
            itemBox = CurrentAllItems.ToList();
        }

        private IEnumerable<Item> CurrentAllItems =>
            AllCharacters
                .Where(x => !x.Character.IsRetired)
                .Concat(DefaultItems.Where(x => x.Name != Name.由良桐香 || model.Year < 3))
                .Concat(additionalItems);
    }
}
