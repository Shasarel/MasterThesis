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
	gl_Position = MVP_Matrix * in_Position;
	fragment_Pos = (model_Matrix * in_Position).xyz;
	pass_Color = in_Color.xyz;
	pass_Normal = mat3(transpose(inverse(model_Matrix))) * in_Normal.xyz;

}