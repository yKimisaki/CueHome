using System;
using System.Linq;

namespace CueHome.Models
{
    /// <summary>
    /// アイテムの効果を表します。
    /// </summary>
    public class ItemEffect
    {
        private enum EffectType
        {
            全キャラにコイン追加,
            周囲のマスにコイン追加,
            周囲の特定のキャラにコイン追加,
            周囲のアイテムを破壊,
        }

        private EffectType type;
        private int effectValue;
        private string[] args;

        // 効果があったら壊れるアイテムかどうか
        private bool isInstant;

        private ItemEffect(EffectType _type, int _effectValue, string[] _args, bool _isInstant)
        {
            type = _type;
            effectValue = _effectValue;
            args = _args;
            isInstant = _isInstant;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="effectCoinAmount"></param>
        /// <param name="isInstant"></param>
        /// <returns></returns>
        public static ItemEffect Get全キャラにコイン追加(int effectCoinAmount, bool isInstant)
        {
            return new ItemEffect(EffectType.全キャラにコイン追加, effectCoinAmount, Array.Empty<string>(), isInstant);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="effectCoinAmount"></param>
        /// <param name="isInstant"></param>
        /// <returns></returns>
        public static ItemEffect Get周囲のマスにコイン追加(int effectCoinAmount, bool isInstant)
        {
            return new ItemEffect(EffectType.周囲のマスにコイン追加, effectCoinAmount, Array.Empty<string>(), isInstant);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="effectCoinAmount"></param>
        /// <param name="isInstant"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ItemEffect Get周囲の特定のキャラにコイン追加(int effectCoinAmount, bool isInstant, params string[] args)
        {
            return new ItemEffect(EffectType.周囲の特定のキャラにコイン追加, effectCoinAmount, args, isInstant);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isInstant"></param>
        /// <returns></returns>
        public static ItemEffect Get周囲のアイテムを破壊(bool isInstant)
        {
            return new ItemEffect(EffectType.周囲のアイテムを破壊, 0, Array.Empty<string>(), isInstant);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description => (type switch
        {
            EffectType.全キャラにコイン追加 => $"登場中の全声優の獲得コインが {effectValue} 増える。",
            EffectType.周囲のマスにコイン追加 => $"周囲の声優の獲得コインが {effectValue} 増える。",
            EffectType.周囲の特定のキャラにコイン追加 => $"周囲の {args.Aggregate((x, y) => x + "、" + y)} の獲得コインが {effectValue} 増える。",
            EffectType.周囲のアイテムを破壊 => $"周囲のアイテムを消す。",
            _ => "",
        }) 
            + (isInstant ? "効果があった場合、このアイテムは消える。" : "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="currentX"></param>
        /// <param name="currentY"></param>
        /// <param name="model"></param>
        public void Effect(Item original, int currentX, int currentY, Main model)
        {
            var isEffected = false;
            switch (type)
            {
                case EffectType.全キャラにコイン追加:
                    for (var x = 0; x < Slot.XLength; ++x)
                        for (var y = 0; y < Slot.YLength; ++y)
                        {
                            var targetCharacter = model.Slot.GetElement(x, y).CurrentItem?.Character;
                            if (targetCharacter is not null)
                            {
                                targetCharacter.AddPendingCoinAmount(effectValue);
                                isEffected = true;
                            }
                        }
                    break;
                case EffectType.周囲のマスにコイン追加:
                    for (var x = currentX - 1; x <= currentX + 1; ++x)
                        for (var y = currentY - 1; y <= currentY + 1; ++y)
                        {
                            if (x < 0 || x >= Slot.XLength)
                                continue;
                            if (y < 0 || y >= Slot.YLength)
                                continue;

                            var targetCharacter = model.Slot.GetElement(x, y).CurrentItem?.Character;
                            if (targetCharacter is not null)
                            {
                                targetCharacter.AddPendingCoinAmount(effectValue);
                                isEffected = true;
                            }
                        }
                    break;
                case EffectType.周囲の特定のキャラにコイン追加:
                    for (var x = currentX - 1; x <= currentX + 1; ++x)
                        for (var y = currentY - 1; y <= currentY + 1; ++y)
                        {
                            if (x < 0 || x >= Slot.XLength)
                                continue;
                            if (y < 0 || y >= Slot.YLength)
                                continue;

                            var targetCharacter = model.Slot.GetElement(x, y).CurrentItem?.Character;
                            if (targetCharacter is not null && args.Contains(targetCharacter.Name))
                            {
                                targetCharacter.AddPendingCoinAmount(effectValue);
                                isEffected = true;
                            }
                        }
                    break;
                case EffectType.周囲のアイテムを破壊:
                    for (var x = currentX - 1; x <= currentX + 1; ++x)
                        for (var y = currentY - 1; y <= currentY + 1; ++y)
                        {
                            if (x < 0 || x >= Slot.XLength)
                                continue;
                            if (y < 0 || y >= Slot.YLength)
                                continue;

                            var targetItem = model.Slot.GetElement(x, y).CurrentItem;
                            if (targetItem is not null && targetItem.IsBreakable)
                            {
                                model.ItemRepository.RemoveItem(targetItem);
                                isEffected = true;
                            }
                        }
                    break;
            }

            if (isEffected && isInstant)
                model.ItemRepository.RemoveItem(original);
        }
    }
}
