using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi301.EFProject
{
    public partial class FrmLocation : Form
    {
        public FrmLocation()
        {
            InitializeComponent();
        }

        EgitimKampiEfTravelDbEntities db = new EgitimKampiEfTravelDbEntities();
        private void btnListele_Click(object sender, EventArgs e)
        {     
            dataGridView1.DataSource = (from x in db.Location
                                        select new
                                        {
                                            x.LocationId,
                                            x.City,
                                            x.Country,
                                            x.Price,
                                            x.DayNight,
                                            x.GuideId,
                                            x.Capacity
                                        }).ToList();
        }

        private void FrmLocation_Load(object sender, EventArgs e)
        {
            var values = db.Guide.Select(x => new
            {
                FullName = x.GuideName + " " + x.GuideSurname,
                x.GuideId
            }).ToList();

            cmbRehber.DisplayMember = "FullName";
            cmbRehber.ValueMember = "GuideId";
            cmbRehber.DataSource = values;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Location location = new Location();
            location.Capacity = byte.Parse(nmrKapasite.Value.ToString());
            location.City = txtSehir.Text;
            location.Country = txtUlke.Text;
            location.Price = decimal.Parse(txtFiyat.Text);
            location.DayNight = txtGunGece.Text;
            location.GuideId = int.Parse(cmbRehber.SelectedValue.ToString());
            db.Location.Add(location);
            db.SaveChanges();
            MessageBox.Show("Ekleme İşlemi Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtLocationId.Text);
            var deletedValue = db.Location.Find(id);
            db.Location.Remove(deletedValue);
            db.SaveChanges();
            MessageBox.Show("Silme İşlemi Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtLocationId.Text);
            var updatedValue = db.Location.Find(id);
            updatedValue.DayNight = txtGunGece.Text;
            updatedValue.Price = decimal.Parse(txtFiyat.Text);
            updatedValue.Capacity = byte.Parse(nmrKapasite.Value.ToString());
            updatedValue.City = txtSehir.Text;
            updatedValue.Country = txtUlke.Text;    
            updatedValue.GuideId = int.Parse(cmbRehber.SelectedValue.ToString());
            db.SaveChanges();
            MessageBox.Show("Güncelleme İşlemi Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
