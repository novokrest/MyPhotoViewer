using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MyPhotoViewer.DAL
{
    public class DateTimePeriod
    {
        public DateTimePeriod()
        {
            From = DateTime.MinValue;
            To = DateTime.MaxValue;
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }
    }

    [ComplexType]
    public class DateTimePeriodMetadata
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column("From")]
        public DateTime From { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column("To")]
        public DateTime To { get; set; }
    }

    public class DateTimePeriodConfiguration : EntityTypeConfiguration<DateTimePeriod>
    {
        public DateTimePeriodConfiguration()
        {
            Property(period => period.From).HasColumnType("datetime2").HasPrecision(0);
            Property(period => period.To).HasColumnType("datetime2").HasPrecision(0);
        }
    }
}