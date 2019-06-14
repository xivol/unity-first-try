using UnityEngine;
using Xivol.Events;

namespace Xivol.StateMachine
{
    [CreateAssetMenu(menuName = "GameStates/GameState")]
    public class GameState : CoreAsset<GameState>
    {
        public GameStateEvent EnterEvent { get; private set; }
        public GameStateEvent LeaveEvent { get; private set; }

        public override void OnEnable()
        {
            base.OnEnable();
            EnterEvent = CreateInstance<GameStateEvent>();
            LeaveEvent = CreateInstance<GameStateEvent>();
        }

        public virtual void Enter()
        {
            Debug.Log("Game enters " + this.AssetName());
            EnterEvent.Raise(this);
        }

        public virtual void Leave()
        {
            Debug.Log("Game leaves " + this.AssetName());
            LeaveEvent.Raise(this);
        }

        public new static readonly string DefaultAssetsFolder = "GameStates";

        #region PathBuilder
        protected new class PathBuilder : CoreAsset<GameState>.PathBuilder
        {
            public PathBuilder(string subFolder = null) : base(DefaultAssetsFolder)
            {
                if (subFolder != null)
                    path += subFolder + "/";
            }
        }

        public new static string PathTo(string fileName = null)
        {
            return new PathBuilder().PathTo(fileName);
        }
        #endregion

        public static GameState LoadOrMake(string fileName)
        {
            return ScriptableObjectUtils.LoadOrMake<GameState>(PathTo(fileName));
        }
    }
}