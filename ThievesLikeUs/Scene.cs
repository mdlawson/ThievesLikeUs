using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ThievesLikeUs.Scene {
    using Entity;

    class SceneManager {
        private Dictionary<String, Scene> scenes;
        public Dictionary<String, Scene> Scenes { get { return scenes; } }
        private Scene currentScene;
        public Scene CurrentScene { get { return currentScene; } }

        public SceneManager(Thieves game) {
            scenes = new Dictionary<string, Scene>();
        }
    }
    class Scene {
        public EntityCollection Entities;
        public Camera Camera;
        public String Name;
        public Game Game;
        private ContentManager Content;
        public virtual void LoadContent() {}
        public virtual void Update(GameTime gameTime) {
            foreach(Entity entity in Entities) {
                entity.Update(gameTime);
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch) {
            Camera.Draw(spriteBatch);
        }
        public void Add(Entity entity) {
            entity.Scene = this;
            Entities.Add(entity);
        }
        public Scene(String name, Game game) {
            this.Name = name;
            this.Game = game;
            Content = new ContentManager(Game.Services);
            Content.RootDirectory = Game.Content.RootDirectory + @"\" + name;
            Entities = new EntityCollection();
            Camera = new Camera(this);
        }
    }
    class Camera {
        public Vector2 Position;
        public float Rotation;
        public float Zoom;
        private Matrix transform { get { return Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(-Position.X, -Position.Y, 0); } }
        private Scene scene;
        public Camera(Scene scene) {
            Position = new Vector2(0f, 0f);
            this.scene = scene;
        }
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, transform);
            foreach ( Entity entity in scene.Entities ) {
                entity.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

    }

}
