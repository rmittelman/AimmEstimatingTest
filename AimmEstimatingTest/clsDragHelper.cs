using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AimmEstimatingTest
{
    
    public static class clsDragHelper
    {
        public struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }
        [DllImport("user32.dll")]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        public static Bitmap GetRowImage(GridView view, int rowHandle)
        {
            GridViewInfo gvi = view.GetViewInfo() as GridViewInfo;
            GridDataRowInfo ri = gvi.RowsInfo.OfType<GridDataRowInfo>().Where(r => r.RowHandle == rowHandle).FirstOrDefault();
            Bitmap rowBmp = null;
            if(ri != null)
            {
                Bitmap gridBmp = new Bitmap(view.GridControl.Width, view.GridControl.Height);
                view.GridControl.DrawToBitmap(gridBmp, new Rectangle(0, 0, view.GridControl.Width, view.GridControl.Height));
                float[][] matrixItems = new float[][]
                {
                    new float[] { 1, 0, 0, 0, 0 },
                    new float[] { 0, 1, 0, 0, 0 },
                    new float[] { 0, 0, 1, 0, 0 },
                    new float[] { 0, 0, 0, 0.7F, 0 },
                    new float[] { 0, 0, 0, 0, 1 }
                };
                ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
                ImageAttributes attr = new System.Drawing.Imaging.ImageAttributes();
                attr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                rowBmp = new Bitmap(ri.DataBounds.Width, ri.DataBounds.Height);
                Graphics gr = Graphics.FromImage(rowBmp);
                Rectangle imageBounds = new Rectangle(Point.Empty, ri.DataBounds.Size);
                gr.DrawImage(gridBmp, imageBounds, ri.DataBounds.X, ri.DataBounds.Y, 
                    ri.DataBounds.Width, ri.DataBounds.Height, GraphicsUnit.Pixel, attr);
            }
            return rowBmp;
        }

        public static Cursor CreateCursor(Bitmap bmp, Point hotspot)
        {
            if(bmp == null)
            {
                return Cursors.Default;
            }

            IntPtr ptr = bmp.GetHicon();
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.fIcon = false;
            tmp.xHotspot = hotspot.X;
            tmp.yHotspot = hotspot.Y;
            ptr = CreateIconIndirect(ref tmp);
            return new Cursor(ptr);
        }

        //public static Cursor GetDragCursor(TreeListNode node, Point e)
        //{
        //    DevExpress.XtraTreeList.ViewInfo.RowInfo ri = node.TreeList.ViewInfo.RowsInfo(node);
        //    Rectangle imageBounds = new Rectangle(new Point(0, 0), ri.  .DataBounds.Size);
        //    Bitmap treeBitmap = new Bitmap(node.TreeList.Bounds.Width, node.TreeList.Bounds.Height);
        //    node.TreeList.DrawToBitmap(treeBitmap, new Rectangle(0, 0, node.TreeList.Bounds.Width, node.TreeList.Bounds.Height));
        //    Bitmap result = new Bitmap(imageBounds.Width, imageBounds.Height);
        //    Graphics resultGraphics = Graphics.FromImage(result);
        //    float[][] matrixItems = { new float[] { 1, 0, 0, 0, 0 }, new float[] { 0, 1, 0, 0, 0 }, new float[] { 0, 0, 1, 0, 0 }, new float[] { 0, 0, 0, 0.7F, 0 }, new float[] { 0, 0, 0, 0, 1 } };
        //    ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
        //    ImageAttributes imageAttributes = new ImageAttributes();
        //    imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        //    resultGraphics.DrawImage(treeBitmap, imageBounds, ri.DataBounds.X, ri.DataBounds.Y, ri.DataBounds.Width, ri.DataBounds.Height, GraphicsUnit.Pixel, imageAttributes);
        //    Point offset = new Point(e.X - ri.DataBounds.X, e.Y - ri.DataBounds.Y);
        //    return CreateCursor(result, offset);
        //}

        //public static Cursor GetDragCursor(DevExpress.XtraGrid.GridControl.TreeListNode node, Point e)
        //{
        //    DevExpress.XtraTreeList.ViewInfo.RowInfo ri = node.TreeList.ViewInfo.RowsInfo(node);
        //    Rectangle imageBounds = new Rectangle(new Point(0, 0), ri.  .DataBounds.Size);
        //    Bitmap treeBitmap = new Bitmap(node.TreeList.Bounds.Width, node.TreeList.Bounds.Height);
        //    node.TreeList.DrawToBitmap(treeBitmap, new Rectangle(0, 0, node.TreeList.Bounds.Width, node.TreeList.Bounds.Height));
        //    Bitmap result = new Bitmap(imageBounds.Width, imageBounds.Height);
        //    Graphics resultGraphics = Graphics.FromImage(result);
        //    float[][] matrixItems = { new float[] { 1, 0, 0, 0, 0 }, new float[] { 0, 1, 0, 0, 0 }, new float[] { 0, 0, 1, 0, 0 }, new float[] { 0, 0, 0, 0.7F, 0 }, new float[] { 0, 0, 0, 0, 1 } };
        //    ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
        //    ImageAttributes imageAttributes = new ImageAttributes();
        //    imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        //    resultGraphics.DrawImage(treeBitmap, imageBounds, ri.DataBounds.X, ri.DataBounds.Y, ri.DataBounds.Width, ri.DataBounds.Height, GraphicsUnit.Pixel, imageAttributes);
        //    Point offset = new Point(e.X - ri.DataBounds.X, e.Y - ri.DataBounds.Y);
        //    return CreateCursor(result, offset);
        //}
    }
}
