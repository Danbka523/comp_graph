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
        public List<Polyhedron> figures=new();
        public Point cameraPos;
        public LightSource lightSource;
        Transformations transformations;
        RT rayTracing;
        int width;
        int height;
        public bool isMirror;
        public bool isTrans;
        bool isLight;
        List<Material> materials=new();
        public List<LightSource> lightSources=new();
        public Scene(int width, int height, bool isMirror, bool isTrans, bool isLight) {
            dataManager=new DataManager();
            transformations = new Transformations();
            this.width= width;
            this.height = height;
            GenMaterials();
            this.isTrans= isTrans;
            this.isMirror= isMirror;
            this.isLight = isLight;
        }

        void GenMaterials()
        {
            Material transparensy = new Material(0.0f, 0.001f, 0.0f, 0.9f, 1f);
            transparensy.Color = new Vector(1, 1, 1);
            Material mirror = new Material(0.0f,0.0f,1f,0.0f,1f);
            mirror.Color = new Vector(1, 1, 1);
            materials.Add(transparensy);
            materials.Add(mirror);

        }

        public void Load() {
            Polyhedron cube = dataManager.Load("cube.obj");
            Polyhedron cube1 = dataManager.Load("cube.obj");
            //Polyhedron sphere = dataManager.Load("new_sphere.obj");
            Polyhedron sphere = dataManager.Load("sphere.obj");
            Polyhedron room = dataManager.Load("cube.obj");


         

            room.Polygons[0].pen = new Pen(Color.Orange);
            room.Polygons[1].pen = new Pen(Color.Orange);
            room.Polygons[2].pen = new Pen(Color.Red);
            room.Polygons[3].pen = new Pen(Color.Red);
            room.Polygons[4].pen = new Pen(Color.Green);
            room.Polygons[5].pen = new Pen(Color.Green);
            room.Polygons[6].pen = new Pen(Color.Blue); 
            room.Polygons[7].pen = new Pen(Color.Blue);
            room.Polygons[8].pen = new Pen(Color.Pink);
            room.Polygons[9].pen = new Pen(Color.Pink);
            room.Polygons[10].pen = new Pen(Color.White);
            room.Polygons[11].pen = new Pen(Color.White);

            transformations.Scale(room, 5, 5, 5);          
            transformations.Shift(cube, 4, 0, -1);
            transformations.Shift(cube1, 0, 0, 0);
            transformations.Shift(sphere, -4, 0, 3);
            transformations.Scale(cube, 1f, 1f, 1.8f);
            transformations.RotateAroundAxis(cube, 10, "Z");
            transformations.RotateAroundAxis(cube1, 30, "Z");
            cube.SetPen(new Pen(Color.Red));
            cube1.SetPen(new Pen(Color.Purple));
            sphere.SetPen(new Pen(Color.Lime));
            if (isMirror)
            {
                cube.material = new Material(materials[1]);
                cube1.material = new Material(materials[1]);
                sphere.material = new Material(materials[1]);

            }
            else if (isTrans)
            {
                cube.material = new Material(materials[0]);
                cube1.material = new Material(materials[0]);
                sphere.material = new Material(materials[0]);
            }
            else
            {
                cube.material = new Material(0.1f, 0.7f, 0.0f, 0.0f, 1.5f);
                
                cube1.material = new Material(0.1f, 0.7f, 0.0f, 0.0f, 1.5f);
                
                cube.material.Color = new Vector(1f, 1f, 1f);
                cube1.material.Color = new Vector(1.0f, 1.0f, 1.0f);
                sphere.material = new Material(0.1f, 0.5f, 0.0f, 0.0f, 1.5f);
                sphere.material.Color = new Vector(0.0f, 1.0f, 0.0f);
             

            }

            
            room.material = new Material(0.005f, 0.7f, 0.0f, 0.0f, 1f);          
            room.material.Color = new Vector(0.2f, 0.2f, 0.2f);

            
            lightSource =new LightSource(new Point(0f, 4f, 4.9f),new Vector(1,1,1));
            lightSources.Add(lightSource);
            var lightSource1 = new LightSource(new Point(-3f, 4f, 4.9f), new Vector(1, 1, 1));
            if (isLight)
                lightSources.Add(lightSource1);
            rayTracing = new RT(this);

            cameraPos = new Vector(room.Polygons[0].Verts[0])+ new Vector(room.Polygons[0].Verts[2])+ new Vector(room.Polygons[0].Verts[1])+ new Vector(room.Polygons[1].Verts[2]);

            figures.Add(room);
            figures.Add(cube);
            figures.Add(cube1);
            //figures.Add(sphere);


        }

        Point SetCameraPos(Polyhedron room) {
            Point res= new Point(0,0,0);

           

            return res;
            
        
        }
        public Point[,] Raster(Polyhedron room)
        {

            Point upLeft = room.Polygons[0].Verts[0];
            Point upRight = room.Polygons[0].Verts[1];
            Point downRight = room.Polygons[0].Verts[2];
            Point downLeft = room.Polygons[1].Verts[2];
            Point[,] pixels = new Point[width, height];
            Vector stepUp = (new Vector(upRight) - new Vector( upLeft)) / (width - 1f);
            Vector stepDown = (new Vector(downRight) - new Vector(downLeft)) / (width - 1f);
            Vector up = new Vector(upLeft);
            Vector down = new Vector(downLeft);
            for (int i = 0; i < width; i++)
            {
                Vector stepY = (new Vector(up) - new Vector(down)) / (height - 1f);
                Vector d = down;
                for (int j = 0; j < height; j++)
                {
                    pixels[i, j] = d;
                    d += stepY;
                }
                up += stepUp;
                down += stepDown;
            }
            return pixels;
        }
        public Bitmap Draw()
        {
            return rayTracing.BackWardRT(width, height, Raster(figures[0]));

        }

    }
}
