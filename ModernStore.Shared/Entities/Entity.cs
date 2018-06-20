using System;
using System.ComponentModel.DataAnnotations;
using FluentValidator;

namespace ModernStore.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        [Key]
        public Guid Id { get; private set; }
        
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
