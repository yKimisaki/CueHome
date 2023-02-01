using CueHome.Data;
using CueHome.Models;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CueHome.Presenters
{
    /// <summary>
    /// 残高が少ない人を表す Presenter です。
    /// </summary>
    public class PinchPresenter : MonoBehaviour
    {
        public Image IconImage;
        public TMP_Text CoinAmountText;

        private IReadOnlyDictionary<string, Character> charactersByName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="characters"></param>
        public void Initialize(IReadOnlyList<Character> characters)
        {
            charactersByName = characters.ToDictionary(x => x.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void UpdatePresenter(string name)
        {
            IconImage.sprite = Resources.Load<Sprite>(IconPath.CharacterByName[name]);
            CoinAmountText.text = charactersByName[name].CoinAmount.ToString();
        }
    }
}
