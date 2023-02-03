using CueHome.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace CueHome.Models
{
    /// <summary>
    /// アイテムの効果タイプを表します。
    /// </summary>
    public enum EffectType
    {
        全キャラにコイン追加,
        周囲のキャラにコイン追加,
        周囲の特定のキャラにコイン追加,
        周囲のアイテムを破壊,
        周囲のキャラの稼ぎを倍にする,
        貯金箱,
    }

    /// <summary>
    /// アイテムの効果を表します。
    /// </summary>
    public class ItemEffect
    {
        public EffectType Type { get; }
        private Func<Main, int> getEffectValue;
        private string[] args;

        // 効果があったら壊れるアイテムかどうか
        private bool isInstant;

        public List<Item> latestTargetItems = new();
        public IEnumerable<Item> LatestTargetItems => latestTargetItems;

        private ItemEffect(EffectType _type, Func<Main, int> _getEffectValue, string[] _args, bool _isInstant)
        {
            Type = _type;
            getEffectValue = _getEffectValue;
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
            return new ItemEffect(EffectType.全キャラにコイン追加, _ => effectCoinAmount, Array.Empty<string>(), isInstant);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="effectCoinAmount"></param>
        /// <param name="isInstant"></param>
        /// <returns></returns>
        public static ItemEffect Get周囲のキャラにコイン追加(int effectCoinAmount, bool isInstant)
        {
            return new ItemEffect(EffectType.周囲のキャラにコイン追加, _ => effectCoinAmount, Array.Empty<string>(), isInstant);
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
            return new ItemEffect(EffectType.周囲の特定のキャラにコイン追加, _ => effectCoinAmount, args, isInstant);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isInstant"></param>
        /// <returns></returns>
        public static ItemEffect Get周囲のアイテムを破壊(bool isInstant)
        {
            return new ItemEffect(EffectType.周囲のアイテムを破壊, _ => 0, Array.Empty<string>(), isInstant);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getEffectValue"></param>
        /// <param name="isInstant"></param>
        /// <returns></returns>
        public static ItemEffect Get周囲のキャラの稼ぎを倍にする(Func<Main, int> getEffectValue, bool isInstant)
        {
            return new ItemEffect(EffectType.周囲のキャラの稼ぎを倍にする, getEffectValue, Array.Empty<string>(), isInstant);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getEffectValue"></param>
        /// <returns></returns>
        public static ItemEffect Get貯金箱(Func<Main, int> getEffectValue)
        {
            return new ItemEffect(EffectType.貯金箱, getEffectValue, Array.Empty<string>(), false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetDescription(string name, Main model)
        {
            if (name == Name.思い出のオルゴール)
                return $"周囲の声優の稼ぎを経過年数倍にする。(アイテムの効果による獲得コインは対象外。)\nこのアイテムは2つ以上保持できない。";
            if (name == Name.へそくり貯金箱)
                return $"リセットハンマーで壊されたとき、登場中の全声優の獲得コインが 経過年数x70 増える。";

            return (Type switch
            {
                EffectType.全キャラにコイン追加 => $"登場中の全声優の獲得コインが {getEffectValue(model)} 増える。",
                EffectType.周囲のキャラにコイン追加 => $"周囲の声優の獲得コインが {getEffectValue(model)} 増える。",
                EffectType.周囲の特定のキャラにコイン追加 => $"周囲の {args.Aggregate((x, y) => x + "、" + y)} の獲得コインが {getEffectValue(model)} 増える。",
                EffectType.周囲のアイテムを破壊 => $"周囲のアイテムを消す。",
                EffectType.周囲のキャラの稼ぎを倍にする => $"周囲の声優の稼ぎを {getEffectValue(model)} 倍にする。(アイテムの効果による獲得コインは対象外。)",
                EffectType.貯金箱 => $"リセットハンマーで壊されたとき、登場中の全声優の獲得コインが {getEffectValue(model)} 増える。",
                _ => "",
            })
            + (isInstant ? "\n効果があった場合、このアイテムは消える。" : "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="currentX"></param>
        /// <param name="currentY"></param>
        /// <param name="model"></param>
        public void Effect(Item original, int currentX, int currentY, Main model)
        {
            latestTargetItems.Clear();
            switch (Type)
            {
                case EffectType.全キャラにコイン追加:
                    latestTargetItems.AddRange(全キャラにコインを追加(getEffectValue, model));
                    break;
                case EffectType.周囲のキャラにコイン追加:
                    for (var x = currentX - 1; x <= currentX + 1; ++x)
                        for (var y = currentY - 1; y <= currentY + 1; ++y)
                        {
                            if (x == 0 && y == 0)
                                continue;
                            if (x < 0 || x >= Slot.XLength)
                                continue;
                            if (y < 0 || y >= Slot.YLength)
                                continue;

                            var targetItem = model.Slot.GetElement(x, y).CurrentItem;
                            var targetCharacter = targetItem?.Character;
                            if (targetCharacter is not null)
                            {
                                targetCharacter.AddPendingCoinAmount(getEffectValue(model));
                                latestTargetItems.Add(targetItem);
                            }
                        }
                    break;
                case EffectType.周囲の特定のキャラにコイン追加:
                    for (var x = currentX - 1; x <= currentX + 1; ++x)
                        for (var y = currentY - 1; y <= currentY + 1; ++y)
                        {
                            if (x == 0 && y == 0)
                                continue;
                            if (x < 0 || x >= Slot.XLength)
                                continue;
                            if (y < 0 || y >= Slot.YLength)
                                continue;

                            var targetItem = model.Slot.GetElement(x, y).CurrentItem;
                            var targetCharacter = targetItem?.Character;
                            if (targetCharacter is not null && args.Contains(targetCharacter.Name))
                            {
                                targetCharacter.AddPendingCoinAmount(getEffectValue(model));
                                latestTargetItems.Add(targetItem);
                            }
                        }
                    break;
                case EffectType.周囲のアイテムを破壊:
                    for (var x = currentX - 1; x <= currentX + 1; ++x)
                        for (var y = currentY - 1; y <= currentY + 1; ++y)
                        {
                            if (x == currentX && y == currentY)
                                continue;
                            if (x < 0 || x >= Slot.XLength)
                                continue;
                            if (y < 0 || y >= Slot.YLength)
                                continue;

                            var targetItem = model.Slot.GetElement(x, y).CurrentItem;
                            if (targetItem is not null && targetItem.IsBreakable)
                            {
                                model.ItemRepository.RemoveItem(targetItem);
                                latestTargetItems.Add(targetItem);
                            }
                        }
                    // 破壊時に効果を発動するアイテム
                    foreach(var targetItem in latestTargetItems)
                        if (targetItem.Effect is not null)
                            switch (targetItem.Effect.Type)
                            {
                                case EffectType.貯金箱:
                                    全キャラにコインを追加(targetItem.Effect.getEffectValue, model);
                                    break;
                            }
                    break;
                case EffectType.周囲のキャラの稼ぎを倍にする:
                    for (var x = currentX - 1; x <= currentX + 1; ++x)
                        for (var y = currentY - 1; y <= currentY + 1; ++y)
                        {
                            if (x == 0 && y == 0)
                                continue;
                            if (x < 0 || x >= Slot.XLength)
                                continue;
                            if (y < 0 || y >= Slot.YLength)
                                continue;

                            var targetItem = model.Slot.GetElement(x, y).CurrentItem;
                            var targetCharacter = targetItem?.Character;
                            if (targetCharacter is not null)
                            {
                                var actualEffectValue = targetCharacter.GetEarnings(model.Year, model.Month, model.Week) * (getEffectValue(model) - 1);
                                if (actualEffectValue <= 0)
                                    continue;

                                targetCharacter.AddPendingCoinAmount(actualEffectValue);
                                latestTargetItems.Add(targetItem);
                            }
                        }
                    break;
            }

            if (latestTargetItems.Any() && isInstant)
                model.ItemRepository.RemoveItem(original);
        }

        private static IEnumerable<Item> 全キャラにコインを追加(Func<Main, int> getEffectValue, Main model)
        {
            var targetItems = new List<Item>();
            for (var x = 0; x < Slot.XLength; ++x)
                for (var y = 0; y < Slot.YLength; ++y)
                {
                    var targetItem = model.Slot.GetElement(x, y).CurrentItem;
                    var targetCharacter = targetItem?.Character;
                    if (targetCharacter is not null)
                    {
                        targetCharacter.AddPendingCoinAmount(getEffectValue(model));
                        targetItems.Add(targetItem);
                    }
                }
            return targetItems;
        }
    }
}
