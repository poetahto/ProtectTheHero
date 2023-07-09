using System;
using Cysharp.Threading.Tasks;
using FSM;
using UnityEngine.SceneManagement;
using Task = System.Threading.Tasks.Task;

namespace DefaultNamespace
{
    [Serializable]
    public class HeroDeadState : StateBase
    {
        public HeroDeadState() : base(false)
        {
        }

        public override void OnEnter()
        {
            DeathTask().Forget();
        }

        private async UniTaskVoid DeathTask()
        {
            // todo: play death animation
            await Task.Delay(1000);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
