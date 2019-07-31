﻿Shader "Photoshop/diejia"
{    Properties
{
_MainTex("Texture", 2D) = "white" {}
_Alpha("Aplha",Range(0,2))=1
}
SubShader
{
	//Tags { "RenderType"="Opaque" }
		 Tags{ "Queue" = "Transparent" } // 不使用这个会导致出现背景透明部分不显示的情况

		 LOD 100

		 GrabPass{} // 绘制当前的屏幕图像到 _GrabTexture 里

		 Pass
		 {
			 CGPROGRAM
				 #pragma vertex vert
			     #pragma fragment frag
			 // make fog work
			 #pragma multi_compile_fog

			  #include "UnityCG.cginc"

			 struct appdata
		   {
				 float4 vertex : POSITION;
				  float2 uv : TEXCOORD0;
			  };

			 struct v2f
			  {
				  float2 uv : TEXCOORD0;
				  UNITY_FOG_COORDS(1)
					 float4 vertex : SV_POSITION;
				 half4 screenuv : TEXCOORD2; // 当前屏幕图像的UV坐标
			  };

			  sampler2D _MainTex;
		  float4 _MainTex_ST;
		  float _Alpha;

			  sampler2D _GrabTexture; // 当前屏幕图像

			 v2f vert(appdata v)
			  {
				  v2f o;
				  o.vertex = UnityObjectToClipPos(v.vertex);
			  o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				  UNITY_TRANSFER_FOG(o, o.vertex);
				  o.screenuv = ComputeGrabScreenPos(o.vertex); // 获取当前屏幕图像的UV坐标
				  return o;
			  }

			 fixed4 MixAdd(fixed4 col1, fixed4 col2,float alpha)
			 {
				 fixed r, g, b;
				 r = (col1.a)*col1.r*alpha +col2.r;
				 g = (col1.a)*col1.g*alpha +col2.g;
				 b = (col1.a)*col1.b*alpha +col2.b;
				 return fixed4(r, g, b, 1.0f);
			 }

			 fixed4 frag(v2f i) : SV_Target
			  {

					  fixed4 colour = tex2D(_GrabTexture, float2(i.screenuv.x, i.screenuv.y)); // 获取当前屏幕图像的颜色

					  // sample the texture
					  fixed4 col = tex2D(_MainTex, i.uv);

					  //fixed4 endd = col + colour; // ADD 混合模式
					  //正统的ADD
					  fixed4 add = MixAdd(col,colour,_Alpha);

					  // apply fog
					   UNITY_APPLY_FOG(i.fogCoord, add);
				  return add;
			   }
		   ENDCG
		   }
}
}