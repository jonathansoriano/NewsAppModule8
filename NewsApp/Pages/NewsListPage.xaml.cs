using NewsApp.Models;
using NewsApp.Services;

namespace NewsApp.Pages;

public partial class NewsListPage : ContentPage
{
    private string UserSelectedCategory; // We need this to have global scope so we can tap into this value in other methods like GetBreakingNews() method
    public List<Article> ArticleList;
    public NewsListPage(String SelectedCategory)
	{
		InitializeComponent();
        this.UserSelectedCategory = SelectedCategory; // Seeting the value of UserSelectedCategory to what was passed in from NewsHomePage. Now we can use UserSelectedCategory anywhere in this class
        NewsListContentPage.Title = SelectedCategory;
        GetBreakingNews();
        ArticleList = new List<Article>();


    }

    private async Task GetBreakingNews()
    {
        // Instaniate the ApiService class
        var apiService = new ApiService();

        // Make a call to the GNews API using GetNews method. This method requires the name of the Category selected to hit that endpoint of the API
        // We store the List of Articles retrieved from that endpoint in the variable newsResult
        var newsResult = await apiService.GetNews(UserSelectedCategory); // NewsResult never gets the deserialized information?? Why?

        // Loop through each Article in the List and Add them to the ArticleList (line 8) so we can display it to the CollectionView
        foreach (var item in newsResult.Articles)
        {
            ArticleList.Add(item);
        }

        //Setting CollectionView called CvNews to the list of articles got from looping newsResult.Articles. This allows users to see articles.
        CvNewsArticles.ItemsSource = ArticleList;
    }

    private void CvNewsArticles_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Get selected Category from the CollectionView and explicitly cast it as type Category
        var selectedArticle = e.CurrentSelection.FirstOrDefault() as Article;

        // If the cast is unsuccessful, exit this method without doing anything
        if (selectedArticle == null)
            return;

        // Otherwise, lets go to the next page
        Navigation.PushAsync(new NewsDetailPage(selectedArticle));

        // Deselect the item in the CollectionView by setting the SelectedItem property to null.
        ((CollectionView)sender).SelectedItem = null;
    }
}