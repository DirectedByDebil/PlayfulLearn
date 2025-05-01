using UnityEngine;
using System;

namespace Playables
{

    [Serializable]
    public struct Character
    {

        public Characters CharacterType;

        public Sprite Icon;

        public string Name;

        public RuntimeAnimatorController AnimatorController;
    }
}