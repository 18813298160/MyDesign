// Compiled shader for PC, Mac & Linux Standalone

//////////////////////////////////////////////////////////////////////////
// 
// NOTE: This is *not* a valid shader file, the contents are provided just
// for information and for debugging purposes only.
// 
//////////////////////////////////////////////////////////////////////////
// Skipping shader variants that would not be included into build of current scene.

Shader "Custom/textShader" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" { }
 _MainTint ("Diffuse Tint", Color) = (1.000000,1.000000,1.000000,1.000000)
 _SpecularColor ("Specular Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 _SpecPower ("Specular Power", Range(0.000000,30.000000)) = 2.000000
 _Specular ("Specular Amount", Range(0.000000,1.000000)) = 0.500000
 _AnisoDir ("Anisotropic Direction", 2D) = "" { }
 _AnisoOffset ("Anisotropic Offset", Range(-1.000000,1.000000)) = -0.200000
}
SubShader { 
 LOD 200
 Tags { "RenderType"="Opaque" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="FORWARDBASE" "SHADOWSUPPORT"="true" "RenderType"="Opaque" }
  //////////////////////////////////
  //                              //
  //      Compiled programs       //
  //                              //
  //////////////////////////////////
//////////////////////////////////////////////////////
Keywords set in this variant: DIRECTIONAL 
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
Keywords set in this variant: DIRECTIONAL SHADOWS_SCREEN 
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
Keywords set in this variant: DIRECTIONAL VERTEXLIGHT_ON 
-- Vertex shader for "metal":
// Compile errors generating this shader.

-- Fragment shader for "metal":
// No shader variant for this keyword set. The closest match will be used instead.

-- Vertex shader for "glcore":
// Compile errors generating this shader.

-- Fragment shader for "metal":
// No shader variant for this keyword set. The closest match will be used instead.

//////////////////////////////////////////////////////
Keywords set in this variant: DIRECTIONAL SHADOWS_SCREEN VERTEXLIGHT_ON 
-- Vertex shader for "metal":
// Compile errors generating this shader.

-- Fragment shader for "metal":
// No shader variant for this keyword set. The closest match will be used instead.

-- Vertex shader for "glcore":
// Compile errors generating this shader.

-- Fragment shader for "metal":
// No shader variant for this keyword set. The closest match will be used instead.

 }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="FORWARDADD" "RenderType"="Opaque" }
  ZWrite Off
  Blend One One
  //////////////////////////////////
  //                              //
  //      Compiled programs       //
  //                              //
  //////////////////////////////////
//////////////////////////////////////////////////////
Keywords set in this variant: POINT 
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
Keywords set in this variant: POINT_COOKIE 
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

 }
}
Fallback "Diffuse"
}