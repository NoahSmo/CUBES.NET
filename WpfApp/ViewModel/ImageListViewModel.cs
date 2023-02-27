using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Api.Models;
using Newtonsoft.Json;

namespace WpfApp.ViewModel
{
    internal class ImageListViewModel : ViewModelBase
    {
        private ObservableCollection<Image> _imagesList;
        private ObservableCollection<Article> _articlesList;

        private bool _visibilityEditMenu;
        
        private Image _selectImage;

        #region "Property"

        public ObservableCollection<Image> ImagesList
        {
            get { return _imagesList; }
            set {SetProperty(ref _imagesList , value); }
        }
        
        public ObservableCollection<Article> ArticlesList
        {
            get { return _articlesList; }
            set {SetProperty(ref _articlesList , value); }
        }

        public bool VisibilityEditMenu
        {
            get { return _visibilityEditMenu; }
            set {SetProperty(ref _visibilityEditMenu , value); }
        }
        
        public Image SelectImage
        {
            get { return _selectImage; }
            set {SetProperty(ref _selectImage , value); }
        }

        #endregion
        
        public ICommand ToggleAddMenu { get; }
        public ICommand ToggleEditMenu { get; }
                
        public ICommand SaveImageCommand { get; }
            
        public ICommand DeleteImageCommand { get; }

        public ImageListViewModel()
        {
            ToggleAddMenu = new ViewModelCommand<Object>(ExecuteToggleAddMenu);
            ToggleEditMenu = new ViewModelCommand<Image>(ExecuteToggleEditMenu);
            
            SaveImageCommand = new ViewModelCommand<Image>(ExecuteSaveImageCommand);
            
            DeleteImageCommand = new ViewModelCommand<Image>(DeleteImage);
            
            GetImages();
            GetArticles();
        }
        

        private async void GetImages()
        {
            var content = await ModeCommun.client.GetStringAsync("Image");
            ImagesList = new ObservableCollection<Image>( JsonConvert.DeserializeObject<List<Image>>(content));
        }
        
        private async void GetArticles()
        {
            var content = await ModeCommun.client.GetStringAsync("Article");
            ArticlesList = new ObservableCollection<Article>( JsonConvert.DeserializeObject<List<Article>>(content));
        }
        
        
        private void ExecuteToggleAddMenu(Object obj)
        {
            SelectImage = new Image();
            VisibilityEditMenu = true;
        }

        private void ExecuteToggleEditMenu(Image obj)
        {
            SelectImage = obj;
            VisibilityEditMenu = true;
        }
        private async void ExecuteSaveImageCommand(Image obj)
        {
            
            if (SelectImage.Id == 0)
            {
                var response = await ModeCommun.client.PostAsJsonAsync("image", SelectImage);                
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetImages();
                    VisibilityEditMenu = false;
                }
            }
            else
            {
                var response = await ModeCommun.client.PutAsJsonAsync("image/" + SelectImage.Id, SelectImage);             
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetImages();
                    VisibilityEditMenu = false;
                }
            }
        }
        
        
        private async void DeleteImage(Image obj)
        {
            if (MessageBox.Show("Are you sure you want to delete this image?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {                
                var response = await ModeCommun.client.DeleteAsync("image/" + obj.Id);
                ImagesList.Remove(obj);
            }
        }
    }
}