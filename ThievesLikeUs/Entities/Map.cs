using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ThievesLikeUs.Entities {
    using Entity;
    class Tile {
        public Texture2D Texture;
        public Rectangle Source;
        public Rectangle Destination;
        public SpriteEffects Effects;
    }
    class Layer {
        public Tile[] Tiles;
        //public Map map;
        public void Draw(SpriteBatch spriteBatch) {
            foreach (Tile t in Tiles) {
                spriteBatch.Draw(t.Texture, t.Destination, t.Source, Color.White, 0, Vector2.Zero, t.Effects, 0);
            }
        }
        public void Update(GameTime gameTime) { }
    }
    class Map {
        public int TileWidth;
        public int TileHeight;
        public List<Layer> Layers = new List<Layer>();

        public void Draw(SpriteBatch spriteBatch) { 
            foreach (Layer l in Layers) { l.Draw(spriteBatch); }
        }
        public void Update(GameTime gameTime) {
            foreach (Layer l in Layers) { l.Update(gameTime); }
        }
    }
}
