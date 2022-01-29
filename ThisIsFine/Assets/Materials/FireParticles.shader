Shader "Unlit/FireParticles"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "RenderQueue"="Transparent" "IgnoreProjector" = "True"}
        LOD 100
        Cull Off ZWrite Off

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"
            //#include "noise.cginc"
            

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float4 uv : TEXCOORD0;
                //float4 center : TEXCOORD1;
                float4 normal : NORMAL;
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                float4 normal : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                float4 vertex = UnityObjectToClipPos(v.vertex);
                //float4 center = UnityObjectToClipPos(v.center);
                //float4 distanceFromCenter = vertex - center;
                //float2 offset = lerp(0, 0.2, v.uv.zw);
                o.uv.xy = TRANSFORM_TEX(v.uv.xy, _MainTex);
                o.uv.zw = v.uv.zw;
                o.vertex = vertex;
                o.normal = v.normal;
                o.color = v.color;
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float dotP = dot(_WorldSpaceLightPos0.xyz, i.normal.xyz);
                dotP += 1;
                dotP /= 2;
                //return float4(dotP, dotP, dotP, 1);
                fixed4 col = i.color;// tex2D(_MainTex, i.uv.xy)* i.color;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                col.xyz *= lerp(0.45, 1, dotP);
                return col;
            }
            ENDCG
        }
    }
}
