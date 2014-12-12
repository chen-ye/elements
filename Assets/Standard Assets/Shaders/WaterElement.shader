Shader "Custom/Water"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    }
 
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType"      = "Opaque" 
        }
 
        LOD 200
        
        Pass
        {
            ZWrite On
            ColorMask 0
        }
 
        UsePass "Transparent/Diffuse/FORWARD"
    }
    FallBack "Diffuse"
}