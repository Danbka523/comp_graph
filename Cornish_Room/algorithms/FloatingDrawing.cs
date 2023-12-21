using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    public enum DISPLAYTYPE {TRIANGLES,LINES,NET};
    internal class FloatingDrawing
    {
        const int scaleFactor = 100;
        const float threshold = 5;
        const float splitting = 20;
        float yawAngle;
        float pitchAngle;
        float[] upHorizon;
        float[] downHorizon;
        Drawing drawing;
        Transformations transformations = new();
        PictureBox canvas;
        public DISPLAYTYPE displayType=DISPLAYTYPE.TRIANGLES;
        string func;
        public FloatingDrawing(PictureBox canvas, string func, Drawing drawing)
        {
            this.canvas = canvas;
            this.func = func;
            this.drawing= drawing;
            Point.world = new PointF(canvas.Width/2,canvas.Height/2);
        }
        FloatingPoint getScaledPoint(Point p)
        {
            var res = transformations.RotateAroundAxis(p, yawAngle, "Y");
            res = transformations.RotateAroundAxis(res, pitchAngle, "X");
            var x = Point.world.X + scaleFactor * res.XF;
            if (x < 0 || x >= canvas.Width)
            {
                return new FloatingPoint(x, scaleFactor * res.YF, res.ZF * scaleFactor, Visibilty.INVISIBLE);
            }

            return new FloatingPoint(x,
                scaleFactor * res.YF, res.ZF * scaleFactor, ref upHorizon, ref downHorizon);
        }

        public void changeViewAngles(float shiftX = 0, float shiftY = 0)
        {
            pitchAngle = Math.Clamp(pitchAngle + shiftY, -89.0f, 89.0f);
            yawAngle = Math.Clamp(yawAngle + shiftX, -89.0f, 89.0f);
        }

        Color getColorByVisibility(Visibilty v)
        {
            switch (v)
            {
                case Visibilty.VISIBLE_UP:
                    return Color.Navy;
                case Visibilty.VISIBLE_DOWN:
                    return Color.CornflowerBlue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(v), v, null);
            }
        }

        void updateHorizons(FloatingPoint last, FloatingPoint curr)
        {
            if (last.X < 0 || curr.X >= canvas.Width)
            {
                return;
            }
            if (curr.X - last.X == 0)
            {
                upHorizon[curr.X] = Math.Max(upHorizon[curr.X], curr.YF);
                downHorizon[curr.X] = Math.Min(downHorizon[curr.X], curr.YF);
            }
            else
            {
                var tg = (curr.YF - last.YF) / (curr.XF - last.XF);
                for (int x = last.X; x <= curr.X; x++)
                {
                    var y = tg * (x - last.XF) + last.YF;
                    upHorizon[x] = Math.Max(upHorizon[x], y);
                    downHorizon[x] = Math.Min(downHorizon[x], y);
                }
            }
        }

        FloatingPoint intersect(FloatingPoint previous, FloatingPoint curr)
        {
            float xStep = (curr.XF - previous.XF) / 20;
            float yStep = (curr.YF - previous.YF) / 20;
            for (int i = 1; i <= 20; i++)
            {
                var point = new FloatingPoint(Math.Clamp(previous.XF + i * xStep, 0, canvas.Width - 1), previous.YF + i * yStep, curr.ZF, ref upHorizon,
                    ref downHorizon);
                if (point.Visibility == Visibilty.INVISIBLE)
                {
                    return point;
                }
            }

            return curr;
        }

        FloatingPoint antiIntersect(FloatingPoint previous, FloatingPoint curr)
        {
            float xStep = (curr.XF - previous.XF) / 20;
            float yStep = (curr.YF - previous.YF) / 20;
            for (int i = 1; i <= 20; i++)
            {
                var point = new FloatingPoint(Math.Clamp(previous.XF + i * xStep, 0, canvas.Width - 1), previous.YF + i * yStep, curr.ZF, ref upHorizon,
                    ref downHorizon);
                if (point.Visibility != Visibilty.INVISIBLE)
                {
                    return point;
                }
            }

            return curr;
        }


        void floatingHorizonByLines()
        {
            float step = threshold * 2.0f / splitting;

            upHorizon = new float[canvas.Width];
            downHorizon = new float[canvas.Width];

            for (int i = 0; i < canvas.Width; i++)
            {
                upHorizon[i] = float.MinValue;
                downHorizon[i] = float.MaxValue;
            }

            var bmp = new Bitmap(canvas.Width, canvas.Height);


            for (float z = threshold; z >= -threshold; z -= step)
            {
                FloatingPoint previous = getScaledPoint(new Point(-threshold, transformations.EvalFunc(func,-threshold,z), z));
                for (float x = -threshold; x <= threshold; x += step)
                {
                    FloatingPoint current = getScaledPoint(new Point(x, transformations.EvalFunc(func,x,z), z));

                    if (current.Visibility == Visibilty.VISIBLE_UP)
                    {
                        if (previous.Visibility != Visibilty.INVISIBLE)
                        {
                            drawing.DrawVuLine(previous.Projection(), current.Projection(),getColorByVisibility(Visibilty.VISIBLE_UP),bmp);
                            updateHorizons(previous, current);
                        }
                        else
                        {
                            var mid = intersect(current, previous);
                            drawing.DrawVuLine(previous.Projection(), current.Projection(),getColorByVisibility(Visibilty.VISIBLE_UP),bmp);
                            updateHorizons(current, mid);
                        }

                    }
                    else if (current.Visibility == Visibilty.VISIBLE_DOWN)
                    {
                        if (previous.Visibility != Visibilty.INVISIBLE)
                        {
                            drawing.DrawVuLine(previous.Projection(), current.Projection(), getColorByVisibility(Visibilty.VISIBLE_DOWN),bmp);
                            updateHorizons(previous, current);
                        }
                        else
                        {
                            var mid = intersect(current, previous);
                            drawing.DrawVuLine(previous.Projection(), current.Projection(), getColorByVisibility(Visibilty.VISIBLE_DOWN),bmp);
                            updateHorizons(current, mid);
                        }

                    }
                    else
                    {
                        if (previous.Visibility == Visibilty.VISIBLE_UP)
                        {
                            var mid = antiIntersect(current, previous);
                            drawing.DrawVuLine(previous.Projection(), mid.Projection(), getColorByVisibility(Visibilty.VISIBLE_UP),bmp);
                            updateHorizons(previous, mid);
                        }
                        else if (previous.Visibility == Visibilty.VISIBLE_DOWN)
                        {
                            var mid = antiIntersect(current, previous);
                            drawing.DrawVuLine(previous.Projection(), mid.Projection(), getColorByVisibility(Visibilty.VISIBLE_DOWN), bmp);
                            updateHorizons(previous, mid);
                        }

                    }

                    previous = current;
                }
            }


            canvas.Image = bmp;
        }

        List<FloatingPoint> prevLine;

        void drawNet(Bitmap bmp, int xNumber, FloatingPoint current, Color color)
        {
            if (prevLine.Count != 0)
            {
                var previous = prevLine[xNumber];
                if (current.Visibility != Visibilty.INVISIBLE)
                {
                    if (previous.Visibility != Visibilty.INVISIBLE)
                    {
                        drawing.DrawVuLine(previous.Projection(), current.Projection(), color,bmp);
                    }
                    else
                    {
                        var mid = intersect(current, previous);
                        drawing.DrawVuLine(current.Projection(), mid.Projection(),color,bmp);
                    }
                }
                else
                {
                    if (previous.Visibility != Visibilty.INVISIBLE)
                    {
                        var mid = antiIntersect(current, previous);
                        drawing.DrawVuLine(previous.Projection(), mid.Projection(), color, bmp);
                    }
                }
            }
        }



        void floatingHorizonByNet()
        {
            float step = threshold * 2.0f / splitting;

            upHorizon = new float[canvas.Width];
            downHorizon = new float[canvas.Width];

            for (int i = 0; i < canvas.Width; i++)
            {
                upHorizon[i] = float.MinValue;
                downHorizon[i] = float.MaxValue;
            }

            var bmp = new Bitmap(canvas.Width, canvas.Height);


            prevLine = new List<FloatingPoint>();
            for (float z = threshold; z >= -threshold; z -= step)
            {
                List<FloatingPoint> currLine = new List<FloatingPoint>();
                FloatingPoint previous = getScaledPoint(new Point(-threshold, transformations.EvalFunc(func,-threshold,z), z));

                for (float x = -threshold; x <= threshold; x += step)
                {
                    FloatingPoint current = getScaledPoint(new Point(x, transformations.EvalFunc(func,x,z), z));
                    currLine.Add(current);
                    if (current.Visibility == Visibilty.VISIBLE_UP)
                    {
                        if (previous.Visibility != Visibilty.INVISIBLE)
                        {
                            drawing.DrawVuLine(previous.Projection(), current.Projection(), getColorByVisibility(Visibilty.VISIBLE_UP), bmp);
                            drawNet(bmp, currLine.Count - 1, current, getColorByVisibility(Visibilty.VISIBLE_UP));
                            updateHorizons(previous, current);
                        }
                        else
                        {

                            var mid = intersect(current, previous);
                            drawing.DrawVuLine(previous.Projection(), current.Projection(),getColorByVisibility(Visibilty.VISIBLE_UP),bmp);
                            drawNet(bmp, currLine.Count - 1, current, getColorByVisibility(Visibilty.VISIBLE_UP));
                            updateHorizons(current, mid);
                        }

                    }
                    else if (current.Visibility == Visibilty.VISIBLE_DOWN)
                    {
                        if (previous.Visibility != Visibilty.INVISIBLE)
                        {
                            drawing.DrawVuLine(previous.Projection(), current.Projection(), getColorByVisibility(Visibilty.VISIBLE_DOWN), bmp);
                            drawNet(bmp, currLine.Count - 1, current, getColorByVisibility(Visibilty.VISIBLE_DOWN));
                            updateHorizons(previous, current);
                        }
                        else
                        {

                            var mid = intersect(current, previous);
                            drawing.DrawVuLine(previous.Projection(), current.Projection(), getColorByVisibility(Visibilty.VISIBLE_DOWN),bmp);
                            drawNet(bmp, currLine.Count - 1, current, getColorByVisibility(Visibilty.VISIBLE_DOWN));
                            updateHorizons(current, mid);
                        }

                    }
                    else
                    {
                        if (previous.Visibility == Visibilty.VISIBLE_UP)
                        {
                            var mid = antiIntersect(current, previous);
                            drawing.DrawVuLine(previous.Projection(), mid.Projection(), getColorByVisibility(Visibilty.VISIBLE_UP), bmp);
                            drawNet(bmp, currLine.Count - 1, current, getColorByVisibility(Visibilty.VISIBLE_UP));
                            updateHorizons(previous, mid);
                        }
                        else if (previous.Visibility == Visibilty.VISIBLE_DOWN)
                        {
                            var mid = antiIntersect(current, previous);
                            drawing.DrawVuLine(previous.Projection(), mid.Projection(), getColorByVisibility(Visibilty.VISIBLE_DOWN), bmp);
                            drawNet(bmp, currLine.Count - 1, current, getColorByVisibility(Visibilty.VISIBLE_DOWN));
                            updateHorizons(previous, mid);
                        }
                    }

                    previous = current;
                }

                prevLine = currLine;
            }


            canvas.Image = bmp;
        }

        void drawTriangles(Bitmap bmp, int xNumber, FloatingPoint current, Color color)
        {
            if (prevLine.Count != 0)
            {
                var previous = prevLine[xNumber];
                if (current.Visibility != Visibilty.INVISIBLE)
                {
                    if (previous.Visibility != Visibilty.INVISIBLE)
                    {
                        drawing.DrawVuLine(previous.Projection(), current.Projection(), color, bmp);
                    }
                    else
                    {
                        var mid = intersect(current, previous);
                        drawing.DrawVuLine(current.Projection(), mid.Projection(), color, bmp);
                    }
                }
                else
                {
                    if (previous.Visibility != Visibilty.INVISIBLE)
                    {
                        var mid = antiIntersect(current, previous);
                        drawing.DrawVuLine(previous.Projection(), mid.Projection(), color, bmp);
                    }
                }

                if (xNumber != 0)
                {
                    var previousLeft = prevLine[xNumber - 1];
                    if (current.Visibility != Visibilty.INVISIBLE)
                    {
                        if (previousLeft.Visibility != Visibilty.INVISIBLE)
                        {
                            drawing.DrawVuLine(previousLeft.Projection(), current.Projection(), color, bmp);
                        }
                        else
                        {
                            var mid = intersect(current, previousLeft);
                            drawing.DrawVuLine(current.Projection(), mid.Projection(), color, bmp);
                        }
                    }
                    else
                    {
                        if (previousLeft.Visibility != Visibilty.INVISIBLE)
                        {
                            var mid = antiIntersect(current, previousLeft);
                            drawing.DrawVuLine(previousLeft.Projection(), mid.Projection(), color, bmp);
                        }
                    }
                }
            }
        }

        void floatingHorizonByTriangles()
        {
            float step = threshold * 2.0f / splitting;

            upHorizon = new float[canvas.Width];
            downHorizon = new float[canvas.Width];

            for (int i = 0; i < canvas.Width; i++)
            {
                upHorizon[i] = float.MinValue;
                downHorizon[i] = float.MaxValue;
            }

            var bmp = new Bitmap(canvas.Width, canvas.Height);


            prevLine = new List<FloatingPoint>();
            for (float z = threshold; z >= -threshold; z -= step)
            {
                List<FloatingPoint> currLine = new List<FloatingPoint>();
                FloatingPoint previous = getScaledPoint(new Point(-threshold, transformations.EvalFunc(func,-threshold,z), z));
                for (float x = -threshold; x <= threshold; x += step)
                {
                    FloatingPoint current = getScaledPoint(new Point(x, transformations.EvalFunc(func,x,z), z));
                    currLine.Add(current);
                    if (current.Visibility == Visibilty.VISIBLE_UP)
                    {
                        if (previous.Visibility != Visibilty.INVISIBLE)
                        {
                            drawing.DrawVuLine(previous.Projection(), current.Projection(),
                                getColorByVisibility(Visibilty.VISIBLE_UP), bmp);
                            drawTriangles(bmp, currLine.Count - 1, current, getColorByVisibility(Visibilty.VISIBLE_UP));
                            updateHorizons(previous, current);
                        }
                        else
                        {
                            var mid = intersect(current, previous);
                            drawing.DrawVuLine(previous.Projection(), current.Projection(),getColorByVisibility(Visibilty.VISIBLE_UP),bmp);
                            drawTriangles(bmp, currLine.Count - 1, current, getColorByVisibility(Visibilty.VISIBLE_UP));
                            updateHorizons(current, mid);
                        }

                    }
                    else if (current.Visibility == Visibilty.VISIBLE_DOWN)
                    {
                        if (previous.Visibility != Visibilty.INVISIBLE)
                        {
                            drawing.DrawVuLine(previous.Projection(), current.Projection(),getColorByVisibility(Visibilty.VISIBLE_DOWN),bmp);
                            drawTriangles(bmp, currLine.Count - 1, current, getColorByVisibility(Visibilty.VISIBLE_DOWN));
                            updateHorizons(previous, current);
                        }
                        else
                        {
                            var mid = intersect(current, previous);
                            drawing.DrawVuLine(previous.Projection(), current.Projection(), getColorByVisibility(Visibilty.VISIBLE_DOWN),bmp);
                            drawTriangles(bmp, currLine.Count - 1, current, getColorByVisibility(Visibilty.VISIBLE_DOWN));
                            updateHorizons(current, mid);
                        }

                    }
                    else
                    {
                        if (previous.Visibility == Visibilty.VISIBLE_UP)
                        {
                            var mid = antiIntersect(current, previous);
                            drawing.DrawVuLine(previous.Projection(), mid.Projection(), getColorByVisibility(Visibilty.VISIBLE_UP),bmp);
                            drawTriangles(bmp, currLine.Count - 1, current, getColorByVisibility(Visibilty.VISIBLE_UP));
                            updateHorizons(previous, mid);
                        }
                        else if (previous.Visibility == Visibilty.VISIBLE_DOWN)
                        {
                            var mid = antiIntersect(current, previous);
                            drawing.DrawVuLine(previous.Projection(), mid.Projection(), getColorByVisibility(Visibilty.VISIBLE_DOWN),bmp);
                            drawTriangles(bmp, currLine.Count - 1, current, getColorByVisibility(Visibilty.VISIBLE_DOWN));
                            updateHorizons(previous, mid);
                        }
                    }

                    previous = current;
                }

                prevLine = currLine;
            }


            canvas.Image = bmp;
        }

        public void ReDraw()
        {
            switch (displayType)
            {
                case DISPLAYTYPE.TRIANGLES:
                    floatingHorizonByTriangles();
                    break;
                case DISPLAYTYPE.LINES:
                    floatingHorizonByLines();
                    break;
                case DISPLAYTYPE.NET:
                    floatingHorizonByNet();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
