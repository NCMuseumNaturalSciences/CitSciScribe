using System;
using System.Data.Entity;
using System.Linq;
using CitSciScribe.Models;

namespace CitSciScribe.Data
{
    public class TranscriptionManager
    {
        public static void AddTranscription(DBContext context, Transcription transcription, string userId)
        {
            transcription.CreatedById = userId;
            transcription.CreatedOn = DateTime.Now;
            transcription.TranscriptionState = TranscriptionState.AwaitingApproval;
            context.Transcriptions.Add(transcription);
        }

        public static void ApproveTranscription(DBContext context, Transcription transcription, string userId)
        {
            transcription.ReviewedById = userId;
            transcription.TranscriptionState = TranscriptionState.Approved;

            context.Transcriptions.Attach(transcription);

            var entry = context.Entry(transcription);
            entry.State = EntityState.Modified;
            entry.Property(e => e.CardId).IsModified = false;
            entry.Property(e => e.CreatedById).IsModified = false;
            entry.Property(e => e.CreatedOn).IsModified = false;

            context.SaveChanges();
        }

        public static void RejectTranscription(DBContext context, int transcriptionId, string userId)
        {
            var transcription = context.Transcriptions.Where(c => c.Id == transcriptionId).Single();
            transcription.ReviewedById = userId;
            transcription.TranscriptionState = TranscriptionState.Rejected;

            context.Transcriptions.Attach(transcription);

            var entry = context.Entry(transcription);
            entry.Property(e => e.ReviewedById).IsModified = true;
            entry.Property(e => e.TranscriptionState).IsModified = true;

            context.SaveChanges();
        }

        public static ProjectStatistics GetProjectStatistics(DBContext context, CollectionProject project)
        {
            return new ProjectStatistics
            {
                TranscriptionsTotal = context.Transcriptions.Count(c => c.Card.CollectionProject == project),
                UsersContributing = context.Transcriptions.Where(c => c.Card.CollectionProject == project)
                    .Select(c => c.CreatedById).Distinct().Count()
            };
        }

        public static SiteStatistics GetSiteStatistics(DBContext context)
        {
            return new SiteStatistics
            {
                TranscriptionsTotal = context.Transcriptions.Count(),
                UsersContributing = context.Transcriptions.Select(c => c.CreatedById).Distinct().Count(),
                ProjectsTotal = 1
            };
        }
    }
}