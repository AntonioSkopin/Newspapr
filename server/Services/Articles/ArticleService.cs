using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using server.Entities;
using server.Models.Articles;

namespace server.Services.Articles
{
    public interface IArticleService
    {
        Task CreateArticle(ArticleModel model);
        Task<Article> UpdateArticle(ArticleModel model);
        Task DeleteArticle(Guid article_gd);
        Task<Article> GetArticle(Guid article_gd);
        Task<List<Article>> GetArticles();
        Task<List<Article>> GetArticlesOfUser(Guid user_gd);
    }
    public class ArticleService : SqlService, IArticleService
    {

        private readonly IConfiguration _configuration;

        public ArticleService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task CreateArticle(ArticleModel model)
        {
            var createArticleQuery =
            @"
                INSERT INTO Articles
                VALUES(NEWID(), @_title, @_content, @_postedBy, GETDATE())
            ";

            await PostQuery(createArticleQuery, new
            {
                _title = model.Title,
                _content = model.Content,
                _postedBy = model.PostedBy
            });
        }

        public async Task DeleteArticle(Guid article_gd)
        {
            var deleteArticleQuery =
            @"
                DELETE FROM Articles
                WHERE Gd = @_gd
            ";

            await DeleteQuery(deleteArticleQuery, new
            {
                _gd = article_gd
            });
        }

        public async Task<Article> GetArticle(Guid article_gd)
        {
            var getArticleQuery =
            @"
                SELECT * FROM Users
                WHERE Gd = @_gd
            ";

            return await GetQuery<Article>(getArticleQuery, new
            {
                _gd = article_gd
            });
        }

        public async Task<List<Article>> GetArticles()
        {
            var getArticlesQuery =
            @"
                SELECT * FROM Articles
                ORDER BY DatePosted DESC
            ";

            return await GetManyQuery<Article>(getArticlesQuery);
        }

        public async Task<List<Article>> GetArticlesOfUser(Guid user_gd)
        {
            var getArticlesOfUserQuery =
            @"
                SELECT article.* FROM Articles article
                LEFT JOIN Users usr 
                ON article.PostedBy = usr.Gd
                WHERE usr.Gd = @_userGd
                ORDER BY DatePosted DESC
            ";

            return await GetManyQuery<Article>(getArticlesOfUserQuery, new
            {
                _userGd = user_gd
            });
        }

        public async Task<Article> UpdateArticle(ArticleModel model)
        {
            var updateArticleQuery =
            @"
                UPDATE Article
                SET Title = @_title
                    Content = @_content
                WHERE Gd = @_gd
            ";

            var newArticle = await PutQuery<Article>(updateArticleQuery, new
            {
                _title = model.Title,
                _content = model.Content,
                _gd = model.Gd
            });

            return newArticle;
        }
    }
}