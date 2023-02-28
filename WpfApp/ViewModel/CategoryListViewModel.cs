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
    internal class CategoryListViewModel : ViewModelBase
    {
        private ObservableCollection<Category> _categorysList;

        private bool _visibilityEditMenu;
        
        private Category _selectCategory;

        #region "Property"

        public ObservableCollection<Category> CategorysList
        {
            get { return _categorysList; }
            set {SetProperty(ref _categorysList , value); }
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
                
        public ICommand ToggleEditMenu { get; }
        public ICommand DeleteCategoryCommand { get; }
        
        public ICommand SaveCategoryCommand { get; }               


        public CategoryListViewModel()
        {
            ToggleAddMenu = new ViewModelCommand<Object>(ExecuteToggleAddMenu);
            
            ToggleEditMenu = new ViewModelCommand<Category>(ExecuteToggleEditMenu);
            DeleteCategoryCommand = new ViewModelCommand<Category>(DeleteCategory);
            
            SaveCategoryCommand = new ViewModelCommand<Object>(ExecuteSaveCategoryCommand);
            RefreshCategory = new ViewModelCommand<object>(ExecuteRefreshCategoryCommand);
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
            VisibilityEditMenu = true;
        }

        
        private void ExecuteToggleEditMenu(Category obj)
        {
            SelectCategory = obj;
            VisibilityEditMenu = true;
        }
        private async void ExecuteSaveCategoryCommand(Object obj)
        {
            
            if (SelectCategory.Id == 0)
            {
                var response = await ModeCommun.client.PostAsJsonAsync("category", SelectCategory);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetCategorys();
                    VisibilityEditMenu = false;
                }

            }
            else
            {
                var response = await ModeCommun.client.PutAsJsonAsync("category/" + SelectCategory.Id, SelectCategory);
                GetCategorys();
                VisibilityEditMenu = false;
            }
        }
        
        
        private async void DeleteCategory(Category obj)
        {
            SelectCategory = obj;
            if (MessageBox.Show("Are you sure you want to delete this category?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                var response = await ModeCommun.client.DeleteAsync("category/" + obj.Id);
                CategorysList.Remove(SelectCategory);
            }
        }

        public ICommand RefreshCategory { get; }
        public async void ExecuteRefreshCategoryCommand(object obj)
        {
            GetCategorys();
        }
    }
}