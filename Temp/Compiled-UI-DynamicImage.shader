// Compiled shader for PC, Mac & Linux Standalone

//////////////////////////////////////////////////////////////////////////
// 
// NOTE: This is *not* a valid shader file, the contents are provided just
// for information and for debugging purposes only.
// 
//////////////////////////////////////////////////////////////////////////
// Skipping shader variants that would not be included into build of current scene.

Shader "UI/DynamicImage" {
Properties {
[PerRendererData]  _MainTex ("Sprite Texture", 2D) = "white" { }
 _Color ("Tint", Color) = (1.000000,1.000000,1.000000,1.000000)
 _StencilComp ("Stencil Comparison", Float) = 8.000000
 _Stencil ("Stencil ID", Float) = 0.000000
 _StencilOp ("Stencil Operation", Float) = 0.000000
 _StencilWriteMask ("Stencil Write Mask", Float) = 255.000000
 _StencilReadMask ("Stencil Read Mask", Float) = 255.000000
 _ColorMask ("Color Mask", Float) = 15.000000
 _TimeScale ("TimeScale", Float) = 1.000000
 _VertexParmas ("VertexParmas", Vector) = (0.000000,0.000000,0.000000,0.000000)
 _TimeOffset ("TimeOffset", Float) = 0.000000
 _ScaleRatio ("ScaleRatio", Float) = 1.000000
[Toggle(UNITY_UI_ALPHACLIP)]  _UseUIAlphaClip ("Use Alpha Clip", Float) = 0.000000
}
SubShader { 
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="true" }
 Pass {
  Name "DEFAULT"
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="true" }
  ZTest [unity_GUIZTestMode]
  ZWrite Off
  Cull Off
  Stencil {
   Ref [_Stencil]
   ReadMask [_StencilReadMask]
   WriteMask [_StencilWriteMask]
   Comp [_StencilComp]
   Pass [_StencilOp]
  }
  Blend SrcAlpha OneMinusSrcAlpha
  ColorMask [_ColorMask]
  //////////////////////////////////
  //                              //
  //      Compiled programs       //
  //                              //
  //////////////////////////////////
//////////////////////////////////////////////////////
No keywords set in this variant.
-- Vertex shader for "metal":
// Compile errors generating this shader.

-- Fragment shader for "metal":
// Compile errors generating this shader.

-- Vertex shader for "glcore":
// Compile errors generating this shader.

-- Fragment shader for "glcore":
Shader Disassembly:
// All GLSL source is contained within the vertex program

//////////////////////////////////////////////////////
Keywords set in this variant: UNITY_UI_ALPHACLIP 
-- Vertex shader for "metal":
// Compile errors generating this shader.

-- Fragment shader for "metal":
// Compile errors generating this shader.

-- Vertex shader for "glcore":
// Compile errors generating this shader.

-- Fragment shader for "glcore":
Shader Disassembly:
// All GLSL source is contained within the vertex program

-- Vertex shader for "metal":
// Compile errors generating this shader.

-- Fragment shader for "metal":
// Compile errors generating this shader.

-- Vertex shader for "glcore":
// Compile errors generating this shader.

-- Fragment shader for "glcore":
Shader Disassembly:
// All GLSL source is contained within the vertex program

//////////////////////////////////////////////////////
Keywords set in this variant: UNITY_UI_CLIP_RECT UNITY_UI_ALPHACLIP 
-- Vertex shader for "metal":
// Compile errors generating this shader.

-- Fragment shader for "metal":
// Compile errors generating this shader.

-- Vertex shader for "glcore":
// Compile errors generating this shader.

-- Fragment shader for "glcore":
Shader Disassembly:
// All GLSL source is contained within the vertex program

 }
}
}