# DivisionEngine

Division Engine is an SDF-based game engine written entirely in C#. Utilizing Avalonia UI for the interface and Silk.Net for native rendering, Division Engine has a complete build pipeline completely written in C#.

The render pipeline is built using an OpenGL backend with HLSL shaders written in C# using ComputeSharp.

## What Are SDFs?

*Signed Distance Fields* are spatial fields that store information represented as a grid sampling of the closest distance to the surface of an object defined as a polygonal model. Usually, the convention of using negative values inside the object and positive values outside the object is applied. Signed distance fields are important in computer graphics and related fields. Often, they are used for collision detection in cloth animation, soft-body physics effects, malleable geometry, volumetric effects, and fluid simulation.
(https://developer.nvidia.com/gpugems/gpugems3/part-v-physics-simulation/chapter-34-signed-distance-fields-using-single-pass-gpu)

Picture this:
- SDF-based rendering
- GPU compute acceleration in C#
- Open source
- Simple tooling

(More coming soon)

###Resources:
Follow the development: https://trello.com/b/mWtyHBMf/division-engine

Tutorials:
- https://www.youtube.com/watch?v=0ifChJ0nJfM&list=PL0EpikNmjs2CYUMePMGh3IjjP4tQlYqji
