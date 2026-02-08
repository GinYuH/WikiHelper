using System;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using static WikiHelper.WikiSystem;
using static WikiHelper.Utils;

namespace WikiHelper
{
    public class ImageExporters
    {
        /// <summary>
        /// Removes coordinate spacing from tiles
        /// </summary>
        /// <param name="exportColors"></param>
        /// <param name="baseColors"></param>
        /// <param name="curIDX"></param>
        /// <param name="curIDY"></param>
        /// <param name="expOffX"></param>
        /// <param name="expOffY"></param>
        /// <param name="impOffX"></param>
        /// <param name="impOffY"></param>
        /// <returns></returns>
        public static Color[,] RemoveSpacingFromTiles(Color[,] exportColors, Color[,] baseColors, int curIDX, int curIDY, int expOffX, int expOffY, int impOffX, int impOffY)
        {
            exportColors[curIDX + expOffX, curIDY + expOffY] = baseColors[impOffX + curIDX, impOffY + curIDY];
            return exportColors;
        }

        /// <summary>
        /// Exports 3x3 wiki previews for every single block
        /// </summary>
        public static void GenerateBlockSprites()
        {
            foreach (var item in ContentSamples.ItemsByType)
            {
                if (item.Value.ModItem == null) continue;
                if (item.Value.ModItem.Mod != CurMod) continue;
                if (item.Value.createTile <= TileID.Dirt)
                    continue;
                if (Main.tileFrameImportant[item.Value.createTile]) continue;

                if (TextureAssets.Tile[item.Value.createTile] == null) continue;

                Texture2D baseTexture = TextureAssets.Tile[item.Value.createTile].Value;
                int width = 48;
                int height = 48;

                Texture2D exporter = new Texture2D(Main.instance.GraphicsDevice, width, height);
                Color[] baseColors = new Color[baseTexture.Width * baseTexture.Height];
                baseTexture.GetData(baseColors);

                Color[] exportColors = new Color[exporter.Width * exporter.Height];
                exporter.GetData(exportColors);
                Color[] result = new Color[exportColors.Length];

                Color[,] baseArrayColors = GetColorsFromTexture(baseTexture);
                Color[,] finalArray = new Color[48, 48];

                if (baseArrayColors.GetLength(0) <= 54 || baseArrayColors.GetLength(1) <= 72)
                {
                    Main.NewText("Texture for " + item.Value.Name + " is too small for export, skipping.");
                    continue;
                }
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        finalArray = RemoveSpacingFromTiles(finalArray, baseArrayColors, i, j, 0, 0, 0, 54); // var 1
                        finalArray = RemoveSpacingFromTiles(finalArray, baseArrayColors, i, j, 16, 0, 36, 0); // var 2
                        finalArray = RemoveSpacingFromTiles(finalArray, baseArrayColors, i, j, 32, 0, 90, 54); // var 3

                        finalArray = RemoveSpacingFromTiles(finalArray, baseArrayColors, i, j, 0, 16, 0, 18); // var 2
                        finalArray = RemoveSpacingFromTiles(finalArray, baseArrayColors, i, j, 16, 16, 54, 18); // var 3
                        finalArray = RemoveSpacingFromTiles(finalArray, baseArrayColors, i, j, 32, 16, 72, 0); // var 3

                        finalArray = RemoveSpacingFromTiles(finalArray, baseArrayColors, i, j, 0, 32, 0, 72); // var 1
                        finalArray = RemoveSpacingFromTiles(finalArray, baseArrayColors, i, j, 16, 32, 36, 36); // var 2
                        finalArray = RemoveSpacingFromTiles(finalArray, baseArrayColors, i, j, 32, 32, 90, 72); // var 3
                    }
                }

                for (int i = 0; i < 48; i++)
                {
                    for (int j = 0; j < 48; j++)
                    {
                        result[i + j * 48] = finalArray[i, j];
                    }
                }
                exporter.SetData(result);

                string path = $@"{exportPath}\" + item.Value.ModItem.DisplayName + " (placed)" + ImageName() + ".png";
                using (Stream stream = File.OpenWrite(path))
                {
                    exporter.SaveAsPng(stream, exporter.Width, exporter.Height);
                }
            }
        }
        /// <summary>
        /// Removes coordinate spacing from walls
        /// </summary>
        /// <param name="exportColors"></param>
        /// <param name="baseColors"></param>
        /// <param name="expX"></param>
        /// <param name="expY"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        public static Color[,] PopulateWallColor(Color[,] exportColors, Color[,] baseColors, int expX, int expY, int startX, int startY, int endX, int endY)
        {
            for (int i = 0; i <= (endX - startX); i++)
            {
                for (int j = 0; j <= (endY - startY); j++)
                {
                    exportColors[expX + i, expY + j] = baseColors[startX + i, startY + j];
                }
            }
            return exportColors;
        }

        /// <summary>
        /// Exports 3x3 wiki previews for every single wall
        /// </summary>
        public static void GenerateWallSprites()
        {
            foreach (var item in ContentSamples.ItemsByType)
            {
                if (item.Value.ModItem == null) continue;
                if (item.Value.ModItem.Mod != CurMod) continue;
                if (item.Value.createWall <= 0)
                    continue;
                if (TextureAssets.Wall[item.Value.createWall] == null) continue;

                Texture2D baseTexture = TextureAssets.Wall[item.Value.createWall].Value;
                int width = 64;
                int height = 64;

                Texture2D exporter = new Texture2D(Main.instance.GraphicsDevice, width, height);

                Color[] baseColors = new Color[baseTexture.Width * baseTexture.Height];
                baseTexture.GetData(baseColors);

                Color[] exportColors = new Color[exporter.Width * exporter.Height];
                exporter.GetData(exportColors);
                Color[] result = new Color[exportColors.Length];

                Color[,] baseArrayColors = GetColorsFromTexture(baseTexture);
                Color[,] finalArray = new Color[64, 64];


                finalArray = PopulateWallColor(finalArray, baseArrayColors, 0, 0, 0, 108, 23, 131); // var 1
                finalArray = PopulateWallColor(finalArray, baseArrayColors, 24, 0, 116, 0, 131, 23); // var 2
                finalArray = PopulateWallColor(finalArray, baseArrayColors, 40, 0, 44, 108, 67, 131); // var 3

                finalArray = PopulateWallColor(finalArray, baseArrayColors, 0, 23, 0, 44, 23, 59); // var 2
                finalArray = PopulateWallColor(finalArray, baseArrayColors, 24, 23, 80, 44, 95, 59); // var 3
                finalArray = PopulateWallColor(finalArray, baseArrayColors, 40, 23, 152, 80, 175, 95); // var 3

                finalArray = PopulateWallColor(finalArray, baseArrayColors, 0, 39, 144, 152, 167, 175); // var 1
                finalArray = PopulateWallColor(finalArray, baseArrayColors, 24, 39, 44, 80, 59, 103); // var 2
                finalArray = PopulateWallColor(finalArray, baseArrayColors, 40, 39, 116, 152, 139, 175); // var 2


                for (int i = 0; i < 64; i++)
                {
                    for (int j = 0; j < 64; j++)
                    {
                        result[i + j * 64] = finalArray[i, j];
                    }
                }
                exporter.SetData(result);

                string path = $@"{exportPath}\" + item.Value.Name + " (placed)" + ImageName() + ".png";
                using (Stream stream = File.OpenWrite(path))
                {
                    exporter.SaveAsPng(stream, exporter.Width, exporter.Height);
                }
            }
        }

        /// <summary>
        /// Exports images for every single furniture (non block tile)
        /// </summary>
        public static void GenerateFurnitureSprites()
        {
            foreach (var item in ContentSamples.ItemsByType)
            {
                if (item.Value.ModItem == null) continue;
                if (item.Value.ModItem.Mod != CurMod) continue;
                if (item.Value.createTile <= TileID.Dirt)
                    continue;
                if (!Main.tileFrameImportant[item.Value.createTile]) continue;
                if (TextureAssets.Tile[item.Value.createTile] == null) continue;

                TileObjectData tileData = TileObjectData.GetTileData(item.Value.createTile, 0);

                if (tileData == null) continue;

                Texture2D baseTexture = TextureAssets.Tile[item.Value.createTile].Value;

                int width = tileData.Width * 16;
                int height = tileData.Height * 16;

                Texture2D exporter = new Texture2D(Main.instance.GraphicsDevice, width, height);

                Color[] baseColors = new Color[baseTexture.Width * baseTexture.Height];
                baseTexture.GetData(baseColors);

                Color[] exportColors = new Color[exporter.Width * exporter.Height];
                exporter.GetData(exportColors);
                Color[] result = new Color[exportColors.Length];

                Color[,] baseArrayColors = GetColorsFromTexture(baseTexture);
                Color[,] finalArray = new Color[width, height];

                int startX = item.Value.placeStyle;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        int shiftX = 0;
                        int shiftY = 0;
                        if (i >= 16)
                            shiftX = 2 * (int)(i / 16);
                        if (j >= 16)
                            shiftY = 2 * (int)(j / 16);

                        if ((i + shiftX) < baseArrayColors.GetLength(0) && j + shiftY < baseArrayColors.GetLength(1))
                            finalArray[i, j] = baseArrayColors[i + shiftX, j + shiftY];
                    }
                }

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        result[i + j * width] = finalArray[i, j];
                    }
                }
                exporter.SetData(result);

                string path = $@"{exportPath}\" + item.Value.ModItem.DisplayName + " (placed)" + ImageName() + ".png";
                using (Stream stream = File.OpenWrite(path))
                {
                    exporter.SaveAsPng(stream, exporter.Width, exporter.Height);
                }
            }
        }

        /// <summary>
        /// Exports images for every item
        /// </summary>
        public static void GenerateItemSprites()
        {
            foreach (var item in ContentSamples.ItemsByType)
            {
                if (item.Value.ModItem == null) continue;
                if (item.Value.ModItem.Mod != CurMod) continue;


                Texture2D baseTexture = TextureAssets.Item[item.Value.type].Value;

                string path = $@"{exportPath}\" + item.Value.ModItem.DisplayName + ImageName() + ".png";
                using (Stream stream = File.OpenWrite(path))
                {
                    baseTexture.SaveAsPng(stream, baseTexture.Width, baseTexture.Height);
                }
            }
        }
    }
}