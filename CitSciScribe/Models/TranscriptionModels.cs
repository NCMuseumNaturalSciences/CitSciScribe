using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitSciScribe.Models
{
    public enum TranscriptionPriority
    {
        Low = 10,
        Medium = 100,
        High = 1000,
        Immediate = 10000
    }

    public enum CardTranscriptionState
    {
        NeedsTranscription = 0,
        TranscriptionAwaitingApproval = 1,
        TranscriptionApproved = 2
    }

    public enum TranscriptionState
    {
        AwaitingApproval,
        Approved,
        Rejected
    }

    public enum CollectionGroup
    {
        Ichthyology
    }

    public enum CollectionProject
    {
        IchthyologyCharleston
    }

    public enum CardType
    {
        Ichthyology1
    }

    public class Card
    {
        [Key] public int Id { get; set; }

        public DateTime UploadedOn { get; set; }
        public TranscriptionPriority Priority { get; set; }
        public CardTranscriptionState TranscriptionState { get; set; }

        [Index] public CollectionGroup CollectionGroup { get; set; }

        [Index] public CollectionProject CollectionProject { get; set; }

        public CardType CardType { get; set; }
        public virtual ICollection<Transcription> Transcriptions { get; set; }
        public string CardImagePath { get; set; }
        public string AcceptedById { get; set; }
        public virtual ApplicationUser AcceptedBy { get; set; }
        public DateTime? AcceptedOn { get; set; }
    }

    public abstract class Transcription
    {
        [Key] public int Id { get; set; }

        public string CreatedById { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public TranscriptionState TranscriptionState { get; set; }
        public int CardId { get; set; }
        public virtual Card Card { get; set; }
        public string ReviewedById { get; set; }
        public virtual ApplicationUser ReviewedBy { get; set; }
    }

    [Table("IchthyologyTranscriptions")]
    public class IchthyologyTranscription : Transcription
    {
        public string Number { get; set; }
        public string ScientificName { get; set; }
        public string Family { get; set; }
        public string CommonName { get; set; }
        public string Drainage { get; set; }
        public string Locality { get; set; }
        public string CollectedBy { get; set; }
        public string Date { get; set; }
        public string Specimens { get; set; }
        public string Measurements { get; set; }
        public string Sex { get; set; }
        public string Bottom { get; set; }
        public string Water { get; set; }
        public string DepthCollected { get; set; }
        public string Temperature { get; set; }
        public string Air { get; set; }
        public string WaterTwo { get; set; }
        public string Salinity { get; set; }
        public string Time { get; set; }
        public string Remarks { get; set; }
        public string Fluid { get; set; }
        public string Donor { get; set; }
        public string AccessNumber { get; set; }
        public string Neg { get; set; }
    }
}