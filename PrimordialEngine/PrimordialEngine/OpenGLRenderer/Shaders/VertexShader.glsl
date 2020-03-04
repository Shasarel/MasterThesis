#version 400 core

in vec4 in_Position;
in vec3 in_Color;  
out vec3 pass_Color;
uniform mat4 MVP_Matrix;

void main(void) {
	gl_Position = MVP_Matrix * in_Position;
	pass_Color = in_Color;
}