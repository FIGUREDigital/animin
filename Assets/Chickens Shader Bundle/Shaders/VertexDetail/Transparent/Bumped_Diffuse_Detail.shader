Shader "Chickenlord/Detail/Vertex Colored/Transparent/Bumped Diffuse" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_Detail ("Detail Base (RGB) Gloss (A)", 2D) = "white" {}
	_DetailBump ("Detail Normalmap", 2D) = "bump" {}
	_VertexMultiplier("Vertex Color Multiplier", Float) = 1
	_VertexAlphaFalloff("Vertex Alpha Falloff", Float) = 1
}
SubShader { 
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 400
	
CGPROGRAM
#pragma surface surf Lambert nodirlightmap vertex:vert alpha
#pragma target 2.0

sampler2D _MainTex;
sampler2D _BumpMap;
sampler2D _Detail;
sampler2D _DetailBump;
fixed4 _Color;
half _VertexMultiplier;
half _VertexAlphaFalloff;

struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
	float2 uv_Detail;
	float4 vertexLighting;
};

 void vert (inout appdata_full v, out Input data) {
	  data.vertexLighting = float4(v.color.rgb*_VertexMultiplier,v.color.a);
    }

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 td = tex2D(_Detail,IN.uv_Detail);
	td= min(td*1.8,0.9)+0.1;
	tex *= td;
	o.Albedo = tex.rgb * _Color.rgb;
	o.Alpha = tex.a * _Color.a;
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	o.Normal = normalize(o.Normal+UnpackNormal(tex2D(_DetailBump, IN.uv_Detail)));
	o.Emission += IN.vertexLighting.rgb*o.Albedo;
	o.Alpha *= pow(1-pow(1-IN.vertexLighting.a ,_VertexAlphaFalloff),_VertexAlphaFalloff);
}
ENDCG
}

FallBack "Specular"
}
