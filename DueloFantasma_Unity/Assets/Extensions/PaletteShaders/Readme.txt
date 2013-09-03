PALETTE SHADERS v1.0           2013 Donitz
==========================================

Description:

	Palette shaders is a collection of color replacement and dithering shaders.

	The shaders use dithering algorithms written by Joel Yliluoma to find and
	replace a color with the closest matching color or pattern of colors.

	The shaders use palette images which are pre-generated with the included scripts.
	The palette images represent one or more colors which are mixed in a dithering
	pattern to replace the original color.

	The palettes have a limit of 4096 unique colors (256x16 pixels).
	
	The palette shaders work best at low resolutions and DOES NOT PIXELIZE the screen.

	Included shaders:
		Normal/Diffuse
		Normal/Unlit
		Normal/Unlit Simple
		Cutout/Diffuse
		Cutout/Unlit
		Cutout/Unlit Simple
		Vertex Color/Diffuse
		Vertex Color/Unlit
		Vertex Color/Unlit Simple

	For more information about the dithering algorithms, check out Joel Yliluoma's paper at
	http://bisqwit.iki.fi/story/howto/dither/jy/

Instructions:

	1. Use the CreatePalette prefab to create a color palette
	   Mixed Color Count: The number of colors to mix in a dithering pattern
	2. Apply one of the Palette Shaders to your material
	2. Configure the shader
	   Base: The main texture
	   Mixed Color Count: The number of mixed colors in your palette (colored rows of 16x16 squares)
	   Palette Height: The palette image height in pixels
	   Palette: The palette image
	   Dither Size: The size of the chosen dither texture
	   Dither: The dither pattern image (With the red values 0 to 254)

Releases:

	1.0
		25.08.2013: First public release