using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Text;
using System.Reflection;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Specialized;

namespace WildMouse.SmoothControls
{
    internal class General
    {
        private static string RESOURCE_BASE = "SmoothControls.Resources.";

        public static void PrepFont(string FontFileName, PrivateFontCollection pfc)
        {
            Assembly _assembly = Assembly.GetExecutingAssembly();
            Stream fontStream = _assembly.GetManifestResourceStream(RESOURCE_BASE + FontFileName);

            //create an unsafe memory block for the data
            System.IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);

            //create a buffer to read in to
            byte[] fontdata = new byte[fontStream.Length];

            //fetch the font program from the resource
            fontStream.Read(fontdata, 0, (int)fontStream.Length);

            //copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, (int)fontStream.Length);

            //pass the font to the font collection
            pfc.AddMemoryFont(data, (int)fontStream.Length);

            //close the resource stream
            fontStream.Close();

            //free the unsafe memory
            Marshal.FreeCoTaskMem(data);
        }

        public static PrivateFontCollection PrepFont(string FontFileName)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();

            PrepFont(FontFileName, pfc);

            return pfc;
        }

        public static Bitmap GetBitmap(string ImageFileName)
        {
            Assembly _assembly = Assembly.GetExecutingAssembly();
            Stream _imageStream = _assembly.GetManifestResourceStream(RESOURCE_BASE + ImageFileName);
            return new Bitmap(_imageStream);
        }
    }

    public class StringCollectionWithEvents : StringCollection
    {
        public delegate void ItemAddedEventHandler(object sender, string sNewStr);
        public delegate void ItemRemovedEventHandler(object sender, int iIndex);
        public delegate void ItemChangedEventHandler(object sender, int iChangedIndex);

        public event ItemAddedEventHandler ItemAdded;
        public event ItemRemovedEventHandler ItemRemoved;
        public event ItemChangedEventHandler ItemChanged;

        public new void Add(string sNewStr)
        {
            base.Add(sNewStr);

            if (ItemAdded != null)
                ItemAdded(this, sNewStr);
        }

        public new void RemoveAt(int iIndex)
        {
            base.RemoveAt(iIndex);

            if (ItemRemoved != null)
                ItemRemoved(this, iIndex);
        }

        public new string this[int iIndex]
        {
            get { return base[iIndex]; }
            set
            {
                base[iIndex] = value;

                if (ItemChanged != null)
                    ItemChanged(this, iIndex);
            }
        }
    }
}
