using System;
using System.Linq;
using CitSciScribe.Models;

namespace CitSciScribe.Data
{
    public class CardManager
    {
        public static Card GetCardToTranscribe(DBContext context, CollectionProject collectionProject)
        {
            return context.Cards
                .Where(c => c.TranscriptionState == CardTranscriptionState.NeedsTranscription &&
                            c.CollectionProject == collectionProject).OrderBy(x => Guid.NewGuid()).First();
        }

        public static Card GetCardToApprove(DBContext context, CollectionProject collectionProject)
        {
            // if (context.Cards.Count(c => c.TranscriptionState == CardTranscriptionState.TranscriptionAwaitingApproval && c.CollectionProject == collectionProject && c.Transcriptions.Count > 0) > 0)
            return context.Cards.Where(c =>
                c.TranscriptionState == CardTranscriptionState.TranscriptionAwaitingApproval &&
                c.CollectionProject == collectionProject && c.Transcriptions.Count > 0).First();
            //else
            //  return null;
        }

        public static void UpdateCardState(DBContext context, int cardId, CardTranscriptionState state)
        {
            context.Cards.Where(c => c.Id == cardId).Single().TranscriptionState = state;
        }

        public static void SkipCard(DBContext context, int cardId)
        {
            var card = context.Cards.Where(c => c.Id == cardId).Single();
            /*
            if (card.Priority == TranscriptionPriority.Immediate)
                card.Priority = TranscriptionPriority.High;
            else if (card.Priority == TranscriptionPriority.High)
                card.Priority = TranscriptionPriority.Medium;
            else if (card.Priority == TranscriptionPriority.Medium)
                card.Priority = TranscriptionPriority.Low;
                */
        }
    }
}