using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThievesLikeUs.Entity {
    using Scene;
    using Component;
    class Entity {
        public Scene Scene;
        public string Name;
        public Vector2 Position;
        public List<Component> components;
        public Entity() { 
            components = new List<Component>();
        }
        public Entity(String name) : this() {
            this.Name = name;
        }    
        public Entity(String name, Vector2 position) : this(name) {
            this.Position = position;
        }
        public void Add(Component component) {
            component.parent = this;
            components.Add(component);
        }
        public virtual void Draw(SpriteBatch spriteBatch) { 
            foreach (Component component in components) {
                component.Draw(spriteBatch);
            }
        }
        public virtual void Update(GameTime gameTime) {
            foreach(Component component in components) {
                component.Update(gameTime);
            }
        }
    }
    class EntityCollection : KeyedCollection<string, Entity> {
        protected override string GetKeyForItem(Entity item) {
            return item.Name;
        }
    }
}
