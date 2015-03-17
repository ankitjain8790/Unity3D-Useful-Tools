//By Ankit Jain
Shader "Custom/OverlayEffect" {
Properties {
_TexA ("TexA", 2D) = "white" {}
_TexB ("TexB", 2D) = "white" {}
}

SubShader {
Tags { "RenderType"="Opaque" }
Pass {
Lighting Off Fog { Mode Off }
CGPROGRAM
#include "UnityCG.cginc"
#pragma vertex vert_img
#pragma fragment frag

sampler2D _TexA, _TexB,_TexC;

half4 frag( v2f_img i ) : COLOR {
half4 a = tex2D(_TexA, i.uv);
half4 b = tex2D(_TexB, i.uv);

half4 r = half4(0,0,0,1);
if (a.r > 0.5) { r.r = 1-(1-2*(a.r-0.5))*(1-b.r); }
else { r.r = (2*a.r)*b.r; }
if (a.g > 0.5) { r.g = 1-(1-2*(a.g-0.5))*(1-b.g); }
else { r.g = (2*a.g)*b.g; }
if (a.b > 0.5) { r.b = 1-(1-2*(a.b-0.5))*(1-b.b); }
else { r.b = (2*a.b)*b.b; }
r = lerp(a,r,b.a);
r = lerp(b,r,a.a);
return r;
}
ENDCG
}
}
}