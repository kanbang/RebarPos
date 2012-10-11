using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Input;

using Autodesk.Windows;

namespace RebarPosCommands
{
    public static class MenuUtility
    {
        public static bool MakeRibbonTab()
        {
            // Main ribbon control
            RibbonControl rc = ComponentManager.Ribbon;
            
            // Check if the tab already exists
            foreach (RibbonTab tab in rc.Tabs)
            {
                if (tab.Id == "ID_REBARPOSTAB")
                {
                    return false;
                }
            }

            // Create a new tab
            RibbonTab rt = new RibbonTab();
            rt.Title = "SI: Metraj";
            rt.Id = "ID_REBARPOSTAB";
            rc.Tabs.Add(rt);

            // Create our custom panel, add it to the ribbon tab
            RibbonPanelSource rps = new RibbonPanelSource();
            rps.Title = "Donatı Pozlandırma ve Metraj";
            RibbonPanel rp = new RibbonPanel();
            rp.Source = rps;
            rt.Panels.Add(rp);

            // Add toolbar buttons
            rps.Items.Add(new SimpleRibbonButton("Poz Düzenle", "POSEDIT ", Properties.Resources.POZEDIT, Properties.Resources.POZEDIT_32));
            rps.Items.Add(new SimpleRibbonButton("Poz Ekle", "NEWPOS ", Properties.Resources.POZEKLE));
            rps.Items.Add(new RibbonSeparator());
            rps.Items.Add(new SimpleRibbonButton("Pozları Numaralandır", "NUMBERPOS ", Properties.Resources.POZNUM, Properties.Resources.POZNUM_32));
            rps.Items.Add(new RibbonSeparator());
            rps.Items.Add(new SimpleRibbonButton("Poz İçeriğini Kopyala", "COPYPOS ", Properties.Resources.POZKOPYALA));
            rps.Items.Add(new SimpleRibbonButton("Poz İçeriğini Kopyala", "COPYPOSDETAIL ", Properties.Resources.POZKOPYALADETAY));
            rps.Items.Add(new SimpleRibbonButton("Son Poz Numarası", "LASTPOSNUMBER ", Properties.Resources.SONPOZ, Properties.Resources.SONPOZ_32));
            rps.Items.Add(new SimpleRibbonButton("Poz Balonlarını Boşalt", "EMPTYPOS ", Properties.Resources.BALONSIL, Properties.Resources.BALONSIL_32));
            rps.Items.Add(new SimpleRibbonButton("L Boyunu Göster/Gizle", "POSLENGTH ", Properties.Resources.LGOSTER));
            rps.Items.Add(new SimpleRibbonButton("Poz Numarasını Benzet", "COPYPOSNUMBER ", Properties.Resources.POZBENZET));
            rps.Items.Add(new SimpleRibbonButton("Metraja Dahil Et/Etme", "INCLUDEPOS ", Properties.Resources.POZKATKI));
            rps.Items.Add(new RibbonSeparator());
            rps.Items.Add(new SimpleRibbonButton("Metraj Tablosu Oluştur", "BOQ ", Properties.Resources.METRAJ, Properties.Resources.METRAJ_32));
            rps.Items.Add(new SimpleRibbonButton("Poz Kontrol", "POSCHECK ", Properties.Resources.POZKONTROL, Properties.Resources.POZKONTROL_32));
            rps.Items.Add(new SimpleRibbonButton("Poz Şekillerini Göster/Gizle", "TOGGLESHAPES ", Properties.Resources.SEKILGOSTER));
            rps.Items.Add(new RibbonSeparator());
            rps.Items.Add(new SimpleRibbonButton("Poz Bul/Değiştir", "POSFIND ", Properties.Resources.POZBUL, Properties.Resources.POZBUL_32));
            rps.Items.Add(new RibbonSeparator());
            rps.Items.Add(new SimpleRibbonButton("Yardım", "POSHELP ", Properties.Resources.YARDIM));

            // Set our tab to be active
            rt.IsActive = true;

            return true;
        }

        private class SimpleRibbonButton : RibbonButton
        {
            public SimpleRibbonButton(string text, string command, Bitmap smallImage, Bitmap largeImage)
            {
                Text = text;
                CommandHandler = new ButtonCommandHandler(command);
                if (smallImage != null)
                {
                    Image = GetBitmap(smallImage);
                }
                if (largeImage != null)
                {
                    LargeImage = GetBitmap(largeImage);
                }
            }
            
            public SimpleRibbonButton(string text, string command, Bitmap smallImage)
                : this(text, command, smallImage, null)
            {
                ;
            }
        }

        private class ButtonCommandHandler : ICommand
        {
            private string m_Command;

            public ButtonCommandHandler(string command)
            {
                m_Command = command;
            }

#pragma warning disable 67
            public event EventHandler CanExecuteChanged;
#pragma warning restore 67

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                Autodesk.AutoCAD.ApplicationServices.Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                doc.SendStringToExecute(m_Command, false, false, true);
            }
        }

        private static BitmapImage GetBitmap(Bitmap image)
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Png);
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = stream;
            bmp.EndInit();

            return bmp;
        }
    }
}
