using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThievesLikeUs {
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
        private Dictionary<String, Entity> entitys;
        public Camera camera;
        public virtual void LoadContent() {}
        public virtual void Update() { }
        public void Draw(SpriteBatch spriteBatch) {
            camera.Draw(spriteBatch);
        }
        public Scene() {
            camera = new Camera(this);
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
        }

    }
}
