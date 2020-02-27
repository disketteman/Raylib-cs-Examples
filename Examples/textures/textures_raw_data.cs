/*******************************************************************************************
*
*   raylib [textures] example - Load textures from raw data
*
*   NOTE: Images are loaded in CPU memory (RAM); textures are loaded in GPU memory (VRAM)
*
*   This example has been created using raylib 1.3 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2015 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.PixelFormat;

namespace Examples
{
    public class textures_raw_data
    {
        public static int Main()
        {
            // Initialization
            //--------------------------------------------------------------------------------------
            const int screenWidth = 800;
            const int screenHeight = 450;

            InitWindow(screenWidth, screenHeight, "raylib [textures] example - texture from raw data");

            // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)

            // Load RAW image data (512x512, 32bit RGBA, no file header)
            Image fudesumiRaw = LoadImageRaw("resources/fudesumi.raw", 384, 512, (int)UNCOMPRESSED_R8G8B8A8, 0);
            Texture2D fudesumi = LoadTextureFromImage(fudesumiRaw);   // Upload CPU (RAM) image to GPU (VRAM)
            UnloadImage(fudesumiRaw);                              // Unload CPU (RAM) image data

            // Generate a isChecked texture by code (1024x1024 pixels)
            int width = 1024;
            int height = 1024;

            // Dynamic memory allocation to store pixels data (Color type)
            // Color *pixels = (Color *)malloc(width*height*sizeof(Color));
            Color[] pixels = new Color[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (((x / 32 + y / 32) / 1) % 2 == 0) pixels[y * height + x] = ORANGE;
                    else pixels[y * height + x] = GOLD;
                }
            }

            // Load pixels data into an image structure and create texture
            Image isCheckedIm = LoadImageEx(pixels, width, height);
            Texture2D isChecked = LoadTextureFromImage(isCheckedIm);
            UnloadImage(isCheckedIm);     // Unload CPU (RAM) image data

            // Dynamic memory must be freed after using it
            // free(pixels);               // Unload CPU (RAM) pixels data
            //---------------------------------------------------------------------------------------

            // Main game loop
            while (!WindowShouldClose())    // Detect window close button or ESC key
            {
                // Update
                //----------------------------------------------------------------------------------
                // TODO: Update your variables here
                //----------------------------------------------------------------------------------

                // Draw
                //----------------------------------------------------------------------------------
                BeginDrawing();

                ClearBackground(RAYWHITE);

                DrawTexture(isChecked, screenWidth / 2 - isChecked.width / 2, screenHeight / 2 - isChecked.height / 2, Fade(WHITE, 0.5f));
                DrawTexture(fudesumi, 430, -30, WHITE);

                DrawText("CHECKED TEXTURE ", 84, 100, 30, BROWN);
                DrawText("GENERATED by CODE", 72, 164, 30, BROWN);
                DrawText("and RAW IMAGE LOADING", 46, 226, 30, BROWN);

                DrawText("(c) Fudesumi sprite by Eiden Marsal", 310, screenHeight - 20, 10, BROWN);

                EndDrawing();
                //----------------------------------------------------------------------------------
            }

            // De-Initialization
            //--------------------------------------------------------------------------------------
            UnloadTexture(fudesumi);    // Texture unloading
            UnloadTexture(isChecked);     // Texture unloading

            CloseWindow();              // Close window and OpenGL context
            //--------------------------------------------------------------------------------------

            return 0;
        }
    }
}