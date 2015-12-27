using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace ImitModelling
{
    public partial class Form1 : Form
    {
        private String fileName;
        private Field field;
        public Form1()
        {
            field = new Field();
            InitializeComponent();
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileName = ofd.FileName;
            }
            field.LoadRoom(fileName);
            this.pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            field.Draw(e.Graphics);
        }
    }

    public abstract class Cell
    {
        private int x;
        private int y;
        public static int r;
        static Cell()
        {
            r = 50;
        }
        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public abstract void Draw(Graphics g);
        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                x = value;
            }
        }
        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                y = value;
            }
        }
    }

    public class EmptyCell : Cell
    {
        public EmptyCell(int x, int y) : base(x, y)
        {

        }
        public override void Draw(Graphics g)
        {

        }
    }

    public class AgentCell : Cell
    {
        public AgentCell(int x, int y) : base(x, y)
        {

        }
        public override void Draw(Graphics g)
        {

        }
    }

    public class ExitCell : Cell
    {
        public ExitCell(int x, int y) : base(x, y)
        {

        }
        public override void Draw(Graphics g)
        {

        }
    }

    public class WallCell : Cell
    {
        public WallCell(int x, int y) : base(x, y)
        {

        }
        public override void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), this.X, this.Y, Cell.r, Cell.r);
        }
    }

    public class Field
    {
        private Cell[,] field;
        public Field()
        {
            field = null;
        }
        public void Draw(Graphics g)
        {
            if (field == null) return;
            for (int i = 0; i < field.GetLength(0); ++i)
            {
                for (int j = 0; j < field.GetLength(1); ++j)
                {
                    field[i, j].Draw(g);
                }
            }
        }
        public void LoadRoom(String filename)
        {
            XmlDocument doc = new XmlDocument();
            {
                doc.Load(filename);
                foreach (XmlNode node in doc.SelectNodes("room"))
                {
                    int h = 0, w = 0;
                    foreach (XmlAttribute attr in node.Attributes)
                    {
                        switch (attr.Name)
                        {
                            case "width":
                                w = Int32.Parse(attr.Value.ToString());
                                break;
                            case "height":
                                h = Int32.Parse(attr.Value.ToString());
                                break;
                            default:
                                break;
                        }
                    }
                    field = new Cell[w, h];
                    for (int i = 0; i < w; ++i)
                    {
                        for (int j = 0; j < h; ++j)
                        {
                            field[i, j] = new EmptyCell(i * Cell.r, j * Cell.r);
                        }
                    }
                    foreach (XmlNode child in node.SelectNodes("wall"))
                    {
                        //Debug.WriteLine(string.Format("{0} = {1}", child.Name, child.InnerText));
                        int xstart = 0, ystart = 0;
                        int xend = 0, yend = 0;
                        foreach (XmlAttribute attr in child.Attributes)
                        {

                            switch (attr.Name)
                            {
                                case "xstart":
                                    xstart = Int32.Parse(attr.Value.ToString());
                                    break;
                                case "ystart":
                                    ystart = Int32.Parse(attr.Value.ToString());
                                    break;
                                case "xend":
                                    xend = Int32.Parse(attr.Value.ToString());
                                    break;
                                case "yend":
                                    yend = Int32.Parse(attr.Value.ToString());
                                    break;
                                default:
                                    break;
                            }

                        }
                        for (int i = xstart; i <= xend; ++i)
                        {
                            for (int j = ystart; j <= yend; ++j)
                            {
                                field[i, j] = new WallCell(i * Cell.r, j * Cell.r);
                            }
                        }
                    }
                    //Debug.WriteLine("--------------");
                }
            }
        }
    }

    
}
