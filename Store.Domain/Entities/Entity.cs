using System;
using Flunt.Notifications;
namespace Store.Domain.Entities;

public class Entity : Notifiable//Notifiable é usado para notificar erros de validação
{
    public Entity()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; private set; }
}