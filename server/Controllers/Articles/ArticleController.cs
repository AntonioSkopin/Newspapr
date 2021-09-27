using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        [HttpDelete]
        public async Task<ActionResult> DeleteArticle(Guid article_gd)
        {
            await _articleService.DeleteArticle(article_gd);
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
        public async Task<ActionResult<List<Article>>> GetTop3ArticlesOfTag(string tag)
        {
            List<Article> articles = await _articleService.GetTop3ArticlesOfTag(tag);
            return Ok(articles);
        }

        [HttpGet]
        public async Task<ActionResult<List<Article>>> GetAllArticlesOfTag(string tag)
        {
            List<Article> articles = await _articleService.GetAllArticlesOfTag(tag);
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