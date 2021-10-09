using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.DTO;
using server.Entities;
using server.Models.Articles;
using server.Services.Articles;

namespace server.Controllers.Articles
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        public IArticleService _articleService;
        private IMapper _mapper;

        public ArticleController(IArticleService articleService, IMapper mapper)
        {
            _articleService = articleService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CreateArticle(ArticleModel model)
        {
            await _articleService.CreateArticle(model);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> LikeArticle(ArticleLikeModel model)
        {
            await _articleService.LikeArticle(model);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> SaveArticle(ArticleSaveModel model)
        {
            await _articleService.SaveArticle(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteArticle(Guid article_gd)
        {
            await _articleService.DeleteArticle(article_gd);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> UnlikeArticle(ArticleLikeModel model)
        {
            await _articleService.UnlikeArticle(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> UnsaveArticle(ArticleSaveModel model)
        {
            await _articleService.UnsaveArticle(model);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<Article>> GetArticle(Guid article_gd)
        {
            Article article = await _articleService.GetArticle(article_gd);
            return Ok(article);
        }

        [HttpGet]
        public async Task<ActionResult<List<Article>>> GetArticles()
        {
            List<Article> articles = await _articleService.GetArticles();
            return Ok(articles);
        }

        [HttpGet]
        public async Task<ActionResult<List<Article>>> GetArticlesOfUser(Guid user_gd)
        {
            List<Article> articles = await _articleService.GetArticlesOfUser(user_gd);
            return Ok(articles);
        }

        [HttpGet]
        public async Task<ActionResult<List<ArticleDTO>>> GetTop3ArticlesOfTag(string tag)
        {
            List<ArticleDTO> articles = await _articleService.GetTop3ArticlesOfTag(tag);
            return Ok(articles);
        }

        [HttpGet]
        public async Task<ActionResult<List<ArticleDTO>>> GetAllArticlesOfTag(string tag)
        {
            List<ArticleDTO> articles = await _articleService.GetAllArticlesOfTag(tag);
            return Ok(articles);
        }

        [HttpGet]
        public async Task<ActionResult<List<Article>>> GetLikedArticlesOfUser(Guid user_gd)
        {
            List<Article> articles = await _articleService.GetLikedArticlesOfUser(user_gd);
            return Ok(articles);
        }

        [HttpGet]
        public async Task<ActionResult<int>> GetLikesOfArticleCount(Guid article_gd)
        {
            int articleCount = await _articleService.GetLikesOfArticleCount(article_gd);
            return Ok(articleCount);
        }

        [HttpGet]
        public async Task<ActionResult<List<Article>>> GetSavedArticlesOfUser(Guid user_gd)
        {
            List<Article> articles = await _articleService.GetSavedArticlesOfUser(user_gd);
            return Ok(articles);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ArticleDTO>>> GetSpotlightArticles(string tag)
        {
            List<ArticleDTO> articles = await _articleService.GetSpotlightArticles(tag);
            return Ok(articles);
        }

        [HttpPut]
        public async Task<ActionResult<Article>> UpdateArticle(ArticleModel model)
        {
            Article article = await _articleService.UpdateArticle(model);
            return Ok(article);
        }
    }
}