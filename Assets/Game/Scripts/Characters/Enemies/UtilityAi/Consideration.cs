using UnityEngine;

namespace UtilityAI.Core
{
    //Class is used to help determine the best action to take 
    public abstract class Consideration : ScriptableObject
    {
        #region variables
        public string Name;
        private float _score;
        #endregion

        public float score
        {
            get { return _score; }
            set
            {
                this._score = Mathf.Clamp01(value); //Score value can only be between 0 and 1
            }
        }

        public virtual void Awake()
        {
            score = 0;
        }

        public abstract float ScoreConsideration(EnemyController enemy);
    }
}

