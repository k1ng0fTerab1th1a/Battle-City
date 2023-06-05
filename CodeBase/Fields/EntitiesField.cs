using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battle_City.Game_Elements.Entities;

namespace Battle_City.Fields
{
    public class EntitiesField
    {
        private Entity?[,] _entities;

        private int Width { get; }
        private int Height { get; }

        public EntitiesField(int width, int height)
        {
            _entities = new Entity?[width, height];
            Width = width;
            Height = height;
        }

        public void Clear()
        {
            _entities = new Entity?[_entities.GetLength(0), _entities.GetLength(1)];
        }

        public Entity? this[int x, int y]
        {
            get { return _entities[x, y]; }
            set { _entities[x, y] = value; }
        }

        public void PlaceOnField(Entity entity, Field field)
        {
            for (int i = entity.X; i < entity.X + entity.Width; i++)
            {
                for (int j = entity.Y; j < entity.Y + entity.Height; j++)
                {
                    if (this[i, j] != null && entity is Bullet bul && !(this[i, j] is Enemy && bul.MasterType != typeof(Player)))
                    {
                        this[i, j]!.Die(field);
                        bul.Die(field);
                        return;
                    }
                    this[i, j] = entity;
                }
            }

        }
        public void Remove(Entity entity)
        {
            for (int i = entity.X; i < entity.X + entity.Width; i++)
            {
                for (int j = entity.Y; j < entity.Y + entity.Height; j++)
                {
                    this[i, j] = null;
                }
            }
        }
    }
}
