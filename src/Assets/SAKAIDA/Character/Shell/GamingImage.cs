using UnityEngine;
using UnityEngine.UI;

public class GamingImage : MonoBehaviour
{
    /// Imageをゲーミングカラーに変更する

    [SerializeField] SpriteRenderer spriteRender;
    [SerializeField] ParticleSystem particle;
    [SerializeField] float Speed = 0.2f;

    /// <summary>
    /// 色相を回転させる（0～1で一周）
    /// </summary>
    public Color RotateHue(Color color, float deltaHue)
    {
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);

        h += deltaHue;
        if (h > 1f) h -= 1f;
        if (h < 0f) h += 1f;

        return Color.HSVToRGB(h, s, v);
    }

    void Update()
    {
        if(spriteRender)
            spriteRender.color = RotateHue(spriteRender.color, Time.deltaTime * Speed);
        if(particle)
            particle.startColor = RotateHue(spriteRender.color, Time.deltaTime * Speed);
    }
}