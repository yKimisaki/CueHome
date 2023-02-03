using CueHome.Data;
using System.Collections.Generic;
using System.Linq;

namespace CueHome.Models
{
    /// <summary>
    /// ゲームのロジックを表します。
    /// </summary>
    public class Main
    {
        public ItemRepository ItemRepository { get; }
        public Slot Slot { get; }

        /// <summary>
        /// キャラクターマスタ
        /// </summary>
        public IReadOnlyList<Character> Characters { get; } = new[]
        {
            new Character(Name.六石陽菜),
            new Character(Name.月居ほのか),
            new Character(Name.鷹取舞花),
            new Character(Name.鹿野志穂),
            new Character(Name.天童悠希),
            new Character(Name.恵庭あいり),
            new Character(Name.赤川千紗),
            new Character(Name.九条柚葉),
            new Character(Name.夜峰美晴),
            new Character(Name.神室絢),
            new Character(Name.日名倉莉子),
            new Character(Name.宮路まほろ),
            new Character(Name.丸山利恵),
            new Character(Name.明神凛音),
            new Character(Name.宇津木聡里),
            new Character(Name.遠見鳴),
        };

        /// <summary>
        /// アイテムマスタ
        /// </summary>
        public IReadOnlyList<Item> Items { get; } = new[]
        {
            new Item(Name.鳳真咲, ItemEffect.Get全キャラにコイン追加(5, false),0, false, false),
            new Item(Name.由良桐香, ItemEffect.Get周囲のキャラの稼ぎを倍にする(_ => 2, false), 0, false, false),
            new Item(Name.五十鈴りお, ItemEffect.Get周囲のキャラにコイン追加(5, false), 0, false, false),
            new Item(Name.思い出のオルゴール, ItemEffect.Get周囲のキャラの稼ぎを倍にする(x => x.Year, false),0,  true, false),

            new Item(Name.あざらし, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.鹿野志穂), 2, true, true),
            new Item(Name.サイコロ, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.九条柚葉, Name.明神凛音), 2, true, true),
            new Item(Name.ふで, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.鹿野志穂), 2, true, true),
            new Item(Name.金フォト, ItemEffect.Get周囲のキャラにコイン追加(75, true), 1, true, true),
            new Item(Name.はちみつジンジャーティー, ItemEffect.Get周囲のキャラにコイン追加(5, false),1,  true, true),
            new Item(Name.はちみつヨーグルト, ItemEffect.Get周囲のキャラにコイン追加(50, true), 1, true, true),
            new Item(Name.いなりずし, ItemEffect.Get周囲の特定のキャラにコイン追加(50, true, Name.天童悠希), 3, true, true),
            new Item(Name.亀井さん, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.六石陽菜), 2, true, true),
            new Item(Name.ケチャップ, ItemEffect.Get周囲の特定のキャラにコイン追加(50, true, Name.赤川千紗, Name.丸山利恵), 3, true, true),
            new Item(Name.黒猫, ItemEffect.Get周囲の特定のキャラにコイン追加(50, true, Name.六石陽菜, Name.神室絢, Name.明神凛音), 3, true, true),
            new Item(Name.LPゼリー, ItemEffect.Get全キャラにコイン追加(20, true), 1,  true, true),
            new Item(Name.RPのど飴, ItemEffect.Get全キャラにコイン追加(3, false), 1,  true, true),
            new Item(Name.PCパーツ, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.遠見鳴), 2, true, true),
            new Item(Name.ラジオマイク, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.夜峰美晴, Name.神室絢, Name.日名倉莉子, Name.宮路まほろ), 2, true, true),
            new Item(Name.虹フォト, ItemEffect.Get周囲のキャラにコイン追加(120, true), 1, true, true),
            new Item(Name.リセットハンマー, ItemEffect.Get周囲のアイテムを破壊(true), 1, true, true),
            new Item(Name.晩酌, ItemEffect.Get周囲の特定のキャラにコイン追加(50, true,  Name.夜峰美晴, Name.日名倉莉子, Name.宮路まほろ), 3, true, true),
            new Item(Name.ステッキ, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false,  Name.恵庭あいり, Name.赤川千紗), 2, true, true),
            new Item(Name.ヤクロト, ItemEffect.Get周囲の特定のキャラにコイン追加(50, true,  Name.丸山利恵), 3, true, true),
            new Item(Name.お肉, ItemEffect.Get周囲の特定のキャラにコイン追加(50, true,  Name.六石陽菜, Name.宇津木聡里), 3, true, true),
            new Item(Name.ラーメン, ItemEffect.Get周囲の特定のキャラにコイン追加(50, true,  Name.鷹取舞花, Name.宮路まほろ), 3, true, true),
            new Item(Name.入浴剤, ItemEffect.Get周囲の特定のキャラにコイン追加(50, true, Name.月居ほのか, Name.天童悠希, Name.丸山利恵), 3, true, true),
            new Item(Name.お弁当, ItemEffect.Get周囲の特定のキャラにコイン追加(50, true, Name.六石陽菜, Name.天童悠希, Name.神室絢), 3, true, true),
            new Item(Name.英英辞書, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.九条柚葉, Name.遠見鳴), 2, true, true),
            new Item(Name.喫茶店の制服, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.赤川千紗, Name.丸山利恵), 2, true, true),
            new Item(Name.お守り, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.恵庭あいり), 2, true, true),
            new Item(Name.ゲーム機, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.鷹取舞花, Name.遠見鳴), 2, true, true),
            new Item(Name.スニーカー, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.鷹取舞花, Name.月居ほのか, Name.日名倉莉子), 2, true, true),
            new Item(Name.カメラ, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.明神凛音), 2, true, true),
            new Item(Name.おもちゃ, ItemEffect.Get周囲の特定のキャラにコイン追加(50, true, Name.天童悠希, Name.恵庭あいり, Name.明神凛音), 2, true, true),
            new Item(Name.小説, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.宇津木聡里), 2, true, true),
            new Item(Name.めがね, ItemEffect.Get周囲の特定のキャラにコイン追加(5, false, Name.宮路まほろ, Name.宇津木聡里),2,  true, true),
        };

        /// <summary>
        /// 現在の経過年数
        /// </summary>
        public int Year { get; private set; } = 1;
        /// <summary>
        /// 現在の経過月
        /// </summary>
        public int Month { get; private set; } = 1;
        /// <summary>
        /// 現在の経過週
        /// </summary>
        public int Week { get; private set; } = 1;

        /// <summary>
        /// 家賃の支払い週かどうか
        /// </summary>
        public bool IsRequiredPayment { get; private set; } = false;
        /// <summary>
        /// 家賃の更新週かどうか
        /// </summary>
        public bool IsRequiredRenewal { get; private set; } = false;
        /// <summary>
        /// もう家に誰もいないかどうか
        /// </summary>
        public bool IsGameOver { get; private set; } = false;

        /// <summary>
        /// 現在の家賃
        /// </summary>
        public int CurrentPaymentAmount { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Main()
        {
            ItemRepository = new ItemRepository(this);
            Slot = new Slot(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            Slot.Initialize();
            Renewal();
        }

        /// <summary>
        /// 盤を抽選します。
        /// </summary>
        public void Spin()
        {
            if (IsRequiredPayment)
                return;

            ItemRepository.ResetBox();
            Slot.LotAll();

            Week++;
            if (Week == 5)
            {
                Week = 1;
                Month++;
                IsRequiredPayment = true;

                if (Month == 13)
                {
                    Month = 1;
                    Year++;

                    IsRequiredRenewal = true;
                }
            }
        }

        /// <summary>
        /// アイテムを取得します。
        /// </summary>
        /// <param name="item"></param>
        public void GetItem(Item item)
        {
            ItemRepository.AddItem(item);
        }

        /// <summary>
        /// 家賃の支払いをします。
        /// </summary>
        public void Pay()
        {
            foreach (var character in Characters)
                if (!character.IsRetired)
                    character.TryPayCoinAmount(this, true);
            IsRequiredPayment = false;

            if (Characters.All(x => x.IsRetired))
                IsGameOver = true;
        }

        /// <summary>
        /// 家賃の更新をhします。
        /// </summary>
        public void Renewal()
        {
            CurrentPaymentAmount = GetPaymentAmount(Year);
            IsRequiredRenewal = false;
        }

        /// <summary>
        /// 経年に応じた家賃を取得します。
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public int GetPaymentAmount(int year) => year switch
        {
            0 => 0,
            1 => 50,
            2 => 75,
            3 => 100,
            4 => 150,
            5 => 200,
            6 => 250,
            7 => 300,
            8 => 350,
            9 => 400,
            10 => 500,
            11 => 600,
            12 => 700,
            13 => 800,
            14 => 900,
            _ => 1000,
        };
    }
}
