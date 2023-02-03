using CueHome.Data;
using CueHome.Models;
using TMPro;
using UnityEngine;

namespace CueHome.Presenters
{
    public class DescriptionPresenter : MonoBehaviour
    {
        public TMP_Text DescriptionText;

        public void Open(Main model, Item item, RectTransform transform)
        {
            gameObject.SetActive(true);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, transform.anchoredPosition.y + 200f);

            if (item.Character is not null)
                DescriptionText.text = $"{item.Name}\n稼ぎ(第1-2-3-4週) : {item.Character.GetEarnings(1, 1, 1)}-{item.Character.GetEarnings(1, 1, 2)}-{item.Character.GetEarnings(1, 1, 3)}-{item.Character.GetEarnings(1, 1, 4)}";
            else if (item.Effect is not null)
            {
                DescriptionText.text = $"{item.Name}\n{item.Effect.GetDescription(item.Name, model)}";
                if (item.Name == Name.由良桐香)
                    DescriptionText.text += $"\n3年目以降は出現しない。";
            }
            else
                gameObject.SetActive(false);

        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
