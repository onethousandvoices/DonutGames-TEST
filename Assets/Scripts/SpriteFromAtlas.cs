using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace DonutLabTest
{
    public class SpriteFromAtlas : MonoBehaviour
    {
        [SerializeField]
        private SpriteAtlas _atlas;
        [SerializeField]
        private string _name;

        private void Start() => GetComponent<Image>().sprite = _atlas.GetSprite(_name);
    }
}