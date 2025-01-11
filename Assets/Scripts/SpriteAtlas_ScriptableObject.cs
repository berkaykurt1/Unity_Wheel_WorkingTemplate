using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace GameDeveloper_Case.SpriteAtlas_SO
{
    [CreateAssetMenu(menuName = "Create Sprite Atlas ScriptableObject",fileName = "Sprite Atlas ScriptableObject",order = 4)]
    public class SpriteAtlas_ScriptableObject : ScriptableObject
    {
        [SerializeField] private SpriteAtlas[] spriteAtlas;
        public SpriteAtlas[] SpriteAtlas { get { return spriteAtlas; } }
    }

}
