

namespace CueHome.Models
{
    /// <summary>
    /// 盤のマスを表します。
    /// </summary>
    public class SlotElement
    {
        private ItemRepository itemRepository;

        /// <summary>
        /// このマスにいる現在のアイテムです。
        /// </summary>
        public Item CurrentItem { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_itemRepository"></param>
        public SlotElement(ItemRepository _itemRepository)
        {
            itemRepository = _itemRepository;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void Initialize(int index)
        {
            if (index < 16)
                CurrentItem = itemRepository.AllCharacters[index];
            else if (index < 19)
                CurrentItem = itemRepository.DefaultItems[index - 16];
        }

        /// <summary>
        /// マスを抽選します。
        /// </summary>
        public void Lot()
        {
            CurrentItem = itemRepository.PickFromBox();
        }
    }
}
