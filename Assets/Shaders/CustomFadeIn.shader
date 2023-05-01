Shader "Custom/FadeIn"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _FadeTime ("Fade Time", Range(0.0, 10.0)) = 1.0
        _FadeSpeed ("Fade Speed", Range(0.0, 10.0)) = 1.0
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}

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

            sampler2D _MainTex;
            float4 _Color;
            float _FadeTime;
            float _FadeSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // calculate alpha value based on time
                float alpha = 1.0 - smoothstep(0.0, _FadeTime, _Time.y * _FadeSpeed);

                // set color with alpha value
                fixed4 col = _Color;
                col.a *= alpha;

                // sample texture and apply color and alpha
                fixed4 tex = tex2D(_MainTex, i.uv);
                return tex * col;
            }
            ENDCG
        }
    }
}
