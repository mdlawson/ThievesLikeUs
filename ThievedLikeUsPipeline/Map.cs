using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;
using TiledLib;

namespace ThievesLikeUsPipeline.Map {
    [ContentSerializerRuntimeType("ThievesLikeUs.Entities.Tile, ThievesLikeUs")]
    public class TileContent {
        public ExternalReference<Texture2DContent> Texture;
        public Rectangle Source;
        public Rectangle Destination;
        public SpriteEffects Effects;
    }
    [ContentSerializerRuntimeType("ThievesLikeUs.Entities.Layer, ThievesLikeUs")]
    public class LayerContent {
        //public MapContent Map; 
        public TileContent[] Tiles;
    }
    [ContentSerializerRuntimeType("ThievesLikeUs.Entities.Map, ThievesLikeUs")]
    public class MapContent {
        public int TileWidth;
        public int TileHeight;
        public List<LayerContent> Layers = new List<LayerContent>();
    }
    [ContentProcessor(DisplayName = "TMX Processor - Thieves")]
    public class MapProcessor : ContentProcessor<TiledLib.MapContent, MapContent> {
        public override MapContent Process(TiledLib.MapContent input, ContentProcessorContext context) {

            //System.Diagnostics.Debugger.Launch();

            TiledHelpers.BuildTileSetTextures(input, context);
            TiledHelpers.GenerateTileSourceRectangles(input);
            MapContent output = new MapContent {
                TileWidth = input.TileWidth,
                TileHeight = input.TileHeight
            };

            foreach (TiledLib.LayerContent layer in input.Layers) {
                TileLayerContent tlc = layer as TileLayerContent;
                if (tlc != null) {
                    LayerContent outLayer = new LayerContent { 
                        //Map = output
                    };
                    outLayer.Tiles = new TileContent[tlc.Data.Length];
                    for (int i = 0; i < tlc.Data.Length; i++) {

                        int tileIndex;
                        SpriteEffects effects;
                        TiledHelpers.DecodeTileID(tlc.Data[i], out tileIndex, out effects);
                        ExternalReference<Texture2DContent> textureContent = null;
                        Rectangle source = new Rectangle();
                        foreach (var tileSet in input.TileSets) {
                            if (tileIndex - tileSet.FirstId < tileSet.Tiles.Count) {
                                textureContent = tileSet.Texture;
                                source = tileSet.Tiles[(int)(tileIndex - tileSet.FirstId)].Source;
                                break;
                            }
                        }

                        outLayer.Tiles[i] = new TileContent {
                            Texture = textureContent,
                            Source = source,
                            Destination = new Rectangle((i % tlc.Width)*input.TileWidth, (i / tlc.Width)*input.TileHeight, input.TileWidth, input.TileHeight),
                            Effects = effects
                        };
                    }
                    output.Layers.Add(outLayer);
                }
            }

            return output;
        }
    }

}
