using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    internal class Scene
    {
        DataManager dataManager;
        public List<Polyhedron> figures;
        public Point cameraPos;
        public LightSource lightSource;
        public Scene() {
            dataManager=new DataManager();
        }


        public void Load(string path) { 
        
        }

    }
}
