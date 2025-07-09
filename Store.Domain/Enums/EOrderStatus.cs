namespace Store.Domain.Enums;

public enum EOrderStatus
{
    WaitingDelivery = 0,
    WaitingPayment = 1,
    Approved = 2,
    Canceled = 3,
    Delivered = 4
}