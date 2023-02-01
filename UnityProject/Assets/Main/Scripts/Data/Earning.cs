using System.Collections.Generic;

namespace CueHome.Data
{
    /// <summary>
    /// 声優の稼ぎです。
    /// </summary>
    public  class Earning
    {
        private int week1;
        private int week2;
        private int week3;
        private int week4;

        private Earning(int _week1, int _week2, int _week3, int _week4)
        {
            week1 = _week1;
            week2 = _week2;
            week3 = _week3;
            week4 = _week4;
        }

        /// <summary>
        /// 現在の声優の稼ぎを取得します。
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="week"></param>
        /// <returns></returns>
        public int Get(int year, int month, int week) => week switch
        {
            1 => week1,
            2 => week2,
            3 => week3,
            4 => week4,
            _ => 0,
        };

        /// <summary>
        /// 声優の稼ぎを名前で引けるようにします。
        /// </summary>
        public static readonly IReadOnlyDictionary<string, Earning> ByName = new Dictionary<string, Earning>
        {
            {Name.六石陽菜, new Earning(5, 5, 5, 5)},
            {Name.月居ほのか, new Earning(0, 10, 0, 10)},
            {Name.鷹取舞花, new Earning(2, 4, 6, 8)},
            {Name.鹿野志穂, new Earning(10, 0, 10, 0)},
            {Name.天童悠希, new Earning(5, 5, 5, 5)},
            {Name.恵庭あいり, new Earning(5, 5, 5, 5)},
            {Name.赤川千紗, new Earning(0, 0, 0, 20)},
            {Name.九条柚葉, new Earning(2, 4, 6, 8)},
            {Name.夜峰美晴, new Earning(5, 5, 5, 5)},
            {Name.神室絢, new Earning(0, 10, 0, 10)},
            {Name.日名倉莉子, new Earning(0, 2, 8, 10)},
            {Name.宮路まほろ, new Earning(5, 5, 5, 5)},
            {Name.丸山利恵, new Earning(0, 0, 0, 20)},
            {Name.明神凛音, new Earning(0, 2, 8, 10)},
            {Name.宇津木聡里, new Earning(5, 5, 5, 5)},
            {Name.遠見鳴, new Earning(2, 4, 6, 8)},
        };
    }
}
