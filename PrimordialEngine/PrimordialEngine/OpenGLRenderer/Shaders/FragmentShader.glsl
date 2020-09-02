#version 400 core
in vec3 pass_Color;
in vec3 pass_Normal;
in vec3 fragment_Pos;
out vec4 out_Color;
uniform vec3 viewPos;

void main(void) {
	out_Color = vec4(pass_Color,1.0f);
}