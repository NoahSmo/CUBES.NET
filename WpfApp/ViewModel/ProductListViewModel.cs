using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModel
{
    internal class ProductListViewModel: ViewModelBase
    {

        private string _productName;

        public string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                SetProperty(ref _productName, value);
            }
        }

        public ProductListViewModel()
        {
            ProductName = "Test Louis 123";
        }

    }
}
