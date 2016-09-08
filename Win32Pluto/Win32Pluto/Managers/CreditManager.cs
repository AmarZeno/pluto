using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Models;

namespace Win32Pluto.Managers
{
    class CreditManager
    {
        List<Credit> creditsCollection = new List<Credit>();

        public void Add(Credit credit) {
            creditsCollection.Add(credit);
        }

        public void Dispose() {
            foreach (Credit credit in creditsCollection) {
                credit.sprite.texture.Dispose();
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Credit credit in creditsCollection) {
                credit.Draw(spriteBatch);
            }
        }
    }
}
