using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Cyber_Warrior_Csharp_Port_Açma
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        NATUPNPLib.IStaticPortMappingCollection mappings;

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                NATUPNPLib.UPnPNATClass upnpnat = new NATUPNPLib.UPnPNATClass();

                SendMessage(txtip.Handle, 0x1501, 1, "192.168.1.1");
                SendMessage(txtport.Handle, 0x1501, 1, "Örn:4444");
                SendMessage(txtdesc.Handle, 0x1501, 1, "Cyber-Warrior.Org");
                mappings = upnpnat.StaticPortMappingCollection;

            }
            catch
            {
                MessageBox.Show("Modeminizin Upnp Özelliğini Desteklemiyor Veya Devre Dışı Bırakılmış Olabilir!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void btnportac_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtip.Text) && !string.IsNullOrEmpty(txtport.Text) && !string.IsNullOrEmpty(cmbprotocol.Text) && !string.IsNullOrEmpty(txtdesc.Text))
                {
                    mappings.Add(int.Parse(txtport.Text), cmbprotocol.Text, int.Parse(txtport.Text), txtip.Text, true, txtdesc.Text);
                    MessageBox.Show("Port Açıldı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Lütfen Boş Alanları Doldurunuz!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Hata: "+ex.Message);
            }
            
        }

        private void cmbprotocol_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
    }
}
