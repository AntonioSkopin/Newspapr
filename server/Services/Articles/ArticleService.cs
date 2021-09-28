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
        Task<List<Article>> GetTop3ArticlesOfTag(string Tag);
        Task<List<Article>> GetAllArticlesOfTag(string Tag);
        Task LikeArticle(ArticleLikeModel model);
        Task<List<Article>> GetLikedArticlesOfUser(Guid user_gd);
        Task UnlikeArticle(Guid article_gd);
        Task<int> GetLikesOfArticleCount(Guid article_gd);
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
                VALUES(NEWID(), @_title, @_content, @_postedBy, GETDATE(), @_tag, @_img_id)
            ";

            await PostQuery(createArticleQuery, new
            {
                _title = model.Title,
                _content = model.Content,
                _postedBy = model.PostedBy,
                _tag = model.Tag,
                _img_id = model.ImageID
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

        public async Task<List<Article>> GetTop3ArticlesOfTag(string Tag)
        {
            var GetTop3ArticlesOfTagQuery = 
            @"
                SELECT TOP 3 * 
                FROM Articles
                WHERE Articles.Tag = @_tag
                ORDER BY DatePosted DESC;
            ";

            return await GetManyQuery<Article>(GetTop3ArticlesOfTagQuery, new
            {
                _tag = Tag
            });
        }

        public async Task<List<Article>> GetAllArticlesOfTag(string Tag)
        {
            var GetAllArticlesOfTagQuery = 
            @"
                SELECT  * 
                FROM Articles
                WHERE Articles.Tag = @_tag
                ORDER BY DatePosted DESC;
            ";

            return await GetManyQuery<Article>(GetAllArticlesOfTagQuery, new
            {
                _tag = Tag
            });
        }

        public async Task LikeArticle(ArticleLikeModel model)
        {
            // User can only like an article once
            var likeArticleQuery =
            @"
                INSERT INTO ArticleLikes(Gd, ArticleGd, UserGd, DateLiked)
                SELECT NEWID(), @_article_gd, @_user_gd, GETDATE()
                FROM ArticleLikes
                WHERE NOT EXISTS (SELECT ArticleGd, UserGD 
                                  FROM ArticleLikes 
                                  WHERE ArticleGd = @_article_gd
                                  AND UserGd = @_user_gd)
            ";

            await PostQuery(likeArticleQuery, new
            {
                _article_gd = model.ArticleGd,
                _user_gd = model.UserGd
            });
        }

        public async Task<List<Article>> GetLikedArticlesOfUser(Guid user_gd)
        {
            var GetLikedArticlesOfUserQuery =
            @"
                SELECT article.* FROM Articles article
                LEFT JOIN ArticleLikes likedArticle on article.Gd = likedArticle.ArticleGd
                LEFT JOIN Users usr on likedArticle.UserGd = usr.Gd
                WHERE usr.Gd = @_user_gd
            ";

            return await GetManyQuery<Article>(GetLikedArticlesOfUserQuery, new
            {
                _user_gd = user_gd
            });
        }

        public async Task UnlikeArticle(Guid article_gd)
        {
            var UnlikeArticleQuery =
            @"
                DELETE FROM ArticleLikes
                WHERE ArticleGd = @_article_gd      
            ";           

            await DeleteQuery(UnlikeArticleQuery, new
            {
                _article_gd = article_gd
            });
        }

        public async Task<int> GetLikesOfArticleCount(Guid article_gd)
        {
            var GetLikesOfArticleCountQuery =
            @"
                SELECT COUNT(Gd) FROM ArticleLikes
                WHERE ArticleGd = @_article_gd
            ";

            return await GetQuery<int>(GetLikesOfArticleCountQuery, new
            {
                _article_gd = article_gd
            });  
        }
    }
}