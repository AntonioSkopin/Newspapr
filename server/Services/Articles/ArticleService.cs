using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using server.DTO;
using server.Entities;
using server.Models.Articles;

namespace server.Services.Articles
{
    public interface IArticleService
    {
        /* BASIC CRUD */
        Task CreateArticle(ArticleModel model);
        Task<Article> UpdateArticle(ArticleModel model);
        Task DeleteArticle(Guid article_gd);
        Task<Article> GetArticle(Guid article_gd);
        /* BASIC CRUD */

        Task<List<Article>> GetArticles();
        Task<List<Article>> GetArticlesOfUser(Guid user_gd);
        Task<List<ArticleDTO>> GetTop3ArticlesOfTag(string Tag);
        Task<List<ArticleDTO>> GetAllArticlesOfTag(string Tag);

        /* LIKE METHODS */
        Task LikeArticle(ArticleLikeModel model);
        Task<List<Article>> GetLikedArticlesOfUser(Guid user_gd);
        Task UnlikeArticle(ArticleLikeModel model);
        Task<int> GetLikesOfArticleCount(Guid article_gd);
        /* LIKE METHODS */

        /* SAVE METHODS */
        Task SaveArticle(ArticleSaveModel model);
        Task<List<Article>> GetSavedArticlesOfUser(Guid user_gd);
        Task UnsaveArticle(ArticleSaveModel model);
        /* SAVE METHODS */

        Task<List<ArticleDTO>> GetSpotlightArticles(string Tag);
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

            // Add image to cloudinary

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

        public async Task<List<ArticleDTO>> GetTop3ArticlesOfTag(string Tag)
        {
            var GetTop3ArticlesOfTagQuery = 
            @"
                SELECT TOP 3 article.*, usr.Fullname 
                FROM Articles article
                LEFT JOIN Users usr on article.PostedBy = usr.Gd
                WHERE article.Tag = @_tag
                ORDER BY DatePosted DESC;
            ";

            return await GetManyQuery<ArticleDTO>(GetTop3ArticlesOfTagQuery, new
            {
                _tag = Tag
            });
        }

        public async Task<List<ArticleDTO>> GetAllArticlesOfTag(string Tag)
        {
            var GetAllArticlesOfTagQuery = 
            @"
                SELECT  * 
                FROM Articles article
                LEFT JOIN Users usr on article.PostedBy = usr.Gd
                WHERE article.Tag = @_tag
                ORDER BY DatePosted DESC;
            ";

            return await GetManyQuery<ArticleDTO>(GetAllArticlesOfTagQuery, new
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

        public async Task UnlikeArticle(ArticleLikeModel model)
        {
            var UnlikeArticleQuery =
            @"
                DELETE FROM ArticleLikes
                WHERE ArticleGd = @_article_gd   
                AND UserGd = @_user_gd   
            ";           

            await DeleteQuery(UnlikeArticleQuery, new
            {
                _article_gd = model.ArticleGd,
                _user_gd = model.UserGd
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

        public async Task SaveArticle(ArticleSaveModel model)
        {
            // User can only like an article once
            var saveArticleQuery =
            @"
                INSERT INTO ArticleSaved
                VALUES(NEWID(), @_article_gd, @_user_gd, GETDATE())
            ";

            await PostQuery(saveArticleQuery, new
            {
                _article_gd = model.ArticleGd,
                _user_gd = model.UserGd
            });
        }

        public async Task<List<Article>> GetSavedArticlesOfUser(Guid user_gd)
        {
            var GetSavedArticlesOfUserQuery =
            @"
                SELECT article.* FROM Articles article
                LEFT JOIN ArticleSaved savedArticle on article.Gd = savedArticle.ArticleGd
                LEFT JOIN Users usr on savedArticle.UserGd = usr.Gd
                WHERE usr.Gd = @_user_gd
            ";

            return await GetManyQuery<Article>(GetSavedArticlesOfUserQuery, new
            {
                _user_gd = user_gd
            });
        }

        public async Task UnsaveArticle(ArticleSaveModel model)
        {
            var UnSaveArticleQuery =
            @"
                DELETE FROM ArticleSaved
                WHERE ArticleGd = @_article_gd     
                AND UserGd = @_user_gd
            ";           

            await DeleteQuery(UnSaveArticleQuery, new
            {
                _article_gd = model.ArticleGd,
                _user_gd = model.UserGd
            });
        }

        public async Task<List<ArticleDTO>> GetSpotlightArticles(string Tag)
        {
            /* 
                Summary:
                    - Show top 4 articles of given Tag
                    - Top 4 is based on:
                        - Amount of likes
                        - Amount of Saves
            */
            var GetSpotlightArticlesQuery =
            @"
                SELECT TOP 4 article.*, likedArticle.numLikes, savedArticle.numSaved, usr.Fullname 
                FROM Articles article 

                LEFT JOIN Users usr on article.PostedBy = usr.Gd

                LEFT JOIN (
                            SELECT ArticleGd, COUNT(ArticleGd) AS numLikes
                            FROM ArticleLikes
                            GROUP BY ArticleGd
                        ) likedArticle 
                ON likedArticle.ArticleGd = article.Gd

                LEFT JOIN (
                            SELECT ArticleGd, COUNT(ArticleGd) AS numSaved
                            FROM ArticleSaved
                            GROUP BY ArticleGd
                        ) savedArticle
                ON savedArticle.ArticleGd = article.Gd

                WHERE Tag = @_tag
                    OR likedArticle.numLikes IS NOT NULL
                    OR savedArticle.numSaved IS NOT NULL

                ORDER BY likedArticle.numLikes DESC, savedArticle.numSaved DESC;
            ";

            return await GetManyQuery<ArticleDTO>(GetSpotlightArticlesQuery, new
            {
                _tag = Tag
            });
        }
    }
}