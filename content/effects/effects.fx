struct VertexShaderInput
{
    float4 Position	: SV_POSITION;
    float4 Colour   : COLOR0;
};

struct VertexToPixel
{
	float4 Position : SV_POSITION;
	float4 Colour   : COLOR0;
};

struct PixelToFrame
{
	float4 Colour   : COLOR0;
};

VertexToPixel ColouredVS(VertexShaderInput input)
{
	VertexToPixel output;

	output.Position = input.Position;
	output.Colour = input.Colour;

	return output;
}

PixelToFrame ColouredPS(VertexToPixel input)
{
	PixelToFrame output;

	output.Colour = input.Colour;

	return output;
}

technique Coloured
{
	pass Pass0
	{
		VertexShader = compile vs_4_0 ColouredVS();
		PixelShader = compile ps_4_0 ColouredPS();
	}
}
