using NewsApp.Models;
using NewsApp.Services;

namespace NewsApp.Pages;

public partial class NewsHomePage : ContentPage
{
	public List<Article> ArticleList;
    public List<Category> CategoryList = new List<Category>()
    {
        new Category(){Name="World", ImageUrl = "world.png"},
        new Category(){Name = "Nation" , ImageUrl="nation.png"},
        new Category(){Name = "Business" , ImageUrl="business.png"},
        new Category(){Name = "Technology" , ImageUrl="technology.png"},
        new Category(){Name = "Entertainment", ImageUrl = "entertainment.png"},
        new Category(){Name = "Sports" , ImageUrl="sports.png"},
        new Category(){Name = "Science", ImageUrl= "science.png"},
        new Category(){Name = "Health", ImageUrl="health.png"},
    };
    public NewsHomePage()
	{
		InitializeComponent();
        GetBreakingNews(); // Making API call to the category we want to see and display articles for the User to see
        ArticleList = new List<Article>();
        CvCategories.ItemsSource = CategoryList; // Setting ItemsSource to the CollectionView here allows us to call the attributes of Category when DataBinding to xaml / ui display
	}
	private async Task GetBreakingNews()
	{
        // Instaniate the ApiService class
		var apiService = new ApiService();

        // Make a call to the GNews API using GetNews method. This method requires the name of the Category selected to hit that endpoint of the API
        // We store the List of Articles retrieved from that endpoint in the variable newsResult
		var newsResult = await apiService.GetNews("Sports");

        // Loop through each Article in the List and Add them to the ArticleList (line 8) so we can display it to the CollectionView
		foreach (var item in newsResult.Articles)
		{
			ArticleList.Add(item);
		}

        //Setting CollectionView called CvNews to the list of articles got from looping newsResult.Articles. This allows users to see articles.
		CvNews.ItemsSource = ArticleList;
	}

    private void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Get selected Category from the CollectionView and explicitly cast it as type Category
        var selectedCategory = e.CurrentSelection.FirstOrDefault() as Category;

        // If the cast is unsuccessful, exit this method without doing anything
        if (selectedCategory == null)
            return;

        // Otherwise, lets go to the next page
        Navigation.PushAsync(new NewsListPage(selectedCategory.Name));

        // Deselect the item in the CollectionView by setting the SelectedItem property to null.
        ((CollectionView)sender).SelectedItem = null;
    }
}