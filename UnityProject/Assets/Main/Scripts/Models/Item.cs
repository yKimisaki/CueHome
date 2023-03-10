
namespace CueHome.Models
{
    /// <summary>
    /// アイテムを表します。
    /// </summary>
    public class Item
    {
        /// <summary>
        /// このアイテムに関連づいている声優を取得します。
        /// </summary>
        public Character Character { get; }
        /// <summary>
        /// このアイテムの効果を取得します。
        /// </summary>
        public ItemEffect Effect { get; }
        /// <summary>
        /// このアイテムの効果を取得します。
        /// </summary>
        public int Group { get; }

        /// <summary>
        /// アイテム名
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// アイテム画像のパス
        /// </summary>
        public string IconPath { get; }
        /// <summary>
        /// 他のアイテムの効果で壊れるアイテムかどうか
        /// </summary>
        public bool IsBreakable { get; }
        /// <summary>
        /// 支払日の報酬で手に入るアイテムかどうか
        /// </summary>
        public bool IsReward { get; }
        /// <summary>
        /// アイテムが破壊されたかどうか
        /// </summary>
        public bool IsBroken { get; private set; } = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_character"></param>
        public Item(Character _character)
        {
            Character = _character;

            Name = _character.Name;
            IconPath = Data.IconPath.CharacterByName[_character.Name];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="effect"></param>
        /// <param name="isBreakable"></param>
        /// <param name="isReward"></param>
        public Item(string name, ItemEffect effect, int group, bool isBreakable, bool isReward)
        {
            Effect = effect;
            Group = group;

            Name = name;
            IconPath = Data.IconPath.ItemByName[Name];

            IsBreakable = isBreakable;
            IsReward = isReward;
        }

        /// <summary>
        /// このアイテムが引かれたときに行う動作を示します。
        /// </summary>
        /// <param name="currentX"></param>
        /// <param name="currentY"></param>
        /// <param name="model"></param>
        public void OnLot(int currentX, int currentY, Main model)
        {
            if (Character is not null)
            {
                var earnings = Character.GetEarnings(model.Year, model.Month, model.Week);
                Character.AddPendingCoinAmount(earnings);
            }
            else if (Effect is not null) 
            {
                Effect.Effect(this, currentX, currentY, model);
            }
        }

        /// <summary>
        /// このアイテムを破壊扱いにします。
        /// </summary>
        public void Break()
        {
            IsBroken = true;
        }

        public static Item Instantiate(Item original)
        {
            if (original.Character is not null)
                return new Item(original.Character);
            else
                return new Item(original.Name, original.Effect, original.Group, original.IsBreakable, original.IsReward);
        }
    }
}
