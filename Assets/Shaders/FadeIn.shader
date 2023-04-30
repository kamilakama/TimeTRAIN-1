Shader "Unlit/FadeIn"
  {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Alpha ("Alpha", Range(0,1)) = 1
        _Radius ("Radius", Range(0,1)) = 1
    }
 
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
 
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _Color;
            float _Alpha;
            float _Radius;
 
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target {
                // Calculate the distance of the current pixel from the center of the sphere
                float dist = length(i.uv - 0.5);
 
                // Calculate the alpha value based on the distance and the radius of the sphere
                float alpha = smoothstep(0, _Radius, dist);
 
                // Multiply the alpha value by the custom "_Alpha" property of the material
                alpha *= _Alpha;
 
                // Multiply the texture color by the custom "_Color" property of the material
                fixed4 tex = tex2D(_MainTex, i.uv) * _Color;
 
                // Combine the texture color with the opaque black color, using the calculated alpha value
                fixed4 col = lerp(tex, fixed4(0,0,0,1), alpha);
 
                // Return the final color
                return col;
            }
            ENDCG
        }
    }
}