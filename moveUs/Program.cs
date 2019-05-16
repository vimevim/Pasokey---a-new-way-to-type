using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace moveUs
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool kontrol;

            Mutex mutex = new Mutex(true, "Program", out kontrol); //Örnek Mutex nesnesi oluşturalım. 
            if (kontrol == false)
            {
                MessageBox.Show("Bu program zaten çalışıyor.");
                return;
            }
            Application.Run(new main());
            GC.KeepAlive(mutex); //Nesneyi kaldırıyoruz. 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
    }
}
