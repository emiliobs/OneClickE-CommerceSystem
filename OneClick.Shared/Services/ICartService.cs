using OneClick.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneClick.Shared.Services;

public interface ICartService
{
    /// Event to notify when something is added (The "Bell")
    event Action OnChange;

    // Add item to the database
    Task AddTCart(CartItem cartItem);

    // Get the total number of items
}