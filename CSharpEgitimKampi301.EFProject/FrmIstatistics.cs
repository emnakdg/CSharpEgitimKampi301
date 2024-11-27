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
    public partial class FrmIstatistics : Form
    {
        public FrmIstatistics()
        {
            InitializeComponent();
        }

        EgitimKampiEfTravelDbEntities db = new EgitimKampiEfTravelDbEntities();
        private void FrmIstatistics_Load(object sender, EventArgs e)
        {
            lblLocationCount.Text = db.Location.Count().ToString();
            lblSumCapacity.Text = db.Location.Sum(x => x.Capacity).ToString();
            lblRehberSayisi1.Text = db.Guide.Count().ToString();
            lblOrtalamaKapasite.Text = db.Location.Average(x => x.Capacity).ToString();
            lblOrtalamaTurFiyatı1.Text = db.Location.Average(x => x.Price).ToString() + " TL";

            int lastCountryId = db.Location.Max(x => x.LocationId);
            lblEklenenSonUlke.Text = db.Location.Where(x => x.LocationId== lastCountryId).Select(y => y.Country).FirstOrDefault();

            lblKapadokyaTurKapasitesi.Text = db.Location.Where(x => x.City == "Kapadokya").Select(y => y.Capacity).FirstOrDefault().ToString();

            lblTurkiyeOrtalamaKapasite.Text = db.Location.Where(x => x.Country == "Türkiye").Average(y => y.Capacity).ToString();

            var romaRehberId = db.Location.Where(x => x.City == "Roma Turistik").Select(y => y.GuideId).FirstOrDefault();
            lblRomaGeziRehberi.Text = db.Guide.Where(x => x.GuideId ==romaRehberId).Select(y => y.GuideName + " " + y.GuideSurname).FirstOrDefault().ToString();

            var maxCapacity = db.Location.Max(x => x.Capacity);
            lblMaksimumKapasiteliLokasyon.Text = db.Location.Where(x => x.Capacity == maxCapacity).Select(y => y.City).FirstOrDefault().ToString();

            var maxPrice = db.Location.Max(x => x.Price);
            lblEnPahaliTur.Text = db.Location.Where(x => x.Price == maxPrice).Select(y => y.City).FirstOrDefault().ToString();

            var guideIdByNameAysegulCinar = db.Guide.Where(x => x.GuideName == "Ayşegül" && x.GuideSurname == "Çınar").Select(y=>y.GuideId).FirstOrDefault();
            lblAysegulCinarLokasyonSayisi1.Text = db.Location.Where(x => x.GuideId == guideIdByNameAysegulCinar).Count().ToString();

        }
    }
}
