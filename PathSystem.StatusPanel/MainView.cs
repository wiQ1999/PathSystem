using PathSystem.StatusPanel.Services;
using System.Windows.Forms;

namespace PathSystem.StatusPanel
{
    public partial class MainView : Form
    {

        private readonly BackgroundUpdateService _worker;

        public MainView()
        {
            InitializeComponent();

            new BackgroundUpdateService(MapControll, EntitiesList);
        }
    }
}
