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
	return input.col;
}
