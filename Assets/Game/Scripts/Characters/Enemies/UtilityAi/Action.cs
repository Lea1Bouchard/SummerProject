using UnityEngine;

namespace UtilityAI.Core
{
    public abstract class Action : ScriptableObject
    {
        #region variables
        public string Name;
        private float _score;
        public int Importance;
        #endregion
        //Sets a score to compare it's desireability
        public float score
        {
            get { return _score; }
            set
            {
                this._score = Mathf.Clamp01(value); //Score value can only be between 0 and 1
            }
        }

        public Consideration[] considerations;

        public virtual void Awake()
        {
            score = 0;
        }
        public abstract void Execute(EnemyController enemy);
    }
}
