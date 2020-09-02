#version 400 core
in vec3 pass_Color;
in vec3 pass_Normal;
in vec3 fragment_Pos;
out vec4 out_Color;
uniform vec3 viewPos;

void main(void) {
	float specularStrength = 1;
	float diffuseStrength = 0.7;
	float ambientStrenght = 0.3;

    vec3 lightColor = vec3(1.0, 1.0, 1.0);
	vec3 lightPos = vec3(500,700,-1000);
	vec3 lightDir = normalize(lightPos - fragment_Pos);

	vec3 ambient = ambientStrenght * lightColor;

	float diff = max(dot(pass_Normal, lightDir), 0.0);
	vec3 diffuse = diffuseStrength * diff * lightColor;

	vec3 viewDir = normalize(viewPos - fragment_Pos);
	vec3 reflectDir = normalize(reflect(-lightDir, pass_Normal)); 
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), 1024);
	vec3 specular = specularStrength * spec * lightColor; 

	vec3 result = (ambient + diffuse + specular) * pass_Color;
	out_Color = vec4(result, 1.0);
}