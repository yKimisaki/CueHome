using System.Linq;
using UnityEngine;

namespace CueHome.Models
{
    /// <summary>
    /// 盤を表します。
    /// </summary>
    public class Slot
    {
        public const int XLength = 5;
        public const int YLength = 5;

        private SlotElement[,] slotElements = new SlotElement[XLength, YLength];

        /// <summary>
        /// マスを取得します。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public SlotElement GetElement(int x, int y) => slotElements[x, y];

        private Main model;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        public Slot(Main _model)
        {
            model = _model;

            for(int x = 0; x < XLength; x++) 
                for(int y = 0; y < YLength; ++y)
                    slotElements[x, y] = new SlotElement(model.ItemRepository);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; ++y)
                    slotElements[x, y].Initialize(x + y * 4);

            for (int y = 0; y < 3; ++y)
                slotElements[4, y].Initialize(16 + y);
        }

        /// <summary>
        /// 盤のすべてのマスの抽選をします。
        /// </summary>
        public void LotAll()
        {
            var xs = Enumerable.Range(0, XLength).OrderBy(_ => Random.Range(0, int.MaxValue)).ToArray();
            var ys = Enumerable.Range(0, YLength).OrderBy(_ => Random.Range(0, int.MaxValue)).ToArray();
            for (int x = 0; x < xs.Length; x++)
                for (int y = 0; y < ys.Length; ++y)
                    slotElements[xs[x], ys[y]].Lot();

            for (int x = 0; x < XLength; x++)
                for (int y = 0; y < YLength; ++y)
                    slotElements[x, y].CurrentItem?.OnLot(x, y, model);

            foreach (var character in model.Characters)
                character.CommitPendingCoinAmount();
        }
    }
}
