using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Text;
using System.Reflection;
using System.Drawing;
using System.Runtime.InteropServices;

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

        /*
        public static void PrepFont(string FontFileName, PrivateFontCollection pfc)
        {
            Assembly _assembly = Assembly.GetExecutingAssembly();

            Stream fontStream = _assembly.GetManifestResourceStream(RESOURCE_BASE + FontFileName);
            byte[] fontdata = new byte[fontStream.Length];

            fontStream.Read(fontdata, 0, (int)fontStream.Length);
            fontStream.Close();

            unsafe
            {
                fixed (byte* pFontData = fontdata)
                {
                    pfc.AddMemoryFont((System.IntPtr)pFontData, fontdata.Length);
                }
            }
        }
        */

        public static Bitmap GetBitmap(string ImageFileName)
        {
            Assembly _assembly = Assembly.GetExecutingAssembly();
            Stream _imageStream = _assembly.GetManifestResourceStream(RESOURCE_BASE + ImageFileName);
            return new Bitmap(_imageStream);
        }
    }
}
