# Division Engine

Division Engine is an SDF-based game engine written entirely in C#. Utilizing Avalonia UI for the interface and Silk.Net for native rendering, Division Engine has a complete build pipeline completely written in C#.

The render pipeline is built using an OpenGL backend with HLSL shaders written in C# using ComputeSharp.

Picture this:
- SDF-based rendering
- GPU compute acceleration in C#
- Open source
- Simple tooling

## What Are SDFs?

*Signed Distance Fields* are spatial fields that store information represented as a grid sampling of the closest distance to the surface of an object defined as a polygonal model. Usually, the convention of using negative values inside the object and positive values outside the object is applied. Signed distance fields are important in computer graphics and related fields. Often, they are used for collision detection in cloth animation, soft-body physics effects, malleable geometry, volumetric effects, and fluid simulation.
(https://developer.nvidia.com/gpugems/gpugems3/part-v-physics-simulation/chapter-34-signed-distance-fields-using-single-pass-gpu)

### Resources:
Follow the development: https://trello.com/b/mWtyHBMf/division-engine

Tutorials:
- Build mathematical worlds: https://www.youtube.com/watch?v=0ifChJ0nJfM&list=PL0EpikNmjs2CYUMePMGh3IjjP4tQlYqji
- Build a 3D landscape: https://www.youtube.com/watch?v=BFld4EBO2RE&t=1190s

## Framework

Division Engine is built using three core packages: Silk.Net, ComputeSharp, and AvaloniaUI.
Check them out here:
- [Silk.Net](https://github.com/dotnet/Silk.NET)
- [ComputeSharp](https://github.com/Sergio0694/ComputeSharp)
- [AvaloniaUI](https://github.com/AvaloniaUI/Avalonia)
