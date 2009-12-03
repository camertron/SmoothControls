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
using WildMouse.Graphics;

namespace WildMouse.SmoothControls
{
    internal class FontVault
    {
        public enum AvailableFonts
        {
            MyriadPro = 0,
            MyriadProBold = 1
        }

        private static string[] C_FILE_NAMES = { "MyriadPro-Regular.ttf", "MyriadPro-Bold.ttf" };
        private static string C_RESOURCE_BASE = "SmoothControls.Resources.";
        private const int C_FONT_COUNT = 2;
        private SmoothFont[] m_asFonts;
        private static object c_objVaultLock = typeof(FontVault);
        private static FontVault c_Vault;

        //private constructor so no outside classes can create instances
        private FontVault()
        {
            m_asFonts = new SmoothFont[C_FONT_COUNT];

            for (int i = 0; i < C_FONT_COUNT; i ++)
                m_asFonts[i] = null;
        }

        public static FontVault GetFontVault()
        {
            //prevent two concurrent calls to GetFontVault() just in case
            lock (c_objVaultLock)
            {
                if (c_Vault == null)
                    c_Vault = new FontVault();

                return c_Vault;
            }
        }

        public Font GetFont(AvailableFonts afFontChoice, int iSize)
        {
            int iFontIndex = (int)afFontChoice;
            PrivateFontCollection pfc;

            if (m_asFonts[iFontIndex] == null)
            {
                m_asFonts[iFontIndex] = new SmoothFont(C_FILE_NAMES[iFontIndex]);
                pfc = PrepFont(C_FILE_NAMES[iFontIndex]);
                m_asFonts[iFontIndex].Base = pfc.Families[0];
            }

            if (! m_asFonts[iFontIndex].FontsBySize.ContainsKey(iSize))
                m_asFonts[iFontIndex].FontsBySize.Add(iSize, new Font(m_asFonts[iFontIndex].Base, iSize));

            return m_asFonts[iFontIndex].FontsBySize[iSize];
        }

        private void PrepFont(string FontFileName, PrivateFontCollection pfc)
        {
            Assembly _assembly = Assembly.GetExecutingAssembly();
            Stream fontStream = _assembly.GetManifestResourceStream(C_RESOURCE_BASE + FontFileName);

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

        private PrivateFontCollection PrepFont(string sFontFileName)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();

            PrepFont(sFontFileName, pfc);

            return pfc;
        }
    }

    internal class SmoothFont
    {
        private string m_sFileName;
        private Dictionary<int, Font> m_dfFontsBySize;
        private FontFamily m_ffBase;

        public SmoothFont(string sInitFileName)
        {
            m_sFileName = sInitFileName;
            m_dfFontsBySize = new Dictionary<int, Font>();
        }

        public string FileName
        {
            get { return m_sFileName; }
            set { m_sFileName = value; }
        }

        public Dictionary<int, Font> FontsBySize
        {
            get { return m_dfFontsBySize; }
        }

        public FontFamily Base
        {
            get { return m_ffBase; }
            set { m_ffBase = value; }
        }
    }

    public class General
    {
        private static string C_RESOURCE_BASE = "SmoothControls.Resources.";

        public static Bitmap GetBitmap(string sImageFileName)
        {
            Assembly _assembly = Assembly.GetExecutingAssembly();
            Stream _imageStream = _assembly.GetManifestResourceStream(C_RESOURCE_BASE + sImageFileName);
            return new Bitmap(_imageStream);
        }

        public static void DrawDropShadow(System.Drawing.Graphics gCanvas, Rectangle rImageArea, Color cStartColor, Color cEndColor, int iShadowWidth)
        {
            Color[] acColors = Gradient.ComputeGradient(cStartColor, cEndColor, iShadowWidth);
            Pen pShadowPen = new Pen(Color.Black);

            rImageArea.Width += ((iShadowWidth * 2) - 1);
            rImageArea.Height += ((iShadowWidth * 2) - 1);
            rImageArea.X -= iShadowWidth;
            rImageArea.Y -= iShadowWidth;

            for (int i = 0; i < iShadowWidth; i ++)
            {
                pShadowPen.Color = acColors[i];
                gCanvas.DrawRectangle(pShadowPen, rImageArea);
                rImageArea.X ++;
                rImageArea.Y ++;
                rImageArea.Width -= 2;
                rImageArea.Height -= 2;
            }
        }

        //adapted from http://www.codeproject.com/KB/GDI-plus/measurestring.aspx
        public static SizeF MeasureDisplayStringWidth(System.Drawing.Graphics gCanvas, string sText, Font fntFont)
        {
            System.Drawing.StringFormat sfFormat = new System.Drawing.StringFormat();
            System.Drawing.RectangleF rtRect = new System.Drawing.RectangleF(0, 0,
                                                                          1000, 1000);
            System.Drawing.CharacterRange[] craRanges = { new System.Drawing.CharacterRange(0, sText.Length) };
            System.Drawing.Region[] raRegions = new System.Drawing.Region[1];

            sfFormat.SetMeasurableCharacterRanges(craRanges);

            raRegions = gCanvas.MeasureCharacterRanges(sText, fntFont, rtRect, sfFormat);
            rtRect = raRegions[0].GetBounds(gCanvas);

            SizeF sfFinal = new SizeF();
            sfFinal.Width = (rtRect.Right + 1.0f);
            sfFinal.Height = (rtRect.Bottom);

            return sfFinal;
        }
    }

    public class StringCollectionWithEvents : StringCollection
    {
        public delegate void ItemAddedEventHandler(object sender, string sNewStr);
        public delegate void ItemRemovedEventHandler(object sender, int iIndex, string sItemText);
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

        public new void AddRange(string[] saRange)
        {
            for (int i = 0; i < saRange.Length; i ++)
                Add(saRange[i]);
        }

        public new void RemoveAt(int iIndex)
        {
            string sRemovedTxt = base[iIndex];
            base.RemoveAt(iIndex);

            if (ItemRemoved != null)
                ItemRemoved(this, iIndex, sRemovedTxt);
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
