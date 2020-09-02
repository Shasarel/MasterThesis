struct VS_IN
{
	float4 pos : POSITION;
	float4 col : COLOR;
	float4 normal : NORMAL;
};
struct PS_IN
{
	float4 pos : SV_POSITION;
	float4 pos2 : POSITION;
	float4 col : COLOR;
	float4 normal : NORMAL;
};
float4x4 worldViewProj;

cbuffer myBuffer: register(b1)
{
    float4x4 modelMatrix;
}

cbuffer myBuffer2: register(b2)
{
    float4x4 modelMatrixInv;
}

PS_IN VS(VS_IN input)
{
	PS_IN output = (PS_IN) 0;
	
	output.pos = mul(input.pos, worldViewProj);
	output.pos2 = mul(modelMatrix,input.pos);
	output.col = input.col;
	output.normal = mul(modelMatrixInv,input.normal);
	
	return output;
}

float4 PS(PS_IN input) : SV_Target
{
	float specularStrength = 1;
	float diffuseStrength = 0.7;
	float ambientStrenght = 0.3;
	float4 lightPos = {500.0f, 700.0f, -1000.0f, 1.0f};
	float4 lightDir = normalize(lightPos - input.pos2);
	float4 viewPos = {0.0f, 0.0f, 0.0f, 1.0f};
	float4 viewDir = normalize(viewPos - input.pos2);

	float4 lightColor = {1.0f, 1.0f, 1.0f, 1.0f};
	float4 reflectDir = normalize(reflect(-lightDir, input.normal));
	float diff = max(dot(input.normal, lightDir), 0.0f);
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), 1024);

	float4 specular = specularStrength * spec * lightColor;
	float4 ambient = ambientStrenght * lightColor;
	float4 diffuse = diffuseStrength * diff * lightColor;


	float4 result = (ambient + diffuse + specular) * input.col;
	return result;
}
