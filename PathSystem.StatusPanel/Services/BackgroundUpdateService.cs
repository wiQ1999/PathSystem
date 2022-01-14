using PathSystem.Models;
using PathSystem.Models.Tables;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PathSystem.StatusPanel.Services
{
    public class BackgroundUpdateService
    {
        private BackgroundWorker _worker;

        private PictureBox _mapControll;

        private ListBox _entitiesList;

        private MapPosition[] _mapPositionModels;

        private Entity[] _entityModels;

        private Path[] _pathModels;

        public APIService API { get; set; } = new APIService();

        public BackgroundUpdateService(PictureBox mapControll, ListBox entieiesList)
        {
            _mapControll = mapControll;
            _entitiesList = entieiesList;

            _mapPositionModels = API.GetMap().ToArray();

            BuildMapControll();

            _worker = new BackgroundWorker();
            _worker.DoWork += Worker_DoWork;
            _worker.WorkerReportsProgress = true;
            _worker.ProgressChanged += Worker_ProgressChanged;
            _worker.RunWorkerAsync();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _entityModels = API.GetEntities().ToArray();
            _pathModels = API.GetPaths().ToArray();

            UpdateEntitiesControll();
            UpdateMapControll();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(1000);

                _worker.ReportProgress(0);
            }
        }

        private void BuildMapControll()
        {
            int height = _mapPositionModels.Max(p => p.PositionY) + 1;
            int with = _mapPositionModels.Max(p => p.PositionX) + 1;

            Bitmap bitmap = new(with, height);

            _mapControll.Image = bitmap;
        }

        private void UpdateMapControll()
        {
            foreach (var position in _mapPositionModels)
                ((Bitmap)_mapControll.Image).SetPixel(position.PositionX, position.PositionY, position.Value ? Color.White : Color.Black);

            foreach (var path in _pathModels)
                foreach (var point in path.PathPositions)
                    ((Bitmap)_mapControll.Image).SetPixel(point.PositionX, point.PositionY, Color.LightBlue); // TODO: zmieniać na kolor ENtity z modelu

            // TODO dodać aktualne pozycje Entities

            _mapControll.Refresh();
        }

        private void UpdateEntitiesControll()
        {
            _entitiesList.Items.Clear();

            foreach (var entitie in _entityModels)
                _entitiesList.Items.Add($"ID: {entitie.Id}; {entitie.Name}; {entitie.Guid}");
        }
    }
}
