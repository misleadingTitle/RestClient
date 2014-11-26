using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using RestClientLib;
using Shared;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainVM getArticleListVM;
        public MainWindow()
        {
            InitializeComponent();
            getArticleListVM = new MainVM();
            this.DataContext = getArticleListVM;
            getArticleListVM.Endpoint = txtendpoint.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getArticleListVM.goForIt();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var insert = new InsertVM();
            insert.Endpoint = txtendpoint.Text;
            insert.ArticleIns = new Article()
            {
                Id = Id.Text,
                Title = Title.Text,
                Abstract = Abstract.Text,
                Category = Category.Text,
                Body = Body.Text,
                IsDeleted = false,
                IsPublished = IsPublished.IsChecked
            };
            insert.goForIt();
            txtoutput.Text = insert.Id;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Id.Text = "";
            Title.Text = "";
            Abstract.Text = "";
            Category.Text = "";
            Body.Text = "";
            IsPublished.IsChecked = false;
        }

        private void BtnGet_Click(object sender, RoutedEventArgs e)
        {
            var specific = new GetByIdVM();
            specific.Endpoint = txtendpoint.Text;
            Article result = specific.goForIt((Article)lstArticles.SelectedItem);
            GetAbstract.Text = result.Abstract;
            GetCategory.Text = result.Category;
            GetBody.Text = result.Body;
            GetIsPublished.IsChecked = result.IsPublished;
            GetIsDeleted.IsChecked = result.IsDeleted;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var delete = new DeleteVM();
            delete.Endpoint = txtendpoint.Text;
            delete.goForIt((Article)lstArticles.SelectedItem);
        }

        private void lstArticles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Article selection = (Article)lstArticles.SelectedItem;
            if (selection != null)
            {
                UpdId.Text = selection.Id;
                UpdTitle.Text = selection.Title;
                UpdAbstract.Text = selection.Abstract;
                UpdCategory.Text = selection.Category;
                UpdBody.Text = selection.Body;
                UpdIsPublished.IsChecked = false;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var update = new UpdateVM();
            update.Endpoint = txtendpoint.Text;
            update.ArticleUpd = new Article()
            {
                Id = UpdId.Text,
                Title = UpdTitle.Text,
                Abstract = UpdAbstract.Text,
                Category = UpdCategory.Text,
                Body = UpdBody.Text,
                IsDeleted = false,
                IsPublished = UpdIsPublished.IsChecked
            };
            update.goForIt();
            txtoutput.Text = update.Id;
        }
    }
}
