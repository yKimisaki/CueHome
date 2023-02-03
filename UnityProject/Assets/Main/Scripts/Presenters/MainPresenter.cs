using CueHome.Models;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CueHome.Presenters
{
    /// <summary>
    /// UI 全体を表す Presenter です。
    /// </summary>
    public class MainPresenter : MonoBehaviour
    {
        private Main model;

        public SlotPresenter SlotPresenter;
        public CharactersDetailPresenter CharactersDetailPresenter;
        public ItemRewardPresenter ItemRewardPresenter;
        public RenewalPresenter RenewalPresenter;
        public PinchPresenter[] PinchPresenters;

        public DescriptionPresenter DescriptionPresenter;

        public GameObject Advice;
        public GameObject GameOver;

        public Button SpinButton;

        public TMP_Text AgeText;
        public TMP_Text PaymentAmountText;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        public void Initialize(Main _model)
        {
            model = _model;
            model.Initialize();

            SlotPresenter.Initialize(model.Slot, model.ItemRepository, (arg1, arg2) => DescriptionPresenter.Open(model, arg1, arg2), DescriptionPresenter.Close);
            CharactersDetailPresenter.Initialize(model, () =>
            {
                UpdatePinchs();

                if (model.IsGameOver)
                {
                    CharactersDetailPresenter.OpenOnGameOver();
                    GameOver.SetActive(true);
                }
            });
            ItemRewardPresenter.Initialize(_model);
            RenewalPresenter.Initialize(_model, () => PaymentAmountText.text = $"家賃 {model.CurrentPaymentAmount} コイン");

            AgeText.text = $"{model.Year}年{model.Month}月 {model.Week}週目";
            PaymentAmountText.text = $"家賃 {model.CurrentPaymentAmount} コイン";

            for(var i = 0; i < PinchPresenters.Length; i++)
                PinchPresenters[i].Initialize(model.Characters);
            UpdatePinchs();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Spin()
        {

            bool isReturn = false;
            if (model.IsRequiredPayment)
            {
                CharactersDetailPresenter.OpenOnPayment();
                ItemRewardPresenter.Open();
                isReturn = true;
            }
            if (model.IsRequiredRenewal)
            {
                RenewalPresenter.Open();
                isReturn = true;
            }

            if (isReturn)
                return;

            model.Spin();

            SlotPresenter.UpdateItemImage();

            AgeText.text = $"{model.Year}年{model.Month}月 {model.Week}週目";
            PaymentAmountText.text = $"家賃 {model.CurrentPaymentAmount} コイン";
            
            UpdatePinchs();
        }

        private void UpdatePinchs()
        {
            var i = 0;
            foreach (var x in model.Characters
                .Where(x => !x.IsRetired)
                .OrderBy(x => x.CoinAmount)
                .Take(PinchPresenters.Length))
            {
                PinchPresenters[i].UpdatePresenter(x.Name);
                i++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Retry()
        {
            SceneManager.LoadScene("MainScene");
        }

        /// <summary>
        /// 
        /// </summary>
        public void OpenAdvice()
        {
            Advice.SetActive(true);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CloseAdvice()
        {
            Advice.SetActive(false);
        }
    }
}
