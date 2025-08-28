using ComputeSharp;

#pragma warning disable CA1416 // Validate platform compatibility
namespace DivisionEngine
{
    [GeneratedComputeShaderDescriptor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    public readonly partial struct SDFShader(
        ReadWriteTexture2D<float4> texture,
        float time,
        float width,
        float height) : IComputeShader
    {
        float rectangle(float2 samplePosition, float2 halfSize)
        {
            float2 componentWiseEdgeDistance = Hlsl.Abs(samplePosition) - halfSize;
            float outsideDistance = Hlsl.Length(Hlsl.Max(componentWiseEdgeDistance, 0));
            float insideDistance = Hlsl.Min(Hlsl.Max(componentWiseEdgeDistance.X, componentWiseEdgeDistance.Y), 0);
            return outsideDistance + insideDistance;
        }

        public void Execute()
        {
            int2 pos = ThreadIds.XY;
            float2 relPos = (float2)pos / new float2(width, height) * 2.0f - 1.0f;
            float dist = rectangle(relPos, new float2(0.5f, 0.5f));
            texture[pos] = new float4(1, dist + time, time, 1);
        }
    }
}
#pragma warning restore CA1416 // Validate platform compatibility
