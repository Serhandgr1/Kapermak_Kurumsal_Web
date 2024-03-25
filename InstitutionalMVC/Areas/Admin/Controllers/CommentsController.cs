using AutoMapper;
using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        GenericRequests<CommentDTO> genericRequests = new GenericRequests<CommentDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;
        public CommentsController(IDistributedCache cache, IMapper mapper)
        {
            _cache = cache;
            _mapper = mapper;
        }
        public IActionResult Index(string? posts)
        {
            ViewBag.Message = posts;
         
             return View();
        }
        public async Task<IActionResult> GetAllCommendIndex(string? updated)
        {
            try {

                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    ViewBag.Message = updated;
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    List<CommentsClientDto> CommetsClientDtos = new List<CommentsClientDto>();
                    var data = await genericRequests.GetHttpRequest("api/Commend/get-all-commend");
                    foreach (var item in data)
                    {
                        var clientComment = _mapper.Map<CommentsClientDto>(item);
                        clientComment.TrLangueDetail = await addJsonLanguage.GetValue(item.CommentDetail, "tr-TR");
                        clientComment.EnLangueDetail = await addJsonLanguage.GetValue(item.CommentDetail, "en-US");
                        CommetsClientDtos.Add(clientComment);
                    }
                    return View("GetCommentIndex", CommetsClientDtos);
                }
                else return RedirectToAction("Index", "Login");

            }
            catch { return RedirectToAction("Index", "Comments"); }
            
        }
        public async Task<IActionResult> GetUpdateIndex(CommentsClientDto comment) 
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    string image = comment.CommentImage.Remove(comment.CommentImage.Length - 4);
                    comment.CommentImage = image;
                    return View("UpdateCommentIndex", comment);
                }
                else return RedirectToAction("Index", "Login");

            } catch {return RedirectToAction("GetAllCommendIndex", "Comments"); }
           
        }
        public async Task<IActionResult> PostsComment(CommentDTO comment)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    await addJsonLanguage.TrSetLanguage(comment.CommentName, comment.CommentName);
                    await addJsonLanguage.TrSetLanguage(comment.CommentDetail, comment.CommentDetail);
                    await addJsonLanguage.TrSetLanguage(comment.CommentTitle, comment.CommentTitle);
                    await addJsonLanguage.EnSetLanguage(comment.CommentTitle, comment.CommentTitle);
                    await addJsonLanguage.EnSetLanguage(comment.CommentDetail, comment.CommentDetail);
                    await addJsonLanguage.EnSetLanguage(comment.CommentName, comment.CommentName);
                    switch (comment.CommentImage)
                    {
                        case "Kadın": comment.CommentImage = "Kadın.jpg"; break;
                        case "Erkek": comment.CommentImage = "Erkek.jpg"; break;
                    }

                    string posts = await genericRequests.PostRequestGeneric("api/Commend/post-commend", comment);
                    return RedirectToAction("Index", "Comments", new { posts = posts });
                }
                else return RedirectToAction("Index", "Login");

            }
            catch { return RedirectToAction("Index", "Comments", new { posts = "Başarısız" }); }
            
        }
        public async Task<IActionResult> DeleteComment(int id)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    var category = genericRequests.GetByIdGeneric("api/Commend/get-commend-by-id", id);
                    await addJsonLanguage.DeleteLanguage(category.Result.CommentName);
                    await addJsonLanguage.DeleteLanguage(category.Result.CommentDetail);
                    await addJsonLanguage.DeleteLanguage(category.Result.CommentTitle);
                    string delete = await deleteRequest.DeleteRequestGeneric("api/Commend/delete-commend", id);
                    return RedirectToAction("GetAllCommendIndex", "Comments", new { updated = delete });
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllCommendIndex", "Comments"); }
            
        }
        public async Task<IActionResult> UpdateComment(CommentsClientDto comment)
        { try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    var commentUpdated = genericRequests.GetByIdGeneric("api/Commend/get-commend-by-id", comment.Id).Result;
                    await addJsonLanguage.UpdateLangue(commentUpdated.CommentDetail, comment.TrLangueDetail, comment.EnLangueDetail);
                    switch (comment.CommentImage)
                    {
                        case "Kadın": comment.CommentImage = "Kadın.jpg"; break;
                        case "Erkek": comment.CommentImage = "Erkek.jpg"; break;
                    }
                    var commentsDto = _mapper.Map<CommentDTO>(comment);
                    var update = await genericRequests.UpdateRequestGeneric("api/Commend/update-commend", commentsDto);
                    return RedirectToAction("GetAllCommendIndex", "Comments", new { updated = update });
                }
                else return RedirectToAction("Index", "Login");

            }
            catch { return RedirectToAction("GetAllCommendIndex", "Comments"); }
        }
    }
}