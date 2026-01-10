using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace gdgoc_aspnet;

public class User
{

    [Key]
    public Guid id {get;set;}
    public string? email {get;set;}
    public string? password {get;set;}

    [MaxLength(15)]
    public string? first_name {get;set;}
    [MaxLength(15)]
    public string? last_name {get;set;}

    public string? address {get;set;}
    public DateTime? created_at{get;set;}
    public DateTime? updated_at{get;set;}
}
