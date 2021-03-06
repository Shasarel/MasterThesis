#version 400 core

in vec4 in_Position;
in vec4 in_Color;  
in vec4 in_Normal;
out vec3 pass_Normal;
out vec3 pass_Color;
out vec3 fragment_Pos;
uniform mat4 MVP_Matrix;
uniform mat4 model_Matrix;

void main(void) {
	gl_Position = in_Position;
	pass_Color = in_Color.xyz;
}