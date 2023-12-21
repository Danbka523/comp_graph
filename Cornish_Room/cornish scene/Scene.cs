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
        public Scene(int width, int height) {
            dataManager=new DataManager();
            transformations = new Transformations();
            this.width= width;
            this.height = height;
        }


        public void Load() {
            Polyhedron cube = dataManager.Load("cube.obj");
            Polyhedron sphere = dataManager.Load("new_sphere.obj");
            Polyhedron room = dataManager.Load("cube.obj");


         

            room.Polygons[0].pen = new Pen(Color.Blue);
            room.Polygons[1].pen = new Pen(Color.Blue);
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
            transformations.Shift(cube, -1, 2, -1f);
            transformations.Scale(cube, 1.2f, 1.2f, 1.2f);
            transformations.RotateAroundAxis(cube, 30, "Z");

            cube.material = new Material(0f, 0f, 0.1f, 0.0f, 1.5f);
            room.material = new Material(0.005f, 0.5f, 0.0f, 0.0f, 1f);
            sphere.material = new Material(0.1f, 0.5f, 0.3f, 0.7f, 1f);

            cube.material.Color = new Vector(1.0f, 1.0f, 1.0f);            
            sphere.material.Color = new Vector(0.0f, 1.0f, 0.0f);
            room.material.Color = new Vector(0.2f, 0.2f, 0.2f);

            
            lightSource =new LightSource(new Point(0f, 0f, 4.9f),new Vector(1,1,1));

            rayTracing = new RT(this);

            cameraPos = new Vector(room.Polygons[0].Verts[0])+ new Vector(room.Polygons[0].Verts[2])+ new Vector(room.Polygons[0].Verts[1])+ new Vector(room.Polygons[1].Verts[2]);

            figures.Add(room);
            figures.Add(cube);
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
