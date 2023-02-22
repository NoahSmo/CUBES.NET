using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Api.Models;
using Newtonsoft.Json;

namespace WpfApp.ViewModel
{
    internal class CategoryListViewModel : ViewModelBase
    {
        private ObservableCollection<Category> _categorysList;

        private bool _visibilityCreateMenu;
        private bool _visibilityEditMenu;
        
        private Category _selectCategory;

        #region "Property"

        public ObservableCollection<Category> CategorysList
        {
            get { return _categorysList; }
            set {SetProperty(ref _categorysList , value); }
        }

        public bool VisibilityCreateMenu
        {
            get { return _visibilityCreateMenu; }
            set {SetProperty(ref _visibilityCreateMenu , value); }
        }

        public bool VisibilityEditMenu
        {
            get { return _visibilityEditMenu; }
            set {SetProperty(ref _visibilityEditMenu , value); }
        }
        
        public Category SelectCategory
        {
            get { return _selectCategory; }
            set {SetProperty(ref _selectCategory , value); }
        }

        #endregion
        
        public ICommand ToggleAddMenu { get; }
        public ICommand CreateCategoryCommand { get; }
        
        
        public ICommand ToggleEditMenu { get; }
        public ICommand SaveCategoryCommand { get; }
        
        
        public ICommand DeleteCategoryCommand { get; }

        public CategoryListViewModel()
        {
            ToggleAddMenu = new ViewModelCommand<Object>(ExecuteToggleAddMenu);
            CreateCategoryCommand = new ViewModelCommand<Object>(ExecuteCreateCategoryCommand);
            
            ToggleEditMenu = new ViewModelCommand<Category>(ExecuteToggleEditMenu);
            SaveCategoryCommand = new ViewModelCommand<Object>(ExecuteSaveCategoryCommand);
            
            DeleteCategoryCommand = new ViewModelCommand<Category>(DeleteCategory);
            
            GetCategorys();
        }
        

        private async void GetCategorys()
        {
            var content = await ModeCommun.client.GetStringAsync("Category");
            CategorysList = new ObservableCollection<Category>( JsonConvert.DeserializeObject<List<Category>>(content));
        }
        
        
        private void ExecuteToggleAddMenu(Object obj)
        {
            SelectCategory = new Category();
            VisibilityCreateMenu = !VisibilityCreateMenu;
        }
        private async void ExecuteCreateCategoryCommand(object obj)
        {
            var response = await ModeCommun.client.PostAsJsonAsync("category", SelectCategory);
            VisibilityCreateMenu = !VisibilityCreateMenu;
            GetCategorys();
        }

        
        private void ExecuteToggleEditMenu(Category obj)
        {
            SelectCategory = obj;
            VisibilityEditMenu = !VisibilityEditMenu;
        }
        private async void ExecuteSaveCategoryCommand(Object obj)
        {
            var response = await ModeCommun.client.PutAsJsonAsync("category/" + SelectCategory.Id, SelectCategory);
            VisibilityEditMenu = !VisibilityEditMenu;
            GetCategorys();
        }
        
        
        private async void DeleteCategory(Category obj)
        {
            var response = await ModeCommun.client.DeleteAsync("category/" + obj.Id);
            GetCategorys();
        }
    }
}