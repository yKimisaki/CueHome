using CueHome.Models;
using CueHome.Presenters;
using UnityEngine;

namespace CueHome
{
    /// <summary>
    /// ���C���Q�[����\�� Scene �ł��B
    /// </summary>
    public class MainScene : MonoBehaviour
    {
        private Main model = new Main();

        public MainPresenter MainPresenter;

        /// <summary>
        /// 
        /// </summary>
        public void Awake()
        {
            MainPresenter.Initialize(model);
        }
    }
}
