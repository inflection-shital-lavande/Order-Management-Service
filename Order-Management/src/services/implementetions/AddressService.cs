using AutoMapper;
using Microsoft.EntityFrameworkCore;

using order_management.auth;
using order_management.common;
using order_management.database;
using order_management.database.dto;
using order_management.database.models;
using order_management.services.interfaces;
using Order_Management.src.database.dto.cart;

namespace order_management.services.implementetions;

public class AddressService : IAddressService
{
    private readonly OrderManagementContext _context;
    private readonly IMapper _mapper;

    public AddressService(OrderManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }




    public async Task<List<AddressResponseModel>> GetAll() =>
    

        _mapper.Map<List<AddressResponseModel>>(await _context.Addresses.ToListAsync());
    
       /* return await _context.Addresses
            
            .Select(a => _mapper.Map<Address>(a))
            .ToListAsync();*/
    
    public async Task<AddressResponseModel> GetById(Guid id)
    {
        var address = await _context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        return address != null ? _mapper.Map<AddressResponseModel>(address) : null;
    }

    
    public async Task<AddressSearchResultsModel> Search(AddressSearchFilterModel filter)
    {
        if (_context.Addresses == null)
            return new AddressSearchResultsModel { Items = new List<AddressResponseModel>() };

        var query = _context.Addresses.AsQueryable();

        if (!string.IsNullOrEmpty(filter.AddressLine1))
            query = query.Where(a => a.AddressLine1.Contains(filter.AddressLine1));
        if (!string.IsNullOrEmpty(filter.AddressLine2))
            query = query.Where(a => a.AddressLine2.Contains(filter.AddressLine2));
        if (!string.IsNullOrEmpty(filter.City))
            query = query.Where(a => a.City.Contains(filter.City));
        if (!string.IsNullOrEmpty(filter.State))
            query = query.Where(a => a.State.Contains(filter.State));
        if (!string.IsNullOrEmpty(filter.Country))
            query = query.Where(a => a.Country.Contains(filter.Country));
        if (!string.IsNullOrEmpty(filter.ZipCode))
            query = query.Where(a => a.ZipCode.Contains(filter.ZipCode));

        var addresses = await query.ToListAsync();
        var results = _mapper.Map<List<AddressResponseModel>>(addresses);

        return new AddressSearchResultsModel { Items = results };
    }

    public async Task<AddressResponseModel> Create(AddressCreateModel create)
    {
        var address = _mapper.Map<Address>(create);
        address.CreatedAt = DateTime.UtcNow;
        address.UpdatedAt = DateTime.UtcNow;

        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        return _mapper.Map<AddressResponseModel>(address);
    }

    public async Task<AddressResponseModel> Update(Guid id, AddressUpdateModel update)
    {
        var address = await _context.Addresses.FindAsync(id);
        if (address == null) return null;

        _mapper.Map(update, address);
        address.UpdatedAt = DateTime.UtcNow;

        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();

        return _mapper.Map<AddressResponseModel>(address);

    }
    
    public async Task<bool> Delete(Guid id)
    {
        var address = await _context.Addresses.FindAsync(id);
        if (address == null) return false;

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();

        return true;
    }

   
}






/*using AutoMapper;
using Microsoft.EntityFrameworkCore;

using order_management.auth;
using order_management.common;
using order_management.database;
using order_management.database.dto;
using order_management.database.models;
using order_management.services.interfaces;

namespace order_management.services.implementetions;

public class AddressService : IAddressService
{
    private readonly OrderManagementContext _context;
    private readonly IMapper _mapper;

    public AddressService(OrderManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AddressResponseDTO> GetAddressById(Guid id) =>
        _mapper.Map<AddressResponseDTO>(await _context.Addresses.FindAsync(id));

    public async Task<List<AddressResponseDTO>> GetAll() =>

       _mapper.Map<List<AddressResponseDTO>>(await _context.Addresses.ToListAsync());




    public async Task<IResult> CreateAddress(AddressCreateDTO addressCreateDTO)
    {
        _context.Addresses.Add(_mapper.Map<Address>(addressCreateDTO));
        await _context.SaveChangesAsync();
        return Results.Ok("create");
    }


   


    
    public async Task<IResult> UpdateAddress(Guid id, AddressUpdateDTO request)
    {
        var existingCoupon = await _context.Addresses.FindAsync(id);
        if (existingCoupon == null)
        {
            return Results.NotFound( request);
        }

        
        _mapper.Map(request, existingCoupon);

        try
        {
            _context.Addresses.Update(existingCoupon);
            await _context.SaveChangesAsync();
            return Results.Ok("update");
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception( ex,"failure", "not found"); 
        }
    }

    

    public async Task<IResult> DeleteAddress(Guid id)
    {
        var address = await _context.Addresses.FindAsync(id);
        if (address == null)
        {
            return ApiResponse.NotFound("Failure", "Address not found." );
        }

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();

        return ApiResponse.Success("Success", "Address deleted successfully.");
    }


    /*public ICollection<Customer> GetCustomerByAddress(Guid id)
    {
        return _context.CustomerAddresses.Where(e => e.AddressId == id).Select(c => c.Customer).ToList();


    }
public async Task<List<AddressSearchResultsDTO>> SearchAddress(AddressSearchFilterDTO filter)
{

    var query = _context.Addresses.AsQueryable();

    if (!string.IsNullOrEmpty(filter.AddressLine1))
        query = query.Where(a => a.AddressLine1.Contains(filter.AddressLine1));

    if (!string.IsNullOrEmpty(filter.AddressLine2))
        query = query.Where(a => a.AddressLine2.Contains(filter.AddressLine2));


    if (!string.IsNullOrEmpty(filter.City))
        query = query.Where(a => a.City.Contains(filter.City));


    if (!string.IsNullOrEmpty(filter.State))
        query = query.Where(a => a.State.Contains(filter.State));

    if (!string.IsNullOrEmpty(filter.Country))
        query = query.Where(a => a.Country.Contains(filter.Country));

    if (!string.IsNullOrEmpty(filter.ZipCode))
        query = query.Where(a => a.ZipCode.Contains(filter.ZipCode));




    var results = await query
        .Select(a => _mapper.Map<AddressSearchResultsDTO>(a))
        .ToListAsync();

    return results;
}

  
}



/*  public async Task<Response> UpdateAddressAsync(Guid id, addressUpdateDTO request)
   {
       _context.Addresses.Update(_mapper.Map<Address>(request));
       await _context.SaveChangesAsync();
       return new Response(true, "Update");
   }*/



/* public async Task<IResult> DeleteAddressAsync(Guid id)
    {
        var cart = await _context.Addresses.FindAsync(id);
        if (cart == null)
            return Results.Ok( "Cart not found");
        _context.Addresses.Remove(cart);
        await _context.SaveChangesAsync();
        return Results.Ok( "Cart deleted successfully");
    }*/


/*  public async Task<IResult> DeleteAddressAsync(Guid id)
   {
       _context.Addresses.Remove(await _context.Addresses.FindAsync(id));
       await _context.SaveChangesAsync();
          return Results.Ok("delete");
   }*/