using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Music
{
    public partial class ucPlaylists : UserControl
    {
        private List<ClassCard> classCards; // Thuộc tính để lưu trữ danh sách ClassCard

        public ucPlaylists()
        {
            InitializeComponent();
        }

        public async void ucPlaylists_Load(object sender, EventArgs e)
        {
            await initDetails(); // Gọi phương thức initDetails() để lấy danh sách ClassCard khi form được load

        }

        private async Task initDetails()
        {
            try
            {
                ClassCard get = new ClassCard();
                classCards = await get.GetPlaylist(); // Lấy danh sách ClassCard từ phương thức GetPlaylist()
                loadCards(); // Sau khi lấy danh sách, gọi phương thức loadCards() để hiển thị
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int i;
        private void loadCards()
        {
            foreach (ClassCard data in classCards)
            {
                i++;
                CardControl cards = new CardControl();
                cards.cardDetails(data);
                cardContainer.Controls.Add(cards);
            }
        }

        private void btAddPlaylists_Click(object sender, EventArgs e)
        {
            Form background = new Form();
            try
            {
                using (AddPlaylist uc = new AddPlaylist())
                {
                    background.StartPosition = FormStartPosition.Manual;
                    background.FormBorderStyle = FormBorderStyle.None;
                    background.Opacity = .50d;
                    background.BackColor = Color.Black;
                    background.WindowState = FormWindowState.Maximized;
                    background.Location = this.Location;
                    background.ShowInTaskbar = false;
                    background.Show();

                    uc.Owner = background;
                    uc.ShowDialog();
                    background.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { background.Dispose(); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (AddPlaylist.refresh == true)
            {
                CardControl card = new CardControl();
                cardContainer.Controls.Add(card);
                AddPlaylist.refresh = false;
            }
        }

        private void deleteTimer_Tick(object sender, EventArgs e)
        {
            if(CardControl.isDeleted == true)
            {
                cardContainer.Controls.Clear();
                initDetails();
                loadCards();
                CardControl.isDeleted = false;
            }
        }
    }
}
