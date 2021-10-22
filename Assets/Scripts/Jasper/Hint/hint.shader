Shader "Hidden/hint"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _bwBlend("Black & White blend", Range(0, 1)) = 0
        _HighlightColor("highlight color", Color) = (0,0,0,0)
        _TargetColor("target color", Color) = (0,0,0,0)
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            uniform float _bwBlend;
            uniform fixed4 _HighlightColor;
            uniform fixed4 _TargetColor;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 finalColor;
                if (abs(col.x - _TargetColor.x) < 0.01 && abs(col.y - _TargetColor.y) < 0.01 && abs(col.z - _TargetColor.z) < 0.01)
                {
                    if (_bwBlend < 1.0)
                    {
                        finalColor.rgb = lerp(float3(0.3, 0.3, 0.3), float3(0.6, 0.2, 0.1), _bwBlend);
                        finalColor.w = 1;
                    }
                    else
                    {
                        finalColor = _HighlightColor;
                    }
                }
                else
                {
                    float intensity = col.x * 0.33 + col.y * 0.33 + col.z * 0.33;
                    finalColor = fixed4(intensity, intensity, intensity, 1);
                    if (_bwBlend < 1.0)
                    {
                        finalColor.rgb = lerp(col.rgb, finalColor.rgb, _bwBlend);
                    }
                }
                return finalColor;
            }
            ENDCG
        }
    }
}
