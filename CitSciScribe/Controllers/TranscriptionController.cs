using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CitSciScribe.Data;
using CitSciScribe.Models;
using CitSciScribe.Attrributes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CitSciScribe.Controllers
{
    public class TranscriptionController : Controller
    {
        private DBContext _context;
        private ApplicationUserManager _userManager;

        public TranscriptionController()
        {
        }

        public TranscriptionController(ApplicationUserManager userManager, DBContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        public DBContext Context
        {
            get
            {
                if (_context == null)
                    _context = new DBContext();
                return _context;
            }
        }

        // GET: Transcribe
        public async Task<ActionResult> Index()
        {
            return RedirectToAction("Projects");
            //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            //if (user.CanApprove)
            //    return RedirectToAction("Approvals");
            //else if (user.CanTranscribe)
            //    return RedirectToAction("Transcribe");
            //else
            //    return RedirectToAction("Index", "Manage");
        }

        public ActionResult Projects()
        {
            var viewModel = new ProjectsViewModel
            {
                CharlestonIcth =
                    TranscriptionManager.GetProjectStatistics(Context, CollectionProject.IchthyologyCharleston)
            };
            return View(viewModel);
        }


        [System.Web.Mvc.Authorize]
        [HasTranscribe]
        public ActionResult Transcribe(CollectionProject collectionProject)
        {
            return View(CardManager.GetCardToTranscribe(Context, collectionProject));
        }

        [System.Web.Mvc.Authorize]
        [HasApprove]
        public async Task<ActionResult> Approvals()
        {
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize]
        [HasApprove]
        public ActionResult Approve(CollectionProject collectionProject)
        {
            return View(CardManager.GetCardToApprove(Context, collectionProject));
        }

        [System.Web.Mvc.Authorize]
        [HasApprove]
        [MultipleButton(Name = "action", Argument = "Reject")]
        public ActionResult RejectTranscription(int id, int cardId)
        {
            TranscriptionManager.RejectTranscription(Context, id, User.Identity.GetUserId());
            CardManager.UpdateCardState(Context, cardId, CardTranscriptionState.NeedsTranscription);
            Context.SaveChanges();
            return RedirectToAction("Approve");
        }

        [System.Web.Mvc.Authorize]
        [HasTranscribe]
        public ActionResult SkipTranscription(int cardId, CollectionProject collectionProject)
        {
            CardManager.SkipCard(Context, cardId);
            Context.SaveChanges();
            return RedirectToAction("Transcribe", new {collectionProject});
        }

        public ActionResult SkipApproval(int cardId, CollectionProject collectionProject)
        {
            CardManager.SkipCard(Context, cardId);
            Context.SaveChanges();
            return RedirectToAction("Approve", new {collectionProject});
        }


        private void IncrementTranscriptionCount()
        {
            ((ClaimsIdentity) User.Identity).IncrementClaim("TranscriptionCount");
        }


        #region Icthyology

        [System.Web.Mvc.Authorize]
        [HasTranscribe]
        public ActionResult TranscribeIchthyology([FromBody] IchthyologyTranscription transcription,
            CollectionProject collectionProject)
        {
            IncrementTranscriptionCount();
            TranscriptionManager.AddTranscription(Context, transcription, User.Identity.GetUserId());
            CardManager.UpdateCardState(Context, transcription.CardId,
                CardTranscriptionState.TranscriptionAwaitingApproval);
            Context.SaveChanges();

            return RedirectToAction("Transcribe", new {collectionProject});
        }

        [System.Web.Mvc.Authorize]
        [HasApprove]
        public ActionResult ApproveIchthyology([FromBody] IchthyologyTranscription transcription,
            CollectionProject collectionProject)
        {
            TranscriptionManager.ApproveTranscription(Context, transcription, User.Identity.GetUserId());
            CardManager.UpdateCardState(Context, transcription.CardId, CardTranscriptionState.TranscriptionApproved);
            Context.SaveChanges();

            return RedirectToAction("Approve", new {collectionProject});
        }

        #endregion
    }
}