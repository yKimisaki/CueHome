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
        private List<Item> defaultItems = new();

        /// <summary>
        /// 最初に予め追加されているアイテム
        /// </summary>
        public IReadOnlyList<Item> DefaultItems => defaultItems;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="_defaultItems"></param>
        public ItemRepository(Main _model)
        {
            model = _model;

            AllCharacters = model.Characters.Select(x => new Item(x)).ToArray();
            defaultItems = model.Items.Take(4).Select(x => Item.Instantiate(x)).ToList();
        }

        /// <summary>
        /// 抽選対象にアイテムを追加します。
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            additionalItems.Add(Item.Instantiate(item));
        }

        /// <summary>
        /// 抽選対象からアイテムを除きます。
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(Item item)
        {
            if (!item.IsBreakable)
                return;

            item.Break();

            additionalItems.Remove(item);
            // 初期アイテムも壊れるものは消す
            defaultItems.Remove(item);
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
                // 由良桐香は原作でも途中で消えるので消す
                .Concat(DefaultItems.Where(x => x.Name != Name.由良桐香 || model.Year < 3))
                .Concat(additionalItems);
    }
}
