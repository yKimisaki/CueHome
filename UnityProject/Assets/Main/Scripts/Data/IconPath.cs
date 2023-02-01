using System.Collections.Generic;

namespace CueHome.Data
{
    /// <summary>
    /// アイコン画像のリソースを管理します。
    /// </summary>
    public static class IconPath
    {
        /// <summary>
        /// 声優の画像を名前で引けるようにします。
        /// </summary>
        public static readonly IReadOnlyDictionary<string, string> CharacterByName = new Dictionary<string, string>
        {
            {Name.六石陽菜, "Textures/haruna"},
            {Name.月居ほのか, "Textures/honoka"},
            {Name.鷹取舞花, "Textures/maika"},
            {Name.鹿野志穂, "Textures/shiho"},
            {Name.天童悠希, "Textures/yuki"},
            {Name.恵庭あいり, "Textures/airi"},
            {Name.赤川千紗, "Textures/chisa"},
            {Name.九条柚葉, "Textures/yuzuha"},
            {Name.夜峰美晴, "Textures/miharu"},
            {Name.神室絢, "Textures/aya"},
            {Name.日名倉莉子, "Textures/riko"},
            {Name.宮路まほろ, "Textures/mahoro"},
            {Name.丸山利恵, "Textures/rie"},
            {Name.明神凛音, "Textures/rinne"},
            {Name.宇津木聡里, "Textures/satori"},
            {Name.遠見鳴, "Textures/mei"},
        };

        /// <summary>
        /// アイテムの画像を名前で引けるようにします。
        /// </summary>
        public static readonly IReadOnlyDictionary<string, string> ItemByName = new Dictionary<string, string>
        {
            {Name.鳳真咲, "Textures/masaki"},
            {Name.由良桐香, "Textures/kirika"},
            {Name.五十鈴りお, "Textures/rio"},

            {Name.あざらし, "Textures/azarasi"},
            {Name.サイコロ, "Textures/dice"},
            {Name.ふで, "Textures/fude"},
            {Name.金フォト, "Textures/gold_photo"},
            {Name.はちみつジンジャーティー, "Textures/honey_ginger_tea"},
            {Name.はちみつヨーグルト, "Textures/honey_yogurt"},
            {Name.いなりずし, "Textures/inarizusi"},
            {Name.亀井さん, "Textures/kameisan"},
            {Name.ケチャップ, "Textures/ketchup"},
            {Name.黒猫, "Textures/kuroneko"},
            {Name.LPゼリー, "Textures/lp_gerry"},
            {Name.PCパーツ, "Textures/pc_parts"},
            {Name.ラジオマイク, "Textures/radio_mic"},
            {Name.虹フォト, "Textures/rainbow_photo"},
            {Name.リセットハンマー, "Textures/reset_hammer"},
            {Name.晩酌, "Textures/sake"},
            {Name.ステッキ, "Textures/stick"},
            {Name.ヤクロト, "Textures/yakuroto"},
        };
    }
}
