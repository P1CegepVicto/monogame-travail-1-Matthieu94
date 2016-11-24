using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet02
{
    class GameObject
    {
        public bool vivant;
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 direction;
        public int vitesse;
        public int vitesse2;
        public bool toucheSol;
        public Rectangle rectColision = new Rectangle();

        public Rectangle GetRect()
        {
            rectColision.X = (int)this.position.X;
            rectColision.Y = (int)this.position.Y;
            rectColision.Width = this.sprite.Width;
            rectColision.Height = this.sprite.Height;

            return rectColision;
        }
    }
}
