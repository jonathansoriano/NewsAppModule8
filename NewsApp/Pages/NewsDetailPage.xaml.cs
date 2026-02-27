using NewsApp.Models;

namespace NewsApp.Pages;

public partial class NewsDetailPage : ContentPage
{
	public NewsDetailPage(Article selectedArticle)
	{
		InitializeComponent();
		ArticleImage.Source = selectedArticle.Image;
		ArticleTitleLabel.Text = selectedArticle.Title;
		ArticleContentLabel.Text = selectedArticle.Content;

	}
}